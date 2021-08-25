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

        public Vector2 position; // absolute position
        public Vector2 velocity; // for slime

        private Vector2 GetCenterPosition()
        {
            if (image == null) return Vector2.Zero;
            return new Vector2(image.Width, image.Height);
        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var imageCenter = new Vector2(image.Width / 2, image.Height / 2);
            spriteBatch.Draw(image, position, null, color, World.worldRotation, imageCenter, Vector2.One, SpriteEffects.None, 0f);
        }

    }
}
