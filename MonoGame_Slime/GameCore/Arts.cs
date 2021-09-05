using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MonoGame_Slime.GameCore
{
    class Arts
    {

        // Face
        public static Texture2D Player_Normal { get; private set; }
        public static Texture2D Player_Blink { get; private set; }
        public static Texture2D Player_Happy { get; private set; }



        // Components
        public static Texture2D World { get; private set; }
        public static Texture2D Wall { get; private set; }

        public static Texture2D Spike_1 { get; private set; }
        public static Texture2D Spike_2 { get; private set; }
        public static Texture2D Spike_3 { get; private set; }

        public static Texture2D Pickup { get; private set; }



        // Body Parts
        public static Texture2D Player_1 { get; private set; }
        public static Texture2D Player_2 { get; private set; }
        public static Texture2D Player_3 { get; private set; }
        public static Texture2D Player_4 { get; private set; }
        public static Texture2D Player_5 { get; private set; }
        public static Texture2D Player_6 { get; private set; }

        public static Texture2D[] PlayerRandom = new Texture2D[6];

        // Game State
        public static Texture2D GameLose { get; private set; }
        public static Texture2D GameWin { get; private set; }


        // Music 

        public static Song BackgroundMusic { get; private set; }




        public static void Load(ContentManager content)
        {
            Player_Normal = content.Load<Texture2D>("Player_Normal");
            Player_Blink = content.Load<Texture2D>("Player_Blink");
            Player_Happy = content.Load<Texture2D>("Player_Happy");

            World = content.Load<Texture2D>("World");
            Wall = content.Load<Texture2D>("Wall");

            Spike_1 = content.Load<Texture2D>("Spike_1");
            Spike_2 = content.Load<Texture2D>("Spike_2");
            Spike_3 = content.Load<Texture2D>("Spike_3");

            Pickup = content.Load<Texture2D>("Pickup");


            for (int i = 0; i < 6; ++i)
            {
                var name = string.Format("Player_{0}", i + 1);
                PlayerRandom[i] = content.Load<Texture2D>(name);
            }

            Player_1 = content.Load<Texture2D>("Player_1");
            Player_2 = content.Load<Texture2D>("Player_2");
            Player_3 = content.Load<Texture2D>("Player_3");
            Player_4 = content.Load<Texture2D>("Player_4");
            Player_5 = content.Load<Texture2D>("Player_5");
            Player_6 = content.Load<Texture2D>("Player_6");


            GameLose = content.Load<Texture2D>("GameLose");
            GameWin = content.Load<Texture2D>("GameWin");

            BackgroundMusic = content.Load<Song>("BackgroundMusic");


        }

        public static Texture2D LoadPlayerArt()
        {
            var rand = new Random();
            var randomIndex = rand.Next(0, 4);
            return PlayerRandom[randomIndex];
        }



        

    }
}
