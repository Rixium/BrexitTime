using BrexitTime.Constants;
using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrexitTime.Screens
{
    public class InsertControllersScreen : Screen
    {
        private readonly string text = "Please Insert 2 Controllers to Play";
        private Vector2 textPos;

        public override void Initialise()
        {
            var textSize = ContentChest.MainFont.MeasureString(text);
            textPos = ScreenSettings.ScreenCenter;
            textPos.X -= textSize.X / 2;
            textPos.Y -= textSize.Y / 2;
            base.Initialise();
        }

        public override void Update(float deltaTime)
        {
            if (ScreenState == ScreenState.Active)
                if (GamePad.GetState(0).IsConnected && GamePad.GetState(1).IsConnected)
                    ChangeScreen(new MainMenuScreen());

            base.Update(deltaTime);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.DrawString(ContentChest.MainFont, text, textPos, Color.Black);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}