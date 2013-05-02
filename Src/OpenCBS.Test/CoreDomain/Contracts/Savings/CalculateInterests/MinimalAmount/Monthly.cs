using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenCBS.CoreDomain.Products;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Events.Saving;

namespace OpenCBS.Test.CoreDomain.Contracts.Savings.CalculateInterests
{
    [TestFixture]
    public class Monthly
    {
        [Test]
        public void CalculateInterest_NoDay()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                                                               new DateTime(2009, 07, 01), null)
                                            {Product = product, InterestRate = 0.1};

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 07, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 0);
        }

        [Test]
        public void CalculateInterest_OneDay()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),  new User(),
                new DateTime(2009, 07, 01), null) { Product = product, InterestRate = 0.1 };

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 07, 02), new User { Id = 1 });

            Assert.AreEqual(list.Count, 0);
        }

        [Test]
        public void CalculateInterest_OneMonth()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = product, InterestRate = 0.1 };
            saving.FirstDeposit(100, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 10);
        }

        [Test]
        public void CalculateInterest_OneMonth_Starting15()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 15), null) { Product = product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 100);
        }

        [Test]
        public void CalculateInterest_OneMonth_Deposit_FirstDayOfMonth()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            //saving.Deposit(100, new DateTime(2009, 01, 01), "depot", new User()); 

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 100);
        }

        [Test]
        public void CalculateInterest_OneMonth_Withdraw_FirstDayOfMonth()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 0
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = product, InterestRate = 0.1, FlatWithdrawFees = 0 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Withdraw(100, new DateTime(2009, 01, 01), "retrait", new User(), false, null);

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 90);
        }
        
        [Test]
        public void CalculateInterest_TwoOperation_In_OneDay()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 0
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = product, InterestRate = 0.1, FlatWithdrawFees = 0 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Withdraw(250, new DateTime(2009, 01, 15), "retrait", new User(), false, null);
//            saving.Deposit(100, new DateTime(2009, 01, 15), "depot", new User(), true, false, OPaymentMethods.Cash, null, null);
            saving.Deposit(100, new DateTime(2009, 01, 15), "depot", new User(), true, false, OSavingsMethods.Cash, null, null);

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 75);
        }

        [Test]
        public void CalculateInterest_OneMonth_Deposit()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = product, InterestRate = 0.1 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

//            saving.Deposit(100, new DateTime(2009, 01, 15), "depot", new User(), true, false, OPaymentMethods.Cash, null, null);
            saving.Deposit(100, new DateTime(2009, 01, 15), "depot", new User(), true, false, OSavingsMethods.Cash, null, null);

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 100);
        }

        [Test]
        public void CalculateInterest_OneMonth_Withdraw()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount,
                WithdrawFeesType = OSavingsFeesType.Flat,
                FlatWithdrawFees = 0
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = product, InterestRate = 0.1, FlatWithdrawFees = 0 };
            saving.FirstDeposit(1000, new DateTime(2009, 01, 01), null, new User(), Teller.CurrentTeller);

            saving.Withdraw(100, new DateTime(2009, 01, 15), "depot", new User(), false, null);

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 90);
        }


        [Test]
        public void CalculateInterest_OneYear()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 01, 01), null) { Product = product, InterestRate = 0.1 };

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2010, 01, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 12);
        }
    }
}
