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
        public float gravity = 25f;
        public float maxSpeed = 300f;



        public void Update(GameTime gameTime)
        {
            foreach(var obj in gravityObjectList)
            {
                // Update Speed Value by gravity
                var newSpeed = obj.velocity.Y + gravity;
                if (newSpeed > maxSpeed)
                {
                    newSpeed = maxSpeed;
                }
                else if (newSpeed < -maxSpeed)
                {
                    newSpeed = -maxSpeed;
                }

                // Apply new speed 
                obj.velocity = new Vector2(obj.velocity.X, newSpeed);

            }
        }

        public void AddGameObject(GameObject obj)
        {
            gravityObjectList.Add(obj);
        }

    }
}
