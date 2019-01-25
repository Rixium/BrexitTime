using BrexitTime.Constants;
using BrexitTime.Managers;
using BrexitTime.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _contentChest =
                new ContentChest(
                    Content); // We load all our resources in the content chest, so we can easily access them.
        }

        protected override void Initialize()
        {
            IsMouseVisible = true; // TODO Mouse sprite, get rid of default.

            _screenManager = new ScreenManager(_contentChest); // Hold the state of the screens.
            ScreenSettings.Initialise(Graphics); // We can store some constants here that we can use throughout.
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _contentChest.Load(); // Load all the required resources here.            


            _screenManager.AddScreen(new MainMenuScreen());
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
            GraphicsDevice.Clear(Color.Black);
            _screenManager.Draw(_spriteBatch); // Draw the list of active screens.
            base.Draw(gameTime);
        }
    }
}