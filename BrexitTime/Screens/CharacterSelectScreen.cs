using BrexitTime.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Screens
{
    public class CharacterSelectScreen : Screen
    {
        public override void Initialise()
        {
            base.Initialise();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            
            spriteBatch.Draw(ContentChest.Stage, new Rectangle(0, ScreenSettings.Height - (ContentChest.Stage.Height * 6), 1280, ContentChest.Stage.Height * 10), Color.White);
            spriteBatch.Draw(ContentChest.Shadow, new Rectangle(100, ScreenSettings.Height / 2 - 65, ContentChest.EUPodium.Width * 2, ContentChest.EUPodium.Height * 2), Color.Black * 0.8f);
            spriteBatch.Draw(ContentChest.Shadow, new Rectangle(ScreenSettings.Width - 100 - ContentChest.UKPodium.Width * 2, ScreenSettings.Height / 2 - 65, ContentChest.UKPodium.Width * 2, ContentChest.UKPodium.Height * 2), Color.Black * 0.8f);
            spriteBatch.Draw(ContentChest.EUPodium, new Rectangle(100, ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2, ContentChest.EUPodium.Width * 2, ContentChest.EUPodium.Height * 2), Color.White);
            spriteBatch.Draw(ContentChest.UKPodium, new Rectangle(ScreenSettings.Width - 100 - ContentChest.UKPodium.Width * 2, ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2, ContentChest.UKPodium.Width * 2, ContentChest.UKPodium.Height * 2), Color.White);

            var text = "CHOOSE YOUR CHARACTER";
            var size = ContentChest.TitleFont.MeasureString(text);
            spriteBatch.DrawString(ContentChest.TitleFont, text, new Vector2(ScreenSettings.Width / 2 - size.X / 2, 20), Color.Black);

            var curr = 1;
            foreach (var c in ContentChest.Characters)
            {
                var cName = c.Name;
                var s = ContentChest.MainFont.MeasureString(cName);
                spriteBatch.DrawString(ContentChest.MainFont, cName, new Vector2(ScreenSettings.Width / 2 - s.X / 2, ScreenSettings.Height / 2 + s.Y * curr + 10 * curr), Color.Black);
                curr++;
            }

            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}