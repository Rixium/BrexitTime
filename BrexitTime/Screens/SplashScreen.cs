using BrexitTime.Constants;
using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace BrexitTime.Screens
{
    public class SplashScreen : Screen
    {
        private const float ShowTime = 0.5f;

        private float _time;

        public override void Initialise()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.Play(ContentChest.MainSong);
            base.Initialise();
        }

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
            var splashPos = new Rectangle((int) ScreenSettings.ScreenCenter.X - ContentChest.Splash.Width / 4,
                (int) ScreenSettings.ScreenCenter.Y - ContentChest.Splash.Height / 4, ContentChest.Splash.Width / 2,
                ContentChest.Splash.Height / 2);
            spriteBatch.Draw(ContentChest.Splash, splashPos, Color.White);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}