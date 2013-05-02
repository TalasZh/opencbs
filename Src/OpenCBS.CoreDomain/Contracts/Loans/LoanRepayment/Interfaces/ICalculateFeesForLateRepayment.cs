// LICENSE PLACEHOLDER

using System;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
    /// <summary>
    /// Summary description for ICalculateFeesForLateRepayment.
    /// </summary>
    public interface ICalculateFeesForLateRepayment
    {
        OCurrency CalculateFees(DateTime pDate);
    }
}
