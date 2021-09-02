using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Slime.Physics;

namespace MonoGame_Slime.GameCore
{
    class Door : Wall
    {
        public Door(Vector2 _centerPosition, Vector2 _size, Texture2D texture, float _rotation = 0f) : base(_centerPosition, _size, texture, _rotation)
        {

        }

        public override void OnCollision(CollisionEventArgs eventArgs)
        {
            // Disable the draw (by swtiching texture to null)

            var slimegame = eventArgs.game as SlimeGame;

            if (isEnable)
            {
                if (slimegame.score == slimegame.scoreMax)
                {
                    slimegame.gameState = SlimeGame.GAMESTATE.WIN;
                    Disable();

                }
            }
                

        }
    }
}
