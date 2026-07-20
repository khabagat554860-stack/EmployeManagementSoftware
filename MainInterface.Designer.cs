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
            sigOutContainerPanel = new Panel();
            lblSignOut = new Label();
            pictureBox5 = new PictureBox();
            menuContainerPanel = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            lblSalary = new Label();
            pictureBox2 = new PictureBox();
            pictureBox4 = new PictureBox();
            lblDashboard = new Label();
            lblEmployees = new Label();
            pictureBox3 = new PictureBox();
            lblStaffroom = new Label();
            mainPanel = new Panel();
            panelMainInterface.SuspendLayout();
            sigOutContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            menuContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // panelMainInterface
            // 
            panelMainInterface.BackColor = Color.White;
            panelMainInterface.BackgroundImage = (Image)resources.GetObject("panelMainInterface.BackgroundImage");
            panelMainInterface.BorderStyle = BorderStyle.FixedSingle;
            panelMainInterface.Controls.Add(sigOutContainerPanel);
            panelMainInterface.Controls.Add(menuContainerPanel);
            panelMainInterface.Controls.Add(pictureBox3);
            panelMainInterface.Controls.Add(lblStaffroom);
            panelMainInterface.Dock = DockStyle.Left;
            panelMainInterface.Location = new Point(0, 0);
            panelMainInterface.Name = "panelMainInterface";
            panelMainInterface.Size = new Size(209, 610);
            panelMainInterface.TabIndex = 2;
            panelMainInterface.Paint += panel1_Paint;
            // 
            // sigOutContainerPanel
            // 
            sigOutContainerPanel.BackColor = Color.Transparent;
            sigOutContainerPanel.BorderStyle = BorderStyle.FixedSingle;
            sigOutContainerPanel.Controls.Add(lblSignOut);
            sigOutContainerPanel.Controls.Add(pictureBox5);
            sigOutContainerPanel.Dock = DockStyle.Bottom;
            sigOutContainerPanel.Location = new Point(0, 555);
            sigOutContainerPanel.Name = "sigOutContainerPanel";
            sigOutContainerPanel.Size = new Size(207, 53);
            sigOutContainerPanel.TabIndex = 1;
            // 
            // lblSignOut
            // 
            lblSignOut.AutoSize = true;
            lblSignOut.BackColor = Color.Transparent;
            lblSignOut.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSignOut.ForeColor = Color.MidnightBlue;
            lblSignOut.Location = new Point(58, 20);
            lblSignOut.Name = "lblSignOut";
            lblSignOut.Size = new Size(69, 19);
            lblSignOut.TabIndex = 11;
            lblSignOut.Text = "Sign Out";
            lblSignOut.Click += lblSignOut_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox5.ErrorImage = null;
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(32, 20);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(21, 18);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 10;
            pictureBox5.TabStop = false;
            // 
            // menuContainerPanel
            // 
            menuContainerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            menuContainerPanel.BackColor = Color.Transparent;
            menuContainerPanel.BorderStyle = BorderStyle.FixedSingle;
            menuContainerPanel.Controls.Add(label2);
            menuContainerPanel.Controls.Add(pictureBox1);
            menuContainerPanel.Controls.Add(lblSalary);
            menuContainerPanel.Controls.Add(pictureBox2);
            menuContainerPanel.Controls.Add(pictureBox4);
            menuContainerPanel.Controls.Add(lblDashboard);
            menuContainerPanel.Controls.Add(lblEmployees);
            menuContainerPanel.Location = new Point(12, 129);
            menuContainerPanel.Name = "menuContainerPanel";
            menuContainerPanel.Size = new Size(179, 309);
            menuContainerPanel.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.MidnightBlue;
            label2.Location = new Point(13, 15);
            label2.Name = "label2";
            label2.Size = new Size(43, 17);
            label2.TabIndex = 1;
            label2.Text = "Menu";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(13, 45);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(21, 21);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // lblSalary
            // 
            lblSalary.AutoSize = true;
            lblSalary.BackColor = Color.Transparent;
            lblSalary.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSalary.ForeColor = Color.MidnightBlue;
            lblSalary.Location = new Point(39, 120);
            lblSalary.Name = "lblSalary";
            lblSalary.Size = new Size(51, 19);
            lblSalary.TabIndex = 9;
            lblSalary.Text = "Salary";
            lblSalary.Click += lblSalary_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.ErrorImage = null;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(13, 85);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(21, 21);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox4.BorderStyle = BorderStyle.FixedSingle;
            pictureBox4.ErrorImage = null;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(13, 120);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(21, 21);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 8;
            pictureBox4.TabStop = false;
            // 
            // lblDashboard
            // 
            lblDashboard.AutoSize = true;
            lblDashboard.BackColor = Color.Transparent;
            lblDashboard.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDashboard.ForeColor = Color.MidnightBlue;
            lblDashboard.Location = new Point(37, 45);
            lblDashboard.Name = "lblDashboard";
            lblDashboard.Size = new Size(85, 19);
            lblDashboard.TabIndex = 5;
            lblDashboard.Text = "Dashboard";
            lblDashboard.Click += lblDashboard_Click;
            // 
            // lblEmployees
            // 
            lblEmployees.AutoSize = true;
            lblEmployees.BackColor = Color.Transparent;
            lblEmployees.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmployees.ForeColor = Color.MidnightBlue;
            lblEmployees.Location = new Point(37, 85);
            lblEmployees.Name = "lblEmployees";
            lblEmployees.Size = new Size(84, 19);
            lblEmployees.TabIndex = 6;
            lblEmployees.Text = "Employees";
            lblEmployees.Click += lblEmployees_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
            pictureBox3.Location = new Point(12, 26);
            pictureBox3.Margin = new Padding(3, 2, 3, 2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(46, 46);
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // lblStaffroom
            // 
            lblStaffroom.AutoSize = true;
            lblStaffroom.BackColor = Color.Transparent;
            lblStaffroom.Font = new Font("Microsoft YaHei", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStaffroom.ForeColor = Color.MidnightBlue;
            lblStaffroom.Location = new Point(64, 33);
            lblStaffroom.Name = "lblStaffroom";
            lblStaffroom.Size = new Size(136, 26);
            lblStaffroom.TabIndex = 0;
            lblStaffroom.Text = "STAFFROOM";
            lblStaffroom.Click += lblStaffroom_Click;
            // 
            // mainPanel
            // 
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(209, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1248, 610);
            mainPanel.TabIndex = 3;
            // 
            // MainInterface
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1457, 610);
            Controls.Add(mainPanel);
            Controls.Add(panelMainInterface);
            Name = "MainInterface";
            Text = "MainInterface";
            Load += MainInterface_Load;
            panelMainInterface.ResumeLayout(false);
            panelMainInterface.PerformLayout();
            sigOutContainerPanel.ResumeLayout(false);
            sigOutContainerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            menuContainerPanel.ResumeLayout(false);
            menuContainerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
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
        private Panel mainPanel;
        private Panel menuContainerPanel;
        private Panel sigOutContainerPanel;
    }
}