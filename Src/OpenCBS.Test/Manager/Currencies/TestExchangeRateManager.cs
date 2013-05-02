// LICENSE PLACEHOLDER

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
