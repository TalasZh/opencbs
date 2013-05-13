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
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Products;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Shared.Settings;
using OpenCBS.CoreDomain.Contracts.Loans;

namespace OpenCBS.Test.CoreDomain.Accounting
{
    [TestFixture]
    public class TestAccountingRuleCollection
    {
        private AccountingRuleCollection _rules;
        private List<Account> _accounts;

        private LoanProduct _loanProductEde34;
        private LoanProduct _loanProductEde60;

        [SetUp]
        public void SetUp()
        {
            _rules = new AccountingRuleCollection();
            _accounts = DefaultAccounts.DefaultAccount(1);

            Account cashSavings = new Account("1020", "CASH_SAVINGS", 0, "CASH_SAVIGNS", true, OAccountCategories.BalanceSheetAsset, 1);
            _accounts.Add(cashSavings);

            _rules.Add(new ContractAccountingRule
            {
                DebitAccount = _accounts[0],
                CreditAccount = cashSavings,
                ProductType = OProductTypes.Saving,
                SavingProduct = null,
                ClientType = OClientTypes.All,
                EconomicActivity = null,
                BookingDirection = OBookingDirections.Both
            });

            _loanProductEde34 = new LoanProduct { Id = 1, Code = "EDE34", Name = "EDEN 34", Currency = new Currency { Id = 1} };

            Account cashEDE34 = new Account("1051", "CASH_EDE34", 0, "CASH_EDE34", true, OAccountCategories.BalanceSheetAsset, 1);
            _accounts.Add(cashEDE34);

            _rules.Add(new ContractAccountingRule
            {
                DebitAccount = _accounts[0],
                CreditAccount = cashEDE34,
                ProductType = OProductTypes.Loan,
                LoanProduct = _loanProductEde34,
                ClientType = OClientTypes.Person,
                EconomicActivity = new EconomicActivity(1, "Agriculture", null, false),
                BookingDirection = OBookingDirections.Both
            });

            _loanProductEde60 = new LoanProduct { Id = 2, Code = "EDE60", Name = "EDEN 60", Currency = new Currency { Id = 1 } };

            Account cashEDE60 = new Account("1052", "CASH_EDE60", 0, "CASH_EDE60", true, OAccountCategories.BalanceSheetAsset, 1);
            _accounts.Add(cashEDE60);

            _rules.Add(new ContractAccountingRule
            {
                DebitAccount = _accounts[0],
                CreditAccount = cashEDE60,
                ProductType = OProductTypes.Loan,
                LoanProduct = _loanProductEde60,
                ClientType = OClientTypes.Person,
                EconomicActivity = new EconomicActivity(1, "Agriculture", null, false),
                BookingDirection = OBookingDirections.Credit
            });
        }

        [Test]
        public void Test_RetrieveForSavings()
        {
            SavingsBookProduct savingsBookProduct = new SavingsBookProduct() { Id = 1};

            Person person = new Person
            {
                FirstName = "Michael",
                LastName = "Krejci",
                Activity = new EconomicActivity(1, "Agriculture", null, false)
            };

            SavingBookContract saving = new SavingBookContract(ApplicationSettings.GetInstance(""),
                                                               new User(),
                                                               savingsBookProduct);
            person.Savings.Add(saving);

            Account specficAccount = _rules.GetSpecificAccount(OAccounts.CASH_CREDIT, saving, OBookingDirections.Both);
            Assert.AreEqual(null, specficAccount);

            specficAccount = _rules.GetSpecificAccount(OAccounts.CASH, saving, OBookingDirections.Both);
            Assert.AreEqual("1020", specficAccount.Number);
        }

        [Test]
        public void Test_RetrieveForLoans()
        {
            Person person = new Person
            {
                FirstName = "Michael",
                LastName = "Krejci",
                Activity = new EconomicActivity(1, "Agriculture", null, false)
            };

            Loan loan = new Loan { Product = new LoanProduct { Id = 2, Currency = new Currency { Id = 1 } } };
            person.AddProject(new Project());
            person.Projects[0].AddCredit(loan, OClientTypes.Person);

            Account specificAccount = _rules.GetSpecificAccount(OAccounts.CASH_CREDIT, loan, OBookingDirections.Both);
            Assert.AreEqual(null, specificAccount);

            specificAccount = _rules.GetSpecificAccount(OAccounts.CASH, loan, OBookingDirections.Both);
            Assert.AreEqual(null, specificAccount);

            loan.Product = _loanProductEde34;
            specificAccount = _rules.GetSpecificAccount(OAccounts.CASH, loan, OBookingDirections.Both);
            Assert.AreEqual("1051", specificAccount.Number);

            loan.Product = _loanProductEde60;
            specificAccount = _rules.GetSpecificAccount(OAccounts.CASH, loan, OBookingDirections.Credit);
            Assert.AreEqual("1052", specificAccount.Number);

            loan.Product = _loanProductEde60;
            specificAccount = _rules.GetSpecificAccount(OAccounts.CASH, loan, OBookingDirections.Debit);
            Assert.AreEqual(null, specificAccount);
        }
    }
}
