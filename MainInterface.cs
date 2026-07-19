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
            mainPanel.Controls.Clear();


            Dashboard dashboardForm = new Dashboard();

            dashboardForm.TopLevel = false;

            dashboardForm.FormBorderStyle = FormBorderStyle.None;

            dashboardForm.Dock = DockStyle.Fill;

            mainPanel.Controls.Add(dashboardForm);
            dashboardForm.Show();
        }

        private void lblEmployees_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();


            AddEmployee employeeForm = new AddEmployee();

            employeeForm.TopLevel = false;

            employeeForm.FormBorderStyle = FormBorderStyle.None;

            employeeForm.Dock = DockStyle.Fill;

            mainPanel.Controls.Add(employeeForm);
            employeeForm.Show();
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
