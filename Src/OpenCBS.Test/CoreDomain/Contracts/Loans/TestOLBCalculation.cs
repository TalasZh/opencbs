using System;
using System.Collections.Generic;
using NUnit.Framework;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Accounting;
using Octopus.CoreDomain.Contracts.Loans;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.CoreDomain.Products;
using Octopus.Enums;
using Octopus.Shared;
using Octopus.Shared.Settings;

namespace Octopus.Test.CoreDomain.Contracts.Loans
{
    [TestFixture]
    public class TestOLBCalculation
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            NonWorkingDateSingleton nonWorkingDateHelper = NonWorkingDateSingleton.GetInstance("");
            nonWorkingDateHelper.WeekEndDay1 = 6;
            nonWorkingDateHelper.WeekEndDay2 = 0;
            nonWorkingDateHelper.PublicHolidays = new Dictionary<DateTime, string>
                                                      {
                                                          {new DateTime(2006, 1, 1), "New Year Eve"},
                                                          {new DateTime(2006, 3, 8), "New Year Eve"},
                                                          {new DateTime(2006, 3, 21), "New Year Eve"},
                                                          {new DateTime(2006, 3, 22), "New Year Eve"},
                                                          {new DateTime(2006, 5, 1), "New Year Eve"},
                                                          {new DateTime(2006, 5, 9), "New Year Eve"},
                                                          {new DateTime(2006, 6, 27), "New Year Eve"},
                                                          {new DateTime(2006, 9, 9), "New Year Eve"},
                                                          {new DateTime(2006, 11, 6), "New Year Eve"},
                                                          {new DateTime(2006, 11, 26), "New Year Eve"},
                                                          {new DateTime(2006, 1, 6), "Christmas"}
                                                      };
            ApplicationSettings dataParam = ApplicationSettings.GetInstance("");
            dataParam.DeleteAllParameters();
            dataParam.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            dataParam.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
            dataParam.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
            dataParam.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            dataParam.AddParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
            dataParam.AddParameter(OGeneralSettings.STOP_WRITEOFF_PENALTY, true);
        }

        [Test]
        public void OLB_Flat_NoRepayment_OneMonthGracePeriod()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 6, 1, OLoanTypes.Flat);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(5, false).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(6, false).Value);
        }

        [Test]
        public void OLB_Flat_OneRepayment_GracePeriod_NoOverDue_KeepExpectedInstallment()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 6, 1, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 3, 1), 30, false, true);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, true).Value);
            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(2, true).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(3, true).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(4, true).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(5, true).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(6, true).Value);
        }

        [Test]
        public void OLB_Flat_OneRepayment_GracePeriod_OverDue_KeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 6, 1, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 3, 1), 530, false, false);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(500, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(300, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(5, false).Value);
            Assert.AreEqual(100, myContract.CalculateExpectedOlb(6, false).Value);
        }
        [Test]
        public void OLB_Flat_OneRepayment_NoGracePeriod_NoOverDue_KeepExpectedInstallment()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 5, 0, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 3, 1), 30, true, true);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, true).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(2, true).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(3, true).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(4, true).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(5, true).Value);
        }

        [Test]
        public void OLB_Flat_TwoRepayments_NoGracePeriod_NoOverDue_KeepExpectedInstallment()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 5, 0, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 3, 1), 230, true, true);
            myContract.Repay(2, new DateTime(2006, 4, 1), 230, true, true);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, true).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(2, true).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(3, true).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(4, true).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(5, true).Value);
        }

        [Test]
        public void OLB_Flat_TwoRepayments_NoGracePeriod_NoOverDue_KeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 5, 0, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 3, 1), 230, true, false);
            myContract.Repay(2, new DateTime(2006, 4, 3), 530, true, false);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(300, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(100, myContract.CalculateExpectedOlb(5, false).Value);
        }

        [Test]
        public void OLB_Flat_OneRepayment_NoGracePeriod_NoOverDue_KeepNotExpectedInstallment()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 5, 0, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 3, 1), 30, true, false);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(5, false).Value);
        }

        [Test]
        public void OLB_Flat_OneRepayment_NoGracePeriod_OverDue_KeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 5, 0, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 3, 1), 630, true, false);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(300, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(100, myContract.CalculateExpectedOlb(5, false).Value);
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);

        }

        [Test]
        public void OLB_Flat_OneRepayment_NoGracePeriod_OverDue_KeepExpectedInstallment()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 5, 0, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 3, 1), 530, true, true);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, true).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(2, true).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(3, true).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(4, true).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(5, true).Value);
        }

        [Test]
        public void OLB_Flat_OneRepaymentBeforeDueDate_NoGracePeriod_NoOverDue_KeepExpectedInstallment()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 5, 0, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 2, 24), 500, true, true);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, true).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(2, true).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(3, true).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(4, true).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(5, true).Value);
        }

        [Test]
        public void OLB_Flat_TwosRepayments_SecondBeforeDueDate_NoGracePeriod_NoOverDue_KeepExpectedInstallment()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 5, 0, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 3, 1), 230, true, true);
            myContract.Repay(2, new DateTime(2006, 4, 1), 230, true, true);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, true).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(2, true).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(3, true).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(4, true).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(5, true).Value);
        }

        [Test]
        public void OLB_Flat_TwosRepayments_SecondBeforeDueDate_NoGracePeriod_NoOverDue_KeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 5, 0, OLoanTypes.Flat);
            
            myContract.Repay(1, new DateTime(2006, 3, 1), 230, true, false);
            myContract.Repay(2, new DateTime(2006, 4, 1), 320, true, false);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(508.18m, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(381.13m, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(254.08m, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(127.03m, myContract.CalculateExpectedOlb(5, false).Value);
        }

        [Test]
        public void OLB_Flat_OneRepaymentBeforeDueDate_NoGracePeriod_NoOverDue_KeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            Loan myContract = _GetLoan(new DateTime(2010, 1, 1), 5, 0, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2010, 1, 26), 500, true, false);

            Assert.AreEqual(524.19m, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(419.35m, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(314.51m, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(209.67m, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(104.83m, myContract.CalculateExpectedOlb(5, false).Value);
        }

        [Test]
        public void OLB_Flat_OneRepayment_GracePeriod_OverDue_KeepExpectedInstallment()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 6, 1, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 3, 1), 500, true, true);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, true).Value);
            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(2, true).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(3, true).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(4, true).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(5, true).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(6, true).Value);
        }

        [Test]
        public void OLB_Flat_TotalRepayment_GracePeriod_KeepNotExpectedInstallment()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 1, 1), 6, 1, OLoanTypes.Flat);
            myContract.Repay(1, new DateTime(2006, 2, 1), 30, true, false);
            myContract.Repay(2, new DateTime(2006, 3, 1), 230, true, false);
            myContract.Repay(3, new DateTime(2006, 3, 20), 920, true, true);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(5, false).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(6, false).Value);
        }

        [Test]
        public void OLB_Flat_NoRepayment_TwoMonthsGracePeriod()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 2, 1), 7, 2, OLoanTypes.Flat);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(5, false).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(6, false).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(7, false).Value);
        }

        [Test]
        public void OLB_Flat_NoRepayment_NoGracePeriod()
        {
            Loan myContract = _GetLoan(new DateTime(2006, 2, 1),5, 0, OLoanTypes.Flat);

            Assert.AreEqual(1000, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(800, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(600, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(400, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(200, myContract.CalculateExpectedOlb(5, false).Value);
        }

        [Test]
        public void OLB_DecliningFixedInstallments_OneRepayment_OneMonthGracePeriod_Overdue_KeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true}
            };

            Loan myContract = new Loan(package, 750, 0.03m, 9, 1, new DateTime(2006, 2, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.Repay(1, new DateTime(2006, 3, 1), 740, false, false);

            Assert.AreEqual(750, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(32.5m, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(28.85m, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(25.09m, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(21.21m, myContract.CalculateExpectedOlb(5, false).Value);
            Assert.AreEqual(17.22m, myContract.CalculateExpectedOlb(6, false).Value);
            Assert.AreEqual(13.11m, myContract.CalculateExpectedOlb(7, false).Value);
            Assert.AreEqual(8.87m, myContract.CalculateExpectedOlb(8, false).Value);
            Assert.AreEqual(4.51m, myContract.CalculateExpectedOlb(9, false).Value);
        }

        [Test]
        public void OLB_DecliningFixedInstallments_FewsLateRepayments_NoGracePeriod_KeepNotExpectedInstallment()
        {
            Loan myContract = _GetLoan(new DateTime(2010, 1, 1), 12, 0, OLoanTypes.DecliningFixedInstallments);
            myContract.NonRepaymentPenalties.OverDueInterest = 0.01;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.01;
            myContract.AnticipatedTotalRepaymentPenalties = 0;

            myContract.Repay(1, new DateTime(2010, 3, 1), 114m, false, false);
            myContract.Repay(2, new DateTime(2010, 5, 2), 133m, false, false);
            myContract.Repay(3, new DateTime(2010, 6, 1), 50, true, false);
            myContract.Repay(3, new DateTime(2010, 7, 3), 81m, false, false);
            myContract.Repay(4, new DateTime(2010, 8, 1), 520m, false, true);

            Assert.AreEqual(1000m, myContract.GetInstallment(0).OLB.Value);
            Assert.AreEqual(625.93m, myContract.GetInstallment(5).OLB.Value);
        }

        [Test]
        public void CalculateExpectedOLB2()
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
            Loan myContract = new Loan(package, 20000, 0.02m, 10, 0, new DateTime(2009, 1, 17), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = false;

            myContract.Repay(1, new DateTime(2009, 1, 17), 1000, true, false);

            Assert.AreEqual(19000, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(17100, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(15200, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(13300, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(11400, myContract.CalculateExpectedOlb(5, false).Value);
            Assert.AreEqual(9500, myContract.CalculateExpectedOlb(6, false).Value);
            Assert.AreEqual(7600, myContract.CalculateExpectedOlb(7, false).Value);
            Assert.AreEqual(5700, myContract.CalculateExpectedOlb(8, false).Value);
            Assert.AreEqual(3800, myContract.CalculateExpectedOlb(9, false).Value);
            Assert.AreEqual(1900, myContract.CalculateExpectedOlb(10, false).Value);
        }

        [Test]
        public void TestRepayFlat_GracePeriod_KeepNotExpectedInstallments()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);

            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true }
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 50000, 0.01m, 8, 2, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = false;

            myContract.Repay(1, new DateTime(2006, 2, 1), 1050, true, true); 
            myContract.Repay(2, new DateTime(2006, 6, 1), 40000, true, true); 

            Assert.AreEqual(50000, myContract.CalculateExpectedOlb(1, false).Value);
            Assert.AreEqual(50000, myContract.CalculateExpectedOlb(2, false).Value);
            Assert.AreEqual(50000m, myContract.CalculateExpectedOlb(3, false).Value);
            Assert.AreEqual(41666.67m, myContract.CalculateExpectedOlb(4, false).Value);
            Assert.AreEqual(33333.34m, myContract.CalculateExpectedOlb(5, false).Value);
            //Assert.AreEqual(11428m, myContract.CalculateExpectedOLB(6, false).Value);
            //Assert.AreEqual(7618.67m, myContract.CalculateExpectedOLB(7, false).Value);
            //Assert.AreEqual(3809.34m, myContract.CalculateExpectedOLB(8, false).Value);
        }

        private static Loan _GetLoan(DateTime pDisburseDate, int pNumberOfInstallments, int pGracePeriod, OLoanTypes pLoanTypes)
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = pLoanTypes,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true}
            };
            return new Loan(package, 1000, 0.03m, pNumberOfInstallments, pGracePeriod, pDisburseDate, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
        }
    }
}
