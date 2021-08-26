using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Slime.GameCore;
using MonoGame_Slime.Collisions;
using System.Collections.Generic;
using System;


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
        

        // Wall (should figure out the efficienty way to add this)
        private List<Wall> wallList = new List<Wall>();
        private List<Player> playerList = new List<Player>();


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

            
            // World init parameters
            var worldCenter = new Vector2(screenWidth / 2, screenHeight / 2);
            var worldSize = new Vector2(1024, 768);
            var newPlayerPos = worldCenter + new Vector2(0,-200f);

            // Create World Instance
            world = new World(worldCenter, worldSize);


            // Add players
            var player1 = new Player(newPlayerPos, 100);
            var player2 = new Player(newPlayerPos + new Vector2(200,200), 100);

            playerList.Add(player1);
            playerList.Add(player2);

            // Create Walls and add them to world
            float wallWidth = 60f;
            var boundBoxWallSizeHorizontal = new Vector2(worldSize.X, wallWidth);
            var boundBoxWallSizeVertical = new Vector2(wallWidth, worldSize.Y);

            float wallOffSetX = (worldSize.X - wallWidth) / 2f;
            float wallOffsety = (worldSize.Y - wallWidth) / 2f;

            // add wall
            var wall_1 = new Wall(worldCenter + new Vector2(0, wallOffsety), boundBoxWallSizeHorizontal);
            var wall_2 = new Wall(worldCenter + new Vector2(0, -wallOffsety), boundBoxWallSizeHorizontal);

            var wall_3 = new Wall(worldCenter + new Vector2(wallOffSetX, 0), boundBoxWallSizeVertical);
            var wall_4 = new Wall(worldCenter + new Vector2(-wallOffSetX, 0), boundBoxWallSizeVertical);

            wallList.Add(wall_1);
            wallList.Add(wall_2);
            wallList.Add(wall_3);
            wallList.Add(wall_4);

            
            foreach(var player in playerList)
            {
                _collisionComponent.AddPlayer(player);
            }


            foreach (var wall in wallList)
            {
                _collisionComponent.AddWall(wall);
                world.AddObjectToWorldList(wall);
            }

            // world.AddObjectToWorldList(player);

           
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
            foreach (var player in playerList) player.Update(gameTime);
            foreach (var wall in wallList) wall.Update(gameTime);

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
            
            foreach(var wall in wallList) wall.Draw(_spriteBatch);
            foreach(var player in playerList) player.Draw(_spriteBatch);

            // Debug
            _spriteBatch.DrawString(font, debugText_1, new Vector2(100, 100), Color.Black);
            _spriteBatch.DrawString(font, debugText_2, new Vector2(100, 200), Color.Black);
            _spriteBatch.DrawString(font, debugText_3, new Vector2(100, 300), Color.Black);
            _spriteBatch.DrawString(font, debugText_4, new Vector2(100, 400), Color.Black);


            // End Draw
            _spriteBatch.End();
            

            base.Draw(gameTime);
        }




        // Tools

        public static float GetDistance(Vector2 a, Vector2 b)
        {
            var distance = MathF.Sqrt(MathF.Pow(a.X - b.X, 2) + MathF.Pow(a.Y - b.Y, 2));
            return distance;
        }

        public static Vector2 RotateVector2(Vector2 originVec, float rad)
        {
            var cos = MathF.Cos(rad);
            var sin = MathF.Sin(rad);

            var x = originVec.X;
            var y = originVec.Y;

            var newX = cos * x - sin * y;
            var newY = sin * x + cos * y;

            var newVec = new Vector2(newX, newY);

            return newVec;

        }

    }
}
