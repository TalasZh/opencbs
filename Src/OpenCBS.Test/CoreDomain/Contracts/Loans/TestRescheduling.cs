// LICENSE PLACEHOLDER

using System;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Rescheduling;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain.Contracts.Loans
{
    [TestFixture]
    public class TestRescheduling
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

        [Test]
        [ExpectedException(typeof(OpenCBS.ExceptionsHandler.ReschedulingContractClosedException))]
        public void Reschedule_ContractClosed_ThrowException()
        {
            Loan loan = _GetContract_6Month_Flat();
            loan.Repay(1, new DateTime(2006, 1, 30), 1120, true, true);
            Assert.AreEqual(loan.Closed, true);
            ReschedulingOptions ro = new ReschedulingOptions();
            loan.Reschedule(ro);
        }

        private static LoanProduct _GetFlatProduct()
        {
            return new LoanProduct
                       {
                           InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                           LoanType = OLoanTypes.Flat,
                           ChargeInterestWithinGracePeriod = true,
                           Currency = new Currency {Id = 1, UseCents = false}
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
                           Currency = new Currency {Id = 1, UseCents = false }
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
        public void Reschedule_Flat_ShiftDate()
        {
            Loan loan = _GetLoan(_GetFlatProduct(), 10000, 0.04m, 6, new DateTime(2009, 12, 24));
            loan.Repay(1, new DateTime(2010, 1, 25), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 2, 24), 2067, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 0,
                RepaymentDateOffset = 38,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 943, 1667); // 10_000 * 0.04 * 66 / 28
            _AssertInstallment(loan, 3, "2010-06-01", 400, 1666);
            _AssertInstallment(loan, 4, "2010-07-01", 400, 1667);
            _AssertInstallment(loan, 5, "2010-08-02", 400, 1666);
        }

        [Test]
        public void Reschedule_Flat_ChangeInterestRate()
        {
            Loan loan = _GetLoan(_GetFlatProduct(), 10000, 0.04m, 6, new DateTime(2009, 12, 24));
            loan.Repay(1, new DateTime(2010, 1, 25), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 2, 24), 2067, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 0,
                RepaymentDateOffset = 0,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-03-24", 300, 1667);
            _AssertInstallment(loan, 3, "2010-04-26", 300, 1666);
            _AssertInstallment(loan, 4, "2010-05-24", 300, 1667);
            _AssertInstallment(loan, 5, "2010-06-24", 300, 1666);
        }

        [Test]
        public void Reschedule_Flat_ShiftDate_ChangeInterestRate()
        {
            Loan loan = _GetLoan(_GetFlatProduct(), 10000, 0.04m, 6, new DateTime(2009, 12, 24));
            loan.Repay(1, new DateTime(2010, 1, 25), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 2, 24), 2067, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m, 
                NewInstallments = 0, 
                RepaymentDateOffset = 38,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 707, 1667); // 10_000 * 0.03 * 66 / 28
            _AssertInstallment(loan, 3, "2010-06-01", 300, 1666);
            _AssertInstallment(loan, 4, "2010-07-01", 300, 1667);
            _AssertInstallment(loan, 5, "2010-08-02", 300, 1666);
        }

        [Test]
        public void Reschedule_Flat_ShiftDate_DoNotAccrueInterest()
        {
            Loan loan = _GetLoan(_GetFlatProduct(), 10000, 0.04m, 6, new DateTime(2009, 12, 24));
            loan.Repay(1, new DateTime(2010, 1, 25), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 2, 24), 2067, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 0,
                RepaymentDateOffset = 38,
                ChargeInterestDuringShift = false
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 400, 1667);
            _AssertInstallment(loan, 3, "2010-06-01", 400, 1666);
            _AssertInstallment(loan, 4, "2010-07-01", 400, 1667);
            _AssertInstallment(loan, 5, "2010-08-02", 400, 1666);
        }

        [Test]
        public void Reschedule_Flat_ShiftDate_UseCents()
        {
            Loan loan = _GetLoan(_GetFlatProduct_WithCents(), 10000, 0.04m, 6, new DateTime(2009, 12, 24));
            loan.Repay(1, new DateTime(2010, 1, 25), 2066.67m, false, true);
            loan.Repay(2, new DateTime(2010, 2, 24), 2066.67m, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 0,
                RepaymentDateOffset = 38,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 942.86m, 1666.67m);
            _AssertInstallment(loan, 3, "2010-06-01", 400, 1666.66m);
            _AssertInstallment(loan, 4, "2010-07-01", 400, 1666.67m);
            _AssertInstallment(loan, 5, "2010-08-02", 400, 1666.66m);
        }

        [Test]
        public void Reschedule_Flat_ShiftDate_ChangeInterestRate_PartiallyPaid()
        {
            Loan loan = _GetLoan(_GetFlatProduct(), 10000, 0.04m, 6, new DateTime(2009, 12, 22));
            loan.Repay(1, new DateTime(2010, 1, 22), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 2, 22), 2067, false, true);
            PaymentMethod paymentMethod = new PaymentMethod(1, "Cash", "", false);
            loan.Repay(3, new DateTime(2010, 3, 5), 1500, false, 0, 0, true, 200, true, false, false, paymentMethod);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(2).IsPartiallyRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 0,
                RepaymentDateOffset = 40,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 729, 2640, 6666);
            _AssertInstallment(loan, 3, "2010-06-01", 300, 1342, 4026);
            _AssertInstallment(loan, 4, "2010-07-01", 300, 1342, 2684);
            _AssertInstallment(loan, 5, "2010-08-02", 300, 1342, 1342);
        }

        [Test]
        public void Reschedule_Flat_ShiftDate_ChangeInterestRate_PartiallyPaid_UseCents()
        {
            Assert.Ignore();
            Loan loan = _GetLoan(_GetFlatProduct_WithCents(), 10000, 0.04m, 6, new DateTime(2009, 12, 22));
            loan.Repay(1, new DateTime(2010, 1, 22), 2066.67m, false, true);
            loan.Repay(2, new DateTime(2010, 2, 22), 2066.67m, false, true);
//            loan.Repay(3, new DateTime(2010, 3, 5), 1500.00m, false, 0, 0, true, 200, true, false, false, OPaymentMethods.Cash);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(2).IsPartiallyRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 0,
                RepaymentDateOffset = 40,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 728.57m, 2641.65m, 6666.66m);
            _AssertInstallment(loan, 3, "2010-06-01", 300.00m, 1341.67m, 4025.01m);
            _AssertInstallment(loan, 4, "2010-07-01", 300.00m, 1341.67m, 2683.34m);
            _AssertInstallment(loan, 5, "2010-08-02", 300.00m, 1341.67m, 1341.67m);
        }

        [Test]
        public void Reschedule_Flat_ExtendMaturity()
        {
            Loan loan = _GetLoan(_GetFlatProduct(), 10000, 0.04m, 6, new DateTime(2009, 12, 22));
            loan.Repay(1, new DateTime(2010, 1, 22), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 2, 22), 2067, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
                                         {
                                             InterestRate = 0.04m,
                                             NewInstallments = 2,
                                             RepaymentDateOffset = 0,
                                             ChargeInterestDuringShift = true
                                         };
            loan.Reschedule(ro);
            _AssertInstallment(loan, 2, "2010-03-22", 400, 1111, 6666, 0, 0);
            _AssertInstallment(loan, 3, "2010-04-22", 400, 1111, 5555, 0, 0);
            _AssertInstallment(loan, 4, "2010-05-24", 400, 1111, 4444, 0, 0);
            _AssertInstallment(loan, 5, "2010-06-22", 400, 1111, 3333, 0, 0);
            _AssertInstallment(loan, 6, "2010-07-22", 400, 1111, 2222, 0, 0);
            _AssertInstallment(loan, 7, "2010-08-23", 400, 1111, 1111, 0, 0);
        }

        [Test]
        public void Reschedule_Flat_ExtendMaturity_ShiftDate()
        {
            Loan loan = _GetLoan(_GetFlatProduct(), 10000, 0.04m, 6, new DateTime(2009, 12, 22));
            loan.Repay(1, new DateTime(2010, 1, 22), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 1, 22), 2067, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 2,
                RepaymentDateOffset = 21,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);
            _AssertInstallment(loan, 2, "2010-04-12", 700, 1111, 6666, 0, 0);
            _AssertInstallment(loan, 3, "2010-05-12", 400, 1111, 5555, 0, 0);
            _AssertInstallment(loan, 4, "2010-06-14", 400, 1111, 4444, 0, 0);
            _AssertInstallment(loan, 5, "2010-07-12", 400, 1111, 3333, 0, 0);
            _AssertInstallment(loan, 6, "2010-08-12", 400, 1111, 2222, 0, 0);
            _AssertInstallment(loan, 7, "2010-09-13", 400, 1111, 1111, 0, 0);
        }

        [Test]
        public void Reschedule_FixedPrincipal_ShiftDate()
        {
            Loan loan = _GetLoan(_GetFixedPrincipalProduct(), 10000, 0.04m, 6, new DateTime(2009, 12, 24));
            loan.Repay(1, new DateTime(2010, 1, 25), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 2, 24), 2000, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 0,
                RepaymentDateOffset = 38,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 629, 1666); // 6_666 * 0.04 * 66 / 28
            _AssertInstallment(loan, 3, "2010-06-01", 200, 1667);
            _AssertInstallment(loan, 4, "2010-07-01", 133, 1666);
            _AssertInstallment(loan, 5, "2010-08-02", 67, 1667);
        }

        [Test]
        public void Reschedule_FixedPrincipal_ChangeInterest()
        {
            Loan loan = _GetLoan(_GetFixedPrincipalProduct(), 10000, 0.04m, 6, new DateTime(2009, 12, 24));
            loan.Repay(1, new DateTime(2010, 1, 25), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 2, 24), 2000, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 0,
                RepaymentDateOffset = 0,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-03-24", 200, 1666);
            _AssertInstallment(loan, 3, "2010-04-26", 150, 1667);
            _AssertInstallment(loan, 4, "2010-05-24", 100, 1666);
            _AssertInstallment(loan, 5, "2010-06-24", 50, 1667);
        }

        [Test]
        public void Reschedule_FixedPrincipal_ExtendMaturity()
        {
            Loan loan = _GetLoan(_GetFixedPrincipalProduct(), 10000, 0.04m, 6, new DateTime(2010, 1, 11));
            loan.Repay(1, new DateTime(2010, 2, 11), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 3, 11), 2000, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 2,
                RepaymentDateOffset = 0,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-04-12", 267, 1111, 6666, 0, 0);
            _AssertInstallment(loan, 3, "2010-05-11", 222, 1111, 5555, 0, 0);
            _AssertInstallment(loan, 4, "2010-06-11", 178, 1111, 4444, 0, 0);
            _AssertInstallment(loan, 5, "2010-07-12", 133, 1111, 3333, 0, 0);
            _AssertInstallment(loan, 6, "2010-08-11", 89, 1111, 2222, 0, 0);
            _AssertInstallment(loan, 7, "2010-09-13", 44, 1111, 1111, 0, 0);
        }

        [Test]
        public void Reschedule_FixedPrincipal_ExtendMaturity_ShiftDate()
        {
            Loan loan = _GetLoan(_GetFixedPrincipalProduct(), 10000, 0.04m, 6, new DateTime(2010, 1, 11));
            loan.Repay(1, new DateTime(2010, 2, 11), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 3, 11), 2000, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 2,
                RepaymentDateOffset = 43,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-25", 625, 1111, 6666, 0, 0);
            _AssertInstallment(loan, 3, "2010-06-25", 222, 1111, 5555, 0, 0);
            _AssertInstallment(loan, 4, "2010-07-26", 178, 1111, 4444, 0, 0);
            _AssertInstallment(loan, 5, "2010-08-25", 133, 1111, 3333, 0, 0);
            _AssertInstallment(loan, 6, "2010-09-27", 89, 1111, 2222, 0, 0);
            _AssertInstallment(loan, 7, "2010-10-25", 44, 1111, 1111, 0, 0);
        }

        [Test]
        public void Reschedule_FixedPrincipal_ExtendMaturity_ShiftDate_ChangeInterestRate_PartiallyPaid_UseCents()
        {
            Loan loan = _GetLoan(_GetFixedPrincipalProduct_WithCents(), 10000, 0.04m, 6, new DateTime(2010, 1, 11));
            loan.Repay(1, new DateTime(2010, 2, 11), 2066.67m, false, true);
            loan.Repay(2, new DateTime(2010, 3, 11), 2000.00m, false, true);
            PaymentMethod paymentMethod = new PaymentMethod(1, "Cash", "", false);
            loan.Repay(3, new DateTime(2010, 4, 5), 1500.00m, false, 0, 0, true, 200, true, false, false, paymentMethod);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(2).IsPartiallyRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 2,
                RepaymentDateOffset = 13,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-04-26", 226.41m, 2194.46m, 6666.66m, 1300.00m, 200.00m);
            _AssertInstallment(loan, 3, "2010-05-25", 134.17m, 894.44m, 4472.20m, 0, 0);
            _AssertInstallment(loan, 4, "2010-06-25", 107.33m, 894.44m, 3577.76m, 0, 0);
            _AssertInstallment(loan, 5, "2010-07-26", 80.50m, 894.44m, 2683.32m, 0, 0);
            _AssertInstallment(loan, 6, "2010-08-25", 53.67m, 894.44m, 1788.88m, 0, 0);
            _AssertInstallment(loan, 7, "2010-09-27", 26.83m, 894.44m, 894.44m, 0, 0);
        }
        
        [Test]
        public void Reschedule_FixedPrincipal_ShiftDate_ChangeInterestRate_PartiallyPaid()
        {
            Loan loan = _GetLoan(_GetFixedPrincipalProduct(), 10000, 0.04m, 6, new DateTime(2010, 1, 11));
            loan.Repay(1, new DateTime(2010, 2, 11), 2067, false, true);
            loan.Repay(2, new DateTime(2010, 3, 11), 2000, false, true);
            PaymentMethod paymentMethod = new PaymentMethod(1, "Cash", "", false);

            loan.Repay(3, new DateTime(2010, 4, 5), 1500, false, 0, 0, true, 200, true, false, false, paymentMethod);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(2).IsPartiallyRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 0,
                RepaymentDateOffset = 19,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 257, 2640, 6666);
            _AssertInstallment(loan, 3, "2010-06-01", 121, 1342, 4026);
            _AssertInstallment(loan, 4, "2010-07-01", 81, 1342, 2684);
            _AssertInstallment(loan, 5, "2010-08-02", 40, 1342, 1342);
        }

        [Test]
        public void Reschedule_FixedPrincipal_ShiftDate_ChangeInterestRate_PartiallyPaid_UseCents()
        {
            Loan loan = _GetLoan(_GetFixedPrincipalProduct_WithCents(), 10000, 0.04m, 6, new DateTime(2010, 1, 11));
            loan.Repay(1, new DateTime(2010, 2, 11), 2066.67m, false, true);
            loan.Repay(2, new DateTime(2010, 3, 11), 2000.00m, false, true);
            PaymentMethod paymentMethod = new PaymentMethod(1, "Cash", "", false);
            loan.Repay(3, new DateTime(2010, 4, 5), 1500.00m, false, 0, 0, true, 200, true, false, false, paymentMethod);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(2).IsPartiallyRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 0,
                RepaymentDateOffset = 19,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 256.59m, 2641.65m, 6666.66m);
            _AssertInstallment(loan, 3, "2010-06-01", 120.75m, 1341.67m, 4025.01m);
            _AssertInstallment(loan, 4, "2010-07-01", 80.50m, 1341.67m, 2683.34m);
            _AssertInstallment(loan, 5, "2010-08-02", 40.25m, 1341.67m, 1341.67m);
        }

        [Test]
        public void Reschedule_FixedPrincipal_ShiftDate_PartiallyPaid_UseCents()
        {
           Loan loan = _GetLoan(_GetFixedPrincipalProduct_WithCents(), 10000, 0.04m, 6, new DateTime(2010, 1, 11));
            loan.Repay(1, new DateTime(2010, 2, 11), 2066.67m, false, true);
            loan.Repay(2, new DateTime(2010, 3, 11), 2000.00m, false, true);
            PaymentMethod paymentMethod = new PaymentMethod(1, "Cash", "", false);
            loan.Repay(3, new DateTime(2010, 4, 5), 1500.00m, false, 0, 0, true, 200, true, false, false, paymentMethod);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(2).IsPartiallyRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 0,
                RepaymentDateOffset = 19,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 342.12m, 2641.65m, 6666.66m);
            _AssertInstallment(loan, 3, "2010-06-01", 161.00m, 1341.67m, 4025.01m);
            _AssertInstallment(loan, 4, "2010-07-01", 107.33m, 1341.67m, 2683.34m);
            _AssertInstallment(loan, 5, "2010-08-02", 53.67m, 1341.67m, 1341.67m);
        }

        [Test]
        public void Reschedule_FixedPrincipal_ChangeInterestRate_PartiallyPaid_UseCents()
        {
           Loan loan = _GetLoan(_GetFixedPrincipalProduct_WithCents(), 10000, 0.04m, 6, new DateTime(2010, 1, 11));
            loan.Repay(1, new DateTime(2010, 2, 11), 2066.67m, false, true);
            loan.Repay(2, new DateTime(2010, 3, 11), 2000.00m, false, true);
            PaymentMethod paymentMethod = new PaymentMethod(1, "Cash", "", false);
            loan.Repay(3, new DateTime(2010, 4, 5), 1500.00m, false, 0, 0, true, 200, true, false, false, paymentMethod);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(2).IsPartiallyRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 0,
                RepaymentDateOffset = 0,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-04-12", 161.00m, 2641.65m, 6666.66m);
            _AssertInstallment(loan, 3, "2010-05-11", 120.75m, 1341.67m, 4025.01m);
            _AssertInstallment(loan, 4, "2010-06-11", 80.50m, 1341.67m, 2683.34m);
            _AssertInstallment(loan, 5, "2010-07-12", 40.25m, 1341.67m, 1341.67m);
        }

        [Test]
        public void Reschedule_Annuity_ShiftDate_ChangeInterestRate_UseCents()
        {
            Loan loan = _GetLoan(_GetAnnuityProduct_WithCents(), 10000, 0.04m, 6, new DateTime(2010, 1, 12));
            loan.Repay(1, new DateTime(2010, 2, 12), 1907.62m, false, true);
            loan.Repay(2, new DateTime(2010, 3, 12), 1907.62m, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 0,
                RepaymentDateOffset = 19,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 335.05m, 1655.13m);
            _AssertInstallment(loan, 3, "2010-06-01", 158.08m, 1704.79m);
            _AssertInstallment(loan, 4, "2010-07-01", 106.94m, 1755.93m);
            _AssertInstallment(loan, 5, "2010-08-02", 54.26m, 1808.61m);
        }

        [Test]
        public void Reschedule_Annuity_ShiftDate_ChangeInterestRate()
        {
            Loan loan = _GetLoan(_GetAnnuityProduct_RoundingTypeBegin(), 10000, 0.04m, 6, new DateTime(2010, 1, 15));
            loan.Repay(1, new DateTime(2010, 2, 15), 1906, false, true);
            loan.Repay(2, new DateTime(2010, 3, 15), 1908, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 0,
                RepaymentDateOffset = 16,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 315, 1656);
            _AssertInstallment(loan, 3, "2010-06-01", 158, 1705);
            _AssertInstallment(loan, 4, "2010-07-01", 107, 1756);
            _AssertInstallment(loan, 5, "2010-08-02", 54, 1809);
        }

        [Test]
        public void Reschedule_Annuity_ShiftDate_ChangeInterestRate_PartiallyPaid_UseCents()
        {
           Loan loan = _GetLoan(_GetAnnuityProduct_RoundingTypeBegin_WithCents(), 10000, 0.04m, 6, new DateTime(2010, 1, 12));
            loan.Repay(1, new DateTime(2010, 2, 12), 1907.62m, false, true);
            loan.Repay(2, new DateTime(2010, 3, 12), 1907.62m, false, true);
            PaymentMethod paymentMethod = new PaymentMethod(1, "Cash", "", false);
            loan.Repay(3, new DateTime(2010, 4, 5), 1500, false, 0, 0, true, 200, true, false, false, paymentMethod);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(2).IsPartiallyRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 0,
                RepaymentDateOffset = 19,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 272.15m, 2644.40m, 6924.46m);
            _AssertInstallment(loan, 3, "2010-06-01", 128.40m, 1384.73m, 4280.06m);
            _AssertInstallment(loan, 4, "2010-07-01", 86.86m, 1426.27m, 2895.33m);
            _AssertInstallment(loan, 5, "2010-08-02", 44.07m, 1469.06m, 1469.06m);
        }
        [Test]

        public void Reschedule_Annuity_ShiftDate_PartiallyPaid()
        {
            Loan loan = _GetLoan(_GetAnnuityProduct_RoundingTypeBegin(), 10000, 0.04m, 6, new DateTime(2010, 1, 15));
            loan.Repay(1, new DateTime(2010, 2, 12), 1906, false, true);
            loan.Repay(2, new DateTime(2010, 3, 12), 1908, false, true);
            PaymentMethod paymentMethod = new PaymentMethod(1, "Cash", "", false);
            loan.Repay(3, new DateTime(2010, 4, 5), 1500, false, 0, 0, true, 200, true, false, false, paymentMethod);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(2).IsPartiallyRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 0,
                RepaymentDateOffset = 16,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-05-03", 341, 2625, 6926);
            _AssertInstallment(loan, 3, "2010-06-01", 172, 1378, 4301);
            _AssertInstallment(loan, 4, "2010-07-01", 117, 1433, 2923);
            _AssertInstallment(loan, 5, "2010-08-02", 60, 1490, 1490);
        }

        [Test]
        public void Reschedule_Annuity_ChangeInterestRate_PartiallyPaid_Overpaid()
        {
            Loan loan = _GetLoan(_GetAnnuityProduct_RoundingTypeBegin(), 10000, 0.04m, 6, new DateTime(2010, 1, 15));
            loan.Repay(1, new DateTime(2010, 2, 12), 1906, false, true);
            loan.Repay(2, new DateTime(2010, 3, 12), 1908, false, true);
            PaymentMethod paymentMethod = new PaymentMethod(1, "Cash", "", false);
            loan.Repay(3, new DateTime(2010, 4, 5), 1500, false, 0, 0, true, 200, true, false, false, paymentMethod);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(2).IsPartiallyRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.03m,
                NewInstallments = 0,
                RepaymentDateOffset = 0,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-04-15", 169, 2643, 6926, 1300, 169);
            _AssertInstallment(loan, 3, "2010-05-17", 128, 1386, 4283, 0, 31);
            _AssertInstallment(loan, 4, "2010-06-15", 87, 1427, 2897, 0, 0);
            _AssertInstallment(loan, 5, "2010-07-15", 44, 1470, 1470, 0, 0);
        }

        [Test]
        public void Reschedule_Annuity_ExtendMaturity()
        {
            Loan loan = _GetLoan(_GetAnnuityProduct_RoundingTypeBegin(), 10000, 0.04m, 6, new DateTime(2010, 1, 15));
            loan.Repay(1, new DateTime(2010, 2, 12), 1906, false, true);
            loan.Repay(2, new DateTime(2010, 3, 12), 1908, false, true);
            Assert.AreEqual(loan.GetInstallment(0).IsRepaid, true);
            Assert.AreEqual(loan.GetInstallment(1).IsRepaid, true);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 2,
                RepaymentDateOffset = 0,
                ChargeInterestDuringShift = true
            };
            loan.Reschedule(ro);

            _AssertInstallment(loan, 2, "2010-04-15", 277, 1046, 6926, 0, 0);
            _AssertInstallment(loan, 3, "2010-05-17", 235, 1086, 5880, 0, 0);
            _AssertInstallment(loan, 4, "2010-06-15", 192, 1129, 4794, 0, 0);
            _AssertInstallment(loan, 5, "2010-07-15", 147, 1174, 3665, 0, 0);
            _AssertInstallment(loan, 6, "2010-08-16", 100, 1221, 2491, 0, 0);
            _AssertInstallment(loan, 7, "2010-09-15", 51, 1270, 1270, 0, 0);
        }
    }
}
