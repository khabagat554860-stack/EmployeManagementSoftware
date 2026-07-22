using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        private bool isRefreshing = false; // Prevents selection events from populating fields during refresh

        public AddEmployee()
        {
            InitializeComponent();

            // Set Full Row Selection
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Automatically trigger field population whenever ANY row selection changes
            this.dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            // Automatically clear fields whenever switching back to this screen from side menu
            this.VisibleChanged += AddEmployee_VisibleChanged;

            InitializeDatabase();
            LoadPositions();
            LoadGenders();
            LoadStatuses();
            LoadData();
        }

        private void AddEmployee_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                ClearFields();
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (isRefreshing) return; // Do nothing if loading or clearing fields

            if (dataGridView1.CurrentRow != null &&
                dataGridView1.CurrentRow.Index >= 0 &&
                !dataGridView1.CurrentRow.IsNewRow &&
                dataGridView1.CurrentRow.Cells[1].Value != null)
            {
                PopulateFieldsFromRow(dataGridView1.CurrentRow.Index);
            }
        }

        private void LoadStatuses()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new[] { "Active", "On Leave", "Inactive" });
            cmbStatus.SelectedIndex = 0; // Default selection to "Active"
        }

        private void LoadGenders()
        {
            cbGender.Items.Clear();
            cbGender.Items.AddRange(new[] { "Male", "Female", "Other" });
        }

        private void LoadPositions()
        {
            cbPosition.Items.Clear();
            cbPosition.Items.AddRange(new[] { "Manager", "Developer", "Designer", "HR", "Sales", "Accountant" });
        }

        private void InitializeDatabase()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    // Create table with Status column
                    string createTable = @"
                    CREATE TABLE IF NOT EXISTS Employees (
                        EmployeeID INTEGER PRIMARY KEY,
                        FullName TEXT NOT NULL,
                        PhoneNumber TEXT,
                        Email TEXT,
                        Position TEXT,
                        Gender TEXT,
                        Photo BLOB,
                        Status TEXT DEFAULT 'Active'
                    );";

                    using (var cmd = new SqliteCommand(createTable, connection))
                        cmd.ExecuteNonQuery();

                    // Auto-migration: Add Status column if database existed previously without it
                    try
                    {
                        string alterTable = "ALTER TABLE Employees ADD COLUMN Status TEXT DEFAULT 'Active';";
                        using (var alterCmd = new SqliteCommand(alterTable, connection))
                            alterCmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        // Column already exists, ignore error
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database initialization error: " + ex.Message);
            }
        }

        private void LoadData()
        {
            isRefreshing = true;
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                // Photo
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol.HeaderText = "Photo";
                imgCol.Width = 80;
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dataGridView1.Columns.Add(imgCol);

                // Text columns
                dataGridView1.Columns.Add("colID", "Employee ID");
                dataGridView1.Columns.Add("colName", "Full Name");
                dataGridView1.Columns.Add("colPhone", "Phone Number");
                dataGridView1.Columns.Add("colEmail", "Email Address");
                dataGridView1.Columns.Add("colPosition", "Position");
                dataGridView1.Columns.Add("colGender", "Gender");
                dataGridView1.Columns.Add("colStatus", "Status");

                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = "SELECT EmployeeID, FullName, PhoneNumber, Email, Position, Gender, Photo, Status FROM Employees";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Image photo = null;
                            byte[] photoBytes = null;

                            if (!reader.IsDBNull(6))
                            {
                                photoBytes = reader.GetValue(6) as byte[];
                                if (photoBytes != null && photoBytes.Length > 0)
                                {
                                    try
                                    {
                                        using (var ms = new MemoryStream(photoBytes))
                                        {
                                            Image img = Image.FromStream(ms);
                                            photo = new Bitmap(img, new Size(60, 60));
                                            img.Dispose();
                                        }
                                    }
                                    catch
                                    {
                                        photo = null;
                                    }
                                }
                            }

                            int employeeId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            string statusVal = reader.IsDBNull(7) || string.IsNullOrWhiteSpace(reader.GetString(7)) ? "Active" : reader.GetString(7);

                            int rowIndex = dataGridView1.Rows.Add(
                                photo,
                                employeeId,
                                reader.IsDBNull(1) ? "" : reader.GetValue(1),
                                reader.IsDBNull(2) ? "" : reader.GetValue(2),
                                reader.IsDBNull(3) ? "" : reader.GetValue(3),
                                reader.IsDBNull(4) ? "" : reader.GetValue(4),
                                reader.IsDBNull(5) ? "" : reader.GetValue(5),
                                statusVal
                            );

                            // Save photo bytes in tag to preserve existing image when editing
                            dataGridView1.Rows[rowIndex].Cells[0].Tag = photoBytes;
                        }
                    }
                }
            }
            finally
            {
                isRefreshing = false;
            }

            // Unselect all grid rows and clear inputs after loading
            ClearFields();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmployeeID.Text) || !int.TryParse(txtEmployeeID.Text.Trim(), out int employeeId))
            {
                MessageBox.Show("Employee ID must be a valid number.");
                return;
            }

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

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !txtEmail.Text.Trim().ToLower().EndsWith("@gmail.com"))
            {
                MessageBox.Show("Only Gmail accounts allowed.");
                return;
            }

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Employees WHERE EmployeeID = @ID OR Email = @Email";
                    using (var checkCmd = new SqliteCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@ID", employeeId);
                        checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text?.Trim() ?? "");
                        if ((long)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Employee ID or Email already exists!");
                            return;
                        }
                    }

                    string insertQuery = @"INSERT INTO Employees 
                    (EmployeeID, FullName, PhoneNumber, Email, Position, Gender, Photo, Status)
                    VALUES (@ID, @FullName, @PhoneNumber, @Email, @Position, @Gender, @Photo, @Status)";

                    using (var cmd = new SqliteCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", employeeId);
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text?.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text?.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@Position", cbPosition.Text ?? "");
                        cmd.Parameters.AddWithValue("@Gender", cbGender.Text ?? "");
                        cmd.Parameters.AddWithValue("@Photo", currentPhoto ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(cmbStatus.Text) ? "Active" : cmbStatus.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                // --- ACTIVITY LOG TRIGGER ---
                DatabaseHelper.LogActivity(
                    txtFullName.Text.Trim(),
                    "Employee Added",
                    $"Added as {cbPosition.Text} (ID: {employeeId})"
                );

                MessageBox.Show("Employee added successfully!");
                ClearFields();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeId == null)
            {
                MessageBox.Show("Please select an employee to edit first.", "No Selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmployeeID.Text) ||
                !int.TryParse(txtEmployeeID.Text.Trim(), out int newEmployeeId))
            {
                MessageBox.Show("Valid Employee ID is required.");
                return;
            }

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    string checkQuery = @"
                    SELECT COUNT(*) FROM Employees 
                    WHERE (EmployeeID = @NewID AND EmployeeID != @OldID) 
                       OR (Email = @Email AND EmployeeID != @OldID)";

                    using (var checkCmd = new SqliteCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@NewID", newEmployeeId);
                        checkCmd.Parameters.AddWithValue("@OldID", selectedEmployeeId.Value);
                        checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text?.Trim() ?? "");

                        if ((long)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Employee ID or Email already exists!", "Duplicate",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string updateQuery = @"UPDATE Employees SET 
                        EmployeeID = @NewID,
                        FullName = @FullName,
                        PhoneNumber = @PhoneNumber,
                        Email = @Email,
                        Position = @Position,
                        Gender = @Gender,
                        Photo = @Photo,
                        Status = @Status
                    WHERE EmployeeID = @OldID";

                    using (var cmd = new SqliteCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@NewID", newEmployeeId);
                        cmd.Parameters.AddWithValue("@OldID", selectedEmployeeId.Value);
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text?.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text?.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@Position", cbPosition.Text ?? "");
                        cmd.Parameters.AddWithValue("@Gender", cbGender.Text ?? "");
                        cmd.Parameters.AddWithValue("@Photo", currentPhoto ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(cmbStatus.Text) ? "Active" : cmbStatus.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // --- ACTIVITY LOG TRIGGER ---
                            DatabaseHelper.LogActivity(
                                txtFullName.Text.Trim(),
                                "Profile Updated",
                                $"Updated details for employee ID: {newEmployeeId}"
                            );

                            MessageBox.Show("Employee updated successfully!");
                            ClearFields();
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("No changes were made.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating employee: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a row in the table to delete.",
                                "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int employeeId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);
            string employeeName = dataGridView1.CurrentRow.Cells[2].Value?.ToString() ?? $"ID: {employeeId}";

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

                        // 1. Delete associated salary record first (prevents orphan records)
                        try
                        {
                            string deleteSalaryQuery = "DELETE FROM Salaries WHERE EmployeeID = @ID";
                            using (var cmdSalary = new SqliteCommand(deleteSalaryQuery, connection))
                            {
                                cmdSalary.Parameters.AddWithValue("@ID", employeeId);
                                cmdSalary.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                            // Handle case where salary table might be named 'Salary'
                            try
                            {
                                string deleteSalaryQueryAlt = "DELETE FROM Salary WHERE EmployeeID = @ID";
                                using (var cmdSalaryAlt = new SqliteCommand(deleteSalaryQueryAlt, connection))
                                {
                                    cmdSalaryAlt.Parameters.AddWithValue("@ID", employeeId);
                                    cmdSalaryAlt.ExecuteNonQuery();
                                }
                            }
                            catch { /* Ignore if salary table hasn't been created yet */ }
                        }

                        // 2. Delete the employee record
                        string query = "DELETE FROM Employees WHERE EmployeeID = @ID";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID", employeeId);
                            command.ExecuteNonQuery();
                        }
                    }

                    // --- ACTIVITY LOG TRIGGER ---
                    DatabaseHelper.LogActivity(
                        employeeName,
                        "Employee Deleted",
                        $"Removed employee ID: {employeeId} and associated records"
                    );

                    MessageBox.Show("Employee deleted successfully!");
                    LoadData();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PopulateFieldsFromRow(e.RowIndex);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PopulateFieldsFromRow(e.RowIndex);
        }

        private void PopulateFieldsFromRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dataGridView1.Rows.Count) return;

            try
            {
                var row = dataGridView1.Rows[rowIndex];

                if (row.Cells[1].Value == null || row.IsNewRow) return;

                selectedEmployeeId = Convert.ToInt32(row.Cells[1].Value);

                txtEmployeeID.Text = selectedEmployeeId.ToString();
                txtFullName.Text = row.Cells[2].Value?.ToString() ?? "";
                txtPhoneNumber.Text = row.Cells[3].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells[4].Value?.ToString() ?? "";
                cbPosition.Text = row.Cells[5].Value?.ToString() ?? "";
                cbGender.Text = row.Cells[6].Value?.ToString() ?? "";
                cmbStatus.Text = row.Cells[7].Value?.ToString() ?? "Active";

                currentPhoto = row.Cells[0].Tag as byte[];

                if (row.Cells[0].Value is Image photoImage)
                {
                    pbPicture.Image = photoImage;
                }
                else
                {
                    pbPicture.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employee data: " + ex.Message);
            }
        }

        public void ClearFields()
        {
            selectedEmployeeId = null;
            txtEmployeeID.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtPhoneNumber.Clear();

            cbPosition.SelectedIndex = -1;
            cbGender.SelectedIndex = -1;
            if (cmbStatus.Items.Count > 0) cmbStatus.SelectedIndex = 0;

            pbPicture.Image = null;
            currentPhoto = null;

            isRefreshing = true;
            dataGridView1.ClearSelection();
            isRefreshing = false;
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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearFields();
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearFields();

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            if (txtPhoneNumber.Text.Length >= 11 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
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
    }
}