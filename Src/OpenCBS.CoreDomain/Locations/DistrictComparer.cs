using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octopus.CoreDomain
{
    public class DistrictComparer : IComparer<District>
    {
        public int Compare(District x, District y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
