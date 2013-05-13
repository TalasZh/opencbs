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

using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.Events;
using OpenCBS.Manager.Accounting;
using OpenCBS.Enums;
using OpenCBS.Manager.Events;
using OpenCBS.Manager.Products;
using OpenCBS.Manager;
using OpenCBS.CoreDomain.Products;
using OpenCBS.CoreDomain.FundingLines;

namespace OpenCBS.Test.Manager.Accounting
{
    [TestFixture]
    public class TestAccountingRuleManager : BaseManagerTest
    {
        private AccountingRuleManager _accountingRuleManager;
        private AccountManager _accountManager;
        private LoanProductManager _loanProductManager;
        private SavingProductManager _savingProductManager;
        private EconomicActivityManager _economicActivityManager;
        private FundingLineManager _fundingLineManager;
        private EventManager _eventManager;

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();

            _accountingRuleManager = (AccountingRuleManager)container["AccountingRuleManager"];
            _accountManager = (AccountManager)container["AccountManager"];
            _loanProductManager = (LoanProductManager)container["LoanProductManager"];
            _savingProductManager = (SavingProductManager)container["SavingProductManager"];
            _economicActivityManager = (EconomicActivityManager)container["EconomicActivityManager"];
            _fundingLineManager = (FundingLineManager)container["FundingLineManager"];
            _eventManager = (EventManager)container["EventManager"];

            Assert.IsNotNull(_accountingRuleManager);
            Assert.IsNotNull(_accountManager);
            Assert.IsNotNull(_loanProductManager);
            Assert.IsNotNull(_savingProductManager);
            Assert.IsNotNull(_economicActivityManager);
            Assert.IsNotNull(_fundingLineManager);
            Assert.IsNotNull(_eventManager);
        }

        private void _compareRules(ContractAccountingRule originalRule, ContractAccountingRule retrievedRule)
        {
            Assert.AreEqual(originalRule.Id, retrievedRule.Id);
            Assert.AreEqual(originalRule.DebitAccount.Id, retrievedRule.DebitAccount.Id);
            Assert.AreEqual(originalRule.CreditAccount.Id, retrievedRule.CreditAccount.Id);

            Assert.AreEqual(originalRule.ProductType, retrievedRule.ProductType);
            if (originalRule.LoanProduct != null)
                Assert.AreEqual(originalRule.LoanProduct.Id, retrievedRule.LoanProduct.Id);
            else
                Assert.AreEqual(null, retrievedRule.LoanProduct);
            if (originalRule.SavingProduct != null)
                Assert.AreEqual(originalRule.SavingProduct.Id, retrievedRule.SavingProduct.Id);
            else
                Assert.AreEqual(null, retrievedRule.SavingProduct);
            Assert.AreEqual(originalRule.ClientType, retrievedRule.ClientType);
            if (originalRule.EconomicActivity != null)
                Assert.AreEqual(originalRule.EconomicActivity.Id, retrievedRule.EconomicActivity.Id);
            else
                Assert.AreEqual(null, retrievedRule.EconomicActivity);
        }

        [Test]
        public void AddContractRuleForAll()
        {
            Account genericAccount = _accountManager.Select(1);
            Account specificAccount = _accountManager.Select(2);
            EventType eventType = _eventManager.SelectEventTypeByEventType("RGLE");
            EventAttribute eventAttribute = _eventManager.SelectEventAttributeByCode("principal");

            ContractAccountingRule rule = new ContractAccountingRule
                                              {
                                                  DebitAccount = genericAccount,
                                                  CreditAccount = specificAccount,
                                                  ProductType = OProductTypes.Loan,
                                                  ClientType = OClientTypes.All,
                                                  BookingDirection = OBookingDirections.Both,
                                                  EventType = eventType,
                                                  EventAttribute = eventAttribute
                                              };

            rule.Id = _accountingRuleManager.AddAccountingRule(rule);
            Assert.AreNotEqual(0, rule.Id);

            ContractAccountingRule retrievedRule = _accountingRuleManager.Select(rule.Id) as ContractAccountingRule;
            _compareRules(rule, retrievedRule);
        }

        [Test]
        public void AddContractRuleForLoanProduct()
        {
            Account genericAccount = _accountManager.Select(1);
            Account specificAccount = _accountManager.Select(2);
            LoanProduct loanProduct = _loanProductManager.Select(1);
            EventType eventType = _eventManager.SelectEventTypeByEventType("RGLE");
            EventAttribute eventAttribute = _eventManager.SelectEventAttributeByCode("principal");

            ContractAccountingRule rule = new ContractAccountingRule
            {
                DebitAccount = genericAccount,
                CreditAccount = specificAccount,
                ProductType = OProductTypes.Loan,
                LoanProduct = loanProduct,
                ClientType = OClientTypes.Corporate,
                BookingDirection = OBookingDirections.Credit,
                EventAttribute = eventAttribute,
                EventType = eventType
            };

            rule.Id = _accountingRuleManager.AddAccountingRule(rule);
            Assert.AreNotEqual(0, rule.Id);

            ContractAccountingRule retrievedRule = _accountingRuleManager.Select(rule.Id) as ContractAccountingRule;
            _compareRules(rule, retrievedRule);
        }

