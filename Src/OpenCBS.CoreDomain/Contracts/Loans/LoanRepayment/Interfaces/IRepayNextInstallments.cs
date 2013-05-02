// LICENSE PLACEHOLDER

using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
    /// <summary>
    /// Summary description for IRepayNextInstallment.
    /// </summary>
    public interface IRepayNextInstallments
    {
        List<Installment> PaidInstallments{ get; set;}

        void RepayNextInstallments(ref OCurrency amountPaid, ref OCurrency interestEvent,
                                   ref OCurrency interestPrepayment, ref OCurrency principalEvent,
                                   ref OCurrency feesEvent, ref OCurrency commissionsEvent);
    }
}
