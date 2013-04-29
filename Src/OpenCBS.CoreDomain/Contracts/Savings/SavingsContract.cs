using System;
using System.Collections.Generic;
using System.Linq;
using Octopus.CoreDomain.Contracts.Loans;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.Enums;
using Octopus.CoreDomain.Accounting;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.Contracts.Savings.CalculateInterests;
using Octopus.CoreDomain.Events.Saving;
using Octopus.CoreDomain.Products;
using Octopus.Shared;
using Octopus.Shared.Settings;

namespace Octopus.CoreDomain.Contracts.Savings
{
    public delegate void AccountAtMaturityHandler(ISavingsContract savingDeposit, DateTime date, User user);
    [Serializable]
    public abstract class SavingsContract : ISavingsContract
    {
        protected ApplicationSettings ApplicationSettings;

        #region ISavingContract Members
        public int Id { get; set;}
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int? NsgID { get; set; }
        public OCurrency InitialAmount { get; set; }
        public OCurrency EntryFees { get; set; }

        public ISavingsContract TransferAccount { get; set; }
        public OSavingsRollover Rollover { get; set; }

        public double InterestRate { get; set; }
        public ISavingProduct Product { get; set; }
        public OSavingsStatus Status { get; set; }
        public IClient Client { get; set; }
        public User SavingsOfficer { get; set; }
        public Branch Branch { get; set; }
        public int? NsgId { get; set; }
        public User User { get; protected set; }

        public List<SavingEvent> Events { get; protected set; }

        public OCurrency GetBalance()
        {
            return GetBalance(TimeProvider.Now);
        }

        public bool HasPendingEvents()
        {
            return Events.Any(savingEvent => savingEvent.IsPending);
        }

        public OCurrency GetBalance(DateTime date)
        {
            OCurrency retval = 0m;

            foreach (SavingEvent e in Events)
            {
                if ((e is SavingPendingDepositEvent) || (e is SavingPendingDepositRefusedEvent)) continue;
                retval += e.GetBalanceChunk(date);
            }

            return retval;
        }

        public string GetFmtAvailBalance(bool showCurrency)
        {
            return GetFmtAvailBalance(TimeProvider.Now, showCurrency);
        }

        public string GetFmtAvailBalance(DateTime date, bool showCurrency)
        {
            OCurrency avbalance = GetBalance(date) - GetAvailBalance(date);
            bool useCents = Product.Currency.UseCents;
            string fmtBalance = avbalance.GetFormatedValue(useCents);

            if (!showCurrency)
                return fmtBalance;

            string currency = Product.Currency.Code;
            return string.Format("{0} {1}", fmtBalance, currency);
        }

        public OCurrency GetAvailBalance(DateTime date)
        {
            OCurrency retval = 0m;

            foreach (SavingEvent e in Events)
            {
                if (e is SavingBlockCompulsarySavingsEvent )
                        retval += e.Amount;
                if (e is SavingUnblockCompulsorySavingsEvent)
                        retval -= e.Amount;
            }

            return retval;
        }

        public string GetFmtBalance(bool showCurrency)
        {
            return GetFmtBalance(TimeProvider.Now, showCurrency);
        }

        public string GetFmtBalance(DateTime date, bool showCurrency)
        {
            OCurrency balance = GetBalance(date);
            bool useCents = Product.Currency.UseCents;
            string fmtBalance = balance.GetFormatedValue(useCents);
            
            if (!showCurrency) 
                return fmtBalance;

            string currency = Product.Currency.Code;
            return string.Format("{0} {1}", fmtBalance, currency);
        }


        public OCurrency GetBalanceMin(DateTime pDate)
        {
            OCurrency amount = GetBalance(pDate);
            List<SavingEvent> diaryEvents = Events.Where(item => item.Date.Equals(pDate) && item.Deleted == false &&
                    (item.Code == OSavingEvents.Deposit || item.Code == OSavingEvents.Withdraw))
                    .Reverse().ToList();

            OCurrency minimalAmount = amount;

            foreach (SavingEvent savingEvent in diaryEvents)
            {
                if (savingEvent is SavingWithdrawEvent)
                    amount += savingEvent.Amount;
                else
                    amount -= savingEvent.Amount;

                if (amount < minimalAmount)
                    minimalAmount = amount;
            }

            return minimalAmount;
        }

