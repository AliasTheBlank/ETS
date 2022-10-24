using System;
using System.Collections;
using System.Collections.Generic;
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

    }
}
