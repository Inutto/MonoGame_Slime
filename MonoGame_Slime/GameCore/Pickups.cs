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

        public bool startPlayerAnimation = false;
        public Player targetPlayer;

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
                ++slimegame.score;
                if (slimegame.score == slimegame.scoreMax) slimegame.gameState = SlimeGame.GAMESTATE.WIN;

                startPlayerAnimation = true;
                targetPlayer = slimegame.playerList[6];


            }
            Disable();    
            
        }

        public override void Update(GameTime gameTime)
        {
            // Draw the score!

            if (startPlayerAnimation)
            {
                startPlayerAnimation = false;
                StatUpdatePlayerTimer(targetPlayer, gameTime);
            }



            base.Update(gameTime);
        }

        public void StatUpdatePlayerTimer(Player player, GameTime gameTime)
        {
            // Player Animation (Blink)
            player.timer_blink.StartTimer(gameTime, 1);
            player.timer_notblink.StartTimer(gameTime, 500);
        }


    }
}
