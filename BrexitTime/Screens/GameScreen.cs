using System;
using System.Collections.Generic;
using System.Globalization;
using BrexitTime.Constants;
using BrexitTime.Enums;
using BrexitTime.Games;
using BrexitTime.Managers;
using BrexitTime.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Random = System.Random;

namespace BrexitTime.Screens
{
    public class GameScreen : Screen
    {
        private readonly Audience _audience;
        private readonly Character _playerOne;
        private readonly Character _playerTwo;
        private readonly Random _random;

        private Statement _activeStatement;
        private BrexitBar _brexitBar;
        private bool _ended;
        private StatementManager _statementManager;
        private bool debug;

        private readonly List<FloatyText> FloatyText = new List<FloatyText>();
        private float gameTime;

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

            _statementManager = new StatementManager(ContentChest, _playerOne, _playerTwo);
            _statementManager.OnStatementEnded += OnStatementEnded;
            _statementManager.OnStatementSelect += OnStatementSelect;

            _brexitBar = new BrexitBar(ContentChest.BarBackground, ContentChest.LeaveBar, ContentChest.RemainBar);

            _brexitBar.SetBrexit(_audience.Brexiteers);
            _brexitBar.SetRemain(_audience.Remainers);

            _audience.OnDecision += OnDecision;

            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(ContentChest.MainSong);
            gameTime = (float) ContentChest.MainSong.Duration.TotalSeconds;
            base.Initialise();
        }

        private void OnSongEnd()
        {
            _ended = true;
            OnDecision(_audience.GetBias());
        }

        private void OnStatementSelect(Character c, Answer ans)
        {
            var textSize = ContentChest.MainFont.MeasureString(ans.Text);
            var position = c.RawTextPosition;
            position.X -= textSize.X / 2;
            position.Y -= 50;
            FloatyText.Add(new FloatyText(ans.Text, position, ContentChest.Pixel, ContentChest.MainFont));
        }

        private void OnDecision(Bias obj)
        {
            ChangeScreen(new ResultScreen(obj));
        }

        private void OnStatementEnded(Answer p1Answer, Answer p2Answer)
        {
            if (p1Answer != null)
                _audience.Distribute(CalculateBias(_playerOne, p1Answer.BiasModifier));
            if (p2Answer != null)
                _audience.Distribute(CalculateBias(_playerTwo, p2Answer.BiasModifier));
        }

        private float CalculateBias(Character p, float bias)
        {
            if (bias < 0 && p == _playerOne)
                return bias / 2.0f;
            if (bias > 0 && p == _playerTwo)
                return bias / 2.0f;

            return (float)(bias * Constants.Random.Rand.NextDouble() + 0.2f);
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
            gameTime -= deltaTime;
            gameTime = MathHelper.Clamp(gameTime, 0, (float) ContentChest.MainSong.Duration.TotalSeconds);

            _brexitBar.SetBrexit(_audience.Brexiteers);
            _brexitBar.SetRemain(_audience.Remainers);
            _brexitBar.Update(deltaTime);

            foreach (var t in new List<FloatyText>(FloatyText))
            {
                t.Update(deltaTime);
                if (t.Finished)
                    FloatyText.Remove(t);
            }

            if (ScreenState == ScreenState.Active && !_ended)
            {
                _statementManager.Update(deltaTime);
                if (gameTime <= 8.5) OnSongEnd();
            }

            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.Draw(ContentChest.StageBackground,
                new Rectangle(0, 0, ScreenSettings.Width, ScreenSettings.Height), Color.White);
            spriteBatch.Draw(ContentChest.Stage,
                new Rectangle(0, ScreenSettings.Height - ContentChest.Stage.Height / 2, 1280,
                    ContentChest.Stage.Height), Color.White);
            spriteBatch.Draw(ContentChest.Shadow,
                new Rectangle(200, ScreenSettings.Height / 2 - 65, ContentChest.EUPodium.Width * 2,
                    ContentChest.EUPodium.Height * 2), Color.Black * 0.8f);
            spriteBatch.Draw(ContentChest.Shadow,
                new Rectangle(ScreenSettings.Width - 200 - ContentChest.UKPodium.Width * 2,
                    ScreenSettings.Height / 2 - 65, ContentChest.UKPodium.Width * 2, ContentChest.UKPodium.Height * 2),
                Color.Black * 0.8f);

            _playerOne.Draw(spriteBatch);
            _playerTwo.Draw(spriteBatch);

            spriteBatch.Draw(ContentChest.EUPodium,
                new Rectangle(200, ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2,
                    ContentChest.EUPodium.Width * 2, ContentChest.EUPodium.Height * 2), Color.White);
            spriteBatch.Draw(ContentChest.UKPodium,
                new Rectangle(ScreenSettings.Width - 200 - ContentChest.UKPodium.Width * 2,
                    ScreenSettings.Height / 2 - ContentChest.EUPodium.Height / 2, ContentChest.UKPodium.Width * 2,
                    ContentChest.UKPodium.Height * 2), Color.White);

            _audience.Draw(spriteBatch);

            var text =
                $"Time Until Vote: {MathHelper.Clamp((float) Math.Floor(gameTime - 7), 0, gameTime + 1).ToString(CultureInfo.InvariantCulture)}";
            var textSize = ContentChest.MainFont.MeasureString(text);
            var timeToVotePos = new Vector2(ScreenSettings.Width / 2 - textSize.X / 2,
                ScreenSettings.Height - _brexitBar.Border.Height - 10 - textSize.Y);
            spriteBatch.DrawString(ContentChest.MainFont, text, timeToVotePos, Color.White);

            foreach (var t in new List<FloatyText>(FloatyText))
                t.Draw(spriteBatch);

            _statementManager.Draw(spriteBatch);
            _brexitBar.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}