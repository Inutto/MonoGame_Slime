using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using Microsoft.Xna.Framework;


namespace MonoGame_Slime.GameCore
{
    class Wall : Object
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

            // Transform
            position = _centerPosition;
            rotation = _rotation;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
