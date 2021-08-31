using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Slime.Physics;

namespace MonoGame_Slime.GameCore
{
    class Pickups : Wall
    {
        public Pickups(Vector2 _centerPosition, Vector2 _size, Texture2D texture, float _rotation = 0f) : base(_centerPosition, _size, texture, _rotation)
        {
            // nothing to add
        }

        public override void OnCollision(CollisionEventArgs eventArgs)
        {
            // Disable the draw (by swtiching texture to null)

            if (isEnable)
            {
                ++SlimeGame.score;
            }
            Disable();    
            
        }

        public override void Update(GameTime gameTime)
        {
            // Draw the score!

            SlimeGame.debugText_4 = string.Format("Score: {0}", SlimeGame.score);

            base.Update(gameTime);
        }


    }
}
