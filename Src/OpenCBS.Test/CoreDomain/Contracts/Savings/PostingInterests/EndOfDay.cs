// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Events.Saving;

namespace OpenCBS.Test.CoreDomain.Contracts.Savings.PostingInterests
{
    [TestFixture]
    public class EndOfDay
    {
        
        [Test]
        public void PostingInterests_OnePosting()
        {
            SavingsBookProduct product = new SavingsBookProduct
                                            {
                                                Id = 1,
                                                InterestBase = OSavingInterestBase.Daily,
                                                InterestFrequency = OSavingInterestFrequency.EndOfDay,
                                                Periodicity = new InstallmentType("Daily", 1, 0)
                                            };
            SavingBookContract saving = new SavingBookContract(
                ApplicationSettings.GetInstance(""), 
                new User(),
                new DateTime(2009, 01, 01), null)
                                            {
                                                Product = product, 
                                                InterestRate = 0.1
                                            };
            saving.NextMaturity = saving.CreationDate;
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();
            list = saving.PostingInterests(
                                            saving.NextMaturity.Value, 
                                            new User { Id = 1 }
                                            );

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void PostingInterests_TenPosting()
        {
//            Assert.Ignore();
            SavingsBookProduct product = new SavingsBookProduct
                                            {
                                                Id = 1,
                                                InterestBase = OSavingInterestBase.Daily,
                                                InterestFrequency = OSavingInterestFrequency.EndOfDay,
                                                Periodicity = new InstallmentType("Daily", 1, 0)
                                             };
            DateTime postingDate = new DateTime(2009, 01, 10);
            SavingBookContract saving = new SavingBookContract(
                                                                ApplicationSettings.GetInstance(""), 
                                                                new User(),
                                                                new DateTime(2009, 01, 01), 
                                                                null)
                                            {
                                                Product = product, 
                                                InterestRate = 0.1,
                                                NextMaturity = new DateTime(2009, 01, 01)
                                                
                                            };

            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();
            List<SavingInterestsAccrualEvent> savingInterestsAccrualEvents = saving.CalculateInterest(
                                                                                        new DateTime(2009, 01, 10), 
                                                                                        new User {Id = 1});
            foreach (SavingInterestsAccrualEvent accrualEvent in savingInterestsAccrualEvents)
            {
                saving.Events.Add(accrualEvent);
            }

            saving.NextMaturity = saving.CreationDate.AddDays(-1);

            while (
                    DateCalculationStrategy.GetNextMaturity(saving.NextMaturity.Value, saving.Product.Periodicity, 1)
                    <=
                    postingDate)
            {
                saving.NextMaturity = DateCalculationStrategy.GetNextMaturity(saving.NextMaturity.Value,
                                                                              saving.Product.Periodicity, 1);
                list.AddRange(saving.PostingInterests(saving.NextMaturity.Value, new User { Id = 1 }));
                foreach (var postingEvent in list)
                {
                    saving.Events.Add(postingEvent);
                }
                list.Clear();
            }


            Assert.AreEqual(10, saving.Events.FindAll(item =>item is SavingInterestsPostingEvent).Count);
        }
    }
}
