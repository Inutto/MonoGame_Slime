using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;




namespace MonoGame_Slime.GameCore
{
    /// <summary>
    /// The container of all game objects. Can only created by once. (Singleton)
    /// </summary>
    public class World
    {

        // Graphics
        public Texture2D image;
        public Color color = Color.White;

        // Transform
        public static Vector2 worldCenter;         // the rotation center, also the center of the world
        public float worldRotation;         // the global orientation of the world

        // Child
        public List<GameObject> objectList; // store all the objects


        public World(Vector2 _center, Vector2 _size)
        {


            


            // Graphics
            image = Arts.World;

            // Transform
            worldCenter = _center;
            worldRotation = 0f;

            // Objects
            objectList = new List<GameObject>();
        }

        private void KeyboardControlUpdate(GameTime gameTime)
        {
            var _keyboardState = Keyboard.GetState();
            var rotationSpeed = 1.5f; // the best para I can get
            var rotationSpeedMultiplier = 2f;

            var originSpeed = rotationSpeed;

            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                originSpeed *= rotationSpeedMultiplier;
            }

            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                
                worldRotation -= originSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                worldRotation += originSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            


        }

        public void Update(GameTime gameTime)
        {
            //// Use Mouse to control Rotation
            //MouseState mouseState = Mouse.GetState();
            //worldRotation = mouseState.X / 200f;

            // Keyboard Control
            KeyboardControlUpdate(gameTime);



            // Move everyobject by changing their rotation and position
            foreach (var obj in objectList)
            {
                var objPos = obj.originPosition;
                var directionVec = objPos - worldCenter;
                var r = worldRotation;
                var newVec = SlimeGame.RotateVector2(directionVec, worldRotation);

                var newPos = newVec + worldCenter;

                // Assign Position
                obj.position = newPos;
                obj.rotation = worldRotation;

            }



        }

        public void AddObjectToWorldRotationList(GameObject obj)
        {
            objectList.Add(obj);
        }

        public void RemoveObjectFromWorldList(GameObject obj)
        {
            objectList.Remove(obj);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var imageCenter = new Vector2(image.Width / 2, image.Height / 2);
            spriteBatch.Draw(image, worldCenter, null, color, worldRotation, imageCenter, Vector2.One, SpriteEffects.None, 0f);
        }

    }
}
