using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Media;

namespace MonoGame_Slime
{
    public static class Program
    {

        // Audio and Music
        public static Song BackgroundMusic;
        public static SoundEffect PickupSound;
        //public static SoundEffect SlimemoveSound;
        //public static SoundEffect SpikeSound;

        public static SoundEffect BackgroundMusic_SFX;


        // Level Loader
        public static int currentLevel = 1;
        public static bool restart = true;

        public static int maxLevel = 2;
        public static int minLevel = 1;


        [STAThread]
        static void Main()
        {

            // Dummy Game just for music
            var dummy = new SlimeGame(true);
            
            PickupSound = dummy.Content.Load<SoundEffect>("PickupSound");
            

            //if (BackgroundMusic == null) BackgroundMusic = dummy.Content.Load<Song>("BackgroundMusic");
            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(BackgroundMusic);
            

                // Music



            // The Real Game
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
            //dummy.Dispose();

        }


    }
}
