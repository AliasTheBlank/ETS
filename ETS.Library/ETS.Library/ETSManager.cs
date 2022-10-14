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
        #endregion

        #region Add methods
        public void AddDonor(string firstName, string lastName, string donorID, string address, 
            string phone, char cardType, string cardNumber, string cardExpiry)
        {
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

        }

        public void AddPrize(string prizeID, string description, double value, double donationLimit, 
            int originalAvailable, int currentAvailable, string sponsorID)
        {

        }

        public void AddDonation(string donationID, string donationDame, string donorID, double donationAmount, string prizeID)
        {

        }
        #endregion

        #region List method
        public string ListDonors()
        {

        }

        public string ListSponsors()
        {

        }

        public string ListDonations()
        {

        }

        public string ListQualifiedPrizes()
        {

        }
        #endregion

        public bool RecordDonation()
        {

        }

    }
}
