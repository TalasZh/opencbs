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
using NUnit.Framework;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.CoreDomain;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Test.CoreDomain.Contracts.Savings
{
    [TestFixture]
	public class TestSavingFees
    {
        private SavingsBookProduct _bookProduct;
        private SavingBookContract _saving;

        [SetUp]
        public void SetUp()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);

            Currency currency = new Currency() { UseCents = true, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };

            _bookProduct = new SavingsBookProduct
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

            _saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
                                             new User(),
                                             new DateTime(2007, 08, 11), _bookProduct, null);
        }


        #region Saving Book Product

        [Test]
        public void Charge_overdaft_fees()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
                                                               new User(), new DateTime(2009, 01, 01), null)
                                            {
                                                Product = _bookProduct,
                                                AgioFees = 0.01,
                                                ChequeDepositFees = 100,
                                                DepositFees = 50,
                                                CloseFees = 100,
                                                ReopenFees = 100,
                                                OverdraftFees = 100
                                            };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            Currency currency = new Currency() { UseCents = true, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };

            Assert.AreEqual(1000, saving.GetBalance().Value);

            //Below, we explicitly implement withdraw method from <Saving services>.<Withdraw>, since withdraw method of 'saving' object doesn't implement 
            // overdraft fee by default

            List<SavingEvent> withdrawEvents = saving.Withdraw(1100, new DateTime(2009, 1, 2), "withdraw", new User(), false, null);
            if (saving.GetBalance() < 0 && !saving.InOverdraft)
            {
                saving.ChargeOverdraftFee(new DateTime(2009, 1, 2), new User());
            }

            Assert.AreEqual(-200, saving.GetBalance().Value);
        }

        [Test]
        public void Charge_agio_fees()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), 
                                       new User(), new DateTime(2009, 01, 01), null)
            {
                Product = _bookProduct,
                AgioFees = 0.01,
                ChequeDepositFees = 100,
                DepositFees = 50,
                CloseFees = 100,
                ReopenFees = 100,
                OverdraftFees = 100
            };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            Currency currency = new Currency() { UseCents = true, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };

            Assert.AreEqual(1000, saving.GetBalance().Value);

            //Below, we explicitly implement withdraw method from <Saving services>.<Withdraw>, since withdraw method of 'saving' object doesn't implement 
            // overdraft fee by default

            List<SavingEvent> withdrawEvents = saving.Withdraw(1100, new DateTime(2009, 1, 2), "withdraw", new User(), false, null);
            saving.Closure(new DateTime(2009, 1, 12), new User());

            //agio for ten days
            Assert.AreEqual(-111.55, saving.GetBalance().Value);
        }

        [Test]
        public void Charge_close_reopen_fees()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), 
                                       new User(), new DateTime(2009, 1, 1), null)
            {
                Product = _bookProduct,
                AgioFees = 0.01,
                ChequeDepositFees = 100,
                DepositFees = 50,
                CloseFees = 100,
                ReopenFees = 100,
                OverdraftFees = 100
            };
            Currency currency = new Currency() { UseCents = true, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };

            List<SavingEvent> closeEvents = saving.Close(new DateTime(2009, 1, 2), new User(), "closing", false, Teller.CurrentTeller, false);

            Assert.AreEqual(OSavingsStatus.Closed, saving.Status);

            foreach (SavingEvent closeEvent in closeEvents)
            {
                Assert.AreEqual(closeEvent.Fee, saving.CloseFees);
            }
            
        }

        [Test]
        public void Charge_reopen_fees()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), 
                                       new User(), new DateTime(2009, 1, 1), null)
            {
                Product = _bookProduct,
                AgioFees = 0.01,
                ChequeDepositFees = 100,
                DepositFees = 50,
                CloseFees = 100,
                ReopenFees = 100,
                OverdraftFees = 100
            };

            saving.Close(new DateTime(2009, 1, 2), new User(), "closing", false, Teller.CurrentTeller, false);
            List<SavingEvent> reopenEvents = saving.Reopen(100, new DateTime(2009, 1, 3), new User(), "reopen", false);

            foreach (SavingEvent reopenEvent in reopenEvents)
            {
                Assert.AreEqual(saving.ReopenFees, reopenEvent.Fee);
            }

        }

        [Test]
        public void Charge_deposit_fees()
        {
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), 
                                       new User(), new DateTime(2009, 1, 1), null)
            {
                Product = _bookProduct,
                AgioFees = 0.01,
                ChequeDepositFees = 100,
                DepositFees = 50,
                CloseFees = 100,
                ReopenFees = 100,
                OverdraftFees = 100
            };
            Currency currency = new Currency() { UseCents = true, Code = "SOM", Id = 1, IsPivot = true, Name = "SOM" };

