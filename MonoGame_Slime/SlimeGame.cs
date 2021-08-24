using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Slime.GameCore;

namespace MonoGame_Slime
{
    public class SlimeGame : Game
    {

        // Singleton
        public static SlimeGame Instance { get; private set; }


        // Graphics
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        // Input
        private KeyboardState _keyboardState; // Global Key State

        public SlimeGame()
        {

            // Graphics
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;


            // Content 
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Recourses
            Arts.Load(Content);
            
            
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(Arts.Player, new Vector2(200, 200), Color.White);
            _spriteBatch.End();
            

            base.Draw(gameTime);
        }

    }
}
