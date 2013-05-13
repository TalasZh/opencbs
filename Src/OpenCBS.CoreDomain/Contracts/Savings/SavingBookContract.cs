// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.Collections.Generic;
using System.Linq;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests;
using OpenCBS.CoreDomain.Contracts.Loans;

namespace OpenCBS.CoreDomain.Contracts.Savings
{
    [Serializable]
	public class SavingBookContract : SavingsContract
    {
        public event AccountAtMaturityHandler AccountAtMaturityEvent;
        private List<Loan> _loans;

        public DateTime? NextMaturity { get; set; }

        public DateTime GetNextMaturity (DateTime currentMaturityDate, InstallmentType periodicity)
        {
            return DateCalculationStrategy.GetNextMaturity(currentMaturityDate, periodicity, 1);
        }

        new public SavingsBookProduct Product 
        {
            get { return (SavingsBookProduct)base.Product; }
            set { base.Product = value; } 
        }

        public bool UseTermDeposit { get; set; }
        public int? TermDepositPeriodMin { get; set; }
        public int? TermDepositPeriodMax { get; set; }

        public OCurrency FlatInterBranchTransferFee { get; set; }
        public double? RateInterBranchTransferFee { get; set; }

        public OCurrency FlatWithdrawFees { get; set; }
        public double? RateWithdrawFees { get; set; }

        public OCurrency FlatTransferFees { get; set; }
        public double? RateTransferFees { get; set; }

        public OCurrency DepositFees { get; set; }
        public OCurrency ChequeDepositFees { get; set; }
        
        public OCurrency OverdraftFees { get; set; }
        public bool InOverdraft { get; set; }

        public OCurrency ManagementFees { get; set; }
        public double? AgioFees { get; set; }

        public OCurrency CloseFees { get; set; }
        public OCurrency ReopenFees { get; set; }

        public SavingBookContract(ApplicationSettings pApplicationSettings, User pUser)
        {
            Events = new List<SavingEvent>();
            ApplicationSettings = pApplicationSettings;
            User = pUser;
            _loans = new List<Loan>();
        }

        public SavingBookContract(ApplicationSettings pApplicationSettings, User pUser, SavingsBookProduct pProduct)
        {
            base.Product = pProduct;

            Events = new List<SavingEvent>();
            ApplicationSettings = pApplicationSettings;
            User = pUser;
            _loans = new List<Loan>();
            UseTermDeposit = pProduct.UseTermDeposit;
        }

        public SavingBookContract(ApplicationSettings pApplicationSettings, User pUser, DateTime pCreationDate, IClient pClient)
        {
            Client = pClient;
            CreationDate = pCreationDate;

            Events = new List<SavingEvent>();
            ApplicationSettings = pApplicationSettings;
            User = pUser;
            _loans = new List<Loan>();
        }

        public SavingBookContract(ApplicationSettings pApplicationSettings, User pUser, 
            DateTime pCreationDate, SavingsBookProduct pProduct, IClient pClient)
        {
            Client = pClient;
            CreationDate = pCreationDate;
            base.Product = pProduct;

            Events = new List<SavingEvent>();
            ApplicationSettings = pApplicationSettings;
            User = pUser;
            _loans = new List<Loan>();
        }

        public List<Loan> Loans
        {
            get { return _loans; }
            set { _loans = value; }
        }

        public void AddLoan(Loan loanAccount)
        {
            _loans.Add(loanAccount);
        }

        public override List<SavingEvent> FirstDeposit(OCurrency pInitialAmount, DateTime pCreationDate, 
                                                        OCurrency pEntryFees, User pUser, Teller teller)
        {
            List<SavingEvent> events = new List<SavingEvent>();

            int? tellerId = null;
            if (teller != null && teller.Id != 0) tellerId = teller.Id;

            SavingInitialDepositEvent initialEvent = new SavingInitialDepositEvent
            {
                Amount = pInitialAmount,
                Date = pCreationDate,
                Description = "First deposit",
                User = pUser,
                Fee = pEntryFees,
                TellerId = tellerId,
                ProductType = typeof(SavingsBookProduct),
                SavingsMethod = OSavingsMethods.Cash
            };

            Events.Add(initialEvent);
            events.Add(initialEvent);

            CreationDate = pCreationDate;
            
            return events;
        }

