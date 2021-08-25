using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.Extended.Collisions;
using MonoGame.Extended;
using Microsoft.Xna.Framework;

namespace MonoGame_Slime.GameCore
{
    class Player : Object, ICollisionActor
    {

        // Collisions
        public IShapeF Bounds { get; }

        // Gravity
        public float gravity = 0.01f;
        public float maxSpeed = 10f;

        public Player()
        {
            image = Arts.Player;
            position = new Microsoft.Xna.Framework.Vector2(
                SlimeGame.screenWidth / 2 + 100f, 
                SlimeGame.screenHeight / 2);
            Bounds = new CircleF(position, image.Width / 2);
        }


        public override void Update(GameTime gameTime)
        {
            // Update Speed Value by gravity
            var currentSpeed = velocity.Y;
            var newSpeed = MathF.Min(currentSpeed + gravity, maxSpeed);

            // Apply new speed 
            velocity += new Vector2(0, newSpeed);
            Bounds.Position = position;
            base.Update(gameTime);
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            
            // Invert Y speed by some extend
            velocity = new Vector2(velocity.X, -velocity.Y);
        }
    }
}
