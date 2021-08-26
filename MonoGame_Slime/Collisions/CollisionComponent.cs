using System;
using System.Collections.Generic;
using System.Text;
using MonoGame_Slime.GameCore;
using Microsoft.Xna.Framework;

namespace MonoGame_Slime.Collisions
{




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
                if (isCollisionWithPlayer(wall))
                {
                    player.OnCollision(wall);
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
        /// The Core collision detection function
        /// </summary>
        /// <param name="wall"></param>
        /// <returns></returns>
        public bool isCollisionWithPlayer(Wall wall)
        {
            // Rotate the circle and rectangle to the horizontal level
            var originX = wall.position.X;
            var originY = wall.position.Y;

            var r = wall.rotation;
            var cx = player.position.X;
            var cy = player.position.Y;

            SlimeGame.debugText_1 = r.ToString();

            var newcx = MathF.Cos(r) * (cx - originX) - MathF.Sin(r) * (cy - originY) + originX;
            var newcy = MathF.Sin(r) * (cx - originX) + MathF.Cos(r) * (cy - originY) + originY;


            // Find the closet point from circle to rectanle (by finding the closestX and cloestY separately)

            var halfWidth = wall.image.Width / 2;
            var halfHeight = wall.image.Height / 2;

            float closeX = 0f;
            float closeY = 0f;


            if(newcx < originX - halfWidth)
            {
                closeX = originX - halfWidth;
            } else if(newcx > originX + halfWidth)
            {
                closeX = originX + halfWidth;
            } else
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

            SlimeGame.debugText_2 = new Vector2(closeX, closeY).ToString();
            // Calculate the distance with unrotated circle and compare with the radius of the circle


            var distance = MathF.Sqrt(MathF.Pow(closeX - cx, 2) + MathF.Pow(closeY - cy, 2));
            var radius = player.image.Width / 2;

            SlimeGame.debugText_3 = distance.ToString();
            SlimeGame.debugText_4 = radius.ToString();


            if (distance > radius)
            {
                return false;
            } else
            {
                return true;
            }
        }


    }
}
