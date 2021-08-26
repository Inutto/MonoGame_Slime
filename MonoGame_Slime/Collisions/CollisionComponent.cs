using System;
using System.Collections.Generic;
using System.Text;
using MonoGame_Slime.GameCore;
using Microsoft.Xna.Framework;

namespace MonoGame_Slime.Collisions
{


    public class CollisionEventArgs {
        public GameObject coll;
        public Vector2 compensationVec;
        public float compensationMagnitude;

        public CollisionEventArgs(Vector2 compensationVec)
        {
            this.compensationVec = compensationVec;
        }

        public CollisionEventArgs(Vector2 compensationVec, GameObject coll)
        {
            this.compensationVec = compensationVec;
            this.coll = coll;
        }
    }


    /// <summary>
    /// Collision System that contains all the objects's info
    /// </summary>
    class CollisionComponent
    {
        public Player player;           // Circle Player
        List<Wall> walls = new List<Wall>();               // Rectangle Walls



        public void Update(GameTime gameTime)
        {
            foreach(var wall in walls)
            {
                var eventArgs = isCollisionWithPlayer(wall);
                if (eventArgs != null)
                {
                    player.OnCollision(eventArgs);
                }
            }
        }

        public void AddWall(Wall wall)
        {
            walls.Add(wall);
        }

        public void RemoveWall(Wall wall)
        {
            walls.Remove(wall);
        }

        public void AddPlayer(Player _player)
        {
            player = _player;
        }



        /// <summary>
        /// Implementation of Collision
        /// </summary>
        /// <param name="rectanglePos"></param>
        /// <param name="circlePos"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="rotation"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static CollisionEventArgs isCollision(Vector2 rectanglePos, Vector2 circlePos, float width, float height, float rotation, float radius)
        {
            // Rotate the circle and rectangle to the horizontal level
            var originX = rectanglePos.X;
            var originY = rectanglePos.Y;

            var r = -rotation; // negative!
            var cx = circlePos.X;
            var cy = circlePos.Y;

            var newcx = MathF.Cos(r) * (cx - originX) - MathF.Sin(r) * (cy - originY) + originX;
            var newcy = MathF.Sin(r) * (cx - originX) + MathF.Cos(r) * (cy - originY) + originY;


            // Find the closet point from circle to rectanle (by finding the closestX and cloestY separately)

            var halfWidth = width / 2;
            var halfHeight = height / 2;

            float closeX = 0f;
            float closeY = 0f;


            if (newcx < originX - halfWidth)
            {
                closeX = originX - halfWidth;
            }
            else if (newcx > originX + halfWidth)
            {
                closeX = originX + halfWidth;
            }
            else
            {
                closeX = newcx;
            }


            if (newcy < originY - halfHeight)
            {
                closeY = originY - halfHeight;
            }
            else if (newcy > originY + halfHeight)
            {
                closeY = originY + halfHeight;
            }
            else
            {
                closeY = newcy;
            }


            // Calculate the distance with unrotated circle and compare with the radius of the circle

            var closePoint = new Vector2(closeX, closeY);
            var newCirclePoint = new Vector2(newcx, newcy);

            var distance = SlimeGame.GetDistance(closePoint, newCirclePoint);
            var directionalVec = newCirclePoint - closePoint;

            // Result
            var originDirectionVec = SlimeGame.RotateVector2(directionalVec, -r);  // Somehow need to fix this

            
            if (distance > radius)
            {
                return null;
            }
            else
            {
                var eventArgs = new CollisionEventArgs(originDirectionVec);
                eventArgs.compensationMagnitude = radius - distance;
                return eventArgs;
            }
        }


        /// <summary>
        /// The Core collision detection function
        /// </summary>
        /// <param name="wall"></param>
        /// <returns></returns>
        public CollisionEventArgs isCollisionWithPlayer(Wall wall)
        {
            var result = isCollision(
                wall.position, 
                player.position, 
                wall.boundBox.Width,
                wall.boundBox.Height, 
                wall.rotation,
                player.boundBox.radius);

            if(result != null)
            {
                result.coll = wall;
                return result;
            } else
            {
                return null;
            }

        }
    }
}
