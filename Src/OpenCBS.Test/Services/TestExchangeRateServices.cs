
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Manager.Accounting;
using OpenCBS.Services.Accounting;
using OpenCBS.Test.Manager;

namespace OpenCBS.Test.Services
{
    [TestFixture]
    public class TestExchangeRateServices : BaseManagerTest
    {
        [Test]
        [ExpectedException(typeof(OctopusAccountException))]
        public void AddExistingCurrency()
        {
            ExchangeRateServices exchangeRateServices = new ExchangeRateServices(new User{Id = 5});

            Currency newCurrency = new Currency
                                       {
                                           Code = "US Dollar",
                                           Id = 1,
                                           IsPivot = false,
                                           Name = "USD"
                                       };

            exchangeRateServices.AddNewCurrency(newCurrency);
            int id = exchangeRateServices.SelectCurrency(newCurrency);
            Assert.IsTrue(id > 0);
            exchangeRateServices.AddNewCurrency(newCurrency);

        }
        [Test]
        [ExpectedException(typeof(OctopusAccountException))]
        public void AddMoreThanTenCurrencies()
        {
            ExchangeRateServices exchangeRateServices = new ExchangeRateServices(new User { Id = 5 });
            for (int i = 0; i < 11; i++)
            {
                Currency newCurrency = new Currency
                                           {
                                               Code = i.ToString(),
                                               Id = i,
                                               IsPivot = false,
                                               Name = i.ToString()
                                           };
                exchangeRateServices.AddNewCurrency(newCurrency);
            }
        }
    }
}
