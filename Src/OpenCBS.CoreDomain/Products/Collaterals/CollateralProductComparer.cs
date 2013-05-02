// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Products.Collaterals
{
    [Serializable]
    public class CollateralProductComparer<T> : IComparer<T>
    {
        public int Compare(T a, T b)
        {
            var x = (ICollateralProduct) a;
            var y = (ICollateralProduct) b;

            if (x.Delete && y.Delete)
                return x.Name.CompareTo(y.Name);
            if (!x.Delete && !y.Delete)
                return x.Name.CompareTo(y.Name);

            return x.Delete ? 1 : -1;
        }
    }
}
