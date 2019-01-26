using BrexitTime.Constants;
using BrexitTime.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Screens
{
    public class GameScreen : Screen
    {
        private readonly Character _playerOne;
        private readonly Character _playerTwo;
        private readonly Audience _audience;

        public GameScreen(Character playerOne, Character playerTwo, Audience audience)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            _audience = audience;
        }

        public override void Initialise()
        {
            base.Initialise();
        }

        public override void Update(float deltaTime)
        {
            _audience.Update(deltaTime);
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
                new Rectangle(100, ScreenSettings.Height / 2 - 65, ContentChest.EUPodium.Width * 2,
                    ContentChest.EUPodium.Height * 2), Color.Black * 0.8f);
            spriteBatch.Draw(ContentChest.Shadow,
                new Rectangle(ScreenSettings.Width - 100 - ContentChest.UKPodium.Width * 2,
                    ScreenSettings.Height / 2 - 65, ContentChest.UKPodium.Width * 2, ContentChest.UKPodium.Height * 2),
                Color.Black * 0.8f);

            _playerOne.Draw(spriteBatch);
            _playerTwo.Draw(spriteBatch);

            spriteBatch.Draw(ContentChest.EUPodium,
                new Rectangle(100, ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2,
                    ContentChest.EUPodium.Width * 2, ContentChest.EUPodium.Height * 2), Color.White);
            spriteBatch.Draw(ContentChest.UKPodium,
                new Rectangle(ScreenSettings.Width - 100 - ContentChest.UKPodium.Width * 2,
                    ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2, ContentChest.UKPodium.Width * 2,
                    ContentChest.UKPodium.Height * 2), Color.White);
            
            _audience.Draw(spriteBatch);

            spriteBatch.DrawString(ContentChest.MainFont, "AUDIENCE BIAS", new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(ContentChest.MainFont, $"REMAIN: {_audience.Remainers}", new Vector2(10, 40), Color.White);
            spriteBatch.DrawString(ContentChest.MainFont, $"LEAVE: {_audience.Brexiteers}", new Vector2(10, 70), Color.White);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}