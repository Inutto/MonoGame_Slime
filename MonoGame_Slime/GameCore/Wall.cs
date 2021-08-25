using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using Microsoft.Xna.Framework;


namespace MonoGame_Slime.GameCore
{
    class Wall : Object, ICollisionActor
    {
        // Collisions
        public IShapeF Bounds { get; }


        public Wall()
        {
            image = Arts.Wall;
            position = new Vector2(
                SlimeGame.screenWidth / 2 + 100f,
                SlimeGame.screenHeight / 2 + 450f);
            var startPosX = position.X - image.Width / 2;
            var startPoxY = position.Y - image.Height / 2;
            Bounds = new RectangleF(startPosX, startPoxY, image.Width, image.Height);


        }

        public override void Update(GameTime gameTime)
        {
            Bounds.Position = position;
            base.Update(gameTime);
        }


        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            
        }
    }
}
