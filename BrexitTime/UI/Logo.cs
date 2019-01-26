using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.UI
{
    public class Logo
    {
        private Vector2 _currentPosition;
        private readonly Vector2 _startPosition;
        private readonly Texture2D _texture;
        private State CurrentState;
        private float _endPositionY;
        private float _velocity = 0;

        public Logo(Texture2D texture, Vector2 startPosition)
        {
            CurrentState = State.FloatUp;

            _texture = texture;
            _startPosition = startPosition;
            _currentPosition = _startPosition;
            _endPositionY = _startPosition.Y - 10.0f;
        }

        public void Update(float deltaTime)
        {
            _currentPosition.Y += _velocity;
            if (CurrentState == State.FloatUp)
                FloatUp(deltaTime);
            else FloatDown(deltaTime);
        }

        private void FloatDown(float deltaTime)
        {
            if (_currentPosition.Y > _startPosition.Y)
            {
                _velocity *= 0.7f;
                if (_velocity >= 0.005f)
                    CurrentState = State.FloatUp;
            }
            else _velocity += deltaTime;

        }

        private void FloatUp(float deltaTime)
        {
            if (_currentPosition.Y < _endPositionY)
            {
                _velocity *= 0.7f;
                if (_velocity <= 0.005f)
                    CurrentState = State.FloatDown;
            } 
            else _velocity -= deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _currentPosition, Color.White);
        }

        private enum State
        {
            FloatUp,
            FloatDown
        }
    }
}