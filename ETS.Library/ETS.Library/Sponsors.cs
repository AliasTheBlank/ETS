using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    internal class Sponsors : CollectionBase
    {
        public void Add(Sponsor newSponsor)
        {
            List.Add(newSponsor);
        }

        public Sponsor this[int index]
        {
            get { return (Sponsor)this[index]; }
            set { this[index] = value; }
        }

        public void Delete(Sponsor sponsor)
        {
            List.Remove(sponsor);
        }

        public void SaveSponsor()
        {
            using (StreamWriter sw = new StreamWriter(@".\Sponsors.txt"))
            {
                foreach (Sponsor sponsor in this)
                {
                    sw.WriteLine(sponsor.ToString());
                }
            }
        }

        public void ReadSponsors()
        {
            if (!File.Exists(@".\Sponsors.txt"))
                return;

            using (StreamReader sr = new StreamReader(@".\Sponsors.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    string str = sr.ReadLine();
                    string[] strArray = str.Split(',');

                    Sponsor sponsor = new Sponsor(strArray[0], strArray[1], strArray[2], Convert.ToDouble(strArray[3]));

                    this.Add(sponsor);
                }
            }
        }
    }
}