//            List<SavingEvent> cashDepositEvents = saving.Deposit(100, new DateTime(2009, 1, 2), "cash deposit", new User(), false,
//                                                             false, OPaymentMethods.Cash, null, null);
            List<SavingEvent> cashDepositEvents = saving.Deposit(100, new DateTime(2009, 1, 2), "cash deposit", new User(), false,
                                                             false, OSavingsMethods.Cash, null, null);
//            List<SavingEvent> chequeDepositEvents = saving.Deposit(100, new DateTime(2009, 1, 2), "cheque deposit", new User(), false,
//                                                             false, OPaymentMethods.Cheque, null, null);
            List<SavingEvent> chequeDepositEvents = saving.Deposit(100, new DateTime(2009, 1, 2), "cheque deposit", new User(), false,
                                                             false, OSavingsMethods.Cheque, null, null);
            foreach (SavingEvent cashEvent in cashDepositEvents)
            {
                Assert.AreEqual(cashEvent.Fee, saving.DepositFees);
            }
            foreach (SavingEvent chequeEvent in chequeDepositEvents)
            {
                Assert.AreEqual(chequeEvent.Fee, saving.ChequeDepositFees);
            }
        }

        [Test]
        public void Create_Saving_Book_Account_With_EntryFees()
        {
            SavingsBookProduct product = new SavingsBookProduct()
            {
                Name = "NewSavingProduct",
                InitialAmountMin = 1000,
                InitialAmountMax = 2000,
                DepositMin = 100,
                DepositMax = 200,
                WithdrawingMin = 100,
                WithdrawingMax = 200,
                InterestRate = 0.2,
                BalanceMin = 1000,
                BalanceMax = 2000,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                ClientType = OClientTypes.All,
                EntryFeesMin = 5,
                EntryFeesMax = 15,
                Currency = new Currency { Id = 1 }
            };

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), 
                new DateTime(2009, 01, 01), product, null);
            saving.InitialAmount = 1000;
            saving.EntryFees = 10;
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), 10, new User(), Teller.CurrentTeller);

            Assert.AreEqual(saving.GetBalance(), 1000);
            Assert.AreEqual(saving.Events.Count, 1);
            Assert.AreEqual(saving.InitialAmount, 1000);
            Assert.AreEqual(saving.EntryFees, 10);
        }

        [Test]
        public void FlatWithdrawFees()
        {
            SavingsBookProduct product = new SavingsBookProduct()
            {
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFeesMin = 1,
                FlatWithdrawFeesMax = 5
            };

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), 
                                       new User(), new DateTime(2009, 01, 01), product, null) {FlatWithdrawFees = 2, };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingEvent> withdrawEvents = saving.Withdraw(10, new DateTime(2009, 01, 02), "withdraw", new User(), false, null);

            Assert.AreEqual(1, withdrawEvents.Count);
            Assert.AreEqual(10, withdrawEvents[0].Amount.Value);
            Assert.AreEqual(2, ((ISavingsFees)withdrawEvents[0]).Fee.Value);
            Assert.AreEqual(988, saving.GetBalance().Value);
        }

        [Test]
        public void RateWithdrawFees()
        {
            SavingsBookProduct product = new SavingsBookProduct()
            {
                WithdrawFeesType = OSavingsFeesType.Rate,
                RateWithdrawFeesMin = 0.01,
                RateWithdrawFeesMax = 0.05
            };

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), new DateTime(2009, 01, 01), product, null) { RateWithdrawFees = 0.03 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);
            List<SavingEvent> withdrawEvents = saving.Withdraw(10, new DateTime(2009, 01, 02), "withdraw", new User(), false, null);

            Assert.AreEqual(1, withdrawEvents.Count);
            Assert.AreEqual(10, withdrawEvents[0].Amount.Value);
            Assert.AreEqual(0.3, ((ISavingsFees)withdrawEvents[0]).Fee.Value);
            Assert.AreEqual(989.7, saving.GetBalance().Value);
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
            _saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), new DateTime(2007, 08, 11), _bookProduct, null);
            _saving.AgioFees = 0.1;
            _saving.ManagementFees = 50;
            _saving.FirstDeposit(10000, new DateTime(2007, 08, 11), null, new User(), Teller.CurrentTeller);
            _saving.Closure(new DateTime(2007, 9, 15), new User());
            List<SavingEvent> events = _saving.Events.FindAll(item => item is SavingManagementFeeEvent);
            Assert.AreEqual(events.Count, 1);
            Assert.AreEqual(_saving.GetBalance().Value,9950);
        }

        [Test]
        public void SeveralMgmtFeeEvents()
        {
            _saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(), new DateTime(2007, 08, 11), _bookProduct, null);
            _saving.AgioFees = 0.1;
            _saving.ManagementFees = 50;
            _saving.FirstDeposit(10000, new DateTime(2007, 08, 01), null, new User(), Teller.CurrentTeller);
            _saving.Closure(new DateTime(2007, 12, 31), new User());
            List<SavingEvent> events = _saving.Events.FindAll(item => item is SavingManagementFeeEvent);
            Assert.AreEqual(events.Count, 4);
            Assert.AreEqual(9800,_saving.GetBalance().Value);
        }

        [Test]
        public void CalculateInterest_AccrualhMode_Daily_EndOfYear_OneClosure_AfterOneWeek_Agio()
        {
            Assert.Ignore();
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            _bookProduct.InterestFrequency = OSavingInterestFrequency.EndOfYear;
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 01, 01), null) { Product = _bookProduct, InterestRate = 0.1, AgioFees = 0.1 };

            saving.Closure(new DateTime(2009, 01, 08), new User() { Id = 1 });

            int accrualEvents = saving.Events.FindAll(item => item is SavingInterestsAccrualEvent).Count;
            int postingEvents = saving.Events.FindAll(items => items is SavingInterestsPostingEvent).Count;

            List<SavingEvent> agioEvents = saving.Events.FindAll(items => items is SavingAgioEvent);
            Assert.AreEqual(agioEvents.Count, 7);
            Assert.AreEqual(agioEvents[0].Fee, 10);
            Assert.AreEqual(saving.GetBalance(), -170);

//            saving.Deposit(200, new DateTime(2009, 01, 08), "depot", new User(), false, false, OPaymentMethods.Cash, null, null);
            saving.Deposit(200, new DateTime(2009, 01, 08), "depot", new User(), false, false, OSavingsMethods.Cash, null, null);
            //saving.Withdraw(230, new DateTime(2009, 02, 03), "retrait", new User(), false);

            saving.Closure(new DateTime(2009, 01, 15), new User() { Id = 1 });
            List<SavingEvent> agioEvents2 = saving.Events.FindAll(items => items is SavingAgioEvent);
            Assert.AreEqual(agioEvents2.Count, 13);
            Assert.AreEqual(agioEvents2[9].Fee, 13);
            Assert.AreEqual(saving.GetBalance(), -51);
        }

        #endregion

      

    }
}

