namespace EmployeManagementSoftware
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            panel1 = new Panel();
            panel3 = new Panel();
            label2 = new Label();
            panel6 = new Panel();
            panel7 = new Panel();
            lblTotalEmployees = new Label();
            panel8 = new Panel();
            pictureBox1 = new PictureBox();
            panel4 = new Panel();
            lblActiveEmployees = new Label();
            panel5 = new Panel();
            lblOverview = new Label();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel7);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(lblOverview);
            panel1.Location = new Point(10, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1327, 219);
            panel1.Size = new Size(1211, 219);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = Color.MidnightBlue;
            panel3.Controls.Add(label2);
            panel3.Controls.Add(panel6);
            panel3.Location = new Point(1055, 73);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(311, 96);
            panel3.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(136, 9);
            label2.Name = "label2";
            label2.Size = new Size(97, 22);
            label2.TabIndex = 8;
            label2.Text = "Employees";
            // 
            // panel6
            // 
            panel6.BackColor = Color.GhostWhite;
            panel6.Location = new Point(11, 9);
            panel6.Margin = new Padding(3, 2, 3, 2);
            panel6.Name = "panel6";
            panel6.Size = new Size(88, 75);
            panel6.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.BackColor = SystemColors.ButtonHighlight;
            panel7.Controls.Add(lblTotalEmployees);
            panel7.Controls.Add(panel8);
            panel7.Location = new Point(25, 73);
            panel7.Margin = new Padding(3, 2, 3, 2);
            panel7.Name = "panel7";
            panel7.Size = new Size(311, 96);
            panel7.TabIndex = 4;
            // 
            // lblTotalEmployees
            // 
            lblTotalEmployees.AutoSize = true;
            lblTotalEmployees.BackColor = SystemColors.ButtonHighlight;
            lblTotalEmployees.Font = new Font("Microsoft YaHei", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalEmployees.ForeColor = SystemColors.ButtonHighlight;
            lblTotalEmployees.Location = new Point(136, 10);
            lblTotalEmployees.ForeColor = Color.MidnightBlue;
            lblTotalEmployees.Location = new Point(136, 10);
            lblTotalEmployees.Name = "lblTotalEmployees";
            lblTotalEmployees.Size = new Size(144, 22);
            lblTotalEmployees.TabIndex = 6;
            lblTotalEmployees.Text = "Total Employees";
            // 
            // panel8
            // 
            panel8.BackColor = Color.GhostWhite;
            panel8.Controls.Add(pictureBox1);
            panel8.Location = new Point(11, 9);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(88, 75);
            panel8.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(88, 75);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel4
            // 
            panel4.BackColor = Color.MidnightBlue;
            panel4.Controls.Add(lblActiveEmployees);
            panel4.Controls.Add(panel5);
            panel4.Location = new Point(533, 73);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(311, 96);
            panel4.TabIndex = 4;
            // 
            // lblActiveEmployees
            // 
            lblActiveEmployees.AutoSize = true;
            lblActiveEmployees.Font = new Font("Microsoft YaHei", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblActiveEmployees.ForeColor = SystemColors.ButtonHighlight;
            lblActiveEmployees.Location = new Point(133, 10);
            lblActiveEmployees.Name = "lblActiveEmployees";
            lblActiveEmployees.Size = new Size(152, 22);
            lblActiveEmployees.TabIndex = 7;
            lblActiveEmployees.Text = "Active Employees";
            // 
            // panel5
            // 
            panel5.BackColor = Color.GhostWhite;
            panel5.Location = new Point(11, 9);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(88, 75);
            panel5.TabIndex = 0;
            // 
            // lblOverview
            // 
            lblOverview.AutoSize = true;
            lblOverview.BackColor = Color.White;
            lblOverview.Font = new Font("Microsoft YaHei", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOverview.ForeColor = Color.MidnightBlue;
            lblOverview.Location = new Point(10, 8);
            lblOverview.Name = "lblOverview";
            lblOverview.Size = new Size(212, 26);
            lblOverview.TabIndex = 2;
            lblOverview.Text = "Dashboard Overview";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = SystemColors.ButtonHighlight;
            panel2.Location = new Point(10, 254);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1211, 307);
            panel2.Size = new Size(1327, 320);
            panel2.TabIndex = 1;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1232, 571);
            ClientSize = new Size(1348, 584);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard";
            Load += Dashboard_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label lblOverview;
        private Panel panel4;
        private Panel panel5;
        private Panel panel3;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private PictureBox pictureBox1;
        private Label lblTotalEmployees;
        private Label label2;
        private Label lblActiveEmployees;
    }
}