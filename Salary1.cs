using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace EmployeManagementSoftware
{
    public partial class Salary1 : Form
    {
        private int selectedRecordID = -1;

        // Flag to prevent event recursion when modifying dtpPayPeriod.Value inside its own event
        private bool isAdjustingPayPeriod = false;

        public Salary1()
        {
            InitializeComponent();

            

            // Make Position ComboBox strictly display-only
            cmbPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPosition.Enabled = false;

            // Format Pay Period picker to display clean Month & Year format
            dtpPayPeriod.Format = DateTimePickerFormat.Custom;
            dtpPayPeriod.CustomFormat = "MMMM yyyy";

            // Set initial Pay Period picker to the 15th of the current month
            DateTime now = DateTime.Now;
            dtpPayPeriod.Value = new DateTime(now.Year, now.Month, 15);

            // Wire up event handlers for value changes and popup calendar closure
            dtpPayPeriod.ValueChanged -= dtpPayPeriod_ValueChanged;
            dtpPayPeriod.ValueChanged += dtpPayPeriod_ValueChanged;

            dtpPayPeriod.CloseUp -= dtpPayPeriod_CloseUp;
            dtpPayPeriod.CloseUp += dtpPayPeriod_CloseUp;

            // Initialize payment date to the 15th of the selected pay period
            SetPaymentDateToFifteenth();

            // Load initial data for selected month
            RefreshSalaryGrid();
            LoadDashboardMetrics();
        }

        /// <summary>
        /// Ensures the Pay Period date is anchored to the 15th of the selected month/year.
        /// </summary>
        private void EnsurePayPeriodIsFifteenth()
        {
            if (isAdjustingPayPeriod) return;

            DateTime selected = dtpPayPeriod.Value;

            // Snap date to 15th if it isn't already
            if (selected.Day != 15)
            {
                isAdjustingPayPeriod = true;
                dtpPayPeriod.Value = new DateTime(selected.Year, selected.Month, 15);
                isAdjustingPayPeriod = false;
            }

            // Sync payment date picker and refresh grid/dashboard metrics
            SetPaymentDateToFifteenth();
            UpdateDashboard();
            RefreshSalaryGrid();
            LoadDashboardMetrics();
        }

        /// <summary>
        /// Automatically forces the payment date picker to snap to the 15th of the currently selected month/year.
        /// </summary>
        private void SetPaymentDateToFifteenth()
        {
            DateTime selected = dtpPayPeriod.Value;
            dtpPaymentDate.Value = new DateTime(selected.Year, selected.Month, 15);
        }

        /// <summary>
        /// Helper to safely assign position text to a DropDownList ComboBox
        /// </summary>
        private void SetPositionText(string position)
        {
            if (string.IsNullOrWhiteSpace(position))
            {
                cmbPosition.SelectedIndex = -1;
                return;
            }

            if (!cmbPosition.Items.Contains(position))
            {
                cmbPosition.Items.Add(position);
            }

            cmbPosition.SelectedItem = position;
        }

        private void RefreshSalaryGrid()
        {
            DataTable dt = DatabaseHelper.GetSalaryRecords(dtpPayPeriod.Value);

            // Unbind first to force WinForms to completely erase old visual rows
            dgvSalaryRecords.DataSource = null;
            dgvSalaryRecords.DataSource = dt;

            lblTotalRecords.Text = dt.Rows.Count.ToString();
        }

        private double GetDecimalValue(Guna.UI2.WinForms.Guna2TextBox tb)
        {
            return (tb.Text == "0.00" || string.IsNullOrWhiteSpace(tb.Text)) ? 0.00 : Convert.ToDouble(tb.Text);
        }

        private void UpdateDashboard()
        {
            using (var conn = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                conn.Open();

                string selectedYear = dtpPayPeriod.Value.Year.ToString();
                string selectedMonth = dtpPayPeriod.Value.ToString("MMMM"); // Dynamic matching e.g., "July"

                // Update date text sub-labels dynamically
                lblGrossMonth.Text = $"For {selectedMonth} {selectedYear}";
                lblDeductionMonth.Text = $"For {selectedMonth} {selectedYear}";
                lblNetMonth.Text = $"For {selectedMonth} {selectedYear}";

                // 1. Total Unique Employees Count
                string empQuery = "SELECT COUNT(*) FROM Employees";
                using (SQLiteCommand cmd = new SQLiteCommand(empQuery, conn))
                {
                    lblTotalEmployees.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                }

                // 2. Financial Totals filtered by Selected Pay Period
                string financeQuery = @"SELECT 
                                            COALESCE(SUM(BasicSalary + Allowances), 0), 
                                            COALESCE(SUM(Deductions), 0), 
                                            COALESCE(SUM(NetSalary), 0) 
                                        FROM SalaryRecords 
                                        WHERE PayPeriodMonth = @Month AND PayPeriodYear = @Year";

                using (SQLiteCommand cmd = new SQLiteCommand(financeQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Month", selectedMonth);
                    cmd.Parameters.AddWithValue("@Year", selectedYear);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblGrossPay.Text = "₱" + Convert.ToDouble(reader[0]).ToString("N2");
                            lblDeduction.Text = "₱" + Convert.ToDouble(reader[1]).ToString("N2");
                            lblNetPay.Text = "₱" + Convert.ToDouble(reader[2]).ToString("N2");
                        }
                    }
                }
            }
        }

        private void LoadSalaryRecords(string employeeId = "")
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT s.RecordID, s.EmployeeID, e.FullName, e.Position, 
                                 s.BasicSalary, s.Allowances, s.Deductions, s.NetSalary, s.PaymentDate 
                                 FROM SalaryRecords s
                                 JOIN Employees e ON s.EmployeeID = e.EmployeeID";

                if (!string.IsNullOrEmpty(employeeId))
                {
                    query += " WHERE s.EmployeeID = @EmployeeID";
                }

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(employeeId))
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvSalaryRecords.DataSource = dt;
                    lblTotalRecords.Text = dt.Rows.Count.ToString(); // Updates bottom counter
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string employeeId = txtEmpID.Text.Trim();

            if (string.IsNullOrEmpty(employeeId))
            {
                MessageBox.Show("Please select or enter an Employee ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Grab the selected pay period date (Month & Year)
            DateTime selectedPayPeriod = dtpPayPeriod.Value;

            // 2. Check for duplicate records within the SAME month and year
            if (DatabaseHelper.DoesSalaryRecordExist(employeeId, selectedPayPeriod))
            {
                MessageBox.Show($"A salary record for Employee ID '{employeeId}' already exists for {selectedPayPeriod:MMMM yyyy}!\n\nPlease use 'Update' to modify it, or select a different pay period.",
                                "Duplicate Monthly Entry",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 3. Clean and parse numeric input values
            string rawBasic = txtBasicSalary.Text.Replace("P", "").Replace("₱", "").Trim();
            string rawAllowances = txtAllowances.Text.Replace("P", "").Replace("₱", "").Trim();
            string rawDeductions = txtDeductions.Text.Replace("P", "").Replace("₱", "").Trim();

            if (!double.TryParse(rawBasic, out double basicSalary) ||
                !double.TryParse(rawAllowances, out double allowances) ||
                !double.TryParse(rawDeductions, out double deductions))
            {
                MessageBox.Show("Please enter valid numeric amounts.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Save to database using the selected pay period date
            bool isSaved = DatabaseHelper.SaveSalaryRecord(employeeId, basicSalary, allowances, deductions, selectedPayPeriod);

            if (isSaved)
            {
                DatabaseHelper.LogActivity(
                    txtEmpName.Text.Trim(),
                    "Salary Created",
                    $"Salary issued for {selectedPayPeriod:MMMM yyyy}"
                );

                MessageBox.Show("Salary record created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshSalaryGrid();
                LoadDashboardMetrics();
                ClearFields();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmpID.Text)) return;

            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Employees WHERE EmployeeID = @ID";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", txtEmpID.Text.Trim());
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtEmpName.Text = reader["FullName"].ToString();

                            // Safely display position
                            SetPositionText(reader["Position"].ToString());

                            LoadSalaryRecords(txtEmpID.Text.Trim()); // Filter grid view
                        }
                        else
                        {
                            MessageBox.Show("Employee not found! Go to 'Employees' to register them.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1. Ensure an Employee ID is present
            string employeeId = txtEmpID.Text.Trim();

            if (string.IsNullOrEmpty(employeeId))
            {
                MessageBox.Show("Please select or enter an Employee ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Parse numeric salary inputs safely
            string rawBasic = txtBasicSalary.Text.Replace("P", "").Replace("₱", "").Trim();
            string rawAllowances = txtAllowances.Text.Replace("P", "").Replace("₱", "").Trim();
            string rawDeductions = txtDeductions.Text.Replace("P", "").Replace("₱", "").Trim();

            if (!double.TryParse(rawBasic, out double basicSalary) ||
                !double.TryParse(rawAllowances, out double allowances) ||
                !double.TryParse(rawDeductions, out double deductions))
            {
                MessageBox.Show("Please enter valid numeric amounts for Basic Salary, Allowances, and Deductions.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Overwrite the salary details in the database
            bool isUpdated = DatabaseHelper.UpdateEmployeeSalaryDetails(employeeId, basicSalary, allowances, deductions);

            if (isUpdated)
            {
                DatabaseHelper.LogActivity(
                    txtEmpName.Text.Trim(),
                    "Salary Updated",
                    $"Salary details updated for ID: {employeeId}"
                );

                MessageBox.Show("Salary details overwritten successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh grid and metrics
                dgvSalaryRecords.DataSource = DatabaseHelper.GetSalaryRecords(dtpPayPeriod.Value);
                LoadDashboardMetrics();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to update salary details. Employee ID not found.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSalaryRecords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSalaryRecords.Rows[e.RowIndex];
                txtEmpID.Text = row.Cells["EmployeeID"].Value?.ToString() ?? "";
                txtEmpName.Text = row.Cells["FullName"].Value?.ToString() ?? "";

                // Safely assign position to read-only ComboBox
                SetPositionText(row.Cells["Position"].Value?.ToString() ?? "");

                txtBasicSalary.Text = row.Cells["BasicSalary"].Value?.ToString() ?? "0";
                txtAllowances.Text = row.Cells["Allowances"].Value?.ToString() ?? "0";
                txtDeductions.Text = row.Cells["Deductions"].Value?.ToString() ?? "0";
                txtNetSalary.Text = row.Cells["NetSalary"].Value?.ToString() ?? "0";

                txtBasicSalary.ForeColor = Color.Black;
            }
        }

        private void dtpPayPeriod_ValueChanged(object sender, EventArgs e)
        {
            EnsurePayPeriodIsFifteenth();
        }

        private void dtpPayPeriod_CloseUp(object sender, EventArgs e)
        {
            EnsurePayPeriodIsFifteenth();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string selectedMonth = dtpPayPeriod.Value.ToString("MMMM");
            string selectedYear = dtpPayPeriod.Value.Year.ToString();

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to clear all salary records for {selectedMonth} {selectedYear}?",
                "Confirm Monthly Salary Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                // 1. Delete matching records from database
                int deletedCount = DatabaseHelper.ClearSalaryRecordsForMonth(dtpPayPeriod.Value);

                // 2. Log activity
                DatabaseHelper.LogActivity(
                    "System",
                    "Salary Cleared",
                    $"Cleared {deletedCount} record(s) for {selectedMonth} {selectedYear}"
                );

                // 3. Reset input textboxes
                ClearFields();

                // 4. Force DataGridView and Dashboard metrics to refresh
                RefreshSalaryGrid();
                LoadDashboardMetrics();

                MessageBox.Show(
                    $"Cleared {deletedCount} record(s) for {selectedMonth} {selectedYear}!",
                    "Cleared",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void ClearFields()
        {
            txtEmpID.Clear();
            txtEmpName.Clear();
            cmbPosition.SelectedIndex = -1;
            txtBasicSalary.Text = "0.00"; txtBasicSalary.ForeColor = Color.Gray;
            txtAllowances.Text = "0.00"; txtAllowances.ForeColor = Color.Gray;
            txtDeductions.Text = "0.00"; txtDeductions.ForeColor = Color.Gray;
            txtNetSalary.Text = "0.00"; txtNetSalary.ForeColor = Color.Gray;

            // Set payment date to 15th of selected period
            SetPaymentDateToFifteenth();
        }

        private void Salary1_Load(object sender, EventArgs e)
        {
            // Initial load logic if required
        }

        private void txtBasicSalary_TextChanged(object sender, EventArgs e)
        {
            CalculateNetPayOnTheFly();
        }

        private void txtAllowances_TextChanged(object sender, EventArgs e)
        {
            CalculateNetPayOnTheFly();
        }

        private void txtDeductions_TextChanged(object sender, EventArgs e)
        {
            CalculateNetPayOnTheFly();
        }

        private void CalculateNetPayOnTheFly()
        {
            // Clean currency characters before parsing
            string rawBasic = txtBasicSalary.Text.Replace("P", "").Replace("₱", "").Trim();
            string rawAllowances = txtAllowances.Text.Replace("P", "").Replace("₱", "").Trim();
            string rawDeductions = txtDeductions.Text.Replace("P", "").Replace("₱", "").Trim();

            decimal.TryParse(rawBasic, out decimal basicSalary);
            decimal.TryParse(rawAllowances, out decimal allowances);
            decimal.TryParse(rawDeductions, out decimal deductions);

            // Business Logic
            decimal grossPay = basicSalary + allowances;
            decimal netSalary = grossPay - deductions;

            // Display the calculation in your Net Salary textbox
            txtNetSalary.Text = netSalary.ToString("N2");
        }

        public void UpdateSalaryDashboardMetrics()
        {
            LoadDashboardMetrics();
        }

        private void LoadDashboardMetrics()
        {
            try
            {
                DataTable dt = DatabaseHelper.GetDashboardMetrics(dtpPayPeriod.Value);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    lblTotalEmployees.Text = row["TotalEmployees"].ToString();
                    lblGrossPay.Text = string.Format("₱{0:N2}", Convert.ToDouble(row["TotalGross"]));
                    lblDeduction.Text = string.Format("₱{0:N2}", Convert.ToDouble(row["TotalDeductions"]));
                    lblNetPay.Text = string.Format("₱{0:N2}", Convert.ToDouble(row["TotalNet"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load salary dashboard metrics: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEmpID_TextChanged_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmpID.Text))
            {
                RefreshSalaryGrid();
            }
        }

        private void btnDeleteSalaryRecord_Click(object sender, EventArgs e)
        {
            string employeeId = txtEmpID.Text.Trim();

            if (string.IsNullOrWhiteSpace(employeeId))
            {
                MessageBox.Show("Please select a salary record from the table to delete.",
                                "No Selection",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            string empName = txtEmpName.Text.Trim();
            DateTime payPeriod = dtpPayPeriod.Value;

            // 2. Confirm single-record deletion with the user
            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to delete the salary record for '{empName}' (ID: {employeeId}) for {payPeriod:MMMM yyyy}?\n\nThis will permanently remove this specific record.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                // 3. Delete only this specific employee's salary record
                bool deleted = DatabaseHelper.DeleteSalaryRecordByEmployeeId(employeeId);

                if (deleted)
                {
                    // 4. Log the deletion event in Activity Logs
                    DatabaseHelper.LogActivity(
                        string.IsNullOrWhiteSpace(empName) ? $"Emp ID {employeeId}" : empName,
                        "Salary Deleted",
                        $"Deleted salary record for pay period {payPeriod:MMMM yyyy}"
                    );

                    MessageBox.Show("Salary record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 5. Update UI, recalculate cards, and reset input controls
                    RefreshSalaryGrid();
                    LoadDashboardMetrics();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Could not find a salary record to delete for this Employee ID.",
                                    "Delete Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }
    }
}