        public virtual List<SavingEvent> FirstDeposit(OCurrency pInitialAmount, DateTime pCreationDate, OCurrency pEntryFees, User pUser, Teller teller)
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
                ProductType = typeof(SavingsBookProduct)
            };

            Events.Add(initialEvent);
            events.Add(initialEvent);

            CreationDate = pCreationDate;

            return events;
        }

        public virtual List<SavingEvent> Withdraw(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees, Teller teller)
        {
            List<SavingEvent> events = new List<SavingEvent>();

            int? tellerId = null;
            if (teller != null) tellerId = teller.Id;

            SavingWithdrawEvent withdrawEvent = new SavingWithdrawEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = true,
                TellerId = tellerId,
                ProductType = Product.GetType()
            };
            events.Add(withdrawEvent);
            Events.Add(withdrawEvent);

            return events;
        }

        public virtual SavingOverdraftFeeEvent ChargeOverdraftFee(DateTime pDate, User pUser)
        {
            SavingOverdraftFeeEvent e = new SavingOverdraftFeeEvent
            {
                Amount = 0m,
                Date = pDate,
                Description = "Overdtaft fee event",
                User = pUser,
                Cancelable = true,
                ProductType = Product.GetType(),
                Fee = 0m,
                Target = this
            };
            Events.Add(e);

            return e;
        }

        public virtual List<SavingEvent> LoanDisbursement(Loan loan, DateTime date, string description, 
                    User user, bool isDesactivateFees, bool isPending, OSavingsMethods savingsMethod, int? pendingEventId, Teller teller)
        {
            var events = new List<SavingEvent>();

            int? tellerId = null;
            if (teller != null && teller.Id != 0) tellerId = teller.Id;

            SavingEvent savingEvent = new SavingLoanDisbursementEvent();
            savingEvent.Amount = loan.Amount;
            savingEvent.Date = date;
            savingEvent.Description = description;
            savingEvent.User = user;
            savingEvent.Cancelable = false;
            savingEvent.IsPending = isPending;
            savingEvent.SavingsMethod = null;
            savingEvent.PendingEventId = pendingEventId;
            savingEvent.TellerId = tellerId;
            savingEvent.LoanEventId = loan.GetNotDeletedDisbursementEvent().Id;
            savingEvent.ProductType = typeof(SavingsBookProduct);

            Events.Add(savingEvent);
            events.Add(savingEvent);

            savingEvent.Fee = Status != OSavingsStatus.Closed && !isDesactivateFees ? loan.GetSumOfFees() : 0;

            return events;
        }

        public virtual List<SavingEvent> RepayLoanFromSaving(Loan loan, int repaymentEventId, DateTime date, OCurrency amount, string description, User user, Teller teller)
        {
            var events = new List<SavingEvent>();
            int? tellerId = null;
            if (teller != null && teller.Id != 0) tellerId = teller.Id;

            SavingEvent repaymentFromSavingEvent = new LoanRepaymentFromSavingEvent
            {
                Amount = amount,
                Date = date,
                Description = description,
                User = user,
                Cancelable = false,
                TellerId = tellerId,
                IsPending = false,
                SavingsMethod = null,
                ProductType = typeof(SavingsBookProduct),
                LoanEventId = repaymentEventId
            };
            events.Add(repaymentFromSavingEvent);
            Events.Add(repaymentFromSavingEvent);
            return events;
        }


        public virtual List<SavingEvent> Deposit(OCurrency pAmount, DateTime pDate, string pDescription, User pUser,
            bool pIsDesactivateFees, bool isPending, OSavingsMethods savingsMethod, int? pendingEventId, Teller teller)
        {
            List<SavingEvent> events = new List<SavingEvent>();
            SavingEvent savingEvent;

            int? tellerId = null;
            if (teller != null) tellerId = teller.Id;

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

            return events;
        }

        public virtual SavingCreditOperationEvent SpecialOperationCredit(OCurrency amount, DateTime date, string description, User user)
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

        public virtual SavingDebitOperationEvent SpecialOperationDebit(OCurrency amount, DateTime date, string description, User user)
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

        

        
        public virtual SavingEvent DebitTransfer(ISavingsContract to, OCurrency amount, OCurrency fee, DateTime date, string description)
        {
            SavingTransferEvent e;
            bool ib = Branch.Id != to.Branch.Id; // inter-branch
            if (ib)
            {
                e = new SavingDebitInterBranchTransferEvent();
            }
            else
            {
                e = new SavingDebitTransferEvent();
            }
            e.Description = ib ? "Inter-branch transfer" : "Transfer";
            e.RelatedContractCode = to.Code;
            e.Amount = amount;
            e.Fee = fee;
            e.Date = date;
            e.User = User.CurrentUser;
            e.Description = description;
            e.ProductType = Product.GetType();
            e.Target = this;
            Events.Add(e);
            return e;
        }

       

        public virtual SavingEvent CreditTransfer(ISavingsContract from, OCurrency amount, DateTime date, string description)
        {
            SavingTransferEvent e;
            bool ib = Branch.Id != from.Branch.Id; // inter-branch
            if (ib)
            {
                e = new SavingCreditInterBranchTransferEvent();
            }
            else
            {
                e = new SavingCreditTransferEvent();
            }
            e.Description = ib ? "Inter-branch transfer" : "Transfer";
            e.RelatedContractCode = from.Code;
            e.Amount = amount;
            e.Fee = 0;
            e.Date = date;
            e.User = User.CurrentUser;
            e.Description = description;
            e.ProductType = Product.GetType();
            e.Target = this;
            Events.Add(e);
            return e;
        }

        public virtual List<SavingEvent> Transfer(ISavingsContract to, OCurrency amount, OCurrency fee, DateTime date, string description)
        {
            List<SavingEvent> events = new List<SavingEvent>(3);
            events.Add(DebitTransfer(to, amount, fee, date, description));
            events.Add(to.CreditTransfer(this, amount, date, description));
            if (GetBalance() < 0)
            {
                SavingOverdraftFeeEvent e = ChargeOverdraftFee(date, User.CurrentUser);
                events.Add(e);
            }
            return events;
        }

        public SavingEvent GetCancelableEvent()
        {
            if (0 == Events.Count) return null;

            SavingEvent evt = Events.OrderBy(item => item.Date).Last(item => !item.Deleted);

            return evt.Cancelable ? evt : null;
        }

        public bool HasCancelableEvents()
        {
            return GetCancelableEvent() != null;
        }

        public void CancelEvent(SavingEvent pSavingEvent)
        {
            pSavingEvent.Deleted = true;
        }

        public SavingEvent CancelLastEvent()
        {
            SavingEvent retval = GetCancelableEvent();
            if (null == retval) return null;

            CancelEvent(retval);
            return retval;
        }

        public virtual List<SavingEvent> Closure(DateTime date, User user)
        {
            SavingEvent evt = GenerateClosureEvent(date);
            if (evt != null)
            {
                Events.Add(evt);
                return new List<SavingEvent> {evt};
            }
            return null;
        }

        public virtual List<SavingEvent> Close(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees, Teller teller, bool isFromClosure)
        {
            throw new NotImplementedException();
        }

