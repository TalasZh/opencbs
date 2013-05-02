// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment;
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
    public class TestRepayLateInstallmentsWithCents
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            NonWorkingDateSingleton nonWorkingDateHelper = NonWorkingDateSingleton.GetInstance("");
            nonWorkingDateHelper.WeekEndDay1 = 6;
            nonWorkingDateHelper.WeekEndDay2 = 0;
            nonWorkingDateHelper.PublicHolidays = new Dictionary<DateTime, string>();
            nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 1, 1), "New Year Eve");
            nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 8), "New Year Eve");
            nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 21), "New Year Eve");
            nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 3, 22), "New Year Eve");
            nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 5, 1), "New Year Eve");
            nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 5, 9), "New Year Eve");
            nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 6, 27), "New Year Eve");
            nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 9, 9), "New Year Eve");
            nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 11, 6), "New Year Eve");
            nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 11, 26), "New Year Eve");
            nonWorkingDateHelper.PublicHolidays.Add(new DateTime(2006, 1, 6), "Christmas");
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
            generalParameters.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
            generalParameters.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalParameters.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
        }

        [Test]
        public void RepayInstallment_CancelFees_InitialAmount_8LateDays_EnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat,new NonRepaymentPenalties(0.003,0,0,0),true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, true, 0, 0);

            OCurrency amountPaid = 30, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            Assert.AreEqual(new DateTime(2006,2,1),myContract.GetInstallment(0).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);

            AssertRepayEvent(feesEvent,0,interestEvent,30,principalEvent,0,amountPaid,0);

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
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            //8 days late
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9));

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30,   0, 0, 0, 24); //1000 * 0.003 * 8
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0,  0);
        }

        [Test]
        public void RepayInstallments_DontCancelFees_InitialAmount_8LateDays_EnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 154m, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(24, feesEvent, 30, interestEvent, 0, principalEvent,100,amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30,  0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void CalculateNewInstallmentsWithlateFees_DontCancelFees_InitialAmount_OLB_8LateDays()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0, 0), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 56); //1000 * 0.003 * 8 + 1000 * 0.004 * 8 => 56
            
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void CalculateNewInstallmentsWithlateFees_DontCancelFees_InitialAmount_OLB_OverdueInterest_OverduePrincipal_36LateDays()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late

            //1000 * 0.003 * 36 + 1000 * 0.004 * 36 + 0 * 0.003 * 36 + 30 * 0.003 * 36 => 255.24
            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 255.24m);
            //30 * 0.003 * 8 + 200 * 0.003 * 8 => 5.52
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 5.52m); 
            
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);

            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true);
            rLI = _SetRepaymentOptions(myContract, false);
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 230.7m);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 4.14m); 

            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void CalculateNewInstallmentsWithlateFees_CancelFees_InitialAmount_OLB_OverdueInterest_OverduePrincipal_36LateDays()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = 
                _SetRepaymentOptions(myContract, true, 0, 0);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30,   0, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_InitialAmount_36daysLate_EnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
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
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), true);
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
        public void RepayInstallment_DontCancelFees_OLB_36DaysLate_NotEnoughMoneyToRepay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = 
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006,2,1),myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006,3,1),myContract.GetInstallment(1).ExpectedDate);

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
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 100, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(0.72m, feesEvent, 30, interestEvent, 0, principalEvent, 69.28m, amountPaid);
            
            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);

            // NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);
            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(1.35m, feesEvent, 60, interestEvent, 0, principalEvent, 38.65m, amountPaid);

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
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006,2,1),myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006,3,1),myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 500, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9));//36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount); //= 3.42 + 5.58
            AssertRepayEvent(8.76m, feesEvent, 60, interestEvent, 200, principalEvent, 231.24m, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9));//36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount); //= 3.42 + 5.58
            AssertRepayEvent(15.60m, feesEvent, 120, interestEvent, 364.40m, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 164.40m, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_OverDuePrincipal_8DaysLate_EnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true);
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
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006,2,1),myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006,3,1),myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 500, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(4.80m, feesEvent, 60, interestEvent, 200, principalEvent, 235.20m, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);
            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true);
            rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); 
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(8.40m, feesEvent, 120, interestEvent, 371.60m, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 171.60m, 30, 0);
            
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_CancelFees_InitialAmount_8DaysLate_EnoughMoneyToRepay()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, true, 0, 0);

            Assert.AreEqual(new DateTime(2006,2,1),myContract.GetInstallment(0).ExpectedDate);

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
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), false);
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
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), false);
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
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), false);
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
        public void RepayInstallment_DontCancelFees_KeepNotExpectedInstallment_OLB_36DaysLate_NoEnoughMoneyToRepay()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);

            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), false);
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
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), false);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            OCurrency amountPaid = 100, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); //8 days late
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(0.72m, feesEvent, 30, interestEvent, 0, principalEvent, 69.28m, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), false);
            rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 2, 9)); 
            rLI.RepayInstallments(new DateTime(2006, 2, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(1.35m, feesEvent, 60, interestEvent, 0, principalEvent, 38.65m, amountPaid);

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
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), false);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 33, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount); //= 3.24
            AssertRepayEvent(3.24m, feesEvent, 29.76m, interestEvent, 0, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 29.76m, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), false);
            rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); 
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount); //= 3.24
            AssertRepayEvent(3.24m, feesEvent, 29.76m, interestEvent, 0, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 2.70m);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 4.14m);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_KeepNotExpectedInstallment_OverDuePrincipal_8DaysLate()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false);
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

            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 500, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(4.80m, feesEvent, 60, interestEvent, 200, principalEvent, 235.20m, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false);
            rLI = _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); 
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(8.40m, feesEvent, 120, interestEvent, 371.60m, principalEvent, 0, amountPaid);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 171.60m, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void CalculateNewInstallmentsWithlateFees_CancelFees_ManualFeesAmountNotNull_InitialAmount_OLB_OverdueInterest_OverduePrincipal_36LateDays()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, true, 30, 0);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 0); //cancelFees
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0); //cancelFees
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_CancelFees_ManualFeesAmountNotNull_InitialAmount_OLB_OverdueInterest_OverduePrincipal_36LateDays()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, true, 30, 0);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 270, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(feesEvent, 0, interestEvent, 60, principalEvent, 200, amountPaid, 10); //fees not repay here

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_KeepNotExpectedInstallement_CancelInterest_ManualInterestLesserThanExpected_InitialAmount_OLB_OverdueInterest_OverduePrincipal_36LateDays()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);

            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), false);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = _SetRepaymentOptions(myContract, false, 0, 0, true, 20);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 480, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(feesEvent, 260.76m, interestEvent, 60, principalEvent, 159.24m, amountPaid, 0); //255.24 + 5.52

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30,   0,   0, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 159.24m, 30, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200,   0,  0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200,   0,  0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200,   0,  0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200,   0,  0, 0);

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), false);
            rLI = _SetRepaymentOptions(myContract, false, 0, 0, true, 20);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); 
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(feesEvent, 260.76m, interestEvent, 60, principalEvent, 159.24m, amountPaid, 0); 

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 230.7m);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 4.14m);
            
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_CancelInterest_ManualInterestLesserThanExpected_KeepExpectedInstallment_InitialAmount_OLB_OverdueInterest_OverduePrincipal_36LateDays()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);

            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = 
                _SetRepaymentOptions(myContract, false, 0, 0, true, 20);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 480, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(feesEvent, 260.76m, interestEvent, 0, principalEvent, 200, amountPaid, 19.24m); //255.24 + 5.52

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30,   0,   0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200,   0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200,   0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200,   0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200,   0, 0, 0);

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true);
            rLI = _SetRepaymentOptions(myContract, false, 0, 0, true, 20);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); 
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(feesEvent, 280, interestEvent, 0, principalEvent, 200, amountPaid, 0); 

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 211.46m);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200, 0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200, 0, 0, 0);
        }

        [Test]
        public void RepayInstallment_DontCancelFees_KeepExpectedInstallment_CancelInterest_ManualInterestTallerThanExpected_InitialAmount_OLB_OverdueInterest_OverduePrincipal_36LateDays()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true);
            OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI = 
                _SetRepaymentOptions(myContract, false, 0, 0, true, 70);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            OCurrency amountPaid = 480, interestEvent = 0, principalEvent = 0, feesEvent = 0;
            OCurrency commissionAmount = 0;
            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); //36 days late
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(feesEvent, 260.76m, interestEvent, 0, principalEvent, 200, amountPaid, 19.24m); //255.24 + 5.52

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30,   0,   0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200,   0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200,   0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200,   0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200,   0, 0, 0);

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0.004, 0.003, 0.003), true);
            rLI = _SetRepaymentOptions(myContract, false, 0, 0, true, 70);

            Assert.AreEqual(new DateTime(2006, 2, 1), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2006, 3, 1), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2006, 3, 9)); 
            rLI.RepayInstallments(new DateTime(2006, 3, 9), ref amountPaid, ref interestEvent, ref principalEvent, ref feesEvent, ref commissionAmount);
            AssertRepayEvent(feesEvent, 280.00m, interestEvent, 0, principalEvent, 200, amountPaid, 0);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 0, 0, 0, 211.46m);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 0, 0, 0);
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
            return _SetRepaymentOptions(pContract, pCancelFees, 0, 0, false,0);
        }

        private static OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments _SetRepaymentOptions(Loan pContract, bool pCancelFees, OCurrency pManualFeesAmount, OCurrency pManualCommissionAmount)
        {
            return _SetRepaymentOptions(pContract, pCancelFees, pManualFeesAmount, pManualCommissionAmount, false, 0);
        }

        private static OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments _SetRepaymentOptions(Loan pContract, bool pCancelFees, OCurrency pManualFeesAmount, OCurrency pManualCommissionAmount, bool pCancelInterest, OCurrency pManualInterestAmount)
        {
            var cCO = new CreditContractOptions(pContract.Product.LoanType, pContract.Product.KeepExpectedInstallment, pCancelFees, pManualFeesAmount, pManualCommissionAmount, 
                pCancelInterest, pManualInterestAmount, pContract.Product.AnticipatedTotalRepaymentPenaltiesBase);

            return new OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments(cCO, pContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));
        }

        private static Loan _SetContract(OLoanTypes pLoansType, NonRepaymentPenalties pNonRepaymentFees,bool pKeepExpectedInstallment)
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = pLoansType,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true }
            };
            package.KeepExpectedInstallment = pKeepExpectedInstallment;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = false;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;
            myContract.NonRepaymentPenalties = pNonRepaymentFees;
            return myContract;
        }

        private static void AssertSpecifiedInstallment(Installment pInstallment, OCurrency pInterestRepayment, OCurrency pCapitalRepayment, 
            OCurrency pPaidCapital, OCurrency pPaidInterest, OCurrency pFeesUnpaid)
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
