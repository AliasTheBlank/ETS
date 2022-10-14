using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ETS.Library
{
    public class ETSManager
    {

        private Donors _donors = new Donors();
        private Sponsors _sponsors = new Sponsors();
        private Donations _donations = new Donations();
        private Prizes _prizes = new Prizes();

        #region extra methods

        // Id verificators, return true if it find a match
        public bool DonorIDExist(string donorID)
        {
            foreach (Donor donor in _donors)
                if (donor.DonorID == donorID)
                    return true;

            return false;
        }

        public bool SponsorsIDExist(string sponsorID)
        {
            foreach (Sponsor sponsor in _sponsors)
                if (sponsor.SponsorID == sponsorID)
                    return true;

            return false;
        }

        public bool DonationIDExist(string donationID)
        {
            foreach (Donation donation in _donations)
                if (donation.DonationID == donationID)
                    return true;
            return false;
        }

        public bool PrizeIDExist(string prizeID)
        {
            foreach (Prize prize in _prizes)
                if (prize.PrizeID == prizeID)
                    return true;
            return false;
        }

        // Returns true if valid phone number format
        public bool ValidPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$");
        }

        public bool CreditOrDebit(char type)
        {
            return (type == 'D' || type == 'C');
        }

        public bool ValidDate(string date)
        {
            if (date.Length != 7)
                return false;

            var month = date.Substring(0, 1);
            var year = date.Substring(3, date.Length - 1);

            if (int.TryParse(month, out var numberOfMonts) && numberOfMonts < 1 && numberOfMonts > 12)
                return false;

            if (int.TryParse(year, out var numberOfYears) && numberOfYears < 2022)
                return false;

            return true;


        }

        public bool IDLenghtVerifier(string iD)
        {
            if (iD.Length != 4)
                return false;
            return true;
        }
        #endregion

        #region Add methods
        public void AddDonor(string firstName, string lastName, string donorID, string address, 
            string phone, char cardType, string cardNumber, string cardExpiry)
        {
            if (!IDLenghtVerifier(donorID))
                return;

            if (DonorIDExist(donorID))
                return;

            if (firstName.Length > 15 || firstName.Length > 15 || address.Length > 40)
                return;

            if (!ValidPhoneNumber(phone))
                return;

            if (!CreditOrDebit(cardType))
                return;

            if (cardNumber.Length != 16)
                return;

            if (!ValidDate(cardExpiry))
                return;

            var newDonor = new Donor(firstName, lastName, donorID, address, phone, cardType, cardNumber, cardExpiry);
            _donors.Add(newDonor);
        }

        public void AddSponsor(string firstName, string lastName, string sponsorID, double totalPrizeValue)
        {
            if (!IDLenghtVerifier(sponsorID))
                return;

            if ((firstName.Length + lastName.Length) > 30)
                return;

            if (SponsorsIDExist(sponsorID))
                return;

            if (totalPrizeValue < 0)
                return;

            var newSponsor = new Sponsor(firstName, lastName, sponsorID, totalPrizeValue);
            _sponsors.Add(newSponsor);
        }

        public void AddPrize(string prizeID, string description, double value, double donationLimit, 
            int originalAvailable, int currentAvailable, string sponsorID)
        {
            if (IDLenghtVerifier(prizeID))
                return;

            if (PrizeIDExist(prizeID))
                return;

            if (description.Length > 15)
                return;

            if (donationLimit < 0)
                return;

            if (originalAvailable < 0)
                return;

            if (currentAvailable < 0 || currentAvailable > originalAvailable)
                return;

            if (!SponsorsIDExist(sponsorID))
                return;

            _prizes.Add(new Prize(prizeID, description, value, donationLimit, originalAvailable, currentAvailable, sponsorID));
        }

        public void AddDonation(string donationID, DateTime donationDate, string donorID, double donationAmount, string prizeID)
        {
            if (!IDLenghtVerifier(donationID))
                return;

            if (DonationIDExist(donationID))
                return;

            var donationDateString = donationDate.ToString("MM/dd/yyyy h:mm tt");

            if (!DonationIDExist(donorID))
                return;

            if (donationAmount < 5 || donationAmount > 999999999)
                return;

            if (!PrizeIDExist(prizeID))
                return;

            _donations.Add(new Donation(donationID, donationDateString, donorID, donationAmount, prizeID));
        }
        #endregion

        #region List method
        public string ListDonors()
        {
            string listedDonor = "";

            foreach (Donor donor in _donors)
            {
                listedDonor += donor.DisplayData() + Environment.NewLine;
            }

            return listedDonor;
        }

        public string ListSponsors()
        {
            string listedSponsors = "";

            foreach (Sponsor sponsor in _sponsors)
            {
                listedSponsors += sponsor.DisplayData() + Environment.NewLine;
            }

            return listedSponsors;
        }

        public string ListPrizes()
        {
            string listedPrized = "";

            foreach (Prize prize in _prizes)
                listedPrized += prize.DisplayData() + Environment.NewLine;

            return listedPrized;
        }

        public string ListDonations()
        {
            string listedDonations = "";

            foreach (Donation donation in _donations)
            {
                listedDonations += donation.DisplayData() + Environment.NewLine;
            }

            return listedDonations;
        }

        public string ListQualifiedPrizes(double donationAmount)
        {
            string listedPrized = "";

            foreach (Prize prize in _prizes)
            {
                if (prize.DonationLimit <= donationAmount)
                listedPrized += prize.DisplayData() + Environment.NewLine;
            }

            return listedPrized;
        }
        #endregion

        public bool RecordDonation(string prizeID, int numberOfPrizes, string donorID, string donationAmount, string donationID)
        {
            foreach (Prize prize in _prizes)
            {
                if (prizeID == prize.GetPrizeID())
                {
                    if (numberOfPrizes <= prize.CurrentAvailable)
                    {
                        prize.Decrease(numberOfPrizes);
                        // It is supossed to call the fuction with the system date?
                        AddDonation(donationID, DateTime.Now, donorID, Convert.ToDouble(donationAmount), prizeID);
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
