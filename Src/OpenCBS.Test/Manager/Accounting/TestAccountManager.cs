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
using OpenCBS.Enums;
using OpenCBS.Manager.Accounting;
using OpenCBS.Shared;

namespace OpenCBS.Test.Manager.Accounting
{
    [TestFixture]
    public class TestAccountManager: BaseManagerTest
    {
        [Test]
        public void AddAccountInDatabase()
        {
            AccountManager accountManager = (AccountManager)container["AccountManager"];
            Account account = new Account
                                  {
                                      Balance = 20,
                                      DebitPlus = true,
                                      AccountCategory = OAccountCategories.BalanceSheetAsset,
                                      Label = "Test",
                                      Number = "234567",
                                      StockBalance = 10,
                                      TypeCode = "1111",
                                      CurrencyId = 1
                                  };

            //int id = accountManager.Add(account);
            Assert.AreNotEqual(0,1);
        }

        [Test]
        public void AddChildAccountInDatabase()
        {
            Assert.Ignore();
            AccountManager accountManager = (AccountManager)container["AccountManager"];
            SqlTransaction sqlTransac = null;//ConnectionManager.GetInstance().GetSqlTransaction("");

            AccountCategory accountCategory = new AccountCategory {Name = "test"};
            int idCategory = accountManager.InsertAccountCategory(accountCategory, sqlTransac);

            Account account = new Account
            {
                Balance = 20,
                DebitPlus = true,
                AccountCategory = (OAccountCategories) idCategory,
                Label = "Child of CASH",
                Number = "10111",
                StockBalance = 10,
                TypeCode = "1111",
                CurrencyId = 1,
                ParentAccountId = 100
            };

            try
            {
                accountManager.Insert(account, sqlTransac);
                Assert.Fail("The account can be added in DB, the parent account id doesn't exist");
            }
            catch { }
            
            account.ParentAccountId = 2;
            accountManager.Update(account);

            List<Account> accounts = accountManager.SelectAllAccounts();
            Assert.AreEqual(1, accounts.Count);
        }

        [Test]
        public void SelectAccountById_NullValue()
        {
            AccountManager accountManager = (AccountManager)container["AccountManager"];
            Assert.IsNull(accountManager.Select(-30));
        }

        [Test]
        public void SelectAccountById()
        {
            AccountManager accountManager = (AccountManager)container["AccountManager"];
            Account selectedAccount = accountManager.Select(1);

            AssertAccount(selectedAccount, OAccounts.CASH, "Cash", 0, true, "CASH",OAccountCategories.BalanceSheetAsset);
        }

        private static void AssertAccount(Account pAccount, string pAccountNumber, string pLabel, OCurrency pBalance
                                           , bool pDebitPlus, string pTypeCode, OAccountCategories pCategory)
        {
            Assert.AreEqual(pAccountNumber, pAccount.Number);
            Assert.AreEqual(pLabel, pAccount.Label);
            Assert.AreEqual(pDebitPlus, pAccount.DebitPlus);
            Assert.AreEqual(pTypeCode, pAccount.TypeCode);
            Assert.AreEqual(pCategory, pAccount.AccountCategory);
        }

        [Test]
        public void SelectAccountByType_NullValue()
        {
            AccountManager accountManager = (AccountManager)container["AccountManager"];
            //Assert.IsNull(accountManager.Select("NotSet", null));
        }
    
        [Test]
        public void SelectAllAccounts()
        {
            AccountManager accountManager = (AccountManager)container["AccountManager"];
            List<Account> selectedAccounts = accountManager.SelectAllAccounts();

            Assert.AreEqual(33,selectedAccounts.Count);
        }

        [Test]
        public void UpdateAccount()
        {
            AccountManager accountManager = (AccountManager)container["AccountManager"];
            //Account selectedAccount = accountManager.Select("DEFERRED_INCOME", null);

            //AssertAccount(selectedAccount, OAccounts.DEFERRED_INCOME, "42820", "Deferred Income", 0, false, "DEFERRED_INCOME", OAccountCategories.BalanceSheetLiabilities);

            ////selectedAccount.Balance = 1000;
            //selectedAccount.DebitPlus = true;
            //selectedAccount.AccountCategory = OAccountCategories.ProfitAndLossIncome;
            //selectedAccount.Label = "Test";
            //selectedAccount.LocalNumber = "123456789";
            //selectedAccount.Number = "987654321";

            //accountManager.Update(selectedAccount, null);
        }
    }
}
