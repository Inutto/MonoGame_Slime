using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace MonoGame_Slime.GameCore
{
    class Object
    {
        protected Texture2D image;
        protected Color color = Color.White;

        public Vector2 position;
        public Vector2 velocity; // for slime


        public virtual void Update()
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draw the sprite
        }

    }
}
