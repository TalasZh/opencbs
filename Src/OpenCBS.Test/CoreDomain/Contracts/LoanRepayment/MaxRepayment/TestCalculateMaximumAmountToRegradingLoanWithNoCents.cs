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
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.MaxRepayment;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using NUnit.Framework;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain.Contracts.LoanRepayment.MaxRepayment
{
    /// <summary>
    /// Summary description for TestCalculateMaximumAmountToRegradingLoan.
    /// </summary>
    [TestFixture]
    public class TestCalculateMaximumAmountToRegradingLoanWithNoCents
    {
        private CalculateMaximumAmountToRegradingLoanStrategy repayStrategy;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            ApplicationSettings generalParameters = ApplicationSettings.GetInstance("");
            generalParameters.DeleteAllParameters();
            generalParameters.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
            generalParameters.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalParameters.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            generalParameters.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
            generalParameters.AddParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
            generalParameters.AddParameter(OGeneralSettings.STOP_WRITEOFF_PENALTY, true);
        }

        [TestFixtureTearDown]
        public void testFixtureTearDown()
        {
            ApplicationSettings generalParameters = ApplicationSettings.GetInstance("");
            generalParameters.DeleteAllParameters();
            generalParameters.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
            generalParameters.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalParameters.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
        }

        [Test]
        public void TestCalculateMaximumAmountWithFlateBadLoanWith42DaysLateWhenNonRepaymentFeesBaseOnInitialAmountAndKeepExpectedInstallmentAndCancelFeesSetToFalse()
        {
            LoanProduct package = new LoanProduct
                                      {
                                          InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                                          LoanType = OLoanTypes.Flat,
                                          ChargeInterestWithinGracePeriod = true,
                                          Currency = new Currency { Id = 1 }
                                      };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package,1000,0.03m,6,1,new DateTime(2006,1,1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.InitialAmount = 0.0123;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            //42 days late
            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 1000 * 0.0123 * 42 = 776.6 => 777
            //  Assert.AreEqual(777, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
            Assert.AreEqual(777, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 0));
        }

        [Test]
        public void TestCalculateMaximumAmountWithFlateBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnInitialAmountAndKeepExpectedInstallmentAndCancelFeesSetToTrue()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.InitialAmount = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, true, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230
            Assert.AreEqual(260, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void TestCalculateMaximumAmountWithFlateBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnInitialAmountAndKeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.InitialAmount = 0.0033;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 1000 * 0.0033 * 42 = 398.6 => 399
            // Assert.AreEqual(399, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
            Assert.AreEqual(399, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 0));

        }

        [Test]
        public void TestCalculateMaximumAmountWithFlateBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOLBAndKeepExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OLB = 0.0034;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 1000 * 0.0034 * 42 = 402.8 => 403
            //Assert.AreEqual(403, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
            Assert.AreEqual(403, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 0));
        }
		
        [Test]
        public void TestCalculateMaximumAmountWithFlateBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOLBAndKeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OLB = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0,  package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 1000 * 0.003 * 42 = 386
            Assert.AreEqual(386, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void TestCalculateMaximumAmountWithFlateBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithoutInterestAndKeepExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, false);
            //30 + 230 + 200 * 0.003 * 12 = 268.4 => 267
            // Assert.AreEqual(267, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
            Assert.AreEqual(267, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 0));

            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);

            //30 + 230 + 200 * 0.003 * 14 = 268.4 => 268
            Assert.AreEqual(268, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void TestCalculateMaximumAmountWithFlateBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithoutInterestAndKeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 200 * 0.003 * 14 = 268.4 => 268
            Assert.AreEqual(268, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void TestCalculateMaximumAmountWithFlateBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithInterestAndKeepExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDueInterest = 0.003;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 30 * 0.003 * 42 + 230 * 0.003 *14 = 30 + 230 + 3.78 + 9.66 =273.44=> 30 + 230 + 4 + 10 = 274
            Assert.AreEqual(273, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }


        [Test]
        public void TestCalculateMaximumAmountWithFlateBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithInterestAndKeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDueInterest = 0.003;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 30 * 0.003 * 42 + 230 * 0.003 *14 = 30 + 230 + 3.78 + 9.66 = 273.44=> 30 + 230 + 4 + 10 = 274	
            Assert.AreEqual(273, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void TestCalculateMaximumAmountWithDecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnInitialAmountAndKeepExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 },
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.InitialAmount = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 1000 * 0.003 * 42 = 30 + 218 + 126 => 374
            Assert.AreEqual(374, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));

        }

        [Test]
        public void TestCalculateMaximumAmountWithDecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnInitialAmountAndKeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 },
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.InitialAmount = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 1000 * 0.003 * 42 = 30 + 218 + 126 => 374
            Assert.AreEqual(374, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }


        [Test]
        public void TestCalculateMaximumAmountWithDecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOLBAndKeepExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 },
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OLB = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 1000 * 0.003 * 42 = 30 + 218 + 126 => 374
            Assert.AreEqual(374, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));

        }

        [Test]
        public void TestCalculateMaximumAmountWithDecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOLBAndKeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 },
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OLB = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0,0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 1000 * 0.003 * 42 = 30 + 218 + 126 => 374
            Assert.AreEqual(374, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void TestCalculateMaximumAmountWithDecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithoutInterestAndKeepExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 },
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 188.35 * 1.003 * 7.91 = 30 + 218 + 8 = 256
            Assert.AreEqual(256, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void TestCalculateMaximumAmountWithDecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithoutInterestAndKeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 },
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 188.35 * 1.003 * 42 = 30 + 218 + 8 = 256
            Assert.AreEqual(256, Math.Round(this.repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void TestCalculateMaximumAmountWithDecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithInterestAndKeepExpectedInstallment()
        {
            LoanProduct package = new LoanProduct{
                                          InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                                          LoanType = OLoanTypes.DecliningFixedInstallments,
                                          ChargeInterestWithinGracePeriod = true,
                                          Currency = new Currency { Id = 1 },
                                          RoundingType = ORoundingType.Approximate
                                      };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDueInterest = 0.003;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0,0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            this.repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 30 * 0.003 * 42 + 218.35 * 0.003 * 14 = 30 + 218 + 4 + 9 = 261
            Assert.AreEqual(261, Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void TestCalculateMaximumAmountWithDecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithInterestAndKeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 },
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDueInterest = 0.003;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0,0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 30 * 0.003 * 42 + 218.35 * 0.003 * 14 = 30 + 218 + 4 + 9 = 261
            Assert.AreEqual(261, Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }
    }
}
