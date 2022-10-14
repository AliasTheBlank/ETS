using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    internal class Donor : Person
    {
        private string _donorID;
        private string _address;
        private string _phone;
        private char _cardType;
        private string _cardNumber;
        private string _cardExpiry;

        public Donor() : base()
        {
            _donorID = "";
            _address = "";
            _phone = "";
            _cardType = '\0';
            _cardNumber = "";
            _cardExpiry = "";
        }

        public Donor(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public Donor(string firstName, string lastName, string donorID, string address, string phone, char cardType, string cardNumber, string cardExpiry) : this(firstName, lastName)
        {
            _donorID = donorID;
            _address = address;
            _phone = phone;
            _cardType = cardType;
            _cardNumber = cardNumber;
            _cardExpiry = cardExpiry;
        }

        public string DonorID { get => _donorID; set => _donorID = value; }
        public string Address { get => _address; set => _address = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public char CardType { get => _cardType; set => _cardType = value; }
        public string CardNumber { get => _cardNumber; set => _cardNumber = value; }
        public string CardExpiry { get => _cardExpiry; set => _cardExpiry = value; }

        public override string ToString()
        {
            return base.ToString() + $",{DonorID},{Address},{Phone},{CardType},{CardNumber},{CardExpiry}";
        }

        public override string DisplayData()
        {
            return base.DisplayData() + $", DonorID: {DonorID}, Address: {Address}, Phone: {Phone}, Card type: {CardType}, Card Number: {CardNumber}, Card Expiracy: {CardExpiry}";
        }
    }
}
