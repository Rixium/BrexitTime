using System;
using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.UI
{
    public class Button : UIElement
    {
        private readonly SpriteFont _font;
        private readonly string _text;
        private readonly Vector2 _textSize;
        private readonly Texture2D _texture;

        public Button(Texture2D texture, SpriteFont font, string text, Vector2 position, int scale,
            Alignment alignment) : base(position,
            texture.Width * scale,
            texture.Height * scale, alignment)
        {
            _texture = texture;
            _font = font;
            _text = text;

            _textSize = _font.MeasureString(_text);
        }

        private Vector2 TextPosition
        {
            get
            {
                var x = Bounds.X + Width / 2 - _textSize.X / 2;
                var y = Bounds.Y + Height / 2 - _textSize.Y / 2;
                return new Vector2(x, y);
            }
        }

        public override void Click()
        {
            Console.WriteLine($"{_text} button clicked.");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Bounds, Color.White);
            spriteBatch.DrawString(_font, _text, TextPosition, Color.White);
        }
    }
}