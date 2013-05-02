// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Products
{
    [Serializable]
    public class ProductComparer<T> : IComparer<T>
    {
        public int Compare(T a, T b)
        {
            IProduct x = (IProduct)a;
            IProduct y = (IProduct)b;

            if (x.Delete && y.Delete)
                return x.Name.CompareTo(y.Name);
            if (!x.Delete && !y.Delete)
                return x.Name.CompareTo(y.Name);
            
            return x.Delete ? 1 : -1;
        }
    }
}
