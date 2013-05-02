// LICENSE PLACEHOLDER

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

namespace OpenCBS.Test.CoreDomain.Contracts.Savings.PostingInterests
{
    [TestFixture]
    public class EndOfWeek
    {
        [Test]
        public void PostingInterests_NoPosting()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfWeek,
                Periodicity = new InstallmentType("Weekly", 7, 0)
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

            saving.NextMaturity = new DateTime(2009, 01, 01);
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();
            
            list = saving.PostingInterests(new DateTime(2009, 01, 02), new User { Id = 1 });

            Assert.AreEqual(0,list.Count);
        }

        [Test]
        public void PostingInterests_OnePosting()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfWeek,
                Periodicity = new InstallmentType("Weekly", 7, 0)
                
            };
            User user = new User(){Id=1};
            
            SavingBookContract saving = new SavingBookContract(
                                                                ApplicationSettings.GetInstance(""),
                                                                user,
                                                                new DateTime(2009, 01, 01), 
                                                                null)
                                                                {
                                                                    Product = product, InterestRate = 0.1
                                                                };
            saving.NextMaturity = saving.GetNextMaturity(saving.CreationDate, saving.Periodicity);
            saving.FirstDeposit(1000, saving.CreationDate, 0, user, new Teller());
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();
            List<SavingInterestsPostingEvent> postingEvents = new List<SavingInterestsPostingEvent>();
            DateTime closureDate = new DateTime(2009, 01, 05);
            List<SavingInterestsAccrualEvent> accrualEvents = saving.CalculateInterest(closureDate, user);
            foreach (var accrualEvent in accrualEvents)
            {
                saving.Events.Add(accrualEvent);
            }

            list = saving.PostingInterests(saving.NextMaturity.Value, user);
            postingEvents.AddRange(list);
            foreach (var postingEvent in list)
            {
                saving.Events.Add(postingEvent);
            }

            Assert.AreEqual( 1, postingEvents.Count);
        }

        [Test]
        public void PostingInterests_ThreePosting()
        {
//            Assert.Ignore();
            SavingsBookProduct product = new SavingsBookProduct
                                            {
                                                Id = 1,
                                                InterestBase = OSavingInterestBase.Daily,
                                                InterestFrequency = OSavingInterestFrequency.EndOfWeek,
                                                Periodicity = new InstallmentType("Weekly", 7, 0)
                
                                            };
            User user = new User() {Id = 1};
            DateTime creationDate = new DateTime(2009, 01, 01);
            DateTime postingDate = new DateTime(2009, 01, 26);
            SavingBookContract saving = new SavingBookContract(
                                                                ApplicationSettings.GetInstance(""),  
                                                                user,
                                                                creationDate, 
                                                                null)
                                            {
                                                Product = product, 
                                                InterestRate = 0.1
                                            };
            saving.FirstDeposit(1000, saving.CreationDate, 0, user, new Teller());
            List<SavingInterestsAccrualEvent> accrualEvents= saving.CalculateInterest(postingDate, user);
            foreach (var accrualEvent in accrualEvents)
            {
                saving.Events.Add(accrualEvent);
            }
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();
            List<SavingInterestsPostingEvent> postingEvents = new List<SavingInterestsPostingEvent>();
            saving.NextMaturity = saving.GetNextMaturity(saving.CreationDate, saving.Periodicity);
            while (saving.NextMaturity<=postingDate)
            {
                list = saving.PostingInterests(saving.NextMaturity.Value, new User { Id = 1 });
                postingEvents.AddRange(list);
                foreach (var postingEvent in list)
                {
                    saving.Events.Add(postingEvent);
                }
                saving.NextMaturity = saving.GetNextMaturity(saving.NextMaturity.Value, saving.Periodicity);
            }
            
            Assert.AreEqual(3, postingEvents.Count);
        }
    }
}
