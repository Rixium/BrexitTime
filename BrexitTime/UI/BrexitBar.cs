using System;
using BrexitTime.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.UI
{
    public class BrexitBar
    {
        public Texture2D Border;
        private readonly Texture2D _leaveBar;
        private readonly Texture2D _remainBar;

        public float Brexiteers;
        public Texture2D Pixel;
        private float NewPercentage;
        private float Percentage;

        public Vector2 Position;
        public float Remainers;

        public BrexitBar(Texture2D border, Texture2D leaveBar, Texture2D remainBar)
        {
            Border = border;
            _leaveBar = leaveBar;
            _remainBar = remainBar;
            Position = new Vector2(ScreenSettings.Width / 2 - Border.Width / 2,
                ScreenSettings.Height - Border.Height - 10);
        }

        public void SetBrexit(float brexitBias)
        {
            Brexiteers = brexitBias;
            NewPercentage = Remainers / (Brexiteers + Remainers);
        }

        public void SetRemain(float remainBias)
        {
            Remainers = remainBias;
            NewPercentage = Remainers / (Brexiteers + Remainers);
        }

        public void Update(float deltaTime)
        {

            if (Math.Abs(NewPercentage - Percentage) < 0.01f)
            {
                Percentage = NewPercentage;
                return;
            }

            if (NewPercentage < Percentage)
                Percentage -= deltaTime * 0.4f;
            else Percentage += deltaTime * 0.4f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Border, Position, Color.White);
            var r = new Rectangle((int) Position.X + 3, (int) Position.Y + 2, Border.Width - 6,
                Border.Height - 4);
            spriteBatch.Draw(_leaveBar, r, Color.Red);

            
            spriteBatch.Draw(_remainBar, new Rectangle(r.X, r.Y, (int)(r.Width * Percentage), r.Height), Color.Blue);
        }
    }
}