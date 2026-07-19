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
            panel1.Location = new Point(11, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1384, 292);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = Color.MidnightBlue;
            panel3.Controls.Add(label2);
            panel3.Controls.Add(panel6);
            panel3.Location = new Point(1206, 97);
            panel3.Name = "panel3";
            panel3.Size = new Size(355, 128);
            panel3.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(155, 12);
            label2.Name = "label2";
            label2.Size = new Size(206, 27);
            label2.TabIndex = 8;
            label2.Text = "Inactive Employees";
            // 
            // panel6
            // 
            panel6.BackColor = Color.GhostWhite;
            panel6.Location = new Point(13, 12);
            panel6.Name = "panel6";
            panel6.Size = new Size(101, 100);
            panel6.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.BackColor = SystemColors.ButtonHighlight;
            panel7.Controls.Add(lblTotalEmployees);
            panel7.Controls.Add(panel8);
            panel7.Location = new Point(29, 97);
            panel7.Name = "panel7";
            panel7.Size = new Size(355, 128);
            panel7.TabIndex = 4;
            // 
            // lblTotalEmployees
            // 
            lblTotalEmployees.AutoSize = true;
            lblTotalEmployees.BackColor = SystemColors.ButtonHighlight;
            lblTotalEmployees.Font = new Font("Microsoft YaHei", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalEmployees.ForeColor = Color.MidnightBlue;
            lblTotalEmployees.Location = new Point(155, 13);
            lblTotalEmployees.Name = "lblTotalEmployees";
            lblTotalEmployees.Size = new Size(178, 27);
            lblTotalEmployees.TabIndex = 6;
            lblTotalEmployees.Text = "Total Employees";
            // 
            // panel8
            // 
            panel8.BackColor = Color.GhostWhite;
            panel8.Controls.Add(pictureBox1);
            panel8.Location = new Point(13, 12);
            panel8.Name = "panel8";
            panel8.Size = new Size(101, 100);
            panel8.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(101, 100);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel4
            // 
            panel4.BackColor = Color.MidnightBlue;
            panel4.Controls.Add(lblActiveEmployees);
            panel4.Controls.Add(panel5);
            panel4.Location = new Point(609, 97);
            panel4.Name = "panel4";
            panel4.Size = new Size(355, 128);
            panel4.TabIndex = 4;
            // 
            // lblActiveEmployees
            // 
            lblActiveEmployees.AutoSize = true;
            lblActiveEmployees.Font = new Font("Microsoft YaHei", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblActiveEmployees.ForeColor = SystemColors.ButtonHighlight;
            lblActiveEmployees.Location = new Point(152, 13);
            lblActiveEmployees.Name = "lblActiveEmployees";
            lblActiveEmployees.Size = new Size(189, 27);
            lblActiveEmployees.TabIndex = 7;
            lblActiveEmployees.Text = "Active Employees";
            // 
            // panel5
            // 
            panel5.BackColor = Color.GhostWhite;
            panel5.Location = new Point(13, 12);
            panel5.Name = "panel5";
            panel5.Size = new Size(101, 100);
            panel5.TabIndex = 0;
            // 
            // lblOverview
            // 
            lblOverview.AutoSize = true;
            lblOverview.BackColor = Color.White;
            lblOverview.Font = new Font("Microsoft YaHei", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOverview.ForeColor = Color.MidnightBlue;
            lblOverview.Location = new Point(11, 11);
            lblOverview.Name = "lblOverview";
            lblOverview.Size = new Size(267, 31);
            lblOverview.TabIndex = 2;
            lblOverview.Text = "Dashboard Overview";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = SystemColors.ButtonHighlight;
            panel2.Location = new Point(11, 339);
            panel2.Name = "panel2";
            panel2.Size = new Size(1517, 427);
            panel2.TabIndex = 1;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1541, 779);
            Controls.Add(panel2);
            Controls.Add(panel1);
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