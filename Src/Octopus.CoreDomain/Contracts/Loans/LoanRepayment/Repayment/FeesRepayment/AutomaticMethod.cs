using System;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using Octopus.Shared;


namespace Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment
{
    [Serializable]
    public class AutomaticMethod : IFeesRepayment
    {
        public void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent)
        {
            if (AmountComparer.Compare(pAmountPaid, pInstallment.FeesUnpaid) > 0)
            {
                pFeesEvent += pInstallment.FeesUnpaid;
                pAmountPaid -= pInstallment.FeesUnpaid;
                pInstallment.PaidFees += pInstallment.FeesUnpaid;
                pInstallment.FeesUnpaid = 0;

            }
            else
            {
                pFeesEvent += pAmountPaid;
                pInstallment.PaidFees += pAmountPaid;
                pInstallment.FeesUnpaid -= pAmountPaid;
                pAmountPaid = 0;
            }
        }
    }
}