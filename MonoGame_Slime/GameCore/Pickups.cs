﻿using System;
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

            var slimegame = eventArgs.game as SlimeGame;

            if (isEnable)
            {
                ++SlimeGame.score;
                if (SlimeGame.score == SlimeGame.scoreMax) slimegame.gameState = SlimeGame.GAMESTATE.WIN;
            }
            Disable();    
            
        }

        public override void Update(GameTime gameTime)
        {
            // Draw the score!

            SlimeGame.debugText_3 = string.Format("Score: {0}", SlimeGame.score);
            SlimeGame.debugText_4 = string.Format("Target Score: {0}", SlimeGame.scoreMax);

            base.Update(gameTime);
        }


    }
}
