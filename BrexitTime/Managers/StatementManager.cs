using System;
using BrexitTime.Constants;
using BrexitTime.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Managers
{
    public class StatementManager
    {
        private Statement _activeStatement;
        private readonly ContentChest _contentChest;
        private readonly Random _random;
        private int currLength = 0;
        private float charTimer = 0;
        private float animateTime = 0.04f;

        private int _activeAnswers = 0;
        private float _activeAnswerTimer = 0;
        private float _activateAnswerMaxTime = 1.0f;

        public StatementManager(ContentChest contentChest)
        {
            _random = new Random();
            _contentChest = contentChest;
        }

        public void SetStatement(Statement statement)
        {
            _activeStatement = statement;
        }

        public Statement GetStatement()
        {
            return _activeStatement;
        }

        public void Update(float deltaTime)
        {
            if (GetStatement() == null)
                NewStatement();

            Animate(deltaTime);
        }

        private void Animate(float deltaTime)
        {
            if (_activeStatement == null) return;
            if (currLength >= _activeStatement.Body.Length)
            {
                AnimateAnswers(deltaTime);
                return;
            }

            charTimer += deltaTime;
            if (charTimer >= animateTime)
            {
                currLength++;
                charTimer = 0;
                _contentChest.Click.Play();
            }

            currLength = MathHelper.Clamp(currLength, 0, _activeStatement.Body.Length);
        }

        private void AnimateAnswers(float deltaTime)
        {
            if (_activeAnswers >= 4) return;

            _activeAnswerTimer += deltaTime;
            if (_activeAnswerTimer < _activateAnswerMaxTime) return;

            _activeAnswerTimer = 0;
            _contentChest.Answer.Play();
            _activeAnswers++;
        }

        private void NewStatement()
        {
            SetStatement(_contentChest.Statements[_random.Next(0, _contentChest.Statements.Count)]);
        }

        public string CurrentText => _activeStatement == null ? 
            "" : _activeStatement.Body.Substring(0, currLength);

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_activeStatement == null) return;

            var size = _contentChest.MainFont.MeasureString(CurrentText);
            spriteBatch.DrawString(_contentChest.MainFont, CurrentText, new Vector2(ScreenSettings.ScreenCenter.X - size.X / 2, 40), Color.White);

            for (var i = 0; i < _activeAnswers; i++)
            {
                var answer = _activeStatement.Answers[i];
                var answerSize = _contentChest.MainFont.MeasureString(answer.Text);
                spriteBatch.DrawString(_contentChest.MainFont, answer.Text,
                    new Vector2(ScreenSettings.ScreenCenter.X - answerSize.X / 2,
                        ScreenSettings.ScreenCenter.Y - (4 * (30 + answerSize.Y)) + i * (answerSize.Y + 30)), Color.White);
            }
        }
    }
}