using System;
using System.Collections.Generic;
using System.Text;
using MonoGame_Slime.Physics;
using MonoGame_Slime.GameCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Slime.Physics
{


    public class ConstraintPairArgs
    {
        public Player player1;
        public Player player2;
        public float maxConstraintDistance;
        public float minConstraintDistance;
    }
        
    /// <summary>
    /// Define the Comstraint Between 2 players. player only.
    /// </summary>
    public class ConstraintComponent
    {
        List<ConstraintPairArgs> constraintPairList = new List<ConstraintPairArgs>();

        public static float defaultConstraintDistance = 250f;



        // Drawing Constraint Para
        public Texture2D drawingTexture;
        public Color drawingColor = Color.Bisque;
        public Color drawingMaxDistanceColor = Color.DarkOrange;
        public Color drawingMinDistanceColor = Color.BlueViolet;



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

                if(distance < playerPairArgs.maxConstraintDistance)
                {
                    color = drawingColor;
                } else if(distance > playerPairArgs.minConstraintDistance)
                {
                    color = drawingMinDistanceColor;
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
            var max = constraintPairArgs.maxConstraintDistance;
            var min = constraintPairArgs.minConstraintDistance;

            // The actual distance
            var distance = SlimeGame.GetDistance(player1.position, player2.position);
            

            if(distance > max)
            {
                // Push player2 towards player1
                var pushVec = (player1.position - player2.position);
                var pushDistance = distance - max;

                var rad = MathF.Atan2(pushVec.Y, pushVec.X);



                pushVec.Normalize();

                player2.position += pushVec * pushDistance * 0.5f;
                player1.position -= pushVec * pushDistance * 0.5f;


                // player2.rotation = -rad;

                player2.velocity = new Vector2(0.1f, player2.velocity.Y);

            } else if(distance < min)
            {
                // Push player2 away from player1
                var pushVec = (player2.position - player1.position);
                var pushDistance = min - distance;
                pushVec.Normalize();

                player2.position += pushVec * pushDistance * 0.5f;
                player1.position -= pushVec * pushDistance * 0.5f;


                player2.velocity = new Vector2(0.1f, player2.velocity.Y);
            }


        }

        public void AddConstraintPair(Player player1, Player player2, float max, float min)
        {
            var newArgs = new ConstraintPairArgs();
            newArgs.player1 = player1;
            newArgs.player2 = player2;
            newArgs.maxConstraintDistance = max;
            newArgs.minConstraintDistance = min;

            constraintPairList.Add(newArgs);
        }

    }
}
