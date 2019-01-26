using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Games
{
    public class Character
    {
        public CharacterData CharacterData;

        public Character(Texture2D texture, Rectangle position, CharacterData characterData)
        {
            Texture = texture;
            Position = position;
            CharacterData = characterData;
        }

        public Texture2D Texture { get; set; }
        public Rectangle Position { get; set; }
        public bool FacingLeft;

        public void Update(float deltaTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(FacingLeft)
                spriteBatch.Draw(Texture, Position, null, Color.White, 0, Center, SpriteEffects.FlipHorizontally, 0);
            else spriteBatch.Draw(Texture, Position, Color.White);
        }

        public Vector2 Center => new Vector2(Texture.Width / 2, 0);
    }
}