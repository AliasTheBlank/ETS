using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    internal class Prize
    {
        private string _prizeID;
        private string _description;
        private double _value;
        private double _donationLimit;
        private int _originalAvailable;
        private int _currentAvailable;
        private string _sponsorID;

        public Prize()
        {
            PrizeID = "";
            Description = "";
            Value = 0;
            DonationLimit = 0;
            OriginalAvailable = 0;
            CurrentAvailable = 0;
            SponsorID = "";
        }
        public Prize(string prizeID, string description, double value, double donationLimit, int originalAvailable, int currentAvailable, string sponsorID)
        {
            PrizeID = prizeID;
            Description = description;
            Value = value;
            DonationLimit = donationLimit;
            OriginalAvailable = originalAvailable;
            CurrentAvailable = currentAvailable;
            SponsorID = sponsorID;
        }

        public string PrizeID { get => _prizeID; set => _prizeID = value; }
        public string Description { get => _description; set => _description = value; }
        public double Value { get => _value; set => _value = value; }
        public double DonationLimit { get => _donationLimit; set => _donationLimit = value; }
        public int OriginalAvailable { get => _originalAvailable; set => _originalAvailable = value; }
        public int CurrentAvailable { get => _currentAvailable; set => _currentAvailable = value; }
        public string SponsorID { get => _sponsorID; set => _sponsorID = value; }

        public override string ToString()
        {
            return $"{PrizeID},{Description},{Value},{DonationLimit},{OriginalAvailable},{CurrentAvailable},{SponsorID}";
        }

        public string GetPrizeID()
        {
            return _prizeID;
        }

        public void Decrease(int timeToDecrease)
        {
            Value -= timeToDecrease;
        }

        public void OnChangePrize()
        {

        }

        public void ClearPrize()
        {

        }
    }
}
