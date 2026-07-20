using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // Photo
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.HeaderText = "Photo";
            imgCol.Width = 80;
            dataGridView1.Columns.Add(imgCol);

            // Other columns
            dataGridView1.Columns.Add("colID", "Employee ID");
            dataGridView1.Columns.Add("colName", "Full Name");
            dataGridView1.Columns.Add("colPhone", "Phone Number");
            dataGridView1.Columns.Add("colEmail", "Email Address");
            dataGridView1.Columns.Add("colPosition", "Position");
            dataGridView1.Columns.Add("colGender", "Gender");

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
                            byte[] bytes = reader.GetValue(6) as byte[];
                            if (bytes != null && bytes.Length > 0)
                            {
                                using (var ms = new MemoryStream(bytes))
                                {
                                    photo = Image.FromStream(ms);
                                }
                            }
                        }

                        int employeeId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);

                        dataGridView1.Rows.Add(
                            photo,
                            employeeId,
                            reader.IsDBNull(1) ? "" : reader.GetValue(1),
                            reader.IsDBNull(2) ? "" : reader.GetValue(2),
                            reader.IsDBNull(3) ? "" : reader.GetValue(3),
                            reader.IsDBNull(4) ? "" : reader.GetValue(4),
                            reader.IsDBNull(5) ? "" : reader.GetValue(5)
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
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    // Drop old table if it exists (to force correct structure)
                    using (var dropCmd = new SqliteCommand("DROP TABLE IF EXISTS Employees", connection))
                    {
                        dropCmd.ExecuteNonQuery();
                    }

                    // Create new table with proper AUTOINCREMENT
                    string createTable = @"
                CREATE TABLE Employees (
                    EmployeeID INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName TEXT NOT NULL,
                    PhoneNumber TEXT,
                    Email TEXT,
                    Position TEXT,
                    Gender TEXT,
                    Photo BLOB
                );";

                    using (var cmd = new SqliteCommand(createTable, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Database recreated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
    {
        MessageBox.Show("Full Name is required.");
        return;
    }

    // Validation
    if (!string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
    {
        string phone = txtPhoneNumber.Text.Trim();
        if (!(phone.Length == 11 && phone.StartsWith("09")) && 
            !(phone.Length == 13 && phone.StartsWith("+63")))
        {
            MessageBox.Show("Invalid Philippine number.");
            return;
        }
    }

    if (!string.IsNullOrWhiteSpace(txtEmail.Text))
    {
        if (!txtEmail.Text.Trim().ToLower().EndsWith("@gmail.com"))
        {
            MessageBox.Show("Only Gmail accounts allowed.");
            return;
        }
    }

    try
    {
        using (var connection = new SqliteConnection($"Data Source={dbPath}"))
        {
            connection.Open();

            // === DUPLICATE CHECK ===
            string checkQuery = @"
                SELECT COUNT(*) FROM Employees 
                WHERE EmployeeID = @ID 
                   OR (FullName = @FullName AND PhoneNumber = @PhoneNumber)
                   OR Email = @Email";

            using (var checkCmd = new SqliteCommand(checkQuery, connection))
            {
                checkCmd.Parameters.AddWithValue("@ID", txtEmployeeID.Text);
                checkCmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                checkCmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text?.Trim() ?? "");
                checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text?.Trim() ?? "");

                long exists = (long)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    MessageBox.Show("An employee with the same ID, Name+Phone, or Email already exists!", 
                                    "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

                    

                    // INSERT new employee
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
        }

        MessageBox.Show("Employee added successfully!");
        ClearFields();
        LoadData();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message);
    }
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
                MessageBox.Show("Please select a row in the table to delete.",
                                "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int employeeId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value); // Change index if needed

            if (MessageBox.Show($"Delete Employee ID {employeeId}?",
                                "Confirm Delete",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
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
                    LoadData();        // Refresh grid
                    ClearFields();     // Clear form
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
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

            try
            {
                var row = dataGridView1.Rows[e.RowIndex];

                // Assuming column order: Photo(0), EmployeeID(1), FullName(2), Phone(3), Email(4), Position(5), Gender(6)
                selectedEmployeeId = Convert.ToInt32(row.Cells[1].Value);

                txtEmployeeID.Text = selectedEmployeeId.ToString();
                txtFullName.Text = row.Cells[2].Value?.ToString() ?? "";
                txtPhoneNumber.Text = row.Cells[3].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells[4].Value?.ToString() ?? "";
                cbPosition.Text = row.Cells[5].Value?.ToString() ?? "";
                cbGender.Text = row.Cells[6].Value?.ToString() ?? "";

                // Load photo preview if possible
                if (row.Cells[0].Value is Image photoImage)
                {
                    pbPicture.Image = photoImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employee data: " + ex.Message);
            }

            if (e.RowIndex < 0) return;

            try
            {
                var row = dataGridView1.Rows[e.RowIndex];

                selectedEmployeeId = Convert.ToInt32(row.Cells[1].Value); // Employee ID column

                txtEmployeeID.Text = selectedEmployeeId.ToString();
                txtFullName.Text = row.Cells[2].Value?.ToString() ?? "";
                txtPhoneNumber.Text = row.Cells[3].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells[4].Value?.ToString() ?? "";
                cbPosition.Text = row.Cells[5].Value?.ToString() ?? "";
                cbGender.Text = row.Cells[6].Value?.ToString() ?? "";

                // Load photo preview
                if (row.Cells[0].Value is Image img)
                    pbPicture.Image = img;
            }
            catch { }
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

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hit.RowIndex].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[hit.RowIndex].Cells[0];
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeId == null)
            {
                MessageBox.Show("Please select a row first.", "No Selection");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required.");
                return;
            }

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    string query = @"UPDATE Employees 
                           SET FullName=@FullName, PhoneNumber=@PhoneNumber, Email=@Email,
                               Position=@Position, Gender=@Gender, Photo=@Photo 
                           WHERE EmployeeID = @ID";

                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text?.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text?.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@Position", cbPosition.Text ?? "");
                        cmd.Parameters.AddWithValue("@Gender", cbGender.Text ?? "");
                        cmd.Parameters.AddWithValue("@Photo", currentPhoto ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ID", selectedEmployeeId.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Employee updated successfully!");
                ClearFields();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        }
}
