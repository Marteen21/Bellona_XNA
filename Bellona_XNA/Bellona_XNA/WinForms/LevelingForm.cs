using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bellona_XNA.MemoryReading;
using Microsoft.Xna.Framework;


namespace Bellona_XNA.WinForms
{
    public partial class LevelingForm : Form
    {
        Task travelTask;
        private CancellationTokenSource Canceller { get; set; }

        public IntPtr getDrawSurface() {
            return pctSurface.Handle;
        }

        public LevelingForm() {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {
            this.dataGridView1.Rows.Add(Game1.mainPlayer.Position.X, Game1.mainPlayer.Position.Y, Game1.mainPlayer.Position.Z);
        }

        private void button1_Click(object sender, EventArgs e) {
            try {
                Program.myGame.ConnectToMainWoW("World of Warcraft");
            }
            catch (NullReferenceException) {

            }
        }

        private void button3_Click(object sender, EventArgs e) {
            var selectedRowCells = dataGridView1.SelectedRows[0].Cells;
            Vector3 vector3 = new Vector3((float)selectedRowCells[0].Value, (float)selectedRowCells[1].Value, (float)selectedRowCells[2].Value);
            Program.myGame.MoveTo(vector3);
        }

        private void TraversePath() {
            for (int i = 0; i < dataGridView1.Rows.Count; ++i) {
                try {
                    var cells = dataGridView1.Rows[i].Cells;
                    Vector3 vector3 = new Vector3((float)cells[0].Value, (float)cells[1].Value, (float)cells[2].Value);
                    Program.myGame.MoveTo(vector3);
                }
                catch {

                }
            }
        }


        private void Grind () {
            while (true) {
                for (int i = 0; i < dataGridView1.Rows.Count; ++i) {
                    try {
                        var cells = dataGridView1.Rows[i].Cells;
                        Vector3 vector3 = new Vector3((float)cells[0].Value, (float)cells[1].Value, (float)cells[2].Value);
                        Program.myGame.MoveTo(vector3);
                        Program.myGame.RefreshShit();
                        Program.myGame.KillNearbyTargets();
                    }
                    catch {

                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            Canceller = new CancellationTokenSource();
            travelTask = Task.Factory.StartNew(() =>
            {
                try {
                    using (Canceller.Token.Register(Thread.CurrentThread.Abort)) {
                        TraversePath();
                    }
                }
                catch (ThreadAbortException) {
                    
                }
                // Whatever code you want in your thread
            });
        }

        private void button5_Click(object sender, EventArgs e) {
            Canceller.Cancel();
        }

        private void button6_Click(object sender, EventArgs e) {
            Program.myGame.KillTarget();
        }

        private void button5_Click_1(object sender, EventArgs e) {
            Program.myGame.KillNearbyTargets();
        }

        private void button7_Click(object sender, EventArgs e) {
            Grind();
        }
    }
}
