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
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using NUnit.Framework;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain.Contracts.LoanRepayment.FeesForAnticipatedRepayment
{
    /// <summary>
    /// Summary description for TestCalculateFeesForAnticipatedRepayment.
    /// </summary>
    [TestFixture]
    public class TestCalculateFeesForAnticipatedRepaymentWithCents
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
        public void CalculateAnticipatedFees_CancelFees()
        {
            Loan myContract = _SetContract(OAnticipatedRepaymentPenaltiesBases.RemainingOLB, 0.01, true, false);
            CalculateAnticipatedFeesStrategy _feesStrategy = _SetFeesCalculationOptions(myContract, true);

            OCurrency pAmountPaid = 100, pFeesEvent = 0;
            _feesStrategy.calculateFees(new DateTime(2006, 2, 1), ref pAmountPaid, ref pFeesEvent);
            AssertFeesAmount(pFeesEvent,0,pAmountPaid,100);
        }

        [Test]
        public void CalculateAnticipatedFees_DontCancelFees_KeepExpectedInstallment_NoOverPaid()
        {
            Loan myContract = _SetContract(OAnticipatedRepaymentPenaltiesBases.RemainingOLB, 0.01, true, false);
            CalculateAnticipatedFeesStrategy _feesStrategy = _SetFeesCalculationOptions(myContract, false);

            OCurrency pAmountPaid = 30, pFeesEvent = 0;
            _feesStrategy.calculateFees(new DateTime(2006, 2, 1), ref pAmountPaid, ref pFeesEvent);
            AssertFeesAmount(pFeesEvent, 0, pAmountPaid, 30);
        }

        [Test]
        public void CalculateAnticipatedFees_DontCancelFees_KeepExpectedInstallment_RemainingInterest_OverPaid()
        {
            Loan myContract = _SetContract(OAnticipatedRepaymentPenaltiesBases.RemainingInterest, 0.01, true, false);
            CalculateAnticipatedFeesStrategy _feesStrategy = _SetFeesCalculationOptions(myContract, false);

            OCurrency pAmountPaid = 300, pFeesEvent = 0;
            _feesStrategy.calculateFees(new DateTime(2006, 2, 1), ref pAmountPaid, ref pFeesEvent);
            AssertFeesAmount(pFeesEvent, 0, pAmountPaid, 300);
        }

        [Test]
        public void CalculateAnticipatedFees_DontCancelFees_KeepNotExpectedInstallment_OverPaid()
        {
            Loan myContract = _SetContract(OAnticipatedRepaymentPenaltiesBases.RemainingOLB, 0.01, false, false);
            CalculateAnticipatedFeesStrategy _feesStrategy = _SetFeesCalculationOptions(myContract, false);

            OCurrency pAmountPaid = 300, pFeesEvent = 0;
            _feesStrategy.calculateFees(new DateTime(2006, 2, 1), ref pAmountPaid, ref pFeesEvent);
            AssertFeesAmount(pFeesEvent, 10, pAmountPaid, 290);
        }

        [Test]
        public void CalculateAnticipatedFees_DontCancelFees_KeepNotExpectedInstallment_RemainingInterest_OverPaid()
        {
            Loan myContract = _SetContract(OAnticipatedRepaymentPenaltiesBases.RemainingInterest, 0.01, false, true);
            CalculateAnticipatedFeesStrategy feesStrategy = _SetFeesCalculationOptions(myContract, false);

            OCurrency amountPaid = 291.5m, feesEvent = 0;
            feesStrategy.calculateFees(new DateTime(2006, 2, 1), ref amountPaid, ref feesEvent);
            AssertFeesAmount(feesEvent, 1.5m, amountPaid, 290);
        }

        #region private methods
        private static Loan _SetContract(OAnticipatedRepaymentPenaltiesBases pAnticipatedRepaymentBase, double pAnticipated, bool pKeepExpectedInstallment, bool useCents)
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                KeepExpectedInstallment = pKeepExpectedInstallment,
                AnticipatedTotalRepaymentPenaltiesBase = pAnticipatedRepaymentBase,
                Currency = new Currency { Id = 1, UseCents = useCents }
            };

            return new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()))
                                  {
                                      BadLoan = false,
                                      AnticipatedTotalRepaymentPenalties = pAnticipated,
                                      NonRepaymentPenalties = {InitialAmount = 0.003}
                                  };
        }

        private static CalculateAnticipatedFeesStrategy _SetFeesCalculationOptions(Loan pContract, bool pCancelFees)
        {
            CreditContractOptions cCO = new CreditContractOptions(pContract.Product.LoanType, pContract.Product.KeepExpectedInstallment, pCancelFees, 0, 0, false, 0, pContract.Product.AnticipatedTotalRepaymentPenaltiesBase);
            return new CalculateAnticipatedFeesStrategy(cCO, pContract, ApplicationSettings.GetInstance(""));
        }
        private static void AssertFeesAmount(OCurrency pActualFees, OCurrency pExpectedFees, OCurrency pActualAmountPaid, OCurrency pExpectedAmountPaid)
        {
            Assert.AreEqual(pExpectedFees.Value, pActualFees.Value);
            Assert.AreEqual(pExpectedAmountPaid.Value, pActualAmountPaid.Value);
        }
        #endregion
    }
}
