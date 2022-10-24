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
        public ETSTelethon(ETSManager manager)
        {
            InitializeComponent();
            myManager = manager;
        }

        #region sponsor
        private void btnAddSponsor_Click(object sender, EventArgs e)
        {
            var result = myManager.AddSponsor(txtSponsorFN.Text.Trim(), txtSponsorLN.Text.Trim(), txtSponsorID.Text.Trim(), (double) 0.0);
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
            var message = myManager.AddPrize(txtPrizeID.Text, txtPrizeDescription.Text, Convert.ToDouble(txtPrizeVpP.Text),
                Convert.ToDouble(txtPrizeMDL.Text), Convert.ToInt16(txtPrizeHM.Text), Convert.ToInt16(txtPrizeHM.Text), txtSponsorID.Text);
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

            var flag = myManager.RecordDonation(txtPrizeID.Text, Convert.ToInt32(txtAwardPrizeNumber.Text), txtDonorID.Text, txtDonationAmount.Text, txtDonationID.Text);

            if (!flag)
            {
                MessageBox.Show("there was an error when creating the donation");
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

            var message = myManager.AddDonor(txtDonorFN.Text, txtDonorLN.Text, txtDonorID.Text, txtDonorAddress.Text, txtDonorPhone.Text, cardtype, txtCreditCardNumber.Text, txtCreditCardExpiry.Text);

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

        }

        #endregion

        #region Donor
        private void btnSaveDonorInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnListDonors_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Close
        private void ETSTelethon_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
