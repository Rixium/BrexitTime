using System.Collections.Generic;
using BrexitTime.Screens;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Managers
{
    public class ScreenManager
    {
        private readonly ContentChest _contentChest;

        private readonly List<IScreen> _screens;

        public ScreenManager(ContentChest contentChest)
        {
            _contentChest = contentChest;
            _screens = new List<IScreen>();
        }

        public void AddScreen(IScreen screen)
        {
            // Set the screen reference of the content chest.
            screen.ContentChest = _contentChest;
            _screens.Add(screen);
        }

        public void RemoveScreen(IScreen screen)
        {
            _screens.Remove(screen);
        }

        public void Update(float deltaTime)
        {
            foreach (var screen in _screens)
                screen.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var screen in _screens)
                screen.Draw(spriteBatch);
        }
    }
}