using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Slime.GameCore
{



    struct Circle
    {
        public Vector2 position;
        public float radius;

        public Circle(Vector2 _position, float _radius)
        {
            position = _position;
            radius = _radius;
        }
    }


    class Player : Object
    {


        public Circle boundBox;

        // Gravity
        public float gravity = 0.0001f;
        public float maxSpeed = 10f;

        public Player(Vector2 _centerPosition, float _radius, float _rotation = 0f)
        {
            // Graphics
            image = Arts.Player;

            // Boundbox (Circle)
            boundBox = new Circle(_centerPosition, _radius);

            // Transform
            position = _centerPosition;
            rotation = _rotation;
            
        }


        public override void Update(GameTime gameTime)
        {
            // Update Speed Value by gravity
            
            var currentSpeed = velocity.Y;
            var newSpeed = MathF.Min(currentSpeed + gravity, maxSpeed);

            // Apply new speed 
            velocity += new Vector2(0, newSpeed);
            
            
            // Debug
            /*
            MouseState mouseState = Mouse.GetState();
            position.X = mouseState.X;
            position.Y = mouseState.Y;
            */
            
            


            base.Update(gameTime);
        }

        public void OnCollision(Wall wall)
        {
            // Invert Y speed by some extend
            var velocityAdd = new Vector2(position.X - wall.position.X, position.Y - wall.position.Y);
            velocityAdd.Normalize();
            position += velocityAdd;
            velocity = new Vector2(velocity.X + velocityAdd.X * 2f, 0);
            wall.color = Color.Red;
            Console.WriteLine("Coli");
        }
    }
}
