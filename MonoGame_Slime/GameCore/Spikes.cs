using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Slime.Physics;

namespace MonoGame_Slime.GameCore
{
    class Spikes : Wall
    {

        public Spikes(Vector2 _centerPosition, Vector2 _size, Texture2D texture, float _rotation = 0f) : base(_centerPosition, _size, texture, _rotation)
        {
            // nothing to add
        }

        public override void OnCollision(CollisionEventArgs eventArgs)
        {
            // Kill the player

            var slimegame = eventArgs.game as SlimeGame;
            var playerList = slimegame.playerList;

            foreach(var player in playerList)
            {
                if(player.isEnable) player.Disable();
            }
            Disable();
            

        }
    }
}
