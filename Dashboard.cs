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
            // Initialize database tables if not already present
            DatabaseHelper.InitializeDatabase();

            // Load UI elements
            LoadEmployees();
            LoadDashboardMetrics();
            LoadRecentActivities(); // Populates flpRecentActivities panel dynamically
        }

        /// <summary>
        /// Dynamically builds activity card panels inside 'flpRecentActivities'
        /// </summary>
        public void LoadRecentActivities()
        {
            try
            {
                DataTable dt = DatabaseHelper.GetRecentActivities();

                if (flpRecentActivities == null) return;

                // Clear previous/static cards
                flpRecentActivities.Controls.Clear();

                if (dt.Rows.Count == 0)
                {
                    Label lblEmpty = new Label
                    {
                        Text = "No recent activity recorded.",
                        Font = new Font("Segoe UI", 9.5f, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(10)
                    };
                    flpRecentActivities.Controls.Add(lblEmpty);
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    string name = row["EmployeeName"].ToString();
                    string action = row["ActionType"].ToString();
                    string details = row["Details"].ToString();

                    string timeStr = "";
                    if (DateTime.TryParse(row["Timestamp"].ToString(), out DateTime parsedDate))
                    {
                        timeStr = parsedDate.ToString("MMM dd, hh:mm tt");
                    }

                    // Card Container
                    Guna.UI2.WinForms.Guna2Panel card = new Guna.UI2.WinForms.Guna2Panel
                    {
                        Size = new Size(flpRecentActivities.Width - 25, 55),
                        FillColor = Color.White,
                        BorderRadius = 8,
                        Margin = new Padding(0, 0, 0, 8)
                    };

                    // Header
                    Label lblHeader = new Label
                    {
                        Text = $"{name} • {action}",
                        Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                        ForeColor = Color.FromArgb(30, 30, 30),
                        Location = new Point(12, 8),
                        AutoSize = true
                    };

                    // Details
                    Label lblSubText = new Label
                    {
                        Text = $"{details}  |  {timeStr}",
                        Font = new Font("Segoe UI", 8.5f, FontStyle.Regular),
                        ForeColor = Color.DimGray,
                        Location = new Point(12, 28),
                        AutoSize = true
                    };

                    card.Controls.Add(lblHeader);
                    card.Controls.Add(lblSubText);
                    flpRecentActivities.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading activities: " + ex.Message);
            }
        }

        private void LoadDashboardMetrics()
        {
            try
            {
                DatabaseHelper.EnsureStatusColumnExists();

                using (var conn = new SQLiteConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();

                    // 1. Queries
                    int totalEmployees = Convert.ToInt32(new SQLiteCommand("SELECT COUNT(*) FROM Employees", conn).ExecuteScalar());

                    string activeQuery = "SELECT COUNT(*) FROM Employees WHERE Status = 'Active' OR Status IS NULL OR Status = ''";
                    int activeEmployees = Convert.ToInt32(new SQLiteCommand(activeQuery, conn).ExecuteScalar());

                    string leaveQuery = "SELECT COUNT(*) FROM Employees WHERE Status = 'On Leave'";
                    int onLeaveEmployees = Convert.ToInt32(new SQLiteCommand(leaveQuery, conn).ExecuteScalar());

                    string inactiveQuery = "SELECT COUNT(*) FROM Employees WHERE Status = 'Inactive'";
                    int inactiveEmployees = Convert.ToInt32(new SQLiteCommand(inactiveQuery, conn).ExecuteScalar());

                    string deptQuery = "SELECT COUNT(DISTINCT Position) FROM Employees WHERE Position IS NOT NULL AND Position != ''";
                    int totalDepartments = Convert.ToInt32(new SQLiteCommand(deptQuery, conn).ExecuteScalar());


                    // --- 2. UPDATE TOP CARDS ---
                    lblTotalEmployeesCount.Text = totalEmployees.ToString();
                    lblActiveEmployeesCount.Text = activeEmployees.ToString();
                    lblOnLeaveCount.Text = onLeaveEmployees.ToString();
                    lblDepartmentsCount.Text = totalDepartments.ToString();


                    // --- 3. CALCULATE TOP CARD PERCENTAGES ---
                    if (totalEmployees > 0)
                    {
                        double activePct = ((double)activeEmployees / totalEmployees) * 100;
                        double leavePct = ((double)onLeaveEmployees / totalEmployees) * 100;

                        lblActiveEmployeesPercent.Text = $"{activePct:F2}% of total";
                        lblOnLeavePercent.Text = $"{leavePct:F2}% of total";
                    }
                    else
                    {
                        lblActiveEmployeesPercent.Text = "0.00% of total";
                        lblOnLeavePercent.Text = "0.00% of total";
                    }


                    // --- 4. UPDATE BOTTOM-RIGHT STATUS PANEL ---
                    lblActiveCount.Text = activeEmployees.ToString();
                    lblOLCount.Text = onLeaveEmployees.ToString();
                    lblInactiveCount.Text = inactiveEmployees.ToString();
                    lblTotalCount.Text = totalEmployees.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating dashboard: " + ex.Message);
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
                LoadEmployees();
            }
            else
            {
                dgvEmployees.DataSource = DatabaseHelper.SearchEmployees(keyword);
            }
        }
    }
}