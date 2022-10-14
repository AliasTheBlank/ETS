using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}
