using BrexitTime.Constants;
using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Screens
{
    public class SplashScreen : Screen
    {
        private const float ShowTime = 0.5f;

        private float _time;

        public override void Update(float deltaTime)
        {
            if (GetState() == ScreenState.Active)
            {
                _time += deltaTime;

                if (_time >= ShowTime)
                    ChangeScreen(new MainMenuScreen());
            }

            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            var splashPos = new Vector2(ScreenSettings.ScreenCenter.X - ContentChest.Splash.Width / 2,
                ScreenSettings.ScreenCenter.Y - ContentChest.Splash.Height / 2);
            spriteBatch.Draw(ContentChest.Splash, splashPos, Color.White);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}