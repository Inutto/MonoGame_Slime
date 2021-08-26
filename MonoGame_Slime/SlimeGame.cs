using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Slime.GameCore;
using MonoGame.Extended.Collisions;
using MonoGame.Extended;


namespace MonoGame_Slime
{
    public class SlimeGame : Game
    {

        // Singleton
        public static SlimeGame Instance { get; private set; }


        // Graphics
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static int screenWidth = 1920;
        public static int screenHeight = 1080;
    

        // Input
        private KeyboardState _keyboardState; // Global Key State


        // Gameplay
        private World world;
        private Player player;
        private Wall wall;


        // Collisions
        private CollisionComponent _collisionComponent;


        public SlimeGame()
        {

            // Graphics
            _graphics = new GraphicsDeviceManager(this);


            // Collisions
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, screenWidth, screenHeight));

            
            // Content 
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            base.Initialize();

            // Full Screen 
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Recourses
            Arts.Load(Content);

            // Create World Instance
            world = new World();
            player = new Player();
            wall = new Wall();

            // Collisions
            _collisionComponent.Insert(player);
            _collisionComponent.Insert(wall);


        }



        protected override void LoadContent()
        {
            
            // Try not to write somehting here

        }

        protected override void Update(GameTime gameTime)
        {
            // Exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Object
            world.Update(gameTime);
            player.Update(gameTime);
            wall.Update(gameTime);

            wall.rotation += 0.01f;

            // Collisions
            _collisionComponent.Update(gameTime);
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Drawing
            _spriteBatch.Begin();

            // All the Object should be drawn here
            world.Draw(_spriteBatch);
            player.Draw(_spriteBatch);
            wall.Draw(_spriteBatch);


            // End Draw
            _spriteBatch.End();
            

            base.Draw(gameTime);
        }

    }
}
