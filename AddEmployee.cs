using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;


namespace EmployeManagementSoftware
{
    public partial class AddEmployee : Form
    {
        private string dbPath = "employees.db";
        private int? selectedEmployeeId = null;
        private byte[] currentPhoto = null;

        public AddEmployee()
        {
            InitializeComponent();
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.Columns.Add("colEmployeeID", "Employee ID");
            this.dataGridView1.Columns.Add("colFullName", "Full Name");
            this.dataGridView1.Columns.Add("colPhoneNumber", "Phone Number");
            this.dataGridView1.Columns.Add("colEmail", "Email Address");
            this.dataGridView1.Columns.Add("colPosition", "Position");
            this.dataGridView1.Columns.Add("colGender", "Gender");

            InitializeDatabase();
            LoadPositions();
            LoadGenders();
            LoadData();
        }



        private void LoadData()
        {
            if (dataGridView1.Columns.Count < 7)
            {
                dataGridView1.Columns.Clear();

                // Photo FIRST (Leftmost)
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn.Name = "colPhoto";
                imgColumn.HeaderText = "Photo";
                imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imgColumn.Width = 80;
                dataGridView1.Columns.Add(imgColumn);

                // Other columns
                dataGridView1.Columns.Add("colEmployeeID", "Employee ID");
                dataGridView1.Columns.Add("colFullName", "Full Name");
                dataGridView1.Columns.Add("colPhoneNumber", "Phone Number");
                dataGridView1.Columns.Add("colEmail", "Email Address");
                dataGridView1.Columns.Add("colPosition", "Position");
                dataGridView1.Columns.Add("colGender", "Gender");
            }

            dataGridView1.Rows.Clear();

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                string query = "SELECT EmployeeID, FullName, PhoneNumber, Email, Position, Gender, Photo FROM Employees";

                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Image photo = null;
                        if (!reader.IsDBNull(6))
                        {
                            byte[] photoBytes = reader.GetValue(6) as byte[];
                            if (photoBytes != null && photoBytes.Length > 0)
                            {
                                using (var ms = new MemoryStream(photoBytes))
                                {
                                    photo = Image.FromStream(ms);
                                }
                            }
                        }

                        dataGridView1.Rows.Add(
                            photo,                        // Photo - First column
                            reader.GetInt32(0),           // Employee ID
                            reader.GetValue(1) ?? "",     // Full Name
                            reader.GetValue(2) ?? "",     // Phone
                            reader.GetValue(3) ?? "",     // Email
                            reader.GetValue(4) ?? "",     // Position
                            reader.GetValue(5) ?? ""      // Gender
                        );
                    }
                }
            }
        }

        private void LoadGenders()
        {
            cbGender.Items.AddRange(new[] { "Male", "Female", "Other" });
        }

        private void LoadPositions()
        {
            cbPosition.Items.AddRange(new[] { "Manager", "Developer", "Designer", "HR", "Sales", "Accountant" });
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                // Add Email column if it doesn't exist
                string alterTable = "ALTER TABLE Employees ADD COLUMN Email TEXT;";
                try
                {
                    using (var command = new SqliteCommand(alterTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch { } // Column already exists - ignore error

                // Ensure table has all columns
                string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Employees (
                EmployeeID INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT NOT NULL,
                PhoneNumber TEXT,
                Email TEXT,
                Position TEXT,
                Gender TEXT,
                Photo BLOB
            );";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required.");
                return;
            }


            if (!string.IsNullOrWhiteSpace(txtPhoneNumber.Text) && txtPhoneNumber.Text.Length != 11)
            {
                MessageBox.Show("Phone Number must be exactly 11 digits.", "Invalid Phone Number",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                string email = txtEmail.Text.Trim().ToLower();
                if (!email.EndsWith("@gmail.com"))
                {
                    MessageBox.Show("Email must be a valid Gmail address (example@gmail.com)",
                                    "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }
            }

            int? targetId = null;

            if (!string.IsNullOrWhiteSpace(txtEmployeeID.Text))
            {
                if (int.TryParse(txtEmployeeID.Text, out int parsedId) && parsedId > 0)
                {
                    targetId = parsedId;
                }
                else
                {
                    MessageBox.Show("Invalid Employee ID.");
                    return;
                }
            }
            else if (selectedEmployeeId.HasValue)
            {
                targetId = selectedEmployeeId;
            }

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                if (targetId.HasValue)
                {
                    // Check if ID exists
                    string checkQuery = "SELECT COUNT(*) FROM Employees WHERE EmployeeID = @ID";
                    using (var checkCmd = new SqliteCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@ID", targetId.Value);
                        long count = (long)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // UPDATE
                            string updateQuery = @"UPDATE Employees 
                        SET FullName=@FullName, PhoneNumber=@PhoneNumber, Email=@Email,
                            Position=@Position, Gender=@Gender, Photo=@Photo 
                        WHERE EmployeeID=@ID";

                            using (var cmd = new SqliteCommand(updateQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                                cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text?.Trim() ?? "");
                                cmd.Parameters.AddWithValue("@Email", txtEmail.Text?.Trim() ?? "");
                                cmd.Parameters.AddWithValue("@Position", cbPosition.Text ?? "");
                                cmd.Parameters.AddWithValue("@Gender", cbGender.Text ?? "");
                                cmd.Parameters.AddWithValue("@Photo", currentPhoto ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@ID", targetId.Value);
                                cmd.ExecuteNonQuery();
                            }
                            MessageBox.Show("Employee updated successfully!");
                        }
                        else
                        {
                            // INSERT with manual ID
                            string insertQuery = @"INSERT INTO Employees 
                        (EmployeeID, FullName, PhoneNumber, Email, Position, Gender, Photo) 
                        VALUES (@ID, @FullName, @PhoneNumber, @Email, @Position, @Gender, @Photo)";

                            using (var cmd = new SqliteCommand(insertQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@ID", targetId.Value);
                                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                                cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text?.Trim() ?? "");
                                cmd.Parameters.AddWithValue("@Email", txtEmail.Text?.Trim() ?? "");
                                cmd.Parameters.AddWithValue("@Position", cbPosition.Text ?? "");
                                cmd.Parameters.AddWithValue("@Gender", cbGender.Text ?? "");
                                cmd.Parameters.AddWithValue("@Photo", currentPhoto ?? (object)DBNull.Value);
                                cmd.ExecuteNonQuery();
                            }
                            MessageBox.Show("Employee added with ID " + targetId.Value);
                        }
                    }
                }
                else
                {
                    // Normal auto-increment INSERT
                    string insertQuery = @"INSERT INTO Employees 
                (FullName, PhoneNumber, Email, Position, Gender, Photo) 
                VALUES (@FullName, @PhoneNumber, @Email, @Position, @Gender, @Photo)";

                    using (var cmd = new SqliteCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text?.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text?.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@Position", cbPosition.Text ?? "");
                        cmd.Parameters.AddWithValue("@Gender", cbGender.Text ?? "");
                        cmd.Parameters.AddWithValue("@Photo", currentPhoto ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("New employee added successfully!");
                }
            }

            ClearFields();
            LoadData();
        }

        private void ClearFields()
        {
            selectedEmployeeId = null;
            txtEmployeeID.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtPhoneNumber.Clear();

            cbPosition.SelectedIndex = -1;
            cbGender.SelectedIndex = -1;

            pbPicture.Image = null;
            currentPhoto = null;

            // Optional: Deselect row in grid
            dataGridView1.ClearSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int employeeId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            if (MessageBox.Show($"Are you sure you want to delete Employee ID {employeeId}?",
                                "Confirm Delete",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = "DELETE FROM Employees WHERE EmployeeID = @ID";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", employeeId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Employee deleted successfully!");
                LoadData();        // Refresh the grid
                ClearFields();     // Clear the form fields
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog { Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentPhoto = File.ReadAllBytes(ofd.FileName);
                pbPicture.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            selectedEmployeeId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            txtEmployeeID.Text = selectedEmployeeId.ToString();
            txtFullName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
            txtPhoneNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value?.ToString();
            cbPosition.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value?.ToString();
            cbGender.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value?.ToString();
            // Photo loading can be extended later
        }

        private void btnLoad_Click(object sender, EventArgs e) => LoadData();

        private void btnClear_Click(object sender, EventArgs e) => ClearFields();

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (backspace, delete, etc.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Limit to 11 digits
            if (txtPhoneNumber.Text.Length >= 11 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow letters, numbers, @, ., _, -, and control keys
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '@' && e.KeyChar != '.' &&
                e.KeyChar != '_' && e.KeyChar != '-' &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
