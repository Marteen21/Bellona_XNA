using Bellona_XNA.MemoryReading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bellona_XNA.WinForms {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }
        public IntPtr getDrawSurface() {
            return pctSurface.Handle;
        }

        private void MainForm_Load(object sender, EventArgs e) {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0) {
                try {
                    WoWConnection tempWoW = new WoWConnection(senderGrid["col_WinTitle", e.RowIndex].Value.ToString());
                    tempWoW.TryToConnect();
                    WoWPlayer.SetWindowTitleForPlayer(ref Game1.allPlayers, tempWoW.GetPlayerGUID(), (senderGrid["col_WinTitle", e.RowIndex].Value.ToString()));
                }
                catch (NullReferenceException) {

                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) {

        }

        private void btn_mainConnect_Click(object sender, EventArgs e) {
            try {
                Program.myGame.ConnectToMainWoW(cmbx_mainConnect.SelectedItem.ToString());
            }
            catch (NullReferenceException) {

            }
        }
    }
}
