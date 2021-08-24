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


        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("Player");
            // World = content.Load<Texture2D>("Resources/World");
            // Wall = content.Load<Texture2D>("Resources/wall");
        }

    }
}
