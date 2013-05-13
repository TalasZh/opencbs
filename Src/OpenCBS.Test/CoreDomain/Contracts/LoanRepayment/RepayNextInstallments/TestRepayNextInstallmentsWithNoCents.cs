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
    public class TestRepayNextInstallmentsWithNoCents
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
        public void RepayNextInstallment_Flat_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat,true,1,6,null);
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, false, 0);

            OCurrency amountPaid = 200, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent,ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);
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

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 10, 0, 320, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 10, 64, 320, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 10, 64, 256, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 10, 64, 192, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 10, 64, 128, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 10, 64, 64, 0, 0);
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

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 0,  50, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(1), 0, 100, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 0,  50, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 0,   0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 0, 200, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 0, 100, 0, 0);
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

            AssertSpecifiedInstallment(myContract.GetInstallment(0), 18, 100, 100, 18);
            AssertSpecifiedInstallment(myContract.GetInstallment(1),  0, 200, 200, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 36, 100, 1, 36);
            AssertSpecifiedInstallment(myContract.GetInstallment(3),  0,   0, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 90, 400, 0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 36, 200, 0, 0);
        }

        [Test]
        public void RepayNextInstallment_Declining_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, false, 1, 7, null);
            RepayNextInstallmentsStrategy rNI = _SetRepaymentOptions(myContract, false, 0);

            OCurrency amountPaid = 342, interestEvent = 0, principalEvent = 0, interestPrepayment = 0;
            OCurrency penaltyEvent = 0;
            OCurrency commissionEvent = 0;
            rNI.RepayNextInstallments(ref amountPaid, ref interestEvent, ref interestPrepayment, ref principalEvent, ref penaltyEvent, ref commissionEvent);

            AssertRepayEvent(interestEvent, 0, principalEvent, 342, amountPaid, 0, interestPrepayment, 0);
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
            AssertSpecifiedInstallment(myContract.GetInstallment(2), 21, 400,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(3), 9,    0,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(4), 9,  200,   0, 0);
            AssertSpecifiedInstallment(myContract.GetInstallment(5), 3,  100,   0, 0);
        }

        #region private methods
        private static void AssertRepayEvent(OCurrency pActualInterest, OCurrency pExpectedInterest,
                                             OCurrency pActualPrincipal, OCurrency pExpectedPrincipal,
                                             OCurrency pActualAmountPaid, OCurrency pExpectedAmountPaid, OCurrency pActualInterestPrepayment, OCurrency pExpectedInterestPrepayment)
        {
            Assert.AreEqual(pExpectedInterest.Value, pActualInterest.Value);
            Assert.AreEqual(pExpectedPrincipal.Value, pActualPrincipal.Value);
            Assert.AreEqual(pExpectedAmountPaid.Value, pActualAmountPaid.Value);
            Assert.AreEqual(pExpectedInterestPrepayment.Value, pActualInterestPrepayment.Value);
        }

        private static void AssertSpecifiedInstallment(Installment pInstallment, OCurrency pInterestRepayment, OCurrency pCapitalRepayment, OCurrency pPaidCapital, OCurrency pPaidInterest)
        {
            Assert.AreEqual(pInterestRepayment.Value, pInstallment.InterestsRepayment.Value);
            Assert.AreEqual(pCapitalRepayment.Value, pInstallment.CapitalRepayment.Value);
            Assert.AreEqual(pPaidCapital.Value, pInstallment.PaidCapital.Value);
            Assert.AreEqual(pPaidInterest.Value, pInstallment.PaidInterests.Value);
        }

        private static void AssertSpecifiedInstallment(Installment pInstallment, OCurrency pInterestRepayment, OCurrency pCapitalRepayment, OCurrency pOLB, OCurrency pPaidCapital, OCurrency pPaidInterest)
        {
            Assert.AreEqual(pInterestRepayment.Value, pInstallment.InterestsRepayment.Value);
            Assert.AreEqual(pCapitalRepayment.Value, pInstallment.CapitalRepayment.Value);
            Assert.AreEqual(pOLB.Value, pInstallment.OLB.Value);
            Assert.AreEqual(pPaidCapital.Value, pInstallment.PaidCapital.Value);
            Assert.AreEqual(pPaidInterest.Value, pInstallment.PaidInterests.Value);
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
                                          Currency = new Currency { Id = 1 }
                                      };
            Loan myContract = new Loan(package, 1000, 0.03m, pMaturity, pGracePeriod, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = false;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;
            myContract.NonRepaymentPenalties.InitialAmount = 0.003;
            return myContract;
        }
        private static RepayNextInstallmentsStrategy _SetRepaymentOptions(Loan pContract, bool pCancelInterest, OCurrency pManualInterestAmount)
        {
            CreditContractOptions cCO = new CreditContractOptions(pContract.Product.LoanType, pContract.Product.KeepExpectedInstallment, true, 0, 0, pCancelInterest, pManualInterestAmount,
                                                                  pContract.Product.AnticipatedTotalRepaymentPenaltiesBase);

            return new RepayNextInstallmentsStrategy(pContract, cCO, new User(), ApplicationSettings.GetInstance(""));
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
        #endregion
    }
}
