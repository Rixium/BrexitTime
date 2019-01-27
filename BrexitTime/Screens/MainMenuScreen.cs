using System.Collections.Generic;
using BrexitTime.Constants;
using BrexitTime.Enums;
using BrexitTime.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrexitTime.Screens
{
    public class MainMenuScreen : Screen
    {
        private const int Padding = 10;
        private readonly string _creditsText = "A Game by Some Guys for the Global Game Jam 2019";

        private readonly List<UIElement> _uiElements;
        private Vector2 _creditsPosition;

        private int _hoverButton;
        private Logo _logo;

        public MainMenuScreen()
        {
            _uiElements = new List<UIElement>();
        }

        public override void Initialise()
        {
            base.Initialise();

            // Anything that requires use of the content chest should be in here.
            var position =
                new Vector2(ScreenSettings.ScreenCenter.X,
                    ContentChest.Logo.Height); // Button position center of screen.
            var startButton = new Button(ContentChest.ButtonBackground, ContentChest.ButtonBackground_Pressed,
                ContentChest.MainFont, "Start", position, 2,
                Alignment.CENTER);
            var howToPlayButton = new Button(ContentChest.ButtonBackground, ContentChest.ButtonBackground_Pressed,
                ContentChest.MainFont, "How To Play",
                new Vector2(position.X, startButton.Bottom.Y + Padding), 2,
                Alignment.CENTER);
            var creditsButton = new Button(ContentChest.ButtonBackground, ContentChest.ButtonBackground_Pressed,
                ContentChest.MainFont, "Credits",
                new Vector2(position.X, howToPlayButton.Bottom.Y + Padding), 2,
                Alignment.CENTER);
            var quitButton = new Button(ContentChest.ButtonBackground, ContentChest.ButtonBackground_Pressed,
                ContentChest.MainFont, "Exit",
                new Vector2(position.X, creditsButton.Bottom.Y + Padding), 2,
                Alignment.CENTER);

            _uiElements.Add(startButton);
            _uiElements.Add(howToPlayButton);
            _uiElements.Add(creditsButton);
            _uiElements.Add(quitButton);

            startButton.OnElementClick += AudioManager.OnButtonClick;
            startButton.OnElementClick += OnStartClicked;
            quitButton.OnElementClick += OnQuitClicked;
            quitButton.OnElementClick += AudioManager.OnButtonClick;

            howToPlayButton.OnElementClick += AudioManager.OnButtonClick;
            howToPlayButton.OnElementClick += OnHowToPlayClicked;
            creditsButton.OnElementClick += OnCreditsClick;
            creditsButton.OnElementClick += AudioManager.OnButtonClick;

            var pad1A = new InputCommand(0, Buttons.A);
            var pad1Down = new InputCommand(0, Buttons.LeftThumbstickDown);
            var pad1Up = new InputCommand(0, Buttons.LeftThumbstickUp);

            InputManager.RegisterGamePadButton(pad1A, ClickSelected);
            InputManager.RegisterGamePadButton(pad1Down, ChangeButton);
            InputManager.RegisterGamePadButton(pad1Up, ChangeButton);

            var _logoPosition = new Vector2(ScreenSettings.ScreenCenter.X - ContentChest.Logo.Width / 2, Padding);
            _logo = new Logo(ContentChest.Logo, _logoPosition);
            var textbounds = ContentChest.MainFont.MeasureString(_creditsText);
            _creditsPosition = new Vector2(ScreenSettings.Width / 2 - textbounds.X / 2,
                ScreenSettings.Height - textbounds.Y - Padding);
        }

        private void OnCreditsClick(UIElement obj)
        {
            ChangeScreen(new CreditsScreen());   
        }

        private void OnHowToPlayClicked(UIElement obj)
        {
            ChangeScreen(new HowToPlayScreen());
        }

        private void ChangeButton(InputCommand obj)
        {
            GamePad.SetVibration(0, 1, 1);
            GamePad.SetVibration(1, 1, 1);
            switch (obj.Button)
            {
                case Buttons.LeftThumbstickUp:
                    _hoverButton--;
                    break;
                case Buttons.LeftThumbstickDown:
                    _hoverButton++;
                    break;
            }

            _hoverButton = MathHelper.Clamp(_hoverButton, 0, _uiElements.Count - 1);
        }

        private void OnStartClicked(UIElement obj)
        {
            ChangeScreen(new CharacterSelectScreen());
        }

        private void ClickSelected(InputCommand obj)
        {
            GamePad.SetVibration(0, 1, 1);
            GamePad.SetVibration(1, 1, 1);
            _uiElements[_hoverButton].Click();
        }

        private void OnQuitClicked(UIElement obj)
        {
            Quit();
        }

        public override void Update(float deltaTime)
        {
            if (ScreenState == ScreenState.Active)
                for (var i = 0; i < _uiElements.Count; i++)
                    if (i == _hoverButton)
                        _uiElements[i].Hover(true);
                    else
                        _uiElements[i].Hover(false);


            foreach (var elem in _uiElements)
                elem.Update(deltaTime);

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