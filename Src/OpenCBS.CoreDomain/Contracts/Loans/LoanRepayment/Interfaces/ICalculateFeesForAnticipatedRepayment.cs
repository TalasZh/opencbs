// LICENSE PLACEHOLDER

using System;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
    /// <summary>
    /// Summary description for ICalculateFeesForAnticipatedRepayment.
    /// </summary>
    public interface ICalculateFeesForAnticipatedRepayment
    {
        void CalculateFees(DateTime date, ref OCurrency amoundPaid, ref OCurrency feesEvent);
    }
}
