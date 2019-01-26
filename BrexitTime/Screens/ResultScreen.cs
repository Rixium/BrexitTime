using BrexitTime.Constants;
using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Screens
{
    public class ResultScreen : Screen
    {
        private readonly Bias _result;

        public ResultScreen(Bias result)
        {
            _result = result;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            var text = _result == Bias.Leave ? "LEAVE" : "REMAIN";
            var textSize = ContentChest.TitleFont.MeasureString(text);

            spriteBatch.DrawString(ContentChest.TitleFont, text, new Vector2(ScreenSettings.Width / 2 - textSize.X / 2, ScreenSettings.Height / 2 - textSize.Y / 2), Color.Black);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}