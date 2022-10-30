using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ETS.Library;

namespace TelethonSystem
{
    public partial class LoginForm : Form
    {
        int tries = 0;
        ETSManager manager = new ETSManager();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                
                MessageBox.Show("Username and Password are required!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!manager.FindUser(txtUsername.Text.Trim(), txtPassword.Text.Trim()))
            {
                tries++;
                if (tries == 3)
                {
                    MessageBox.Show("Username and Password are required! System Shutdown", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

                else
                    MessageBox.Show("Invalid user! Try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
                
            this.Hide();
            var myForm = new ETSTelethon(manager);
            myForm.Visible = true;
            myForm.Activate();
            
            
        }

        // cleanup
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure that you want to close the form?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                e.Cancel = true;
        }
    }
}
