using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using MonoGame_Slime.Physics;

namespace MonoGame_Slime.GameCore
{
    class Spikes : Wall
    {

        public Spikes(Vector2 _centerPosition, Vector2 _size, Texture2D texture, float _rotation = 0f) : base(_centerPosition, _size, texture, _rotation)
        {
            
        }

        public override void OnCollision(CollisionEventArgs eventArgs)
        {
            // Kill the player

            var slimegame = eventArgs.game as SlimeGame;
            var playerList = slimegame.playerList;

            var spikeSound = slimegame.Content.Load<SoundEffect>("SpikeSound");
            spikeSound.Play();

            foreach (var player in playerList)
            {
                if(player.isEnable) player.Disable();
                slimegame.gameState = SlimeGame.GAMESTATE.LOSE;
            }
            Disable();
            

        }
    }
}
