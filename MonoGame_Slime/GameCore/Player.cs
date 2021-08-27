using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame_Slime.Physics;

namespace MonoGame_Slime.GameCore
{



    public struct Circle
    {
        public Vector2 position;
        public float radius;

        public Circle(Vector2 _position, float _radius)
        {
            
            position = _position;
            radius = _radius;
        }
    }


    public class Player : GameObject
    {


        public Circle boundBox;

        // Gravity
        public float gravity = 25f;
        public float maxSpeed = 300f;

        public Player(Vector2 _centerPosition, float _radius, Color _color, float _rotation = 0f)
        {


            // Graphics
            image = Arts.LoadPlayerArt();

            // Boundbox (Circle)
            boundBox = new Circle(_centerPosition, _radius);

            // Transform
            position = _centerPosition;
            rotation = _rotation;

            // Apply Scale
            //var scaleR = (float)boundBox.radius / (float)(image.Width / 2);
            var scaleMultiplier = 2f;
            scale = new Vector2(scaleMultiplier, scaleMultiplier);

            color = _color;
            
        }


        public override void Update(GameTime gameTime)
        {
           

            SlimeGame.debugText_1 = velocity.ToString();

            base.Update(gameTime);
        }

        public void OnCollision(CollisionEventArgs eventArgs)
        {


            var coll = eventArgs.coll;
           
            // Modify Compensation vec
            var compensationVec = eventArgs.compensationVec;
            var compensationMagnitude = eventArgs.compensationMagnitude;
            compensationVec.Normalize();

            if (coll is Wall)
            {
                compensationMagnitude *= 1f;
                velocity = new Vector2(velocity.X, 0f);
            }
            else
            {
                compensationMagnitude *= 1f;
                
            }

            // Move the player just out of the compensationvec direction

            var deltaDistance = compensationMagnitude * compensationVec;
            position += deltaDistance;
            
            
            

            


        }
    }
}
