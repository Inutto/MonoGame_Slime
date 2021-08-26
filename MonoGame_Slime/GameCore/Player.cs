﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame_Slime.Collisions;

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


    class Player : GameObject
    {


        public Circle boundBox;

        // Gravity
        public float gravity = 25f;
        public float maxSpeed = 300f;

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
            var newSpeed = velocity.Y + gravity;
            if(newSpeed > maxSpeed)
            {
                newSpeed = maxSpeed;
            } else if(newSpeed < -maxSpeed)
            {
                newSpeed = -maxSpeed;
            } 

            // Apply new speed 
            velocity = new Vector2(velocity.X, newSpeed);

            SlimeGame.debugText_1 = velocity.ToString();

            base.Update(gameTime);
        }

        public void OnCollision(CollisionEventArgs eventArgs)
        {

            var wall = eventArgs.coll as Wall;

            // Modify Compensation vec
            var compensationVec = eventArgs.compensationVec;
            var compensationMagnitude = eventArgs.compensationMagnitude;
            compensationVec.Normalize();

            // Invert Y speed by some extend
            var velocityAdd = new Vector2(position.X - wall.position.X, position.Y - wall.position.Y);
            velocityAdd.Normalize();

            // Move the player just out of the compensationvec direction

            position += compensationMagnitude * compensationVec;
            velocity += compensationVec * 50f;

            wall.color = Color.Red;

        }

        public void AwayFromWall(Vector2 awayDirection)
        {
            
        }
    }
}
