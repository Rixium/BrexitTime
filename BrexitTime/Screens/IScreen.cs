using System;
using BrexitTime.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Screens
{
    public interface IScreen
    {
        ContentChest ContentChest { get; set; }

        void Initialise();
        void Update(float deltaTime);
        void Draw(SpriteBatch spriteBatch);
        void SetState(ScreenState transitionOn);
        void AddOnQuitListener(Action onQuit);
    }
}