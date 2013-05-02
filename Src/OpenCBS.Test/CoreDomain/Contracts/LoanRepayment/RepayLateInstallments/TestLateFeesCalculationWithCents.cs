// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using NUnit.Framework;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain.Contracts.LoanRepayment.RepayLateInstallments
{
    /// <summary>
    /// Summary description for TestLateFeesCalculationWithCents.
    /// </summary>
    [TestFixture]
    public class TestLateFeesCalculationWithCents
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

        private static Loan _SetContract(OLoanTypes pLoansType, NonRepaymentPenalties pNonRepaymentFees, bool pKeepExpectedInstallment, bool useCents)
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = pLoansType,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = useCents }
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
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true, false);
            Assert.AreEqual(12m, CalculationBaseForLateFees.FeesBasedOnInitialAmount(myContract, new DateTime(2006, 2, 5), 1, false, 
                ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance("")).Value); // 1000 * 0.003 * 4 = 12
        }

        [Test]
        public void LateFeesCalculation_OLB()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), true, false);
            Assert.AreEqual(12m, CalculationBaseForLateFees.FeesBasedOnOlb(myContract, new DateTime(2006, 2, 5), 1, false, 
                ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance("")).Value); // 1000 * 0.003 * 4 = 12
        }

        [Test]
        public void LateFeesCalculation_OverdueInterest()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0, 0.003), true, true);

            Assert.AreEqual(2.52m, CalculationBaseForLateFees.FeesBasedOnOverdueInterest(myContract, new DateTime(2006, 3, 5), 1, false, ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance("")).Value); // 30 * 0.003 * 28 = 2.88m
            
            //CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0, 0.003), true, true);
            Assert.AreEqual(2.88m, CalculationBaseForLateFees.FeesBasedOnOverdueInterest(myContract, new DateTime(2006, 3, 5), 1, false, ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance("")).Value); // 30 * 0.003 * 32 = 2.88m
        }

        [Test]
        public void LateFeesCalculation_OverduePrincipal()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true, false);
            Assert.AreEqual(0m, CalculationBaseForLateFees.FeesBasedOnOverdueInterest(myContract, new DateTime(2006, 3, 5), 1, false, ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance("")).Value);//0 (1st installment without interest) * 0.003 * 32 = 0
        }
    }
}
