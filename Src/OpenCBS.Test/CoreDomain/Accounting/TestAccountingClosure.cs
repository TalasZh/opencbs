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
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.Events;
using OpenCBS.CoreDomain.Events.Teller;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;

namespace OpenCBS.Test.CoreDomain.Accounting
{
    [TestFixture]
    class TestAccountingClosure
    {
        AccountingRuleCollection _rules = new AccountingRuleCollection();
        private List<Account> _accounts;
        private readonly List<Teller> _tellers = new List<Teller>();
        private LoanProduct _loanProductEde34;
        private LoanProduct _loanProductEde60;

        [SetUp]
        public void SetUp()
        {
            _rules = new AccountingRuleCollection();
            _accounts = DefaultAccounts.DefaultAccount(1);

            Account account1 = new Account("1020", "Test1", 0, "Test1", true, OAccountCategories.BalanceSheetAsset, 1);
            _accounts.Add(account1);

            Account account2 = new Account("1052", "Test2", 0, "Test2", true, OAccountCategories.BalanceSheetAsset, 1);
            _accounts.Add(account2);

            Account account3 = new Account("1051", "Test3", 0, "Test3", true, OAccountCategories.BalanceSheetAsset, 1);
            _accounts.Add(account3);

            Account vaultAccount = new Account("1999", "VaultAccount", 0, "VA", true, OAccountCategories.BalanceSheetAsset, 1);
            _accounts.Add(vaultAccount);

            Account tellerAccount1 = new Account("1991", "TellerAccount1", 0, "TA1", true, OAccountCategories.BalanceSheetAsset, 1);
            _accounts.Add(tellerAccount1);

            Account tellerAccount2 = new Account("1992", "TellerAccount2", 0, "TA2", true, OAccountCategories.BalanceSheetAsset, 1);
            _accounts.Add(tellerAccount2);

            _loanProductEde60 = new LoanProduct { Id = 2, Code = "EDE60", Name = "EDEN 60", Currency = new Currency { Id = 1 } };
            _loanProductEde34 = new LoanProduct { Id = 1, Code = "EDE34", Name = "EDEN 34", Currency = new Currency { Id = 1 } };

            var vault = new Teller
                               {
                                   Id = 1,
                                   Name = "Vault",
                                   Account = vaultAccount,
                                   Branch = new Branch {Id = 1},
                                   Currency = new Currency {Id = 1},
                                   User = new User {Id = 0},
                                   Deleted = false
                               };
            _tellers.Add(vault);

            var teller1 = new Teller
                              {
                                  Id = 2,
                                  Name = "Teller1",
                                  Account = tellerAccount1,
                                  Branch = new Branch {Id = 1},
                                  Currency = new Currency {Id = 1},
                                  User = new User {Id = 1},
                                  Deleted = false,
                                  Vault = vault
                              };

            _tellers.Add(teller1);

            var teller2 = new Teller
                              {
                                  Id = 3,
                                  Name = "Teller2",
                                  Account = tellerAccount2,
                                  Branch = new Branch {Id = 1},
                                  Currency = new Currency {Id = 1},
                                  User = new User {Id = 2},
                                  Deleted = false,
                                  Vault = vault
                              };
            _tellers.Add(teller2);

            _rules.Add(new ContractAccountingRule
            {
                DebitAccount = account2,
                CreditAccount = account3,
                Order = 1,
                EventType = new EventType("RGLE"),
                EventAttribute = new EventAttribute("principal", "RGLE"),
                ProductType = OProductTypes.Loan,
                LoanProduct = _loanProductEde60,
                ClientType = OClientTypes.Person,
                //EconomicActivity = new EconomicActivity(1, "Agriculture", null, false),
                BookingDirection = OBookingDirections.Credit,
                Currency = new Currency { Id = 1 }
            });

            _rules.Add(new ContractAccountingRule
                           {
                               DebitAccount = account1,
                               CreditAccount = account2,
                               Order = 4,
                               EventType = new EventType("RGLE"),
                               EventAttribute = new EventAttribute("principal", "RGLE"),
                               ProductType = OProductTypes.All,
                               LoanProduct = null,
                               ClientType = OClientTypes.Group,
                               EconomicActivity = null,
                               BookingDirection = OBookingDirections.Credit,
                               Currency = new Currency { Id = 2 }
                           });

            _rules.Add(new ContractAccountingRule
            {
                DebitAccount = account1,
                CreditAccount = account2,
                Order = 9,
                EventType = new EventType("RGLE"),
                EventAttribute = new EventAttribute("principal", "RGLE"),
                ProductType = OProductTypes.All,
                SavingProduct = null,
                ClientType = OClientTypes.All,
                EconomicActivity = null,
                BookingDirection = OBookingDirections.Both
            });
            
            _rules.Add(new ContractAccountingRule
            {
                DebitAccount = account2,
                CreditAccount = account1,
                Order = 3,
                EventType = new EventType("LODE"),
                EventAttribute = new EventAttribute("amount", "LODE"),
                ProductType = OProductTypes.Loan,
                LoanProduct = null,
                ClientType = OClientTypes.Person,
                EconomicActivity = new EconomicActivity(1, "Agriculture", null, false),
                BookingDirection = OBookingDirections.Both
            });
        }

