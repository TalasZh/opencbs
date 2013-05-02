// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain;
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Shared;

namespace OpenCBS.Test.CoreDomain
{
	[TestFixture]
	public class TestChartOfAccounts
	{
		ChartOfAccounts chartOfAccounts;
		Account cash;
        [TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
            chartOfAccounts = ChartOfAccounts.GetInstance(new User());
		}

		[SetUp]
		public void SetUp()
		{
            chartOfAccounts.Accounts = new List<Account>();
			cash = new Account();
			cash.DebitPlus = true;
			cash.Number = "1011";
			cash.Balance = 1000;
			cash.TypeCode = "CASH";
		    cash.StockBalance = 1002;
		    cash.CurrencyId = 1;
			chartOfAccounts.AddAccount(cash);
		}
		
		[Test]
		public void TestAddAccount()
		{
			Assert.AreEqual(1,chartOfAccounts.Accounts.Count);
		}

		[Test]
		public void TestGetAccountByTypeCode()
		{
			Assert.AreEqual(1000m,chartOfAccounts.GetAccountByTypeCode("CASH", 1).Balance.Value);
		}

        [Test]
        public void TestGetStockBalance()
        {
            Assert.AreEqual(1002m, chartOfAccounts.GetAccountByTypeCode("CASH", 1).StockBalance.Value);
        }

		[Test]
		public void TestGetAccountByTypeCodeWhenNoAccountMatchesWithSpecifiedCode()
		{
			Assert.IsNull(chartOfAccounts.GetAccountByTypeCode("", 1));
		}

		[Test]
		public void TestGetAccountByNumber()
		{
			Assert.AreEqual("CASH",chartOfAccounts.GetAccountByNumber("1011", 1).TypeCode);
		}

		[Test]
		public void TestGetAccountByNumberWhenNoAccountMatchesWithSpecifiedNumber()
		{
			Assert.IsNull(chartOfAccounts.GetAccountByNumber("", 1));
		}

		[Test]
		public void TestBook()
		{
			Account cashCredit = new Account();
			cashCredit.DebitPlus = true;
			cashCredit.Number = "1031";
			cashCredit.TypeCode = "CASH_CREDIT";
			cashCredit.Balance = 0;
		    cashCredit.CurrencyId = 1;
			chartOfAccounts.AddAccount(cash);
			chartOfAccounts.AddAccount(cashCredit);
		    // OMFS-200
			AccountingTransaction movementSet = new AccountingTransaction();
            Booking mvt = new Booking(2, cashCredit, 100, cash, movementSet.Date, new Branch{Id = 1});
			movementSet.AddBooking(mvt);
			chartOfAccounts.Book(movementSet);
			Assert.AreEqual(1100m,chartOfAccounts.GetAccountByTypeCode("CASH",1).Balance.Value);
			Assert.AreEqual(-100m,chartOfAccounts.GetAccountByTypeCode("CASH_CREDIT",1).Balance.Value);
		}

		[Test]
		public void TestBookAndUnBook()
		{
		    // Actors
            Account _cash = null, _cashCredit = null;
            OCurrency cashBalance1 = 0, cashBalance2 = 0;
            OCurrency cashBalance3 = 0, cashBalance4 = 0;
		    Booking mvt = null;
            AccountingTransaction movementSet = null;

		    // Activites
            chartOfAccounts.Accounts = new List<Account>();
			_cash = new Account();
			_cash.DebitPlus = true;
			_cash.Number = "1011";
			_cash.Balance = 1000;
			_cash.TypeCode = "CASH";
		    _cash.CurrencyId = 1;

			_cashCredit = new Account();
			_cashCredit.DebitPlus = true;
			_cashCredit.Number = "1031";
			_cashCredit.TypeCode = "CASH_CREDIT";
			_cashCredit.Balance = 0;
		    _cashCredit.CurrencyId = 1;
			chartOfAccounts.AddAccount(_cash);
			chartOfAccounts.AddAccount(_cashCredit);
            movementSet = new AccountingTransaction();
		    
		    // Asertions
            mvt = new Booking(2, _cashCredit, 100, cash, movementSet.Date, new Branch{Id = 1});
			movementSet.AddBooking(mvt);
			chartOfAccounts.Book(movementSet);
		    cashBalance1 = chartOfAccounts.GetAccountByTypeCode("CASH",1).Balance;
		    cashBalance2 = chartOfAccounts.GetAccountByTypeCode("CASH_CREDIT", 1).Balance;
            chartOfAccounts.UnBook(movementSet);
            cashBalance3 = chartOfAccounts.GetAccountByTypeCode("CASH", 1).Balance;
            cashBalance4 = chartOfAccounts.GetAccountByTypeCode("CASH_CREDIT",1).Balance;
   
            // Asertions
            Assert.AreEqual (1100m,   cashBalance1.Value);
            Assert.AreEqual (-100m,   cashBalance2.Value);
            Assert.AreEqual (1000m,   cashBalance3.Value);
            Assert.AreEqual (0m,      cashBalance4.Value);
		}

		[Test]
		public void TestRetrievedDefaultAccount()
		{
			Assert.AreEqual(33,chartOfAccounts.DefaultAccounts.Count);
		}
	}
}
