using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    internal class Donation
    {
        private string _donationID;
        private string _donationDate;
        private string _donorID;
        private double _donationAmount;
        private string _prizeID;

        public Donation()
        {
            DonationID = "";
            DonationDate = "";
            DonorID = "";
            DonationAmount = 0;
            PrizeID = "";
        }

        public Donation(string donationID, string donationDate, string donorID, double donationAmount, string prizeID)
        {
            DonationID = donationID;
            DonationDate = donationDate;
            DonorID = donorID;
            DonationAmount = donationAmount;
            PrizeID = prizeID;
        }

        public string DonationID { get => _donationID; set => _donationID = value; }
        public string DonationDate { get => _donationDate; set => _donationDate = value; }
        public string DonorID { get => _donorID; set => _donorID = value; }
        public double DonationAmount { get => _donationAmount; set => _donationAmount = value; }
        public string PrizeID { get => _prizeID; set => _prizeID = value; }

        public override string ToString()
        {
            return $"{DonationID},{DonationDate},{DonorID},{DonationAmount},{PrizeID}";
        }

        public string DisplayData()
        {
            return $"DonationID: {DonationID}, Donation Date: {DonationDate}, DonorID {DonorID}, Donation Amount: {DonationAmount}, PrizeID:{PrizeID}";
        }
    }
}
