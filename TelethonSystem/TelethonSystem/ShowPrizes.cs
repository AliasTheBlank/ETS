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
    public partial class ShowPrizes : Form
    {
        private ETSTelethon _etsTelethon;
        private ETSManager _myManager;
        public ShowPrizes(ETSTelethon etsTelethon, ETSManager manager, string amount)
        {
            InitializeComponent();
            _etsTelethon = etsTelethon;
            _myManager = manager;

            if (amount == "")
                rbAllPrize.Checked = true;
            else
            {
                rbQualifiedPrizes.Checked = true;
                txtAmount.Text = amount;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var item = listView1.SelectedItems[0].Text;

             _etsTelethon.InsertPrizeID(_myManager.GetPrizeID(item));
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbAllPrize_CheckedChanged(object sender, EventArgs e)
        {
            lAmount.Visible = false;
            txtAmount.Visible = false;
            txtAmount.Text = "";

            listView1.Items.Clear();
            var text = _myManager.ListPrizes().Split('\n');

            foreach (var t in text)
                listView1.Items.Add(t);
        }

        private void rbQualifiedPrizes_CheckedChanged(object sender, EventArgs e)
        {
            lAmount.Visible = true;
            txtAmount.Visible = true;

            LoadQualifiedPrizes();
        }

        private void LoadQualifiedPrizes()
        {
            listView1.Items.Clear();
            string[] text = new string[] {""};

            if (double.TryParse(txtAmount.Text, out var amount))
                text = _myManager.ListQualifiedPrizes(amount).Split('\n');

            foreach (var t in text)
                listView1.Items.Add(t);
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            LoadQualifiedPrizes();
        }
    }
}
