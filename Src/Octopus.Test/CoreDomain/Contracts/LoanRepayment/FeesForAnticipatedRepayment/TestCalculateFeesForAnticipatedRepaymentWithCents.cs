using System;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Contracts.Loans;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.CoreDomain.Contracts.Loans.LoanRepayment;
using Octopus.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment;
using Octopus.CoreDomain.Products;
using Octopus.Enums;
using NUnit.Framework;
using Octopus.Shared;
using Octopus.CoreDomain.Accounting;
using Octopus.Shared.Settings;

namespace Octopus.Test.CoreDomain.Contracts.LoanRepayment.FeesForAnticipatedRepayment
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