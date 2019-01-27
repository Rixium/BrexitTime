using System;
using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Games
{
    public class AudienceMember
    {
        private readonly Color _color;
        private readonly Vector2 _startPosition;

        private readonly Texture2D _texture;
        private readonly Random random;
        private float _bias;
        private Vector2 _pos;
        public Bias Bias;
        public Action<Bias> OnBiasChanged;
        private float wiggleTimer;
        private bool render;

        public AudienceMember(Texture2D texture, Vector2 pos, Color c, Random r, Bias memberBias, bool b = true)
        {
            _texture = texture;
            _pos = pos;
            random = r;
            _color = c;
            _startPosition = _pos;
            Bias = memberBias;
            render = b;
            if (Bias == Bias.Remain)
            {
                _bias = (float) memberBias;
                _bias = (float) r.NextDouble() - 0.5f;
            }
            else
            {
                _bias = (float) r.NextDouble() + 0.5f;
            }

            _bias = MathHelper.Clamp(_bias, 0, 1);
        }

        public float RealBias => _bias;

        public void Update(float deltaTime)
        {
            wiggleTimer -= deltaTime;

            if (wiggleTimer > 0) return;
            if (random.Next(0, 100) <= 5) return;

            var offset = random.NextDouble();
            if (offset > 0.5)
                offset = -1 + offset;

            _pos = new Vector2((float) (_pos.X + offset), (float) (_pos.Y + offset));

            _pos.X = MathHelper.Clamp(_pos.X, _startPosition.X - 5, _startPosition.X + 5);
            _pos.Y = MathHelper.Clamp(_pos.Y, _startPosition.Y - 5, _startPosition.Y + 5);

            wiggleTimer = random.Next(0, 1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!render) return;
            spriteBatch.Draw(_texture, _pos, _color);
        }

        public void UpdateBias(float modifier)
        {
            _bias += modifier;

            _bias = MathHelper.Clamp(_bias, 0.3f, 0.7f);

            var newBias = (Bias) Math.Round(_bias);

            if (newBias == Bias) return;

            Bias = newBias;
            OnBiasChanged?.Invoke(Bias);
        }
    }
}