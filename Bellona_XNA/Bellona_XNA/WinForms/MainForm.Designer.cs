namespace Bellona_XNA.WinForms {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pctSurface = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.col_WinTitle = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.col_ConnectBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.col_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbx_mainConnect = new System.Windows.Forms.ComboBox();
            this.btn_mainConnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctSurface)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 606F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.pctSurface, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 606F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1008, 729);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pctSurface
            // 
            this.pctSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctSurface.Location = new System.Drawing.Point(3, 3);
            this.pctSurface.Name = "pctSurface";
            this.pctSurface.Size = new System.Drawing.Size(600, 600);
            this.pctSurface.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pctSurface.TabIndex = 1;
            this.pctSurface.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_WinTitle,
            this.col_ConnectBtn,
            this.col_Status});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(609, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(396, 600);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // col_WinTitle
            // 
            this.col_WinTitle.HeaderText = "Window Title";
            this.col_WinTitle.Items.AddRange(new object[] {
            "World of Warcraft",
            "WoW1",
            "WoW2",
            "WoW3",
            "WoW4",
            "WoW5",
            "WoW6",
            "WoW7"});
            this.col_WinTitle.Name = "col_WinTitle";
            this.col_WinTitle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_WinTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // col_ConnectBtn
            // 
            this.col_ConnectBtn.HeaderText = "Add Bot";
            this.col_ConnectBtn.Name = "col_ConnectBtn";
            this.col_ConnectBtn.Text = "Add Bot";
            this.col_ConnectBtn.UseColumnTextForButtonValue = true;
            // 
            // col_Status
            // 
            this.col_Status.HeaderText = "Status";
            this.col_Status.Name = "col_Status";
            this.col_Status.ReadOnly = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmbx_mainConnect);
            this.flowLayoutPanel1.Controls.Add(this.btn_mainConnect);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(609, 609);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(396, 117);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // cmbx_mainConnect
            // 
            this.cmbx_mainConnect.FormattingEnabled = true;
            this.cmbx_mainConnect.Items.AddRange(new object[] {
            "World of Warcraft",
            "WoW1",
            "WoW2",
            "WoW3",
            "WoW4",
            "WoW5",
            "WoW6",
            "WoW7"});
            this.cmbx_mainConnect.Location = new System.Drawing.Point(3, 3);
            this.cmbx_mainConnect.Name = "cmbx_mainConnect";
            this.cmbx_mainConnect.Size = new System.Drawing.Size(121, 21);
            this.cmbx_mainConnect.TabIndex = 0;
            // 
            // btn_mainConnect
            // 
            this.btn_mainConnect.Location = new System.Drawing.Point(130, 3);
            this.btn_mainConnect.Name = "btn_mainConnect";
            this.btn_mainConnect.Size = new System.Drawing.Size(114, 23);
            this.btn_mainConnect.TabIndex = 1;
            this.btn_mainConnect.Text = "Main Connection";
            this.btn_mainConnect.UseVisualStyleBackColor = true;
            this.btn_mainConnect.Click += new System.EventHandler(this.btn_mainConnect_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(700, 700);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctSurface)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pctSurface;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbx_mainConnect;
        private System.Windows.Forms.Button btn_mainConnect;
        private System.Windows.Forms.DataGridViewComboBoxColumn col_WinTitle;
        private System.Windows.Forms.DataGridViewButtonColumn col_ConnectBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Status;
    }
}