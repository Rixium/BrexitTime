using Microsoft.Xna.Framework.Graphics;

namespace BrexitTime.Screens
{
    public class CharacterSelectScreen : Screen
    {
        public override void Initialise()
        {
            base.Initialise();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}