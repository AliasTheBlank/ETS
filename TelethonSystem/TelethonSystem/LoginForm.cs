using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelethonSystem
{
    public partial class LoginForm : Form
    {
        int tries = 0;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Username and Password are required!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtUsername.Text != "ETS" && txtPassword.Text == "admin")
            {
                MessageBox.Show("Invalid user! Try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tries++;
                return;
            }

            if (tries == 3)
            {
                MessageBox.Show("Username and Password are required! System Shutdown", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
                
            this.Hide();
            var myForm = new ETSTelethon();
            myForm.Visible = true;
            myForm.Activate();
            
            
        }

        // cleanup
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
