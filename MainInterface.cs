using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeManagementSoftware
{
    public partial class MainInterface : Form
    {
        public MainInterface()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void MainInterface_Load(object sender, EventArgs e)
        {

        }

        private void lblDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboardForm = new Dashboard();


            dashboardForm.Show();


            this.Hide();
        }

        private void lblEmployees_Click(object sender, EventArgs e)
        {

        }

        private void lblStaffroom_Click(object sender, EventArgs e)
        {

        }

        private void lblSignOut_Click(object sender, EventArgs e)
        {

            Form1 loginForm = new Form1();

            
            loginForm.Show();

            
            this.Close();
        }
    }
}
