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
        private readonly Texture2D _pressedTexture;
        private bool _pressed;
        private bool _hovering;
        private float _pressedTimer;

        public Button(Texture2D texture, Texture2D pressedTexture, SpriteFont font, string text,
            Vector2 position, int scale,
            Alignment alignment) : base(position,
            texture.Width * scale,
            texture.Height * scale, alignment)
        {
            _texture = texture;
            _pressedTexture = pressedTexture;
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

        public override void Update(float deltaTime)
        {
            if (!_pressed) return;

            _pressedTimer -= deltaTime;

            if (_pressedTimer <= 0)
                _pressed = false;
        }

        public override void Hover(bool hovering)
        {
            _hovering = hovering;
        }

        public override void Click()
        {
            OnElementClick?.Invoke(this);
            _pressedTimer = 0.1f;
            _pressed = true;
        }

        public override void Release()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var color = Color.White * 0.5f;
            if (_hovering) color = Color.White;

            if (_pressed)
            {
                spriteBatch.Draw(_pressedTexture, Bounds, color);

                spriteBatch.DrawString(_font, _text, new Vector2(TextPosition.X, TextPosition.Y + 5), color);
            }
            else
            {
                spriteBatch.Draw(_texture, Bounds, color);

                spriteBatch.DrawString(_font, _text, TextPosition, color);
            }
            
        }
    }
}