﻿using System;
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
        public static Vector2 worldCenter;         // the rotation center, also the center of the world
        public static float worldRotation;         // the global orientation of the world

        // Child
        public List<Object> objectList; // store all the objects


        public World(Vector2 _center, Vector2 _size)
        {
            // Graphics
            image = Arts.World;

            // Transform
            worldCenter = _center;
            worldRotation = 0f;

            // Objects
            objectList = new List<Object>();
        }

        public void Update(GameTime gameTime)
        {
            // Use Mouse to control Rotation
            MouseState mouseState = Mouse.GetState();
            worldRotation = mouseState.X / 300f;

            // Move everyobject by changing their rotation and position
            foreach(var obj in objectList)
            {
                var objPos = obj.originPosition;
                var directionVec = objPos - worldCenter;
                var r = worldRotation;

                var cos = MathF.Cos(r);
                var sin = MathF.Sin(r);

                var x = directionVec.X;
                var y = directionVec.Y;




                var newX = cos * x - sin * y;
                var newY = sin * x + cos * y;

                var newVec = new Vector2(newX, newY);

                var newPos = newVec + worldCenter;

                // Assign Position
                obj.position = newPos;
                obj.rotation = worldRotation;

            }



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
            spriteBatch.Draw(image, worldCenter, null, color, worldRotation, imageCenter, Vector2.One, SpriteEffects.None, 0f);
        }

    }
}
