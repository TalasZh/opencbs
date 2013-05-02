// LICENSE PLACEHOLDER

using System;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
    /// <summary>
    /// Summary description for IBaseToCalculateFeesForAnticipatedRepayment.
    /// </summary>
    public interface IBaseToCalculateFeesForAnticipatedRepayment
    {
        OCurrency BaseToCalculateFees(DateTime pDate);
    }
}
