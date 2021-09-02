using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MonoGame_Slime.GameCore
{
    class Arts
    {
        public static Texture2D Player { get; private set; }
        public static Texture2D World { get; private set; }
        public static Texture2D Wall { get; private set; }

        public static Texture2D Player_1 { get; private set; }
        public static Texture2D Player_2 { get; private set; }
        public static Texture2D Player_3 { get; private set; }
        public static Texture2D Player_4 { get; private set; }
        public static Texture2D Player_5 { get; private set; }

        public static Texture2D[] PlayerRandom = new Texture2D[5];

        public static Texture2D GameLose { get; private set; }
        public static Texture2D GameWin { get; private set; }



        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("Player");
            World = content.Load<Texture2D>("World");
            Wall = content.Load<Texture2D>("Wall");


            for(int i = 0; i < 5; ++i)
            {
                var name = string.Format("Player_{0}", i + 1);
                PlayerRandom[i] = content.Load<Texture2D>(name);
            }

            Player_1 = content.Load<Texture2D>("Player_1");
            Player_2 = content.Load<Texture2D>("Player_2");
            Player_3 = content.Load<Texture2D>("Player_3");
            Player_4 = content.Load<Texture2D>("Player_4");
            Player_5 = content.Load<Texture2D>("Player_5");


            GameLose = content.Load<Texture2D>("GameLose");
            GameWin = content.Load<Texture2D>("GameWin");


        }

        public static Texture2D LoadPlayerArt()
        {
            var rand = new Random();
            var randomIndex = rand.Next(0, 4);
            return PlayerRandom[randomIndex];
        }



        

    }
}
