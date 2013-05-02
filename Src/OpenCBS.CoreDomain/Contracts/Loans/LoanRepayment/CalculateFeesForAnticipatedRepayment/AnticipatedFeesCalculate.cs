// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment.BaseToCalculateFeesForAnticipatedRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment
{
    /// <summary>
    /// Summary description for AnticipatedFeesCalculate.
    /// </summary>
    [Serializable]
    public class AnticipatedFeesCalculate : ICalculateFeesForAnticipatedRepayment
    {
        private readonly CalculationBaseForAnticipatedFees _bTcffar;
        private readonly Loan _loan;

        public AnticipatedFeesCalculate(CreditContractOptions pCCO,Loan pContract)
        {
            _loan = pContract;
            _bTcffar = new CalculationBaseForAnticipatedFees(pCCO,pContract);
        }

        public void CalculateFees(DateTime pDate, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent)
        {
            OCurrency fees = _loan.UseCents 
                                        ? Math.Round(_bTcffar.CalculateFees(pDate).Value,2) 
                                        : Math.Round(_bTcffar.CalculateFees(pDate).Value, 0);

            if(AmountComparer.Compare( fees, pAmountPaid) > 0)
            {
                pFeesEvent += pAmountPaid;
                pAmountPaid = 0;
            }
            else
            {
                pAmountPaid -= fees;
                pFeesEvent += fees;
            }
        }
    }
}
