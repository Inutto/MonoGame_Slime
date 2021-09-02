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

            player.timer_goto_blink.StartTimer(gameTime, 3000);
            player.timer_goto_normal.StartTimer(gameTime, 3200);
            player.timer_goto_happy.StartTimer(gameTime, 0);
            player.timer_goto_normal_constant.StartTimer(gameTime, 2000);
        }


    }
}
