// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;

namespace OpenCBS.CoreDomain
{
	/// <summary>
	/// Summary description for InstallmentComparer.
    /// </summary>
    [Serializable]
    public class InstallmentComparer : IComparer<Installment>
	{
	    public int Compare(Installment x, Installment y)
	    {
            return DateTime.Compare(x.ExpectedDate, y.ExpectedDate);
	    }
	}
}
