
using System;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment
{
    [Serializable]
    public class ManualMethod : IFeesRepayment
    {
        public void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent)
        {
            //Nothing to modify
            pInstallment.PaidFees += pFeesEvent;
            pAmountPaid -= pFeesEvent;
            pFeesEvent = 0;
        }
    }
}