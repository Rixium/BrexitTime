using System;
using System.Collections.Generic;
using System.Linq;
using BrexitTime.Constants;
using BrexitTime.Enums;
using BrexitTime.Games;
using BrexitTime.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrexitTime.Screens
{
    public class CharacterSelectScreen : Screen
    {
        public List<UIElement> _playerButtons;

        public Character character1;
        public Character character2;
        private Audience audience;

        private float pulseTimer = 0;

        private bool c1LockedIn = false;
        private bool c2LockedIn = false;

        public int maxSelection;

        public int selectedC1;
        public int selectedC2;

        private SoundEffectInstance soundEffect;

        public override void Initialise()
        {
            var c1 = ContentChest.CharacterData[0];
            var c2 = ContentChest.CharacterData[2];

            var c1Position = new Rectangle(200, ScreenSettings.Height / 2 - ContentChest.Characters[c1.Name].Height / 2,
                ContentChest.Characters[c1.Name].Width, ContentChest.Characters[c1.Name].Height);
            var c2Position = new Rectangle(ScreenSettings.Width - 200 - ContentChest.Characters[c1.Name].Width / 2,
                c1Position.Y, ContentChest.Characters[c1.Name].Width, ContentChest.Characters[c1.Name].Height);

            character1 = new Character(ContentChest.Characters[c1.Name], ContentChest.MainFont, c1Position, c1);
            character2 = new Character(ContentChest.Characters[c2.Name], ContentChest.MainFont, c2Position, c2)
            {
                FacingLeft = true
            };

            _playerButtons = new List<UIElement>();

            var count = ContentChest.CharacterData.Count;

            var height = ContentChest.PortraitBackground.Height;
            var padding = 10;
            var requiredHeight = count * (height + padding * 2);
            var curr = 0;
            foreach (var c in ContentChest.CharacterData)
            {
                if (ContentChest.Portraits.ContainsKey(c.Name) == false) continue;

                var cPortrait = ContentChest.Portraits[c.Name];
                var cButton = new CharacterButton(c.Name, ContentChest.PortraitBackground, cPortrait,
                    new Vector2(ScreenSettings.ScreenCenter.X,
                        ScreenSettings.ScreenCenter.Y - requiredHeight / 2 + curr * (height + padding * 2)),
                    Alignment.CENTER);
                _playerButtons.Add(cButton);
                
                curr++;
            }

            selectedC2 = 1;
            SelectPlayerOne();
            SelectPlayerTwo();

            maxSelection = curr - 1;

            InputManager.RegisterGamePadButton(new InputCommand(0, Buttons.LeftThumbstickUp), PlayerOneSelect);
            InputManager.RegisterGamePadButton(new InputCommand(0, Buttons.LeftThumbstickDown), PlayerOneSelect);
            InputManager.RegisterGamePadButton(new InputCommand(1, Buttons.LeftThumbstickUp), PlayerTwoSelect);
            InputManager.RegisterGamePadButton(new InputCommand(1, Buttons.LeftThumbstickDown), PlayerTwoSelect);

            InputManager.RegisterOnKeyPress(Keys.W, PlayerOneSelect);
            InputManager.RegisterOnKeyPress(Keys.S, PlayerOneSelect);
            InputManager.RegisterOnKeyPress(Keys.Up, PlayerTwoSelect);
            InputManager.RegisterOnKeyPress(Keys.Down, PlayerTwoSelect);

            InputManager.RegisterGamePadButton(new InputCommand(0, Buttons.A), PlayerOneLockIn);
            InputManager.RegisterGamePadButton(new InputCommand(0, Buttons.B), Back);
            InputManager.RegisterGamePadButton(new InputCommand(1, Buttons.A), PlayerTwoLockIn);


            InputManager.RegisterOnKeyPress(Keys.E, (k) => PlayerOneLockIn(null));
            InputManager.RegisterOnKeyPress(Keys.Escape, (k) => Back(null));
            InputManager.RegisterOnKeyPress(Keys.Enter, (k) => PlayerTwoLockIn(null));

            soundEffect = ContentChest.Audience.CreateInstance();
            soundEffect.IsLooped = true;
            soundEffect.Play();

            audience = new Audience(ContentChest);
            
            base.Initialise();
        }
        
        private void PlayerTwoSelect(Keys obj)
        {
            if (obj == Keys.Up)
                PlayerTwoSelect(new InputCommand(1, Buttons.LeftThumbstickUp));
            else PlayerTwoSelect(new InputCommand(1, Buttons.LeftThumbstickDown));
        }

        private void PlayerOneSelect(Keys obj)
        {
            if(obj == Keys.W)
                PlayerOneSelect(new InputCommand(0, Buttons.LeftThumbstickUp));
            else PlayerOneSelect(new InputCommand(0, Buttons.LeftThumbstickDown));
        }

        private void Back(InputCommand obj)
        {
            ChangeScreen(new MainMenuScreen());
            AudioManager.OnButtonClick(null);
        }

        private void StartGame(InputCommand obj)
        {
            soundEffect.Stop(true);
            AudioManager.OnStart();
            ChangeScreen(new GameScreen(character1, character2, audience));
        }

        private void PlayerTwoLockIn(InputCommand obj)
        {
            c2LockedIn = true;
            ContentChest.SelectionClips[character2.CharacterData.Name].Play();
            AudioManager.OnSelect();
        }

        private void PlayerOneLockIn(InputCommand obj)
        {
            if (c1LockedIn && c2LockedIn)
            {
                StartGame(null);
                return;
            }

            ContentChest.SelectionClips[character1.CharacterData.Name].Play();
            AudioManager.OnSelect();
            c1LockedIn = true;
        }
        

        private void PlayerTwoSelect(InputCommand obj)
        {
            var last = selectedC2;
            switch (obj.Button)
            {
                case Buttons.LeftThumbstickUp:
                    selectedC2--;
                    break;
                case Buttons.LeftThumbstickDown:
                    selectedC2++;
                    break;
            }

            selectedC2 = MathHelper.Clamp(selectedC2, 0, maxSelection);

            var button = (CharacterButton) _playerButtons[selectedC2];
            if (button.Hovering && button.HoverColor != Color.Red * 0.5f)
            {
                if (selectedC2 == 0 || selectedC2 == maxSelection)
                    selectedC2 = last;
                else
                    switch (obj.Button)
                    {
                        case Buttons.LeftThumbstickUp:
                            selectedC2--;
                            break;
                        case Buttons.LeftThumbstickDown:
                            selectedC2++;
                            break;
                    }
            }

            selectedC2 = MathHelper.Clamp(selectedC2, 0, maxSelection);
            if (selectedC2 == last) return;
            AudioManager.OnButtonClick(null);
            SelectPlayerTwo();
            c2LockedIn = false;
            _playerButtons[last].Hover(false);
        }

        private void SelectPlayerTwo()
        {
            var button = (CharacterButton) _playerButtons[selectedC2];

            if (button.Hovering && button.HoverColor != Color.Red * 0.5f) return;

            button.HoverColor = Color.Red * 0.5f;
            _playerButtons[selectedC2].Hover(true);
            character2.CharacterData =
                ContentChest.CharacterData.First(c =>
                    c.Name.Equals(button.Character, StringComparison.OrdinalIgnoreCase));
            character2.Texture = ContentChest.Characters[button.Character];
        }

        private void PlayerOneSelect(InputCommand obj)
        {
            var last = selectedC1;
            switch (obj.Button)
            {
                case Buttons.LeftThumbstickUp:
                    selectedC1--;
                    break;
                case Buttons.LeftThumbstickDown:
                    selectedC1++;
                    break;
            }

            selectedC1 = MathHelper.Clamp(selectedC1, 0, maxSelection);

            var button = (CharacterButton) _playerButtons[selectedC1];
            if (button.Hovering && button.HoverColor != Color.Blue * 0.5f)
            {
                if (selectedC1 == 0 || selectedC1 == maxSelection)
                    selectedC1 = last;
                else
                    switch (obj.Button)
                    {
                        case Buttons.LeftThumbstickUp:
                            selectedC1--;
                            break;
                        case Buttons.LeftThumbstickDown:
                            selectedC1++;
                            break;
                    }
            }


            selectedC1 = MathHelper.Clamp(selectedC1, 0, maxSelection);

            if (selectedC1 == last) return;

            AudioManager.OnButtonClick(null);
            SelectPlayerOne();
            c1LockedIn = false;
            _playerButtons[last].Hover(false);
        }

        private void SelectPlayerOne()
        {
            var button = (CharacterButton) _playerButtons[selectedC1];
            if (button.Hovering && button.HoverColor != Color.Blue * 0.5f) return;

            button.HoverColor = Color.Blue * 0.5f;
            _playerButtons[selectedC1].Hover(true);

            character1.CharacterData =
                ContentChest.CharacterData.First(c =>
                    c.Name.Equals(button.Character, StringComparison.OrdinalIgnoreCase));
            character1.Texture = ContentChest.Characters[button.Character];
        }

        public override void Update(float deltaTime)
        {
            pulseTimer += deltaTime * 3;
            audience.Update(deltaTime);
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.Draw(ContentChest.StageBackground, new Rectangle(0, 0, ScreenSettings.Width, ScreenSettings.Height), Color.White);
            spriteBatch.Draw(ContentChest.Stage,
                new Rectangle(0, ScreenSettings.Height - ContentChest.Stage.Height / 2, 1280,
                    ContentChest.Stage.Height), Color.White);
            spriteBatch.Draw(ContentChest.Shadow,
                new Rectangle(200, ScreenSettings.Height / 2 - 65, ContentChest.EUPodium.Width * 2,
                    ContentChest.EUPodium.Height * 2), Color.Black * 0.8f);
            spriteBatch.Draw(ContentChest.Shadow,
                new Rectangle(ScreenSettings.Width - 200 - ContentChest.UKPodium.Width * 2,
                    ScreenSettings.Height / 2 - 65, ContentChest.UKPodium.Width * 2, ContentChest.UKPodium.Height * 2),
                Color.Black * 0.8f);

            character1.Draw(spriteBatch, true);
            character2.Draw(spriteBatch, true);

            spriteBatch.Draw(ContentChest.EUPodium,
                new Rectangle(200, ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2,
                    ContentChest.EUPodium.Width * 2, ContentChest.EUPodium.Height * 2), Color.White);
            spriteBatch.Draw(ContentChest.UKPodium,
                new Rectangle(ScreenSettings.Width - 200 - ContentChest.UKPodium.Width * 2,
                    ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2, ContentChest.UKPodium.Width * 2,
                    ContentChest.UKPodium.Height * 2), Color.White);

            audience.Draw(spriteBatch);

            foreach (var b in _playerButtons)
                b.Draw(spriteBatch);

            var text = "CHOOSE YOUR CHARACTER";
            var size = ContentChest.TitleFont.MeasureString(text);
            spriteBatch.DrawString(ContentChest.TitleFont, text, new Vector2(ScreenSettings.Width / 2 - size.X / 2, 20),
                Color.White);

            DrawButtons(spriteBatch);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }

        private void DrawButtons(SpriteBatch spriteBatch)
        {
            var showOne = false;
            var b1 = ContentChest.GamepadButtons[ButtonType.A][GamePad.GetCapabilities(0).DisplayName];
            var b2 = ContentChest.GamepadButtons[ButtonType.A][GamePad.GetCapabilities(1).DisplayName];
            
            
            var b1Pos = new Rectangle(ScreenSettings.Width / 2 - b1.Width / 2 - b1.Width - 10, ScreenSettings.Height - b1.Height - 20, b1.Width, b1.Height);
            var b2Pos = new Rectangle(ScreenSettings.Width / 2 - b2.Width / 2 + b2.Width + 10, ScreenSettings.Height - b2.Height - 20, b2.Width, b2.Height);

            var text = "Select Character";

            if (c1LockedIn && c2LockedIn)
            {
                text = "Start Game";
                showOne = true;
            }


            if (GamePad.GetCapabilities(0).DisplayName == GamePad.GetCapabilities(1).DisplayName || showOne)
            {
                b1Pos = new Rectangle(ScreenSettings.Width / 2 - b1.Width / 2, ScreenSettings.Height - b1.Height - 20, b1.Width, b1.Height);
                showOne = true;
            }


            var textSize = ContentChest.MainFont.MeasureString(text);
            spriteBatch.DrawString(ContentChest.MainFont, text, new Vector2(ScreenSettings.Width / 2 - textSize.X / 2, b1Pos.Y - textSize.Y - 5), Color.White);

            if (Math.Abs(Math.Floor(pulseTimer) % 2) == 0)
            {
                if (showOne)
                {
                    b1Pos = new Rectangle(ScreenSettings.Width / 2 - ((b1.Width - 10) / 2), ScreenSettings.Height - (b1.Height) - 20, b1.Width - 10, b1.Height - 10);
                } else b1Pos = new Rectangle(ScreenSettings.Width / 2 - (b1.Width / 2) - b1.Width - 5, ScreenSettings.Height - (b1.Height) - 20, b1.Width - 10, b1.Height - 10);
                b2Pos = new Rectangle(ScreenSettings.Width / 2 - (b2.Width / 2) + b2.Width + 5, ScreenSettings.Height - (b2.Height) - 20, b2.Width - 10, b2.Height - 10);
            }

            if(!c1LockedIn || showOne)
                spriteBatch.Draw(b1, b1Pos, Color.White);

            if(!showOne && !c2LockedIn)
                spriteBatch.Draw(b2, b2Pos, Color.White);
        }

    }
}