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
using OpenCBS.CoreDomain.Events;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using NUnit.Framework;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain.Contracts.LoanRepayment.RepayLateInstallments
{
    /// <summary>
    /// Summary description for TestRepayLateInstallments.
    /// </summary>
    [TestFixture]
    public class TestRepayLateInstallmentsWithNoCents
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
            generalParameters.AddParameter(OGeneralSettings.STOP_WRITEOFF_PENALTY, false);
        }

        [TestFixtureTearDown]
        public void testFixtureTearDown()
        {
            ApplicationSettings generalParameters = ApplicationSettings.GetInstance("");
            generalParameters.DeleteAllParameters();
            generalParameters.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalParameters.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
        }

        [Test]
        public void RepayInstallment_WrittenOffContract_StopWriteOff_InitialAmount_8LateDays_EnoughMoneyToRepay()
        {
            ApplicationSettings gp = ApplicationSettings.GetInstance("");
            gp.UpdateParameter(OGeneralSettings.STOP_WRITEOFF_PENALTY, true);
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.0026, 0, 0, 0), true, 1);
            myContract.WriteOff(new DateTime(2006, 2, 1));
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            //8 days late
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9));

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 0); 
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_WrittenOffContract_DontStopWriteOff_InitialAmount_8LateDays_EnoughMoneyToRepay()
        {
            ApplicationSettings gp = ApplicationSettings.GetInstance("");
            gp.UpdateParameter(OGeneralSettings.STOP_WRITEOFF_PENALTY, false);
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.0026, 0, 0, 0), true, 1);
            myContract.WriteOff(new DateTime(2006, 2, 1));
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            //8 days late
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9));

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 21); //1000 * 0.0026 * 8 = 20.8 => 21
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_CancelFees_InitialAmount_8LateDays_EnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, true);

            OCurrency amountPaid = 30, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);

            AssertRepayEvent(feesEvent, 0, interestEvent, 30, principalEvent, 0, amountPaid, 0);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void CalculateNewInstallmentsWithLateFees_DontCancelFees_InitialAmount_8LateDays()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.0026, 0, 0, 0), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            //8 days late
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9));

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 21); //1000 * 0.0026 * 8 = 20.8 => 21
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallments_DontCancelFees_InitialAmount_8LateDays_EnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 154m, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0; 
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(24, feesEvent, 30, interestEvent, 0, principalEvent, 100, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallments_DontCancelFees_InitialAmount_RepaymentTwicelyADay()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "10-days", 10, 0),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;

            Loan myContract = new Loan(package, 8000, 0.03333m, 5, 0, new DateTime(2010, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = false;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;
            myContract.NonRepaymentPenalties = new NonRepaymentPenalties(0, 0, 0.0667, 0.01);

            myContract.CalculateInstallments(true);

            myContract.Disburse(new DateTime(2010, 1, 1), false, true);
            myContract.Repay(1, new DateTime(2010, 1, 8), 30, false, true);

            RepaymentEvent rPE = myContract.Repay(1, new DateTime(2010, 1, 11), 0, false, true);

            Assert.AreEqual(rPE.Penalties, 0);
        }

        [Test]
        public void CalculateNewInstallmentsWithlateFees_DontCancelFees_InitialAmount_OLB_8LateDays()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.0026, 0.0046, 0, 0), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 58); //1000 * 0.0026 * 8 + 1000 * 0.0046 * 8 = 57.6 =>58
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void CalculateNewInstallmentsWithlateFees_DontCancelFees_InitialAmount_OLB_OverdueInterest_OverduePrincipal_36LateDays()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);

            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 255); //1000 * 0.003 * 36 + 1000 * 0.004 * 36 + 0 * 0.003 * 36 + 30 * 0.003 * 36 = 255.24 =>255
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 6); //30 * 0.003 * 8 + 200 * 0.003 * 8 = 5.52 => 6
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);

            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true, 1);
            rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 231); 
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 5); 
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void CalculateNewInstallmentsWithlateFees_CancelFees_InitialAmount_OLB_OverdueInterest_OverduePrincipal_36LateDays()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, true);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9));//36 days late

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_InitialAmount_36daysLate_EnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 500, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(108, feesEvent, 60, interestEvent, 200, principalEvent, 132, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_OLB_8LateDays_EnoughMoneyToRepay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 100, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(24, feesEvent, 30, interestEvent, 0, principalEvent, 46, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_OLB_36DaysLate_NotEnoughMoneyToRepay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = 
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 100, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(100, feesEvent, 0, interestEvent, 0, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 8);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_OverDuePrincipal_OverdueInterest_8DaysLate_EnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 100, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(1, feesEvent, 30, interestEvent, 0, principalEvent, 69, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_OverDuePrincipal_OverdueInterest_36DaysLate_EnoughMoneyToRepay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);
            
            OCurrency amountPaid = 500, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9));//36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount); //= 3.42 + 5.58
            AssertRepayEvent(9, feesEvent, 60, interestEvent, 200, principalEvent, 231, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);

            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);
            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true, 1);
            rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9));//36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount); //= 3.42 + 5.58
            AssertRepayEvent(17, feesEvent, 120, interestEvent, 363, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 163, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_OverDuePrincipal_8DaysLate_EnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 100, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(0, feesEvent, 30, interestEvent, 0, principalEvent, 70, amountPaid); //0 because (gracePeriod) donc no principal

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_OverDuePrincipal_36DaysLate_EnoughMoneyToRepay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 500, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(5, feesEvent, 60, interestEvent, 200, principalEvent, 235, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true, 1);
            rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(9, feesEvent, 120, interestEvent, 371, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 171, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_CancelFees_InitialAmount_8DaysLate_EnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, true);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 30, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(0, feesEvent, 30, interestEvent, 0, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_KeepNotExpectedInstallment_InitialAmount_8daysLate()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), false, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 100, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(24, feesEvent, 30, interestEvent, 0, principalEvent, 46, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_KeepNotExpectedInstallment_InitialAmount_36daysLate()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), false, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 500, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(108, feesEvent, 60, interestEvent, 200, principalEvent, 132, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_KeepNotExpectedInstallment_OLB_8DaysLate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), false, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = 
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 100, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(24, feesEvent, 30, interestEvent, 0, principalEvent, 46, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_KeepNotExpectedInstallment_OLB_36DaysLate_NoEnoughMoneyToRepay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);

            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), false, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = 
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 100, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(100, feesEvent, 0, interestEvent, 0, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 8);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_KeepNotExpectedInstallment_OverDuePrincipal_OverDueInterest_8DaysLate()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), false, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 100, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(1, feesEvent, 30, interestEvent, 0, principalEvent, 69, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_KeepNotExpectedInstallment_OverDueInterest_OverduePrincipal_36DaysLate_NoEnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), false, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 33, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount); //= 3
            AssertRepayEvent(3, feesEvent, 30, interestEvent, 0, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);// fees = 0
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_KeepNotExpectedInstallment_OverDuePrincipal_8DaysLate()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 100, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(0, feesEvent, 30, interestEvent, 0, principalEvent, 70, amountPaid); //0 because (gracePeriod) donc no principal

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_KeepNotExpectedInstallment_OverDuePrincipal_36DaysLate()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false, 1);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 500, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(5, feesEvent, 60, interestEvent, 200, principalEvent, 235, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true, 1);
            rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(9, feesEvent, 120, interestEvent, 371, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 171, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        #region private methods
        private static void AssertRepayEvent(OCurrency pActualFees, OCurrency pExpectedFees, OCurrency pActualInterest, OCurrency pExpectedInterest,
                                             OCurrency pActualPrincipal, OCurrency pExpectedPrincipal, OCurrency pActualAmountPaid, OCurrency pExpectedAmountPaid)
        {
            Assert.AreEqual(pExpectedFees.Value, pActualFees.Value);
            Assert.AreEqual(pExpectedInterest.Value, pActualInterest.Value);
            Assert.AreEqual(pExpectedPrincipal.Value, pActualPrincipal.Value);
            Assert.AreEqual(pExpectedAmountPaid.Value, pActualAmountPaid.Value);
        }

        private static OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments _SetRepaymentOptions(Loan pContract, bool pCancelFees)
        {
            CreditContractOptions cCO = new CreditContractOptions(pContract.Product.LoanType, pContract.Product.KeepExpectedInstallment, pCancelFees, 0, 0, false, 0,
                                                                  pContract.Product.AnticipatedTotalRepaymentPenaltiesBase);

            return new OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments(cCO, pContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));
        }

        private static Loan _SetContract(OLoanTypes pLoansType, NonRepaymentPenalties pNonRepaymentFees, bool pKeepExpectedInstallment, int NumberOfGracePeriod)
        {
            LoanProduct package = new LoanProduct{
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = pLoansType,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };
            package.KeepExpectedInstallment = pKeepExpectedInstallment;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, NumberOfGracePeriod, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = false;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;
            myContract.NonRepaymentPenalties = pNonRepaymentFees;
            return myContract;
        }

        private static void AssertSpecifiedInstallment(Installment pInstallment, OCurrency pInterestRepayment, OCurrency pCapitalRepayment, OCurrency pPaidCapital, OCurrency pPaidInterest, OCurrency pFeesUnpaid)
        {
            Assert.AreEqual(pInterestRepayment.Value, pInstallment.InterestsRepayment.Value);
            Assert.AreEqual(pCapitalRepayment.Value, pInstallment.CapitalRepayment.Value);
            Assert.AreEqual(pPaidCapital.Value, pInstallment.PaidCapital.Value);
            Assert.AreEqual(pPaidInterest.Value, pInstallment.PaidInterests.Value);
            Assert.AreEqual(pFeesUnpaid.Value, pInstallment.FeesUnpaid.Value);
        }
        #endregion
    }
}
