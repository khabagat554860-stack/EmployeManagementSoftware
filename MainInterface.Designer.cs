namespace EmployeManagementSoftware
{
    partial class MainInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainInterface));
            panelMainInterface = new Panel();
            lblSignOut = new Label();
            pictureBox5 = new PictureBox();
            lblSalary = new Label();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            lblEmployees = new Label();
            lblDashboard = new Label();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            lblStaffroom = new Label();
            panelMainInterface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelMainInterface
            // 
            panelMainInterface.BackColor = Color.White;
            panelMainInterface.Controls.Add(lblSignOut);
            panelMainInterface.Controls.Add(pictureBox5);
            panelMainInterface.Controls.Add(lblSalary);
            panelMainInterface.Controls.Add(pictureBox4);
            panelMainInterface.Controls.Add(pictureBox3);
            panelMainInterface.Controls.Add(lblEmployees);
            panelMainInterface.Controls.Add(lblDashboard);
            panelMainInterface.Controls.Add(pictureBox2);
            panelMainInterface.Controls.Add(pictureBox1);
            panelMainInterface.Controls.Add(label2);
            panelMainInterface.Controls.Add(lblStaffroom);
            panelMainInterface.Dock = DockStyle.Left;
            panelMainInterface.Location = new Point(0, 0);
            panelMainInterface.Margin = new Padding(3, 4, 3, 4);
            panelMainInterface.Name = "panelMainInterface";
            panelMainInterface.Size = new Size(239, 600);
            panelMainInterface.TabIndex = 2;
            panelMainInterface.Paint += panel1_Paint;
            // 
            // lblSignOut
            // 
            lblSignOut.AutoSize = true;
            lblSignOut.BackColor = Color.White;
            lblSignOut.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSignOut.ForeColor = Color.MidnightBlue;
            lblSignOut.Location = new Point(61, 488);
            lblSignOut.Name = "lblSignOut";
            lblSignOut.Size = new Size(84, 24);
            lblSignOut.TabIndex = 11;
            lblSignOut.Text = "Sign Out";
            // 
            // pictureBox5
            // 
            pictureBox5.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox5.ErrorImage = null;
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(31, 488);
            pictureBox5.Margin = new Padding(3, 4, 3, 4);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(24, 24);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 10;
            pictureBox5.TabStop = false;
            // 
            // lblSalary
            // 
            lblSalary.AutoSize = true;
            lblSalary.BackColor = Color.White;
            lblSalary.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSalary.ForeColor = Color.MidnightBlue;
            lblSalary.Location = new Point(61, 276);
            lblSalary.Name = "lblSalary";
            lblSalary.Size = new Size(62, 24);
            lblSalary.TabIndex = 9;
            lblSalary.Text = "Salary";
            // 
            // pictureBox4
            // 
            pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox4.ErrorImage = null;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(31, 276);
            pictureBox4.Margin = new Padding(3, 4, 3, 4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(24, 24);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 8;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.Location = new Point(14, 34);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(53, 53);
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // lblEmployees
            // 
            lblEmployees.AutoSize = true;
            lblEmployees.BackColor = Color.White;
            lblEmployees.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmployees.ForeColor = Color.MidnightBlue;
            lblEmployees.Location = new Point(58, 229);
            lblEmployees.Name = "lblEmployees";
            lblEmployees.Size = new Size(102, 24);
            lblEmployees.TabIndex = 6;
            lblEmployees.Text = "Employees";
            lblEmployees.Click += lblEmployees_Click;
            // 
            // lblDashboard
            // 
            lblDashboard.AutoSize = true;
            lblDashboard.BackColor = Color.White;
            lblDashboard.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDashboard.ForeColor = Color.MidnightBlue;
            lblDashboard.Location = new Point(58, 176);
            lblDashboard.Name = "lblDashboard";
            lblDashboard.Size = new Size(102, 24);
            lblDashboard.TabIndex = 5;
            lblDashboard.Text = "Dashboard";
            lblDashboard.Click += lblDashboard_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.ErrorImage = null;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(31, 229);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 24);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(31, 176);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 24);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.MidnightBlue;
            label2.Location = new Point(14, 135);
            label2.Name = "label2";
            label2.Size = new Size(53, 19);
            label2.TabIndex = 1;
            label2.Text = "Menu";
            // 
            // lblStaffroom
            // 
            lblStaffroom.AutoSize = true;
            lblStaffroom.BackColor = Color.White;
            lblStaffroom.Font = new Font("Microsoft YaHei", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStaffroom.ForeColor = Color.MidnightBlue;
            lblStaffroom.Location = new Point(73, 44);
            lblStaffroom.Name = "lblStaffroom";
            lblStaffroom.Size = new Size(138, 31);
            lblStaffroom.TabIndex = 0;
            lblStaffroom.Text = "Staffroom";
            lblStaffroom.Click += lblStaffroom_Click;
            // 
            // MainInterface
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(panelMainInterface);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainInterface";
            Text = "MainInterface";
            Load += MainInterface_Load;
            panelMainInterface.ResumeLayout(false);
            panelMainInterface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMainInterface;
        private Label label2;
        private Label lblStaffroom;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label lblEmployees;
        private Label lblDashboard;
        private PictureBox pictureBox3;
        private Label lblSalary;
        private PictureBox pictureBox4;
        private Label lblSignOut;
        private PictureBox pictureBox5;
    }
}