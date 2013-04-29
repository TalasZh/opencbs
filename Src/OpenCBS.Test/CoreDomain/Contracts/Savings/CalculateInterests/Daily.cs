using System;
using System.Collections.Generic;
using NUnit.Framework;
using Octopus.CoreDomain.Products;
using Octopus.CoreDomain.Contracts.Savings;
using Octopus.Shared.Settings;
using Octopus.CoreDomain;
using Octopus.Enums;
using Octopus.CoreDomain.Events.Saving;
using Octopus.CoreDomain.Accounting;

namespace Octopus.Test.CoreDomain.Contracts.Savings.CalculateInterests
{
    [TestFixture]
    public class Daily
    {
        [Test]
        public void CalculateInterest_NoDay()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfYear
            };
            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""), new User(),
                new DateTime(2009, 07, 01), null) { Product = product, InterestRate = 0.1 };

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
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfYear
            };
            SavingBookContract saving = new SavingBookContract(
                                            ApplicationSettings.GetInstance(""), 
                                            new User(),
                                            new DateTime(2009, 07, 01),
                                            null)
                                            {
                                                Product = product, 
                                                InterestRate = 0.1
                                            };

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 07, 02), new User { Id = 1 });

            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void CalculateInterest_ThirtyDay()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfYear
            };
            SavingBookContract saving = new SavingBookContract(
                ApplicationSettings.GetInstance(""), 
                new User(),
                new DateTime(2009, 01, 01), 
                null) { Product = product, InterestRate = 0.1 };

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 01, 31), new User { Id = 1 });

            Assert.AreEqual(31, list.Count);
        }


        [Test]
        public void CalculateInterest_OneYear()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfYear
            };
            SavingBookContract saving = new SavingBookContract(
                                                                ApplicationSettings.GetInstance(""),
                                                                new User(),
                                                                new DateTime(2009, 01, 01), 
                                                                null) 
                                                                { 
                                                                    Product = product, 
                                                                    InterestRate = 0.1 
                                                                };

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2010, 01, 01), new User { Id = 1 });

            Assert.AreEqual(366, list.Count);
        }
    }
}
