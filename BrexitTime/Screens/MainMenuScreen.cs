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
        private readonly string _creditsText = "A Game by Some Guys for the Global Game Jam 2019";

        private readonly List<UIElement> _uiElements;
        private Vector2 _creditsPosition;
        private Logo _logo;

        public MainMenuScreen()
        {
            _uiElements = new List<UIElement>();
        }

        public override void Initialise()
        {
            // Anything that requires use of the content chest should be in here.
            var position =
                new Vector2(ScreenSettings.ScreenCenter.X,
                    ContentChest.Logo.Height + Padding * 2); // Button position center of screen.
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

            var _logoPosition = new Vector2(ScreenSettings.ScreenCenter.X - ContentChest.Logo.Width / 2, Padding);
            _logo = new Logo(ContentChest.Logo, _logoPosition);
            var textbounds = ContentChest.MainFont.MeasureString(_creditsText);
            _creditsPosition = new Vector2(ScreenSettings.Width / 2 - textbounds.X / 2,
                ScreenSettings.Height - textbounds.Y - Padding);
        }

        private void OnQuitClicked()
        {
            Quit();
        }

        private void OnStartClicked()
        {
            ChangeScreen(new CharacterSelectScreen());
        }

        public override void Update(float deltaTime)
        {
            if (ScreenState == ScreenState.Active)
            {
            }


            _logo.Update(deltaTime);
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            _logo.Draw(spriteBatch);
            foreach (var elem in _uiElements)
                elem.Draw(spriteBatch);

            spriteBatch.DrawString(ContentChest.MainFont, _creditsText, _creditsPosition, Color.Black);
            spriteBatch.End();

            base.Draw(spriteBatch); // Required for transitions.
        }
    }
}