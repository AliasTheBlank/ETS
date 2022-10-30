using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETS.Library
{
    internal class Prizes : CollectionBase
    {
        public void Add(Prize newPrize)
        {
            List.Add(newPrize);
        }

        public Prize this[int index]
        {
            get { return (Prize)this[index]; }
            set { this[index] = value; }
        }

        public void Remove(Prize oldPrize)
        {
            List.Remove(oldPrize);
        }

        public void SavePrizes()
        {
            using (StreamWriter sw = new StreamWriter(@".\Prize.txt"))
            {
                foreach (Prize prize in this)
                {
                    sw.WriteLine(prize.ToString());
                }
            }
        }

        public void ReadPrizes()
        {
            if (!File.Exists(@".\Prize.txt"))
                return;

            using (StreamReader sr = new StreamReader(@".\Prize.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    string str = sr.ReadLine();
                    string[] strArray = str.Split(',');

                    Prize prize = new Prize(strArray[0], strArray[1], Convert.ToDouble(strArray[2]), Convert.ToDouble(strArray[3]), Convert.ToInt32(strArray[4]), Convert.ToInt32(strArray[5]), strArray[6]);

                    this.Add(prize);
                }
            }
        }
    }
}
