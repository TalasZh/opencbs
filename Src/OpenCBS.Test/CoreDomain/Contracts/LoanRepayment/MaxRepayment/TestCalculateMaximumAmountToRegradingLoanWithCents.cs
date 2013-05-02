// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.MaxRepayment;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using NUnit.Framework;
using OpenCBS.Shared;
using System.Collections;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain.Contracts.LoanRepayment.MaxRepayment
{
    /// <summary>
    /// Summary description for TestCalculateMaximumAmountToRegradingLoan.
    /// </summary>
    [TestFixture]
    public class TestCalculateMaximumAmountToRegradingLoanWithCents
    {
        private CalculateMaximumAmountToRegradingLoanStrategy repayStrategy;
        private NonWorkingDateSingleton nonWorkingDateHelper;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            nonWorkingDateHelper = NonWorkingDateSingleton.GetInstance("");
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
            generalParameters.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Cash);
            generalParameters.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
            generalParameters.AddParameter(OGeneralSettings.INTEREST_RATE_DECIMAL_PLACES, 2);
            generalParameters.AddParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
            generalParameters.AddParameter(OGeneralSettings.STOP_WRITEOFF_PENALTY, true);
        }

        [SetUp]
        public void SetUp()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
        }

        [TestFixtureTearDown]
        public void testFixtureTearDown()
        {
            ApplicationSettings generalParameters = ApplicationSettings.GetInstance("");
            generalParameters.DeleteAllParameters();
            generalParameters.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
        }

        [Test]
        public void Flate_BadLoan_42DaysLate_BasedOnInitialAmount_KeepExpectedInstallment_Fees()
        {
            LoanProduct package = new LoanProduct
                                      {
                                          InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                                          LoanType = OLoanTypes.Flat,
                                          ChargeInterestWithinGracePeriod = true,
                                          KeepExpectedInstallment = true,
                                          AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB,
                                          Currency = new Currency { Id = 1 }
                                      };
            Loan myContract = new Loan(package,1000,0.03m,6,1,new DateTime(2006,1,1) , new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.InitialAmount = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            //42 days late
            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0, 0, false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 1000 * 0.003 * 42 = 386
            Assert.AreEqual(386,Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006,3,15)).Value,2));
        }

        [Test]
        public void Flate_BadLoan_42dayslate_BasedOnInitialAmount_KeepExpectedInstallment_NoFees()
        {
            LoanProduct package = new LoanProduct
                                      {
                                          InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                                          LoanType = OLoanTypes.Flat,
                                          ChargeInterestWithinGracePeriod = true,
                                          KeepExpectedInstallment = true,
                                          AnticipatedTotalRepaymentPenaltiesBase =
                                              OAnticipatedRepaymentPenaltiesBases.RemainingOLB,
                                          Currency = new Currency { Id = 1 }
                                      };
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.InitialAmount = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, true, 0, 0, false, 0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230
            Assert.AreEqual(260,Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006,3,15)).Value,2));
        }

        [Test]
        public void Flate_BadLoan_42dayslate_BasedOnInitialAmount_KeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
                                      {
                                          InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                                          LoanType = OLoanTypes.Flat,
                                          ChargeInterestWithinGracePeriod = true,
                                          KeepExpectedInstallment = false,
                                          AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB,
                                          Currency = new Currency { Id = 1 }
                                      };
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.InitialAmount = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0, 0, false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 1000 * 0.003 * 42 = 386
            Assert.AreEqual(386,Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006,3,15)).Value,2));
        }

        [Test]
        public void Flate_BadLoan_42dayslate_BasedOnOLB_KeepExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
                                      {
                                          InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                                          LoanType = OLoanTypes.Flat,
                                          ChargeInterestWithinGracePeriod = true,
                                          KeepExpectedInstallment = true,
                                          AnticipatedTotalRepaymentPenaltiesBase =
                                              OAnticipatedRepaymentPenaltiesBases.RemainingOLB,
                                          Currency = new Currency { Id = 1 }
                                      };
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(),
                                       ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""),
                                       ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()))
                                  {
                                      BadLoan = true,
                                      NonRepaymentPenalties = {OLB = 0.003},
                                      AnticipatedTotalRepaymentPenalties = 0.01
                                  };

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0, 0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 1000 * 0.003 * 42 = 386
            Assert.AreEqual(386, Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }
		
        [Test]
        public void Flate_BadLoan_42dayslate_OnOLB_KeepNotExpectedInstallment()
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
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OLB = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0,0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 1000 * 0.003 * 42 = 386
            Assert.AreEqual(386, Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void _Flate_BadLoan_42dayslate_BasedOnOverDueWithoutInterest_KeepExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);

            LoanProduct package = new LoanProduct
                                      {
                                          InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                                          LoanType = OLoanTypes.Flat,
                                          ChargeInterestWithinGracePeriod = true,
                                          Currency = new Currency {Id = 1, UseCents = true},
                                          KeepExpectedInstallment = true,
                                          AnticipatedTotalRepaymentPenaltiesBase =
                                              OAnticipatedRepaymentPenaltiesBases.RemainingOLB
                                      };
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(),
                                       ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""),
                                       ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()))
                                  {
                                      BadLoan = true,
                                      NonRepaymentPenalties = {OverDuePrincipal = 0.003},
                                      AnticipatedTotalRepaymentPenalties = 0.01
                                  };

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 200 * 0.003 * 14 
            Assert.AreEqual(268.4m, Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, false);            
            
            //30 + 230 + 200 * 0.003 * 12 
            Assert.AreEqual(266.60m, repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value);

        }

        [Test]
        public void FlateBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithoutInterestAndKeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);

            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true }
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;
            
            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false, 0, 0, false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 200 * 0.003 * 14 
            Assert.AreEqual(268.4m, Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);            

            //30 + 230 + 200 * 0.003 * 12 
            Assert.AreEqual(266.60m, repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value);


        }

        [Test]
        public void FlateBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithInterestAndKeepExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true }
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.NonRepaymentPenalties.OverDueInterest = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false, 0, 0, false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 30 * 0.003 * 42 + 230 * 0.003 *14
            Assert.AreEqual(273.44m, Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);            

            //30 + 230 + 30 * 0.003 * 36 + 230 * 0.003 *12
            Assert.AreEqual(270.74m, repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value);
        }


        [Test]
        public void FlateBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithInterestAndKeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true }
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDueInterest = 0.003;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false, 0, 0, false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 230 + 30 * 0.003 * 42 + 230 * 0.003 *14		

            Assert.AreEqual(273.44m, Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);            

            

            //30 + 230 + 30 * 0.003 * 36 + 230 * 0.003 *12		
            Assert.AreEqual(270.74m, repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value);


        }

        [Test]
        public void Declining_BadLoanWith42dayslateWhenNonRepaymentFeesBaseOnInitialAmountAndKeepExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true},
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), 
                NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.InitialAmount = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0,0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 1000 * 0.003 * 42
            Assert.AreEqual(374.35m, repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value);

        }

        [Test]
        public void DecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnInitialAmountAndKeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true},
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.InitialAmount = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0,0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 1000 * 0.003 * 42
            Assert.AreEqual(374.35m, repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value);
        }


        [Test]
        public void DecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOLBAndKeepExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true},
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OLB = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;
			
            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0,0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 1000 * 0.003 * 42
            Assert.AreEqual(374.35m, repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value);

        }

        [Test]
        public void DecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOLBAndKeepNotExpectedInstallment()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true},
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OLB = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0,0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 1000 * 0.003 * 42
            Assert.AreEqual(374.35m, repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value);
        }

        [Test]
        public void DecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithoutInterestAndKeepExpectedInstallment2()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);

            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true},
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            Assert.AreEqual(256.26m, repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value);
            //30 + 218.35 + 188.35 * 1.003 * 14
            //30+218.3546

            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            cCO = new CreditContractOptions(package.LoanType, package.KeepExpectedInstallment, false, 0, 0, false, 0, package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            Assert.AreEqual(254.57m, repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value);
        }

        [Test]
        public void DecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithoutInterestAndKeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true},
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0,0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            ////30 + 218.35 + 188.35 * 1.003 * 14
            Assert.AreEqual(256.26m, Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));

        }

        [Test]
        public void DecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithInterestAndKeepExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true},
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = true;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDueInterest = 0.003;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0,0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 30 * 0.003 * 42 + 218.35 * 0.003 * 14
            Assert.AreEqual(261.30m, Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void DecliningBadLoanWith42dayslateWhenNonRepaymentFeesBaseOnOverDueWithInterestAndKeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.DecliningFixedInstallments,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true},
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = false;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.NonRepaymentPenalties.OverDueInterest = 0.003;
            myContract.NonRepaymentPenalties.OverDuePrincipal = 0.003;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;

            CreditContractOptions cCO = new CreditContractOptions(package.LoanType,package.KeepExpectedInstallment,false,0,0,false,0,package.AnticipatedTotalRepaymentPenaltiesBase);
            repayStrategy = new CalculateMaximumAmountToRegradingLoanStrategy(cCO, myContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));

            //30 + 218.35 + 30 * 0.003 * 42 + 218.35 * 0.003 * 14
            Assert.AreEqual(261.30m, Math.Round(repayStrategy.CalculateMaximumAmountToRegradingLoan(new DateTime(2006, 3, 15)).Value, 2));
        }
    }
}
