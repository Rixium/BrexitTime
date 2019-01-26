﻿using BrexitTime.Enums;

namespace BrexitTime.Games
{
    public class Answer
    {
        public string Text { get; set; }
        public bool Used = false;
        public Bias BiasModifier { get; set; }
    }
}