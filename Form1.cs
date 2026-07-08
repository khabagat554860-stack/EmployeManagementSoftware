using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace EmployeManagementSoftware
{
    public partial class Form1 : Form
    {
        private string username = "Sanguenza", password = "12345";

        public Form1()
        {
            InitializeComponent();
        }

        public Boolean isvalid()
        {
            if (string.IsNullOrEmpty(txtusername.Text))
            {
                MessageBox.Show("Enter Username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            else if (string.IsNullOrEmpty(txtpassword.Text))
            {
                MessageBox.Show("Enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (isvalid())
            {
                if (txtusername.Text == username && txtpassword.Text == password)
                {
                    MessageBox.Show("Login Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainInterface mainInterface = new MainInterface();
                    mainInterface.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid, Please Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }





            }
        }

        private void linksignup_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Hide();

        }


        private void checkshowpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkshowpassword.Checked)
            {
                txtpassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtpassword.UseSystemPasswordChar = true;
            }
        }

        
    }
}