        public override List<SavingEvent> LoanDisbursement(Loan loan, DateTime date, string description, User user, 
                bool isDesactivateFees, bool isPending, OSavingsMethods savingsMethod, int? pendingEventId, Teller teller)
        {
            var events = new List<SavingEvent>();
            int? tellerId = null;
            if (teller != null && teller.Id != 0) tellerId = teller.Id;

            SavingEvent savingEvent = new SavingLoanDisbursementEvent
                                          {
                                              Amount = loan.Amount,
                                              Date = date,
                                              Description = description,
                                              User = user,
                                              Cancelable = false,
                                              IsPending = isPending,
                                              SavingsMethod = null,
                                              PendingEventId = pendingEventId,
                                              TellerId = tellerId,
                                              LoanEventId = loan.GetNotDeletedDisbursementEvent().Id,
                                              ProductType = typeof (SavingsBookProduct)
                                          };
            Events.Add(savingEvent);
            events.Add(savingEvent);
            savingEvent.Fee = Status != OSavingsStatus.Closed && !isDesactivateFees ? loan.GetSumOfFees() : 0;

            return events;
        }

        public override List<SavingEvent> Deposit(OCurrency pAmount, DateTime pDate, string pDescription, User pUser,
                bool pIsDesactivateFees, bool isPending, OSavingsMethods savingsMethod, int? pendingEventId, Teller teller)
        {
            List<SavingEvent> events = new List<SavingEvent>();

            int? tellerId = null;
            if (teller != null && teller.Id != 0) tellerId = teller.Id;

            SavingEvent savingEvent;

            if (isPending)
            {
                savingEvent = new SavingPendingDepositEvent();
            }
            else
            {
                savingEvent = new SavingDepositEvent();
            }

            savingEvent.Amount = pAmount;
            savingEvent.Date = pDate;
            savingEvent.Description = pDescription;
            savingEvent.User = pUser;
            savingEvent.Cancelable = true;
            savingEvent.IsPending = isPending;
            savingEvent.SavingsMethod = savingsMethod;
            savingEvent.PendingEventId = pendingEventId;
            savingEvent.TellerId = tellerId;
            savingEvent.ProductType = typeof(SavingsBookProduct);

            Events.Add(savingEvent);
            events.Add(savingEvent);

            if (Status != OSavingsStatus.Closed && !pIsDesactivateFees)
                savingEvent.Fee = savingsMethod == OSavingsMethods.Cheque ? ChequeDepositFees : DepositFees;

            return events;
        }

        public override List<SavingEvent> Withdraw(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees, Teller teller)
        {
            List<SavingEvent> events = new List<SavingEvent>();

            int? tellerId = null;
            if (teller != null && teller.Id != 0) tellerId = teller.Id;

            SavingWithdrawEvent withdrawEvent = new SavingWithdrawEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = true,
                Fee = 0m,
                TellerId = tellerId,
                ProductType = typeof(SavingsBookProduct),
                SavingsMethod = OSavingsMethods.Cash
            };
            Events.Add(withdrawEvent);
            events.Add(withdrawEvent);

            if (Status != OSavingsStatus.Closed && !pIsDesactivateFees)
               withdrawEvent.Fee = Product.WithdrawFeesType == OSavingsFeesType.Flat ? FlatWithdrawFees : pAmount * RateWithdrawFees.Value;

            return events;
        }

        public override SavingOverdraftFeeEvent ChargeOverdraftFee(DateTime pDate, User pUser)
        {
            SavingOverdraftFeeEvent e = new SavingOverdraftFeeEvent
            {
                Amount = 0m,
                Date = pDate,
                Description = "Overdraft fee event : " + Code,
                User = pUser,
                Cancelable = true,
                ProductType = typeof(SavingsBookProduct),
                Fee = 0m
            };
            Events.Add(e);

            if (Status != OSavingsStatus.Closed)
                e.Fee = OverdraftFees;

            return e;
        }

        public override List<SavingEvent> DebitTransfer(OCurrency pAmount, ISavingsContract pCreditAccount, DateTime pDate, 
            string pDescription, User pUser, bool pIsDesactivateFees)
        {
            List<SavingEvent> events = new List<SavingEvent>();
            SavingDebitTransferEvent transferEvent = new SavingDebitTransferEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = false,
                RelatedContractCode = pCreditAccount.Code,
                ProductType = typeof(SavingsBookProduct)
            }; 
            events.Add(transferEvent);
            Events.Add(transferEvent);

