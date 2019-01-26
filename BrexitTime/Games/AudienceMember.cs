using System;
using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Games
{
    public class AudienceMember
    {
        
        private readonly Texture2D _texture;
        private Vector2 _pos;
        private readonly Vector2 _startPosition;
        private readonly Color _color;
        private float wiggleTimer = 0;
        private Random random;
        public Bias Bias;
        private float _bias;

        public AudienceMember(Texture2D texture, Vector2 pos, Color c, Random r, Bias memberBias)
        {
            _texture = texture;
            _pos = pos;
            random = r;
            _color = c;
            _startPosition = _pos;
            Bias = memberBias;
            _bias = (float) memberBias;
        }

        public void Update(float deltaTime)
        {
            wiggleTimer -= deltaTime;

            if (wiggleTimer > 0) return;
            if (random.Next(0, 100) <= 5) return;

            var offset = random.NextDouble();
            if (offset > 0.5)
                offset = -1 + offset;
            
            _pos = new Vector2((float)(_pos.X + offset), (float)(_pos.Y + offset));

            _pos.X = MathHelper.Clamp(_pos.X, _startPosition.X - 5, _startPosition.X + 5);
            _pos.Y = MathHelper.Clamp(_pos.Y, _startPosition.Y - 5, _startPosition.Y + 5);

            wiggleTimer = random.Next(0, 1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _pos, _color);
        }
    }
}