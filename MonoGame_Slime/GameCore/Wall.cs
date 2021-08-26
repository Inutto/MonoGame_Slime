using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Slime.GameCore
{
    class Wall : GameObject
    {
        // Collisions
        public Rectangle boundBox;

        public Wall(Vector2 _centerPosition, Vector2 _size, float _rotation = 0f)
        {
            // Graphics
            image = Arts.Wall;

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
            scale = new Vector2(scaleX, scaleY);

            // Transform
            position = _centerPosition;
            rotation = _rotation;
            originPosition = _centerPosition;

            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SlimeGame.debugText_1 = scale.ToString();
            SlimeGame.debugText_2 = image.Width.ToString();


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Just draw the bound

            
        }

    }
}
