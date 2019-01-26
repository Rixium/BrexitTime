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
        private bool pressed;
        private float pressedTimer;

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
            if (!pressed) return;

            pressedTimer -= deltaTime;

            if (pressedTimer <= 0)
                pressed = false;
        }

        public override void Click()
        {
            OnElementClick?.Invoke(this);
            pressedTimer = 0.1f;
            pressed = true;
        }

        public override void Release()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (pressed)
            {
                spriteBatch.Draw(_pressedTexture, Bounds, Color.White);

                spriteBatch.DrawString(_font, _text, new Vector2(TextPosition.X, TextPosition.Y + 5), Color.White);
            }
            else
            {
                spriteBatch.Draw(_texture, Bounds, Color.White);

                spriteBatch.DrawString(_font, _text, TextPosition, Color.White);
            }
            
        }
    }
}