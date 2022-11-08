using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    class Sponsor : Person
    {
        private string _sponsorID;
        private double _totalPrizeValue;

        public Sponsor() : base()
        {
            _sponsorID = "";
            _totalPrizeValue = 0;
        }
        public Sponsor(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public Sponsor(string firstName, string lastName, string sponsorID, double totalPrizeValue) : this(firstName, lastName)
        {
            _sponsorID = sponsorID;
            _totalPrizeValue = totalPrizeValue;
        }

        public string SponsorID { get => _sponsorID; set => _sponsorID = value; }
        public double TotalPrizeValue { get => _totalPrizeValue; set => _totalPrizeValue = value; }

        public override string ToString()
        {
            return base.ToString() + $",{SponsorID},{TotalPrizeValue}";
        }

        public string GetID()
        {
            return SponsorID;
        }

        public double AddValue(double valueToAdd)
        {
            _totalPrizeValue += valueToAdd;
            return TotalPrizeValue;
        }

        public void DeductValue(double valueToDeduct)
        {
            _totalPrizeValue -= valueToDeduct;
        }

        public override string DisplayData()
        {
            return $"SponsorID: {SponsorID}, " + base.DisplayData() + $", Total Prize Value: {TotalPrizeValue}";
        }
    }
}
