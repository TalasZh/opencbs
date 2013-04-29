using System;
using System.Collections.Generic;
using System.Text;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.Shared;

namespace Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
    public interface IFeesRepayment
    {
        void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent);
    }
}