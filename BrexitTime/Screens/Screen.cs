using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Screens
{
    public class Screen : IScreen
    {

        public ContentChest ContentChest { get; set; }

        public virtual void Update(float deltaTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

    }
}