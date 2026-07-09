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
            panel1 = new Panel();
            label4 = new Label();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label4);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, -7);
            panel1.Name = "panel1";
            panel1.Size = new Size(183, 483);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.MidnightBlue;
            label4.Location = new Point(51, 173);
            label4.Name = "label4";
            label4.Size = new Size(74, 17);
            label4.TabIndex = 5;
            label4.Text = "Employees";
            label4.Click += label4_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.ErrorImage = null;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(27, 172);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(18, 18);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(27, 132);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(18, 18);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.MidnightBlue;
            label3.Location = new Point(51, 133);
            label3.Name = "label3";
            label3.Size = new Size(75, 17);
            label3.TabIndex = 2;
            label3.Text = "Dashboard";
            label3.Click += label3_Click;
            // 
            panelMainInterface.BackColor = Color.White;
            panelMainInterface.Controls.Add(sigOutContainerPanel);
            panelMainInterface.Controls.Add(menuContainerPanel);
            panelMainInterface.Controls.Add(pictureBox3);
            panelMainInterface.Controls.Add(lblStaffroom);
            panelMainInterface.Dock = DockStyle.Left;
            panelMainInterface.Location = new Point(0, 0);
            panelMainInterface.Name = "panelMainInterface";
            panelMainInterface.Size = new Size(209, 450);
            panelMainInterface.TabIndex = 2;
            panelMainInterface.Paint += panel1_Paint;
            // 
            // lblSignOut
            // 
            lblSignOut.AutoSize = true;
            lblSignOut.BackColor = Color.White;
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
            // dataGridView1
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.MidnightBlue;
            label2.Location = new Point(13, 15);
            label2.Name = "label2";
            label2.Size = new Size(43, 17);
            label2.TabIndex = 1;
            label2.Text = "Menu";
            // 
            // lblStaffroom
            // 
            lblStaffroom.AutoSize = true;
            lblStaffroom.BackColor = Color.White;
            lblStaffroom.Font = new Font("Microsoft YaHei", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStaffroom.ForeColor = Color.MidnightBlue;
            lblStaffroom.Location = new Point(64, 33);
            lblStaffroom.Name = "lblStaffroom";
            lblStaffroom.Size = new Size(136, 26);
            lblStaffroom.TabIndex = 0;
            lblStaffroom.Text = "STAFFROOM";
            lblStaffroom.Click += lblStaffroom_Click;
            // 
            // menuContainerPanel
            // 
            menuContainerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            menuContainerPanel.Controls.Add(label2);
            menuContainerPanel.Controls.Add(pictureBox1);
            menuContainerPanel.Controls.Add(lblSalary);
            menuContainerPanel.Controls.Add(pictureBox2);
            menuContainerPanel.Controls.Add(pictureBox4);
            menuContainerPanel.Controls.Add(lblDashboard);
            menuContainerPanel.Controls.Add(lblEmployees);
            menuContainerPanel.Location = new Point(12, 129);
            menuContainerPanel.Name = "menuContainerPanel";
            menuContainerPanel.Size = new Size(179, 151);
            menuContainerPanel.TabIndex = 0;
            // 
            // sigOutContainerPanel
            // 
            sigOutContainerPanel.Controls.Add(lblSignOut);
            sigOutContainerPanel.Controls.Add(pictureBox5);
            sigOutContainerPanel.Dock = DockStyle.Bottom;
            sigOutContainerPanel.Location = new Point(0, 397);
            sigOutContainerPanel.Name = "sigOutContainerPanel";
            sigOutContainerPanel.Size = new Size(209, 53);
            sigOutContainerPanel.TabIndex = 1;
            // 
            // mainPanel
            // 
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(209, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(591, 450);
            mainPanel.TabIndex = 3;
            // 
            // MainInterface
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(mainPanel);
            Controls.Add(panelMainInterface);
            Name = "MainInterface";
            Text = "MainInterface";
            Load += MainInterface_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
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