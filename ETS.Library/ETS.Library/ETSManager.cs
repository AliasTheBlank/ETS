using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    public class ETSManager
    {

        private Donors _donors = new Donors();
        private Sponsors _sponsors = new Sponsors();
        private Donations _donations = new Donations();
        private Prizes _prizes = new Prizes();

        #region Add methods
        public void AddDonor(string firstName, string lastName, string donorID, string address, 
            string phone, char cardType, string cardNumber, string cardExpiry)
        {

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
