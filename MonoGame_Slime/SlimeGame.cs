using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Slime.GameCore;
using MonoGame_Slime.Collisions;
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
        private Wall wall_1;
        private Wall wall_2;
        private Wall wall_3;



        // Collisions
        private CollisionComponent _collisionComponent;

        // Debug
        private SpriteFont font;
        public static string debugText_1 = "";
        public static string debugText_2 = "";
        public static string debugText_3 = "";
        public static string debugText_4 = "";


        public SlimeGame()
        {

            // Graphics
            _graphics = new GraphicsDeviceManager(this);


            // Collisions
            _collisionComponent = new CollisionComponent();

            
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
            player = new Player(new Vector2(400,400));

            wall_1 = new Wall(new Vector2(400, 800));
            wall_2 = new Wall(new Vector2(200, 500));
            wall_3 = new Wall(new Vector2(700, 1200));



            _collisionComponent.AddPlayer(player);
            _collisionComponent.AddWall(wall_1);
            _collisionComponent.AddWall(wall_2);

            _collisionComponent.AddWall(wall_3);



            // Font
            font = Content.Load<SpriteFont>("Debug");


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
            wall_1.Update(gameTime);
            wall_2.Update(gameTime);
            wall_3.Update(gameTime);

            wall_1.rotation += 0.01f;

            wall_3.rotation += 0.04f;



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
            
            wall_1.Draw(_spriteBatch);
            wall_2.Draw(_spriteBatch);
            wall_3.Draw(_spriteBatch);
            player.Draw(_spriteBatch);

            // Debug
            _spriteBatch.DrawString(font, debugText_1, new Vector2(100, 100), Color.Black);
            _spriteBatch.DrawString(font, debugText_2, new Vector2(100, 200), Color.Black);
            _spriteBatch.DrawString(font, debugText_3, new Vector2(100, 300), Color.Black);
            _spriteBatch.DrawString(font, debugText_4, new Vector2(100, 400), Color.Black);




            // End Draw
            _spriteBatch.End();
            

            base.Draw(gameTime);
        }

    }
}
