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
        ETSManager myManager;
        public string storedPrizeID; 
        public ETSTelethon(ETSManager manager)
        {
            InitializeComponent();
            myManager = manager;
            rbSimple.Checked = true;
        }

        #region sponsor
        private void btnAddSponsor_Click(object sender, EventArgs e)
        {
            var result = myManager.AddSponsor(txtSponsorFN.Text.Trim(), txtSponsorLN.Text.Trim(), txtSponsorID.Text.Trim(), "0.0");
            MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnViewSponsors_Click(object sender, EventArgs e)
        {
            rtbxList.Clear();
            rtbxList.Text = myManager.ListSponsors();
        }

        #endregion

        #region prizes

        private void btnAddPrize_Click(object sender, EventArgs e)
        {
            var message = myManager.AddPrize(txtPrizeID.Text, txtPrizeDescription.Text, txtPrizeVpP.Text,
                txtPrizeMDL.Text, txtPrizeHM.Text, txtPrizeHM.Text, txtSponsorID.Text);
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnViewPrizes_Click(object sender, EventArgs e)
        {
            rtbxList.Clear();
            rtbxList.Text = myManager.ListPrizes();
        }

        #endregion

        #region Donation

        private void btnAddDonation_Click(object sender, EventArgs e)
        {
            var donorExist = myManager.DonorAlreadyExist(txtDonorID.Text);

            var flag = myManager.RecordDonation(txtAwardPrizeID.Text, txtAwardPrizeNumber.Text, txtDonorID.Text, txtDonationAmount.Text, txtDonationID.Text);

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
                myManager.HandleUserError(txtPrizeID.Text, txtDonationID.Text, txtDonationAmount.Text);
                return;
            }

            var message = myManager.AddDonor(txtDonorFN.Text, txtDonorLN.Text, txtDonorID.Text, txtDonorAddress.Text, txtDonorPhone.Text, 
                cardtype, txtCreditCardNumber.Text, txtCreditCardExpiry.Text);

            if (message.Item2 == false)
            {
                MessageBox.Show(message.Item1, "Error");
                myManager.HandleUserError(txtPrizeID.Text, txtDonationID.Text, txtDonationAmount.Text);
                return;
            }

            MessageBox.Show("the donation and donor were successfully recorded");
        }

        private void btnListDonations_Click(object sender, EventArgs e)
        {
            rtbxList.Clear();
            rtbxList.Text = myManager.ListDonations();
        }

        #endregion

        #region Donor
        private void btnSaveDonorInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnListDonors_Click(object sender, EventArgs e)
        {
            rtbxList.Clear();
            rtbxList.Text = myManager.ListDonors();
        }

        #endregion

        #region Close
        private void ETSTelethon_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure that you want to close the form?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                Environment.Exit(0);

            e.Cancel = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btnShowPrizes_Click(object sender, EventArgs e)
        {
            var test = new ShowPrizes(this, myManager, txtDonationAmount.Text);
            test.ShowDialog();
        }

        private void btnSaveSponors_Click(object sender, EventArgs e)
        {
            myManager.SaveSponsors();
        }

        private void btnSavePrizes_Click(object sender, EventArgs e)
        {
            myManager.SavePrizes();
        }

        private void btnSaveDonors_Click(object sender, EventArgs e)
        {
            myManager.SaveDonors();
        }

        private void btnSaveDonations_Click(object sender, EventArgs e)
        {
            myManager.SaveDonations();
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
                myManager.SavePrizes();
                myManager.SaveSponsors();
                myManager.SaveDonors();
                myManager.SaveDonations();
            }
            else
            {
                if (cbSaveDonation.Checked)
                    myManager.SaveDonations();

                if (cbSaveDonors.Checked)
                    myManager.SaveDonors();

                if (cbSavePrizes.Checked)
                    myManager.SavePrizes();

                if (cbSaveSponsor.Checked)
                    myManager.SaveSponsors();
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
    }
}
