using System;
using System.Collections.Generic;
using BrexitTime.Constants;
using BrexitTime.Enums;
using BrexitTime.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrexitTime.Screens
{
    public class CreditsScreen : Screen
    {
        public Vector2 _creditsPosition = ScreenSettings.ScreenCenter;
        public float _pulseTimer;
        public List<Credits> Credits = new List<Credits>();
        public float textY;

        public override void Initialise()
        {
            Credits.Add(new Credits("MUSIC", "Skullbeatz"));
            Credits.Add(new Credits("SOUND", "FreeSound.org"));

            textY = ContentChest.MainFont.MeasureString(Credits[0].creditText).Y +
                    ContentChest.TitleFont.MeasureString(Credits[0].titleText).Y + 10;
            _creditsPosition.Y -= textY * Credits.Count / 2;
            _creditsPosition.X = 20;

            InputManager.RegisterGamePadButton(new InputCommand(0, Buttons.B), (x) => ChangeScreen(new MainMenuScreen()));
            base.Initialise();
        }

        public override void Update(float deltaTime)
        {
            _pulseTimer += deltaTime * 3;
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            for (var i = 0; i < Credits.Count; i++)
            {
                var titlePos = _creditsPosition + new Vector2(0, textY * i);
                spriteBatch.DrawString(ContentChest.TitleFont, Credits[i].titleText,
                    titlePos, Color.Black);
                spriteBatch.DrawString(ContentChest.MainFont, Credits[i].creditText, new Vector2(titlePos.X, titlePos.Y + ContentChest.TitleFont.MeasureString(Credits[i].titleText).Y + 3), Color.Black);
            }

            DrawButtons(spriteBatch);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }

        
        private void DrawButtons(SpriteBatch spriteBatch)
        {
            var b1 = ContentChest.GamepadButtons[ButtonType.B].ContainsKey(GamePad.GetCapabilities(0).DisplayName) ? 
                ContentChest.GamepadButtons[ButtonType.B][GamePad.GetCapabilities(0).DisplayName] : 
                ContentChest.GamepadButtons[ButtonType.B]["XInput Controller"];

            var b1Pos = new Rectangle(ScreenSettings.Width - b1.Width - 20, ScreenSettings.Height - b1.Height - 20,
                b1.Width, b1.Height);


            var text = "Main Menu";

            var textSize = ContentChest.MainFont.MeasureString(text);
            spriteBatch.DrawString(ContentChest.MainFont, text,
                new Vector2(b1Pos.X - textSize.X - 20, b1Pos.Y + b1Pos.Height / 2 - textSize.Y / 2), Color.Black);

            if (Math.Abs(Math.Floor(_pulseTimer) % 2) == 0)
                b1Pos = new Rectangle(ScreenSettings.Width - b1.Width - 10, ScreenSettings.Height - b1.Height - 20,
                    b1.Width - 5, b1.Height - 5);

            spriteBatch.Draw(b1, b1Pos, Color.White);
        }
    }
}