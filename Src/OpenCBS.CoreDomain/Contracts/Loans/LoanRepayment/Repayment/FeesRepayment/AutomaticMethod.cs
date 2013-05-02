using System;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Shared;


namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment
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