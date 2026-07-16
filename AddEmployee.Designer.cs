namespace EmployeManagementSoftware
{
    partial class AddEmployee
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
            panel2 = new Panel();
            txtEmail = new TextBox();
            txtPhoneNumber = new TextBox();
            label7 = new Label();
            btnClear = new Button();
            btnLoad = new Button();
            btnDelete = new Button();
            cbPosition = new ComboBox();
            label6 = new Label();
            btnAdd = new Button();
            btnImport = new Button();
            pbPicture = new PictureBox();
            label5 = new Label();
            cbGender = new ComboBox();
            label4 = new Label();
            txtFullName = new TextBox();
            label3 = new Label();
            txtEmployeeID = new TextBox();
            label2 = new Label();
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPicture).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = SystemColors.ButtonHighlight;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(txtEmail);
            panel2.Controls.Add(txtPhoneNumber);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(btnClear);
            panel2.Controls.Add(btnLoad);
            panel2.Controls.Add(btnDelete);
            panel2.Controls.Add(cbPosition);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(btnAdd);
            panel2.Controls.Add(btnImport);
            panel2.Controls.Add(pbPicture);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(cbGender);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(txtFullName);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(txtEmployeeID);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(12, 278);
            panel2.Name = "panel2";
            panel2.Size = new Size(1481, 243);
            panel2.TabIndex = 5;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(429, 13);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(150, 23);
            txtEmail.TabIndex = 18;
            txtEmail.KeyPress += txtEmail_KeyPress;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(429, 59);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(150, 23);
            txtPhoneNumber.TabIndex = 17;
            txtPhoneNumber.KeyPress += txtPhoneNumber_KeyPress;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(320, 61);
            label7.Name = "label7";
            label7.Size = new Size(94, 15);
            label7.TabIndex = 16;
            label7.Text = "Phone Number:";
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnClear.BackColor = Color.FromArgb(0, 150, 200);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderColor = Color.FromArgb(0, 150, 200);
            btnClear.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 150, 200);
            btnClear.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 150, 200);
            btnClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 150, 200);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            btnClear.Location = new Point(1109, 160);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(297, 50);
            btnClear.TabIndex = 15;
            btnClear.Text = "CLEAR";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnLoad
            // 
            btnLoad.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnLoad.BackColor = Color.FromArgb(0, 150, 200);
            btnLoad.Cursor = Cursors.Hand;
            btnLoad.FlatAppearance.BorderColor = Color.FromArgb(0, 150, 200);
            btnLoad.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 150, 200);
            btnLoad.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 150, 200);
            btnLoad.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 150, 200);
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            btnLoad.Location = new Point(731, 160);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(327, 50);
            btnLoad.TabIndex = 14;
            btnLoad.Text = "LOAD";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.BackColor = Color.FromArgb(0, 150, 200);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderColor = Color.FromArgb(0, 150, 200);
            btnDelete.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 150, 200);
            btnDelete.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 150, 200);
            btnDelete.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 150, 200);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            btnDelete.Location = new Point(370, 160);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(304, 50);
            btnDelete.TabIndex = 13;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // cbPosition
            // 
            cbPosition.FormattingEnabled = true;
            cbPosition.Location = new Point(429, 99);
            cbPosition.Name = "cbPosition";
            cbPosition.Size = new Size(150, 23);
            cbPosition.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(343, 99);
            label6.Name = "label6";
            label6.Size = new Size(54, 15);
            label6.TabIndex = 11;
            label6.Text = "Position:";
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            btnAdd.BackColor = Color.FromArgb(0, 150, 200);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 150, 200);
            btnAdd.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 150, 200);
            btnAdd.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 150, 200);
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 150, 200);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            btnAdd.Location = new Point(25, 160);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(287, 50);
            btnAdd.TabIndex = 10;
            btnAdd.Text = "ADD";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnImport
            // 
            btnImport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnImport.BackColor = Color.FromArgb(0, 150, 200);
            btnImport.Cursor = Cursors.Hand;
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnImport.Location = new Point(1352, 132);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(118, 25);
            btnImport.TabIndex = 9;
            btnImport.Text = "Import";
            btnImport.UseVisualStyleBackColor = false;
            btnImport.Click += btnImport_Click;
            // 
            // pbPicture
            // 
            pbPicture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pbPicture.BackColor = SystemColors.ButtonShadow;
            pbPicture.Location = new Point(1352, 28);
            pbPicture.Name = "pbPicture";
            pbPicture.Size = new Size(118, 101);
            pbPicture.TabIndex = 8;
            pbPicture.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(320, 15);
            label5.Name = "label5";
            label5.Size = new Size(89, 15);
            label5.TabIndex = 6;
            label5.Text = "Email Address:";
            // 
            // cbGender
            // 
            cbGender.FormattingEnabled = true;
            cbGender.Location = new Point(96, 99);
            cbGender.Name = "cbGender";
            cbGender.Size = new Size(150, 23);
            cbGender.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(10, 99);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 4;
            label4.Text = "Gender:";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(96, 57);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(150, 23);
            txtFullName.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(10, 59);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 2;
            label3.Text = "Full Name:";
            // 
            // txtEmployeeID
            // 
            txtEmployeeID.Location = new Point(96, 13);
            txtEmployeeID.Name = "txtEmployeeID";
            txtEmployeeID.Size = new Size(150, 23);
            txtEmployeeID.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(10, 15);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 0;
            label2.Text = "Employee ID:";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(1481, 231);
            panel1.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(10, 31);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1460, 179);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(10, 5);
            label1.Name = "label1";
            label1.Size = new Size(148, 23);
            label1.TabIndex = 0;
            label1.Text = "Employee's Data";
            // 
            // AddEmployee
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1505, 526);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "AddEmployee";
            Text = "AddEmployee";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbPicture).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Button btnClear;
        private Button btnLoad;
        private Button btnDelete;
        private ComboBox cbPosition;
        private Label label6;
        private Button btnAdd;
        private Button btnImport;
        private PictureBox pbPicture;
        private Label label5;
        private ComboBox cbGender;
        private Label label4;
        private TextBox txtFullName;
        private Label label3;
        private TextBox txtEmployeeID;
        private Label label2;
        private Panel panel1;
        private DataGridView dataGridView1;
        private Label label1;
        private TextBox txtPhoneNumber;
        private Label label7;
        private TextBox txtEmail;
    }
}