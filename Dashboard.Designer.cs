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
            panel2 = new Panel();
            lblOverview = new Label();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.Controls.Add(panel7);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(lblOverview);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 151);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonHighlight;
            panel2.Location = new Point(12, 195);
            panel2.Name = "panel2";
            panel2.Size = new Size(776, 243);
            panel2.TabIndex = 1;
            // 
            // lblOverview
            // 
            lblOverview.AutoSize = true;
            lblOverview.BackColor = Color.White;
            lblOverview.Font = new Font("Microsoft YaHei", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOverview.ForeColor = Color.MidnightBlue;
            lblOverview.Location = new Point(12, 10);
            lblOverview.Name = "lblOverview";
            lblOverview.Size = new Size(267, 31);
            lblOverview.TabIndex = 2;
            lblOverview.Text = "Dashboard Overview";
            // 
            // panel3
            // 
            panel3.BackColor = Color.MidnightBlue;
            panel3.Location = new Point(12, 44);
            panel3.Name = "panel3";
            panel3.Size = new Size(240, 90);
            panel3.TabIndex = 3;
            // 
            // panel4
            // 
            panel4.BackColor = Color.MidnightBlue;
            panel4.Controls.Add(panel5);
            panel4.Location = new Point(522, 44);
            panel4.Name = "panel4";
            panel4.Size = new Size(240, 90);
            panel4.TabIndex = 4;
            // 
            // panel5
            // 
            panel5.BackColor = Color.GhostWhite;
            panel5.Location = new Point(13, 12);
            panel5.Name = "panel5";
            panel5.Size = new Size(66, 66);
            panel5.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.BackColor = Color.MidnightBlue;
            panel7.Controls.Add(panel8);
            panel7.Location = new Point(267, 44);
            panel7.Name = "panel7";
            panel7.Size = new Size(240, 90);
            panel7.TabIndex = 4;
            // 
            // panel8
            // 
            panel8.BackColor = Color.GhostWhite;
            panel8.Controls.Add(pictureBox1);
            panel8.Location = new Point(13, 12);
            panel8.Name = "panel8";
            panel8.Size = new Size(66, 66);
            panel8.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(66, 66);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Dashboard";
            Text = "Dashboard";
            Load += Dashboard_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel4.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label lblOverview;
        private Panel panel3;
        private Panel panel7;
        private Panel panel8;
        private Panel panel4;
        private Panel panel5;
        private PictureBox pictureBox1;
    }
}