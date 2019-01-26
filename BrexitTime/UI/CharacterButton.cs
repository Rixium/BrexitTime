using System;
using BrexitTime.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.UI
{
    internal class CharacterButton : UIElement
    {
        public string Character { get; set; }
        public Color HoverColor { get; set; }
        public bool Hovering => _hovering;

        public Texture2D Background;
        public Texture2D Foreground;
        private bool _hovering;

        public CharacterButton(string character, Texture2D background, Texture2D foreground, Vector2 position,
            Alignment alignment) : base(position, background.Width,
            background.Height, alignment)
        {
            Background = background;
            Foreground = foreground;
            Character = character;
        }

        public override void Click()
        {
            base.Click();
        }

        public override void Hover(bool hovering)
        {
            _hovering = hovering;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var portraitPosition = new Vector2(Bounds.X + Bounds.Width / 2 - Foreground.Width / 2,
                Bounds.Y + Bounds.Height / 2 - Foreground.Height / 2);

            if (_hovering)
            {
                spriteBatch.Draw(Background, Bounds, HoverColor);
                
                spriteBatch.Draw(Foreground, portraitPosition, Color.White);
                return;
            }

            spriteBatch.Draw(Background, Bounds, Color.White * 0.5f);
            spriteBatch.Draw(Foreground, portraitPosition, Color.White * 0.5f);

        }

        public override void Update(float deltaTime)
        {
        }
    }
}