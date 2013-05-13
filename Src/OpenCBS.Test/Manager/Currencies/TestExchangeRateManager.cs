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
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Manager.Currencies;
using OpenCBS.Shared;

namespace OpenCBS.Test.Manager.Currencies
{
    [TestFixture]
    public class TestExchangeRateManager : BaseManagerTest
    {
        [Test]
        public void AddExchangeRate()
        {
            ExchangeRateManager exchangeRateManager = (ExchangeRateManager) container["ExchangeRateManager"];


            exchangeRateManager.Add(new DateTime(2009,1,1),2,new Currency{Id = 1});

        }

        [Test]
        public void SelectExchangeRate_Null()
        {
            ExchangeRateManager exchangeRateManager = (ExchangeRateManager)container["ExchangeRateManager"];

            ExchangeRate selectedExchangeRate = exchangeRateManager.Select(new DateTime(3333, 2, 2),new Currency{Id = 1});

            Assert.IsNull(selectedExchangeRate);
        }

        [Test]
        public void SelectExchangeRate()
        {
            ExchangeRateManager exchangeRateManager = (ExchangeRateManager)container["ExchangeRateManager"];
            ExchangeRate selectedExchangeRate = exchangeRateManager.Select(new DateTime(2008, 8, 8), new Currency { Id = 2 });

            Assert.AreEqual(2, selectedExchangeRate.Rate);

        }

        [Test]
        public void SelectMostRecentlyExchangeRate()
        {
            ExchangeRateManager exchangeRateManager = (ExchangeRateManager)container["ExchangeRateManager"];
            double selectedRate = exchangeRateManager.GetMostRecentlyRate(TimeProvider.Today,new Currency {Id = 2});

            Assert.AreEqual(2, selectedRate);
        }

        [Test]
        public void SelectMostRecentlyExchangeRate_NoCurrency()
        {
            ExchangeRateManager exchangeRateManager = (ExchangeRateManager)container["ExchangeRateManager"];
            double selectedRate = exchangeRateManager.GetMostRecentlyRate(TimeProvider.Today,new Currency { Id = 0 });

            Assert.AreEqual(0, selectedRate);
        }

        [Test]
        public void UpdateExchangeRate()
        {
            ExchangeRateManager exchangeRateManager = (ExchangeRateManager)container["ExchangeRateManager"];
            ExchangeRate selectedExchangeRate = exchangeRateManager.Select(new DateTime(2008, 8, 8),new Currency{Id = 2});

            Assert.AreEqual(2,selectedExchangeRate.Rate);

            exchangeRateManager.Update(new DateTime(2008, 8, 8), 12, new Currency {Id = 2});

            ExchangeRate updatedExchangeRate = exchangeRateManager.Select(new DateTime(2008, 8, 8), new Currency { Id = 2 });

            Assert.AreEqual(12, updatedExchangeRate.Rate);
        }
    }
}
