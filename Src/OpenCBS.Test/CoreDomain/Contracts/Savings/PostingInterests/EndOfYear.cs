using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Test.CoreDomain.Contracts.Savings.PostingInterests
{
    [TestFixture]
    public class EndOfYear
    {
        [Test]
        public void PostingInterests_NoPosting()
        {
//            Assert.Ignore();
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                Periodicity = new InstallmentType("Yearly", 0, 12)
                
            };
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
            list = saving.PostingInterests(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 0);
        }

        [Test]
        public void PostingInterests_OnePosting()
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

            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();
            list = saving.PostingInterests(new DateTime(2010, 01, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
        }
    }
}
