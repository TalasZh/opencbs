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
using System.Text;
using NUnit.Framework;
using OpenCBS.CoreDomain.Products;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain;
using OpenCBS.Shared;
using OpenCBS.Enums;
using NUnit.Framework.SyntaxHelpers;
using OpenCBS.CoreDomain.Events.Saving;

namespace OpenCBS.Test.CoreDomain.Contracts.Savings.CalculateInterests
{
    [TestFixture]
    public class Monthly
    {
        [Test]
        public void CalculateInterest_NoDay()
        {
            SavingProduct product = new SavingProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            Saving saving = new Saving(ApplicationSettings.GetInstance(""), new User(),
                1000, new DateTime(2009, 07, 01)) { Product = product, InterestRate = 0.1 };

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 07, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 0);
        }

        [Test]
        public void CalculateInterest_OneDay()
        {
            SavingProduct product = new SavingProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            Saving saving = new Saving(ApplicationSettings.GetInstance(""), new User(),
                1000, new DateTime(2009, 07, 01)) { Product = product, InterestRate = 0.1 };

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 07, 02), new User { Id = 1 });

            Assert.AreEqual(list.Count, 0);
        }

        [Test]
        public void CalculateInterest_OneMonth()
        {
            SavingProduct product = new SavingProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            Saving saving = new Saving(ApplicationSettings.GetInstance(""), new User(),
                1000, new DateTime(2009, 01, 01)) { Product = product, InterestRate = 0.1 };

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 100);
        }

        [Test]
        public void CalculateInterest_OneMonth_Starting15()
        {
            SavingProduct product = new SavingProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            Saving saving = new Saving(ApplicationSettings.GetInstance(""), new User(),
                1000, new DateTime(2009, 01, 15)) { Product = product, InterestRate = 0.1 };

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 100);
        }

        [Test]
        public void CalculateInterest_OneMonth_Deposit_FirstDayOfMonth()
        {
            SavingProduct product = new SavingProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            Saving saving = new Saving(ApplicationSettings.GetInstance(""), new User(),
                1000, new DateTime(2009, 01, 01)) { Product = product, InterestRate = 0.1 };

            saving.Deposit(100, new DateTime(2009, 01, 01), "depot", new User()); 

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 110);
        }

        [Test]
        public void CalculateInterest_OneMonth_Withdraw_FirstDayOfMonth()
        {
            SavingProduct product = new SavingProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            Saving saving = new Saving(ApplicationSettings.GetInstance(""), new User(),
                1000, new DateTime(2009, 01, 01)) { Product = product, InterestRate = 0.1 };

            saving.Withdraw(100, new DateTime(2009, 01, 01), "retrait", new User());

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 90);
        }

        [Test]
        public void CalculateInterest_OneMonth_Deposit()
        {
            SavingProduct product = new SavingProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            Saving saving = new Saving(ApplicationSettings.GetInstance(""), new User(),
                1000, new DateTime(2009, 01, 01)) { Product = product, InterestRate = 0.1 };

            saving.Deposit(100, new DateTime(2009, 01, 15), "depot", new User());

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 100);
        }

        [Test]
        public void CalculateInterest_OneMonth_Withdraw()
        {
            SavingProduct product = new SavingProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            Saving saving = new Saving(ApplicationSettings.GetInstance(""), new User(),
                1000, new DateTime(2009, 01, 01)) { Product = product, InterestRate = 0.1 };

            saving.Withdraw(100, new DateTime(2009, 01, 15), "depot", new User());

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2009, 02, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].Amount, 90);
        }


        [Test]
        public void CalculateInterest_OneYear()
        {
            SavingProduct product = new SavingProduct
            {
                Id = 1,
                InterestBase = OSavingInterestBase.Monthly,
                InterestFrequency = OSavingInterestFrequency.EndOfYear,
                CalculAmountBase = OSavingCalculAmountBase.MinimalAmount
            };
            Saving saving = new Saving(ApplicationSettings.GetInstance(""), new User(),
                1000, new DateTime(2009, 01, 01)) { Product = product, InterestRate = 0.1 };

            List<SavingInterestsAccrualEvent> list = new List<SavingInterestsAccrualEvent>();
            list = saving.CalculateInterest(new DateTime(2010, 01, 01), new User { Id = 1 });

            Assert.AreEqual(list.Count, 12);
        }
    }
}
