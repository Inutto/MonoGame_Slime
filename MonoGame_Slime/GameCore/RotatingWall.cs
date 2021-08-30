using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Slime.GameCore
{
    public class RotatingWall : Wall
    {

        public float rotationSpeed = -0.05f;

        public RotatingWall(Vector2 _centerPosition, Vector2 _size, float _rotation = 0f) : base(_centerPosition, _size, _rotation)
        {
            // nothing to add
        }

        public override void Update(GameTime gameTime)
        {
            rotation += rotationSpeed;
            base.Update(gameTime);
        }


    }
}
