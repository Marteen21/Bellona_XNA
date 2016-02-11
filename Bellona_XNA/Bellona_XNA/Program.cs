using Bellona_XNA.WinForms;
using System;

namespace Bellona_XNA {
#if WINDOWS || XBOX
    static class Program
    {
        public static Game1 myGame;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            MainForm form = new MainForm();
            form.Show();

            myGame = new Game1(form.getDrawSurface());
            myGame.Run();
            
        }
    }
#endif
}

