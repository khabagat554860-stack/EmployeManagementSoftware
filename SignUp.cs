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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private bool IsValidPhoneNumber(string phone)
        {
            return phone.Length == 11 && phone.All(char.IsDigit);
        }

        private bool IsValidUsername(string username)
        {
            return username.Length >= 5;
        }
        private void ClearForm()
        {
            txtFullName.Clear();
            txtEmail.Clear();
            txtContactNumber.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirm.Clear();
            chkShowPassword.Checked = false;
            txtPassword.PasswordChar = '*';
            txtConfirm.PasswordChar = '*';
            txtFullName.Focus();
        }
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowPassword.Checked;
            txtPassword.PasswordChar = show ? '\0' : '*';
            txtConfirm.PasswordChar = show ? '\0' : '*';
        }


        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show(
              "Go to Login page?",
              "Confirm",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Redirecting to Login page...", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }



            this.Hide();


            Form1 loginForm = new Form1();
            loginForm.Show();



        }

        private void btnCreateAccount_Click_1(object sender, EventArgs e)
        {
            // Get all values
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string contactNumber = txtContactNumber.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;

            // 1. Validate Full Name
            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Full name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (fullName.Length < 2)
            {
                MessageBox.Show("Full name must be at least 2 characters.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            // 2. Validate Email
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // 3. Validate Contact Number
            if (string.IsNullOrWhiteSpace(contactNumber))
            {
                MessageBox.Show("Contact number is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactNumber.Focus();
                return;
            }

            if (!IsValidPhoneNumber(contactNumber))
            {
                MessageBox.Show("Please enter a valid 10-15 digit phone number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactNumber.Focus();
                return;
            }

            // 4. Validate Username
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (username.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (!IsValidUsername(username))
            {
                MessageBox.Show("Username can only contain letters, numbers, and underscore.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            // 5. Validate Password
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // 6. Validate Confirm Password
            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirm.Focus();
                return;
            }

            // ============ SUCCESS MESSAGE ============
            MessageBox.Show(
                $"✅ Account created successfully!\n\n" +
                $"👤 Full Name: {fullName}\n" +
                $"📧 Email: {email}\n" +
                $"📱 Contact: {contactNumber}\n" +
                $"🔑 Username: {username}",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Clear all fields
            ClearForm();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
