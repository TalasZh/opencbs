using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.CoreDomain.Products
{
    public interface ISavingProduct
    {
        int Id { get; set; }
        bool Delete { get; set; }
        string Name { get; set; }
        string Code { get; set; }
        OClientTypes ClientType { get; set; }
        OCurrency InitialAmountMin { get; set; }
        OCurrency InitialAmountMax { get; set; }
        OCurrency BalanceMin { get; set; }
        OCurrency BalanceMax { get; set; }
        OCurrency WithdrawingMin { get; set; }
        OCurrency WithdrawingMax { get; set; }
        OCurrency DepositMin { get; set; }
        OCurrency DepositMax { get; set; }
        OCurrency ChequeDepositMin { get; set; }
        OCurrency ChequeDepositMax { get; set; }
        OCurrency TransferMin { get; set; }
        OCurrency TransferMax { get; set; }
        double? InterestRate { get; set; }
        double? InterestRateMin { get; set; }
        double? InterestRateMax { get; set; }
        OCurrency EntryFeesMin { get; set; }
        OCurrency EntryFeesMax { get; set; }
        OCurrency EntryFees { get; set; }
        Currency Currency { get; set; }
        InstallmentType Periodicity { get; set; }
    }
}
