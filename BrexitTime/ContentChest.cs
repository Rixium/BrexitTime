﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime
{
    public class ContentChest
    {
        private readonly ContentManager _contentManager;

        public ContentChest(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public Texture2D ButtonBackground { get; set; }
        public Texture2D Cursor { get; set; }
        public Texture2D Pixel { get; set; }
        public SpriteFont MainFont { get; set; }

        public void Load()
        {
            // Load all assets in here.
            ButtonBackground = Load<Texture2D>("UI/button");
            Cursor = Load<Texture2D>("cursor");
            Pixel = Load<Texture2D>("pixel");
            MainFont = Load<SpriteFont>("Fonts/MainFont");
        }

        // Quick function to make loading a little more straight forward.
        private T Load<T>(string path)
        {
            return _contentManager.Load<T>(path);
        }
    }
}