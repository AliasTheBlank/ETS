using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    internal class Donors : CollectionBase
    {
        public void Add(Donor newDonor)
        {
            List.Add(newDonor);
        }

        public Donor this[int index]
        {
            get { return (Donor)this[index]; }
            set { this[index] = value; }
        }

        public void Remove(string donorToRemove)
        {
            foreach (Donor donor in this)
            {
                if (donor.DonorID == donorToRemove)
                    List.Remove(donor);
            }
        }

        public void SaveDonors()
        {
            using (StreamWriter sw = new StreamWriter(@".\Donors.txt"))
            {
                foreach (Donor donors in this)
                {
                    sw.WriteLine(donors.ToString());
                }
            }
        }

        public void ReadDonors()
        {
            if (!File.Exists(@".\Donors.txt"))
                return;

            using (StreamReader sr = new StreamReader(@".\Donors.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    string str = sr.ReadLine();
                    string[] strArray = str.Split(',');

                    Donor donor = new Donor(strArray[0], strArray[1], strArray[2], strArray[3], strArray[4], Convert.ToChar(strArray[5]), strArray[6], strArray[7]);

                    this.Add(donor);
                }
            }
        }
    }
}
