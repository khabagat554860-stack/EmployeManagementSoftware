namespace EmployeManagementSoftware
{
    public partial class MainInterface : Form
    {
        public MainInterface()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void MainInterface_Load(object sender, EventArgs e)
        {
            SetActiveMenu(panel1, lblDashboard);
            lblDashboard_Click(sender, e);
        }

        private void lblDashboard_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();

            SetActiveMenu(panel1, lblDashboard);
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

            SetActiveMenu(panel4, lblEmployees);
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

        private void lblSalary_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();

            SetActiveMenu(panel2, lblSalary);
            Salary1 salaryForm = new Salary1();

            salaryForm.TopLevel = false;

            salaryForm.FormBorderStyle = FormBorderStyle.None;
            salaryForm.Dock = DockStyle.Fill;

            mainPanel.Controls.Add(salaryForm);
            salaryForm.Show();
        }

        private void SetActiveMenu(Panel activePanel, Label activeLabel)
        {
            Color inactiveText = Color.Navy;
            Color inactiveBg = Color.Transparent;

            // 2. Define active (highlighted) colors
            Color activeText = Color.White;
            Color activeBg = Color.FromArgb(10, 25, 100); // Dark Navy Blue highlight

            // 3. Reset ALL Panels & Labels back to default
            panel1.BackColor = inactiveBg;
            lblDashboard.ForeColor = inactiveText;

            panel4.BackColor = inactiveBg;
            lblEmployees.ForeColor = inactiveText;

            panel2.BackColor = inactiveBg;
            lblSalary.ForeColor = inactiveText;

            // 4. Highlight the selected Panel & Label
            if (activePanel != null) activePanel.BackColor = activeBg;
            if (activeLabel != null) activeLabel.ForeColor = activeText;
        }
    }
}
