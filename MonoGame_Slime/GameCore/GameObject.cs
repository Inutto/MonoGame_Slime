using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;




namespace MonoGame_Slime.GameCore
{
    public class GameObject
    {

        // Graphics
        public Texture2D image;
        public Color color = Color.White;
        public Vector2 scale = Vector2.One;

        // Transform
        public Vector2 position; // absolute position, the center of image usually
        public Vector2 velocity; // for slime
        public float rotation;

        // World Rotation
        public Vector2 originPosition;




        private Vector2 GetCenterPosition()
        {
            if (image == null) return Vector2.Zero;
            return new Vector2(image.Width, image.Height);
        }

        public virtual void Update(GameTime gameTime)
        {
            position += velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var imageCenter = new Vector2(image.Width / 2, image.Height / 2);
            spriteBatch.Draw(image, position, null, color, rotation, imageCenter, scale, SpriteEffects.None, 0f);
        }

    }
}
