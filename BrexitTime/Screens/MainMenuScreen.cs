using System.Collections.Generic;
using BrexitTime.Constants;
using BrexitTime.Enums;
using BrexitTime.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Screens
{
    public class MainMenuScreen : Screen
    {
        private const int Padding = 10;

        private readonly List<UIElement> _uiElements;

        public MainMenuScreen()
        {
            _uiElements = new List<UIElement>();
        }

        public override void Initialise()
        {
            // Anything that requires use of the content chest should be in here.
            var position =
                new Vector2(ScreenSettings.ScreenCenter.X,
                    ScreenSettings.ScreenCenter.Y); // Button position center of screen.
            var startButton = new Button(ContentChest.ButtonBackground, ContentChest.MainFont, "Start", position, 2,
                Alignment.CENTER);
            var quitButton = new Button(ContentChest.ButtonBackground, ContentChest.MainFont, "Exit",
                new Vector2(position.X, startButton.Bottom.Y + Padding), 2,
                Alignment.CENTER);

            _uiElements.Add(startButton);
            _uiElements.Add(quitButton);

            // Register all of the UI elements so that our input manager knows if they've been clicked.
            InputManager.RegisterUIElement(OnStartClicked, startButton);
            InputManager.RegisterUIElement(OnQuitClicked, quitButton);
        }

        private void OnQuitClicked()
        {
            Quit();
        }

        private void OnStartClicked()
        {
        }

        public override void Update(float deltaTime)
        {
            if (ScreenState == ScreenState.Active)
            {
            }

            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            foreach (var elem in _uiElements)
                elem.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(spriteBatch); // Required for transitions.
        }
    }
}