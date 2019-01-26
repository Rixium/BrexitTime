using System;
using Microsoft.Xna.Framework.Input;

namespace BrexitTime
{
    public class InputCommand
    {
        public InputCommand(int player, Buttons button)
        {
            GamePadNumber = player;
            Button = button;
        }

        public int GamePadNumber { get; set; }
        public Buttons Button { get; set; }

    }
}