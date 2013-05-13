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
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contract;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.CoreDomain.Products;
using OpenCBS.CoreDomain.SearchResult;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Events;
using OpenCBS.DatabaseConnection;
using OpenCBS.Manager;
using NUnit.Framework;
using System.Collections;
using OpenCBS.Manager.Clients;
using OpenCBS.Manager.Products;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.Manager
{
    /// <summary>
    /// Class containing tests ensuring the validity of ContractManagement methods.
    /// </summary>

    [TestFixture]
    public class TestcreditManagement
    {
        private CreditContractManagement creditManagement;
        private InstallmentManager installmentManagement;
        private ClientManager clientManagement;
        private ConnectionManager connectionManager;
        private ProjectManager projectManager;
        private FundingLineManager fundingLineManager;
        private UserManager userManager;
        private readonly DataHelper addDataForTesting = new DataHelper();
        private LoanProductManager _packageManager;
        private LoanProduct _package;

        private readonly Group tiers = new Group();
        private readonly Guarantor guarantor = new Guarantor();
        private readonly LoanProduct package = new LoanProduct
        {
            Currency = new Currency { Id = 1 }
        };
        private readonly User user = new User();

        private Installment installment1;
        private Installment installment2;
        private InstallmentType biWeekly;
        private Loan credit;
        private Collateral collateral;
        private Project _project;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            ProvisioningTable.GetInstance(new User()).Add(new ProvisioningRate { Number = 1, NbOfDaysMin = 0, NbOfDaysMax = 1000, Rate = 0 });
            ApplicationSettings.GetInstance("").DeleteAllParameters();
            ApplicationSettings.GetInstance("").AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            ApplicationSettings.GetInstance("").AddParameter(OGeneralSettings.USECENTS, true);

            ApplicationSettings.GetInstance("").AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
            ApplicationSettings.GetInstance("").AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            ApplicationSettings.GetInstance("").AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);

            TechnicalSettings.UseOnlineMode = false;

            projectManager = new ProjectManager(DataUtil.TESTDB);
            creditManagement = new CreditContractManagement(DataUtil.TESTDB);
            installmentManagement = new InstallmentManager(DataUtil.TESTDB);
            clientManagement = new ClientManager(DataUtil.TESTDB);
            connectionManager = ConnectionManager.GetInstance(DataUtil.TESTDB);
            fundingLineManager = new FundingLineManager(DataUtil.TESTDB);
            userManager = new UserManager(DataUtil.TESTDB);
            _packageManager = new LoanProductManager(DataUtil.TESTDB);


            installment1 = new Installment();
            installment1.Number = 1;
            installment1.CapitalRepayment = 200;
            installment1.InterestsRepayment = 100;
            installment1.PaidCapital = 200;
            installment1.PaidInterests = 100;
            //to initialize Installment.FeesUnpaid, ocurrency is an object, must be initialized
            installment1.FeesUnpaid = 0;
            installment1.ExpectedDate = DateTime.Today.AddDays(-1);

            installment2 = new Installment();
            installment2.Number = 2;
            installment2.CapitalRepayment = 200;
            installment2.InterestsRepayment = 100;
            installment2.PaidCapital = 0;
            installment2.PaidInterests = 100;
            //to initialize Installment.FeesUnpaid, ocurrency is an object, must be initialized
            installment2.FeesUnpaid = 0;
            installment2.ExpectedDate = DateTime.Today.AddMonths(1);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            addDataForTesting.DeleteInstallments();
            addDataForTesting.DeleteCreditContract();
            addDataForTesting.DeletedProject();
            addDataForTesting.DeleteInstallmentTypes();
            addDataForTesting.DeleteTiers();
            addDataForTesting.DeletePackage();
        }

        [SetUp]
        public void SetUp()
        {
            addDataForTesting.DeleteInstallments();
            addDataForTesting.DeleteCreditContract();
            addDataForTesting.DeletedProject();
            addDataForTesting.DeleteInstallmentTypes();
            addDataForTesting.DeleteTiers();
            addDataForTesting.DeleteAllUser();
            addDataForTesting.AddGenericFundingLine();
            collateral = addDataForTesting.AddCollateral();

            biWeekly = addDataForTesting.AddBiWeeklyInstallmentType();
            package.Id = addDataForTesting.AddGenericPackage();
            package.Name = "Package";
            user.Id = addDataForTesting.AddUserWithIntermediaryAttributs();
            //addDataForTesting.AddGenericFundingLine();
            tiers.Id = addDataForTesting.AddGenericTiersIntoDatabase(OClientTypes.Group);
            tiers.LoanCycle = 1;

            _project = new Project();
            _project.ProjectStatus = OProjectStatus.Refused;
            _project.Name = "NotSet";
            _project.Code = "NotSet";
            _project.Aim = "NotSet";

            _project.BeginDate = TimeProvider.Today;
            _project.Id = projectManager.Add(_project, tiers.Id, null);


            //fundingLine = new FundingLine("AFD130",false);

            credit = new Loan(new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisioningTable.GetInstance(new User()));
            credit.LoanOfficer = user;

            credit.BranchCode = "DU";
            credit.CreationDate = DateTime.Today.AddDays(-1);
            credit.StartDate = DateTime.Today;
            credit.CloseDate = DateTime.Today.AddDays(1);
            credit.Closed = true;
            credit.Rural = true;
            credit.Product = package;
            credit.Amount = 1000m;
            credit.InterestRate = 3;
            credit.InstallmentType = biWeekly;
            credit.NbOfInstallments = 2;
            credit.NonRepaymentPenalties.InitialAmount = 2.5;
            credit.AnticipatedRepaymentPenalties = 1.2;
            credit.Disbursed = false;
            credit.LoanOfficer = user;
            credit.FundingLine = new FundingLine { Name = "AFD130" };
            credit.EntryFees = 2.05;
            credit.WriteOff = false;
            credit.Rescheduled = false;
            credit.BadLoan = false;

            credit.AddInstallment(installment1);
            credit.AddInstallment(installment2);
        }

        private void SetNullAuthorizedValues()
        {
            credit.GracePeriod = 1;
            //credit.CollateralAmount = 2050.54m;
        }

        private static void _CompareCreditContract(Loan pContractA, Loan pContractB)
        {
            Assert.AreEqual(pContractB.Id, pContractA.Id);
            if (pContractB.Amount.HasValue)
                Assert.AreEqual(pContractB.Amount.Value, pContractA.Amount.Value);
            Assert.AreEqual(pContractB.Disbursed, pContractA.Disbursed);
            Assert.AreEqual(pContractB.InterestRate, pContractA.InterestRate);
            Assert.AreEqual(pContractB.NonRepaymentPenalties.InitialAmount, pContractA.NonRepaymentPenalties.InitialAmount);
            Assert.AreEqual(pContractB.NonRepaymentPenalties.OLB, pContractA.NonRepaymentPenalties.OLB);
            Assert.AreEqual(pContractB.NonRepaymentPenalties.OverDueInterest, pContractA.NonRepaymentPenalties.OverDueInterest);
            Assert.AreEqual(pContractB.NonRepaymentPenalties.OverDuePrincipal, pContractA.NonRepaymentPenalties.OverDuePrincipal);
            Assert.AreEqual(pContractB.AnticipatedRepaymentPenalties, pContractA.AnticipatedRepaymentPenalties);
            Assert.AreEqual(pContractB.EntryFees, pContractA.EntryFees);
            Assert.AreEqual(pContractB.GracePeriod, pContractA.GracePeriod);
            Assert.AreEqual(pContractB.NbOfInstallments, pContractA.NbOfInstallments);
            Assert.AreEqual(pContractB.FundingLine.Id, pContractA.FundingLine.Id);
            Assert.AreEqual(pContractB.LoanOfficer.Id, pContractA.LoanOfficer.Id);

        }

        [Test]
        public void TestAddLoanInDatabaseWithCreditCommitteeCode()
        {
            FundingLine fund = new FundingLine
            {
                Purpose = "Microsoft financement",
                Name = "AFD130",
                Deleted = false,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Currency = new Currency { Id = 1 }
            };

            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.CreditCommitteeCode = "Code";

            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);

            Assert.AreEqual("Code", contractRetrieved.CreditCommitteeCode);
        }

        [Test]
        public void TestAddCreditContractAndSelectByIdNonNullAuthorizedValues()
        {
            Assert.AreEqual(2, credit.InstallmentList.Count);
            FundingLine fund = new FundingLine
                                   {
                                       Purpose = "Microsoft financement",
                                       Name = "AFD130",
                                       Deleted = false,
                                       StartDate = DateTime.Now,
                                       EndDate = DateTime.Now,
                                       Currency = new Currency { Id = 1 }
                                   };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;

            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);

            Assert.AreEqual(OClientTypes.Group, contractRetrieved.ClientType);
            Assert.AreEqual("DU", contractRetrieved.BranchCode);
            Assert.AreEqual(DateTime.Today.AddDays(-1), contractRetrieved.CreationDate);
            Assert.AreEqual(DateTime.Today, contractRetrieved.StartDate);
            Assert.AreEqual(DateTime.Today.AddDays(1), contractRetrieved.CloseDate);
            Assert.IsTrue(contractRetrieved.Rural);
            Assert.AreEqual(package.Id, contractRetrieved.Product.Id);
            Assert.AreEqual(package.Name, contractRetrieved.Product.Name);
            Assert.AreEqual(1000m, contractRetrieved.Amount.Value);
            Assert.AreEqual(3, contractRetrieved.InterestRate);
            Assert.AreEqual(biWeekly.Id, contractRetrieved.InstallmentType.Id);
            Assert.AreEqual(biWeekly.Name, contractRetrieved.InstallmentType.Name);
            Assert.AreEqual(2, contractRetrieved.NbOfInstallments);
            Assert.AreEqual(2.5, contractRetrieved.NonRepaymentPenalties.InitialAmount);
            Assert.AreEqual(1.2, contractRetrieved.AnticipatedRepaymentPenalties);
            Assert.IsFalse(contractRetrieved.Disbursed);
            Assert.AreEqual(user.Id, contractRetrieved.LoanOfficer.Id);
            Assert.AreEqual("mariam", contractRetrieved.LoanOfficer.UserName);
            Assert.AreEqual("AFD130", contractRetrieved.FundingLine.Name);
            Assert.AreEqual(2.05, contractRetrieved.EntryFees);
            Assert.IsFalse(contractRetrieved.WriteOff);
            Assert.IsFalse(contractRetrieved.Rescheduled);
            Assert.IsFalse(contractRetrieved.BadLoan);
            Assert.IsTrue(contractRetrieved.Closed);
            Assert.AreEqual(2, contractRetrieved.InstallmentList.Count);
            Assert.AreEqual(installment1.Number, contractRetrieved.GetInstallment(0).Number);
            Assert.AreEqual(installment1.PaidInterests.Value, contractRetrieved.GetInstallment(0).PaidInterests.Value);
        }

        [Test]
        public void TestAddCreditContractAndSelectByIdNullAuthorizedValues()
        {

            var fund = new FundingLine
                                   {
                                       Purpose = "Microsoft financement",
                                       Name = "AFD130",
                                       Deleted = false,
                                       StartDate = DateTime.Now,
                                       EndDate = DateTime.Now,
                                       Currency = new Currency { Id = 1 }
                                   };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            this.SetNullAuthorizedValues();
            collateral.Amount = 100.0000m;
            credit.AddCollateral(collateral);

            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);

            Assert.AreEqual(1, contractRetrieved.GracePeriod.Value);
            Assert.AreEqual(collateral.Name, contractRetrieved.Collaterals[0].Name);
            Assert.AreEqual(collateral.Amount.Value, contractRetrieved.Collaterals[0].Amount.Value);
        }

        [Test]
        public void AddCredit_SelectById_ContractChartOfAccounts()
        {
            FundingLine fund = new FundingLine
                                   {
                                       Purpose = "Microsoft financement",
                                       Name = "AFD130",
                                       Deleted = false,
                                       StartDate = DateTime.Now,
                                       EndDate = DateTime.Now,
                                       Currency = new Currency { Id = 1 }
                                   };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.ChartOfAccounts.Accounts[1].Balance = 3000;
            credit.ChartOfAccounts.Accounts[4].Balance = 1254;
            credit.ChartOfAccounts.Accounts[6].Balance = 4587;
            credit.ChartOfAccounts.Accounts[16].Balance = 32.54m;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);

            Assert.AreEqual(3000m, contractRetrieved.ChartOfAccounts.Accounts[1].Balance.Value);
            Assert.AreEqual(1254m, contractRetrieved.ChartOfAccounts.Accounts[4].Balance.Value);
            Assert.AreEqual(4587m, contractRetrieved.ChartOfAccounts.Accounts[6].Balance.Value);
            Assert.AreEqual(32.54m, contractRetrieved.ChartOfAccounts.Accounts[16].Balance.Value);
        }

        [Test]
        public void AddCredit_SelectByCode_ContractChartOfAccounts()
        {
            FundingLine fund = new FundingLine
                                   {
                                       Purpose = "Microsoft financement",
                                       Name = "AFD130",
                                       Deleted = false,
                                       StartDate = DateTime.Now,
                                       EndDate = DateTime.Now,
                                       Currency = new Currency { Id = 1 }
                                   };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Code = "TEST";
            credit.ChartOfAccounts.Accounts[1].Balance = 3000;
            credit.ChartOfAccounts.Accounts[4].Balance = 1254;
            credit.ChartOfAccounts.Accounts[6].Balance = 4587;
            credit.ChartOfAccounts.Accounts[16].Balance = 32.54m;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectContractByContractCode(credit.Code);

            Assert.AreEqual(3000m, contractRetrieved.ChartOfAccounts.Accounts[1].Balance.Value);
            Assert.AreEqual(1254m, contractRetrieved.ChartOfAccounts.Accounts[4].Balance.Value);
            Assert.AreEqual(4587m, contractRetrieved.ChartOfAccounts.Accounts[6].Balance.Value);
            Assert.AreEqual(32.54m, contractRetrieved.ChartOfAccounts.Accounts[16].Balance.Value);
        }

        [Test]
        public void TestDeleteContract()
        {
            FundingLine fund = new FundingLine
                                   {
                                       Purpose = "Microsoft financement",
                                       Name = "AFD130",
                                       Deleted = false,
                                       StartDate = DateTime.Now,
                                       EndDate = DateTime.Now,
                                       Currency = new Currency { Id = 1 }
                                   };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            guarantor.Tiers = tiers;
            guarantor.Amount = 1000m;
            credit.AddGuarantor(guarantor);

            credit.Events.Add(new LoanDisbursmentEvent { Date = new DateTime(2006, 1, 1), Amount = 100, Commission = 100, ClientType = credit.ClientType });

            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrievedBeforeDelete = creditManagement.SelectCreditById(credit.Id);

            Assert.AreEqual(credit.Id, contractRetrievedBeforeDelete.Id);
            creditManagement.DeleteContract(credit, null);
            Loan contractRetrievedAfterDelete = creditManagement.SelectCreditById(credit.Id);
            Assert.IsNull(contractRetrievedAfterDelete);
        }

        [Test]
        public void DeleteContract_ContractAccountsStock()
        {
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            guarantor.Tiers = tiers;
            guarantor.Amount = 1000m;
            credit.AddGuarantor(guarantor);
            credit.ChartOfAccounts.Accounts[3].Balance = 1000;

            credit.Events.Add(new LoanDisbursmentEvent { Date = new DateTime(2006, 1, 1), Amount = 100, Commission = 100, ClientType = credit.ClientType });

            Assert.AreEqual(28, credit.ChartOfAccounts.Accounts.Count);
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrievedBeforeDelete = creditManagement.SelectCreditById(credit.Id);

            Assert.AreEqual(28, contractRetrievedBeforeDelete.ChartOfAccounts.Accounts.Count);
            Assert.AreEqual(1000m, contractRetrievedBeforeDelete.ChartOfAccounts.Accounts[3].Balance.Value);
            Assert.AreEqual(credit.Id, contractRetrievedBeforeDelete.Id);

            creditManagement.DeleteContract(contractRetrievedBeforeDelete, null);
            Loan contractRetrievedAfterDelete = creditManagement.SelectCreditById(contractRetrievedBeforeDelete.Id);
            Assert.IsNull(contractRetrievedAfterDelete);
        }

        [Test]
        public void TestAddCreditContractWithGuarantorsAndSelectById()
        {
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            guarantor.Tiers = tiers;
            guarantor.Amount = 1000m;
            credit.AddGuarantor(guarantor);

            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);

            Assert.AreEqual(guarantor.Tiers.Id, contractRetrieved.GetGuarantor(0).Tiers.Id);
            Assert.AreEqual(guarantor.Amount.Value, contractRetrieved.GetGuarantor(0).Amount.Value);
        }

        [Test]
        public void TestAddCreditContractWithCollateralAndSelectById()
        {
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            collateral.Amount = 100.0000m;
            credit.AddCollateral(collateral);

            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);

            Assert.AreEqual(collateral.Amount.Value, contractRetrieved.Collaterals[0].Amount.Value);
        }

        [Test]
        public void TestSelectContractByIdWhenNoDataMatchesWith()
        {
            Assert.IsNull(creditManagement.SelectCreditById(-13));
        }

        [Test]
        public void TestUpdateContractToRescheduled()
        {
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.IsFalse(contractRetrieved.Rescheduled);

            creditManagement.UpdateContractToRescheduled(credit.Id, null);
            contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.IsTrue(contractRetrieved.Rescheduled);
        }

        [Test]
        public void TestUpdateContractToDisbursedAndUpdateStartDate()
        {
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.IsFalse(contractRetrieved.Disbursed);
            Assert.AreEqual(DateTime.Today, contractRetrieved.StartDate);

            contractRetrieved.StartDate = new DateTime(2006, 12, 6);
            contractRetrieved.Disbursed = true;

            creditManagement.UpdateContract(contractRetrieved, null);
            Loan contract = creditManagement.SelectCreditById(credit.Id);
            Assert.IsTrue(contract.Disbursed);
            Assert.AreEqual(new DateTime(2006, 12, 6), contract.StartDate);
        }

        [Test]
        public void TestUpdateContractToWrittenOff()
        {
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.IsFalse(contractRetrieved.WriteOff);

            creditManagement.UpdateContractToWriteOff(credit.Id, null);
            contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.IsTrue(contractRetrieved.WriteOff);
        }



        [Test]
        public void TestSelectAllContractsForClosureWhenNoResult()
        {
            ArrayList ids = creditManagement.SelectAllContractIdsForClosure();
            Assert.AreEqual(0, ids.Count);
        }

        [Test]
        public void TestSelectAllContractsForClosure()
        {
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Disbursed = true;
            credit.WriteOff = false;
            credit.Rescheduled = false;
            credit.BadLoan = false;
            creditManagement.AddCredit(credit, _project.Id, null);

            credit.Disbursed = false;
            credit.WriteOff = false;
            credit.Rescheduled = false;
            credit.BadLoan = false;
            creditManagement.AddCredit(credit, _project.Id, null);

            int i = creditManagement.SelectNumberOfContractsForClosure();
            Assert.AreEqual(1, i);
            ArrayList idsList = creditManagement.SelectAllContractIdsForClosure();
            Assert.AreEqual(1, idsList.Count);
            Loan contrat = creditManagement.SelectContractForClosure((int)idsList[0]);
            Assert.AreEqual(credit.Amount.Value, contrat.Amount.Value);
        }


        [Test]
        public void TestSearchContractInDatabaseWithContractCodeSet()
        {
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Code = "DU/08";
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            creditManagement.AddCredit(credit, _project.Id, null);
            Assert.AreEqual(1, creditManagement.SearchCreditInDatabase(1, "DU/08").Count);
        }

        [Test]
        public void TestSearchContractInDatabaseWithClientNameSetWhenClientIsGroup()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            List<CreditSearchResult> ds = creditManagement.SearchCreditInDatabase(1, "SC");
            Assert.AreEqual(1, ds.Count);
            Assert.AreEqual("SCG", ds[0].ClientName);
        }

        [Test]
        public void TestSearchContractInDatabaseWithClientNameSetWhenClientIsPerson()
        {

            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            addDataForTesting.DeleteInstallments();
            addDataForTesting.DeleteCreditContract();
            addDataForTesting.DeletedProject();
            addDataForTesting.DeleteInstallmentTypes();
            addDataForTesting.DeleteTiers();
            addDataForTesting.DeleteAllUser();

            biWeekly = addDataForTesting.AddBiWeeklyInstallmentType();
            package.Id = addDataForTesting.AddGenericPackage();
            user.Id = addDataForTesting.AddUserWithIntermediaryAttributs();
            addDataForTesting.AddGenericFundingLine();

            credit.InstallmentType = biWeekly;
            credit.Product = package;
            credit.LoanOfficer = user;

            Person person = new Person();
            person.Id = addDataForTesting.AddGenericTiersIntoDatabase(OClientTypes.Person);
            _project.Id = projectManager.Add(_project, person.Id, null);
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            List<CreditSearchResult> ds = creditManagement.SearchCreditInDatabase(1, "Nico");
            Assert.AreEqual(1, ds.Count);
            Assert.AreEqual("Nicolas BARON", ds[0].ClientName);
        }

        [Test]
        public void TestSearchContractInDatabaseWithStartDateSetAndLoanOfficerFilterNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            Assert.AreEqual(1, creditManagement.SearchCreditInDatabase(1, null).Count);
        }

        [Test]
        public void TestSearchContractInDatabaseWithStartDateSetAndLoanOfficerFilterActive()
        {

            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            User _user = new User();
            _user.Id = addDataForTesting.AddUserWithMaximumAttributs();
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            credit.LoanOfficer = _user;
            creditManagement.AddCredit(credit, _project.Id, null);

            Assert.AreEqual(2, creditManagement.SearchCreditInDatabase(1, null).Count);

            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, true);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            User.CurrentUser = new User();
            User.CurrentUser.Id = _user.Id;
            User.CurrentUser.Role = User.Roles.ADMIN;

            CreditContractManagement manager = new CreditContractManagement(DataUtil.TESTDB, User.CurrentUser);
            Assert.AreEqual(2, manager.SearchCreditInDatabase(1, null).Count);

            User.CurrentUser.Role = User.Roles.LOF;
            CreditContractManagement manager2 = new CreditContractManagement(DataUtil.TESTDB, User.CurrentUser);
            Assert.AreEqual(1, manager2.SearchCreditInDatabase(1, null).Count);
        }

        [Test]
        public void TestSearchContractInDatabaseWithStartDateSetAndLoanOfficerFilterActiveButCurrentUserIsAdmin()
        {

            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            User _user = new User();
            _user.Id = this.addDataForTesting.AddUserWithMaximumAttributs();
            _user.Role = User.Roles.ADMIN;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            credit.LoanOfficer = _user;
            creditManagement.AddCredit(credit, _project.Id, null);

            Assert.AreEqual(2, creditManagement.SearchCreditInDatabase(1, null).Count);

            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, true);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            User.CurrentUser = new User();
            User.CurrentUser.Id = _user.Id;
            User.CurrentUser.Role = User.Roles.ADMIN;


            CreditContractManagement manager = new CreditContractManagement(DataUtil.TESTDB, User.CurrentUser);
            Assert.AreEqual(2, manager.SearchCreditInDatabase(1, null).Count);
        }

        [Test]
        public void TestSearchContractInDatabaseWithCloseDateSet()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            Assert.AreEqual(1, creditManagement.SearchCreditInDatabase(1, null).Count);
        }

        [Test]
        public void TestGetNumberOfRecordsFoundForSearchCreditWhenLoanOfficerPortFolioNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            Assert.AreEqual(1, creditManagement.GetNumberOfRecordsFoundForSearchCreditContract("SC"));
        }

        [Test]
        public void TestGetNumberOfRecordsFoundForSearchCreditWhenNoRecordFoundAndLoanOfficerFilterNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            Assert.AreEqual(0, creditManagement.GetNumberOfRecordsFoundForSearchCreditContract("toto34"));
        }

        [Test]
        public void TestGetNumberOfRecordsFoundForSearchCreditWhenRecordFoundAndLoanOfficerFilterActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            User _user = new User();
            _user.Id = addDataForTesting.AddUserWithMaximumAttributs();
            _user.Role = User.Roles.LOF;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            credit.LoanOfficer = _user;
            creditManagement.AddCredit(credit, _project.Id, null);

            Assert.AreEqual(2, creditManagement.GetNumberOfRecordsFoundForSearchCreditContract("SC"));

            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, true);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            User.CurrentUser = new User();
            User.CurrentUser.Role = User.Roles.LOF;
            User.CurrentUser.Id = _user.Id;

            CreditContractManagement manager = new CreditContractManagement(DataUtil.TESTDB, User.CurrentUser);
            Assert.AreEqual(1, manager.GetNumberOfRecordsFoundForSearchCreditContract("SC"));
        }

        [Test]
        public void TestGetNumberOfRecordsFoundForSearchCreditWhenRecordFoundAndLoanOfficerFilterActiveButCurrentUserIsAdmin()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            User _user = new User();
            _user.Id = this.addDataForTesting.AddUserWithMaximumAttributs();
            _user.Role = User.Roles.ADMIN;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            credit.LoanOfficer = _user;
            creditManagement.AddCredit(credit, _project.Id, null);

            Assert.AreEqual(2, creditManagement.GetNumberOfRecordsFoundForSearchCreditContract("SC"));

            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, true);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            User.CurrentUser = new User();
            User.CurrentUser.Id = _user.Id;
            User.CurrentUser.Role = User.Roles.ADMIN;


            CreditContractManagement manager = new CreditContractManagement(DataUtil.TESTDB, User.CurrentUser);
            Assert.AreEqual(2, manager.GetNumberOfRecordsFoundForSearchCreditContract("SC"));
        }

        [Test]
        public void TestSearchContractInDatabaseWithUserIdSetAndUserLastNameAndFirstNameNotNull()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            UserManager userManager = new UserManager(DataUtil.TESTDB);
            User loanOfficer = new User();
            loanOfficer.UserName = "lo";
            loanOfficer.Password = "lo";
            loanOfficer.FirstName = "jean";
            loanOfficer.LastName = "paul";
            loanOfficer.Role = User.Roles.LOF;
            loanOfficer.Mail = "Not Set";
            loanOfficer.Id = userManager.AddUser(loanOfficer);
            credit.LoanOfficer = loanOfficer;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Code = "DURRR";
            creditManagement.AddCredit(credit, _project.Id, null);
            List<CreditSearchResult> ds = creditManagement.SearchCreditInDatabase(1, "DU");
            Assert.AreEqual(1, ds.Count);
            Assert.AreEqual("jean paul", ds[0].LoanOfficerName);
        }

        [Test]
        public void TestSearchContractInDatabaseWithUserIdSetAndUserLastNameAndFirstNameNull()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            UserManager userManager = new UserManager(DataUtil.TESTDB);
            User loanOfficer = new User();
            loanOfficer.UserName = "lo";
            loanOfficer.Password = "lo";
            loanOfficer.Role = User.Roles.LOF;
            loanOfficer.Mail = "Not Set";
            loanOfficer.Id = userManager.AddUser(loanOfficer);
            credit.LoanOfficer = loanOfficer;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            List<CreditSearchResult> ds = creditManagement.SearchCreditInDatabase(1, "G");
            Assert.AreEqual(1, ds.Count);
            Assert.AreEqual("lo", ds[0].LoanOfficerName);
        }

        [Test]
        public void TestSelectAllDisbursmentAlertWhenStartDateLowerThanTodayAndDisbursedFalseAndLoanOfficerFilterNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Code = "DSDSDs";
            creditManagement.AddCredit(credit, _project.Id, null);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(1, alertStock.GetNumberOfAlerts);
        }

        [Test]
        public void TestSelectAllDisbursmentAlertWhenStartDateLowerThanTodayAndDisbursedFalseAndLoanOfficerFilterActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);

            User _user = new User();
            _user.Id = this.addDataForTesting.AddUserWithMaximumAttributs();
            credit.LoanOfficer = _user;
            creditManagement.AddCredit(credit, _project.Id, null);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(2, alertStock.GetNumberOfAlerts);

            User.CurrentUser = new User();
            User.CurrentUser.Id = this.user.Id;
            User.CurrentUser.Role = User.Roles.LOF;

            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, true);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            FundingLine fund0 = new FundingLine();
            fund0.Purpose = "Microsoft financement";
            fund0.Name = "AFD130";
            fund0.Deleted = false;
            fund0.StartDate = DateTime.Now;
            fund0.EndDate = DateTime.Now;
            fund0.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund0, null);
            credit.FundingLine = fund0;

            CreditContractManagement manager = new CreditContractManagement(DataUtil.TESTDB, User.CurrentUser);
            AlertStock alertStock2 = manager.SelectAllAlerts("");
            Assert.AreEqual(1, alertStock2.GetNumberOfAlerts);
        }
        [Test]
        public void TestSelectAllAlertsWhenClientIsACorporate()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);

            User _user = new User();
            _user.Id = this.addDataForTesting.AddUserWithMaximumAttributs();
            credit.LoanOfficer = _user;

            Corporate myCorporate = new Corporate();
            myCorporate.Id = 42;
            credit.ClientType = OClientTypes.Corporate;
            FundingLine fund = new FundingLine
                                            {
                                                Purpose = "Funding",
                                                Name = "NJO42",
                                                Deleted = false,
                                                StartDate = DateTime.Now,
                                                EndDate = DateTime.Now,
                                                Currency = new Currency { Id = 1 }
                                            };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;

            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan corporateCredit = creditManagement.SelectCreditById(credit.Id);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(1, alertStock.GetNumberOfAlerts);
            Assert.AreEqual(credit.Id, alertStock.GetAlert(credit.Id).ContractId);
        }
        [Test]
        public void TestSelectAllAlertsDisbursmentTypeCorrectlyDefinedAndLoanOfficerFilterNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(1, alertStock.GetNumberOfAlerts);
        }

        [Test]
        public void TestSelectAllAlertsDisbursmentAlertWhenStartDateGreaterThanTodayAndDisbursedFalseAndLoanOfficerFilterNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);

            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.StartDate = DateTime.Today.AddDays(1);

            creditManagement.AddCredit(credit, _project.Id, null);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(1, alertStock.GetNumberOfAlerts);
        }

        [Test]
        public void TestSelectAllAlertsDisbursmentAlertWhenStartDateGreaterThanTodayAndDisbursedTrueAndLoanOfficerFilterNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            credit.StartDate = DateTime.Today.AddDays(1);
            credit.Disbursed = true;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(0, alertStock.GetNumberOfAlerts);
        }

        [Test]
        public void TestSelectAllAlertsDisbursmentAlertWhenStartDateLowerThanTodayAndDisbursedTrueAndLoanOfficerFilterNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);

            credit.Disbursed = true;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(0, alertStock.GetNumberOfAlerts);
        }

        [Test]
        public void TestSelectAllAlertsRepaymentAlertWhenInstallmentFullyRepaidAndLoanOfficerFilterNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            credit.Disbursed = true;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(0, alertStock.GetNumberOfAlerts);
        }

        [Test]
        public void TestSelectAllAlertsRepaymentAlertWhenInstallmentNotFullyRepaidAndLoanOfficerFilterNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            dataParam.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);

            //NonWorkingDateSingleton nonWorkingDateHelper = NonWorkingDateSingleton.GetInstance("");
            //nonWorkingDateHelper.WeekEndDay1 = 6;
            //nonWorkingDateHelper.WeekEndDay2 = 0;
            //nonWorkingDateHelper.PublicHolidays = new Hashtable();
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 1, 1), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 8), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 21), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 22), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 5, 1), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 5, 9), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 6, 27), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 9, 9), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 11, 6), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 11, 26), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 1, 6), "Christmas");

            //dataParam.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);

            credit.InstallmentList = new List<Installment>();
            installment1.PaidCapital = 0;
            credit.AddInstallment(installment1);
            credit.AddInstallment(installment2);
            credit.Disbursed = true;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(1, alertStock.GetNumberOfAlerts);
        }

        [Test]
        public void TestSelectAllAlertsRepaymentTypeCorrectlyDefinedAndLoanOfficerFilterNotActive()
        {
            //NonWorkingDateSingleton nonWorkingDateHelper = NonWorkingDateSingleton.GetInstance("");
            //nonWorkingDateHelper.WeekEndDay1 = 6;
            //nonWorkingDateHelper.WeekEndDay2 = 0;
            //nonWorkingDateHelper.PublicHolidays = new Hashtable();
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 1, 1), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 8), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 21), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 22), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 5, 1), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 5, 9), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 6, 27), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 9, 9), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 11, 6), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 11, 26), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 1, 6), "Christmas");

            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            dataParam.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);

            credit.InstallmentList = new List<Installment>();
            installment1.PaidCapital = 0;
            credit.AddInstallment(installment1);
            credit.AddInstallment(installment2);
            credit.Disbursed = true;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(1, alertStock.GetNumberOfAlerts);
        }

        [Test]
        public void TestSelectAllAlertsRepaymentAlertWhen2NonRepaidInstallmentsAndLoanOfficerFilterNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            dataParam.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);

            //NonWorkingDateSingleton nonWorkingDateHelper = NonWorkingDateSingleton.GetInstance("");
            //nonWorkingDateHelper.WeekEndDay1 = 6;
            //nonWorkingDateHelper.WeekEndDay2 = 0;
            //nonWorkingDateHelper.PublicHolidays = new Hashtable();
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 1, 1), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 8), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 21), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 22), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 5, 1), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 5, 9), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 6, 27), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 9, 9), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 11, 6), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 11, 26), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 1, 6), "Christmas");

            //dataParam.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);

            credit.InstallmentList = new List<Installment>();

            Installment installment1 = new Installment();
            installment1.Number = 1;
            installment1.CapitalRepayment = 200;
            installment1.InterestsRepayment = 100;
            installment1.PaidCapital = 0;
            installment1.PaidInterests = 100;
            installment1.FeesUnpaid = 0;
            installment1.ExpectedDate = DateTime.Today.AddDays(-2);

            Installment installment2 = new Installment();
            installment2.Number = 2;
            installment2.CapitalRepayment = 200;
            installment2.InterestsRepayment = 100;
            installment2.PaidCapital = 0;
            installment2.PaidInterests = 65;
            installment2.FeesUnpaid = 0;
            installment2.ExpectedDate = DateTime.Today.AddDays(-1);

            credit.Disbursed = true;
            credit.AddInstallment(installment1);
            credit.AddInstallment(installment2);
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(1, alertStock.GetNumberOfAlerts);
        }

        [Test]
        public void TestSelectAllAlertsWhenBothDisbursmentRepaymentAlertAndLoanOfficerFilterNotActive()
        {
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.LOANOFFICERPORTFOLIOFILTER, false);
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.USECENTS, true);
            dataParam.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);

            //NonWorkingDateSingleton nonWorkingDateHelper = NonWorkingDateSingleton.GetInstance("");
            //nonWorkingDateHelper.WeekEndDay1 = 6;
            //nonWorkingDateHelper.WeekEndDay2 = 0;
            //nonWorkingDateHelper.PublicHolidays = new Hashtable();
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 1, 1), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 8), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 21), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 22), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 5, 1), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 5, 9), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 6, 27), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 9, 9), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 11, 6), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 11, 26), "New Year Eve");
            //nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 1, 6), "Christmas");

            //dataParam.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);

            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);
            credit.Disbursed = true;
            credit.InstallmentList = new List<Installment>();
            installment1.PaidCapital = 0;
            credit.AddInstallment(installment1);
            credit.AddInstallment(installment2);
            creditManagement.AddCredit(credit, _project.Id, null);

            AlertStock alertStock = creditManagement.SelectAllAlerts("");
            Assert.AreEqual(2, alertStock.GetNumberOfAlerts);
        }

        [Test]
        public void TestUpdateContractLoanOfficer()
        {
            User user = new User();
            user.Id = addDataForTesting.AddUserWithMaximumAttributs();
            credit.LoanOfficer = user;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.AreEqual(user.Id, contractRetrieved.LoanOfficer.Id);

            credit.LoanOfficer.Id = addDataForTesting.AddRussianUserWithMinimumAttributs();

            creditManagement.UpdateContract(credit, null);
            contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.AreEqual(credit.LoanOfficer.Id, contractRetrieved.LoanOfficer.Id);
        }

        [Test]
        public void TestUpdateContractRural()
        {
            User user = new User();
            user.Id = addDataForTesting.AddUserWithMaximumAttributs();
            credit.LoanOfficer = user;
            credit.Rural = true;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.IsTrue(contractRetrieved.Rural);

            credit.Rural = false;
            creditManagement.UpdateContract(credit, null);
            contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.IsFalse(contractRetrieved.Rural);
        }

        [Test]
        public void TestUpdateContractAllLoanDetailsFormAttributesAreCorrectlyUpdated()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);

            User user = new User();
            user.Id = addDataForTesting.AddUserWithMaximumAttributs();
            credit.LoanOfficer = user;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = new DateTime(2008, 01, 01);
            fund.EndDate = new DateTime(2008, 04, 01);
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Amount = 4200;
            collateral.Amount = 100.0000m;
            credit.AddCollateral(collateral);
            credit.EntryFees = 1;
            credit.GracePeriod = 0;
            credit.InterestRate = 2;
            credit.NonRepaymentPenalties.InitialAmount = 4;
            credit.NonRepaymentPenalties.OLB = 4;
            credit.NonRepaymentPenalties.OverDueInterest = 4;
            credit.NonRepaymentPenalties.OverDuePrincipal = 4;
            credit.AnticipatedRepaymentPenalties = 5;
            credit.Disbursed = true;
            credit.NbOfInstallments = 1;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);

            credit.NbOfInstallments = 3;
            credit.Amount = 4566700000;
            credit.CalculateInstallments(true);
            // Update installment list and Update Contract
            installmentManagement.DeleteInstallments(credit.Id, null);
            installmentManagement.AddInstallments(credit.InstallmentList, credit.Id, null);

            creditManagement.UpdateContract(credit, null);

            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            _CompareCreditContract(contractRetrieved, credit);


            Assert.AreEqual(collateral.Amount.Value, contractRetrieved.Collaterals[0].Amount.Value);
        }

        [Test]
        public void UpdateCredit_SelectById_ContractChartOfAccounts()
        {
            credit.ChartOfAccounts.Accounts[1].Balance = 3000;
            credit.ChartOfAccounts.Accounts[4].Balance = 1254;
            credit.ChartOfAccounts.Accounts[6].Balance = 4587;
            credit.ChartOfAccounts.Accounts[16].Balance = 32.54m;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);

            contractRetrieved.ChartOfAccounts.Accounts[1].Balance = 2687;
            contractRetrieved.ChartOfAccounts.Accounts[4].Balance = 1254;
            contractRetrieved.ChartOfAccounts.Accounts[8].Balance = 12;
            contractRetrieved.ChartOfAccounts.Accounts[16].Balance = 3264;
            contractRetrieved.ChartOfAccounts.Accounts[19].Balance = 32.64m;


            creditManagement.UpdateContract(contractRetrieved, null);
            Loan contractRetrieved2 = creditManagement.SelectCreditById(credit.Id);

            Assert.AreEqual(2687m, contractRetrieved2.ChartOfAccounts.Accounts[1].Balance.Value);
            Assert.AreEqual(1254m, contractRetrieved2.ChartOfAccounts.Accounts[4].Balance.Value);
            Assert.AreEqual(4587m, contractRetrieved2.ChartOfAccounts.Accounts[6].Balance.Value);
            Assert.AreEqual(12m, contractRetrieved2.ChartOfAccounts.Accounts[8].Balance.Value);
            Assert.AreEqual(3264m, contractRetrieved2.ChartOfAccounts.Accounts[16].Balance.Value);
            Assert.AreEqual(32.64m, contractRetrieved2.ChartOfAccounts.Accounts[19].Balance.Value);
        }

        [Test]
        public void UpdateCredit_SelectByCode_ContractChartOfAccounts()
        {
            credit.Code = "Code";
            credit.ChartOfAccounts.Accounts[2].Balance = 3000;
            credit.ChartOfAccounts.Accounts[5].Balance = 1254;
            credit.ChartOfAccounts.Accounts[7].Balance = 4587;
            credit.ChartOfAccounts.Accounts[17].Balance = 32.54m;

            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectContractByContractCode(credit.Code);

            contractRetrieved.ChartOfAccounts.Accounts[2].Balance = 2687;
            contractRetrieved.ChartOfAccounts.Accounts[5].Balance = 1254;
            contractRetrieved.ChartOfAccounts.Accounts[9].Balance = 12;
            contractRetrieved.ChartOfAccounts.Accounts[17].Balance = 3264;
            contractRetrieved.ChartOfAccounts.Accounts[20].Balance = 32.64m;

            creditManagement.UpdateContract(contractRetrieved, null);
            Loan contractRetrieved2 = creditManagement.SelectContractByContractCode(credit.Code);

            Assert.AreEqual(2687m, contractRetrieved2.ChartOfAccounts.Accounts[2].Balance.Value);
            Assert.AreEqual(1254m, contractRetrieved2.ChartOfAccounts.Accounts[5].Balance.Value);
            Assert.AreEqual(4587m, contractRetrieved2.ChartOfAccounts.Accounts[7].Balance.Value);
            Assert.AreEqual(12m, contractRetrieved2.ChartOfAccounts.Accounts[9].Balance.Value);
            Assert.AreEqual(3264m, contractRetrieved2.ChartOfAccounts.Accounts[17].Balance.Value);
            Assert.AreEqual(32.64m, contractRetrieved2.ChartOfAccounts.Accounts[20].Balance.Value);
        }

        [Test]
        public void TestUpdateContractFundingLines()
        {
            User user = new User();
            user.Id = addDataForTesting.AddUserWithMaximumAttributs();
            credit.LoanOfficer = user;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.AreEqual("AFD130", contractRetrieved.FundingLine.Name);

            FundingLine fund0 = new FundingLine();
            fund0.Purpose = "Microsoft financement";
            fund0.Name = "AFD1302";
            fund0.Deleted = false;
            fund0.StartDate = DateTime.Now;
            fund0.EndDate = DateTime.Now;
            fund0.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund0, null);
            credit.FundingLine = fund0;

            addDataForTesting.AddGenericFundingLine2();
            creditManagement.UpdateContract(credit, null);
            Loan contractRetrieved2 = creditManagement.SelectCreditById(credit.Id);
            Assert.AreEqual("AFD1302", contractRetrieved2.FundingLine.Name);
        }

        [Test]
        public void TestUpdateContractCommentsOfEndWhenNoExistingCommentsBefore()
        {
            User user = new User();
            user.Id = addDataForTesting.AddUserWithMaximumAttributs();
            credit.LoanOfficer = user;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.IsNull(contractRetrieved.CommentsOfEnd);

            string commentsOfEnd = "Contract has always been repaid correctly";
            creditManagement.UpdateContractCommentsOfEnd(credit.Id, commentsOfEnd);
            Assert.AreEqual("Contract has always been repaid correctly", creditManagement.SelectCreditById(credit.Id).CommentsOfEnd);
        }

        [Test]
        public void TestUpdateContractCommentsOfEndWhenACommentAlreadyExists()
        {
            User user = new User();
            user.Id = addDataForTesting.AddUserWithMaximumAttributs();
            credit.LoanOfficer = user;
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            string commentsOfEnd = "Contract has always been repaid correctly";
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            creditManagement.UpdateContractCommentsOfEnd(credit.Id, commentsOfEnd);
            Loan contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.AreEqual("Contract has always been repaid correctly", contractRetrieved.CommentsOfEnd);

            commentsOfEnd = "Mistake : second installment was not repaid correctly";
            creditManagement.UpdateContractCommentsOfEnd(credit.Id, commentsOfEnd);
            Assert.AreEqual("Mistake : second installment was not repaid correctly", creditManagement.SelectCreditById(credit.Id).CommentsOfEnd);
        }

        [Test]
        public void TestSeachContractByCode()
        {
            FundingLine fund = new FundingLine();
            fund.Purpose = "Microsoft financement";
            fund.Name = "AFD130";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.Code = "TEST22";
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);
            int id = credit.Id;

            Assert.AreEqual(id, creditManagement.SelectContractByContractCode(credit.Code).Id);
        }

        [Test]
        public void TestChangesContractHistory()
        {
            User user = new User();
            user.FirstName = "Andrey";
            user.LastName = "Galkin";
            user.Password = "Password";
            user.UserName = "UserName";
            user.Mail = "xx@yy.zz";

            userManager.AddUser(user);

            Assert.AreEqual(2, credit.InstallmentList.Count);
            FundingLine fund = new FundingLine
            {
                Purpose = "Microsoft financement",
                Name = "AFD130",
                Deleted = false,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Currency = new Currency { Id = 1 }
            };

            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.LoanOfficer = user;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);

            var contractRetrieved = creditManagement.SelectCreditById(credit.Id);
            Assert.AreEqual(credit.LoanOfficer.Id, contractRetrieved.LoanOfficer.Id);

            var newUser = new User { Id = addDataForTesting.AddUserWithMaximumAttributs() };

            credit.LoanInitialOfficer = user;
            credit.LoanOfficer = newUser;
            creditManagement.UpdateContract(credit, null);
            creditManagement.UpdateCreditByOfficer(credit.Id, newUser.Id, user.Id, null);

            Loan contractReassing = creditManagement.SelectCreditById(credit.Id);
            Assert.AreEqual(credit.LoanOfficer.Id, contractReassing.LoanOfficer.Id);

            List<int> contract_id = creditManagement.SelectContractIdByNewOldLoanOfficer(newUser.Id, user.Id, null);
            Assert.AreEqual(credit.Id, contract_id[0]);
        }

        [Test]
        public void TestReassignContract()
        {
            User user = new User();
            user.FirstName = "Andrey";
            user.LastName = "Galkin";
            user.Password = "Password";
            user.UserName = "UserName";
            user.Mail = "xx@yy.zz";

            userManager.AddUser(user);

            Assert.AreEqual(2, credit.InstallmentList.Count);
            FundingLine fund = new FundingLine
            {
                Purpose = "Microsoft financement",
                Name = "AFD130",
                Deleted = false,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Currency = new Currency { Id = 1 }
            };

            fundingLineManager.AddFundingLine(fund, null);
            credit.FundingLine = fund;
            credit.LoanOfficer = user;
            credit.Id = creditManagement.AddCredit(credit, _project.Id, null);

            var newUser = new User { Id = addDataForTesting.AddUserWithMaximumAttributs() };

            creditManagement.UpdateCreditByOfficer(credit.Id, newUser.Id, user.Id, null);

            List<int> contract_id = creditManagement.SelectContractIdByNewOldLoanOfficer(newUser.Id, user.Id, null);
            Assert.AreEqual(credit.Id, contract_id[0]);
        }

        [Test]
        public void TestUpdateContractRelatedToPackage()
        {
            addDataForTesting.DeletePackage();
            _package = new LoanProduct();
            _package.Name = "Package";
            _package.Delete = false;
            _package.LoanType = OLoanTypes.Flat;
            _package.ClientType = 'G';
            _package.ChargeInterestWithinGracePeriod = true;
            _package.InstallmentType = biWeekly;
            _package.KeepExpectedInstallment = false;
            _package.AnticipatedRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingInterest;
            _package.LoanType = OLoanTypes.Flat;
            _package.Amount = 100;
            _package.AnticipatedTotalRepaymentPenalties = 0.1;

            FundingLine fund = new FundingLine();
            fund.Id = 0;
            fund.Purpose = "Microsoft financement";
            fund.Name = "Clyon";
            fund.Deleted = false;
            fund.StartDate = DateTime.Now;
            fund.EndDate = DateTime.Now;
            fund.Currency = new Currency { Id = 1 };
            fundingLineManager.AddFundingLine(fund, null);
            _package.FundingLine = fund;
            _package.Code = "code";
            _package.Currency = new Currency { Id = 1 };
            _package.Id = _packageManager.Add(_package);

            credit.Product = _package;
            credit.FundingLine = fund;
            creditManagement.AddCredit(credit, _project.Id, null);

            _package.AnticipatedTotalRepaymentPenalties = 0.99;
            _packageManager.UpdatePackage(_package, true);

            Loan selectedCredit = creditManagement.SelectCreditById(credit.Id);

            Assert.AreEqual(selectedCredit.AnticipatedRepaymentPenalties, 0.99);
        }

    }
}
