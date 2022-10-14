using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}
