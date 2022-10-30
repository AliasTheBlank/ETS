using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    internal class Donations : CollectionBase
    {
        public void Add(Donation newDonation)
        {
            List.Add(newDonation);
        }

        public Donation this[int index]
        {
            get { return (Donation)this[index]; }
            set { this[index] = value; }
        }

        public void Remove(Donation donation)
        {
            List.Remove(donation);
        }

        public void SaveDonation()
        {
            using (StreamWriter sw = new StreamWriter(@".\Donations.txt"))
            {
                foreach (Donation donation in this)
                {
                    sw.WriteLine(donation.ToString());
                }
            }
        }

        public void ReadDonation()
        {
            if (!File.Exists(@".\Donations.txt"))
                return;

            using (StreamReader sr = new StreamReader(@".\Donations.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    string str = sr.ReadLine();
                    string[] strArray = str.Split(',');

                    Donation donation = new Donation(strArray[0], strArray[1], strArray[2], Convert.ToDouble(strArray[3]), strArray[4]);

                    this.Add(donation);
                }
            }
        }
    }
}
