using System;
using BrexitTime.Constants;
using BrexitTime.Enums;
using BrexitTime.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Screens
{
    public class Screen : IScreen
    {
        protected readonly InputManager InputManager;
        private Action<IScreen> _changeScreen;
        private float _fadeAlpha = 1.0f;
        private Action _onQuit;

        public Screen()
        {
            InputManager = new InputManager();
        }

        protected ScreenState ScreenState { get; set; }

        public float FadeSpeed { get; set; } = 0.01f;

        public ContentChest ContentChest { get; set; }

        public virtual void Initialise()
        {
        }

        public virtual void Update(float deltaTime)
        {
            if (ScreenState == ScreenState.Active)
                InputManager.Update(deltaTime);

            switch (ScreenState)
            {
                case ScreenState.Active:
                    return;
                case ScreenState.TransitionOn:
                    FadeIn(deltaTime);
                    break;
                default:
                    FadeOut(deltaTime);
                    break;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_fadeAlpha <= 0) return;
            spriteBatch.Begin();
            spriteBatch.Draw(ContentChest.Pixel, new Rectangle(0, 0, ScreenSettings.Width, ScreenSettings.Height),
                Color.Black * _fadeAlpha);
            spriteBatch.End();
        }

        public void SetState(ScreenState state)
        {
            ScreenState = state;
        }

        public void AddOnQuitListener(Action onQuit)
        {
            _onQuit += onQuit;
        }

        public ScreenState GetState()
        {
            return ScreenState;
        }

        public void AddOnScreenChangeListener(Action<IScreen> action)
        {
            _changeScreen += action;
        }

        private void FadeOut(float deltaTime)
        {
            _fadeAlpha += FadeSpeed;
            if (_fadeAlpha >= 1) ScreenState = ScreenState.InActive;
        }

        private void FadeIn(float deltaTime)
        {
            _fadeAlpha -= FadeSpeed;
            if (_fadeAlpha <= 0) ScreenState = ScreenState.Active;
        }

        protected virtual void Quit()
        {
            _onQuit?.Invoke();
        }

        protected virtual void ChangeScreen(IScreen screen)
        {
            SetState(ScreenState.TransitionOff);
            _changeScreen?.Invoke(screen);
        }
    }
}