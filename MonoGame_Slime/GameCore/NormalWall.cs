using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Slime.GameCore
{
    class NormalWall : Wall
    {
        
        public NormalWall(Vector2 _centerPosition, Vector2 _size, float _rotation = 0f) : base(_centerPosition, _size, _rotation)
        {
            // nothing to add
        }

    }
}
