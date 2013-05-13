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
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using NUnit.Framework;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain.Contracts.LoanRepayment.RepayNextInstallments
{
    /// <summary>
    /// Summary description for TestRepayNextInstallments.
    /// </summary>
    [TestFixture]
    public class TestRepayNextInstallmentsWithCents
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            ApplicationSettings generalParameters = ApplicationSettings.GetInstance("");
            generalParameters.DeleteAllParameters();
            generalParameters.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalParameters.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
            generalParameters.AddParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
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
        public void RepayNextInstallment_Flat_DontManualInterest_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, true, 1, 6, null);
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract,false,0);

            OCurrency amountPaid = 200, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;

            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 60, principalEvent, 140, amountPaid, 0, interestPrepayment, 60);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30,   0,   0, 30);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 140, 30);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200,   0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200,   0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200,   0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200,   0,  0);
        }

        [Test]
        public void RepayNextInstallment_Flat_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, false, 1, 6, null);
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, false, 0);

            OCurrency amountPaid = 680, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 0, principalEvent, 680, amountPaid, 0, interestPrepayment, 0);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 9.6m, 0, 320, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 9.6m, 64,320, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 9.6m, 64,256, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 9.6m, 64,192, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 9.6m, 64,128, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 9.6m, 64, 64, 0, 0);
        }

        [Test]
        public void RepayNextInstallment_Flat_ManualInterest_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, true, 1, 6, null);
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, true, 40);

            OCurrency amountPaid = 200, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 0, principalEvent, 200, amountPaid, 0, interestPrepayment, 0);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30,   0,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30, 200, 200, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 30, 200,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 30, 200,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 30, 200,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 30, 200,   0, 0);
        }

        [Test]
        public void RepayNextInstallment_ExoticFlat_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, false, 0, 6, _SetExotic(false));
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, false, 0);

            OCurrency amountPaid = 680, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 180, principalEvent, 500, amountPaid, 0, interestPrepayment, 180);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 0, 50, 0, 0); //repay interest first
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 0,100, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 0, 50, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 0,  0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 0,200, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 0,100, 0, 0);
        }

        [Test]
        public void RepayNextInstallment_ExoticFlat_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, true, 0, 6, _SetExotic(false));
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, false, 0);

            OCurrency amountPaid = 355, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 54, principalEvent, 301, amountPaid, 0, interestPrepayment, 54);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 18, 100, 100, 18); //repay interest first
            AssertSpecifiedInstallment(myContract.GetInstallment(1),  0, 200, 200,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 36, 100,   1, 36);
            AssertSpecifiedInstallment(myContract.GetInstallment(3),  0,   0,   0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 90, 400,   0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 36, 200,   0,  0);
        }

        [Test]
        public void RepayNextInstallment_ExoticFlat_ManualFees_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, true, 0, 6, _SetExotic(false));
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, true, 20);

            OCurrency amountPaid = 301, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 0, principalEvent, 301, amountPaid, 0, interestPrepayment, 0);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 18, 100, 100, 0); 
            AssertSpecifiedInstallment(myContract.GetInstallment(1),  0, 200, 200, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 36, 100,   1, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3),  0,   0,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 90, 400,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 36, 200,   0, 0);
        }

        [Test]
        public void RepayNextInstallment_Declining_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, false, 0, 7, null);
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, false, 0);

            OCurrency amountPaid = 342, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 0, principalEvent, 342, amountPaid, 0, interestPrepayment, 0);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 19.74m,  85.87m, 658    , 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 17.16m,  88.45m, 572.13m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 14.51m,  91.10m, 483.68m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 11.78m,  93.84m, 392.58m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4),  8.96m,  96.65m, 298.74m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5),  6.06m,  99.55m, 202.09m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(6),  3.08m, 102.54m, 102.54m, 0, 0);
        }

        [Test]
        public void RepayNextInstallment_Declining_ManualInterest_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, false, 0, 7, null);
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, true, 30);

            OCurrency amountPaid = 342, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 0, principalEvent, 342, amountPaid, 0, interestPrepayment, 0);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 19.74m, 85.87m, 658, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 17.16m, 88.45m, 572.13m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 14.51m, 91.10m, 483.68m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 11.78m, 93.84m, 392.58m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 8.96m, 96.65m, 298.74m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 6.06m, 99.55m, 202.09m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(6), 3.08m, 102.54m, 102.54m, 0, 0);
        }

        [Test]
        public void RepayNextInstallment_Declining_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, true, 1, 7, null);
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, false, 0);

            OCurrency amountPaid = 342, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent.Value, 85.36m, principalEvent.Value, 256.64m, amountPaid.Value, 0, interestPrepayment.Value, 85.36m);

            Assert.AreEqual(1000, _CheckInstalmentsTotalPrincipal(myContract).Value);

            AssertSpecifiedInstallment(myContract.GetInstallment(0),     30,       0,1000   ,       0,     30);
            AssertSpecifiedInstallment(myContract.GetInstallment(1),     30, 154.60m,1000   , 154.60m,     30);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 25.36m, 159.24m,845.40m, 102.04m, 25.36m);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 20.58m, 164.02m,686.16m,       0,      0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 15.66m, 168.93m,522.14m,       0,      0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 10.60m, 173.99m,353.21m,       0,      0);
            AssertSpecifiedInstallment(myContract.GetInstallment(6),  5.38m, 179.22m,179.22m,       0,      0);
        }

        [Test]
        public void RepayNextInstallment_Declining_ManualInterest_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, true, 1, 7, null);
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, true, 40);

            OCurrency amountPaid = 256.64m;
            OCurrency interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 0, principalEvent.Value, 256.64m, amountPaid.Value, 0, interestPrepayment.Value, 0);


            Assert.AreEqual(1000, _CheckInstalmentsTotalPrincipal(myContract).Value);
            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30    , 0      , 1000   , 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 30    , 154.60m, 1000   , 154.60m, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 25.36m, 159.24m, 845.40m, 102.04m, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 20.58m, 164.02m, 686.16m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 15.66m, 168.93m, 522.14m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 10.60m, 173.99m, 353.21m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(6),  5.38m, 179.22m, 179.22m, 0, 0);
        }

        [Test]
        public void RepayNextInstallment_ExoticDeclining_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, true, 0, 6, _SetExotic(true));
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, false, 0);

            OCurrency amountPaid = 355, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 57, principalEvent, 298, amountPaid, 0, interestPrepayment, 57);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 100, 100, 30);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 27, 200, 198, 27);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 21, 400,   0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3),  9,   0,   0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4),  9, 200,   0,  0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5),  3, 100,   0,  0);
        }

        [Test]
        public void RepayNextInstallment_ExoticDeclining_ManualInterest_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, true, 0, 6, _SetExotic(true));
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, true, 40);

            OCurrency amountPaid = 298, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 0, principalEvent, 298, amountPaid, 0, interestPrepayment, 0);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 30, 100, 100, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 27, 200, 198, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 21, 400,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3),  9,   0,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4),  9, 200,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5),  3, 100,   0, 0);
        }

        [Test]
        public void RepayNextInstallment_ExoticDeclining_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, false, 0, 6, _SetExotic(true));
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, false, 0);

            OCurrency amountPaid = 355, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 0, principalEvent, 355, amountPaid, 0, interestPrepayment, 0);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 19.35m, 64.5m, 645   , 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 17.42m, 129  , 580.5m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 13.54m, 258  , 451.5m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3),  5.80m,   0  , 193.5m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4),  5.80m, 129  , 193.5m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5),  1.94m, 64.5m,  64.5m, 0, 0);
        }

        [Test]
        public void RepayNextInstallment_ExoticDeclining_ManualInterest_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, false, 0, 6, _SetExotic(true));
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, true, 40);

            OCurrency amountPaid = 298, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
            AssertRepayEvent(interestEvent, 0, principalEvent, 298, amountPaid, 0, interestPrepayment, 0);

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 21.06m,  70.2m,702, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 18.95m, 140.4m,631.8m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 14.74m, 280.8m,491.4m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3),  6.32m,      0,210.6m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4),  6.32m, 140.4m,210.6m, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5),  2.11m,  70.2m,70.2m, 0, 0);
        }

        #region private methods

        private static OCurrency _CheckInstalmentsTotalPrincipal(Loan pLoan)
        {
            OCurrency amount = 0;
            foreach (Installment installment in pLoan.InstallmentList)
            {
                amount += installment.CapitalRepayment;
            }
            return amount;
        }
        private static ExoticInstallmentsTable _SetExotic(bool pForDecliningLoan)
        {
            ExoticInstallmentsTable exoticProduct = new ExoticInstallmentsTable();
            if (!pForDecliningLoan)
            {
                exoticProduct.Add(new ExoticInstallment(1, 0.1, 0.1));
                exoticProduct.Add(new ExoticInstallment(2, 0.2, 0));
                exoticProduct.Add(new ExoticInstallment(3, 0.1, 0.2));
                exoticProduct.Add(new ExoticInstallment(4, 0, 0));
                exoticProduct.Add(new ExoticInstallment(5, 0.4, 0.5));
                exoticProduct.Add(new ExoticInstallment(6, 0.2, 0.2));
            }
            else
            {
                exoticProduct.Add(new ExoticInstallment(1, 0.1, null));
                exoticProduct.Add(new ExoticInstallment(2, 0.2, null));
                exoticProduct.Add(new ExoticInstallment(3, 0.4, null));
                exoticProduct.Add(new ExoticInstallment(4, 0, null));
                exoticProduct.Add(new ExoticInstallment(5, 0.2, null));
                exoticProduct.Add(new ExoticInstallment(6, 0.1, null));
            }
            return exoticProduct;
        }

        private static void AssertRepayEvent(OCurrency pActualInterest, OCurrency pExpectedInterest,
                                             OCurrency pActualPrincipal, OCurrency pExpectedPrincipal,
                                             OCurrency pActualAmountPaid, OCurrency pExpectedAmountPaid, OCurrency pActualInterestPrepayment, OCurrency pExpectedInterestPrepayment)
        {
            Assert.AreEqual(pExpectedInterest.Value, pActualInterest.Value);
            Assert.AreEqual(pExpectedPrincipal.Value, pActualPrincipal.Value);
            Assert.AreEqual(pExpectedAmountPaid.Value, pActualAmountPaid.Value);
            Assert.AreEqual(pExpectedInterestPrepayment.Value, pActualInterestPrepayment.Value);
        }

        private static void AssertSpecifiedInstallment(Installment pInstallment, OCurrency pInterestRepayment, OCurrency pCapitalRepayment,OCurrency pOLB, OCurrency pPaidCapital, OCurrency pPaidInterest)
        {
            Assert.AreEqual(pInterestRepayment.Value, pInstallment.InterestsRepayment.Value);
            Assert.AreEqual(pCapitalRepayment.Value, pInstallment.CapitalRepayment.Value);
            Assert.AreEqual(pOLB.Value, pInstallment.OLB.Value);
            Assert.AreEqual(pPaidCapital.Value, pInstallment.PaidCapital.Value);
            Assert.AreEqual(pPaidInterest.Value, pInstallment.PaidInterests.Value);
        }

        private static void AssertSpecifiedInstallment(Installment pInstallment, OCurrency pInterestRepayment, OCurrency pCapitalRepayment, OCurrency pPaidCapital, OCurrency pPaidInterest)
        {
            Assert.AreEqual(pInterestRepayment.Value, pInstallment.InterestsRepayment.Value);
            Assert.AreEqual(pCapitalRepayment.Value, pInstallment.CapitalRepayment.Value);
            Assert.AreEqual(pPaidCapital.Value, pInstallment.PaidCapital.Value);
            Assert.AreEqual(pPaidInterest.Value, pInstallment.PaidInterests.Value);
        }

        private static RepayNextInstallmentsStrategy _SetRepaymentOptions(Loan pContract, bool pCancelInterest, OCurrency pManualInterestAmount)
        {
            CreditContractOptions cCO = new CreditContractOptions(pContract.Product.LoanType, pContract.Product.KeepExpectedInstallment, true, 0, 0, pCancelInterest, pManualInterestAmount,
                                                                  pContract.Product.AnticipatedTotalRepaymentPenaltiesBase);

            return new RepayNextInstallmentsStrategy(pContract, cCO, new User(), ApplicationSettings.GetInstance(""));
        }

        private static Loan _SetContract(OLoanTypes pLoansType, bool pKeepExpectedInstallment, int pGracePeriod, int pMaturity, ExoticInstallmentsTable pExoticProduct)
        {
            LoanProduct package = new LoanProduct
                                      {
                                          InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                                          LoanType = pLoansType,
                                          ChargeInterestWithinGracePeriod = true,
                                          KeepExpectedInstallment = pKeepExpectedInstallment,
                                          ExoticProduct = pExoticProduct,
                                          AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB,
                                          Currency = new Currency { Id = 1, UseCents = true},
                                          RoundingType = ORoundingType.Approximate
                                      };
            return new Loan(package, 1000, 0.03m, pMaturity, pGracePeriod, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()))
                                  {
                                      BadLoan = false,
                                      AnticipatedTotalRepaymentPenalties = 0.01,
                                      NonRepaymentPenalties = {InitialAmount = 0.003}
                                  };
        }
        #endregion
    }
}
