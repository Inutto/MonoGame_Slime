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

            var game = new SlimeGame();

            // Restart The Game PHISICALLY!
            do
            {
                game.Exit();
                Program.restart = false;
                game = new SlimeGame();
                game.Run();
            }
            while (Program.restart);

        }
    }
}
