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
            _uiElements.Add(new Button(ContentChest.ButtonBackground, position, 2, Alignment.CENTER));
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearClamp);
            foreach (var elem in _uiElements)
                elem.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}