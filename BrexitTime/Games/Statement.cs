using System;
using System.Linq;
using BrexitTime.Enums;

namespace BrexitTime.Games
{
    public class Statement
    {
        public string Body { get; set; }
        public Answer[] Answers { get; set; }
        public ButtonType[] P1AnswerButtons { get; set; }
        public ButtonType[] P2AnswerButtons { get; set; }

        public void RandomiseButtons(Random rnd)
        {
            P1AnswerButtons = new[] {0, 1, 2, 3}.OrderBy(x => rnd.Next()).Select(m => (ButtonType) m).ToArray();
            P2AnswerButtons = new[] {3, 0, 2, 1}.OrderBy(x => rnd.Next()).Select(m => (ButtonType) m).ToArray();
        }

        public Answer GetAnswerFor(int player, ButtonType button)
        {
            for (var i = 0; i < P1AnswerButtons.Length; i++)
            {
                var b = P1AnswerButtons[i];
                var b2 = P2AnswerButtons[i];
                if (player == 0)
                {
                    if (b != button) continue;
                    if (Answers[i].Used) return null;
                    Answers[i].Used = true;
                    return Answers[i];
                }

                if (player == 1)
                {
                    if (b2 != button) continue;
                    if (Answers[i].Used) return null;
                    Answers[i].Used = true;
                    return Answers[i];
                }
            }

            return null;
        }

        public void Reset()
        {
            foreach (var answer in Answers)
                answer.Used = false;
        }
    }
}