        [Test]
        public void TestSimpleClosure()
        {
            EventStock eventStock = new EventStock
                                        {
                                            new LoanDisbursmentEvent
                                                {
                                                    Id = 1,
                                                    Date = new DateTime(2000,1,1),
                                                    Amount = 100,
                                                    PaymentMethod = new PaymentMethod {Id = 0},
                                                    ClientType = OClientTypes.Person,
                                                    EconomicActivity =
                                                        new EconomicActivity(1, "Agriculture", null, false),
                                                        Currency = new Currency(){Id = 0}
                                                },
                                            new RepaymentEvent
                                                {
                                                    Id = 2,
                                                    Principal = 100,
                                                    Interests = 5,
                                                    Penalties = 1,
                                                    Commissions = 0,
                                                    ClientType = OClientTypes.All,
                                                    Date = new DateTime(2000,1,1),
                                                    Currency = new Currency(){Id = 0}
                                                }
                                        };

            List<FiscalYear> fYears = new List<FiscalYear> { new FiscalYear { OpenDate = new DateTime(1900, 1, 1) } };
            AccountingClosure closure =  new AccountingClosure();
            List<Booking> bookings = closure.GetBookings(_rules, eventStock, null, null, null, fYears);

            Assert.AreEqual(bookings[0].Amount, 100);
            Assert.AreEqual(bookings[0].DebitAccount.Number, "1052");
            Assert.AreEqual(bookings[0].CreditAccount.Number, "1020");
        }

        [Test]
        public void TestClosureEventWithCurrency()
        {
            EventStock eventStock = new EventStock
                                        {
                                            new LoanDisbursmentEvent
                                                {
                                                    Id = 1,
                                                    Date = new DateTime(2000,1,1),
                                                    Amount = 100,
                                                    PaymentMethod = new PaymentMethod {Id = 0},
                                                    ClientType = OClientTypes.Person,
                                                    EconomicActivity = new EconomicActivity(1, "Agriculture", null, false),
                                                    Currency = new Currency{Id =1}
                                                },
                                            new RepaymentEvent
                                                {
                                                    Id = 2,
                                                    Principal = 100,
                                                    Interests = 5,
                                                    Penalties = 1,
                                                    Commissions = 0,
                                                    Date = new DateTime(2000,1,1),
                                                    Currency = new Currency {Id = 1},
                                                    ClientType = OClientTypes.Person
                                                },
                                        };

            eventStock.GetRepaymentEvents()[0].LoanProduct = new LoanProduct
                                                                 {
                                                                     Id = 2,
                                                                     Code = "EDE60",
                                                                     Name = "EDEN 60",
                                                                     Currency = new Currency {Id = 1}
                                                                 };
            AccountingClosure closure = new AccountingClosure();
            List<FiscalYear> fYears = new List<FiscalYear> {new FiscalYear{OpenDate = new DateTime(1900,1,1)}};
            List<Booking> bookings = closure.GetBookings(_rules, eventStock, null, null, null, fYears);
            Assert.AreEqual(bookings[0].Amount, 100);
            Assert.AreEqual(bookings[0].DebitAccount.Number, "1052");
            Assert.AreEqual(bookings[0].CreditAccount.Number, "1020");
        }

        [Test]
        public void TestClosureLodeEvent()
        {
            EventStock eventStock = new EventStock
                                        {
                                            new LoanDisbursmentEvent
                                                {
                                                    Id = 1,
                                                    Amount = 100,
                                                    Date = new DateTime(2000,1,1),
                                                    EconomicActivity =
                                                        new EconomicActivity(1, "Agriculture", null, false),
                                                    ClientType = OClientTypes.Person,
                                                    PaymentMethod = new PaymentMethod {Id = 0},
                                                    LoanProduct =
                                                        new LoanProduct
                                                            {
                                                                Id = 1,
                                                                Code = "EDE34",
                                                                Name = "EDEN 34",
                                                                Currency = new Currency {Id = 1}
                                                            },
                                                             Currency = new Currency {Id = 0}
                                                }
                                        };

            List<FiscalYear> fYears = new List<FiscalYear> { new FiscalYear { OpenDate = new DateTime(1900, 1, 1) } };
            AccountingClosure closure = new AccountingClosure();
            List<Booking> bookings = closure.GetBookings(_rules, eventStock, null, null, null, fYears);
            Assert.AreEqual(bookings[0].Amount, 100);
            Assert.AreEqual(bookings[0].DebitAccount.Number, "1052");
            Assert.AreEqual(bookings[0].CreditAccount.Number, "1020");
        }

        [Test]
        public void TestClosureTellerEvents()
        {
            EventStock eventStock = new EventStock
                                        {
                                            new TellerCashInEvent
                                                {
                                                    Id = 1,
                                                    Amount = 1000,
                                                    Currency = new Currency {Id = 1},
                                                    Date = new DateTime(2000, 1, 1),
                                                    TellerId = 2
                                                }
                                        };

            eventStock.Add(new EventStock
                               {
                                   new TellerCashOutEvent
                                       {
                                           Id = 2,
                                           Amount = 267,
                                           Currency = new Currency {Id = 1},
                                           Date = new DateTime(2000, 1, 1),
                                           TellerId = 3
                                       }
                               });

            List<FiscalYear> fYears = new List<FiscalYear> { new FiscalYear { OpenDate = new DateTime(1900, 1, 1) } };
            AccountingClosure closure = new AccountingClosure();
            List<Booking> bookings = closure.GetBookings(_rules, eventStock, _tellers, null, null, fYears);
            Assert.AreEqual(bookings[0].Amount, 1000);
            Assert.AreEqual(bookings[0].CreditAccount.Number, "1991");
            Assert.AreEqual(bookings[0].DebitAccount.Number, "1999");
            Assert.AreEqual(bookings[1].Amount, 267);
            Assert.AreEqual(bookings[1].CreditAccount.Number, "1999");
            Assert.AreEqual(bookings[1].DebitAccountNumber, "1992");
        }
    }
}