        [Test]
        public void AddContractRuleForSavingsProduct()
        {
            Account genericAccount = _accountManager.Select(1);
            Account specificAccount = _accountManager.Select(2);
            ISavingProduct savingsProduct = _savingProductManager.SelectSavingProduct(1);

            EventType eventType = _eventManager.SelectEventTypeByEventType("RGLE");
            EventAttribute eventAttribute = _eventManager.SelectEventAttributeByCode("principal");

            ContractAccountingRule rule = new ContractAccountingRule
            {
                DebitAccount = genericAccount,
                CreditAccount = specificAccount,
                ProductType = OProductTypes.Saving,
                SavingProduct = savingsProduct,
                ClientType = OClientTypes.Person,
                BookingDirection = OBookingDirections.Both,
                EventAttribute = eventAttribute,
                EventType = eventType
            };

            rule.Id = _accountingRuleManager.AddAccountingRule(rule);
            Assert.AreNotEqual(0, rule.Id);

            ContractAccountingRule retrievedRule = _accountingRuleManager.Select(rule.Id) as ContractAccountingRule;
            _compareRules(rule, retrievedRule);
        }

        [Test]
        public void AddContractRuleForEconomicActivity()
        {
            Account genericAccount = _accountManager.Select(1);
            Account specificAccount = _accountManager.Select(2);
            EconomicActivity economicActivty = _economicActivityManager.SelectEconomicActivity(1);

            EventType eventType = _eventManager.SelectEventTypeByEventType("RGLE");
            EventAttribute eventAttribute = _eventManager.SelectEventAttributeByCode("principal");

            ContractAccountingRule rule = new ContractAccountingRule
                                              {
                                                  DebitAccount = genericAccount,
                                                  CreditAccount = specificAccount,
                                                  ProductType = OProductTypes.All,
                                                  ClientType = OClientTypes.Village,
                                                  EconomicActivity = economicActivty,
                                                  BookingDirection = OBookingDirections.Credit,
                                                  EventType = eventType,
                                                  EventAttribute = eventAttribute
                                              };

            rule.Id = _accountingRuleManager.AddAccountingRule(rule);
            Assert.AreNotEqual(0, rule.Id);

            ContractAccountingRule retrievedRule = _accountingRuleManager.Select(rule.Id) as ContractAccountingRule;
            _compareRules(rule, retrievedRule);
        }

        [Test]
        public void AddFundingLineRuleForFundingLine()
        {
            Account genericAccount = _accountManager.Select(1);
            Account specificAccount = _accountManager.Select(2);
            FundingLine fundingLine = _fundingLineManager.SelectFundingLineById(1, false);
            EventType eventType = _eventManager.SelectEventTypeByEventType("RGLE");
            EventAttribute eventAttribute = _eventManager.SelectEventAttributeByCode("principal");

            FundingLineAccountingRule rule = new FundingLineAccountingRule
            {
                DebitAccount = genericAccount,
                CreditAccount = specificAccount,
                FundingLine = fundingLine,
                BookingDirection = OBookingDirections.Credit,
                EventAttribute = eventAttribute,
                EventType = eventType
            };

            rule.Id = _accountingRuleManager.AddAccountingRule(rule);
            Assert.AreNotEqual(0, rule.Id);
        }

        [Test]
        public void UpdateContractRule()
        {
            Account genericAccount = _accountManager.Select(1);
            Account specificAccount = _accountManager.Select(2);
            EventType eventType = _eventManager.SelectEventTypeByEventType("RGLE");
            EventAttribute eventAttribute = _eventManager.SelectEventAttributeByCode("principal");

            ContractAccountingRule rule = new ContractAccountingRule
            {
                DebitAccount = genericAccount,
                CreditAccount = specificAccount,
                ProductType = OProductTypes.Loan,
                ClientType = OClientTypes.All,
                BookingDirection = OBookingDirections.Both,
                EventType = eventType,
                EventAttribute = eventAttribute
            };

            rule.Id = _accountingRuleManager.AddAccountingRule(rule);
            Assert.AreNotEqual(0, rule.Id);

            rule.DebitAccount = _accountManager.Select(2);
            rule.CreditAccount = _accountManager.Select(4);
            rule.ProductType = OProductTypes.Saving;
            rule.LoanProduct = null;
            rule.SavingProduct = _savingProductManager.SelectSavingProduct(1);
            rule.ClientType = OClientTypes.Person;
            rule.EconomicActivity = _economicActivityManager.SelectEconomicActivity(1);
            rule.BookingDirection = OBookingDirections.Credit;

            _accountingRuleManager.UpdateAccountingRule(rule);
            ContractAccountingRule retrievedRule = _accountingRuleManager.Select(rule.Id) as ContractAccountingRule;
            _compareRules(rule, retrievedRule);
        }

