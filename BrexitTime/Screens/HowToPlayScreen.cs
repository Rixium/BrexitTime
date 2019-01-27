using System;
using BrexitTime.Constants;
using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrexitTime.Screens
{
    public class HowToPlayScreen : Screen
    {
        private float _pulseTimer;

        public override void Initialise()
        {
            InputManager.RegisterGamePadButton(new InputCommand(0, Buttons.B), (x) => ChangeScreen(new MainMenuScreen()) );
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
            var titleSize = ContentChest.TitleFont.MeasureString("Remainers");
            spriteBatch.DrawString(ContentChest.TitleFont, "Remainers",
                new Vector2(ScreenSettings.Width / 4 - titleSize.X / 2, 20), Color.Black);

            var text = "Select a buzzword that screams remain.";
            var textSize = ContentChest.MainFont.MeasureString(text);
            spriteBatch.DrawString(ContentChest.MainFont, text,
                new Vector2(ScreenSettings.Width / 4 - textSize.X / 2, 20 + titleSize.Y + 20), Color.Black);


            text = "The better the buzzword, the more impact!";
            textSize = ContentChest.MainFont.MeasureString(text);
            spriteBatch.DrawString(ContentChest.MainFont, text,
                new Vector2(ScreenSettings.Width / 2 - textSize.X / 2, 20 + titleSize.Y + 20 + 100), Color.Black);


            text = "Have a majority by the end of the game to win!";
            textSize = ContentChest.MainFont.MeasureString(text);
            spriteBatch.DrawString(ContentChest.MainFont, text,
                new Vector2(ScreenSettings.Width / 2 - textSize.X / 2, 20 + titleSize.Y + 20 + 150), Color.Black);



            titleSize = ContentChest.TitleFont.MeasureString("Brexiteers");
            spriteBatch.DrawString(ContentChest.TitleFont, "Brexiteers",
                new Vector2(ScreenSettings.Width / 2 + ScreenSettings.Width / 4 - titleSize.X / 2, 20), Color.Black);

            text = "Select a buzzword that screams leave.";
            textSize = ContentChest.MainFont.MeasureString(text);
            spriteBatch.DrawString(ContentChest.MainFont, text,
                new Vector2(ScreenSettings.Width / 2 + ScreenSettings.Width / 4 - textSize.X / 2, 20 + titleSize.Y + 20), Color.Black);

            DrawButtons(spriteBatch);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }



        private void DrawButtons(SpriteBatch spriteBatch)
        {
            var b1 = ContentChest.GamepadButtons[ButtonType.B][GamePad.GetCapabilities(0).DisplayName];
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