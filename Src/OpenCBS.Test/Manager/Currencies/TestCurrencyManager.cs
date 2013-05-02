// LICENSE PLACEHOLDER

using System.Collections.Generic;
using System.Data.SqlClient;
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Manager.Currencies;

namespace OpenCBS.Test.Manager.Currencies
{
    [TestFixture]
    public class TestCurrencyManager : BaseManagerTest
    {
        [Test]
        public void Add_Currency()
        {
            CurrencyManager currencyManager = (CurrencyManager)container["CurrencyManager"];
            using (SqlConnection conn = currencyManager.GetConnection())
            {
                Currency currency = new Currency { Code = "US Dollar", IsPivot = false, Name = "USD" };
                SqlTransaction t = conn.BeginTransaction();
                currency.Id = currencyManager.Add(currency, t);
                Assert.AreNotEqual(0, currency.Id);   
            }
        }

        [Test]
        public void SelectAllCurrency()
        {
            CurrencyManager currencyManager = (CurrencyManager)container["CurrencyManager"];
            using (SqlConnection conn = currencyManager.GetConnection())
            {
                SqlTransaction t = conn.BeginTransaction();
                List<Currency> list = currencyManager.SelectAllCurrencies(t);
                Assert.AreEqual(2, list.Count);
                _AssertCurrency(list[0], 1, "KGS", "KGS", true);
                _AssertCurrency(list[1], 2, "USD", "USD", false);
            }
        }



        private static void _AssertCurrency(Currency pSelectedCurrency, int pId, string pName, string pCode, bool pIsPivot)
        {
            Assert.AreEqual(pSelectedCurrency.Id, pId);
            Assert.AreEqual(pSelectedCurrency.Name, pName);
            Assert.AreEqual(pSelectedCurrency.Code, pCode);
            Assert.AreEqual(pSelectedCurrency.IsPivot, pIsPivot);
        }
    }
}
