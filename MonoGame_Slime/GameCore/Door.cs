using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Slime.Physics;

namespace MonoGame_Slime.GameCore
{
    class Door : Wall, IFreeRotation
    {

        public bool startPlayerAnimation = false;
        public Player targetPlayer;

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

                    startPlayerAnimation = true;
                    targetPlayer = slimegame.playerList[6];

                    var pickupSound = slimegame.Content.Load<SoundEffect>("PickupSound");
                    pickupSound.Play();
                    slimegame.gameState = SlimeGame.GAMESTATE.WIN;
                    Disable();

                }
            }
                

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

            player.timer_goto_happy.StartTimer(gameTime, 0);
            player.timer_goto_normal_constant.StartTimer(gameTime, 2000);
        }
    }
}
