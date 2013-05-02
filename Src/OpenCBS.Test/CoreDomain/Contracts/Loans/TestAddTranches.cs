// LICENSE PLACEHOLDER

using System;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.Tranches;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain.Contracts.Loans
{
    [TestFixture]
    public class TestAddTranches
    {
        private User _user;
        private ProvisionTable _provisions;
        private NonWorkingDateSingleton _daysOff;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _user = new User();
            _provisions = ProvisionTable.GetInstance(_user);
            _provisions.Add(new ProvisioningRate { Number = 1, NbOfDaysMin = 0, NbOfDaysMax = 30, Rate = 10 });
            _provisions.Add(new ProvisioningRate { Number = 2, NbOfDaysMin = 31, NbOfDaysMax = 60, Rate = 25 });
            _provisions.Add(new ProvisioningRate { Number = 3, NbOfDaysMin = 61, NbOfDaysMax = 90, Rate = 50 });
            _provisions.Add(new ProvisioningRate { Number = 4, NbOfDaysMin = 91, NbOfDaysMax = 180, Rate = 75 });
            _provisions.Add(new ProvisioningRate { Number = 5, NbOfDaysMin = 181, NbOfDaysMax = 365, Rate = 100 });
            _provisions.Add(new ProvisioningRate { Number = 6, NbOfDaysMin = 366, NbOfDaysMax = 99999, Rate = 100 });
            _daysOff = NonWorkingDateSingleton.GetInstance("");
            _daysOff.WeekEndDay1 = 6;
            _daysOff.WeekEndDay2 = 0;
            ApplicationSettings settings = ApplicationSettings.GetInstance("");
            settings.DeleteAllParameters();
            settings.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            settings.AddParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, false);
            settings.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
            settings.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            settings.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
        }

        private static Loan _GetContract_6Month_Flat()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };
            Loan retval = new Loan(package, 1000, 0.02m, 6, 1, new DateTime(2006, 1, 3), new User()
                , ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            return retval;
        }

        private static LoanProduct _GetFlatProduct()
        {
            return new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = false }
            };
        }

        private static LoanProduct _GetFlatProduct_WithCents()
        {
            return new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true }
            };
        }

        private static LoanProduct _GetFixedPrincipalProduct()
        {
            return new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedPrincipal,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = false }
            };
        }

        private static LoanProduct _GetFixedPrincipalProduct_WithCents()
        {
            return new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedPrincipal,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true }
            };
        }

        private static LoanProduct _GetAnnuityProduct()
        {
            return new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = false },
            };
        }

        private static LoanProduct _GetAnnuityProduct_WithCents()
        {
            return new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true },
            };
        }

        private static LoanProduct _GetAnnuityProduct_RoundingTypeBegin()
        {
            return new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = false },
                RoundingType = ORoundingType.Begin
            };
        }

        private static LoanProduct _GetAnnuityProduct_RoundingTypeBegin_WithCents()
        {
            return new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true },
                RoundingType = ORoundingType.Begin
            };
        }

        private static Loan _GetLoan(LoanProduct product, OCurrency amount, decimal interestRate, int installments, DateTime startDate)
        {
            return new Loan(product, amount, interestRate, installments, 0, startDate,
                new User(), ApplicationSettings.GetInstance(""),
                NonWorkingDateSingleton.GetInstance(""),
                ProvisionTable.GetInstance(new User()),
                ChartOfAccounts.GetInstance(new User()));
        }

        private static void _AssertInstallment(Loan loan, int number, string date, OCurrency interest, OCurrency principal)
        {
            Installment i = loan.GetInstallment(number);
            Assert.AreEqual(DateTime.Parse(date), i.ExpectedDate);
            Assert.AreEqual(interest, i.InterestsRepayment);
            Assert.AreEqual(principal, i.CapitalRepayment);
        }

        private static void _AssertInstallment(Loan loan, int number, string date, OCurrency interest, OCurrency principal, OCurrency olb)
        {
            Installment i = loan.GetInstallment(number);
            Assert.AreEqual(DateTime.Parse(date), i.ExpectedDate);
            Assert.AreEqual(interest, i.InterestsRepayment);
            Assert.AreEqual(principal, i.CapitalRepayment);
            Assert.AreEqual(olb, i.OLB);
        }

        private static void _AssertInstallment(Loan loan, int number, string date, OCurrency interest, OCurrency principal, OCurrency olb, OCurrency paidPrincipal, OCurrency paidInterest)
        {
            Installment i = loan.GetInstallment(number);
            Assert.AreEqual(DateTime.Parse(date), i.ExpectedDate);
            Assert.AreEqual(interest, i.InterestsRepayment);
            Assert.AreEqual(principal, i.CapitalRepayment);
            Assert.AreEqual(olb, i.OLB);
            Assert.AreEqual(paidPrincipal, i.PaidCapital);
            Assert.AreEqual(paidInterest, i.PaidInterests);
        }

        [Test]
        public void AddTranche_Annuity_ApplyNewInterest_Accrual()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            Loan loan = _GetLoan(_GetAnnuityProduct(), 1000, 0.025m, 4, new DateTime(2010, 5, 11));
            loan.Disburse(new DateTime(2010, 5, 11), true, false);
            loan.Repay(1, new DateTime(2010, 6, 11), 266m, false, true);
            loan.Repay(2, new DateTime(2010, 7, 9), 266m, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            TrancheOptions to = new TrancheOptions
            {
                TrancheDate = new DateTime(2010, 7, 15),
                CountOfNewInstallments = 4,
                TrancheAmount = 1000,
                InterestRate = 0.03m,
                ApplyNewInterestOnOLB = true
            };

             loan.AddTranche(loan.CalculateTranche(to));

            _AssertInstallment(loan, 2, "2010-08-16", 46m, 362m);
            _AssertInstallment(loan, 3, "2010-09-15", 35m, 372m);
            _AssertInstallment(loan, 4, "2010-10-15", 23m, 384m);
            _AssertInstallment(loan, 5, "2010-11-15", 12m, 394m);
        }

        [Test]
        public void AddTranche_Annuity_ApplyNewInterest_Cash()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            Loan loan = _GetLoan(_GetAnnuityProduct(), 1000, 0.025m, 4, new DateTime(2010, 5, 11));
            loan.Disburse(new DateTime(2010, 5, 11), true, false);
            loan.Repay(1, new DateTime(2010, 6, 11), 266m, false, true);
            loan.Repay(2, new DateTime(2010, 7, 9), 266m, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            TrancheOptions to = new TrancheOptions
            {
                TrancheDate = new DateTime(2010, 7, 15),
                CountOfNewInstallments = 4,
                TrancheAmount = 1000,
                InterestRate = 0.03m,
                ApplyNewInterestOnOLB = true
            };

            loan.AddTranche(loan.CalculateTranche(to));

            _AssertInstallment(loan, 2, "2010-08-16", 45m, 362m);
            _AssertInstallment(loan, 3, "2010-09-15", 35m, 372m);
            _AssertInstallment(loan, 4, "2010-10-15", 23m, 384m);
            _AssertInstallment(loan, 5, "2010-11-15", 12m, 394m);
        }

        [Test]
        public void AddTranche_FixedPrincipal_ApplyNewInterest_Accrual()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            Loan loan = _GetLoan(_GetFixedPrincipalProduct(), 1000, 0.025m, 4, new DateTime(2010, 5, 11));
            loan.Disburse(new DateTime(2010, 5, 11), true, false);
            loan.Repay(1, new DateTime(2010, 6, 11), 275m, false, true);
            loan.Repay(2, new DateTime(2010, 7, 9), 269m, false, true);

            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            TrancheOptions to = new TrancheOptions
            {
                TrancheDate = new DateTime(2010, 7, 15),
                CountOfNewInstallments = 4,
                TrancheAmount = 1000,
                InterestRate = 0.03m,
                ApplyNewInterestOnOLB = true
            };

            loan.AddTranche(loan.CalculateTranche(to));

            _AssertInstallment(loan, 2, "2010-08-16", 46m, 500m);
            _AssertInstallment(loan, 3, "2010-09-15", 30m, 500m);
            _AssertInstallment(loan, 4, "2010-10-15", 15m, 250m);
            _AssertInstallment(loan, 5, "2010-11-15", 8m, 250m);
        }

        [Test]
        public void AddTranche_FixedPrincipal_ApplyNewInterest_Cash()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            Loan loan = _GetLoan(_GetFixedPrincipalProduct(), 1000, 0.025m, 4, new DateTime(2010, 5, 11));
            loan.Disburse(new DateTime(2010, 5, 11), true, false);
            loan.Repay(1, new DateTime(2010, 6, 11), 275m, false, true);
            loan.Repay(2, new DateTime(2010, 7, 9), 269m, false, true);
            
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            TrancheOptions to = new TrancheOptions
            {
                TrancheDate = new DateTime(2010, 7, 15),
                CountOfNewInstallments = 4,
                TrancheAmount = 1000,
                InterestRate = 0.03m,
                ApplyNewInterestOnOLB = true
            };

            loan.AddTranche(loan.CalculateTranche(to));

            _AssertInstallment(loan, 2, "2010-08-16", 45m, 500m);
            _AssertInstallment(loan, 3, "2010-09-15", 30m, 500m);
            _AssertInstallment(loan, 4, "2010-10-15", 15m, 250m);
            _AssertInstallment(loan, 5, "2010-11-15", 8m, 250m);
        }

    }
}
