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
        // Rotation
        public Vector2 center;          // the rotation center, also the center of the world
        public float direction;         // the global orientation of the world

        // Child
        public List<Object> objectList; // store all the objects

        public void Update()
        {

        }

        public void Draw()
        {

        }

    }
}
