using System;
using System.Collections.Generic;
using System.Text;
using MonoGame_Slime.Physics;
using MonoGame_Slime.GameCore;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Slime.Physics
{


    public class ConstraintPairArgs
    {
        public Player player1;
        public Player player2;
        public float constraintDistance;
    }
        




    /// <summary>
    /// Define the Comstraint Between 2 players. player only.
    /// </summary>
    class ConstraintComponent
    {
        List<ConstraintPairArgs> constraintPairList = new List<ConstraintPairArgs>();

        public static float defaultConstraintDistance = 250f;



        // Drawing Constraint Para
        public Texture2D drawingTexture;
        public Color drawingColor = Color.Bisque;
        public Color drawingMaxDistanceColor = Color.DarkOrange;



        public ConstraintComponent(SpriteBatch spriteBatch)
        {
            drawingTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            drawingTexture.SetData(new[] { drawingColor });
        }


        /// <summary>
        /// Constraint every pair in th playerPairDic
        /// </summary>
        /// <param name="gameTime"></param>
        /// 
        public void Update(GameTime gameTime)
        {
            foreach(var playerPairArgs in constraintPairList)
            {
                CheckConstraintPair(playerPairArgs);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the constraint
            foreach (var playerPairArgs in constraintPairList)
            {
                var point1 = playerPairArgs.player1.position;
                var point2 = playerPairArgs.player2.position;
                var thickness = 4f;

                var distance = SlimeGame.GetDistance(point1, point2);
                var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
                var origin = new Vector2(0f, 0.5f);
                var scale = new Vector2(distance, thickness);

                var color = new Color();

                if(distance < playerPairArgs.constraintDistance)
                {
                    color = drawingColor;
                } else
                {
                    color = drawingMaxDistanceColor;
                }

                spriteBatch.Draw(drawingTexture, point1, null, color, angle, origin, scale, SpriteEffects.None, 0);
            }
        }


        /// <summary>
        /// Push player2 towards player1
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public void CheckConstraintPair(ConstraintPairArgs constraintPairArgs)
        {

            var player1 = constraintPairArgs.player1;
            var player2 = constraintPairArgs.player2;
            var constraintDistance = constraintPairArgs.constraintDistance;

            // The actual distance
            var distance = SlimeGame.GetDistance(player1.position, player2.position);

            if(distance > constraintDistance)
            {
                // Push player2 towards player1
                var pushVec = (player1.position - player2.position);
                var pushDistance = distance - constraintDistance;
                pushVec.Normalize();

                player2.position += pushVec * pushDistance;
            }


        }

        public void AddConstraintPair(Player player1, Player player2, float constraintDistance)
        {
            var newArgs = new ConstraintPairArgs();
            newArgs.player1 = player1;
            newArgs.player2 = player2;
            newArgs.constraintDistance = constraintDistance;

            constraintPairList.Add(newArgs);
        }

    }
}
