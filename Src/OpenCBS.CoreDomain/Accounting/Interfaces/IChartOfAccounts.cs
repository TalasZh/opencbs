// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Contracts;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.Enums;
namespace OpenCBS.CoreDomain.Accounting.Interfaces
{
    public interface IChartOfAccounts
    {
        void AddAccount(Account account);
        Account GetAccountByNumber(string number, int pCurrency_id);
        Account GetAccountByNumber(string number, int pCurrency_id, ISavingsContract pSavingsContract, OBookingDirections pBookingDirection);
        Account GetAccountByNumber(string number, int pCurrency_id, IContract pContract, OBookingDirections pBookingDirection);
        void Book(AccountingTransaction movementSet);
        void UnBook(AccountingTransaction movementSet);
    }
}
