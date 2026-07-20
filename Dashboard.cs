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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadEmployees();
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
