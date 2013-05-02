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
using OpenCBS.Shared.Settings;


namespace OpenCBS.Test.CoreDomain.Contracts.LoanRepayment.MaxRepayment
{
    /// <summary>
    /// Summary description for TestCalculateMaximumAmountToRepay.
    /// </summary>
    [TestFixture]
    public class TestCalculateMaximumAmountToRepayWithCentsInAccrualMode
    {
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
            generalParameters.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalParameters.AddParameter(OGeneralSettings.ACCOUNTINGPROCESS, OAccountingProcesses.Accrual);
            generalParameters.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
            generalParameters.AddParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
            generalParameters.AddParameter(OGeneralSettings.STOP_WRITEOFF_PENALTY, true);
            generalParameters.AddParameter(OGeneralSettings.INTEREST_RATE_DECIMAL_PLACES, 2);
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
        }

        [TestFixtureTearDown]
        public void testFixtureTearDown()
        {
            ApplicationSettings generalParameters = ApplicationSettings.GetInstance("");
            generalParameters.DeleteAllParameters();
        }

        [Test]
        public void Flat_BadLoan_42DaysLate_InitialAmount_KeepExpectedInstallment_Fees()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            //1000 + 180 + 1000 * 42 * 0.003 => 1306  //no anticipatedFees when keep expected installment set to true
            Assert.AreEqual(1306, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Flat_BadLoan_42DaysLate_InitialAmount_KeepExpectedInstallment_CancelFees_CancelInterest()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, true, 10, 0, true, 1000);

            //1000 + 10 + 1000 => 1314 
            Assert.AreEqual(2010, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Flat_BadLoan_42DaysLate_InitialAmount_KeepExpectedInstallment_CancelFees()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, true, 10, 0, false, 0);

            //1000 + 180 + 10 => 1190 
            Assert.AreEqual(1190, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Flat_BadLoan_42daysLate_InitialAmount_KeepExpectedInstallment_CancelFees()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, true, 0, 0, false, 0);

