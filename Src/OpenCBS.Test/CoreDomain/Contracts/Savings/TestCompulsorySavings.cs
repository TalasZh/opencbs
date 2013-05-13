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
using NUnit.Framework;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.CoreDomain.Products;
using OpenCBS.CoreDomain.Events;
using OpenCBS.Shared;
using OpenCBS.Enums;
using OpenCBS.CoreDomain;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans;

namespace OpenCBS.Test.CoreDomain.Contracts.CompulsorySavingss
{
	[TestFixture]
	public class TestCompulsorySavings
	{
        [SetUp]
        public void SetUp()
        {
            ChartOfAccounts.GetInstance(new User()).Accounts.Clear();
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
        }

		[Test]
		public void Get_Set_Product()
		{
            CompulsorySavingsProduct product = new CompulsorySavingsProduct { Id = 1 };
            CompulsorySavings CompulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(), 12m, TimeProvider.Today, product, null);
			Assert.AreEqual(product, CompulsorySavings.Product);
		}

		[Test]
		public void Get_Set_Id()
		{
            CompulsorySavings CompulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(), 12m, TimeProvider.Today, null) { Id = 1 };
			Assert.AreEqual(1, CompulsorySavings.Id);
		}

        [Test]
        public void Get_Set_Loan()
        {
            Loan loan = new Loan() { Id = 1 };
            CompulsorySavings compulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()),  new User()) { Loan = loan };
            Assert.AreEqual(loan, compulsorySavings.Loan);
        }

		[Test]
		public void Get_Set_Code_ForPerson()
		{
            CompulsorySavings CompulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(), 12m, new DateTime(2007, 08, 11), null) 
            { Code = "", Product = new CompulsorySavingsProduct() { Id = 2, Name = "CompulsorySavingsProduct", Code = "CompulsorySavingsProduct" } };
			CompulsorySavings.GenerateSavingCode(new Person { Id = 2, FirstName="Vincent", LastName = "Guigui" }, 3, "BC/YY/PC-PS/CN-ID", "IMF",  "BC");
			Assert.AreEqual("S/BC/2007/COMPU-4/GUIG-2", CompulsorySavings.Code);

            CompulsorySavings.GenerateSavingCode(new Person { Id = 10, FirstName = "Vincent", LastName = "Guigui" }, 4, "IC/BC/CS/ID", "IMF", "BC");
            Assert.AreEqual("IMF/BC/05/00010/73", CompulsorySavings.Code);
        }

		[Test]
		public void Get_Set_Code_ForCorporate()
		{
            CompulsorySavings CompulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(), 12m, new DateTime(2007, 08, 11), null) 
            { Code = "", Product = new CompulsorySavingsProduct() { Id = 2, Name = "CompulsorySavingsProduct", Code = "CompulsorySavingsProduct" } };
            CompulsorySavings.GenerateSavingCode(new Corporate { Id = 2, Name = "Guigui" }, 0, "BC/YY/PC-PS/CN-ID", "IMF", "BC");
			Assert.AreEqual("S/BC/2007/COMPU-1/GUIG-2", CompulsorySavings.Code);

            CompulsorySavings.GenerateSavingCode(new Corporate { Id = 10, Name = "Guigui" }, 1, "IC/BC/CS/ID", "IMF", "BC");
            Assert.AreEqual("IMF/BC/02/00010/94", CompulsorySavings.Code);
		}

		[Test]
		public void Get_Set_CreationDate()
		{
            CompulsorySavings CompulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(), 12m, new DateTime(2008, 10, 21), null);
			Assert.AreEqual(new DateTime(2008, 10, 21), CompulsorySavings.CreationDate);
		}

        [Test]
        public void Get_Set_ClosedDate()
        {
            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(), 12m, new DateTime(2008, 10, 21), null) { ClosedDate = new DateTime(2009, 01, 01) };
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
            CompulsorySavings CompulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(), 12m, new DateTime(2008, 08, 18), null);
			CompulsorySavings.Events.AddRange(events);
			Assert.AreEqual(events.Count+1, CompulsorySavings.Events.Count);
			CompulsorySavings.Events.Sort();
			events.Sort();
			Assert.AreEqual(12m, CompulsorySavings.Events[0].Amount.Value);
			Assert.AreEqual(75m, CompulsorySavings.Events[1].Amount.Value);
			Assert.AreEqual(100m, CompulsorySavings.Events[2].Amount.Value);

		}

		[Test]
		public void Get_Set_InterestRate()
		{
			double interestRate = 2.0;
            CompulsorySavings CompulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), 
                new User(), 12m, TimeProvider.Today, null) { InterestRate = interestRate };
			Assert.AreEqual(interestRate, CompulsorySavings.InterestRate);
		}

		[Test]
		public void Get_Set_InitialAmount()
		{
			List<SavingEvent> events = new List<SavingEvent>
               	{
               		new SavingWithdrawEvent {Amount = 75, Date = new DateTime(2008, 10, 21)},
               		new SavingDepositEvent {Amount = 12.46m,Date = new DateTime(2008, 08, 18)},
               		new SavingDepositEvent {Amount = 86, Date = new DateTime(2008, 09, 01)}
               	};
            CompulsorySavings CompulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), 
                new User(), 150m, new DateTime(2008, 07, 18), null);

            CompulsorySavings.Events.AddRange(events);

			Assert.AreEqual(173.46m, CompulsorySavings.GetBalance().Value);
			Assert.AreEqual(150m, CompulsorySavings.InitialAmount.Value);
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

            CompulsorySavings CompulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), 
                new User(), 200m, new DateTime(2008, 07, 18), null);

            CompulsorySavings.Events.AddRange(events);

			Assert.AreEqual(3750.46, CompulsorySavings.GetBalance().Value);
        }
        
        [Test]
        public void Get_Balance_At_Date()
        {
            CompulsorySavingsProduct product = new CompulsorySavingsProduct();
            CompulsorySavings CompulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), 
                new User(), 1000, new DateTime(2009, 01, 01), product, null);
            CompulsorySavings.Deposit(100, new DateTime(2009, 02, 01), "depot", new User(), false, false, OSavingsMethods.Cash);
            CompulsorySavings.Withdraw(230, new DateTime(2009, 02, 03), "retrait", new User(), false);

            Assert.AreEqual(CompulsorySavings.GetBalance(new DateTime(2009, 01, 31)), 1000);
            Assert.AreEqual(CompulsorySavings.GetBalance(new DateTime(2009, 02, 01)), 1100);
            Assert.AreEqual(CompulsorySavings.GetBalance(new DateTime(2009, 02, 02)), 1100);
            Assert.AreEqual(CompulsorySavings.GetBalance(new DateTime(2009, 02, 03)), 870);
        }
        
        [Test]
        public void Get_BalanceMin_At_Date()
        {
            CompulsorySavingsProduct product = new CompulsorySavingsProduct();
            CompulsorySavings CompulsorySavings = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), 
                new User(), 1000, new DateTime(2009, 01, 01), product, null);
            CompulsorySavings.Withdraw(100, new DateTime(2009, 02, 02), "retrait", new User(), false);
            CompulsorySavings.Deposit(230, new DateTime(2009, 02, 02), "depot", new User(), false, false, OSavingsMethods.Cash);

            Assert.AreEqual(CompulsorySavings.GetBalanceMin(new DateTime(2009, 01, 01)), 1000);
            Assert.AreEqual(CompulsorySavings.GetBalanceMin(new DateTime(2009, 02, 01)), 1000);
            Assert.AreEqual(CompulsorySavings.GetBalanceMin(new DateTime(2009, 02, 02)), 900);
        }


        [Test]
        public void CloseAccount()
        {
            Currency currency = new Currency() { UseCents = false, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };
            CompulsorySavingsProduct product = new CompulsorySavingsProduct();
            product.Currency = currency;
            
            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(),
                1000, new DateTime(2009, 01, 01), null) { InterestRate = 0.01, Product = product };

            List<SavingEvent> events = saving.Close(new DateTime(2009, 02, 26), new User() { Id = 1 }, "Close savings contract", false);

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 58);
            Assert.AreEqual(accrualEvents, 56);
            Assert.AreEqual(postingEvents, 1);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS, 1).Balance.Value, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.COMPULSORY_SAVINGS, 1).Balance.Value, 1560);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 560);
            Assert.AreEqual(saving.GetBalance(), 1560);
            Assert.AreEqual(saving.Status, OSavingsStatus.Closed);
        }


        #region Closure Day of Creation
        [Test]
        public void CalculateInterest_CashMode_Daily_EndOfYear_OneClosure_DayOfCreation()
        {
            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(),
                1000, new DateTime(2009, 01, 15), null) { InterestRate = 0.1, Product = new CompulsorySavingsProduct() };

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS, 1).Balance.Value, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.COMPULSORY_SAVINGS, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfYear_OneClosure_DayOfCreation()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(),
                1000, new DateTime(2009, 01, 15), null) { InterestRate = 0.1, Product = new CompulsorySavingsProduct() };

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 15, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(accrualEvents, 0);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS, 1).Balance.Value, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.COMPULSORY_SAVINGS, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 0);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }
        #endregion

        #region Closure After One Day
        [Test]
        public void CalculateInterest_CashMode_Daily_EndOfYear_OneClosure_AfterOneDay()
        {
            Currency currency = new Currency() { UseCents = false, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };
            CompulsorySavingsProduct product = new CompulsorySavingsProduct();
            product.Currency = currency;

            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(),
                1000, new DateTime(2009, 01, 1), null) { InterestRate = 0.1, Product = product };

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 2, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 2);
            Assert.AreEqual(accrualEvents, 1);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS, 1).Balance.Value, 100);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.COMPULSORY_SAVINGS, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 100);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfYear_OneClosure_AfterOneDay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            Currency currency = new Currency() { UseCents = false, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };
            CompulsorySavingsProduct product = new CompulsorySavingsProduct();
            product.Currency = currency;

            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(),
                1000, new DateTime(2009, 01, 1), null) { InterestRate = 0.1, Product = product };

            List<SavingEvent> events = saving.Closure(new DateTime(2009, 01, 2, 1, 1, 1), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(item => item is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(events.Count, 2);
            Assert.AreEqual(accrualEvents, 1);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS, 1).Balance.Value, 100);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.COMPULSORY_SAVINGS, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 100);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }
        #endregion

        #region Closure After One Week
        [Test]
        public void CalculateInterest_CashMode_Daily_EndOfYear_OneClosure_AfterOneWeek()
        {
            Currency currency = new Currency() { UseCents = false, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };
            CompulsorySavingsProduct product = new CompulsorySavingsProduct();
            product.Currency = currency;

            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(),
                1000, new DateTime(2009, 01, 01), null) { InterestRate = 0.1, Product = product };

            saving.Closure(new DateTime(2009, 01, 08), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(accrualEvents, 7);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS, 1).Balance.Value, 700);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.COMPULSORY_SAVINGS, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 700);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualhMode_Daily_EndOfYear_OneClosure_AfterOneWeek()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            Currency currency = new Currency() { UseCents = false, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };
            CompulsorySavingsProduct product = new CompulsorySavingsProduct();
            product.Currency = currency;

            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(),
                1000, new DateTime(2009, 01, 01), null) { InterestRate = 0.1, Product = product };

            saving.Closure(new DateTime(2009, 01, 08), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(accrualEvents, 7);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS, 1).Balance.Value, 700);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.COMPULSORY_SAVINGS, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 700);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        #endregion

        #region Closure One Day Before Posting Date
        [Test]
        public void CalculateInterest_CashMode_Daily_EndOfYear_OneClosure_OneDayBefore_PostingDate()
        {
            Currency currency = new Currency() { UseCents = false, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };
            CompulsorySavingsProduct product = new CompulsorySavingsProduct();
            product.Currency = currency;

            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(),
                1000, new DateTime(2009, 01, 01), null) { InterestRate = 0.1, Product = product };

            saving.Closure(new DateTime(2009, 12, 31), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(accrualEvents, 364);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS, 1).Balance.Value, 36400);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.COMPULSORY_SAVINGS, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 36400);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfYear_OneClosure_OneDayBefore_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            Currency currency = new Currency() { UseCents = false, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };
            CompulsorySavingsProduct product = new CompulsorySavingsProduct();
            product.Currency = currency;

            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(),
                1000, new DateTime(2009, 01, 01), null) { InterestRate = 0.1, Product = product };

            saving.Closure(new DateTime(2009, 12, 31), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            Assert.AreEqual(accrualEvents, 364);
            Assert.AreEqual(postingEvents, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS, 1).Balance.Value, 36400);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.COMPULSORY_SAVINGS, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 36400);
            Assert.AreEqual(saving.GetBalance(), 1000);
        }
        #endregion

        #region Closure On Posting Date
        [Test]
        public void CalculateInterest_CashMode_Daily_EndOfYear_OneClosure_On_PostingDate()
        {
            Currency currency = new Currency() { UseCents = false, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };
            CompulsorySavingsProduct product = new CompulsorySavingsProduct();
            product.Currency = currency;

            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(),
                1000, new DateTime(2009, 01, 01), null) { InterestRate = 0.1, Product = product };

            saving.Closure(new DateTime(2010, 01, 01), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;
            OCurrency interests = saving.Events.Where(item => item is SavingInterestsAccrualEvent).Sum(item => item.Amount.Value);


            Assert.AreEqual(accrualEvents, 365);
            Assert.AreEqual(postingEvents, 1);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS, 1).Balance.Value, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.COMPULSORY_SAVINGS, 1).Balance.Value, 37500);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 36500);
            Assert.AreEqual(saving.GetBalance(), 37500);
        }

        [Test]
        public void CalculateInterest_AccrualMode_Daily_EndOfYear_OneClosure_On_PostingDate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            Currency currency = new Currency() { UseCents = false, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };
            CompulsorySavingsProduct product = new CompulsorySavingsProduct();
            product.Currency = currency;

            CompulsorySavings saving = new CompulsorySavings(ApplicationSettings.GetInstance(""), ChartOfAccounts.GetInstance(new User()), new User(),
                1000, new DateTime(2009, 01, 01), null) { InterestRate = 0.1, Product = product };

            saving.Closure(new DateTime(2010, 01, 01), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;
            OCurrency interests = saving.Events.Where(item => item is SavingInterestsAccrualEvent).Sum(item => item.Amount.Value);


            Assert.AreEqual(accrualEvents, 365);
            Assert.AreEqual(postingEvents, 1);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS, 1).Balance.Value, 0);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.COMPULSORY_SAVINGS, 1).Balance.Value, 37500);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH, 1).Balance.Value, 1000);
            Assert.AreEqual(saving.ChartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_DEPOSIT_ACCOUNT, 1).Balance.Value, 36500);
            Assert.AreEqual(saving.GetBalance(), 37500);
        }
        #endregion
    }
}
    