        [Test]
        public void DeleteRule()
        {
            Account genericAccount = _accountManager.Select(1);
            Account specificAccount = _accountManager.Select(2);
            EventType eventType = _eventManager.SelectEventTypeByEventType("RGLE");
            EventAttribute eventAttribute = _eventManager.SelectEventAttributeByCode("principal");

            ContractAccountingRule rule = new ContractAccountingRule
            {
                DebitAccount = genericAccount,
                CreditAccount = specificAccount,
                ProductType = OProductTypes.Loan,
                ClientType = OClientTypes.All,
                BookingDirection = OBookingDirections.Both,
                EventAttribute = eventAttribute,
                EventType = eventType
            };

            rule.Id = _accountingRuleManager.AddAccountingRule(rule);
            Assert.AreNotEqual(0, rule.Id);

            _accountingRuleManager.DeleteAccountingRule(rule);
            Assert.AreEqual(null, _accountingRuleManager.Select(rule.Id));
        }

        [Test]
        public void SelectAll()
        {
            Account genericAccount = _accountManager.Select(1);
            Account specificAccount = _accountManager.Select(2);

            EventType eventType = _eventManager.SelectEventTypeByEventType("RGLE");
            EventAttribute eventAttribute = _eventManager.SelectEventAttributeByCode("principal");

            #region adding rule 1
            ContractAccountingRule ruleOne = new ContractAccountingRule
            {
                DebitAccount = genericAccount,
                CreditAccount = specificAccount,
                ProductType = OProductTypes.All,
                ClientType = OClientTypes.All,
                BookingDirection = OBookingDirections.Both,
                EventType = eventType,
                EventAttribute = eventAttribute
            };
            ruleOne.Id = _accountingRuleManager.AddAccountingRule(ruleOne);
            Assert.AreNotEqual(0, ruleOne.Id);
            #endregion

            #region adding rule 2
            LoanProduct loanProduct = _loanProductManager.Select(1);
            ContractAccountingRule ruleTwo = new ContractAccountingRule
            {
                DebitAccount = genericAccount,
                CreditAccount = specificAccount,
                ProductType = OProductTypes.Loan,
                LoanProduct = loanProduct,
                ClientType = OClientTypes.Corporate,
                BookingDirection = OBookingDirections.Credit,
                EventType = eventType,
                EventAttribute = eventAttribute
            };
            ruleTwo.Id = _accountingRuleManager.AddAccountingRule(ruleTwo);
            Assert.AreNotEqual(0, ruleTwo.Id);

            #endregion
 
            #region adding rule 3
            ContractAccountingRule ruleThree = new ContractAccountingRule
                {
                    DebitAccount = genericAccount,
                    CreditAccount = specificAccount,
                    //ProductType = OProductTypes.Guarantee,
                    ClientType = OClientTypes.Group,
                    BookingDirection = OBookingDirections.Debit,
                    EventType = eventType,
                    EventAttribute = eventAttribute
                };

            ruleThree.Id = _accountingRuleManager.AddAccountingRule(ruleThree);
            Assert.AreNotEqual(0, ruleThree.Id); 
            #endregion

            #region adding rule 4
            ISavingProduct savingsProduct = _savingProductManager.SelectSavingProduct(1);
            ContractAccountingRule ruleFour = new ContractAccountingRule
            {
                DebitAccount = genericAccount,
                CreditAccount = specificAccount,
                ProductType = OProductTypes.Saving,
                SavingProduct = savingsProduct,
                ClientType = OClientTypes.Person,
                BookingDirection = OBookingDirections.Both,
                EventType = eventType,
                EventAttribute = eventAttribute
            };
            ruleFour.Id = _accountingRuleManager.AddAccountingRule(ruleFour);
            Assert.AreNotEqual(0, ruleFour.Id); 
            #endregion

            #region adding rule 5
            EconomicActivity economicActivty = _economicActivityManager.SelectEconomicActivity(1);
            ContractAccountingRule ruleFive = new ContractAccountingRule
            {
                DebitAccount = genericAccount,
                CreditAccount = specificAccount,
                ProductType = OProductTypes.All,
                ClientType = OClientTypes.Village,
                EconomicActivity = economicActivty,
                BookingDirection = OBookingDirections.Credit,
                EventType = eventType,
                EventAttribute = eventAttribute
            };

            ruleFive.Id = _accountingRuleManager.AddAccountingRule(ruleFive);
            Assert.AreNotEqual(0, ruleFive.Id); 
            #endregion

            AccountingRuleCollection rules = _accountingRuleManager.SelectAll();
            Assert.AreEqual(5, rules.Count);

            _compareRules(ruleOne, rules[0] as ContractAccountingRule);
            _compareRules(ruleTwo, rules[1] as ContractAccountingRule);
            _compareRules(ruleThree, rules[2] as ContractAccountingRule);
            _compareRules(ruleFour, rules[3] as ContractAccountingRule);
            _compareRules(ruleFive, rules[4]as ContractAccountingRule);
        }
    }
}
