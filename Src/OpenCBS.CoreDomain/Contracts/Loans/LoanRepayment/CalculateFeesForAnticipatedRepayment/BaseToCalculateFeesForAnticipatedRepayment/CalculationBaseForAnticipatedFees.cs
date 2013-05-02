// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment.BaseToCalculateFeesForAnticipatedRepayment
{
    /// <summary>
    /// Summary description for BaseToCalculateFeesForAnticipatedRepayment.
    /// </summary>
    [Serializable]
    public class CalculationBaseForAnticipatedFees
    {
        private readonly IBaseToCalculateFeesForAnticipatedRepayment _iBtcffar;

        public CalculationBaseForAnticipatedFees(CreditContractOptions pCCO, Loan pContract)
        {
            if (pCCO.AnticipatedTotalRepaymentPenaltiesBase == OAnticipatedRepaymentPenaltiesBases.RemainingInterest)
                _iBtcffar = new RemainingInterest(pContract);
            else
                _iBtcffar = new RemainingOLB(pContract);
        }

        public OCurrency CalculateFees(DateTime pDate)
        {
            return _iBtcffar.BaseToCalculateFees(pDate);
        }
    }
}