            if (Status != OSavingsStatus.Closed && !pIsDesactivateFees)
                    transferEvent.Fee = Product.TransferFeesType == OSavingsFeesType.Flat ? FlatTransferFees : pAmount * RateTransferFees.Value;

            return events;
        }

        public override List<SavingEvent> RefusePendingDeposit(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, OSavingsMethods method, 
            int? pendingEventId)
        {
            List<SavingEvent> events = new List<SavingEvent>();
            SavingPendingDepositRefusedEvent refuseEvent = new SavingPendingDepositRefusedEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = false,
                Fee = 0m,
                SavingsMethod = method,
                PendingEventId = pendingEventId,
                ProductType = typeof(SavingsBookProduct)
            };

            events.Add(refuseEvent);
            Events.Add(refuseEvent);

            return events;
        }

        public override List<SavingEvent> Reopen(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees)
        {
            List<SavingEvent> events = new List<SavingEvent>();
            SavingReopenEvent reopenEvent = new SavingReopenEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = false,
                Fee = 0m,
                ProductType = typeof(SavingsBookProduct)
            };

            if (!pIsDesactivateFees) reopenEvent.Fee = ReopenFees;

            events.Clear();
            events.Add(reopenEvent);

            Status = OSavingsStatus.Active;
            ClosedDate = null;
            return events;
        }

        public override List<SavingEvent> Close(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees, Teller teller, bool isFromClosure)
        {
            int? tellerId = null;
            if (teller != null && teller.Id != 0) tellerId = teller.Id;
            List<SavingEvent> listEvents = new List<SavingEvent>();
            if (!isFromClosure)
                listEvents = Closure(pDate, pUser);

            SavingInterestsPostingEvent postingEvent = PostPayableInterests(pDate, pUser);
            if (postingEvent != null)
            {
                listEvents.Add(postingEvent);
            }
            OCurrency amountToPost = listEvents.Where(item => item is SavingInterestsPostingEvent).Sum(item => item.Amount.Value);

            SavingCloseEvent closeEvent = new SavingCloseEvent
            {
                Amount = amountToPost,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = false,
                Fee = 0m,
                TellerId = tellerId,
                ProductType = typeof(SavingsBookProduct),
            };

            Events.Add(closeEvent);

            if (!pIsDesactivateFees)
            {
                closeEvent.Fee = CloseFees;
            }

            listEvents.Clear();
            listEvents.Add(closeEvent);
            Status = OSavingsStatus.Closed;
            ClosedDate = pDate;

            foreach (SavingEvent e in listEvents)
            {
                e.Target = this;
            }
            return listEvents;
        }

        public override List<SavingEvent> SimulateClose(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees, Teller teller)
        {
            return Close(pDate, pUser, pDescription, pIsDesactivateFees, teller, false);
        }

        public override SavingCreditOperationEvent SpecialOperationCredit(OCurrency amount, DateTime date, string description, User user)
        {
            SavingCreditOperationEvent spEvent = new SavingCreditOperationEvent
            {
                Amount = amount,
                Date = date,
                Description = description,
                User = user,
                Cancelable = true,
                ProductType = typeof(SavingsBookProduct)
            };

            Events.Add(spEvent);
            return spEvent;
        }

        public override SavingDebitOperationEvent SpecialOperationDebit(OCurrency amount, DateTime date, string description, User user)
        {
            SavingDebitOperationEvent spEvent = new SavingDebitOperationEvent
            {
                Amount = amount,
                Date = date,
                Description = description,
                User = user,
                Cancelable = true,
                ProductType = typeof(SavingsBookProduct)
            };

            Events.Add(spEvent);
            return spEvent;
        }

        protected DateTime GetLastManagementFeeEventDate()
        {
            CreationDate = new DateTime(CreationDate.Year
                , CreationDate.Month
                , CreationDate.Day
                , DateTime.Now.Hour
                , DateTime.Now.Minute
                , DateTime.Now.Second);
            SavingEvent smfe = Events
                .FindAll(e => e is SavingManagementFeeEvent && !e.Deleted)
                .OrderByDescending(e => e.Date)
                .FirstOrDefault();
            return null == smfe ? CreationDate : smfe.Date;
        }

        private List<SavingEvent> GenerateManagementFeeEvent(DateTime prevDate, DateTime nextDate)
        {
            List<SavingEvent> retval = new List<SavingEvent>();

            const string desc = "Management fee of {0:.00} for {1:dd.MM.yyyy} - {2:dd.MM.yyyy} : {3}";
            object[] items = new object[] { ManagementFees.GetFormatedValue(true), prevDate, nextDate, Code};

            SavingManagementFeeEvent smfe = new SavingManagementFeeEvent
            {
                Amount = 0m,
                Cancelable = true,
                Date = nextDate,
                Description = string.Format(desc, items),
                Fee = ManagementFees.HasValue ? ManagementFees : 0m,
                User = User,
                ProductType = Product.GetType(),
                Branch = Branch,
                Currency = Product.Currency,
                ContracId = Id
            };
            retval.Add(smfe);
            AddSavingEvent(smfe);

            OCurrency balance = GetBalance(nextDate);
            if (balance < 0)
            {
                if (!InOverdraft)
                {
                    SavingOverdraftFeeEvent overdraftFeeEvent = new SavingOverdraftFeeEvent
                    {
                        Amount = 0m,
                        Date = nextDate,
                        Description = "Overdraft fee event : " + Code,
                        User = User,
                        Cancelable = false,
                        Fee = OverdraftFees,
                        ProductType = typeof(SavingsBookProduct),
                        Branch = Branch,
                        Currency = Product.Currency,
                        ContracId = Id
                    };

                    AddSavingEvent(overdraftFeeEvent);
                    retval.Add(overdraftFeeEvent);
                    InOverdraft = true;
                }
            }

            return retval;
        }

        private DateTime GetLastAgioEventDate()
        {
            CreationDate = new DateTime(CreationDate.Year, CreationDate.Month, CreationDate.Day, DateTime.Now.Hour,
                                        DateTime.Now.Minute, DateTime.Now.Second);
            SavingEvent savingEvent = Events.OrderByDescending(item => item.Date).
                FirstOrDefault(item =>(item is SavingAgioEvent || item is SavingClosureEvent) && !item.Deleted);
            return (null == savingEvent) ? CreationDate : savingEvent.Date;
        }

        private SavingAgioEvent GenerateAgioEvent(DateTime prevDate, DateTime nextDate)
        {
            OCurrency balance = GetBalance(nextDate);
            if (balance < 0)
            {
                const string desc = "Agio of {0} for {1:dd.MM.yyyy} - {2:dd.MM.yyyy} : {3}";
                object[] items = new object[] { Math.Abs((balance * AgioFees.Value).Value), prevDate, nextDate, Code };

                SavingAgioEvent savingAgioEvent = new SavingAgioEvent
                {
                    Amount = 0m,
                    Cancelable = true,
                    Date = nextDate,
                    Description = string.Format(desc, items),
                    Fee = Math.Abs((balance * AgioFees.Value).Value),
                    User = User,
                    ProductType = Product.GetType(),
                    Branch = Branch,
                    Currency = Product.Currency,
                    ContracId = Id
                };

                AddSavingEvent(savingAgioEvent);
                return savingAgioEvent;
            }

            return null;
        }

        public SavingUnblockCompulsorySavingsEvent GenerateUnblockCompulsoruSavingEvent(User user, bool cancellable)
        {
            int contracId = Id;
            SavingBlockCompulsarySavingsEvent savingBlockEvent = GetBlockCompulsorySavingEvent();
            SavingUnblockCompulsorySavingsEvent savingUnblockEvent = new SavingUnblockCompulsorySavingsEvent
                                                                         {
                                                                             ContracId = contracId,
                                                                             User = user,
                                                                             Amount = savingBlockEvent.Amount,
                                                                             Date = TimeProvider.Now,
                                                                             EntryDate = TimeProvider.Now,
                                                                             Cancelable = cancellable,
                                                                             Branch = Branch,
                                                                             Currency = Product.Currency,
                                                                         };
            Events.Add(savingUnblockEvent);
            return savingUnblockEvent;
        }

        public SavingBlockCompulsarySavingsEvent GetBlockCompulsorySavingEvent()
        {
            return Events.FirstOrDefault(
                e => !e.Deleted && e.Code == OSavingEvents.BlockCompulsarySavings
                       ) as SavingBlockCompulsarySavingsEvent;
        }

        public SavingUnblockCompulsorySavingsEvent GetUnblockCompulsorySavingEvent()
        {
            return Events.FirstOrDefault(
                e => !e.Deleted && e.Code == OSavingEvents.UnblockCompulsorySavings
                       ) as SavingUnblockCompulsorySavingsEvent;
        }

       
        private List<SavingEvent> DoClosureWithTermDeposit(DateTime date, User user)
        {
            List<SavingEvent> savingEvents = DoClosureWithoutTermDeposit(date, user);

            List<SavingEvent> events = new List<SavingEvent>();

           
            while (NextMaturity <= date)
            {
                if (Rollover == OSavingsRollover.None)
                {
                    events.AddRange(AddSavingEvent(CalculateInterest(NextMaturity.Value, user)));
                    events.AddRange(AddSavingEvent(PostingInterests(NextMaturity.Value, user)));

                    DateTime transferDate = NextMaturity.GetValueOrDefault();
                    if (NextMaturity.Value.Date == CreationDate.Date)
                    {
                        NextMaturity = NextMaturity.Value.AddDays(-1);
                    }
                    
                    NextMaturity = DateCalculationStrategy.GetNextMaturity(NextMaturity.Value,
                                                                           Product.Periodicity, 
                                                                           1);

                    

                    int previousPeriodsCount =
                                    this.Events.FindAll(item => 
                                                        item is SavingInterestsPostingEvent && 
                                                        item.Deleted == false).Count;

                    if (AccountAtMaturityEvent != null &&
                        previousPeriodsCount % NumberOfPeriods == 0)
                    {
                        AccountAtMaturityEvent(this, transferDate, user);
                        break;
                    }
                }

                else if (Rollover==OSavingsRollover.Principal)
                {
                    events.AddRange(AddSavingEvent(CalculateInterest(NextMaturity.Value, user)));
                    events.AddRange(AddSavingEvent(PostingInterests(NextMaturity.Value, user)));
                    DateTime transferDate = NextMaturity.GetValueOrDefault();
                    if (NextMaturity.Value.Date == CreationDate.Date)
                        NextMaturity = NextMaturity.Value.AddDays(-1);
                    NextMaturity = DateCalculationStrategy.GetNextMaturity(NextMaturity.Value, 
                                                                           Product.Periodicity, 
                                                                           1);
                    int previousPeriodCount = this.Events.FindAll(item =>
                                                                  item is SavingInterestsPostingEvent &&
                                                                  item.Deleted == false).Count;
                    if (AccountAtMaturityEvent != null && 
                        previousPeriodCount % NumberOfPeriods == 0)
                    {
                        AccountAtMaturityEvent(this, transferDate, user);
                    }
                }

                else if (Rollover == OSavingsRollover.PrincipalAndInterests)
                {
                    events.AddRange(AddSavingEvent(CalculateInterest(NextMaturity.Value, user)));
                    events.AddRange(AddSavingEvent(PostingInterests(NextMaturity.Value, user)));
                    DateTime transferDate = NextMaturity.GetValueOrDefault();
                    if (NextMaturity.Value.Date == CreationDate.Date)
                        NextMaturity = NextMaturity.Value.AddDays(-1);
                    NextMaturity = DateCalculationStrategy.GetNextMaturity(NextMaturity.Value, 
                                                                           Product.Periodicity, 
                                                                           1);
                }
                    
            }

            if (!(Rollover == OSavingsRollover.None && GetLastPostingDate() != null))
            {
                events.AddRange(AddSavingEvent(CalculateInterest(date, user)));
            }
            else if (!(Rollover == OSavingsRollover.Principal && GetLastPostingDate()!=null))
            {
                events.AddRange(AddSavingEvent(CalculateInterest(date, user)));
            }
            savingEvents.AddRange(events);
            return savingEvents;
        }

        private List<SavingEvent> DoClosureWithoutTermDeposit(DateTime date, User user)
        {
            List<SavingEvent> savingEvents = new List<SavingEvent>();

            savingEvents.AddRange(ChargeManagementFee(date));

            switch (Product.InterestFrequency)
            {
                case OSavingInterestFrequency.EndOfYear:
                    savingEvents.AddRange(PostingEndOfYear(date, user));
                    break;
                case OSavingInterestFrequency.EndOfMonth:
                    savingEvents.AddRange(PostingEndOfMonth(date, user));
                    break;
                case OSavingInterestFrequency.EndOfWeek:
                    savingEvents.AddRange(PostingEndOfWeek(date, user));
                    break;
                case OSavingInterestFrequency.EndOfDay:
                    savingEvents.AddRange(PostingEndOfDay(date, user));
                    break;
                /*default:
                    Debug.Fail("Savings closure: debug fail!");
                    retval = new List<SavingEvent>();
                    break;*/
            }

            savingEvents.AddRange(ChargeAgioFee(date));

            return savingEvents;
        }

        

        private List<SavingEvent> ChargeAgioFee(DateTime closureDate)
        {
            List<SavingEvent> retval = new List<SavingEvent>();

            DateTime prevDate = GetLastAgioEventDate();
            DateTime nextDate = prevDate;
            nextDate = nextDate.AddMonths(Product.AgioFeesFreq.NbOfMonths);
            nextDate = nextDate.AddDays(Product.AgioFeesFreq.NbOfDays);

            while (nextDate.Date <= closureDate.Date)
            {
                SavingAgioEvent savingAgioEvent = GenerateAgioEvent(prevDate, nextDate);
                if (savingAgioEvent != null)
                    if (savingAgioEvent.Fee > 0)
                        retval.Add(savingAgioEvent);

                prevDate = nextDate;
                nextDate = nextDate.AddMonths(Product.AgioFeesFreq.NbOfMonths);
                nextDate = nextDate.AddDays(Product.AgioFeesFreq.NbOfDays);
            }
            
            return retval;
        }

        private List<SavingEvent> ChargeManagementFee(DateTime closureDate)
        {
            List<SavingEvent> retval = new List<SavingEvent>();

            DateTime prevDate = GetLastManagementFeeEventDate();
            DateTime nextDate = prevDate;
            nextDate = nextDate.AddMonths(Product.ManagementFeeFreq.NbOfMonths);
            nextDate = nextDate.AddDays(Product.ManagementFeeFreq.NbOfDays);

            while (nextDate.Date <= closureDate.Date)
            {
                List<SavingEvent> list = GenerateManagementFeeEvent(prevDate, nextDate);
                if (list != null)
                {
                    if (list[0].Fee > 0) retval.Add(list[0]);
                    if (list.Count > 1 && list[1].Fee > 0) retval.Add(list[1]);
                }

                prevDate = nextDate;
                nextDate = nextDate.AddMonths(Product.ManagementFeeFreq.NbOfMonths);
                nextDate = nextDate.AddDays(Product.ManagementFeeFreq.NbOfDays);
            }

            return retval;
        }

        public DateTime GetNextWeekly(DateTime pDate)
        {
            int weekEndDay2 = ApplicationSettings.WeekEndDay2 == 6 ? 1 : ApplicationSettings.WeekEndDay2 + 1;

            return pDate.AddDays((weekEndDay2 <= (int)pDate.DayOfWeek) ? 7 - ((int)pDate.DayOfWeek - weekEndDay2)
                   : weekEndDay2 - (int)pDate.DayOfWeek);
        }

        private List<SavingEvent> PostingEndOfDay(DateTime pDate, User pUser)
        {
            DateTime lastPostingDate = GetLastPostingDate();
            List<SavingEvent> events = new List<SavingEvent>();

            while (DateCalculationStrategy.DateCalculationDiary(lastPostingDate, pDate))
            {
                lastPostingDate = lastPostingDate.AddDays(1);

                events.AddRange(AddSavingEvent(CalculateInterest(new DateTime(lastPostingDate.Year, lastPostingDate.Month, lastPostingDate.Day,
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second), pUser)));
                events.AddRange(AddSavingEvent(PostingInterests(new DateTime(lastPostingDate.Year, lastPostingDate.Month, lastPostingDate.Day,
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second), pUser)));
            }

            return events;
        }

        private List<SavingEvent> PostingEndOfWeek(DateTime pDate, User pUser)
        {
            DateTime lastPostingDate = GetLastPostingDate();
            List<SavingEvent> events = new List<SavingEvent>();

            while (DateCalculationStrategy.DateCalculationWeekly(lastPostingDate, pDate, ApplicationSettings.WeekEndDay2))
            {
                lastPostingDate = DateCalculationStrategy.GetNextWeekly(lastPostingDate, ApplicationSettings.WeekEndDay2);

                events.AddRange(AddSavingEvent(CalculateInterest(new DateTime(lastPostingDate.Year, lastPostingDate.Month, lastPostingDate.Day,
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second), pUser)));
                events.AddRange(AddSavingEvent(PostingInterests(new DateTime(lastPostingDate.Year, lastPostingDate.Month, lastPostingDate.Day,
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second), pUser)));
            }

            events.AddRange(AddSavingEvent(CalculateInterest(pDate, pUser)));

            return events;
        }

        private List<SavingEvent> PostingEndOfMonth(DateTime pDate, User pUser)
        {
            DateTime lastPostingDate = GetLastPostingDate();
            List<SavingEvent> events = new List<SavingEvent>();

            while (DateCalculationStrategy.DateCalculationMonthly(lastPostingDate, pDate))
            {
                lastPostingDate = lastPostingDate.AddMonths(1);

                events.AddRange(AddSavingEvent(CalculateInterest(new DateTime(lastPostingDate.Year, lastPostingDate.Month, 01,
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second), pUser)));
                events.AddRange(AddSavingEvent(PostingInterests(new DateTime(lastPostingDate.Year, lastPostingDate.Month, 01,
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second), pUser)));
            }

            if ((Product).InterestBase != OSavingInterestBase.Monthly)
                events.AddRange(AddSavingEvent(CalculateInterest(pDate, pUser)));

            return events;
        }

        private List<SavingEvent> PostingEndOfYear(DateTime pDate, User pUser)
        {
            DateTime lastPostingDate = GetLastPostingDate();
            List<SavingEvent> events = new List<SavingEvent>();

            while (DateCalculationStrategy.DateCalculationYearly(lastPostingDate, pDate))
            {
                lastPostingDate = lastPostingDate.AddYears(1);

                events.AddRange(AddSavingEvent(CalculateInterest(new DateTime(lastPostingDate.Year, 01, 01,
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second), pUser)));
                events.AddRange(AddSavingEvent(PostingInterests(new DateTime(lastPostingDate.Year, 01, 01,
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second), pUser)));
            }

            events.AddRange(AddSavingEvent(CalculateInterest(pDate, pUser)));
            return events;
        }

	    public bool UseCents
	    {
	        get
	        {
	            return null == Product ? true : Product.UseCents;
	        }
	    }

        public new bool HasPendingEvents()
        {
            return Events.Any(savingEvent => savingEvent.IsPending);
        }

        private void CloseAndTransfer(ISavingsContract from, ISavingsContract to, DateTime date, User pUser,
            OCurrency amount, bool pIsDesactivateFees, Teller teller)
        {
            from.Transfer(to, amount, 0, date, "Closing transfer");
            from.Close(date, pUser, "Close savings contract", pIsDesactivateFees, teller, true);
        }

        public void SavingServicesAccountAtMaturity(ISavingsContract savingContract, DateTime date, User user)
        {
            if (savingContract.Rollover == OSavingsRollover.None)
            {
                CloseAndTransfer(savingContract, savingContract.TransferAccount, date, user,
                                 savingContract.GetBalance(date), true, Teller.CurrentTeller);
            }
            
            if (savingContract.Rollover == OSavingsRollover.Principal)
            {
                DateTime lastMaturity = DateCalculationStrategy.GetLastMaturity(date,
                                                                                savingContract.Product.Periodicity,
                                                                                savingContract.NumberOfPeriods);
                
                OCurrency interests = savingContract.Events.Where(
                                                                     item => item is SavingInterestsPostingEvent && 
                                                                     item.Date.Date > lastMaturity && 
                                                                     item.Date.Date <= date
                                                                     ).
                                                            Sum(item => item.Amount.Value);

                // TODO: replace the fee of zero with a meaningful value
                Transfer(TransferAccount, interests, 0, date, "Transfer interests");
            }
        }

        public override List<SavingEvent> Closure(DateTime date, User user)
        {
            List<SavingEvent> savingEvents = UseTermDeposit
                                                 ? DoClosureWithTermDeposit(date, user)
                                                 : DoClosureWithoutTermDeposit(date, user);

            List < SavingEvent > list = base.Closure(date, user);
            if(list != null)
                savingEvents.AddRange(list);

            return savingEvents;
        }

        #region ICloneable Members

        public override object Clone()
        {
            SavingBookContract saving = (SavingBookContract)MemberwiseClone();
            saving.Events = new List<SavingEvent>();
            saving.Events.AddRange(Events);
            return saving;
        }

        #endregion
    }
}
