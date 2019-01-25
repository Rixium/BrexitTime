using BrexitTime.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Screens
{
    public class Screen : IScreen
    {
        protected readonly InputManager InputManager;

        public Screen()
        {
            InputManager = new InputManager();
        }

        public ContentChest ContentChest { get; set; }

        public virtual void Initialise()
        {
        }

        public virtual void Update(float deltaTime)
        {
            InputManager.Update(deltaTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}