using System.Collections.Generic;
using BrexitTime.Enums;
using BrexitTime.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Managers
{
    public class ScreenManager
    {
        private readonly ContentChest _contentChest;
        private readonly Game _game;

        private readonly Queue<IScreen> _screens;

        public ScreenManager(Game game, ContentChest contentChest)
        {
            _game = game;
            _contentChest = contentChest;
            _screens = new Queue<IScreen>();
        }

        public void AddScreen(IScreen screen)
        {
            // Set the screen reference of the content chest.
            screen.ContentChest = _contentChest;
            screen.Initialise();
            screen.SetState(ScreenState.TransitionOn);
            screen.AddOnQuitListener(OnQuit);
            screen.AddOnScreenChangeListener(AddScreen);
            _screens.Enqueue(screen);
        }

        public void RemoveScreen()
        {
            if (_screens.Count == 0) return;
            _screens.Dequeue();
        }

        public void Update(float deltaTime)
        {
            if (_screens.Count == 0) return;
            GetScreen()?.Update(deltaTime);
        }

        private IScreen GetScreen()
        {
            if (_screens.Count == 0) return null;

            var screen = _screens.Peek();
            if (screen.GetState() == ScreenState.InActive)
                RemoveScreen();

            return _screens.Count == 0 ? null : _screens.Peek();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_screens.Count == 0) return;
            GetScreen()?.Draw(spriteBatch);
        }

        public void OnQuit()
        {
            _game.Exit();
        }
    }
}