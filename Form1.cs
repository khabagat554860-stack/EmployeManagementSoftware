using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using System.Data.SQLite;

namespace EmployeManagementSoftware
{
    public partial class Form1 : Form
    {
        // 💡 We removed the hardcoded credentials string here because we are checking the DB now!

        public Form1()
        {
            InitializeComponent();
        }

        // Keep your exact validation method!
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
                string usernameInput = txtusername.Text.Trim();
                string passwordInput = txtpassword.Text;

                // 2. Query to search the SQLite database we built
                string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND Password = @Password";

                using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.ConnectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Safely bind the input textboxes to the query
                        cmd.Parameters.AddWithValue("@Username", usernameInput);
                        cmd.Parameters.AddWithValue("@Password", passwordInput);

                        try
                        {
                            conn.Open();
                            int count = Convert.ToInt32(cmd.ExecuteScalar());

                            // If a row is found, the credentials match!
                            if (count == 1)
                            {
                                MessageBox.Show("Login Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // 🚀 Open your main application interface!
                                MainInterface mainInterface = new MainInterface();
                                mainInterface.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid, Please Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
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
            txtpassword.PasswordChar = checkshowpassword.Checked ? '\0' : '*';
        }
    }
}
