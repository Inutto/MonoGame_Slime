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
        // State
        public bool isEnable = true;

        public Circle boundBox;

        // Gravity
        public float gravityMultiplier = 1f;
        public Vector2 gravityVec = new Vector2(0, 50f);
        public Vector2 gravityVecNormal = new Vector2(0, 50f);

        // Gravity Settings
        public float gravity = 50f;
        public float maxSpeed = 400f;

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
            var scaleMultiplier = 1.4f;
            scale = new Vector2(scaleMultiplier, scaleMultiplier);

            color = _color;
            
        }

        public void Disable()
        {
            isEnable = false;
            image = null;
            boundBox = new Circle(Vector2.Zero, 0); // Zero size
        }


        public override void Update(GameTime gameTime)
        {
           

            SlimeGame.debugText_1 = gravityVec.ToString();

            UpdateGravityVec();

            base.Update(gameTime);
        }


        public void UpdateGravityVec()
        {

            var newSpeedY = velocity.Y + gravity;
            if (newSpeedY > maxSpeed)
            {
                newSpeedY = maxSpeed;
            }
            else if (newSpeedY < -maxSpeed)
            {
                newSpeedY = -maxSpeed;
            }

            gravityVec = new Vector2(velocity.X, newSpeedY);

        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the circle boundry



            base.Draw(spriteBatch);
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
                // velocity = new Vector2(velocity.X, 0.1f);
                gravityVec = GetGravityIncrement(compensationVec * compensationMagnitude);
            }

            // Move the player just out of the compensationvec direction

            var deltaDistance = compensationMagnitude * compensationVec;
            position += deltaDistance;
            
        }


        /// <summary>
        /// Input the complete compensationVEc!
        /// </summary>
        /// <param name="compensationVec"></param>
        /// <returns></returns>
        public Vector2 GetGravityIncrement(Vector2 compensationVec)
        {
            var compensationMagnitude = compensationVec.Length();
            var rad = Math.Acos(compensationVec.Y / compensationMagnitude);
            var gravityVecMagnitude = compensationMagnitude / (float)Math.Cos(rad);
            var gravityVec = new Vector2(0, gravityVecMagnitude);

            return gravityVec + compensationVec;
        }
    }
}
