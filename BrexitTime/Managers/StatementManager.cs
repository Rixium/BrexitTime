using System;
using System.Globalization;
using BrexitTime.Constants;
using BrexitTime.Enums;
using BrexitTime.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrexitTime.Managers
{
    public class StatementManager
    {
        private readonly float _activateAnswerMaxTime = 1.0f;
        private readonly Character _c1;
        private readonly Character _c2;
        private readonly ContentChest _contentChest;
        private readonly System.Random _random;
        private readonly float animateTime = 0.04f;

        private int _activeAnswers;
        private float _activeAnswerTimer;
        private Statement _activeStatement;

        private float _countDown = 4.0f;
        private float charTimer;
        private int currLength;
        private float lastClick;
        public Action<Answer, Answer> OnStatementEnded;
        public Action<Character, Answer> OnStatementSelect;

        public Answer SelectedP1;
        public Answer SelectedP2;

        public StatementManager(ContentChest contentChest, Character c1, Character c2)
        {
            _random = new System.Random();
            _contentChest = contentChest;
            _c1 = c1;
            _c2 = c2;
        }

        public string CurrentText => _activeStatement == null ? "" : _activeStatement.Body.Substring(0, currLength);

        public void SetStatement(Statement statement)
        {
            _activeStatement = statement;
            _activeStatement.Reset();
            _activeStatement.RandomiseButtons(_random);
            _activeAnswers = 0;
            _countDown = 4;
            lastClick = 4;
            SelectedP2 = null;
            SelectedP1 = null;
            currLength = 0;
        }

        public Statement GetStatement()
        {
            return _activeStatement;
        }

        public void Update(float deltaTime)
        {
            if (_activeAnswers >= 4) CountDown(deltaTime);

            if (GetStatement() == null)
                NewStatement();

            Animate(deltaTime);
        }

        private void CountDown(float deltaTime)
        {
            _countDown -= deltaTime;

            if (lastClick - _countDown >= 1)
            {
                _contentChest.Click.Play();
                lastClick = _countDown;
            }

            if (_countDown <= 0)
            {
                OnStatementEnded?.Invoke(SelectedP1, SelectedP2);
                _activeStatement = null;
            }
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

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_activeStatement == null) return;

            var size = _contentChest.QuestionFont.MeasureString(CurrentText);
            spriteBatch.DrawString(_contentChest.QuestionFont, CurrentText,
                new Vector2(ScreenSettings.ScreenCenter.X - size.X / 2, 40), Color.White);

            for (var i = 0; i < _activeAnswers; i++)
            {
                var answer = _activeStatement.Answers[i];
                var answerSize = _contentChest.MainFont.MeasureString(answer.Text);
                var b = _contentChest.GamepadButtons[_activeStatement.P1AnswerButtons[i]];
                var b2 = _contentChest.GamepadButtons[_activeStatement.P2AnswerButtons[i]];

                var textPos = new Vector2(ScreenSettings.ScreenCenter.X - answerSize.X / 2,
                    ScreenSettings.ScreenCenter.Y - 4 * (30 + answerSize.Y) + i * (answerSize.Y + 30));


                if (!answer.Used)
                {
                    if (SelectedP1 == null)
                        spriteBatch.Draw(b,
                            new Rectangle((int) (ScreenSettings.ScreenCenter.X - 200 - b.Width / 2), (int) textPos.Y,
                                32,
                                32),
                            Color.White);
                    if (SelectedP2 == null)
                        spriteBatch.Draw(b2,
                            new Rectangle((int) (ScreenSettings.ScreenCenter.X + 200), (int) textPos.Y, 32, 32),
                            Color.White);
                    spriteBatch.DrawString(_contentChest.MainFont, answer.Text, textPos, Color.White);
                }
                else
                {
                    if (answer.UsedBy == Bias.Remain)
                        spriteBatch.DrawString(_contentChest.MainFont, answer.Text, textPos, Color.Blue * 0.8f);
                    else
                        spriteBatch.DrawString(_contentChest.MainFont, answer.Text, textPos, Color.Red * 0.8f);
                }
            }

            if (_activeAnswers < 4) return;
            var text = Math.Ceiling(_countDown).ToString(CultureInfo.InvariantCulture);
            spriteBatch.DrawString(_contentChest.TitleFont, text,
                new Vector2(ScreenSettings.Width / 2 - _contentChest.TitleFont.MeasureString(text).X / 2, 100),
                Color.White);
        }

        public void MakeSelection(int player, Buttons b)
        {
            if (_activeAnswers >= 4)
            {
                if (player == 0)
                    PlayerOneSelect(b);
                else PlayerTwoSelect(b);
            }

            if (SelectedP1 != null && SelectedP2 != null)
            {
                _activeStatement = null;
                OnStatementEnded?.Invoke(SelectedP1, SelectedP2);
            }
        }

        private void PlayerTwoSelect(Buttons buttons)
        {
            if (SelectedP2 != null) return;
            var answer = _activeStatement.GetAnswerFor(1, ConvertButton(buttons));
            if (answer == null) return;

            SelectedP2 = answer;
            OnStatementSelect?.Invoke(_c2, SelectedP2);
            answer.UsedBy = Bias.Leave;
            _contentChest.AnswerSelect.Play();
        }

        private void PlayerOneSelect(Buttons buttons)
        {
            if (SelectedP1 != null) return;
            var answer = _activeStatement.GetAnswerFor(0, ConvertButton(buttons));
            if (answer == null) return;

            answer.UsedBy = Bias.Remain;
            _contentChest.AnswerSelect.Play();
            SelectedP1 = answer;
            OnStatementSelect?.Invoke(_c1, SelectedP1);
        }

        private ButtonType ConvertButton(Buttons buttons)
        {
            if (buttons == Buttons.A)
                return ButtonType.A;
            if (buttons == Buttons.B)
                return ButtonType.B;
            if (buttons == Buttons.X)
                return ButtonType.X;
            if (buttons == Buttons.Y)
                return ButtonType.Y;

            return ButtonType.A;
        }
    }
}