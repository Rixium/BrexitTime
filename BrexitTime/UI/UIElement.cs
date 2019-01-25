using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.UI
{
    public abstract class UIElement
    {
        private readonly Alignment _alignment;

        protected UIElement(Vector2 position, int width, int height, Alignment alignment)
        {
            Position = position;
            Width = width;
            Height = height;
            _alignment = alignment;
        }

        public Vector2 Position { get; }
        public int Width { get; }
        public int Height { get; }

        public Rectangle Bounds => GetBounds();

        private Rectangle GetBounds()
        {
            switch (_alignment)
            {
                case Alignment.CENTER:
                    return new Rectangle((int) Position.X - Width / 2, (int) Position.Y - Height / 2, Width, Height);
                case Alignment.RIGHT:
                    return new Rectangle((int) Position.X - Width, (int) Position.Y, Width, Height);
                default:
                    return new Rectangle((int) Position.X, (int) Position.Y, Width, Height);
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}