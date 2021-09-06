using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace MonoGame_Slime
{
    public static class Program
    {
        public static Song BackgroundMusic;
        public static int currentLevel = 1;
        public static bool restart = true;


        [STAThread]
        static void Main()
        {

            // Dummy Game just for music
            var dummy = new SlimeGame();
            BackgroundMusic = dummy.Content.Load<Song>("BackgroundMusic");
            MediaPlayer.Play(BackgroundMusic);
            MediaPlayer.IsRepeating = true;


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
                game = new SlimeGame_1();
                game.Run();
            }
            while (Program.restart);

            game = null;
            dummy.Dispose();

        }


    }
}
