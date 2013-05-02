// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Text;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
    public interface IFeesRepayment
    {
        void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent);
    }
}
