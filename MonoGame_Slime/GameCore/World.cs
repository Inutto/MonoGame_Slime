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
    class World
    {

        // Graphics
        public Texture2D image;
        public Color color = Color.White;

        // Transform
        public Vector2 position;
        public static Vector2 worldCenter;         // the rotation center, also the center of the world
        public static float worldRotation;         // the global orientation of the world

        // Child
        public List<Object> objectList; // store all the objects


        public World()
        {
            image = Arts.World;
            position = new Vector2(SlimeGame.screenWidth / 2 + 200f, SlimeGame.screenHeight / 2);
            worldCenter = new Vector2(SlimeGame.screenWidth / 2, SlimeGame.screenHeight / 2);
            worldRotation = 0f;

            objectList = new List<Object>();

            
            
        }

        public void Update(GameTime gameTime)
        {
            // Use Mouse to control Rotation
            MouseState mouseState = Mouse.GetState();
            worldRotation = mouseState.X / 300f;

            
        
        }

        public void AddObjectToWorldList(Object obj)
        {
            objectList.Add(obj);
        }

        public void RemoveObjectFromWorldList(Object obj)
        {
            objectList.Remove(obj);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var imageCenter = new Vector2(image.Width / 2, image.Height / 2);
            spriteBatch.Draw(image, position, null, color, worldRotation, imageCenter, Vector2.One, SpriteEffects.None, 0f);
        }

    }
}
