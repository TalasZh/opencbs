using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.CoreDomain.Contracts.Savings;
using Octopus.CoreDomain.Events.Saving;
using Octopus.CoreDomain.Products;
using Octopus.Shared;
using Octopus.Enums;
using Octopus.CoreDomain;
using Octopus.Shared.Settings;
using Octopus.CoreDomain.Accounting;

namespace Octopus.Test.CoreDomain.Contracts.Savings
{
	[TestFixture]
	public class TestSaving 
	{
	    private SavingsBookProduct _product;
	    private SavingBookContract _saving;

        [SetUp]
        public void SetUp()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);

            Currency currency = new Currency() { UseCents = true, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };

            _product = new SavingsBookProduct
            {
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                Id = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfWeek,
                ManagementFeeFreq = new InstallmentType
                {
                    Id = 1, 
                    Name = "Monthly", 
                    NbOfDays = 0, 
                    NbOfMonths = 1
                },
                AgioFeesFreq = new InstallmentType
                {
                    Id = 2,
                    Name = "Daily",
                    NbOfDays = 1,
                    NbOfMonths = 0
                },
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 0,
                Currency = currency
            };

            _saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                                             new DateTime(2007, 08, 11), _product, null);
            _saving.FirstDeposit(12m, new DateTime(2007, 08, 11), 0, new User(), new Teller());

            User.CurrentUser = new User {Id = 1};

            Teller.CurrentTeller = new Teller();
        }

		[Test]
		public void Get_Set_Product()
		{
			SavingsBookProduct product = new SavingsBookProduct { Id = 1 };
		    SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
		                                                       new User(), TimeProvider.Today, null) {Product = product};
            saving.FirstDeposit(12m, TimeProvider.Today, 0, new User(), Teller.CurrentTeller);
			Assert.AreEqual(product, saving.Product);
		}

		[Test]
		public void Get_Set_Id()
		{
		    SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
		                                                       new User(), TimeProvider.Today, null) {Id = 1};
            saving.FirstDeposit(12m, TimeProvider.Today, 0, new User(), Teller.CurrentTeller);
			Assert.AreEqual(1, saving.Id);
		}

		[Test]
		public void Get_Set_Code_ForPerson()
		{
		    SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
		                                                       new User(),
		                                                       new DateTime(2007, 08, 11), null)
		                                    {Code = "", Product = new SavingsBookProduct() {Id = 2, Code = "SavingProduct"}};
            saving.FirstDeposit(12m, new DateTime(2007, 08, 11), 0, new User(), Teller.CurrentTeller);

			saving.GenerateSavingCode(new Person { Id = 2, FirstName="Vincent", LastName = "Guigui" }, 3, "BC/YY/PC-PS/CN-ID", "IMF", "BC");
			Assert.AreEqual("S/BC/2007/SAVIN-4/GUIG-2", saving.Code);

            saving.GenerateSavingCode(new Person { Id = 2, FirstName = "Vincent", LastName = "Guigui" }, 4, "IC/BC/CS/ID", "IMF", "BC");
            Assert.AreEqual("IMF/BC/05/00002", saving.Code);
		}

		[Test]
		public void Get_Set_Code_ForCorporate()
		{
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
		                                                       new DateTime(2007, 08, 11), null)
		                                    {Code = "", Product = new SavingsBookProduct() {Id = 2, Code = "SavingProduct"}};
            saving.FirstDeposit(12m, new DateTime(2007, 08, 11), 0, new User(), Teller.CurrentTeller);

            saving.GenerateSavingCode(new Corporate { Id = 2, Name = "Guigui" }, 0, "BC/YY/PC-PS/CN-ID", "IMF", "BC");
			Assert.AreEqual("S/BC/2007/SAVIN-1/GUIG-2", saving.Code);

            saving.GenerateSavingCode(new Corporate { Id = 2, Name = "Guigui" }, 1, "IC/BC/CS/ID", "IMF", "BC");
            Assert.AreEqual("IMF/BC/02/00002", saving.Code);
		}

        [Test]
		public void Get_Set_CreationDate()
		{
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), new DateTime(2008, 10, 21), null);
            saving.FirstDeposit(12m, new DateTime(2008, 10, 21), 0, new User(), Teller.CurrentTeller);
            Assert.AreEqual(new DateTime(2008, 10, 21), saving.CreationDate);
		}

        [Test]
        public void Get_Set_ClosedDate()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
                                                               new User(),
                                                               new DateTime(2008, 10, 21), null)
                                            {ClosedDate = new DateTime(2009, 01, 01)};
            saving.FirstDeposit(12m, new DateTime(2008, 10, 21), 0, new User(), Teller.CurrentTeller);
            Assert.AreEqual(new DateTime(2009, 01, 01), saving.ClosedDate);
        }

		[Test]
		public void Get_Set_Events()
		{
			List<SavingEvent> events = new List<SavingEvent>();
			SavingInitialDepositEvent eventDeposit = new SavingInitialDepositEvent
			{
				Id = 1,
				Date = new DateTime(2008, 10, 18),
				Description = "First deposit of 100",
				Amount = 100
			};
			events.Add(eventDeposit);
			SavingWithdrawEvent eventWithdraw = new SavingWithdrawEvent
			{
				Id = 2,
				Date = new DateTime(2008, 10, 08),
				Description = "2nd withdraw of 75",
				Amount = 75
			};
			events.Add(eventWithdraw);
		    SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
		                                                       new User(),
		                                                       new DateTime(2008, 08, 18), null);
            saving.FirstDeposit(100, new DateTime(2008, 08, 18), null, new User(), Teller.CurrentTeller);
			saving.Events.AddRange(events);
			Assert.AreEqual(events.Count+1, saving.Events.Count);
			saving.Events.Sort();
			events.Sort();
			Assert.AreEqual(100m, saving.Events[0].Amount.Value);
			Assert.AreEqual(75m, saving.Events[1].Amount.Value);
			Assert.AreEqual(100m, saving.Events[2].Amount.Value);
		}

		[Test]
		public void Get_Set_InterestRate()
		{
			double interestRate = 2.0;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null) { InterestRate = interestRate };
			Assert.AreEqual(interestRate, saving.InterestRate);
		}

		[Test]
		public void Get_Set_InitialAmount()
		{
			List<SavingEvent> events = new List<SavingEvent>
               	{
               		new SavingWithdrawEvent {Amount = 75, Date = new DateTime(2008, 10, 21)},
               		new SavingDepositEvent {Amount = 12.46m, Date = new DateTime(2008, 08, 18)},
               		new SavingDepositEvent {Amount = 86, Date = new DateTime(2008, 09, 01)}
               	};
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), new DateTime(2008, 07, 18), null);
            saving.InitialAmount = 1000;
            saving.FirstDeposit(1000, new DateTime(2008, 07, 18), null, new User(), Teller.CurrentTeller);

			saving.Events.AddRange(events);

			Assert.AreEqual(1023.46m, saving.GetBalance().Value);
			Assert.AreEqual(1000m, saving.InitialAmount.Value);
		}

        [Test]
        public void Get_Set_FlatWithdrawFees()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null) { FlatWithdrawFees = 5m };
            Assert.AreEqual(5m, saving.FlatWithdrawFees.Value);
        }

        [Test]
        public void Get_Set_RateWithdrawFees()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null) { RateWithdrawFees = 0.1d };
            Assert.AreEqual(0.1d, saving.RateWithdrawFees.Value);
        }

        [Test]
        public void Get_Set_FlatTransferFees()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null) { FlatTransferFees = 5m };
            Assert.AreEqual(5m, saving.FlatTransferFees.Value);
        }

        [Test]
        public void Get_Set_RateTransferFees()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), TimeProvider.Today, null) { RateTransferFees = 0.1d };
            Assert.AreEqual(0.1d, saving.RateTransferFees.Value);
        }

		[Test]
		public void Get_Balance()
		{
			List<SavingEvent> events = new List<SavingEvent>
           		{
           			new SavingDepositEvent {Amount = 3500.46m},
           			new SavingWithdrawEvent {Amount = 20},
           			new SavingDepositEvent {Amount = 100},
           			new SavingWithdrawEvent {Amount = 30}
           		};

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), new DateTime(2008, 07, 18), null);
            saving.FirstDeposit(1000, new DateTime(2008, 07, 18), null, new User(), Teller.CurrentTeller);

			saving.Events.AddRange(events);

            Assert.AreEqual(4550.46m, saving.GetBalance().Value);
        }
        
        [Test]
        public void Get_Balance_At_Date()
        {
            SavingsBookProduct product = new SavingsBookProduct()
            {
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 0
            };

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), new DateTime(2009, 01, 01), 
                product, null) { FlatWithdrawFees = 0, DepositFees = 5, CloseFees = 6, ManagementFees = 7};
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

