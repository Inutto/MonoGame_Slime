using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Slime.GameCore
{
    class Player : Object
    {

        public Player()
        {
            image = Arts.Player;
            position = new Microsoft.Xna.Framework.Vector2(SlimeGame.screenWidth / 2 + 100f, SlimeGame.screenHeight / 2);

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }
    }
}
