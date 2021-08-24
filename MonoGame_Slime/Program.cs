using System;

namespace MonoGame_Slime
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SlimeGame())
                game.Run();
        }
    }
}
