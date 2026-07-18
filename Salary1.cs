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
        public Salary1()
        {
            InitializeComponent();
            //InitializePlaceholders(); //Commened out temporarily
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
                string query = @"SELECT s.RecordID, s.EmployeeID, e.EmployeeName, e.Position, 
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
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                conn.Open();

                string insertEmp = "INSERT OR IGNORE INTO Employees (EmployeeID, EmployeeName, Position) VALUES (@ID, @Name, @Pos)";
                using (SQLiteCommand cmd = new SQLiteCommand(insertEmp, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", txtEmpID.Text.Trim());
                    cmd.Parameters.AddWithValue("@Name", txtEmpName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Pos", txtPosition.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                string insertSalary = @"INSERT INTO SalaryRecords 
                    (EmployeeID, BasicSalary, Allowances, Deductions, NetSalary, PaymentDate, PayPeriodMonth, PayPeriodYear) 
                    VALUES (@ID, @Basic, @Allow, @Ded, @Net, @Date, @Month, @Year)";

                using (SQLiteCommand cmd = new SQLiteCommand(insertSalary, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", txtEmpID.Text.Trim());
                    cmd.Parameters.AddWithValue("@Basic", GetDecimalValue(txtBasicSalary));
                    cmd.Parameters.AddWithValue("@Allow", GetDecimalValue(txtAllowances));
                    cmd.Parameters.AddWithValue("@Ded", GetDecimalValue(txtDeductions));
                    cmd.Parameters.AddWithValue("@Net", GetDecimalValue(txtNetSalary));
                    cmd.Parameters.AddWithValue("@Date", dtpPaymentDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Month", dtpPayPeriod.Value.ToString("MMMM"));
                    cmd.Parameters.AddWithValue("@Year", dtpPayPeriod.Value.ToString("yyyy"));

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Record Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateDashboard(); // FIXED: Called the correct method name
            LoadSalaryRecords();
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
                            txtEmpName.Text = reader["EmployeeName"].ToString();
                            txtPosition.Text = reader["Position"].ToString();

                            LoadSalaryRecords(txtEmpID.Text.Trim()); // Filter grid view
                        }
                        else
                        {
                            MessageBox.Show("Employee not found! Click 'Add New' to register them.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                conn.Open();
                string updateEmp = "UPDATE Employees SET EmployeeName = @Name, Position = @Pos WHERE EmployeeID = @ID";
                using (SQLiteCommand cmd = new SQLiteCommand(updateEmp, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", txtEmpName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Pos", txtPosition.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID", txtEmpID.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Employee Info Updated!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadSalaryRecords();
            UpdateDashboard();
        }

        private void dgvSalaryRecords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSalaryRecords.Rows[e.RowIndex];
                txtEmpID.Text = row.Cells["EmployeeID"].Value.ToString();
                txtEmpName.Text = row.Cells["EmployeeName"].Value?.ToString() ??"";
                txtPosition.Text = row.Cells["Position"].Value.ToString();
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

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearFields();
            txtEmpID.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadSalaryRecords();
        }
        private void ClearFields()
        {
            txtEmpID.Clear();
            txtEmpName.Clear();
            txtPosition.Clear();
            txtBasicSalary.Text = "0.00"; txtBasicSalary.ForeColor = Color.Gray;
            txtAllowances.Text = "0.00"; txtAllowances.ForeColor = Color.Gray;
            txtDeductions.Text = "0.00"; txtDeductions.ForeColor = Color.Gray;
            txtNetSalary.Text = "0.00"; txtNetSalary.ForeColor = Color.Gray;
            dtpPaymentDate.Value = DateTime.Now;
        }
    }
}
