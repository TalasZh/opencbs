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
