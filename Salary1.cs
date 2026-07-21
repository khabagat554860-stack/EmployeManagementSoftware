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

        public Salary1()
        {
            InitializeComponent();

            cmbPosition.Enabled = false;

            // Force-wire the event handler directly in code
            dtpPayPeriod.ValueChanged -= dtpPayPeriod_ValueChanged; // avoid duplicate binding
            dtpPayPeriod.ValueChanged += dtpPayPeriod_ValueChanged;

            // Load initial data for selected month
            RefreshSalaryGrid();
            LoadDashboardMetrics();
        }

        private void RefreshSalaryGrid()
        {

            DataTable dt = DatabaseHelper.GetSalaryRecords(dtpPayPeriod.Value);

            dgvSalaryRecords.DataSource = dt;
            lblTotalRecords.Text = dt.Rows.Count.ToString();

        }

        private void InitializePlaceholders()
        {

            throw new NotImplementedException();

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
                string selectedMonth = dtpPayPeriod.Value.ToString("MMMM"); // Dynamic matching for text e.g., "July"

                // Update date text sub-labels dynamically
                lblGrossMonth.Text = $"For {selectedMonth} {selectedYear}";
                lblDeductionMonth.Text = $"For {selectedMonth} {selectedYear}";
                lblNetMonth.Text = $"For {selectedMonth} {selectedYear}";

                // 1. Total Unique Employees Count
                string empQuery = "SELECT COUNT(*) FROM Employees";
                using (SQLiteCommand cmd = new SQLiteCommand(empQuery, conn))
                {
                    lblTotalEmployees.Text = cmd.ExecuteScalar().ToString();
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
                MessageBox.Show("Salary record created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshSalaryGrid();
                LoadDashboardMetrics();

                // Clear input fields
                txtEmpID.Clear();
                txtEmpName.Clear();
                txtBasicSalary.Clear();
                txtAllowances.Clear();
                txtDeductions.Clear();
                txtNetSalary.Clear();
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
                            cmbPosition.Text = reader["Position"].ToString();

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

        private void LoadGridData(object text)
        {
            throw new NotImplementedException();
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

            // 2. Parse numeric salary inputs safely (removes 'P' or currency symbols if present)
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
                MessageBox.Show("Salary details overwritten successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 4. Refresh the grid with SALARY columns
                dgvSalaryRecords.DataSource = DatabaseHelper.GetSalaryRecords(dtpPayPeriod.Value);

                // 5. Refresh the top 4 metric cards instantly!
                LoadDashboardMetrics();
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
                txtEmpID.Text = row.Cells["EmployeeID"].Value.ToString();
                txtEmpName.Text = row.Cells["FullName"].Value?.ToString() ?? "";
                cmbPosition.Text = row.Cells["Position"].Value.ToString();
                txtBasicSalary.Text = row.Cells["BasicSalary"].Value.ToString();
                txtAllowances.Text = row.Cells["Allowances"].Value.ToString();
                txtDeductions.Text = row.Cells["Deductions"].Value.ToString();
                txtNetSalary.Text = row.Cells["NetSalary"].Value.ToString();
                txtBasicSalary.ForeColor = Color.Black;
            }
        }

        private void dtpPayPeriod_ValueChanged(object sender, EventArgs e)
        {
            UpdateDashboard();
        }

       

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Are you sure you want to clear all salary figures and records?",
        "Confirm Salary Clear",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning
    );

            if (result == DialogResult.Yes)
            {
                // 1. Clear salary data (leaves employee profiles intact)
                DatabaseHelper.ClearAllSalaryRecords();

                // 2. Refresh DataGridView
                RefreshSalaryGrid();

                // 3. Reload dashboard cards (Preserves Total Employees, resets salary cards to ₱0.00)
                LoadDashboardMetrics();

                // 4. Clear input fields on the left
                txtEmpID.Clear();
                txtEmpName.Clear();
                txtBasicSalary.Clear();
                txtAllowances.Clear();
                txtDeductions.Clear();
                txtNetSalary.Clear();

                MessageBox.Show("Salary details cleared successfully! Total employee count retained.", "Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            dtpPaymentDate.Value = DateTime.Now;
        }

        private void Salary1_Load(object sender, EventArgs e)
        {

        }

        private void txtBasicSalary_TextChanged(object sender, EventArgs e)
        {
            CalculateNetPayOnTheFly();
        }

        private void CalculateNetPayOnTheFly()
        {
            // Safely parse the text inputs, defaulting to 0 if empty or invalid
            decimal.TryParse(txtBasicSalary.Text, out decimal basicSalary);
            decimal.TryParse(txtAllowances.Text, out decimal allowances);
            decimal.TryParse(txtDeductions.Text, out decimal deductions);

            // Business Logic
            decimal grossPay = basicSalary + allowances;
            decimal netSalary = grossPay - deductions;

            // Display the calculation in your Net Salary textbox (formatted to 2 decimal places)
            txtNetSalary.Text = netSalary.ToString("N2");
        }

        private void txtAllowances_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDeductions_TextChanged(object sender, EventArgs e)
        {

        }

        public void UpdateSalaryDashboardMetrics()
        {
            try
            {
                // 1. Fetch the combined payroll data from our DatabaseHelper
                System.Data.DataTable dt = DatabaseHelper.GetDashboardMetrics(dtpPayPeriod.Value);

                if (dt.Rows.Count > 0)
                {
                    System.Data.DataRow row = dt.Rows[0];

                    // 2. Assign the database values directly to the labels at the top of your screen
                    // Double-check your Salary1.cs [Design] tab to make sure these match your exact Label names!
                    lblTotalEmployees.Text = row["TotalEmployees"].ToString();
                    lblGrossPay.Text = "₱" + Convert.ToDecimal(row["TotalGross"]).ToString("#,##0.00");
                    lblDeduction.Text = "₱" + Convert.ToDecimal(row["TotalDeductions"]).ToString("#,##0.00");
                    lblNetPay.Text = "₱" + Convert.ToDecimal(row["TotalNet"]).ToString("#,##0.00");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load salary dashboard metrics: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDashboardMetrics()
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
       
        

        private void txtEmpID_TextChanged_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmpID.Text))
            {
                RefreshSalaryGrid();
            }
        }
    }
}
