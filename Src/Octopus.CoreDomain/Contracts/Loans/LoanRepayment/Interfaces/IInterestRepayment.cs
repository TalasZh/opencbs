
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.Shared;

namespace Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
    public interface IInterestRepayment
    {
        void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pInterestEvent,ref OCurrency pInterestPrepayment);
    }
}