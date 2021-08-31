using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Slime.Physics;

namespace MonoGame_Slime.GameCore
{
    public class Wall : GameObject
    {
        // Collisions
        public Rectangle boundBox;
        public bool isEnable = true;

        public Wall(Vector2 _centerPosition, Vector2 _size, Texture2D texture, float _rotation = 0f)
        {
            // Graphics
            image = texture;

            var halfWidth = _size.X / 2;
            var halfHeight = _size.Y / 2;

            // Boundbox
            var tx = (int)(_centerPosition.X - halfWidth);
            var ty = (int)(_centerPosition.Y - halfHeight);
            var width = (int)_size.X;
            var height = (int)_size.Y;

            boundBox = new Rectangle(tx, ty, width, height);


            // Apply scale
            var scaleX = (float)boundBox.Width / (float)image.Width;
            var scaleY = (float)boundBox.Height / (float)image.Height;
            var scaleMultiplier = 1.1f;
            scale = new Vector2(scaleX, scaleY) * scaleMultiplier;

            // Transform
            position = _centerPosition;
            rotation = _rotation;
            originPosition = _centerPosition;
            color = Color.White;

        }

        public void Disable()
        {
            isEnable = false;
            image = null;
            boundBox = new Rectangle(0,0,0,0); // Zero size
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Just draw the bound
        }

        public virtual void OnCollision(CollisionEventArgs eventArgs)
        {
            // Implement this in child if needed
        }

    }
}
