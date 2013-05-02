// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Accounting.Comparer
{
    [Serializable]
    class AccountComparer : IComparer<Account>
    {
        public int Compare(Account x, Account y)
        {
            return x.Number.CompareTo(y.Number);
        }
    }
}
