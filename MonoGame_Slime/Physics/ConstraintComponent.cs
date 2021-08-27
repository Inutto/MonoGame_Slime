using System;
using System.Collections.Generic;
using System.Text;
using MonoGame_Slime.Physics;
using MonoGame_Slime.GameCore;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGame_Slime.Physics
{
    /// <summary>
    /// Define the Comstraint Between 2 players. player only.
    /// </summary>
    class ConstraintComponent
    {
        Dictionary<Player, Player> playerConstraintPairDic = new Dictionary<Player, Player>(); // Single Direct Connection



        /// <summary>
        /// Constraint every pair in th playerPairDic
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach(var playerPair in playerConstraintPairDic)
            {
                var player1 = playerPair.Key;
                var player2 = playerPair.Value;


                // Constraint them after calculating gravity


            }
        }

    }
}
