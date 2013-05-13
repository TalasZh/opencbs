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
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Contract;
using OpenCBS.Enums;
using NUnit.Framework;
using OpenCBS.CoreDomain.Events;
using OpenCBS.Shared;
using System.Collections;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Test.CoreDomain
{
    /// <summary>
    /// Summary description for TestCashReceipt.
    /// </summary>
    [TestFixture]
    public class TestCashReceipt
    {
        private Credit contractForAPerson;
        private Credit contractForAGroup;
        private Person _person;
        private Group _group;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            User.CurrentUser = new User();
            User.CurrentUser.Id = 1;

            GeneralSettings dataParam = GeneralSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            ChartOfAccounts chartOfAccounts = ChartOfAccounts.GetInstance(User.CurrentUser);
            chartOfAccounts.Accounts = chartOfAccounts.DefaultAccounts;

            ProvisioningTable provisioningTable = ProvisioningTable.GetInstance(User.CurrentUser);
            provisioningTable.AddProvisioningRate(new ProvisioningRate(1, 0, 0, 2));
        }

        [SetUp]
        public void SetUp()
        {

            Package package = new Package(new InstallmentType(1, "Monthly", 0, 1), OLoanTypes.Flat, true);
            package.KeepExpectedInstallment = true;
            package.NonRepaymentPenalties.InitialAmount = 0.003;

            contractForAPerson = new Credit(package, 1000, 0.03, 6, 1, new DateTime(2006, 1, 1), User.CurrentUser, GeneralSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisioningTable.GetInstance(User.CurrentUser));
            contractForAPerson.NonRepaymentPenalties.InitialAmount = 0.003;
            
            _person = new Person();
            _person.FirstName = "Nicolas";
            _person.LastName = "MANGIN";
            _person.District = new District(1, "Ile de France", new Province(1, "France"));
            _person.City = "Paris";

            User user = User.CurrentUser;
            user.FirstName = "Nicolas";
            user.LastName = "MANGIN";
            contractForAPerson.LoanOfficer = user;

            contractForAGroup = new Credit(package, 1000, 0.03, 6, 1, new DateTime(2006, 1, 1), User.CurrentUser, GeneralSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisioningTable.GetInstance(User.CurrentUser));
            _group = new Group();
            _group.Name = "Groupe MANGIN";
            _group.District = new District(1, "Lorraine", new Province(1, "France"));
            _group.City = "Nancy";
            contractForAGroup.EntryFees = 0.03;
        }

        [Test]
        public void TestIfUserIdCorrectlySaveAndRetrieved()
        {
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson,_person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1),
                false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual(1, cashReceipt.UserId);
        }

        [Test]
        public void TestIfContractCodeCorrectlyRetrived()
        {
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1),
                false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual(contractForAPerson.Code, cashReceipt.ContractCode);
        }

        [Test]
        public void TestIfDateCorrectlyRetrieved()
        {
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1),
                false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual(new DateTime(2006, 2, 1), cashReceipt.PaidDate);
        }

        [Test]
        public void TestIfNameCorrectlyRetrievedWhenContractBeneficiaryIsAPerson()
        {
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1),
                false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual("Nicolas MANGIN", cashReceipt.Name);
        }

        [Test]
        public void TestIfNameCorrectlyRetrievedWhenContractBeneficiaryIsAGroup()
        {
            CashReceipt cashReceipt = new CashReceipt(contractForAGroup, _group, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1),
                false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual("Groupe MANGIN", cashReceipt.Name);
        }

        [Test]
        public void TestIfCityCorrectlyRetrievedWhenContractBeneficiaryIsAPerson()
        {
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1),
                false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual("Paris", cashReceipt.City);
        }

        [Test]
        public void TestIfCityCorrectlyRetrievedWhenContractBeneficiaryIsAGroup()
        {
            CashReceipt cashReceipt = new CashReceipt(contractForAGroup, _group, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false,
                User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual("Nancy", cashReceipt.City);
        }

        [Test]
        public void TestIfDistrictCorrectlyRetrievedWhenContractBeneficiaryIsAPerson()
        {
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false,
                User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual("Ile de France", cashReceipt.District);
        }

        [Test]
        public void TestIfDistrictCorrectlyRetrievedWhenContractBeneficiaryIsAGroup()
        {
            CashReceipt cashReceipt = new CashReceipt(contractForAGroup, _group, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false,
                User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual("Lorraine", cashReceipt.District);
        }

        [Test]
        public void TestIfLoanOfficerNameCorrectlyRetrieved()
        {
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false,
                User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual("Nicolas MANGIN", cashReceipt.LoanOfficerName_Contract);
        }

        [Test]
        public void TestIfInstallmentCorrectlyRetrieved()
        {
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 2, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false,
                User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual(3, cashReceipt.Installment.Number);
        }

        [Test]
        public void TestIfCashReceiptInCorrectlySaveAndRetrievedWhenNoCashReceiptIn()
        {
            contractForAPerson.Disbursed = false;
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, null, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.IsTrue(!cashReceipt.CashReceiptIn.HasValue);
        }

        [Test]
        public void TestIfCashReceiptInCorrectlySaveAndRetrieved()
        {
            contractForAPerson.Disbursed = true;
            _person.CashReceiptIn = 1;
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual(2, cashReceipt.CashReceiptIn.Value);
            Assert.IsTrue(!cashReceipt.CashReceiptOut.HasValue);
        }

        [Test]
        public void CashReceipt_CorrectlySaveAndRetrieved_LoanAmount()
        {
            contractForAPerson.Disbursed = true;
            _person.CashReceiptIn = 1;
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual(1000, cashReceipt.LoanAmount);
        }

        [Test]
        public void CashReceipt_CorrectlySaveAndRetrieved_DisbursmentDate()
        {
            contractForAPerson.Disbursed = true;
            _person.CashReceiptIn = 1;
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual(new DateTime(2006, 1, 1), cashReceipt.DisbursmentDate);
        }

        [Test]
        public void CashReceipt_CorrectlySaveAndRetrieved_Maturity()
        {
            contractForAPerson.Disbursed = true;
            _person.CashReceiptIn = 1;
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual(6, cashReceipt.Maturity);
        }

        [Test]
        public void CashReceipt_CorrectlySaveAndRetrieved_GracePeriod()
        {
            contractForAPerson.Disbursed = true;
            _person.CashReceiptIn = 1;
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual(1, cashReceipt.GracePeriod);
        }

        [Test]
        public void TestIfCashReceiptOutCorrectlySaveAndRetrievedWhenNoCashReceiptOut()
        {
            contractForAPerson.Disbursed = false;
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, null, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual(1, cashReceipt.CashReceiptOut.Value);
        }

        [Test]
        public void TestIfInterestPrincipalAndFeesCorrectlyRetrievedWhenDisburseAndNoFees()
        {
            Assert.AreEqual(new DateTime(2006, 1, 1), contractForAPerson.StartDate);
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, null, "USD", "en-US", new ExchangeRate(new DateTime(2006, 1, 1), 3), new DateTime(2006, 1, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            //Negative value for principal if event is a loanDisbursmentEvent 
            Assert.AreEqual(-3000, cashReceipt.PaidPrincipalInExternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidInterestInExternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidFeesInExternalCurrency);
        }

        [Test]
        public void TestIfInterestPrincipalAndFeesCorrectlyRetrievedWhenDisburseAndNoFeesButNoExchangeRate()
        {
            Assert.AreEqual(new DateTime(2006, 1, 1), contractForAPerson.StartDate);
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, null, "USD", "en-US", null, new DateTime(2006, 1, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);

            //Negative value for principal if event is a loanDisbursmentEvent but ExternalCurrencyIs null
            Assert.AreEqual(-1000, cashReceipt.PaidPrincipalInInternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidInterestInInternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidFeesInInternalCurrency);

            Assert.AreEqual(-1, cashReceipt.PaidPrincipalInExternalCurrency);
            Assert.AreEqual(-1, cashReceipt.PaidInterestInExternalCurrency);
            Assert.AreEqual(-1, cashReceipt.PaidFeesInExternalCurrency);
        }

        [Test]
        public void TestIfInterestPrincipalAndFeesCorrectlyRetrievedWhenDisburseAndFees()
        {
            Assert.AreEqual(new DateTime(2006, 1, 1), contractForAGroup.StartDate);
            CashReceipt cashReceipt = new CashReceipt(contractForAGroup, _person, null, "USD", "en-US", new ExchangeRate(new DateTime(2006, 1, 1), 3), new DateTime(2006, 1, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            //Negative value for principal if event is a loanDisbursmentEvent 
            Assert.AreEqual(-3000, cashReceipt.PaidPrincipalInExternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidInterestInExternalCurrency);
            Assert.AreEqual(90, cashReceipt.PaidFeesInExternalCurrency);
        }

        [Test]
        public void TestIfInterestPrincipalAndFeesCorrectlyRetrievedWhenRepayAndNoFees()
        {
            Credit contract = contractForAPerson.Copy();
            contract.Disburse(new DateTime(2006, 1, 1), true, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), contract.GetInstallment(0).ExpectedDate);
            CashReceipt cashReceipt = new CashReceipt(contract, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 2, 1), 3), new DateTime(2006, 2, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);

            Assert.AreEqual(0, cashReceipt.PaidPrincipalInExternalCurrency);
            Assert.AreEqual(90, cashReceipt.PaidInterestInExternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidFeesInExternalCurrency);
        }

        [Test]
        public void TestIfInterestPrincipalAndFeesCorrectlyRetrievedWhenRepayAndFees()
        {
            Credit contract = contractForAPerson.Copy();

            contract.Disburse(new DateTime(2006, 1, 1), true, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), contract.GetInstallment(0).ExpectedDate);
            CashReceipt cashReceipt = new CashReceipt(contract, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 3, 1), 3), new DateTime(2006, 3, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);

            Assert.AreEqual(0, cashReceipt.PaidPrincipalInExternalCurrency);
            Assert.AreEqual(90, cashReceipt.PaidInterestInExternalCurrency);
            Assert.AreEqual(252.00m, Math.Round(cashReceipt.PaidFeesInExternalCurrency, 2));
        }

        [Test]
        public void TestIfLocalAccountNumberCorrectlyRetrievedWhenDisburse()
        {
            ChartOfAccounts chartOfAccounts = ChartOfAccounts.GetInstance(User.CurrentUser);

            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, null, "USD", "en-US", null, new DateTime(2006, 1, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            Assert.AreEqual(String.Empty, cashReceipt.InterestLocalAccountNumber);
            Assert.AreEqual(chartOfAccounts.GetAccountByTypeCode("CASH").LocalNumber, cashReceipt.PrincipalLocalAccountNumber);
            Assert.AreEqual(chartOfAccounts.GetAccountByTypeCode("COMMISSIONS").LocalNumber, cashReceipt.FeesLocalAccountNumber);
        }

        [Test]
        public void TestIfLocalAccountNumberCorrectlyRetrievedWhenRepayABadLoan()
        {
            ChartOfAccounts chartOfAccounts = ChartOfAccounts.GetInstance(User.CurrentUser);

            Credit contract = contractForAPerson.Copy();
            contract.Disburse(new DateTime(2006, 1, 1), true, false);
            contract.BadLoan = true;

            CashReceipt cashReceipt = new CashReceipt(contract, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 3, 1), 3), new DateTime(2006, 3, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);

            Assert.AreEqual(chartOfAccounts.GetAccountByTypeCode("IPOPDL").LocalNumber, cashReceipt.InterestLocalAccountNumber);
            Assert.AreEqual(chartOfAccounts.GetAccountByTypeCode("BAD_LOANS").LocalNumber, cashReceipt.PrincipalLocalAccountNumber);
            Assert.AreEqual(chartOfAccounts.GetAccountByTypeCode("IPOPDL").LocalNumber, cashReceipt.FeesLocalAccountNumber);
        }

        [Test]
        public void TestIfLocalAccountNumberCorrectlyRetrievedWhenRepayARescheduledLoan()
        {
            ChartOfAccounts chartOfAccounts = ChartOfAccounts.GetInstance(User.CurrentUser);

            Credit contract = contractForAPerson.Copy();
            contract.Disburse(new DateTime(2006, 1, 1), true, false);
            contract.Rescheduled = true;

            CashReceipt cashReceipt = new CashReceipt(contract, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 3, 1), 3), new DateTime(2006, 3, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);

            Assert.AreEqual(chartOfAccounts.GetAccountByTypeCode("INTERESTS_ON_RESCHEDULED_LOANS").LocalNumber, cashReceipt.InterestLocalAccountNumber);
            Assert.AreEqual(chartOfAccounts.GetAccountByTypeCode("RESCHEDULED_LOANS").LocalNumber, cashReceipt.PrincipalLocalAccountNumber);
            Assert.AreEqual(chartOfAccounts.GetAccountByTypeCode("COMMISSIONS").LocalNumber, cashReceipt.FeesLocalAccountNumber);
        }

        [Test]
        public void TestIfLocalAccountNumberCorrectlyRetrievedWhenRepayAGoodLoan()
        {

            ChartOfAccounts chartOfAccounts = ChartOfAccounts.GetInstance(User.CurrentUser);

            Credit contract = contractForAPerson.Copy();
            contract.Disburse(new DateTime(2006, 1, 1), true, false);
            CashReceipt cashReceipt = new CashReceipt(contract, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 3, 1), 3), new DateTime(2006, 3, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);

            Assert.AreEqual(chartOfAccounts.GetAccountByNumber(OAccounts.INTERESTS_ON_CASH_CREDIT_INDIVIDUAL_LOAN).LocalNumber, cashReceipt.InterestLocalAccountNumber);
            Assert.AreEqual(chartOfAccounts.GetAccountByNumber(OAccounts.CASH_CREDIT_INDIVIDUAL_LOAN).LocalNumber, cashReceipt.PrincipalLocalAccountNumber);
            Assert.AreEqual(chartOfAccounts.GetAccountByTypeCode("COMMISSIONS").LocalNumber, cashReceipt.FeesLocalAccountNumber);
        }

        [Test]
        public void TestIfInterestPrincipalAndFeesInLetterCorrectlyRetrievedWhenDisburseAndNoFeesWhenCurrencyIsUSDAndLanguageIsEnglish()
        {
            Assert.AreEqual(new DateTime(2006, 1, 1), contractForAPerson.StartDate);
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, null, "USD", "en-US", new ExchangeRate(new DateTime(2006, 1, 1), 3), new DateTime(2006, 1, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            //Negative value for principal if event is a loanDisbursmentEvent 

            Assert.AreEqual(-3000, cashReceipt.PaidPrincipalInExternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidInterestInExternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidFeesInExternalCurrency);
            Assert.AreEqual("zero USD", cashReceipt.PaidInterestInLetter);
            Assert.AreEqual("zero USD", cashReceipt.PaidFeesInLetter);
            Assert.AreEqual("three thousand USD", cashReceipt.PaidPrincipalInLetter);
        }

        [Test]
        public void TestIfInterestPrincipalAndFeesInLetterCorrectlyRetrievedWhenDisburseAndNoFeesWhenCurrencyIsSOMAndLanguageIsEnglish()
        {
            Assert.AreEqual(new DateTime(2006, 1, 1), contractForAPerson.StartDate);
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, null, "SOM", "en-US", new ExchangeRate(new DateTime(2006, 1, 1), 3), new DateTime(2006, 1, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            //Negative value for principal if event is a loanDisbursmentEvent 

            Assert.AreEqual(-3000, cashReceipt.PaidPrincipalInExternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidInterestInExternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidFeesInExternalCurrency);
            Assert.AreEqual("zero SOM", cashReceipt.PaidInterestInLetter);
            Assert.AreEqual("zero SOM", cashReceipt.PaidFeesInLetter);
            Assert.AreEqual("three thousand SOM", cashReceipt.PaidPrincipalInLetter);
        }

        [Test]
        public void TestIfInterestPrincipalAndFeesInLetterCorrectlyRetrievedWhenDisburseAndNoFeesWhenCurrencyIsSOMAndLanguageIsRussian()
        {
            Assert.AreEqual(new DateTime(2006, 1, 1), contractForAPerson.StartDate);
            CashReceipt cashReceipt = new CashReceipt(contractForAPerson, _person, null, "SOM", "ru-RU", new ExchangeRate(new DateTime(2006, 1, 1), 3), new DateTime(2006, 1, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);
            //Negative value for principal if event is a loanDisbursmentEvent 

            Assert.AreEqual(-3000, cashReceipt.PaidPrincipalInExternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidInterestInExternalCurrency);
            Assert.AreEqual(0, cashReceipt.PaidFeesInExternalCurrency);
            Assert.AreEqual("ноль SOM", cashReceipt.PaidInterestInLetter);
            Assert.AreEqual("ноль SOM", cashReceipt.PaidFeesInLetter);
            Assert.AreEqual("три тысячи SOM", cashReceipt.PaidPrincipalInLetter);
        }

        [Test]
        public void TestIfInterestPrincipalAndFeesInLetterCorrectlyRetrievedWhenRepayAndFeesWhenCurrencyIsUSDAndLanguageIsEnglish()
        {
            Credit contract = contractForAPerson.Copy();
            contract.Disburse(new DateTime(2006, 1, 1), true, false);

            CashReceipt cashReceipt = new CashReceipt(contract, _person, 0, "USD", "en-US", new ExchangeRate(new DateTime(2006, 3, 1), 3), new DateTime(2006, 3, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);

            Assert.AreEqual(0, cashReceipt.PaidPrincipalInExternalCurrency);
            Assert.AreEqual(90, cashReceipt.PaidInterestInExternalCurrency);
            Assert.AreEqual(252.00m, Math.Round(cashReceipt.PaidFeesInExternalCurrency, 2));
            Assert.AreEqual("ninety USD", cashReceipt.PaidInterestInLetter);
            Assert.AreEqual("two hundred and fifty-two USD", cashReceipt.PaidFeesInLetter);
            Assert.AreEqual("zero USD", cashReceipt.PaidPrincipalInLetter);
        }

        [Test]
        public void TestIfInterestPrincipalAndFeesInLetterCorrectlyRetrievedWhenRepayAndFeesWhenCurrencyIsUSDAndLanguageIsRussian()
        {
            Credit contract = contractForAPerson.Copy();
            contract.Disburse(new DateTime(2006, 1, 1), true, false);

            CashReceipt cashReceipt = new CashReceipt(contract, _person, 0, "USD", "ru-RU", new ExchangeRate(new DateTime(2006, 3, 1), 3), new DateTime(2006, 3, 1), false, User.CurrentUser, GeneralSettings.GetInstance(""), ChartOfAccounts.GetInstance(User.CurrentUser), true);

            Assert.AreEqual(0, cashReceipt.PaidPrincipalInExternalCurrency);
            Assert.AreEqual(90, cashReceipt.PaidInterestInExternalCurrency);
            Assert.AreEqual(252.00m, Math.Round(cashReceipt.PaidFeesInExternalCurrency, 2));
            Assert.AreEqual("девяносто USD", cashReceipt.PaidInterestInLetter);
            Assert.AreEqual("двести пятьдесят два USD", cashReceipt.PaidFeesInLetter);
            Assert.AreEqual("ноль USD", cashReceipt.PaidPrincipalInLetter);
        }
    }
}