//            saving.Deposit(100, new DateTime(2009, 02, 01), "depot", new User(), false, false, OPaymentMethods.Cash, null, null);
            saving.Deposit(100, new DateTime(2009, 02, 01), "depot", new User(), false, false, OSavingsMethods.Cash, null, null);
            saving.Withdraw(230, new DateTime(2009, 02, 03), "retrait", new User(), false, null);

            Assert.AreEqual(saving.GetBalance(new DateTime(2009, 01, 31)), 1000);
            Assert.AreEqual(saving.GetBalance(new DateTime(2009, 02, 01)), 1100);
            Assert.AreEqual(saving.GetBalance(new DateTime(2009, 02, 02)), 1100);
            Assert.AreEqual(saving.GetBalance(new DateTime(2009, 02, 03)), 870);
        }
        
        [Test]
        public void Get_BalanceMin_At_Date()
        {
            SavingsBookProduct product = new SavingsBookProduct()
            {
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 0
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), new DateTime(2009, 01, 01),
                product, null) { FlatWithdrawFees = 0, DepositFees = 5, CloseFees = 6, ManagementFees = 7 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Withdraw(100, new DateTime(2009, 02, 02), "retrait", new User(), false, null);
//            saving.Deposit(230, new DateTime(2009, 02, 02), "depot", new User(), false, false, OPaymentMethods.Cash, null, null);
            saving.Deposit(230, new DateTime(2009, 02, 02), "depot", new User(), false, false, OSavingsMethods.Cash, null, null);

            Assert.AreEqual(saving.GetBalanceMin(new DateTime(2009, 01, 01)), 1000);
            Assert.AreEqual(saving.GetBalanceMin(new DateTime(2009, 02, 01)), 1000);
            Assert.AreEqual(saving.GetBalanceMin(new DateTime(2009, 02, 02)), 900);
        }

        [Test]
        public void Cancel_Last_Event()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfWeek
            };

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  
                new User(), new DateTime(2009, 01, 01), product, null) { InterestRate = 0.1 };
            saving.FirstDeposit(100, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            Assert.AreEqual(saving.GetBalance(), 100);
            
            saving.CancelLastEvent();

            Assert.AreEqual(saving.GetBalance(), 100);}
        
        [Test]
        public void Cancel_Last_Deposit_Event_After_Closure()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), new DateTime(2009, 01, 01), 
                _product, null) { InterestRate = 0.1, DepositFees = 5, CloseFees = 6, ManagementFees = 7, AgioFees = 8, OverdraftFees = 9};
            saving.FirstDeposit(100, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 01, 02), new User());
