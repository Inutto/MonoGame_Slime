using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;



namespace MonoGame_Slime.GameCore
{
    class Object : ICollisionActor
    {
        protected Texture2D image;
        protected Color color = Color.White;

        // Transform
        public Vector2 position; // absolute position, the center of image usually
        public Vector2 velocity; // for slime
        public float rotation;

        // Collisions
        public IShapeF Bounds { get; }
        public bool isGravity = false;



        public Object()
        {
            Bounds = 
        }


        private Vector2 GetCenterPosition()
        {
            if (image == null) return Vector2.Zero;
            return new Vector2(image.Width, image.Height);
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var imageCenter = new Vector2(image.Width / 2, image.Height / 2);
            spriteBatch.Draw(image, position, null, color, rotation, imageCenter, Vector2.One, SpriteEffects.None, 0f);
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            
        }



    }
}
