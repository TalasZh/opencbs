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
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using NUnit.Framework;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain.Contracts.LoanRepayment.RepayLateInstallments
{
    /// <summary>
    /// Summary description for TestLateFeesCalculationWithCents.
    /// </summary>
    [TestFixture]
    public class TestLateFeesCalculationWithNoCents
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            ApplicationSettings generalParameters = ApplicationSettings.GetInstance("");
            generalParameters.DeleteAllParameters();
            generalParameters.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
            generalParameters.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalParameters.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
            generalParameters.AddParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
            generalParameters.AddParameter(OGeneralSettings.STOP_WRITEOFF_PENALTY, true);
        }

        [TestFixtureTearDown]
        public void testFixtureTearDown()
        {
            ApplicationSettings generalParameters = ApplicationSettings.GetInstance("");
            generalParameters.DeleteAllParameters();
            generalParameters.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalParameters.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
        }

        private static Loan _SetContract(OLoanTypes pLoansType, NonRepaymentPenalties pNonRepaymentFees, bool pKeepExpectedInstallment)
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = pLoansType,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };

            package.KeepExpectedInstallment = pKeepExpectedInstallment;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = false;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;
            myContract.NonRepaymentPenalties = pNonRepaymentFees;

            return myContract;
        }

        private static Loan _SetContractWithGracePeriodOfFees(OLoanTypes pLoansType, NonRepaymentPenalties pNonRepaymentFees, int gpLateFees, bool pKeepExpectedInstallment)
        {
            LoanProduct package = new LoanProduct
                                      {
                                          InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                                          LoanType = pLoansType,
                                          ChargeInterestWithinGracePeriod = true,
                                          Currency = new Currency {Id = 1},
                                          GracePeriodOfLateFees = gpLateFees
                                      };

            package.KeepExpectedInstallment = pKeepExpectedInstallment;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;

            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = false;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;
            myContract.NonRepaymentPenalties = pNonRepaymentFees;

            return myContract;
        }

        [Test]
        public void LateFeesCalculation_InitialAmount()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.0026, 0, 0, 0), true);
            Assert.AreEqual(10m, CalculationBaseForLateFees.FeesBasedOnInitialAmount(myContract, new DateTime(2006, 2, 5), 1, false, ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance("")).Value); // 1000 * 0.0026 * 4 = 10.4 => 10
        }

        [Test]
        public void LateFeesCalculation_OLB()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0,0.0026, 0, 0), true);

            Assert.AreEqual(10m, CalculationBaseForLateFees.FeesBasedOnOlb(myContract, new DateTime(2006, 2, 5), 1, false, ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance("")).Value); // 1000 * 0.0026 * 4 = 10.4 => 10
            
        }

        [Test]
        public void LateFeesCalculation_OverdueInterest()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);                        
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0, 0.003), true);
            Assert.AreEqual(3m, CalculationBaseForLateFees.FeesBasedOnOverdueInterest(myContract, new DateTime(2006, 3, 5), 1, false, ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance("")).Value); // 30 * 0.003 * 32 = 2.88 =>3
            
            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);
            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0, 0.003), true);
            Assert.AreEqual(3m, CalculationBaseForLateFees.FeesBasedOnOverdueInterest(myContract, new DateTime(2006, 3, 5), 1, false, ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance("")).Value); // 30 * 0.003 * 28 = 2.430 =>2
        }

        [Test]
        public void LateFeesCalculation_OverduePrincipal()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true);
            Assert.AreEqual(0m, CalculationBaseForLateFees.FeesBasedOnOverdueInterest(myContract, new DateTime(2006, 3, 5), 1, false, ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance("")).Value);//0 (1st installment without interest) * 0.003 * 32 = 0
        }

        [Test]
        public void LateFeesCalculation_OverduePrincipalWithGracePeriodLatePenalties()
        {
            Loan myContract = _SetContractWithGracePeriodOfFees(OLoanTypes.Flat, new NonRepaymentPenalties(0.01, 0.01, 0.01, 0.01), 5, true);
            Assert.AreEqual(0m, CalculationBaseForLateFees.FeesBasedOnOverdueInterest(myContract, new DateTime(2006, 2, 6), 1, false, ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance("")).Value);//0 (1st installment without interest) * 0.003 * 32 = 0
        }
    }
}
