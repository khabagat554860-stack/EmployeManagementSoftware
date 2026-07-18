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
            panelMainInterface.Controls.Add(sigOutContainerPanel);
            panelMainInterface.Controls.Add(menuContainerPanel);
            panelMainInterface.Controls.Add(pictureBox3);
            panelMainInterface.Controls.Add(lblStaffroom);
            panelMainInterface.Dock = DockStyle.Left;
            panelMainInterface.Location = new Point(0, 0);
            panelMainInterface.Margin = new Padding(3, 4, 3, 4);
            panelMainInterface.Name = "panelMainInterface";
            panelMainInterface.Size = new Size(239, 813);
            panelMainInterface.TabIndex = 2;
            panelMainInterface.Paint += panel1_Paint;
            // 
            // sigOutContainerPanel
            // 
            sigOutContainerPanel.BackColor = Color.Transparent;
            sigOutContainerPanel.Controls.Add(lblSignOut);
            sigOutContainerPanel.Controls.Add(pictureBox5);
            sigOutContainerPanel.Dock = DockStyle.Bottom;
            sigOutContainerPanel.Location = new Point(0, 742);
            sigOutContainerPanel.Margin = new Padding(3, 4, 3, 4);
            sigOutContainerPanel.Name = "sigOutContainerPanel";
            sigOutContainerPanel.Size = new Size(239, 71);
            sigOutContainerPanel.TabIndex = 1;
            // 
            // lblSignOut
            // 
            lblSignOut.AutoSize = true;
            lblSignOut.BackColor = Color.Transparent;
            lblSignOut.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSignOut.ForeColor = Color.MidnightBlue;
            lblSignOut.Location = new Point(66, 27);
            lblSignOut.Name = "lblSignOut";
            lblSignOut.Size = new Size(84, 24);
            lblSignOut.TabIndex = 11;
            lblSignOut.Text = "Sign Out";
            lblSignOut.Click += lblSignOut_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox5.ErrorImage = null;
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(37, 27);
            pictureBox5.Margin = new Padding(3, 4, 3, 4);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(24, 24);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 10;
            pictureBox5.TabStop = false;
            // 
            // menuContainerPanel
            // 
            menuContainerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            menuContainerPanel.BackColor = Color.Transparent;
            menuContainerPanel.Controls.Add(label2);
            menuContainerPanel.Controls.Add(pictureBox1);
            menuContainerPanel.Controls.Add(lblSalary);
            menuContainerPanel.Controls.Add(pictureBox2);
            menuContainerPanel.Controls.Add(pictureBox4);
            menuContainerPanel.Controls.Add(lblDashboard);
            menuContainerPanel.Controls.Add(lblEmployees);
            menuContainerPanel.Location = new Point(14, 172);
            menuContainerPanel.Margin = new Padding(3, 4, 3, 4);
            menuContainerPanel.Name = "menuContainerPanel";
            menuContainerPanel.Size = new Size(205, 415);
            menuContainerPanel.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.MidnightBlue;
            label2.Location = new Point(15, 20);
            label2.Name = "label2";
            label2.Size = new Size(53, 19);
            label2.TabIndex = 1;
            label2.Text = "Menu";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(15, 60);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 28);
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
            lblSalary.Location = new Point(45, 160);
            lblSalary.Name = "lblSalary";
            lblSalary.Size = new Size(62, 24);
            lblSalary.TabIndex = 9;
            lblSalary.Text = "Salary";
     
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.ErrorImage = null;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(15, 113);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 28);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox4.ErrorImage = null;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(15, 160);
            pictureBox4.Margin = new Padding(3, 4, 3, 4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(24, 28);
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
            lblDashboard.Location = new Point(42, 60);
            lblDashboard.Name = "lblDashboard";
            lblDashboard.Size = new Size(102, 24);
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
            lblEmployees.Location = new Point(42, 113);
            lblEmployees.Name = "lblEmployees";
            lblEmployees.Size = new Size(102, 24);
            lblEmployees.TabIndex = 6;
            lblEmployees.Text = "Employees";
            lblEmployees.Click += lblEmployees_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.Location = new Point(14, 35);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(53, 61);
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // lblStaffroom
            // 
            lblStaffroom.AutoSize = true;
            lblStaffroom.BackColor = Color.Transparent;
            lblStaffroom.Font = new Font("Microsoft YaHei", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStaffroom.ForeColor = Color.MidnightBlue;
            lblStaffroom.Location = new Point(73, 44);
            lblStaffroom.Name = "lblStaffroom";
            lblStaffroom.Size = new Size(169, 31);
            lblStaffroom.TabIndex = 0;
            lblStaffroom.Text = "STAFFROOM";
            lblStaffroom.Click += lblStaffroom_Click;
            // 
            // mainPanel
            // 
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(239, 0);
            mainPanel.Margin = new Padding(3, 4, 3, 4);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1426, 813);
            mainPanel.TabIndex = 3;
            // 
            // MainInterface
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1665, 813);
            Controls.Add(mainPanel);
            Controls.Add(panelMainInterface);
            Margin = new Padding(3, 4, 3, 4);
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