// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment.BaseToCalculateFeesForAnticipatedRepayment
{
    /// <summary>
    /// Summary description for RemainingInterest.
    /// </summary>
    [Serializable]
    public class RemainingInterest : IBaseToCalculateFeesForAnticipatedRepayment
    {
        private readonly Loan _contract;
        public RemainingInterest(Loan pContract)
        {
            _contract = pContract;
        }

        public OCurrency BaseToCalculateFees(DateTime pDate)
        {
            return (_contract.CalculateRemainingInterests().Value - _contract.CalculateRemainingInterests(pDate).Value) * Convert.ToDecimal(_contract.AnticipatedTotalRepaymentPenalties);
        }
    }
}
