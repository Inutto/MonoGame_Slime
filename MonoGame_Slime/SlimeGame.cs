using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Slime.GameCore;
using MonoGame_Slime.Physics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;



namespace MonoGame_Slime
{


    public class SlimeGame : Game, IDisposable
    {

        // Singleton
        public static SlimeGame Instance { get; set; }

        // Graphics
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public static int screenWidth = 1920;
        public static int screenHeight = 1080;

        // World Constant
        public static float worldSizeMultiplier = 0.9f;
        public static int worldWidth = (int)(1024 * worldSizeMultiplier);
        public static int worldHeight = (int)(768 * worldSizeMultiplier);

        // World 
        public World world;
        public Vector2 worldCenter = new Vector2(screenWidth / 2, screenHeight / 2);
        public Vector2 worldSize = new Vector2(worldWidth, worldHeight);

        // GameObjects
        public List<Wall> wallList = new List<Wall>();
        public List<Player> playerList = new List<Player>();

        // Physics 
        public CollisionComponent _collisionComponent;
        public GravityComponent _gravityComponent;
        public ConstraintComponent _constraintComponent;

        // Score
        public int score = 0;
        public int scoreMax = 2;

        // Game Status
        public enum GAMESTATE { GAME, WIN, LOSE};
        public GAMESTATE gameState = GAMESTATE.GAME;

        // Dummy
        public bool dummymode = false;
        public Song music;


        // Debug
        public SpriteFont font;
        public static string debugText_1 = "";
        public static string debugText_2 = "";
        public static string debugText_3 = "";
        public static string debugText_4 = "";




        public SlimeGame()
        {

            // Graphics
            _graphics = new GraphicsDeviceManager(this);
            
            // Physics
            _collisionComponent = new CollisionComponent(this);
            _gravityComponent = new GravityComponent();

            // Content 
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
        }

        public SlimeGame(bool isDummy)
        {
            dummymode = true;

            // Graphics
            _graphics = new GraphicsDeviceManager(this);

            // Content 
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {

            base.Initialize();

            if (dummymode) {
                music = Content.Load<Song>("BackgroundMusic");
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(music);
                return;
            } 


            // Full Screen 
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _constraintComponent = new ConstraintComponent(_spriteBatch);



            // Load Recourses
            Arts.Load(Content);

            // World
            AddWorld();

            // GameObject
            AddPlayers();
            AddWalls();

            // Font
            font = Content.Load<SpriteFont>("Debug");

            // BGM
            //MediaPlayer.Play(Arts.BackgroundMusic);
            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.MediaStateChanged += OnMediaStateChanged;


        }

        //protected void OnMediaStateChanged(object sender, EventArgs e)
        //{
        //    MediaPlayer.Volume = 0.8f;
        //    MediaPlayer.Play(Arts.BackgroundMusic);
        //}

        protected virtual void AddWorld()
        {
            // Create World Instance
            world = new World(worldCenter, worldSize);
        }

        protected virtual void AddPlayers()
        {
           
        }

        protected virtual void AddWalls()
        {
           
        }


        protected override void Update(GameTime gameTime)
        {

            if (dummymode) return;


            // Exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                GotoCurrentLevel();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                GotoNextLevel();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                GotoPreviousLevel();
            }

            // Object
            world.Update(gameTime);
            foreach (var player in playerList) player.Update(gameTime);
            foreach (var wall in wallList) wall.Update(gameTime);

            // Physics (The following order matters!)
            
            _gravityComponent.Update(gameTime);
            _collisionComponent.Update(gameTime);
            _constraintComponent.Update(gameTime);
            _collisionComponent.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            if (dummymode) return;

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Drawing
            _spriteBatch.Begin();

            // All the Object should be drawn here
            world.Draw(_spriteBatch);
            
            foreach(var wall in wallList) wall.Draw(_spriteBatch);
            foreach(var player in playerList) player.Draw(_spriteBatch);

            // Draw Debug
            DebugDraw();

            // Draw GameState
            GameStateDraw();

            // Draw Constraint
            // _constraintComponent.Draw(_spriteBatch);

            // End Draw
            _spriteBatch.End();
            

            base.Draw(gameTime);
        }

        public void GameStateDraw()
        {
            switch (gameState)
            {
                case GAMESTATE.LOSE:
                    OnGameLose();
                    break;
                case GAMESTATE.WIN:
                    OnGameWin();
                    break;
            }
        }

        public void DebugDraw()
        {
            // Debug
            _spriteBatch.DrawString(font, debugText_1, new Vector2(100, 100), Color.White);
            _spriteBatch.DrawString(font, debugText_2, new Vector2(100, 200), Color.White);
            _spriteBatch.DrawString(font, debugText_3, new Vector2(100, 300), Color.White);
            _spriteBatch.DrawString(font, debugText_4, new Vector2(100, 400), Color.White);
        }


        public void OnGameWin()
        {
            var image = Arts.GameWin;
            DrawNormalImage(_spriteBatch, image, worldCenter, Color.White);

            // Goto Next Level (without consent)
            
        }

        public void OnGameLose()
        {
            var image = Arts.GameLose;
            DrawNormalImage(_spriteBatch, image, worldCenter, Color.White);
        }

        public void GotoLevel(int levelIndex)
        {

            Program.currentLevel = levelIndex;
            Program.restart = true;
            Exit();

        }

        public void GotoNextLevel()
        {
            var current = Program.currentLevel;
            current = Math.Min(Program.maxLevel, current + 1);
            GotoLevel(current);
        }

        public void GotoPreviousLevel()
        {
            var current = Program.currentLevel;
            current = Math.Max(Program.minLevel, current - 1);
            GotoLevel(current);
        }

        public void GotoCurrentLevel()
        {
            GotoLevel(Program.currentLevel);
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

        public static void DrawNormalImage(SpriteBatch spriteBatch, Texture2D image, Vector2 position, Color color, float rotation = 0f)
        {
            if (image != null)
            {
                var imageCenter = new Vector2(image.Width / 2, image.Height / 2);
                var scale = 1f;
                spriteBatch.Draw(image, position, null, color, rotation, imageCenter, scale, SpriteEffects.None, 0f);
            }
        }



        



    }
}
