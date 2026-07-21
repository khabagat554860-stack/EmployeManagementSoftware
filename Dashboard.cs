using Microsoft.Data.Sqlite;
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

namespace EmployeManagementSoftware
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            using (var conn = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                conn.Open();

                // ⚡ Your exact lines 70-74 from Salary1.cs
                string empQuery = "SELECT COUNT(*) FROM Employees";
                using (SQLiteCommand cmd = new SQLiteCommand(empQuery, conn))
                {
                    string totalEmployees = cmd.ExecuteScalar().ToString();

                    // 1. Update the Top Card "Total Employees" label
                    lblTotalEmployeesCount.Text = totalEmployees;


                }
            }

            LoadEmployees();
            LoadDashboardMetrics();
        }

            private void LoadDashboardMetrics()
            {
                    using (var conn = new SQLiteConnection(DatabaseHelper.ConnectionString))
                    {
                        conn.Open();

                        // 1. Query Total Employees
                        int totalEmployees = 0;
                        string totalQuery = "SELECT COUNT(*) FROM Employees";
                        using (SQLiteCommand cmd = new SQLiteCommand(totalQuery, conn))
                        {
                            totalEmployees = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 2. Query Total Departments / Unique Positions
                        int totalDepartments = 0;
                        string deptQuery = "SELECT COUNT(DISTINCT Position) FROM Employees WHERE Position IS NOT NULL AND Position != ''";
                        using (SQLiteCommand cmd = new SQLiteCommand(deptQuery, conn))
                        {
                            totalDepartments = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 3. Fallback Status Logic (Without DB Column)
                        int activeEmployees = totalEmployees; // All employees are active
                        int onLeaveEmployees = 0;            // Default to 0
                        int inactiveEmployees = 0;           // Default to 0

                        // --- UPDATE TOP CARDS ---
                        lblTotalEmployeesCount.Text = totalEmployees.ToString();
                        lblActiveEmployeesCount.Text = activeEmployees.ToString();
                        lblOnLeaveCount.Text = onLeaveEmployees.ToString();
                        lblDepartmentsCount.Text = totalDepartments.ToString();

                        // Top Card Subtitle Percentages
                        if (totalEmployees > 0)
                        {
                            lblActiveEmployeesPercent.Text = "100.00% of total";
                            lblOnLeavePercent.Text = "0.00% of total";
                        }
                        else
                        {
                            lblActiveEmployeesPercent.Text = "0.00% of total";
                            lblOnLeavePercent.Text = "0.00% of total";
                        }

                        // --- UPDATE EMPLOYEE STATUS PANEL (Bottom Right) ---
                        lblActiveCount.Text = activeEmployees.ToString();
                        lblOLCount.Text = onLeaveEmployees.ToString();
                        lblInactiveCount.Text = inactiveEmployees.ToString();
                        lblTotalCount.Text = totalEmployees.ToString();
                    }
            }


           
        
        private void LoadEmployees()
        {
            dgvEmployees.DataSource = DatabaseHelper.GetEmployees();
        }

        private void txtRecentEmployees_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                LoadEmployees();   // Show all employees
            }
            else
            {
                dgvEmployees.DataSource = DatabaseHelper.SearchEmployees(keyword);
            }
        }
    }
}
