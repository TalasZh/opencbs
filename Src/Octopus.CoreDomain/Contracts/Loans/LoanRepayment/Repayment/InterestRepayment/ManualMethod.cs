using System;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using Octopus.Shared;

namespace Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.InterestRepayment
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