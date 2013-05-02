// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;


namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment
{
    /// <summary>
    /// Summary description for CalculateAnticipatedFeesStrategy.
    /// </summary>
    [Serializable]
    public class CalculateAnticipatedFeesStrategy
    {
        private readonly ICalculateFeesForAnticipatedRepayment _cFfar;
        public CalculateAnticipatedFeesStrategy(CreditContractOptions pCCO,Loan pContract, ApplicationSettings pGeneralSettings)
        {
            if(pCCO.CancelFees || pCCO.KeepExpectedInstallments)
                _cFfar = new AnticipatedFeesNotCalculate();
            else
                _cFfar = new AnticipatedFeesCalculate(pCCO,pContract);
        }

        public void calculateFees(DateTime pDate, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent)
        {
            _cFfar.CalculateFees(pDate,ref pAmountPaid,ref pFeesEvent);
        }
    }
}
