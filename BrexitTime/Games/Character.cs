using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Games
{
    public class Character
    {
        private readonly SpriteFont _font;
        public CharacterData CharacterData;
        public bool FacingLeft;

        public Character(Texture2D texture, SpriteFont font, Rectangle position, CharacterData characterData)
        {
            Texture = texture;
            Position = position;
            CharacterData = characterData;
            _font = font;
        }

        public Texture2D Texture { get; set; }
        public Rectangle Position { get; set; }

        public Vector2 Center => new Vector2(Texture.Width / 2, 0);

        public void Update(float deltaTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var fontSize = _font.MeasureString(CharacterData.Name);

            if (FacingLeft)
            {
                spriteBatch.Draw(Texture, Position, null, Color.White, 0, Center, SpriteEffects.FlipHorizontally, 0);
                spriteBatch.DrawString(_font, CharacterData.Name,
                    new Vector2(Position.X - fontSize.X / 2, Position.Y - 5 - fontSize.Y), Color.White);
                return;
            }

            spriteBatch.Draw(Texture, Position, Color.White);
            spriteBatch.DrawString(_font, CharacterData.Name,
                new Vector2(Position.X + Texture.Width / 2 - fontSize.X / 2, Position.Y - 5 - fontSize.Y), Color.White);
        }
    }
}