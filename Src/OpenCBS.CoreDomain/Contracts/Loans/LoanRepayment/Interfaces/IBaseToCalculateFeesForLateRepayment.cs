// LICENSE PLACEHOLDER

using System;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
    /// <summary>
    /// Summary description for IBaseToCalculateFeesForLateRepayment.
    /// </summary>
    public interface IBaseToCalculateFeesForLateRepayment
    {
        OCurrency CalculateFees(Loan pContract, DateTime pDate);
    }
}
