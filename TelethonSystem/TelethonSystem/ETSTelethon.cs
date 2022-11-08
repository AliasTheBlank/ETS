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
    public partial class ETSTelethon : Form
    {
        private ETSManager _myManager;

        private string _activeUser;
        public string storedPrizeID; 

        public ETSTelethon(ETSManager manager, bool create, bool delete, bool manage, string activeUser)
        {
            InitializeComponent();
            _myManager = manager;
            rbSimple.Checked = true;
            _activeUser = activeUser;

            if (!create)
            {
                tabcontrol1.TabPages.Remove(DonorsTab);
                tabcontrol1.TabPages.Remove(SponsorTab);
            }

            if (!delete)
                tabcontrol1.TabPages.Remove(Data);

            if (!manage)
                tabcontrol1.TabPages.Remove(Users);

            LoadGridView();
        }

        #region sponsor
        private void btnAddSponsor_Click(object sender, EventArgs e)
        {
            var result = _myManager.AddSponsor(txtSponsorFN.Text.Trim(), txtSponsorLN.Text.Trim(), txtSponsorID.Text.Trim(), "0.0");
            MessageBox.Show(result.Item1, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (result.Item2)
            {
                txtSponsorFN.Text = "";
                txtSponsorLN.Text = "";
                txtSponsorID.Text = "";
            }
        }

        private void btnViewSponsors_Click(object sender, EventArgs e)
        {
            rtbxList.Clear();
            rtbxList.Text = _myManager.ListSponsors();
        }

        #endregion

        #region prizes

        private void btnAddPrize_Click(object sender, EventArgs e)
        {
            var message = _myManager.AddPrize(txtPrizeID.Text, txtPrizeDescription.Text, txtPrizeVpP.Text,
                txtPrizeMDL.Text, txtPrizeHM.Text, txtPrizeHM.Text, txtSponsorID.Text);
            MessageBox.Show(message.Item1, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (message.Item2)
            {
                txtPrizeID.Text = "";
                txtPrizeDescription.Text = "";
                txtPrizeVpP.Text = "";
                txtPrizeMDL.Text = "";
                txtPrizeHM.Text = "";
                txtPrizeHM.Text = "";
                txtSponsorID.Text = "";
            }
        }

        private void btnViewPrizes_Click(object sender, EventArgs e)
        {
            rtbxList.Clear();
            rtbxList.Text = _myManager.ListPrizes();
        }

        #endregion

        #region Donation

        private void btnAddDonation_Click(object sender, EventArgs e)
        {
            var donorExist = _myManager.DonorAlreadyExist(txtDonorID.Text);

            var flag = _myManager.RecordDonation(txtAwardPrizeID.Text, txtAwardPrizeNumber.Text, txtDonorID.Text, txtDonationAmount.Text, txtDonationID.Text);

            if (!flag.Item1)
            {
                MessageBox.Show(flag.Item2);
                return;
            }

            if (donorExist)
            {
                MessageBox.Show("The donation has been recorded");
                return;
            }

            char cardtype = '\0';

            if (rbAMEX.Checked)
                cardtype = 'A';
            else if (rbVisa.Checked)
                cardtype = 'V';
            else if (rbMC.Checked)
                cardtype = 'M';
            else
            {
                MessageBox.Show("Please select a card type");
                _myManager.HandleUserError(txtPrizeID.Text, txtDonationID.Text, txtDonationAmount.Text);
                return;
            }

            var message = _myManager.AddDonor(txtDonorFN.Text, txtDonorLN.Text, txtDonorID.Text, txtDonorAddress.Text, txtDonorPhone.Text, 
                cardtype, txtCreditCardNumber.Text, txtCreditCardExpiry.Text);

            if (message.Item2 == false)
            {
                MessageBox.Show(message.Item1, "Error");
                _myManager.HandleUserError(txtPrizeID.Text, txtDonationID.Text, txtDonationAmount.Text);
                return;
            }

            txtAwardPrizeID.Text = "";
            txtAwardPrizeNumber.Text = "";
            txtDonorID.Text = "";
            txtDonationAmount.Text = "";
            txtDonationID.Text = "";

            txtDonorFN.Text = "";
            txtDonorLN.Text = "";
            txtDonorAddress.Text = "";
            txtDonorPhone.Text = "";
            txtCreditCardNumber.Text = "";
            txtCreditCardExpiry.Text = "";

            rbAMEX.Checked = false;
            rbVisa.Checked = false;
            rbMC.Checked = false;

            MessageBox.Show("the donation and donor were successfully recorded");
        }

        private void btnListDonations_Click(object sender, EventArgs e)
        {
            rtbxList.Clear();
            rtbxList.Text = _myManager.ListDonations();
        }

        #endregion

        #region Donor
        private void btnSaveDonorInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnListDonors_Click(object sender, EventArgs e)
        {
            rtbxList.Clear();
            rtbxList.Text = _myManager.ListDonors();
        }

        #endregion

        #region Close
        private void ETSTelethon_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure that you want to close the form?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.ExitThread();
            }

            e.Cancel = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btnShowPrizes_Click(object sender, EventArgs e)
        {
            var test = new ShowPrizes(this, _myManager, txtDonationAmount.Text);
            test.ShowDialog();
        }

        private void btnSaveSponors_Click(object sender, EventArgs e)
        {
            _myManager.SaveSponsors();
        }

        private void btnSavePrizes_Click(object sender, EventArgs e)
        {
            _myManager.SavePrizes();
        }

        private void btnSaveDonors_Click(object sender, EventArgs e)
        {
            _myManager.SaveDonors();
        }

        private void btnSaveDonations_Click(object sender, EventArgs e)
        {
            _myManager.SaveDonations();
        }

        private void rbEspecific_CheckedChanged(object sender, EventArgs e)
        {
            btnSaveDonations.Visible = rbSimple.Checked;
            btnSaveDonors.Visible = rbSimple.Checked;
            btnSaveSponors.Visible = rbSimple.Checked;
            btnSavePrizes.Visible = rbSimple.Checked;

            cbSaveDonation.Visible = rbEspecific.Checked;
            cbSaveDonors.Visible = rbEspecific.Checked;
            cbSavePrizes.Visible = rbEspecific.Checked;
            cbSaveSponsor.Visible = rbEspecific.Checked;

            btnSaveAll.Text = "Save";
        }

        private void rbSimple_CheckedChanged(object sender, EventArgs e)
        {
            btnSaveDonations.Visible = rbSimple.Checked;
            btnSaveDonors.Visible = rbSimple.Checked;
            btnSaveSponors.Visible = rbSimple.Checked;
            btnSavePrizes.Visible = rbSimple.Checked;

            cbSaveDonation.Visible = rbEspecific.Checked;
            cbSaveDonors.Visible = rbEspecific.Checked;
            cbSavePrizes.Visible = rbEspecific.Checked;
            cbSaveSponsor.Visible = rbEspecific.Checked;

            btnSaveAll.Text = "Save All";
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            if (btnSaveAll.Text == "Save All")
            {
                _myManager.SavePrizes();
                _myManager.SaveSponsors();
                _myManager.SaveDonors();
                _myManager.SaveDonations();
            }
            else
            {
                if (cbSaveDonation.Checked)
                    _myManager.SaveDonations();

                if (cbSaveDonors.Checked)
                    _myManager.SaveDonors();

                if (cbSavePrizes.Checked)
                    _myManager.SavePrizes();

                if (cbSaveSponsor.Checked)
                    _myManager.SaveSponsors();
            }
        }

        private void txtCreditCardExpiry_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool flags = true;
            if (txtCreditCardExpiry.Text.Length == 5)
            {
                e.Handled = false;
                flags = false;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && flags == true)
            {
                e.Handled = true;
            }
            if (txtCreditCardExpiry.Text.Length == 2 && e.KeyChar != (char)Keys.Back)
            {
                txtCreditCardExpiry.AppendText("/");
            }
        }

        private void txtDonationAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        public void InsertPrizeID(string prizeID)
        {
            txtAwardPrizeID.Text = prizeID;
        }

        private void rbDonation_CheckedChanged(object sender, EventArgs e)
        {
            DisplayDonations();
        }

        private void DisplayDonations()
        {
            lvData.Items.Clear();
            var text = _myManager.ListDonations().Split('\n');

            foreach (var t in text)
                lvData.Items.Add(t);
        }

        private void rbDonor_CheckedChanged(object sender, EventArgs e)
        {
            DisplayDonors();
        }

        private void DisplayDonors()
        {
            lvData.Items.Clear();
            var text = _myManager.ListDonors().Split('\n');

            foreach (var t in text)
                lvData.Items.Add(t);
        }

        private void rbPrize_CheckedChanged(object sender, EventArgs e)
        {
            DisplayPrizes();
        }

        private void DisplayPrizes()
        {
            lvData.Items.Clear();
            var text = _myManager.ListPrizes().Split('\n');

            foreach (var t in text)
                lvData.Items.Add(t);
        }

        private void rbSponsor_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySponsors();
        }

        private void DisplaySponsors()
        {
            lvData.Items.Clear();
            var text = _myManager.ListSponsors().Split('\n');

            foreach (var t in text)
                lvData.Items.Add(t);
        }

        private void btnDataDelete_Click(object sender, EventArgs e)
        {
            if (rbSponsor.Checked == false && rbPrize.Checked == false && rbDonation.Checked == false && rbDonor.Checked == false)
            {
                MessageBox.Show("Please select a category");
            }

            if (lvData.Focused == true)
            {
                MessageBox.Show("Please an item");
                return;
            }

            var id = _myManager.GetID(lvData.SelectedItems[0].Text);

            if (rbSponsor.Checked)
            {
                var message = _myManager.DeleteSponsor(id, false);

                if (message.Item2 == true) 
                {
                    MessageBox.Show(message.Item1, "Notification");
                    return;
                }


                var result = MessageBox.Show(message.Item1, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return;

                message = _myManager.DeleteSponsor(id, true);
                MessageBox.Show(message.Item1, "Notification");
                DisplaySponsors();
            }

            else if (rbDonation.Checked)
            {
                var message = _myManager.DeleteDonation(id, false);

                if (message.Item2 == true)
                {
                    MessageBox.Show(message.Item1, "Notification");
                    return;
                }


                var result = MessageBox.Show(message.Item1, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return;

                message = _myManager.DeleteDonation(id, true);
                MessageBox.Show(message.Item1, "Notification");
                DisplayDonations();
            }

            else if (rbDonor.Checked)
            {
                var message = _myManager.DeleteDonor(id, false);

                if (message.Item2 == true)
                {
                    MessageBox.Show(message.Item1, "Notification");
                    return;
                }


                var result = MessageBox.Show(message.Item1, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return;

                message = _myManager.DeleteDonor(id, true);
                MessageBox.Show(message.Item1, "Notification");
                DisplayDonors();
            }

            else if (rbPrize.Checked)
            {

                var message = _myManager.DeletePrize(id);
                MessageBox.Show(message, "Notification");
                DisplayPrizes();
                return;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var message = _myManager.CreateUser(txtUsername.Text.Trim(), txtPassword.Text.Trim(), cbCreate.Checked, cbDelete.Checked, cbManage.Checked);
            MessageBox.Show(message);
            LoadGridView();
        }

        private void LoadGridView()
        {
            listView1.Items.Clear();
            var users = _myManager.UsersString();

            foreach (var user in users)
            {
                var temp = user.Split(',');
                var itm = new ListViewItem(temp);
                listView1.Items.Add(itm); 
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var message = _myManager.EditUser(txtUsername.Text.Trim(), txtPassword.Text.Trim(), cbCreate.Checked, cbDelete.Checked, cbManage.Checked);
            MessageBox.Show(message);
            LoadGridView();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUsername.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtPassword.Text = listView1.SelectedItems[0].SubItems[1].Text;

            cbCreate.Checked = Convert.ToBoolean(listView1.SelectedItems[0].SubItems[2].Text);
            cbDelete.Checked = Convert.ToBoolean(listView1.SelectedItems[0].SubItems[3].Text);
            cbManage.Checked = Convert.ToBoolean(listView1.SelectedItems[0].SubItems[4].Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var message = _myManager.DeleteUser(txtUsername.Text.Trim());
            MessageBox.Show(message);
            LoadGridView();
        }

        private void btnSaveUsers_Click(object sender, EventArgs e)
        {
            _myManager.SaveUsers();
            MessageBox.Show("Users successfully saved");
        }
    }
}
