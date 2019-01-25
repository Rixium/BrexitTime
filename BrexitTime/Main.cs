using BrexitTime.Constants;
using BrexitTime.Managers;
using BrexitTime.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrexitTime
{
    public class Main : Game
    {
        private readonly ContentChest _contentChest;
        public readonly GraphicsDeviceManager Graphics;
        private ScreenManager _screenManager;
        private SpriteBatch _spriteBatch;

        public Main()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            Content.RootDirectory = "Content";
            _contentChest =
                new ContentChest(
                    Content); // We load all our resources in the content chest, so we can easily access them.
        }

        public Vector2 MousePosition
        {
            get
            {
                var mouseState = Mouse.GetState();
                return new Vector2(mouseState.X, mouseState.Y);
            }
        }

        protected override void Initialize()
        {
            Window.Title = "Brexit Time - The Ultimate Brexit Showdown Simulator 2019";
            _screenManager = new ScreenManager(this, _contentChest); // Hold the state of the screens.
            ScreenSettings.Initialise(Graphics); // We can store some constants here that we can use throughout.
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _contentChest.Load(); // Load all the required resources here.            


            _screenManager.AddScreen(new SplashScreen());
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var deltaTime =
                gameTime.ElapsedGameTime.Milliseconds / 1000.0f; // Get milliseconds passed since last frame.
            _screenManager.Update(deltaTime); // Update all the active screens.
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _screenManager.Draw(_spriteBatch); // Draw the list of active screens.

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            _spriteBatch.Draw(_contentChest.Cursor, MousePosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}