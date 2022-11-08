using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        public (string, bool) AddSponsor(string firstName, string lastName, string sponsorID, string totalPrizeValue)
        {
            if (sponsorID == "")
                return ("Please especify an sponsor ID", false);

            if (!IDLenghtVerifier(sponsorID))
                return ("Id lenght is different than 4 characters", false);

            if ((firstName.Length + lastName.Length) > 30)
                return ("Name and last name lenght have more than 30 characters", false);

            if (SponsorsIDExist(sponsorID))
                return ("The given id is already in use", false);

            if (!double.TryParse(totalPrizeValue, out var dTotalPrizeValue))
                return ("the total prize has to be a number", false);

            if (dTotalPrizeValue < 0)
                return ("the total prize can't be negativa", false);

            var newSponsor = new Sponsor(firstName, lastName, sponsorID, dTotalPrizeValue);
            _sponsors.Add(newSponsor);

            return ("The sponsor was successfully added", true);
        }

        public (string, bool) AddPrize(string prizeID, string description, string value, string donationLimit, 
        string originalAvailable, string currentAvailable, string sponsorID)
        {
            if (prizeID == "")
                return ("Please insert a prize id", false);

            if (!IDLenghtVerifier(prizeID))
                return ("Id must be 4 characters long", false);

            if (PrizeIDExist(prizeID))
                return ("The prize id is already being in used", false);

            if (description.Length > 15)
                return ("The prize length is bigger than 15 characters", false);

            if (!double.TryParse(value, out var dValue))
                return ("The value has to be number", false);

            if (dValue < 0)
                return ("the value can't be negative", false);

            if (!double.TryParse(donationLimit, out var dDonationLimit))
                return ("The donation limit cannot be negative", false);

            if (dDonationLimit < 0)
                return ("The donation limit cannot be negative", false);

            if (!int.TryParse(originalAvailable, out var dOriginalAvailable))
                return ("The original available amount has to be a number", false);

            if (dOriginalAvailable < 0)
                return ("There cannot be a negative number of prizes", false);

            if (!int.TryParse(currentAvailable, out var dCurrentAvailable))
                return ("The current available cannot be negative", false);

            if (dCurrentAvailable < 0 || dCurrentAvailable > dOriginalAvailable)
                return ("The actual number of prizes cannot be bigger than the original prize", false);

            if (sponsorID == "")
                return ("Please especify an sponsor ID", false);

            if (!SponsorsIDExist(sponsorID))
                return ("Error 404, Sponsor Id not found", false);

            RecordSponsorDonation(sponsorID, dOriginalAvailable, dValue);

            var temp = new Prize(prizeID, description, dValue, dDonationLimit, dOriginalAvailable, dCurrentAvailable, sponsorID);
            _prizes.Add(temp);
            return ("The prize was successfully added", true);
        }

        private void RecordSponsorDonation(string sponsorID, int numberOfitem, double itemValue)
        {
            foreach (Sponsor sponsor in _sponsors)
            {
                if (sponsor.GetID() == sponsorID)
                {
                    sponsor.AddValue(numberOfitem * itemValue);
                }
            }
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
            if (prizeID == "")
                return (false, "Please especify a prize Id");

            if (donorID == "")
                return (false, "Please especify a donor Id");

            if (donationID == "")
                return (false, "Please especify a donor Id");


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

                    else if (iNumberOfPrizes > prize.CurrentAvailable)
                        return (false, "Unavaliable number of prizes");

                    else if (dDonationAmount < (prize.Value * iNumberOfPrizes))
                        return (false, "The donation amount isn't enough for the prize(s)");
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

        public void SaveUsers()
        {
            using (StreamWriter sw = new StreamWriter(@".\users.txt"))
            {
                foreach (User user in myUsers)
                {
                    sw.WriteLine(user.ToString());
                }
            }
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

        // Finds the first ID, the class identifier and then return it
        public string GetID(string message)
        {
            var strArr = message.Split(',');
            string id = "";
            foreach (string item in strArr)
            {
                if (item.Contains("ID:")) 
                {
                    var temp = item.Split(' ');
                    id = temp[1];
                    return id;
                }
            }

            return id;
        }

        public (string, bool) DeleteSponsor(string sponsorID, bool forceDelete)
        {
            var listOfPrizes = new List<string>();

            foreach (Sponsor sponsor in _sponsors)
            {
                if (sponsor.GetID() == sponsorID)
                {
                    foreach (Prize prize in _prizes)
                    {
                        if (prize.SponsorID == sponsorID && !forceDelete)
                            return ("The sponsor that you want to delete is related to one or more prizes, are you sure you want to delete? acception this will also delete the prizes", false);


                        else if (prize.SponsorID == sponsorID && forceDelete)
                            listOfPrizes.Add(prize.PrizeID);
                    }


                    _sponsors.Delete(sponsor);
                    break;
                }
            }

            
            foreach (var id in listOfPrizes)
            {
                foreach (Prize prize in _prizes)
                {
                    if (prize.GetPrizeID() == id)
                    {
                        _prizes.Remove(prize);
                        break;
                    }
                }
            }

            return ("The sponsor was succefully deleted", true);

        }

        public string DeletePrize(string prizeID)
        {
            foreach (Prize prize in _prizes)
            {
                if (prize.GetPrizeID() == prizeID)
                {
                    foreach (Sponsor sponsor in _sponsors)
                    {
                        if (sponsor.GetID() == prize.SponsorID)
                            sponsor.DeductValue(prize.OriginalAvailable * prize.Value);
                    }

                    _prizes.Remove(prize);
                    return "The prize was succefully deleted";
                }
                   
            }
            return "The sponsor wasn't found";
        }

        public (string, bool) DeleteDonor(string donorID, bool forceDelete)
        {
            var listOfDonations = new List<string>();

            foreach (Donor donor in _donors)
            {
                if (donor.DonorID == donorID)
                {
                    foreach (Donation donation in _donations)
                    {
                        if (donation.DonorID == donorID && !forceDelete)
                            return ("The donor that you want to delete is related to one or more donations, are you sure you want to delete? acception this will also delete the donations", false);


                        else if (donation.DonorID == donorID && forceDelete)
                            listOfDonations.Add(donation.DonationID);
                    }

                    // a donor doesn't exist without a donation
                    _donors.Remove(donor);
                    break;

                }
            }

            foreach (var id in listOfDonations)
                foreach (Donation donation in _donations)
                    if (donation.DonorID == id)
                    {
                        _donations.Remove(donation);
                        break;
                    }

            return ("The donor was succefully deleted", true);

        }

        public (string, bool) DeleteDonation(string donationID, bool forceDelete)
        {
            bool moreThanOneDonation = false;

            foreach (Donation donation in _donations)
            {
                if (donation.DonationID == donationID)
                {
                    foreach (Donation jdonation in _donations)
                    {
                        if (donation.DonorID == jdonation.DonorID)
                        {
                            moreThanOneDonation = true;
                            break;
                        }
                    }

                    if (moreThanOneDonation)
                    {
                        _donations.Remove(donation);
                        return ("The donation was deleted", true);
                    }

                    if (!forceDelete)
                        return ("To delete this donation you need to also remove the donor, since a donor cannot be exist without a donation", false);

                    foreach (Donor donor in _donors)
                        if (donor.DonorID == donation.DonorID)
                        {
                            _donors.Remove(donor);
                            _donations.Remove(donation);
                            return ("the donation was deleted", true);
                        }
                    
                    
                }
            }

            return ("The donation wasn't found", false);
        }

        public string CreateUser(string username, string password, bool create, bool delete, bool manage)
        {
            foreach (User user in myUsers)
            {
                if (user.Username == username)
                    return "This username is already in use";
            }

            var temp = new User(username, password, create, delete, manage);
            myUsers.Add(temp);
            return "The user was succesfully added";
        }

        public (bool, bool, bool) GetPermits(string username)
        {
            bool create = false;
            bool delete = false;
            bool manage = false;
            foreach (var user in myUsers)
            {
                if (user.Username == username)
                {
                    create |= user.Permit.HasFlag(Permit.Create);
                    delete |= user.Permit.HasFlag(Permit.Delete);
                    manage |= user.Permit.HasFlag(Permit.Manage);
                }
            }

            return (create, delete, manage);
        }

        public string EditUser(string username, string password, bool create, bool delete, bool manage)
        {
            foreach (User user in myUsers)
            {
                if (user.Username == username)
                {
                    user.Password = password;

                    if (user.Permit.HasFlag(Permit.Master))
                        return "Master password has been change. However, master permits can not be change";

                    if (!create && user.Permit.HasFlag(Permit.Create))
                        user.Permit &= ~Permit.Create;
                    else if (create && !user.Permit.HasFlag(Permit.Create))
                        user.Permit |= Permit.Create;

                    if (!delete && user.Permit.HasFlag(Permit.Delete))
                        user.Permit &= ~Permit.Delete;
                    else if (delete && !user.Permit.HasFlag(Permit.Delete))
                        user.Permit |= Permit.Delete;

                    if (!manage && user.Permit.HasFlag(Permit.Manage))
                        user.Permit &= ~Permit.Manage;
                    else if (manage && !user.Permit.HasFlag(Permit.Manage))
                        user.Permit |= Permit.Manage;

                    return "The user has been edited";
                }
            }
            return "Invalid user";
        }

        public List<string> UsersString()
        {
            var message = new List<string>();
            foreach (User user in myUsers)
            {
                message.Add(user.DetailedData());
            }

            return message;
        }

        public string DeleteUser(string username, string activeUser)
        {
            if (username == "admin")
                return "this user cannot be deleted";
            if (username == activeUser)
                return "the user cannot delete itself";
            foreach (User user in myUsers)
            {
                if (user.Username == username)
                {
                    myUsers.Remove(user);
                    return "The user was successfully deleted";
                } 
            }

            return "There was an error when deleting the user";
        }
    }
}
