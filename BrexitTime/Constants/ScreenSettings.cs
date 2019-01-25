using Microsoft.Xna.Framework;

namespace BrexitTime.Constants
{
    public static class ScreenSettings
    {
        private static GraphicsDeviceManager _graphics;

        public static Vector2 ScreenCenter =>
            new Vector2(_graphics.PreferredBackBufferWidth / 2.0f, _graphics.PreferredBackBufferHeight / 2.0f);

        public static int Width => _graphics.PreferredBackBufferWidth;
        public static int Height => _graphics.PreferredBackBufferHeight;

        public static void Initialise(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
        }
    }
}