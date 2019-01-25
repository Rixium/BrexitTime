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

        private readonly List<IScreen> _screens;

        public ScreenManager(Game game, ContentChest contentChest)
        {
            _game = game;
            _contentChest = contentChest;
            _screens = new List<IScreen>();
        }

        public void AddScreen(IScreen screen)
        {
            // Set the screen reference of the content chest.
            screen.ContentChest = _contentChest;
            screen.Initialise();
            screen.SetState(ScreenState.TransitionOn);
            screen.AddOnQuitListener(OnQuit);
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

        public void OnQuit()
        {
            _game.Exit();
        }

    }
}