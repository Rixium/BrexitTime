using System;
using BrexitTime.Constants;
using BrexitTime.Enums;
using BrexitTime.Games;
using BrexitTime.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrexitTime.Screens
{
    public class GameScreen : Screen
    {
        private readonly Character _playerOne;
        private readonly Character _playerTwo;
        private readonly Audience _audience;
        private readonly Random _random;
        private bool debug;

        private Statement _activeStatement;
        private StatementManager _statementManager;

        public GameScreen(Character playerOne, Character playerTwo, Audience audience)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            _audience = audience;
            _random = new Random();
        }

        public override void Initialise()
        {
            InputManager.RegisterOnKeyPress(Keys.F1, ToggleDebug);


            InputManager.RegisterGamePadButton(new InputCommand(0, Buttons.A), ButtonDown);
            InputManager.RegisterGamePadButton(new InputCommand(0, Buttons.B), ButtonDown);
            InputManager.RegisterGamePadButton(new InputCommand(0, Buttons.X), ButtonDown);
            InputManager.RegisterGamePadButton(new InputCommand(0, Buttons.Y), ButtonDown);

            InputManager.RegisterGamePadButton(new InputCommand(1, Buttons.A), ButtonDown);
            InputManager.RegisterGamePadButton(new InputCommand(1, Buttons.B), ButtonDown);
            InputManager.RegisterGamePadButton(new InputCommand(1, Buttons.X), ButtonDown);
            InputManager.RegisterGamePadButton(new InputCommand(1, Buttons.Y), ButtonDown);

            _statementManager = new StatementManager(ContentChest);
            base.Initialise();
        }

        private void ButtonDown(InputCommand obj)
        {
            _statementManager.MakeSelection(obj.GamePadNumber, obj.Button);
        }
        

        private void ToggleDebug(Keys obj)
        {
            debug = !debug;
        }

        public override void Update(float deltaTime)
        {
            _audience.Update(deltaTime);

            if (ScreenState == ScreenState.Active)
            {
                _statementManager.Update(deltaTime);
            }

            base.Update(deltaTime);
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.Draw(ContentChest.StageBackground, new Rectangle(0, 0, ScreenSettings.Width, ScreenSettings.Height), Color.White);
            spriteBatch.Draw(ContentChest.Stage,
                new Rectangle(0, ScreenSettings.Height - ContentChest.Stage.Height / 2, 1280,
                    ContentChest.Stage.Height), Color.White);
            spriteBatch.Draw(ContentChest.Shadow,
                new Rectangle(100, ScreenSettings.Height / 2 - 65, ContentChest.EUPodium.Width * 2,
                    ContentChest.EUPodium.Height * 2), Color.Black * 0.8f);
            spriteBatch.Draw(ContentChest.Shadow,
                new Rectangle(ScreenSettings.Width - 100 - ContentChest.UKPodium.Width * 2,
                    ScreenSettings.Height / 2 - 65, ContentChest.UKPodium.Width * 2, ContentChest.UKPodium.Height * 2),
                Color.Black * 0.8f);

            _playerOne.Draw(spriteBatch);
            _playerTwo.Draw(spriteBatch);

            spriteBatch.Draw(ContentChest.EUPodium,
                new Rectangle(100, ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2,
                    ContentChest.EUPodium.Width * 2, ContentChest.EUPodium.Height * 2), Color.White);
            spriteBatch.Draw(ContentChest.UKPodium,
                new Rectangle(ScreenSettings.Width - 100 - ContentChest.UKPodium.Width * 2,
                    ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2, ContentChest.UKPodium.Width * 2,
                    ContentChest.UKPodium.Height * 2), Color.White);
            
            _audience.Draw(spriteBatch);

            if (debug)
            {
                spriteBatch.DrawString(ContentChest.MainFont, "AUDIENCE BIAS", new Vector2(10, 10), Color.White);
                spriteBatch.DrawString(ContentChest.MainFont, $"REMAIN: {_audience.Remainers}", new Vector2(10, 40),
                    Color.White);
                spriteBatch.DrawString(ContentChest.MainFont, $"LEAVE: {_audience.Brexiteers}", new Vector2(10, 70),
                    Color.White);
            }

            _statementManager.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}