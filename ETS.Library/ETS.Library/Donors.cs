using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}