//        public virtual List<SavingEvent> RefusePendingDeposit(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, OPaymentMethods method, int? pendingEventId)
//        {
//            throw new NotImplementedException();
//        }

        public virtual List<SavingEvent> RefusePendingDeposit(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, OSavingsMethods method, int? pendingEventId)
        {
            throw new NotImplementedException();
        }

        public virtual List<SavingEvent> Reopen(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees)
        {
            throw new NotImplementedException();
        }

        public virtual List<SavingEvent> SimulateClose(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees, Teller teller)
        {
            throw new NotImplementedException();
        }


        public DateTime GetLastPostingDate()
        {
            SavingInterestsPostingEvent lastPosting = (SavingInterestsPostingEvent) Events.OrderByDescending(
                item => item.Date).FirstOrDefault(item => item is SavingInterestsPostingEvent && !item.Deleted);

            if (lastPosting == null)
            {
                if (Product is SavingsBookProduct)
                {
                    if (((SavingsBookProduct) Product).InterestFrequency == OSavingInterestFrequency.EndOfDay ||
                        ((SavingBookContract) this).UseTermDeposit)
                    {
                        return CreationDate.AddDays(-1);
                    }
                    return CreationDate;
                }
                return CreationDate;
            }
            return lastPosting.Date;
        }

        public DateTime GetLastAccrualDate()
        {
            SavingInterestsAccrualEvent lastClosure = (SavingInterestsAccrualEvent)Events.OrderByDescending(item => item.Date).FirstOrDefault(item => item is SavingInterestsAccrualEvent && !item.Deleted);
            return (lastClosure == null) ? CreationDate : lastClosure.Date;
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pDate, User pUser)
        {
            CalculateInterestsStrategy cis = new CalculateInterestsStrategy(this, pUser, ApplicationSettings.WeekEndDay2);
            return cis.CalculateInterest(pDate);
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime pDate, User pUser)
        {
            PostingInterestsStrategy pis = new PostingInterestsStrategy(this, pUser, ApplicationSettings.WeekEndDay2);
            return pis.PostingInterests(pDate);
        }

        public string GenerateSavingCode(Client pClient, 
                                         int pSavingsCount, 
                                         string pCodeTemplate, 
                                         string pImfCode, 
                                         string pBranchCode)
        {
            switch (pCodeTemplate)
            {
                case "BC/YY/PC-PS/CN-ID":
                    {
                        string clientName = (pClient is Person) ? ((Person)pClient).LastName : pClient.Name;
                        clientName = clientName.Replace(" ", "");
                        string productCode = Product.Code.Replace(" ", "");
                        Code = "S/{0}/{1}/{2}-{3}/{4}-{5}";
                        Code = string.Format(Code,
                                             pBranchCode,
                                             CreationDate.Year,
                                             productCode.Substring(0, Math.Min(productCode.Length, 5)).ToUpper(),
                                             pSavingsCount + 1,
                                             clientName.Substring(0, Math.Min(clientName.Length, 4)).ToUpper(),
                                             pClient.Id);
                        break;
                    }
                case "IC/BC/CS/ID":
                    {
                        string clientCode = pClient.Id.ToString().PadLeft(5, '0');
                        string savingsCount = (pSavingsCount + 1).ToString().PadLeft(2, '0');
                        Code = string.Format("{0}/{1}/{2}/{3}", pImfCode, pBranchCode, savingsCount, clientCode);
                        break;
                    }
            }
            return Code;
        }

        public InstallmentType Periodicity { get { return Product.Periodicity; } }

        public int NumberOfPeriods { get; set; }
       
        #endregion


        #region Protected members
        protected ContractChartOfAccounts FillChartOfAccounts(ChartOfAccounts pChartOfAccounts)
        {
            ContractChartOfAccounts contractChartOfAccounts;
            if (pChartOfAccounts.UniqueAccounts.Count > 0)
                contractChartOfAccounts = new ContractChartOfAccounts(pChartOfAccounts.UniqueAccounts.
                    Select(item => new Account(item.Number, item.Label, 0, item.TypeCode, item.DebitPlus, item.AccountCategory, Product != null && Product.Currency != null ? Product.Currency.Id : 1)).ToList());
            else
                contractChartOfAccounts = new ContractChartOfAccounts(pChartOfAccounts.DefaultAccounts.ToList());

            contractChartOfAccounts.AccountingRuleCollection = pChartOfAccounts.AccountingRuleCollection;

            return contractChartOfAccounts;
        }

        protected SavingEvent EvaluateSavingsEvent(SavingEvent e)
        {
            if (!e.Amount.HasValue) e.Amount = 0m;
            if (!e.Fee.HasValue) e.Fee = 0m;
            if (e is SavingInterestsAccrualEvent || e is SavingInterestsPostingEvent)
            {
                if (e.Amount > 0)
                {
                    e.Amount = Product.Currency.UseCents
                                   ? Math.Round(e.Amount.Value, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(e.Amount.Value, 0, MidpointRounding.AwayFromZero);
                    if (e.Amount > 0)
                        return e;
                }
            }

            if(e is SavingAgioEvent || e is SavingManagementFeeEvent)
            {
                if (e.Fee > 0)
                {
                    e.Fee = Product.Currency.UseCents ? Math.Round(e.Fee.Value, 2, MidpointRounding.AwayFromZero) :
                        Math.Round(e.Fee.Value, 0, MidpointRounding.AwayFromZero);
                    if (e.Fee > 0) 
                        return e;
                }
            }

            //return filteredEvents;
            return null;
        }

        protected List<SavingEvent> AddSavingEvent(List<SavingInterestsAccrualEvent> pEvents)
        {
            List<SavingEvent> retval = new List<SavingEvent>();
            List<SavingEvent> events = pEvents.ConvertAll<SavingEvent>(delegate(SavingInterestsAccrualEvent e) { return e; });
            
            foreach (SavingEvent e in events)
            {
                SavingEvent savEvent = EvaluateSavingsEvent(e);
                if (savEvent != null)
                {
                    Events.Add(savEvent);
                    retval.Add(savEvent);
                }
            }

            return retval;
        }

        protected List<SavingEvent> AddSavingEvent(List<SavingInterestsPostingEvent> pEvents)
        {
            List<SavingEvent> retval = new List<SavingEvent>();
            List<SavingEvent> events = pEvents.ConvertAll<SavingEvent>(delegate(SavingInterestsPostingEvent e) { return e; });
            
            foreach (SavingEvent e in events)
            {
                SavingEvent savEvent = EvaluateSavingsEvent(e);
                if (savEvent != null)
                {
                    Events.Add(e);
                    retval.Add(savEvent);
                }
            }

            return retval;
        }

        protected SavingInterestsPostingEvent PostPayableInterests(DateTime pDate, User pUser)
        {
            OCurrency interestsToPost = 0;

            if (interestsToPost > 0)
            {
                SavingInterestsPostingEvent postingEvent = new SavingInterestsPostingEvent()
                {
                    Date = pDate,
                    Amount = interestsToPost,
                    User = pUser,
                    Cancelable = true,
                    Description = string.Format("Posting interests for period : {0:d} to {1:d}", GetLastPostingDate(), pDate),
                    ProductType = Product.GetType()
                };

                Events.Add(postingEvent);
                return postingEvent;
            }

            return null;
        }
        #endregion

        #region public Members
        public SavingEvent  AddSavingEvent(SavingEvent e)
        {
            SavingEvent savEvent = EvaluateSavingsEvent(e);
            if (savEvent != null)
            {
                Events.Add(e);
            }

            return savEvent;
        }

        public SavingCreditTransferEvent CreditTransfer(OCurrency pAmount, ISavingsContract pDebitAccount, DateTime pDate, string pDescription, User pUser)
        {
            SavingCreditTransferEvent transferEvent = new SavingCreditTransferEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Fee = 0,
                Cancelable = false,
                RelatedContractCode = pDebitAccount.Code,
                ProductType = Product.GetType(),
                ContracId = Id
            };
            Events.Add(transferEvent);

            return transferEvent;
        }

        public virtual List<SavingEvent> DebitTransfer(OCurrency pAmount, ISavingsContract pCreditAccount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees)
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
                ProductType = Product.GetType(),
                ContracId = Id
            };
            events.Add(transferEvent);
            Events.Add(transferEvent);

            return events;
        }

        public override string ToString()
        {
            return Code;
        }

        public SavingClosureEvent GenerateClosureEvent(DateTime date)
        {
            SavingClosureEvent scle = new SavingClosureEvent
            {
                Amount = 0m,
                Cancelable = false,
                Date = date,
                Description = "Closure event : " + Code,
                Fee = 0m,
                User = User,
                ProductType = Product.GetType(),
                Target = this,
                Branch = Branch,
                Currency = Product.Currency,
                ContracId = Id
            };

            if (Events.Find(item => item.Code == scle.Code && item.Date.Date == scle.Date.Date && item is SavingClosureEvent) != null)
                scle = null;

            return scle;
        }
        #endregion

        #region ICloneable Members

        public virtual object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
