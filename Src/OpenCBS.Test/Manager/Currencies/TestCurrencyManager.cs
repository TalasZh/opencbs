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
