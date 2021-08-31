using System;
using System.Collections.Generic;
using System.Text;
using MonoGame_Slime.GameCore;
using Microsoft.Xna.Framework;

namespace MonoGame_Slime.Physics
{
    class GravityComponent
    {
        public List<GameObject> gravityObjectList = new List<GameObject>();

        // Gravity Settings
        public float gravity = 50f;
        public float maxSpeed = 400f;



        public void Update(GameTime gameTime)
        {
            foreach(var obj in gravityObjectList)
            {

                Vector2 newSpeed = Vector2.Zero;
                if(obj is Player)
                {
                    // Update Speed by player gravity

                    newSpeed = (obj as Player).gravityVec;

                } else
                {
                    // Update Speed Value by gravity
                    var newSpeedY = obj.velocity.Y + gravity;
                    if (newSpeedY > maxSpeed)
                    {
                        newSpeedY = maxSpeed;
                    }
                    else if (newSpeedY < -maxSpeed)
                    {
                        newSpeedY = -maxSpeed;
                    }

                    newSpeed = new Vector2(obj.velocity.X, newSpeedY);
                }

                // Apply new speed 
                obj.velocity = newSpeed;

            }
        }

        public void AddGameObject(GameObject obj)
        {
            gravityObjectList.Add(obj);
        }

    }
}
