using System.Collections.Generic;
using System.IO;
using BrexitTime.Enums;
using BrexitTime.Games;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;

namespace BrexitTime
{
    public class ContentChest
    {
        private readonly ContentManager _contentManager;

        public ContentChest(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public List<CharacterData> CharacterData { get; set; }
        public Song MainSong { get; set; }
        public Texture2D ButtonBackground { get; set; }
        public Texture2D ButtonBackground_Pressed { get; set; }
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
        public SoundEffect Click { get; set; }

        public Dictionary<ButtonType, Texture2D> GamepadButtons { get; set; } = new Dictionary<ButtonType, Texture2D>();
        public Dictionary<string, Texture2D> Characters { get; set; } = new Dictionary<string, Texture2D>();
        public Dictionary<string, Texture2D> Portraits { get; set; } = new Dictionary<string, Texture2D>();
        public Texture2D PortraitBackground { get; set; }

        public void Load()
        {
            // Load all assets in here.
            ButtonBackground = Load<Texture2D>("UI/button");
            ButtonBackground_Pressed = Load<Texture2D>("UI/button_pressed");
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
            Click = Load<SoundEffect>("SoundEffects/click");

            PortraitBackground = Load<Texture2D>("Portraits/Portrait_Background");

            GamepadButtons.Add(ButtonType.X, Load<Texture2D>("UI/Xbox_X"));
            GamepadButtons.Add(ButtonType.B, Load<Texture2D>("UI/Xbox_B"));
            GamepadButtons.Add(ButtonType.Y, Load<Texture2D>("UI/Xbox_Y"));
            GamepadButtons.Add(ButtonType.A, Load<Texture2D>("UI/Xbox_A"));

            Characters.Add("Jeremy", Load<Texture2D>("Characters/Jeremy"));
            Characters.Add("Boris", Load<Texture2D>("Characters/Boris"));
            Characters.Add("Theresa", Load<Texture2D>("Characters/Theresa"));

            Portraits.Add("Jeremy", Load<Texture2D>("Portraits/Jeremy_Portrait"));
            Portraits.Add("Boris", Load<Texture2D>("Portraits/Boris_Portrait"));
            Portraits.Add("Theresa", Load<Texture2D>("Portraits/Theresa_Portrait"));
            LoadCharacterData();
        }

        private void LoadCharacterData()
        {
            var text = File.ReadAllText("Content/Data/Characters.json");
            CharacterData = JsonConvert.DeserializeObject<List<CharacterData>>(text);
        }

        // Quick function to make loading a little more straight forward.
        private T Load<T>(string path)
        {
            return _contentManager.Load<T>(path);
        }
    }
}