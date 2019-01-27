using System;
using BrexitTime.Constants;
using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrexitTime.Screens
{
    public class ResultScreen : Screen
    {
        private readonly Bias _result;
        private float _pulseTimer;

        public ResultScreen(Bias result)
        {
            _result = result;
        }

        public override void Initialise()
        {
            InputManager.RegisterGamePadButton(new InputCommand(0, Buttons.A), OnMainMenuPress);
            base.Initialise();
        }

        private void OnMainMenuPress(InputCommand obj)
        {
            ChangeScreen(new SplashScreen());
        }

        public override void Update(float deltaTime)
        {
            _pulseTimer += deltaTime * 3;
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            var text = _result == Bias.Leave ? "LEAVE" : "REMAIN";
            var textSize = ContentChest.TitleFont.MeasureString(text);

            spriteBatch.DrawString(ContentChest.TitleFont, text,
                new Vector2(ScreenSettings.Width / 2 - textSize.X / 2, ScreenSettings.Height / 2 - textSize.Y / 2),
                Color.Black);

            DrawButtons(spriteBatch);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }


        private void DrawButtons(SpriteBatch spriteBatch)
        {
            var b1 = ContentChest.GamepadButtons[ButtonType.A].ContainsKey(GamePad.GetCapabilities(0).DisplayName) ?
                ContentChest.GamepadButtons[ButtonType.A][GamePad.GetCapabilities(0).DisplayName] :
                ContentChest.GamepadButtons[ButtonType.A]["XInput Controller"];
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