//            saving.Deposit(50, new DateTime(2009, 01, 02), "depot", new User(), false, false, OPaymentMethods.Cash, null, null);
            saving.Deposit(50, new DateTime(2009, 01, 02), "depot", new User(), false, false, OSavingsMethods.Cash, null, null);

            Assert.AreEqual(saving.GetBalance(), 150);
           
            saving.Closure(new DateTime(2009, 01, 05), new User());
            Assert.AreEqual(saving.GetBalance(), 150);

            saving.CancelLastEvent();
            Assert.AreEqual(saving.GetBalance(), 150);
        }

        [Test]
        public void Cancel_Last_Withdraw_Event_After_Closure()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), new DateTime(2009, 01, 01), 
                _product, null) { InterestRate = 0.1, FlatWithdrawFees = 0, AgioFees = 0.1 };
            saving.FirstDeposit(100, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);
            
            saving.Closure(new DateTime(2009, 01, 02), new User());
            saving.Withdraw(50, new DateTime(2009, 01, 02), "retrait", new User(), false, null);

            Assert.AreEqual(saving.GetBalance(), 50);

            saving.Closure(new DateTime(2009, 01, 05), new User());
            Assert.AreEqual(saving.GetBalance(), 50);
            saving.CancelLastEvent();
            Assert.AreEqual(saving.GetBalance(), 50);
        }

        #region Transfer
        private SavingBookContract GetSaving()
        {
            ApplicationSettings settings = ApplicationSettings.GetInstance("");
            ChartOfAccounts chart = ChartOfAccounts.GetInstance(User.CurrentUser);
            SavingBookContract saving = new SavingBookContract(settings, User.CurrentUser, DateTime.Today, null);
            saving.Product = _product;
            saving.Branch = new Branch {Id = 1, Name = "Default"};
            return saving;
        }

        [Test]
        public void Transfer_Event()
        {
//            Assert.Ignore();
            SavingBookContract from = GetSaving();
            from.FirstDeposit(100, DateTime.Today, 0, User.CurrentUser, Teller.CurrentTeller);
            SavingBookContract to = GetSaving();

            List<SavingEvent> e = from.Transfer(to, 20, 0, DateTime.Today, "Transfer description");

            Assert.AreEqual(2, e.Count());
            Assert.IsInstanceOfType(typeof(SavingDebitTransferEvent), e[0]);
//            Assert.IsInstanceOfType(typeof(SavingCreditTransferEvent), e[1]);
        }
        
        [Test]
        public void Transfe_EventTargets()
        {
//            Assert.Ignore();
            SavingBookContract from = GetSaving();
            from.FirstDeposit(100, DateTime.Today, 0, User.CurrentUser, Teller.CurrentTeller);
            SavingBookContract to = GetSaving();

            List<SavingEvent> e = from.Transfer(to, 50, 0, DateTime.Today, "Transfer description");

            Assert.AreEqual(e[0].Target, from);
//            Assert.AreEqual(e[1].Target, to);
        }

        [Test]
        public void InterBranchTransfer_Event()
        {
//            Assert.Ignore();
            SavingBookContract from = GetSaving();
            from.FirstDeposit(100, DateTime.Today, 0, User.CurrentUser, Teller.CurrentTeller);
            SavingBookContract to = GetSaving();
            to.Branch = new Branch {Id = 2, Name = "Another branch"};

            List<SavingEvent> e = from.Transfer(to, 20, 0, DateTime.Today, "Transfer description");

            Assert.IsInstanceOfType(typeof(SavingDebitInterBranchTransferEvent), e[0]);
//            Assert.IsInstanceOfType(typeof(SavingCreditInterBranchTransferEvent), e[1]);
        }

        [Test]
        public void Transfer_AmountAndFee()
        {
//            Assert.Ignore();
            SavingBookContract from = GetSaving();
            from.FirstDeposit(100, DateTime.Today, 0, User.CurrentUser, Teller.CurrentTeller);
            SavingBookContract to = GetSaving();

            List<SavingEvent> e = from.Transfer(to, 50, 1, DateTime.Today, "Transfer description");

            Assert.AreEqual(e[0].Amount, 50);
            Assert.AreEqual(e[0].Fee, 1);
//            Assert.AreEqual(e[1].Amount, 50);
//            Assert.AreEqual(e[1].Fee, 0);
        }

        [Test]
        public void Transfer_RelatedCode()
        {
//            Assert.Ignore();
            SavingBookContract from = GetSaving();
            from.FirstDeposit(100, DateTime.Today, 0, User.CurrentUser, Teller.CurrentTeller);
            from.Code = "SOURCE";
            SavingBookContract to = GetSaving();
            to.Code = "TARGET";

            List<SavingEvent> e = from.Transfer(to, 50, 1, DateTime.Today, "Transfer description");

            Assert.IsInstanceOfType(typeof (SavingTransferEvent), e[0]);
//            Assert.IsInstanceOfType(typeof (SavingTransferEvent), e[1]);

            SavingTransferEvent e1 = (SavingTransferEvent) e[0];
//            SavingTransferEvent e2 = (SavingTransferEvent) e[1];

            Assert.AreEqual(e1.RelatedContractCode, "TARGET");
//            Assert.AreEqual(e2.RelatedContractCode, "SOURCE");
        }

        [Test]
        public void Transfer_Balance()
        {
            SavingBookContract from = GetSaving();
            from.FirstDeposit(100, DateTime.Today, 0, User.CurrentUser, Teller.CurrentTeller);
            SavingBookContract to = GetSaving();

            from.Transfer(to, 50, 1, DateTime.Today, "Transfer description");

            Assert.AreEqual(from.GetBalance(), 49m);
            Assert.AreEqual(to.GetBalance(), 50m);
        }

        [Test]
        public void Tansfer_Overdraft()
        {
//            Assert.Ignore();
            SavingBookContract from = GetSaving();
            from.Product.BalanceMin = -100;
            from.OverdraftFees = 5;
            SavingBookContract to = GetSaving();

            List<SavingEvent> e = from.Transfer(to, 50, 1, DateTime.Today, "Transfer description");

            Assert.AreEqual(3, e.Count());
            Assert.IsInstanceOfType(typeof (SavingOverdraftFeeEvent), e[2]);
            Assert.AreEqual(from.GetBalance(), -56m);
            Assert.AreEqual(to.GetBalance(), 50m);
        }
        #endregion Transfer

        [Test]
        public void CloseAccount()
        {
            Assert.Ignore();
           _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;

            SavingBookContract saving = new SavingBookContract(
                ApplicationSettings.GetInstance(""), 
                
                new User(),
                new DateTime(2009, 01, 01), 
                null)
                {
                    Product = _product, 
                    InterestRate = 0.1
                };

            List<SavingEvent> events = saving.Close(
                                                        new DateTime(2009, 02, 15), 
                                                        new User(), 
                                                        "Close savings contract", 
                                                        false, 
                                                        Teller.CurrentTeller, 
                                                        false
                                                    );

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 10);
            Assert.AreEqual(accrualEvents, 6);
            Assert.AreEqual(postingEvents, 2);
            Assert.AreEqual(saving.GetBalance(), 1640);
            Assert.AreEqual(saving.Status, OSavingsStatus.Closed);
        }

        #region Closure Day of creation
        [Test]
        public void CalculateInterest_CashMode_Daily_EndOfDay_OneClosure_DayOfCreation()
        {
            _product.InterestFrequency = OSavingInterestFrequency.EndOfDay;

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfDay_OneClosure_DayOfCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(
                                                                OGeneralSettings.ACCOUNTINGPROCESS, 
                                                                OAccountingProcesses.Accrual);
            
            _product.InterestFrequency = OSavingInterestFrequency.EndOfDay;

            SavingBookContract saving = new SavingBookContract(
                                                ApplicationSettings.GetInstance(""), 
                                                
                                                new User(),
                                                new DateTime(2009, 01, 15), 
                                                null)
                                            {
                                                Product = _product, 
                                                InterestRate = 0.1
                                            };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEventsCount = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEventsCount = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;
            
            Assert.AreEqual(31, events.Count);
            Assert.AreEqual(15, accrualEventsCount);
            Assert.AreEqual(15, postingEventsCount);
            Assert.AreEqual(4142.72m, saving.GetBalance().Value);
        }

        [Test]
        public void CalculateInterest_CashMode_Daily_EndOfWeek_OneClosure_DayOfCreation()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfWeek_OneClosure_DayOfCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Daily_EndOfMonth_OneClosure_DayOfCreation()
        {
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfMonth_OneClosure_DayOfCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(
                                                            OGeneralSettings.ACCOUNTINGPROCESS, 
                                                            OAccountingProcesses.Accrual);

            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(
                                            ApplicationSettings.GetInstance(""), 
                                            
                                            new User(),
                                            new DateTime(2009, 01, 15), 
                                            null)
                                            {
                                                Product = _product, 
                                                InterestRate = 0.1
                                            };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEventsCount = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 16);
            Assert.AreEqual(accrualEventsCount, 15);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(1000m, saving.GetBalance().Value);
        }

        [Test]
        public void CalculateInterest_CashMode_Daily_EndOfYear_OneClosure_DayOfCreation()
        {
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfYear_OneClosure_DayOfCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Weekly_EndOfWeek_OneClosure_DayOfCreation()
        {
            _product.InterestBase = OSavingInterestBase.Weekly;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfWeek_OneClosure_DayOfCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            _product.InterestBase = OSavingInterestBase.Weekly;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Weekly_EndOfMonth_OneClosure_DayOfCreation()
        {
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfMonth_OneClosure_DayOfCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Weekly_EndOfYear_OneClosure_DayOfCreation()
        {
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfYear_OneClosure_DayOfCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Monthly_EndOfMonth_OneClosure_DayOfCreation()
        {
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Monthly_EndOfMonth_OneClosure_DayOfCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Monthly_EndOfYear_OneClosure_DayOfCreation()
        {
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Monthly_EndOfYear_OneClosure_DayOfCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 15), 0, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        #endregion

        #region Closure after One Day
        [Test]
        public void CalculateInterest_CashMode_Daily_EndOfDay_OneClosure_AfterOneDay()
        {
            _product.InterestFrequency = OSavingInterestFrequency.EndOfDay;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
                                                                new User(),
                                                               new DateTime(2009, 01, 01), null)
                                            {Product = _product, InterestRate = 0.1};
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfDay_OneClosure_AfterOneDay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfDay;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

       [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfWeek_OneClosure_AfterOneDay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(
                                                            OGeneralSettings.ACCOUNTINGPROCESS, 
                                                            OAccountingProcesses.Accrual);

            SavingBookContract saving = new SavingBookContract(
                                            ApplicationSettings.GetInstance(""), 
                                            
                                            new User(),
                                            new DateTime(2009, 01, 1), 
                                            null)
                                            {
                                                Product = _product, 
                                                InterestRate = 0.1, 
                                                ManagementFees = 10, 
                                                AgioFees = 0.1
                                            };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 2, 1, 1, 1), new User() { Id = 1 });

            int accrualEventsCount = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEventsCount = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(3, events.Count);
            Assert.AreEqual(2, accrualEventsCount);
            Assert.AreEqual(0, postingEventsCount);
            Assert.AreEqual(1000m, saving.GetBalance().Value);
        }

      

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfMonth_OneClosure_AfterOneDay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 1), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 2, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 3);
            Assert.AreEqual(accrualEvents, 2);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        
        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfYear_OneClosure_AfterOneDay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(
                                                                OGeneralSettings.ACCOUNTINGPROCESS, 
                                                                OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(
                ApplicationSettings.GetInstance(""), 
                new User(),
                new DateTime(2009, 01, 1), null) 
                { Product = _product, InterestRate = 0.1, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 2, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(3,  events.Count);
            Assert.AreEqual(2, accrualEvents);
            Assert.AreEqual(0, postingEvents);
            Assert.AreEqual(1000m,saving.GetBalance().Value);
        }

        [Test]
        public void CalculateInterest_CashMode_Monthly_EndOfMonth_OneClosure_AfterOnDay()
        {
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 02, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Monthly_EndOfMonth_OneClosure_AfterOneDay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 02, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Monthly_EndOfYear_OneClosure_AfterOnDay()
        {
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 02, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Monthly_EndOfYear_OneClosure_AfterOneDay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 02, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Weekly_EndOfWeek_OneClosure_AfterOnDay()
        {
            _product.InterestBase = OSavingInterestBase.Weekly;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 02, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfWeek_OneClosure_AfterOneDay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 02, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Weekly_EndOfMonth_OneClosure_AfterOnDay()
        {
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 02, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfMonth_OneClosure_AfterOneDay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 02, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Weekly_EndOfYear_OneClosure_AfterOnDay()
        {
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 02, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfYear_OneClosure_AfterOneDay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), 0, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 02, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        #endregion

        #region Closure after One Week
        
        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfDay_OneClosure_AfterOneWeek()
        {
            
            ApplicationSettings.GetInstance("").UpdateParameter(
                OGeneralSettings.ACCOUNTINGPROCESS, 
                OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfDay;
            SavingBookContract saving = new SavingBookContract(
                ApplicationSettings.GetInstance(""),
                new User(),
                new DateTime(2009, 01, 01), 
                null) 
                { Product = _product, InterestRate = 0.1, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 01, 08), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(8, accrualEvents);
            Assert.AreEqual(8, postingEvents);
            Assert.AreEqual(2125.87m, saving.GetBalance().Value);
        }

       [Test]
        public void CalculateInterest_AccrualhMode_Daily_EndOfYear_OneClosure_AfterOneWeek()
        {
           ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = 
                new SavingBookContract(
                    ApplicationSettings.GetInstance(""), 
                     new User(),
                    new DateTime(2009, 01, 01), 
                    null)
                    {
                        Product = _product, 
                        InterestRate = 0.1, 
                        AgioFees = 8
                    };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 01, 08), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(8, accrualEvents);
            Assert.AreEqual(0, postingEvents);
            Assert.AreEqual(1000m, saving.GetBalance().Value);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfMonth_OneClosure_AfterOneWeek()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(
                ApplicationSettings.GetInstance(""),
                
                new User(),
                new DateTime(2009, 01, 01),
                null) 
                { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 01, 08), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(8,  accrualEvents);
            Assert.AreEqual(0, postingEvents);
            Assert.AreEqual(1000m, saving.GetBalance().Value);
        }

       [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfWeek_OneClosure_AfterOneWeek()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            SavingBookContract saving = new SavingBookContract(
                ApplicationSettings.GetInstance(""), 
                new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 01, 08), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(8,  accrualEvents);
            Assert.AreEqual(1, postingEvents);
            Assert.AreEqual(1400m, saving.GetBalance().Value);
        }

      

        [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfWeek_OneClosure_AfterOneWeek()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 08, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(3, events.Count);
            Assert.AreEqual(1, accrualEvents);
            Assert.AreEqual(1, postingEvents);
            Assert.AreEqual(1100m, saving.GetBalance().Value);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Week_EndOfMonth_OneClosure_AfterOneWeek()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 08, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(2, events.Count);
            Assert.AreEqual(1, accrualEvents);
            Assert.AreEqual(0, postingEvents);
            Assert.AreEqual(1000m, saving.GetBalance().Value);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Week_EndOfYear_OneClosure_AfterOneWeek()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 08, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 2);
            Assert.AreEqual(accrualEvents, 1);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Monthly_EndOfMonth_OneClosure_AfterOneWeek()
        {
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 08, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
           Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Monthly_EndOfMonth_OneClosure_AfterOneWeek()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 08, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_CashMode_Monthly_EndOfYear_OneClosure_AfterOneWeek()
        {
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
               new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 08, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Monthly_EndOfYear_OneClosure_AfterOneWeek()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 08, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        #endregion

        #region Closure One Day before Posting Date

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfWeek_OneClosure_OneDayBefore_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 01, 4), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(accrualEvents, 4);
            Assert.AreEqual(postingEvents, 0);
             Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfMonth_OneClosure_OneDayBefore_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 01, 31), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(accrualEvents, 31);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfYear_OneClosure_OneDayBefore_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 12, 31), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(accrualEvents, 365);
            Assert.AreEqual(postingEvents, 0);
              Assert.AreEqual(saving.GetBalance(), 890);
        }

        [Test]
        public void CalculateInterest_CashMode_Weekly_EndOfWeek_OneClosure_OneDayBefore_PostingDate()
        {
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 04, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfWeek_OneClosure_OneDayBefore_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 04, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }


        [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfMonth_OneClosure_OneDayBefore_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 31, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 5);
            Assert.AreEqual(accrualEvents, 4);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

       [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfYear_OneClosure_OneDayBefore_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 12, 31, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 64);
            Assert.AreEqual(accrualEvents, 52);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 890);
        }

        [Test]
        public void CalculateInterest_CashMode_Monthly_EndOfMonth_OneClosure_OneDayBefore_PostingDate()
        {
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 31, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Monthly_EndOfMonth_OneClosure_OneDayBefore_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 31, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

       [Test]
        public void CalculateInterest_AccrualMode_Monthly_EndOfYear_OneClosure_OneDayBefore_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 12, 31, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 23);
            Assert.AreEqual(accrualEvents, 11);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 890);
        }
        
        #endregion

        #region Closure On Posting Date
      

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfYear_OneClosure_On_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 0, AgioFees = 0 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2010, 01, 01), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;
            OCurrency interests = saving.Events.Where(item => item is SavingInterestsAccrualEvent).Sum(item => item.Amount.Value);

            Assert.AreEqual(accrualEvents, 366);
            Assert.AreEqual(postingEvents, 1);
            Assert.AreEqual(saving.GetBalance(), 37500);
        }

        

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfMonth_OneClosure_On_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2008, 01, 01), null) { Product = _product, InterestRate = 1, ManagementFees = 0, AgioFees = 0 };
            saving.FirstDeposit(100, new DateTime(2008, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2008, 02, 01), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;
            OCurrency interests = saving.Events.Where(item => item is SavingInterestsAccrualEvent).Sum(item => item.Amount.Value);

            Assert.AreEqual(accrualEvents, 32);
            Assert.AreEqual(postingEvents, 1);
            Assert.AreEqual(saving.GetBalance(), 3300);
        }

        [Test]
        public void CalculateInterest_CashMode_Daily_EndOfWeek_OneClosure_On_PostingDate()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.01, ManagementFees = 0, AgioFees = 0 };
            saving.FirstDeposit(100, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 01, 05), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;
            OCurrency interests = saving.Events.Where(item => item is SavingInterestsAccrualEvent).Sum(item => item.Amount.Value);

            Assert.AreEqual(accrualEvents, 5);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 100);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfWeek_OneClosure_On_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.01, AgioFees = 0 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 01, 05), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;
            OCurrency interests = saving.Events.Where(item => item is SavingInterestsAccrualEvent).Sum(item => item.Amount.Value);

            Assert.AreEqual(accrualEvents, 5);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

       [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfWeek_OneClosure_On_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 05, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        
        [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfMonth_OneClosure_On_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 0, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 02, 01, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 6);
            Assert.AreEqual(accrualEvents, 4);
            Assert.AreEqual(postingEvents, 1);
            Assert.AreEqual(saving.GetBalance(), 1400);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Weekly_EndOfYear_OneClosure_On_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Weekly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2010, 01, 01, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(accrualEvents, 52);
            Assert.AreEqual(postingEvents, 1);
            Assert.AreEqual(saving.GetBalance(), 5793);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Monthly_EndOfMonth_OneClosure_On_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 0, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 02, 01, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 3);
            Assert.AreEqual(accrualEvents, 1);
            Assert.AreEqual(postingEvents, 1);
            Assert.AreEqual(saving.GetBalance(), 1100);
        }

       [Test]
        public void CalculateInterest_AccrualMode_Monthly_EndOfYear_OneClosure_On_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestBase = OSavingInterestBase.Monthly;
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.1, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2010, 01, 01, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(accrualEvents, 12);
            Assert.AreEqual(postingEvents, 1);
            Assert.AreEqual(2014.0, saving.GetBalance().Value);
        }

        #endregion

        #region Closure Many days, months,... or years after Creation
        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfMonth_OneClosure_65Days_AfterCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 1, ManagementFees = 0, AgioFees = 0 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 03, 07), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;
            //OCurrency interests = saving.Events.Where(item => item is SavingInterestsAccrualEvent).Sum(item => item.Amount.Value);

            Assert.AreEqual(accrualEvents, 66);
            Assert.AreEqual(postingEvents, 2);
            Assert.AreEqual(saving.GetBalance(), 958000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfMonth_OneClosure_TwoMonthsAndOneBrouette_AfterCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfMonth;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _product, InterestRate = 0.01, ManagementFees = 10, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 02, 04), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;
            //OCurrency interests = saving.Events.Where(item => item is SavingInterestsAccrualEvent).Sum(item => item.Amount.Value);

            Assert.AreEqual(accrualEvents, 35);
            Assert.AreEqual(postingEvents, 1);
            Assert.AreEqual(saving.GetBalance(), 1309.9);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfWeek_OneClosure()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2008, 01, 21), null) { Product = _product, InterestRate = 0.0007, AgioFees = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2008, 01, 21), null, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2008, 01, 26), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;
            OCurrency interests = saving.Events.Where(item => item is SavingInterestsAccrualEvent).Sum(item => item.Amount.Value);

            Assert.AreEqual(accrualEvents, 6);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }
        
        #endregion

        [Test]
        public void HasCancelableEvent()
        {
            Assert.AreEqual(_saving.HasCancelableEvents(), false);
//            _saving.Deposit(500m, new DateTime(2008, 09, 01), "deposit", new User(), false, false, OPaymentMethods.Cash, null, null);
            _saving.Deposit(500m, new DateTime(2008, 09, 01), "deposit", new User(), false, false, OSavingsMethods.Cash, null, null);
            Assert.AreEqual(_saving.HasCancelableEvents(), true);
        }

        [Test]
        public void NoMgmtFeeEvent()
        {
            _saving.AgioFees = 0.1;
            _saving.Closure(new DateTime(2007, 8, 30), new User());
            List<SavingEvent> events = _saving.Events.FindAll(item => item is SavingManagementFeeEvent);
            Assert.AreEqual(events.Count, 0);
        }

        [Test]
        public void OneMgmtFeeEvent()
        {
            // Fix
//            Assert.Ignore();

            _saving = new SavingBookContract(
                                                ApplicationSettings.GetInstance(""), 
                                                 
                                                new User(), 
                                                new DateTime(2007, 08, 11), 
                                                _product, 
                                                null
                                            );
            _saving.FirstDeposit(10000m, new DateTime(2007, 08, 11), 0, new User(), Teller.CurrentTeller);
            _saving.AgioFees = 0.1;
            _saving.ManagementFees = 50;
            _saving.Closure(new DateTime(2007, 9, 11), new User());
            List<SavingEvent> events = _saving.Events.FindAll(item => item is SavingManagementFeeEvent);
            Assert.AreEqual(events.Count, 1);
        }

        [Test]
        public void SeveralMgmtFeeEvents()
        {
            _saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), new DateTime(2007, 08, 11), _product, null);
            _saving.FirstDeposit(10000m, new DateTime(2007, 08, 11), 0, new User(), Teller.CurrentTeller);
            _saving.AgioFees = 0.1;
            _saving.ManagementFees = 50;

            _saving.Closure(new DateTime(2007, 12, 31), new User());
            List<SavingEvent> events = _saving.Events.FindAll(item => item is SavingManagementFeeEvent);
            Assert.AreEqual(events.Count, 4);
        }

        /* Agio tests */
        [Test]
        public void CalculateInterest_AccrualhMode_Daily_EndOfYear_OneClosure_AfterOneWeek_Agio()
        {
            
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _product.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
                                                               new User(),
                                                               new DateTime(2009, 01, 01), null)
                                            {Product = _product, InterestRate = 0.1, AgioFees = 0.1};
            saving.FirstDeposit(-100, new DateTime(2009, 01, 01), 0, new User(), Teller.CurrentTeller);

            saving.Closure(new DateTime(2009, 01, 08), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            /*Assert.AreEqual(accrualEvents, 7);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_SAVINGS_BOOKS, 1).Balance.Value, 700);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.SAVINGS, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 700);
             */

            List<SavingEvent> agioEvents = saving.Events.FindAll(items => items is SavingAgioEvent);
            Assert.AreEqual(agioEvents.Count, 7);
            Assert.AreEqual(agioEvents[0].Fee, 10);
            Assert.AreEqual(saving.GetBalance(), -194.88);

//            saving.Deposit(200, new DateTime(2009, 01, 08), "depot", new User(), false, false, OPaymentMethods.Cash, null, null);
            saving.Deposit(200, new DateTime(2009, 01, 08), "depot", new User(), false, false, OSavingsMethods.Cash, null, null);
            //saving.Withdraw(230, new DateTime(2009, 02, 03), "retrait", new User(), false);

            saving.Closure(new DateTime(2009, 01, 15), new User() { Id = 1 });
            List<SavingEvent> agioEvents2 = saving.Events.FindAll(items => items is SavingAgioEvent);
            Assert.AreEqual(agioEvents2.Count, 7);
            Assert.AreEqual(saving.GetBalance(), 5.12);
        }
    }
}