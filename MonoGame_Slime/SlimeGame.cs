using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Slime.GameCore;
using MonoGame_Slime.Physics;
using System.Collections.Generic;
using System;



namespace MonoGame_Slime
{
    public class SlimeGame : Game, IDisposable
    {

        // Singleton
        public static SlimeGame Instance { get; set; }

        // Graphics
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static int screenWidth = 1920;
        public static int screenHeight = 1080;

        // World Constant
        public static float worldSizeMultiplier = 0.9f;
        public static int worldWidth = (int)(1024 * worldSizeMultiplier);
        public static int worldHeight = (int)(768 * worldSizeMultiplier);
       
        // World 
        private World world;
        private Vector2 worldCenter = new Vector2(screenWidth / 2, screenHeight / 2);
        private Vector2 worldSize = new Vector2(worldWidth, worldHeight);

        // GameObjects
        private List<Wall> wallList = new List<Wall>();
        public  List<Player> playerList = new List<Player>();

        // Physics 
        private CollisionComponent _collisionComponent;
        private GravityComponent _gravityComponent;
        private ConstraintComponent _constraintComponent;

        // Score
        public int score = 0;
        public int scoreMax = 2;

        // Game Status
        public enum GAMESTATE { GAME, WIN, LOSE};
        public GAMESTATE gameState = GAMESTATE.GAME;
        
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
            
            // Physics
            _collisionComponent = new CollisionComponent(this);
            _gravityComponent = new GravityComponent();

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


        }



        private void AddWorld()
        {

           
            // Create World Instance
            world = new World(worldCenter, worldSize);
        }

        private void AddPlayers()
        {
            // Player Parameters
            var newPlayerPos = worldCenter + new Vector2(0, -200f);
            var playerRadius = 20f;
            var commonMaxDistance = 50f;
            var commonMinDistance = 30f;
            var playerParametersMultiplier = 0.65f;

            // Add players
            var normalColor = Color.White;
            var notNormalColor = Color.Red;

            var player1 = new Player(newPlayerPos, playerRadius, normalColor);
            player1.image = Arts.Player_Normal;
            player1.scale = Vector2.One * 0.32f;

            // Animation
            var gameTime = new GameTime();

            player1.timer_goto_blink.StartTimer(gameTime, 3000);
            player1.timer_goto_normal.StartTimer(gameTime, 3200);


            var player2 = new Player(newPlayerPos + new Vector2(0, -100) * playerParametersMultiplier, playerRadius, normalColor);
            var player3 = new Player(newPlayerPos + new Vector2(87, -50) * playerParametersMultiplier, playerRadius, normalColor);

            var player4 = new Player(newPlayerPos + new Vector2(87, 50) * playerParametersMultiplier, playerRadius, normalColor);
            var player5 = new Player(newPlayerPos + new Vector2(0, 100) * playerParametersMultiplier, playerRadius, normalColor);
            var player6 = new Player(newPlayerPos + new Vector2(-87, 50) * playerParametersMultiplier, playerRadius, normalColor);
            var player7 = new Player(newPlayerPos + new Vector2(-87, -50) * playerParametersMultiplier, playerRadius, normalColor);


            player2.image = Arts.Player_2;
            player3.image = Arts.Player_3;
            player4.image = Arts.Player_4;
            player5.image = Arts.Player_5;
            player6.image = Arts.Player_2;
            player7.image = Arts.Player_3;



            playerList.Add(player2);
            playerList.Add(player3);
            playerList.Add(player4);
            playerList.Add(player5);
            playerList.Add(player6);
            playerList.Add(player7);
            playerList.Add(player1);

            // Physics
            foreach (var player in playerList)
            {
                _collisionComponent.AddPlayer(player);
                _gravityComponent.AddGameObject(player);
                // world.AddObjectToWorldRotationList(player);
            }

            _constraintComponent.AddConstraintPair(player1, player2, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player1, player3, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player1, player4, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player1, player5, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player1, player6, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player1, player7, commonMaxDistance, commonMinDistance);

            _constraintComponent.AddConstraintPair(player2, player3, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player3, player4, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player4, player5, commonMaxDistance, commonMinDistance);
            // _constraintComponent.AddConstraintPair(player5, player6, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player6, player7, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player7, player2, commonMaxDistance, commonMinDistance);


 
        }

