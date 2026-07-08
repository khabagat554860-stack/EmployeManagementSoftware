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

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowPassword.Checked;
            txtPassword.PasswordChar = show ? '\0' : '*';
            txtConfirm.PasswordChar = show ? '\0' : '*';
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Full name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirm.Focus();

                MessageBox.Show($"Account created successfully!\n\nFull Name: {fullName}\nEmail: {email}",
               "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtFullName.Clear();
                txtEmail.Clear();
                txtPassword.Clear();
                txtConfirm.Clear();
                chkShowPassword.Checked = false;
                txtFullName.Focus();
            }

        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtFullName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtConfirm.Clear();
            chkShowPassword.Checked = false;
            txtFullName.Focus();
        }
    }
}
