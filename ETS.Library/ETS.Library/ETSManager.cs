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

        private List<User> myUsers = new List<User>();

        public ETSManager()
        {
            OnLoad();
        }

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

        // Obsolete
        public bool ValidDate(string date)
        {
            if (date.Length != 5)
                return false;

            var year = date.Substring(3);
            var month = date.Substring(0, 2);
            

            if (int.TryParse(month, out var numberOfMonts) && numberOfMonts < 1 && numberOfMonts > 12)
                return false;

            if (int.TryParse(year, out var numberOfYears) && numberOfYears < 2022)
                return false;

            return true;


        }

        public bool ValidExpiradateDate(string expiracyDate)
        {
            if (!Regex.IsMatch(expiracyDate, @"\b(0[1-9]|1[0-2])\/?([0-9]{2})\b"))
                return false;

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            year = year % 100;

            int givenYear = Convert.ToInt16(expiracyDate.Substring(3));
            int givenMonth = Convert.ToInt16(expiracyDate.Substring(0, 2));

            if (givenYear == year && month > givenMonth || givenYear < year)
                return false;
            else if (givenYear < year)
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
        public (string, bool) AddDonor(string firstName, string lastName, string donorID, string address, 
            string phone, char cardType, string cardNumber, string cardExpiry)
        {
            if (!IDLenghtVerifier(donorID))
                return ("ID character length different than 4 character", false);

            if (DonorIDExist(donorID))
                return ("The donor ID given is already in use", false);

            if (firstName.Length > 15 || firstName.Length > 15 || address.Length > 40)
                return ("the name, last name, or address has more characters than given", false);

            if (!ValidPhoneNumber(phone))
                return ("The phone number format is not valid", false);

            //if (!CreditOrDebit(cardType))
            //    return ("ID character length different than 4 character", false);

            if (cardNumber.Length != 16)
                return ("The card number has to be 16 numbers long", false);

            if (!ValidExpiradateDate(cardExpiry))
                return ("The card's expiracy date is not valid", false);

            var newDonor = new Donor(firstName, lastName, donorID, address, phone, cardType, cardNumber, cardExpiry);
            _donors.Add(newDonor);

            return ("The donor was successfully added", true);
        }

        public string AddSponsor(string firstName, string lastName, string sponsorID, string totalPrizeValue)
        {
            if (!IDLenghtVerifier(sponsorID))
                return "Id lenght is different than 4 characters";

            if ((firstName.Length + lastName.Length) > 30)
                return "Name and last name lenght have more than 30 characters";

            if (SponsorsIDExist(sponsorID))
                return "The given id is already in use";

            if (!double.TryParse(totalPrizeValue, out var dTotalPrizeValue))
                return "the total prize has to be a number";

            if (dTotalPrizeValue < 0)
                return "the total prize can't be negativa";

            var newSponsor = new Sponsor(firstName, lastName, sponsorID, dTotalPrizeValue);
            _sponsors.Add(newSponsor);

            return "The sponsor was successfully added";
        }

        public string AddPrize(string prizeID, string description, string value, string donationLimit, 
        string originalAvailable, string currentAvailable, string sponsorID)
        {
            if (!IDLenghtVerifier(prizeID))
                return "Id must be 4 characters long";

            if (PrizeIDExist(prizeID))
                return "The prize id is already being in used";

            if (description.Length > 15)
                return "The prize length is bigger than 15 characters";

            if (!double.TryParse(value, out var dValue))
                return "The value has to be number";

            if (dValue < 0)
                return "the value can't be negative";

            if (!double.TryParse(donationLimit, out var dDonationLimit))
                return "The donation limit cannot be negative";

            if (dDonationLimit < 0)
                return "The donation limit cannot be negative";

            if (!int.TryParse(originalAvailable, out var dOriginalAvailable))
                return "The original available amount has to be a number";

            if (dOriginalAvailable < 0)
                return "There cannot be a negative number of prizes";

            if (!int.TryParse(currentAvailable, out var dCurrentAvailable))
                return "The current available cannot be negative";

            if (dCurrentAvailable < 0 || dCurrentAvailable > dOriginalAvailable)
                return "The actual number of prizes cannot be bigger than the original prize";

            if (!SponsorsIDExist(sponsorID))
                return "Error 404, Sponsor Id not found";

            var temp = new Prize(prizeID, description, dValue, dDonationLimit, dOriginalAvailable, dCurrentAvailable, sponsorID);
            _prizes.Add(temp);
            return "The prize was successfully added";
        }

        public (bool,string) AddDonation(string donationID, string donationDate, string donorID, string donationAmount, string prizeID)
        {
            if (!IDLenghtVerifier(donationID))
                 return (false, "Invalid id lenght");

            if (DonationIDExist(donationID))
                return (false, "Given donation id is already in use");

            //if (DonorIDExist(donorID))
            //    return;

            if (!double.TryParse(donationAmount, out var dDonationAmount))
                return (false, "The donation have to be a number");

            if (dDonationAmount < 5 || dDonationAmount > 999999999)
                return (false, "Donation amount can't be less than 5 or more than 999'999'999");

            if (!PrizeIDExist(prizeID))
                return (false, "Prize ID not found");

            var temp = new Donation(donationID, donationDate, donorID, dDonationAmount, prizeID);
            _donations.Add(temp);
            return (true, "donation was added");
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

        public (bool, string) RecordDonation(string prizeID, string numberOfPrizes, string donorID, string donationAmount, string donationID)
        {
            if (!int.TryParse(numberOfPrizes, out var iNumberOfPrizes))
                return (false, "the number of prizes has to be a number");

            if (!double.TryParse(donationAmount, out var dDonationAmount))
                return (false, "The donation amount is not a number");

            foreach (Prize prize in _prizes)
            {
                if (prizeID == prize.GetPrizeID())
                {
                    if (dDonationAmount < prize.DonationLimit)
                        return (false, "The donation amount doesn't qualify to this prize");

                    if (iNumberOfPrizes <= prize.CurrentAvailable && dDonationAmount >= (prize.Value * iNumberOfPrizes))
                    { 
                        var date = DateTime.Now.ToString("MM/dd/yyyy");
                        var flag = AddDonation(donationID, date, donorID, donationAmount, prizeID);

                        if (!flag.Item1)
                        {
                            return flag;
                        }

                        prize.Decrease(iNumberOfPrizes);
                        return flag;
                    }
                }
            }

            return (false, "Please insert a valid prize id");
        }

        public void HandleUserError(string prizeID, string donationID, string donationAmount)
        {
            foreach (Prize prize in _prizes)
            {
                if (prize.GetPrizeID() == prizeID)
                    prize.CurrentAvailable += Convert.ToInt32(donationAmount);
            }

            foreach (Donation donation in _donations)
            {
                if (donation.DonationID == donationID)
                {
                    _donations.Remove(donation);
                    return;
                }
            }
        }

        public void OnLoad()
        {
            myUsers = User.ReadUsers();
            _sponsors.ReadSponsors();
            _donations.ReadDonation();
            _donors.ReadDonors();
            _prizes.ReadPrizes();
        }

        public void SaveDonors()
        {
            _donors.SaveDonors();
        }

        public void SaveDonations()
        {
            _donations.SaveDonation();
        }

        public void SavePrizes()
        {
            _prizes.SavePrizes();
        }

        public void SaveSponsors()
        {
            _sponsors.SaveSponsor();
        }

        public bool FindUser(string username, string password)
        {
            foreach (var user in myUsers)
            {
                if (user.GetUserName() == username && user.GetPassword() == password)
                    return true;
            }
            return false;
        }

        public bool DonorAlreadyExist(string donorID)
        {
            foreach (Donor donor in _donors)
            {
                if (donor.DonorID == donorID)
                {
                    return true;
                }
            }

            return false;
        }

        // To make this better I need to create a regex
        public string GetPrizeID(string message)
        {
            var temp = message.Split(',');
            temp = temp[0].Split(' ');

            return temp[1];
        }
    }
}
