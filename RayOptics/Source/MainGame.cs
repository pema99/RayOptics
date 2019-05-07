using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RayOptics
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private World world;

        public MainGame()
        {
            this.IsMouseVisible = true;

            graphics = new GraphicsDeviceManager(this);
        }

        protected override void LoadContent()
        {
            world = new World();

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            world.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
