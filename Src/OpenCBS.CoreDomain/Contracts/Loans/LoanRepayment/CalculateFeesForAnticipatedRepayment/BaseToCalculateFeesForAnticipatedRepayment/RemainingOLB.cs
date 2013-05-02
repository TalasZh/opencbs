// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment.BaseToCalculateFeesForAnticipatedRepayment
{
    /// <summary>
    /// Summary description for RemainingOLB.
    /// </summary>
    [Serializable]
    public class RemainingOLB : IBaseToCalculateFeesForAnticipatedRepayment
    {
        private readonly Loan _contract;
        public RemainingOLB(Loan pContract)
        {
            _contract = pContract;
        }

        public OCurrency BaseToCalculateFees(DateTime pDate)
        {
            return _contract.CalculateActualOlb().Value * Convert.ToDecimal(_contract.AnticipatedTotalRepaymentPenalties);
        }
    }
}
