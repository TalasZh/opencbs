using System;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.InterestRepayment
{
    [Serializable]
    public class ManualMethod : IInterestRepayment
    {
        public void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pInterestEvent, ref OCurrency pInterestPrepayment)
        {
            //Nothing to modify
        }
    }
}