        private void AddWalls()
        {
            // Wall parameters
            float wallWidth = 750f;
            var boundBoxWallSizeHorizontal = new Vector2(worldSize.X + 2 * wallWidth, wallWidth);
            var boundBoxWallSizeVertical = new Vector2(wallWidth, worldSize.Y + 2 * wallWidth);

            float wallOffSetX = (worldSize.X + wallWidth) / 2f;
            float wallOffsety = (worldSize.Y + wallWidth) / 2f;

            var normalWallTexture = Arts.Wall;
            var spikeTexture = Arts.Spike_1;


            // add obstagles walls
            var wall_obs_Offset = new Vector2(256, 250);
            var wall_obs_size = new Vector2(100, 400);


            var wall_obs_1 = new NormalWall(worldCenter + wall_obs_Offset, wall_obs_size, normalWallTexture);
            var wall_obs_2 = new NormalWall(worldCenter - wall_obs_Offset, wall_obs_size, normalWallTexture);

            wallList.Add(wall_obs_1);
            wallList.Add(wall_obs_2);


            // add border walls
            var wall_1 = new NormalWall(worldCenter + new Vector2(0, wallOffsety), boundBoxWallSizeHorizontal, normalWallTexture);
            var wall_2 = new NormalWall(worldCenter + new Vector2(0, -wallOffsety), boundBoxWallSizeHorizontal, normalWallTexture);

            var wall_3 = new NormalWall(worldCenter + new Vector2(wallOffSetX, 0), boundBoxWallSizeVertical, normalWallTexture);
            var wall_4 = new NormalWall(worldCenter + new Vector2(-wallOffSetX, 0), boundBoxWallSizeVertical, normalWallTexture);

            wallList.Add(wall_1);
            wallList.Add(wall_2);
            wallList.Add(wall_3);
            wallList.Add(wall_4);

            // add visual border wall (dont' add to the wallList)

            var wall_left = new NormalWall(worldCenter + new Vector2(wallOffSetX, 0), boundBoxWallSizeVertical, Arts.World);
            var wall_right = new NormalWall(worldCenter + new Vector2(-wallOffSetX, 0), boundBoxWallSizeVertical, Arts.World);

            // add rotating wall (not add to wallList)
            var wall_rotate_size = new Vector2(70, 240);
            var wall_rotate = new RotatingWall(worldCenter, wall_rotate_size, normalWallTexture);

            wallList.Add(wall_rotate);



            // add pickups and spikes

            var pickup_size = new Vector2(50, 50);
            var pickup_position_offset = new Vector2(400, 200);

            var pickup_1 = new Pickups(worldCenter + pickup_position_offset, pickup_size, Arts.Pickup);
            var pickup_2 = new Pickups(worldCenter - pickup_position_offset, pickup_size, Arts.Pickup);


            var spike_position_offset = pickup_position_offset * new Vector2(1, -1);
            var spike_1 = new Spikes(worldCenter + spike_position_offset, pickup_size * 1.3f, Arts.Spike_3);
            var spike_2 = new Spikes(worldCenter - spike_position_offset, -pickup_size * 1.3f, Arts.Spike_3);


            pickup_1.color = Color.Aqua;
            pickup_2.color = Color.Aqua;


            spike_1.color = Color.Red;
            spike_2.color = Color.Red;

            wallList.Add(pickup_1);
            wallList.Add(pickup_2);

            wallList.Add(spike_1);
            wallList.Add(spike_2);



            // Physics
            foreach (var wall in wallList)
            {
                if(!(wall is RotatingWall))
                {
                    world.AddObjectToWorldRotationList(wall);
                }
                _collisionComponent.AddWall(wall);
                
            }
        }


        protected override void Update(GameTime gameTime)
        {
            // Exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Program.restart = true;
                Exit();
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
        }

        public void OnGameLose()
        {
            var image = Arts.GameLose;
            DrawNormalImage(_spriteBatch, image, worldCenter, Color.White);
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
