using System;
using Microsoft.Xna.Framework;

namespace MonoGame_Slime
{
    public static class Program
    {


        public static bool restart = true;

        [STAThread]
        static void Main()
        {
            SlimeGame game = null;
            
            // Restart The Game PHISICALLY!
            do
            {
                if (game != null)
                {
                    game.Exit();
                    game.Dispose();
                }
                    
                Program.restart = false;
                game = new SlimeGame();
                game.Run();
            }
            while (Program.restart);

            game = null;

        }
    }
}
