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
            // 🔒 Limits the check: Must be up to 11 digits and contain only numbers
            return phone.Length <= 11 && phone.All(char.IsDigit);
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
            // 1. Wipe the sign-up fields clean so they are fresh if the user comes back
            ClearForm();

            // 2. Open the auto-closing popup timer (3 seconds)
            using (Form redirectPopup = new Form())
            {
                redirectPopup.Text = "Redirecting";
                redirectPopup.Size = new Size(350, 150);
                redirectPopup.FormBorderStyle = FormBorderStyle.FixedDialog;
                redirectPopup.StartPosition = FormStartPosition.CenterParent;
                redirectPopup.ControlBox = false; // Removes close, min, max buttons
                redirectPopup.BackColor = Color.White;

                Label msgLabel = new Label();
                msgLabel.Text = "Redirecting to Login page in 3 seconds...";
                msgLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                msgLabel.TextAlign = ContentAlignment.MiddleCenter;
                msgLabel.Dock = DockStyle.Fill;
                redirectPopup.Controls.Add(msgLabel);

                int secondsRemaining = 3;

                // 🔒 Explicitly using System.Windows.Forms.Timer to avoid the CS0104 ambiguity error!
                System.Windows.Forms.Timer countdownTimer = new System.Windows.Forms.Timer();
                countdownTimer.Interval = 1000; // Ticks every 1 second

                countdownTimer.Tick += (s, args) =>
                {
                    secondsRemaining--;
                    if (secondsRemaining > 0)
                    {
                        msgLabel.Text = $"Redirecting to Login page in {secondsRemaining} seconds...";
                    }
                    else
                    {
                        countdownTimer.Stop();
                        redirectPopup.Close(); // Safely closes the popup on completion
                    }
                };

                countdownTimer.Start();
                redirectPopup.ShowDialog(this); // Opens popup centered over Sign Up form
                countdownTimer.Dispose();
            }

            // 3. Once the popup automatically closes, hide Sign Up and show Login!
            this.Hide();
            Form1 loginForm = new Form1();
            loginForm.Show();
        }
        

        private void btnCreateAccount_Click_1(object sender, EventArgs e)
        {
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

            // 🔒 Updated Phone number prompt and checks to match your rules!
            if (!IsValidPhoneNumber(contactNumber) || contactNumber.Length > 11)
            {
                MessageBox.Show("Please enter a valid phone number up to only 11 digits.", "Validation Error",
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
                MessageBox.Show("Username must be at least 5 characters.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // ==================== SAVE TO DATABASE ====================
            string query = "INSERT INTO Users (FullName, Email, Username, ContactNumber, Password) VALUES (@FullName, @Email, @Username, @Contact, @Password)";

            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Contact", contactNumber);
                    cmd.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery(); // This executes the insert!

                        // Clear all fields
                        ClearForm();

                        // ⏱️ CREATE AUTO-CLOSING POPUP TIMER (3 Seconds)
                        using (Form redirectPopup = new Form())
                        {
                            redirectPopup.Text = "Success";
                            redirectPopup.Size = new Size(350, 150);
                            redirectPopup.FormBorderStyle = FormBorderStyle.FixedDialog;
                            redirectPopup.StartPosition = FormStartPosition.CenterParent;
                            redirectPopup.ControlBox = false; // Removes close, min, max buttons
                            redirectPopup.BackColor = Color.White;

                            Label msgLabel = new Label();
                            msgLabel.Text = "✔️ Account created successfully!\r\n\r\nRedirecting to Login page in 3 seconds...";
                            msgLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                            msgLabel.TextAlign = ContentAlignment.MiddleCenter;
                            msgLabel.Dock = DockStyle.Fill;
                            redirectPopup.Controls.Add(msgLabel);

                            int secondsRemaining = 3;
                            System.Windows.Forms.Timer countdownTimer = new System.Windows.Forms.Timer();
                            countdownTimer.Interval = 1000; // Ticks every 1 second

                            countdownTimer.Tick += (s, args) =>
                            {
                                secondsRemaining--;
                                if (secondsRemaining > 0)
                                {
                                    msgLabel.Text = $"✔️ Account created successfully!\r\n\r\nRedirecting to Login page in {secondsRemaining} seconds...";
                                }
                                else
                                {
                                    countdownTimer.Stop();
                                    redirectPopup.Close(); // Safely closes the popup on completion
                                }
                            };

                            countdownTimer.Start();
                            redirectPopup.ShowDialog(this); // Opens popup centered over Sign Up form
                            countdownTimer.Dispose();
                        }

                        // Redirect to Login form once the popup closes
                        Form1 loginForm = new Form1();
                        loginForm.Show();
                        this.Hide(); // Closes/Hides the signup screen
                    }
                    catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint)
                    {
                        // This will catch the error if someone tries to sign up with a username that already exists
                        MessageBox.Show("This username is already taken. Please choose another one.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUsername.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving to database: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtContactNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 🔒 Blocks typing any keys that are NOT backspace/control or digit characters!
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Tells Windows to ignore the keypress completely
            }
        }
    }
}
