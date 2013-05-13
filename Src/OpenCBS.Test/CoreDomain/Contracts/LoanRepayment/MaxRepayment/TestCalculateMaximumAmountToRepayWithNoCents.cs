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
    /// Summary description for TestCalculateMaximumAmountToRepay.
    /// </summary>
    [TestFixture]
    public class TestCalculateMaximumAmountToRepayWithNoCents
    {
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
        public void CalculateMaximumAmountFlat_BadLoan_42DaysLate_InitialAmount_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            //1000 + 180 + 1000 * 42 * 0.003 => 1306 
            Assert.AreEqual(1306, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmountFlat_BadLoan_42DaysLate_InitialAmount_KeepExpectedInstallment_CancelFees()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, true, 0, 0, false, 0);

            Assert.AreEqual(1180, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmountFlat_BadLoan_42DaysLate_InitialAmount_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            //1000 + 60 + 1000 * 42 * 0.003 + 800 * 0.01 => 1194 
            Assert.AreEqual(1194, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmountFlat_BadLoan_42DaysLate_OLB_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            //1000 + 180 + 1000 * 42 * 0.003  => 1306 
            Assert.AreEqual(1306, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmountFlat_BadLoan_42DaysLate_OLB_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            //1000 + 60 + 1000 * 42 * 0.003 + 800 * 0.01 => 1194 
            Assert.AreEqual(1194, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmountFlat_BadLoan_42DaysLate_OverduePrincipal_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);


            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, false);
            //1000 + 180 + 200 * 12 * 0.003
            Assert.AreEqual(1187, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);

            //1000 + 180 + 200 * 14 * 0.003
            Assert.AreEqual(1188, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmountFlat_BadLoan_42DaysLate_OverduePrincipal_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);
            
            //1000 + 60 + 200 * 14 * 0.003 + 800 * 0.01 => 1196,4 =>1076
            Assert.AreEqual(1076, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmountFlat_BadLoan_42DaysLate_OverduePrincipalAndInterest_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1193, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmountFlat_BadLoan_42DaysLate_OverdueInterestAndPrincipal_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1081, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42DaysLate_InitialAmount_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1248, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42DaysLate_InitialAmount_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0.003, 0, 0, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1194.00m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42DaysLate_OLB_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0.003, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1248, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42DaysLate_OLB_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0.003, 0, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1194.00m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42DaysLate_OverduePrincipal_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1130, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42DaysLate_OverduePrincipal_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1076, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42DaysLate_OverduePrincipalAndInterest_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1135, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_DecliningFixedPrincipal_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(2196, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2007, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42DaysLate_OverduePrincipalAndInterest_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0.003), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1081m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        #region private methods
        private static Loan _SetContract(OLoanTypes pLoansType, NonRepaymentPenalties pNonRepaymentPenalties, bool pKeepExpectedInstallment)
        {
            LoanProduct package = new LoanProduct
                                      {
                                          InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                                          LoanType = pLoansType,
                                          ChargeInterestWithinGracePeriod = true,
                                          KeepExpectedInstallment = pKeepExpectedInstallment,
                                          AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB,
                                          Currency = new Currency { Id = 1 },
                                          RoundingType = ORoundingType.Approximate
                                      };
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;
            myContract.NonRepaymentPenalties = pNonRepaymentPenalties;
            return myContract;
        }

        private static CalculateMaximumAmountToRepayStrategy _MaximumAmountToRepay(Loan pContract, bool pCancelFees, OCurrency pManualFeesAmount, OCurrency pManualCommissionAmount, bool pCancelInterest, OCurrency pManualInterestAmount)
        {
            CreditContractOptions cCO = new CreditContractOptions(pContract.Product.LoanType, pContract.Product.KeepExpectedInstallment, pCancelFees, 
                pManualFeesAmount, pManualCommissionAmount, 
                pCancelInterest, pManualInterestAmount, pContract.Product.AnticipatedTotalRepaymentPenaltiesBase);

            return new CalculateMaximumAmountToRepayStrategy(cCO, pContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));
        }
        #endregion
    }
}
