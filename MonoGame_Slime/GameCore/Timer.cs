using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGame_Slime.GameCore
{
    public class Timer
    {
        public double delay = 0;
        public double currentTime = 0;
        public double endTime = 10000000;


        
        public void StartTimer(GameTime gameTime, double delay)
        {
            this.delay = delay;
            currentTime = gameTime.TotalGameTime.TotalMilliseconds;
            endTime = currentTime + delay;

        }

        public void Update(GameTime gameTime, Action action = null)
        {
            if(endTime > currentTime)
            {
                // counting timer
                currentTime = gameTime.TotalGameTime.TotalMilliseconds;
            } else
            {
                endTime = 0;
                action?.Invoke();
            }
        }

        public void Update(GameTime gameTime, Action<GameTime> action = null)
        {
            if (endTime > currentTime)
            {
                // counting timer
                currentTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
            else
            {
                endTime = 0;
                action(gameTime);
            }
        }


    }
}
