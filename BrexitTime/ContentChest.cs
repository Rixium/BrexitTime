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
        public List<Statement> Statements { get; set; }
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
        public Texture2D AudiencePerson { get; set; }
        public Texture2D StageBackground { get; set; }
        public SoundEffect Click { get; set; }
        public SoundEffect Select { get; set; }
        public SoundEffect Start { get; set; }
        public SoundEffect Audience { get; set; }
        public SoundEffect Answer { get; set; }
        public SoundEffect AnswerSelect { get; set; }

        public List<SoundEffect> Remains { get; set; } = new List<SoundEffect>();
        public List<SoundEffect> Leaves { get; set; } = new List<SoundEffect>();

        public Dictionary<ButtonType, Dictionary<string, Texture2D>> GamepadButtons { get; set; } = new Dictionary<ButtonType, Dictionary<string, Texture2D>>();
        public Dictionary<string, Texture2D> Characters { get; set; } = new Dictionary<string, Texture2D>();
        public Dictionary<string, Texture2D> Portraits { get; set; } = new Dictionary<string, Texture2D>();
        public Dictionary<string, SoundEffect> SelectionClips { get; set; } = new Dictionary<string, SoundEffect>();
        public List<Texture2D> AudiencePeople = new List<Texture2D>();
        public Texture2D PortraitBackground { get; set; }
        public SpriteFont QuestionFont { get; set; }
        public Texture2D BarBackground { get; set; }

        public Texture2D LeaveBar { get; set; }
        public Texture2D RemainBar { get; set; }
        public Song MenuMusic { get; set; }

        public void Load()
        {
            // Load all assets in here.
            ButtonBackground = Load<Texture2D>("UI/button");
            BarBackground = Load<Texture2D>("UI/brexitBar");
            ButtonBackground_Pressed = Load<Texture2D>("UI/button_pressed");
            Cursor = Load<Texture2D>("cursor");
            Splash = Load<Texture2D>("splash");
            Pixel = Load<Texture2D>("pixel");
            MainFont = Load<SpriteFont>("Fonts/MainFont");
            TitleFont = Load<SpriteFont>("Fonts/TitleFont");
            QuestionFont = Load<SpriteFont>("Fonts/QuestionFont");
            GameBackground = Load<Texture2D>("Background/background");
            MainSong = Load<Song>("music/mainsong");
            MenuMusic = Load<Song>("music/MenuSong");
            UKPodium = Load<Texture2D>("gameobjects/podium_uk");
            EUPodium = Load<Texture2D>("gameobjects/podium_eu");
            Logo = Load<Texture2D>("logo");
            LeaveBar = Load<Texture2D>("UI/leaveBar");
            RemainBar = Load<Texture2D>("UI/remainBar");
            Shadow = Load<Texture2D>("gameobjects/shadow");
            Stage = Load<Texture2D>("Background/stage");
            StageBackground = Load<Texture2D>("Background/stage_background");
            Click = Load<SoundEffect>("SoundEffects/click");
            Select = Load<SoundEffect>("SoundEffects/select");
            Answer = Load<SoundEffect>("SoundEffects/answer");
            AnswerSelect = Load<SoundEffect>("SoundEffects/answerSelect");
            Start = Load<SoundEffect>("SoundEffects/start");
            Audience = Load<SoundEffect>("SoundEffects/audience");
            AudiencePeople.Add(Load<Texture2D>("Characters/audience/audience_1"));
            AudiencePeople.Add(Load<Texture2D>("Characters/audience/audience_2"));
            AudiencePeople.Add(Load<Texture2D>("Characters/audience/audience_3"));

            for (var i = 1; i <= 7; i++)
            {
                Remains.Add(Load<SoundEffect>("SoundEffects/Crowd/remain_" + i));
                Leaves.Add(Load<SoundEffect>("SoundEffects/Crowd/leave_" + i));
            }
            PortraitBackground = Load<Texture2D>("Portraits/Portrait_Background");

            var x = new Dictionary<string, Texture2D>
            {
                {
                    "XInput Controller", Load<Texture2D>("UI/Xbox_X")
                },
                {
                    "PS4 Controller", Load<Texture2D>("UI/PS4_Square")
                }
            };

            var a = new Dictionary<string, Texture2D>
            {
                {
                    "XInput Controller", Load<Texture2D>("UI/Xbox_A")
                },
                {
                    "PS4 Controller", Load<Texture2D>("UI/PS4_Cross")
                }
            };

            var b = new Dictionary<string, Texture2D>
            {
                {
                    "XInput Controller", Load<Texture2D>("UI/Xbox_B")
                },
                {
                    "PS4 Controller", Load<Texture2D>("UI/PS4_Circle")
                }
            };

            var y = new Dictionary<string, Texture2D>
            {
                {
                    "XInput Controller", Load<Texture2D>("UI/Xbox_Y")
                },
                {
                    "PS4 Controller", Load<Texture2D>("UI/PS4_Triangle")
                }
            };

            GamepadButtons.Add(ButtonType.X, x);
            GamepadButtons.Add(ButtonType.B, b);
            GamepadButtons.Add(ButtonType.Y, y);
            GamepadButtons.Add(ButtonType.A, a);

            Characters.Add("Jeremy", Load<Texture2D>("Characters/Jeremy"));
            Characters.Add("Boris", Load<Texture2D>("Characters/Boris"));
            Characters.Add("Theresa", Load<Texture2D>("Characters/Theresa"));
            Characters.Add("Diane", Load<Texture2D>("Characters/Diane"));

            SelectionClips.Add("Jeremy", Load<SoundEffect>("SoundEffects/Selections/Jeremy"));
            SelectionClips.Add("Boris", Load<SoundEffect>("SoundEffects/Selections/Boris"));
            SelectionClips.Add("Theresa", Load<SoundEffect>("SoundEffects/Selections/Theresa"));
            SelectionClips.Add("Diane", Load<SoundEffect>("SoundEffects/Selections/Theresa"));

            Portraits.Add("Jeremy", Load<Texture2D>("Portraits/Jeremy_Portrait"));
            Portraits.Add("Boris", Load<Texture2D>("Portraits/Boris_Portrait"));
            Portraits.Add("Theresa", Load<Texture2D>("Portraits/Theresa_Portrait"));
            Portraits.Add("Diane", Load<Texture2D>("Portraits/Diane_Portrait"));
            LoadCharacterData();
            LoadStatements();
        }

        private void LoadCharacterData()
        {
            var text = File.ReadAllText("Content/Data/Characters.json");
            CharacterData = JsonConvert.DeserializeObject<List<CharacterData>>(text);
        }

        private void LoadStatements()
        {
            var text = File.ReadAllText("Content/Data/Questions.json");
            Statements = JsonConvert.DeserializeObject<List<Statement>>(text);
        }

        // Quick function to make loading a little more straight forward.
        private T Load<T>(string path)
        {
            return _contentManager.Load<T>(path);
        }
    }
}