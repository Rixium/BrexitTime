using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.UI
{
    public class FloatyText
    {

        public string Text;
        public Vector2 Position;
        private readonly Texture2D _pixel;
        public float fadeTimer = 2.0f;
        public float alpha = 1;
        
        public bool Finished => alpha <= 0;

        public FloatyText(string text, Vector2 position, Texture2D pixel, SpriteFont font)
        {
            Text = text;
            Font= font;
            Position = position;
            _pixel = pixel;
        }

        public SpriteFont Font { get; set; }

        public void Update(float deltaTime)
        {
            if (fadeTimer <= 0)
            {
                alpha -= deltaTime;
            }
            else fadeTimer -= deltaTime;

            Position.Y -= deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var textSize = Font.MeasureString(Text);
            spriteBatch.Draw(_pixel, new Rectangle((int)Position.X - 10, (int)Position.Y - 5, (int)textSize.X + 20, (int)textSize.Y + 10), Color.Black * 0.5f * alpha);
            spriteBatch.DrawString(Font, Text, Position, Color.White * alpha);
            
        }
    }
}