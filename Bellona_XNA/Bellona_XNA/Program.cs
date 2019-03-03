using Bellona_XNA.WinForms;
using System;
using System.Windows.Forms;

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
            IntPtr pctSurface;
            DialogResult dialogResult = MessageBox.Show("Raider or Grinder", "Grinder?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
                MainForm form = new MainForm();
                form.Show();
                pctSurface = form.getDrawSurface();
            } else if (dialogResult == DialogResult.No) {
                LevelingForm form = new LevelingForm();
                form.Show();
                pctSurface = form.getDrawSurface();
            } else {
                return;
            }


            myGame = new Game1(pctSurface);
            myGame.Run();
            
        }
    }
#endif
}

