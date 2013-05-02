using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Contracts.Savings
{
    public interface ISavingsContract : ICloneable
    {
        int Id { get; set; }
        string Code { get; set; }
        ISavingProduct Product { get; set; }
        DateTime CreationDate { get; set; }
        DateTime? ClosedDate { get; set; }
        double InterestRate { get; set; }
        OSavingsStatus Status { get; set; }
        User User { get; }
        List<SavingEvent> Events { get; }
        int? NsgID { get; set; }

        OCurrency InitialAmount { get; set; }
        OCurrency EntryFees { get; set; }
        
        IClient Client { get; set; }
        User SavingsOfficer { get; set; }
        Branch Branch { get; set; }
        
        OCurrency GetBalance();
        OCurrency GetBalance(DateTime date);
        string GetFmtBalance(bool showCurrency);
        string GetFmtBalance(DateTime date, bool showCurrency);
        string GetFmtAvailBalance(bool showCurrency);
        string GetFmtAvailBalance(DateTime date, bool showCurrency);

        bool HasPendingEvents();
        bool HasCancelableEvents();
        SavingEvent GetCancelableEvent();
        SavingEvent CancelLastEvent();
        void CancelEvent(SavingEvent pSavingEvent);
        

        List<SavingEvent> FirstDeposit(OCurrency pInitialAmount, DateTime pCreationDate, OCurrency pEntryFees, User pUser, Teller teller);

        OCurrency GetBalanceMin(DateTime pDate);
        List<SavingEvent> Withdraw(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees, Teller teller);
 
        List<SavingEvent> Deposit(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees, 
            bool isPending, OSavingsMethods savingsMethod, int? pendingEventId, Teller teller);
        
        List<SavingEvent> LoanDisbursement(Loan loan, DateTime date, string description, User user, 
            bool isDesactivateFees, bool isPending, OSavingsMethods savingsMethod, int? pendingEventId, Teller teller);
        
        SavingCreditOperationEvent SpecialOperationCredit(OCurrency amount, DateTime date, string description, User user);
        SavingDebitOperationEvent SpecialOperationDebit(OCurrency amount, DateTime date, string description, User user);

        SavingEvent DebitTransfer(ISavingsContract to, OCurrency amount, OCurrency fee, DateTime date, string description);
        SavingEvent CreditTransfer(ISavingsContract from, OCurrency amount, DateTime date, string description);
        List<SavingEvent> Transfer(ISavingsContract to, OCurrency amount, OCurrency fee, DateTime date, string description);
        SavingOverdraftFeeEvent ChargeOverdraftFee(DateTime date, User user);

        List<SavingEvent> Closure(DateTime pDate, User user);
        List<SavingEvent> SimulateClose(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees, Teller teller);
        List<SavingEvent> Close(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees, Teller teller, bool isFromClosure);
        List<SavingEvent> Reopen(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees);
        List<SavingEvent> RefusePendingDeposit(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, OSavingsMethods method, int? pendingEventId);
        
        DateTime GetLastPostingDate();
        DateTime GetLastAccrualDate();
        List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pDate, User pUser);
        List<SavingInterestsPostingEvent> PostingInterests(DateTime pDate, User pUser);
        string GenerateSavingCode(Client pClient, int pSavingsCount, string pCodeTemplate, string pImfCode, string pBranchCode);

        List<SavingEvent> RepayLoanFromSaving(Loan loan, int repaymentEventId, DateTime date, OCurrency amount, string description, User user, Teller teller);
        OSavingsRollover Rollover { get; set; }
        ISavingsContract TransferAccount { get; set; }
        int NumberOfPeriods { get; set; }

    }
}