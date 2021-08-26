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

        public Wall()
        {
            image = Arts.Wall;
            rotation = 0.9f;
            position = new Vector2(
                SlimeGame.screenWidth / 2 + 50f,
                SlimeGame.screenHeight / 2 + 200f);
            var startPosX = position.X - image.Width / 2;
            var startPoxY = position.Y - image.Height / 2;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
