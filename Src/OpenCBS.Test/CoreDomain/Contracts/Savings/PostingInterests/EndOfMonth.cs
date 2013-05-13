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
    public class EndOfMonth
    {
        [Test]
        public void PostingInterests_NoPosting()
        {
          SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                Periodicity = new InstallmentType("Monthly", 0, 1)
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
            list = saving.PostingInterests(new DateTime(2009, 01, 02), new User { Id = 1 });

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void PostingInterests_OnePosting()
        {
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                Periodicity = new InstallmentType("Monthly",0,1)
                
            };
            User user = new User() { Id = 1 };
            DateTime closureDate = new DateTime(2009, 02, 01);
            SavingBookContract saving = new SavingBookContract(
                                                                ApplicationSettings.GetInstance(""),
                                                                user,
                                                                new DateTime(2009, 01, 01), 
                                                                null
                                                              ) 
                                                            {
                                                                Product = product,
                                                                InterestRate = 0.1 
                                                            };
            saving.NextMaturity = DateCalculationStrategy.GetNextMaturity(saving.CreationDate.Date,
                saving.Periodicity, 1);
            
            List<SavingInterestsAccrualEvent> savingInterestsAccrualEvents =
                saving.CalculateInterest(closureDate, user);
            foreach (var accrualEvent in savingInterestsAccrualEvents)
            {
                saving.Events.Add(accrualEvent);
            }
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();
            list = saving.PostingInterests(closureDate, user);

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void PostingInterests_12Posting()
        {
//            Assert.Ignore();
            SavingsBookProduct product = new SavingsBookProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Daily,
                InterestFrequency = OSavingInterestFrequency.EndOfMonth,
                Periodicity = new InstallmentType("Monthly", 0, 1)

            };
           
            User user = new User() {Id = 1};
            DateTime creationDate = new DateTime(2009, 01, 01);
            SavingBookContract saving = new SavingBookContract(
                                                                    ApplicationSettings.GetInstance(""), 
                                                                    user,
                                                                    creationDate, 
                                                                    null
                                                                ) 
                                                            { 
                                                                Product = product,
                                                                InterestRate = 0.1
                                                            };
            DateTime closureDate = new DateTime(2010, 01, 01);
            saving.NextMaturity = saving.CreationDate.Date;
            saving.NumberOfPeriods = 1;
            saving.FirstDeposit(1000, creationDate, 0, user, new Teller());
            saving.NextMaturity = DateCalculationStrategy.GetNextMaturity(
                                                                        saving.CreationDate.Date, 
                                                                        saving.Product.Periodicity, 
                                                                        saving.NumberOfPeriods);
            List<SavingInterestsAccrualEvent> interestsAccrualEvents =
                                    saving.CalculateInterest(closureDate, new User {Id = 1});
            foreach (var accrualEvent in interestsAccrualEvents)
            {
                saving.Events.Add(accrualEvent);
            }
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();
            List<SavingInterestsPostingEvent> postingEvents = new List<SavingInterestsPostingEvent>();

            
            while (saving.NextMaturity.Value <= closureDate)
            {
                list = saving.PostingInterests(saving.NextMaturity.Value, new User() {Id = 1});
                postingEvents.AddRange(list);
                foreach (var postingEvent in list)
                {
                    saving.Events.Add(postingEvent);
                }
                saving.NextMaturity = DateCalculationStrategy.GetNextMaturity(saving.NextMaturity.Value, saving.Periodicity, 1);
            }

            list = saving.PostingInterests(new DateTime(2010, 01, 01), new User { Id = 1 });

            Assert.AreEqual(12, postingEvents.Count);
        }
    }
}
