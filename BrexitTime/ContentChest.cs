using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace BrexitTime
{
    public class ContentChest
    {
        private readonly ContentManager _contentManager;

        public ContentChest(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public Song MainSong { get; set; }
        public Texture2D ButtonBackground { get; set; }
        public Texture2D Cursor { get; set; }
        public Texture2D Pixel { get; set; }
        public SpriteFont MainFont { get; set; }
        public SpriteFont TitleFont { get; set; }
        public Texture2D Splash { get; set; }
        public Texture2D Logo { get; set; }
        public Texture2D GameBackground { get; set; }

        public Texture2D UKPodium { get; set; }
        public Texture2D EUPodium { get; set; }
        public Texture2D Shadow { get; set; }
        public Texture2D Stage { get; set; }

        public void Load()
        {
            // Load all assets in here.
            ButtonBackground = Load<Texture2D>("UI/button");
            Cursor = Load<Texture2D>("cursor");
            Splash = Load<Texture2D>("splash");
            Pixel = Load<Texture2D>("pixel");
            MainFont = Load<SpriteFont>("Fonts/MainFont");
            TitleFont = Load<SpriteFont>("Fonts/TitleFont");
            GameBackground = Load<Texture2D>("Background/background");
            MainSong = Load<Song>("music/mainsong");
            UKPodium = Load<Texture2D>("gameobjects/podium_uk");
            EUPodium = Load<Texture2D>("gameobjects/podium_eu");
            Logo = Load<Texture2D>("logo");
            Shadow = Load<Texture2D>("gameobjects/shadow");
            Stage = Load<Texture2D>("Background/stage");
        }

        // Quick function to make loading a little more straight forward.
        private T Load<T>(string path)
        {
            return _contentManager.Load<T>(path);
        }
    }
}