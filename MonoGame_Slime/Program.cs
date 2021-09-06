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

        public static int maxLevel = 2;
        public static int minLevel = 1;


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


                switch (currentLevel)
                {
                    case 1:
                        game = new SlimeGame_1();
                        break;
                    case 2:
                        game = new SlimeGame_2();
                        break;
                        
                }

                game.Run();
            }
            while (Program.restart);

            game = null;
            dummy.Dispose();

        }


    }
}
