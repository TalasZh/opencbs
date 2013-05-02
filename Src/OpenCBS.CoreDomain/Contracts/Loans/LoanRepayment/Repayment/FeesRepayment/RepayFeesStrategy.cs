using System;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment
{
    [Serializable]
    public class RepayFeesStrategy
    {
        private readonly IFeesRepayment _methodToCalculateFees;
        public RepayFeesStrategy(CreditContractOptions pCCO)
        {
            if (!pCCO.CancelFees)
                _methodToCalculateFees = new AutomaticMethod();
            else
                _methodToCalculateFees = new ManualMethod();
        }

        public void RepayFees(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent)
        {
            _methodToCalculateFees.Repay(pInstallment,ref pAmountPaid, ref pFeesEvent);
        }
    }
}