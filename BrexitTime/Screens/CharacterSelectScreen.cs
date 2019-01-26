using System;
using System.Collections.Generic;
using System.Linq;
using BrexitTime.Constants;
using BrexitTime.Enums;
using BrexitTime.Games;
using BrexitTime.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrexitTime.Screens
{
    public class CharacterSelectScreen : Screen
    {

        public List<UIElement> _playerButtons;

        public int selectedC1 = 0;
        public int selectedC2 = 0;
        public int maxSelection;

        public Character character1;
        public Character character2;

        public override void Initialise()
        {
            var c1 = ContentChest.CharacterData[0];
            var c2 = ContentChest.CharacterData[2];

            var c1Position = new Rectangle(100, ScreenSettings.Height / 2 - ContentChest.Characters[c1.Name].Height / 2, ContentChest.Characters[c1.Name].Width, ContentChest.Characters[c1.Name].Height);
            var c2Position = new Rectangle(ScreenSettings.Width - 100 - ContentChest.Characters[c1.Name].Width / 2, c1Position.Y, ContentChest.Characters[c1.Name].Width, ContentChest.Characters[c1.Name].Height);

            character1 = new Character(ContentChest.Characters[c1.Name], c1Position, c1);
            character2 = new Character(ContentChest.Characters[c2.Name], c2Position, c2)
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
                if (ContentChest.Portraits.ContainsKey(c.Name) == false)
                {
                    continue;
                }

                var cPortrait = ContentChest.Portraits[c.Name];
                var cButton = new CharacterButton(c.Name, ContentChest.PortraitBackground, cPortrait, new Vector2(ScreenSettings.ScreenCenter.X, ScreenSettings.ScreenCenter.Y - requiredHeight / 2 + curr * (height + padding * 2)), Alignment.CENTER);
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
            base.Initialise();
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

            var button = ((CharacterButton)_playerButtons[selectedC2]);
            if (button.Hovering && button.HoverColor != Color.Red * 0.5f)
            {
                if (selectedC2 == 0 || selectedC2 == maxSelection)
                    selectedC2 = last;
                else
                {
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
            }

            selectedC2 = MathHelper.Clamp(selectedC2, 0, maxSelection);
            if (selectedC2 == last) return;
            SelectPlayerTwo();
            
            _playerButtons[last].Hover(false);
        }

        private void SelectPlayerTwo()
        {
            var button = ((CharacterButton) _playerButtons[selectedC2]);

            if (button.Hovering && button.HoverColor != Color.Red * 0.5f) return;

            button.HoverColor = Color.Red * 0.5f;
            _playerButtons[selectedC2].Hover(true);
            character2.CharacterData =
                ContentChest.CharacterData.First(c => c.Name.Equals(button.Character, StringComparison.OrdinalIgnoreCase));
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
            
            var button = ((CharacterButton)_playerButtons[selectedC1]);
            if (button.Hovering && button.HoverColor != Color.Blue * 0.5f)
            {
                if (selectedC1 == 0 || selectedC1 == maxSelection)
                    selectedC1 = last;
                else
                {
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
            }


            selectedC1 = MathHelper.Clamp(selectedC1, 0, maxSelection);

            if (selectedC1 == last) return;

            SelectPlayerOne();
            
            _playerButtons[last].Hover(false);
        }

        private void SelectPlayerOne()
        {
            var button = ((CharacterButton)_playerButtons[selectedC1]);
            if (button.Hovering && button.HoverColor != Color.Blue * 0.5f) return;

            button.HoverColor = Color.Blue * 0.5f;
            _playerButtons[selectedC1].Hover(true);

            character1.CharacterData =
                ContentChest.CharacterData.First(c => c.Name.Equals(button.Character, StringComparison.OrdinalIgnoreCase));
            character1.Texture = ContentChest.Characters[button.Character];
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.Draw(ContentChest.Stage,
                new Rectangle(0, ScreenSettings.Height - ContentChest.Stage.Height * 6, 1280,
                    ContentChest.Stage.Height * 10), Color.White);
            spriteBatch.Draw(ContentChest.Shadow,
                new Rectangle(100, ScreenSettings.Height / 2 - 65, ContentChest.EUPodium.Width * 2,
                    ContentChest.EUPodium.Height * 2), Color.Black * 0.8f);
            spriteBatch.Draw(ContentChest.Shadow,
                new Rectangle(ScreenSettings.Width - 100 - ContentChest.UKPodium.Width * 2,
                    ScreenSettings.Height / 2 - 65, ContentChest.UKPodium.Width * 2, ContentChest.UKPodium.Height * 2),
                Color.Black * 0.8f);

            character1.Draw(spriteBatch);
            character2.Draw(spriteBatch);

            spriteBatch.Draw(ContentChest.EUPodium,
                new Rectangle(100, ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2,
                    ContentChest.EUPodium.Width * 2, ContentChest.EUPodium.Height * 2), Color.White);
            spriteBatch.Draw(ContentChest.UKPodium,
                new Rectangle(ScreenSettings.Width - 100 - ContentChest.UKPodium.Width * 2,
                    ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2, ContentChest.UKPodium.Width * 2,
                    ContentChest.UKPodium.Height * 2), Color.White);

            foreach(var b in _playerButtons)
                b.Draw(spriteBatch);

            var text = "CHOOSE YOUR CHARACTER";
            var size = ContentChest.TitleFont.MeasureString(text);
            spriteBatch.DrawString(ContentChest.TitleFont, text, new Vector2(ScreenSettings.Width / 2 - size.X / 2, 20),
                Color.Black);

            var curr = 1;
            foreach (var c in ContentChest.CharacterData)
            {
                var cName = c.Name;
                var s = ContentChest.MainFont.MeasureString(cName);
                spriteBatch.DrawString(ContentChest.MainFont, cName,
                    new Vector2(ScreenSettings.Width / 2 - s.X / 2, ScreenSettings.Height / 2 + s.Y * curr + 10 * curr),
                    Color.Black);
                curr++;
            }

            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}