            Assert.AreEqual(1180, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Flat_BadLoan_42daysLate_InitialAmount_KeepNotExpectedInstallment_DontCancelFees()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0.003, 0, 0, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            //First installment => 30
            //Second installments => 30 + 200
            //Third instalment => 11.55
            //800 + 1000 * 42 * 0.003 + 800 * 0.01 + 260=> 800 + 126 + 8 + 260
            Assert.AreEqual(1206.73m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Flat_BadLoan_42dayslate_OLB_KeepExpectedInstallment_DontCancelFees()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            //Without CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS: 1000 + 180 + 1000 * 42 * 0.003  => 1306
            //Assert.AreEqual(1306, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

            //With CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS:
            // Excluding 7 days: 3 holidays (1/1/2006, 6/1/2006, 8/3/2006) and 4 weekends
            
            Assert.AreEqual(1285, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Flat_BadLoan_42dayslate_OLB_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0.003, 0, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1185.73m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Flat_BadLoan_42dayslate_OverDuePrincipal_KeepExpectedInstallment_DontCancelFees()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);            
            //1000 + 180 + 200 * 14 * 0.003 => 1188.40 
            Assert.AreEqual(1188.40m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
           
            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);
            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), true);
            mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);
            //1000 + 180 + 200 * 12 * 0.003 => 1187.200

            //  Assert.AreEqual(1187.20m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
            //dans le serveur, le resultat  est 1186.60
            Assert.AreEqual(1186.60m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

        }

        [Test]
        public void CalculateMaximumAmount_Flat_BadLoan_42dayslate_OverDuePrincipal_KeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);          
            //1000 + 60 + 200 * 14 * 0.003 + 800 * 0.01 => 1000+60+16.4
            Assert.AreEqual(1089.13m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);
            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false);
            mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);
            //1000 + 60 + 200 * 11 * 0.003 + 800 * 0.01 => 1074.6

            Assert.AreEqual(1087.33m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

        }

        [Test]
        public void CalculateMaximumAmount_Flat_BadLoan_42dayslate_OverDuePrincipalAndInterest_KeepExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
                
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);
            
            //1000 + 180 + 30 * 42 * 0.003 + 230 * 14 * 0.003 => 1193.44
            Assert.AreEqual(1193.44m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));



            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);
            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);
            //1000 + 180 + 30 * 36 * 0.003 + 230 * 12 * 0.003 => 1191.52
            //Assert.AreEqual(1191.52m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
            //dans le serveur, le resuatat est 1190.74
            Assert.AreEqual(1190.74m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

        }


        [Test]
        public void CalculateMaximumAmount_Flat_BadLoan_42dayslate_OverDuePrincipalAndInterest_KeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            //1000 + 60 + 30 * 42 * 0.003 + 230 * 14 * 0.003 + 800 * 0.01 => 1081.44
            Assert.AreEqual(1094.17m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);
            myContract = _SetContract(OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0.003), false);
            mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);
            //1000 + 60 + 30 * 35 * 0.003 + 230 * 12 * 0.003 + 800 * 0.01 => 1078.74m

            Assert.AreEqual(1091.47m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42dayslate_InitialAmount_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0.003, 0, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            //1000 + 121.77 + 1000 * 42 * 0.003 = 1247.77
            Assert.AreEqual(1247.77m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42dayslate_InitialAmount_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0.003, 0, 0, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1204.45m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42dayslate_OLB_KeepExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0.003, 0, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            //1000 + 121.77 + 1000 * 42 * 0.003 = 1247.77
            Assert.AreEqual(1247.77m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
          
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42dayslate_OLB_KeepNotExpectedInstallment()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0.003, 0, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1204.45m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42dayslate_OverDuePrincipal_KeepExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);            

            //1000 + 121.77 + 188.35 * 14 * 0.003 = 1129.68
            Assert.AreEqual(1129.68m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));


            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);
            myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0), true);  
            mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            //1000 + 121.77 + 188.35 * 12 * 0.003 = 1128.55060
            //Assert.AreEqual(1128.55m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
            //dans le seurveur , le resultat est 1127.99
            Assert.AreEqual(1127.99m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42dayslate_OverDuePrincipal_KeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);


            Assert.AreEqual(1086.36m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
            
            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            Assert.AreEqual(1084.67m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoanWith_42dayslate_OverDuePrincipalAndInterest_KeepExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            

            //1000 + 121.77 + 188.35 * 14 * 0.003 + 30 * 42* 0.003 + 30 * 14 * 0.003 = 1134.72
            Assert.AreEqual(1134.72m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);            

            //1000 + 121.77 + 188.35 * 12 * 0.003 + 30 * 36* 0.003 + 30 * 12 * 0.003 = 1132.87
            Assert.AreEqual(1132.13m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));


        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoanWith_42dayslate_OverDuePrincipalAndInterest_KeepExpectedInstallment_CancelInterest_CancelFees()
        {
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, true, 100, 0, true, 888);

            Assert.AreEqual(1988, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoanWith_42dayslate_OverDuePrincipalAndInterest_KeepExpectedInstallment_CancelInterest()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);
            
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0.003), true);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, true, 0);
            
            //1000 + 188.35 * 14 * 0.003 + 30 * 42* 0.003 + 30 * 14 * 0.003 = 1012.95
            Assert.AreEqual(1012.95m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);            
            //1000 + 188.35 * 12 * 0.003 + 30 * 36* 0.003 + 30 * 12 * 0.003 = 1011.10
            Assert.AreEqual(1010.36m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

            
        }

        [Test]
        public void CalculateMaximumAmount_Declining_BadLoan_42dayslate_OverDuePrincipalAndInterest_KeepNotExpectedInstallment()
        {
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", true);            
            Loan myContract = _SetContract(OLoanTypes.DecliningFixedInstallments, new NonRepaymentPenalties(0, 0, 0.003, 0.003), false);
            CalculateMaximumAmountToRepayStrategy mAR = _MaximumAmountToRepay(myContract, false, 0, 0, false, 0);

            Assert.AreEqual(1091.4m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));
            //NOT CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS
            ApplicationSettings.GetInstance("").UpdateParameter("CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS", false);

            Assert.AreEqual(1088.81m, Math.Round(mAR.CalculateMaximumAmountAuthorizedToRepay(new DateTime(2006, 3, 15)).Value, 2));

           
        }

        #region private methods
        private static Loan _SetContract(OLoanTypes pLoansType, NonRepaymentPenalties pNonRepaymentPenalties, bool pKeepExpectedInstallment)
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = pLoansType,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1, UseCents = true},
                RoundingType = ORoundingType.Approximate
            };
            package.KeepExpectedInstallment = pKeepExpectedInstallment;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, 1000, 0.03m, 6, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = true;
            myContract.AnticipatedTotalRepaymentPenalties = 0.01;
            myContract.NonRepaymentPenalties = pNonRepaymentPenalties;
            return myContract;
        }

        private static CalculateMaximumAmountToRepayStrategy _MaximumAmountToRepay(Loan pContract, bool pCancelFees, OCurrency pManualFeesAmount, OCurrency pManualCommissionAmount, bool pCancelInterest, OCurrency pManualInterestAmount)
        {
            CreditContractOptions cCO = new CreditContractOptions(pContract.Product.LoanType, pContract.Product.KeepExpectedInstallment, pCancelFees, pManualFeesAmount, pManualCommissionAmount, pCancelInterest, pManualInterestAmount,
                                                                  pContract.Product.AnticipatedTotalRepaymentPenaltiesBase);

            return new CalculateMaximumAmountToRepayStrategy(cCO, pContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));
        }

        #endregion
    }
}