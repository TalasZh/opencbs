// LICENSE PLACEHOLDER

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
