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

        public Wall(Vector2 _position, float _rotation = 0f)
        {
            image = Arts.Wall;
            position = _position;
            rotation = _rotation;
            var startPosX = position.X - image.Width / 2;
            var startPoxY = position.Y - image.Height / 2;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
