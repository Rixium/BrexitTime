using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.UI
{
    public class Button : UIElement
    {
        private readonly Texture2D _texture;

        public Button(Texture2D texture, Vector2 position, int scale, Alignment alignment) : base(position,
            texture.Width * scale,
            texture.Height * scale, alignment)
        {
            _texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Bounds, Color.White);
        }
    }
}