using System;
using System.Collections.Generic;
using NUnit.Framework;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Contracts.Loans;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.CoreDomain.Contracts.Loans.LoanRepayment;
using Octopus.CoreDomain.Contracts.Rescheduling;
using Octopus.CoreDomain.Events;
using Octopus.CoreDomain.Events.Loan;
using Octopus.CoreDomain.Products;
using Octopus.Services.Accounting;
using NUnit.Mocks;
using Octopus.Manager.Accounting;
using Octopus.CoreDomain.Accounting;
using Octopus.Enums;
using Octopus.ExceptionsHandler.Exceptions.AccountExceptions;
using Octopus.Shared;
using Octopus.Shared.Settings;

namespace Octopus.Test.Services.Accounting
{
    [TestFixture]
    public class TestAccountingRuleServices
    {
        private AccountingRuleServices _accountingRuleServices;
        private DynamicMock _accountingRuleManagerMock;

        [SetUp]
        public void SetUp()
        {
            _accountingRuleManagerMock = new DynamicMock(typeof(AccountingRuleManager));
            _accountingRuleServices = new AccountingRuleServices((AccountingRuleManager)_accountingRuleManagerMock.MockInstance);

            NonWorkingDateSingleton holidays = NonWorkingDateSingleton.GetInstance("");
            holidays.WeekEndDay1 = 6;
            holidays.WeekEndDay2 = 0;
            holidays.PublicHolidays = new Dictionary<DateTime, string>
                                          {
                                              {new DateTime(2011, 1, 1), "New Year Eve"},
                                              {new DateTime(2011, 12, 25), "Christmas"}
                                          };
            ApplicationSettings generalSettings = ApplicationSettings.GetInstance("");

            generalSettings.DeleteAllParameters();
            generalSettings.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalSettings.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
            generalSettings.AddParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
            generalSettings.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
            generalSettings.AddParameter(OGeneralSettings.BAD_LOAN_DAYS, 50);
            generalSettings.AddParameter(OGeneralSettings.STOP_WRITEOFF_PENALTY, true);
        }

        [Test]
        public void Test_SaveContractAccountingRule_When_GenericAccount_Is_Null()
        {
            ContractAccountingRule rule = new ContractAccountingRule
            {
                CreditAccount = new Account { Id = 2, Number = "1250" },
                ClientType = OClientTypes.All,
                ProductType = OProductTypes.All
            };

            try
            {
                _accountingRuleServices.SaveAccountingRule(rule);
                Assert.Fail("Accounting Rule shouldn't pass validation test while trying to save (Generic Account is Null).");
            }
            catch (OctopusAccountingRuleException exception)
            {
                Assert.AreEqual(OctopusAccountingRuleExceptionEnum.GenericAccountIsInvalid, exception.Code);
            }
        }

        [Test]
        public void Test_SaveFundingLineAccountingRule_When_GenericAccount_Is_Null()
        {
            FundingLineAccountingRule rule = new FundingLineAccountingRule
            {
                CreditAccount = new Account { Id = 2, Number = "1250" }
            };

            try
            {
                _accountingRuleServices.SaveAccountingRule(rule);
                Assert.Fail("Accounting Rule shouldn't pass validation test while trying to save (Generic Account is Null).");
            }
            catch (OctopusAccountingRuleException exception)
            {
                Assert.AreEqual(OctopusAccountingRuleExceptionEnum.GenericAccountIsInvalid, exception.Code);
            }
        }

        [Test]
        public void Test_SaveContractAccountingRule_When_SpecificAccount_Is_Null()
        {
            ContractAccountingRule rule = new ContractAccountingRule
            {
                DebitAccount = new Account { Id = 1, Number = "1150" },
                ClientType = OClientTypes.All,
                ProductType = OProductTypes.All
            };

            try
            {
                _accountingRuleServices.SaveAccountingRule(rule);
                Assert.Fail("Accounting Rule shouldn't pass validation test while trying to save (Generic Account is Null).");
            }
            catch (OctopusAccountingRuleException exception)
            {
                Assert.AreEqual(OctopusAccountingRuleExceptionEnum.SpecificAccountIsInvalid, exception.Code);
            }
        }

        [Test]
        public void Test_SaveFundingLineAccountingRule_When_SpecificAccount_Is_Null()
        {
            FundingLineAccountingRule rule = new FundingLineAccountingRule
            {
                DebitAccount = new Account { Id = 1, Number = "1150" }
            };

            try
            {
                _accountingRuleServices.SaveAccountingRule(rule);
                Assert.Fail("Accounting Rule shouldn't pass validation test while trying to save (Generic Account is Null).");
            }
            catch (OctopusAccountingRuleException exception)
            {
                Assert.AreEqual(OctopusAccountingRuleExceptionEnum.SpecificAccountIsInvalid, exception.Code);
            }
        }

        [Test]
        public void Test_SaveContractAccountingRule_When_ClientType_Is_Null()
        {
            ContractAccountingRule rule = new ContractAccountingRule
            {
                DebitAccount = new Account { Id = 1, Number = "1150" },
                CreditAccount = new Account { Id = 2, Number = "1250" },
                ProductType = OProductTypes.All
            };

            try
            {
                _accountingRuleServices.SaveAccountingRule(rule);
                Assert.Fail("Accounting Rule shouldn't pass validation test while trying to save (Client Type is Null).");
            }
            catch (OctopusAccountingRuleException exception)
            {
                Assert.AreEqual(OctopusAccountingRuleExceptionEnum.ClientTypeIsInvalid, exception.Code);
            }
        }

        [Test]
        public void Test_SaveContractAccountingRule_When_ProductType_Is_Null()
        {
            ContractAccountingRule rule = new ContractAccountingRule
            {
                DebitAccount = new Account { Id = 1, Number = "1150" },
                CreditAccount = new Account { Id = 2, Number = "1250" },
                ClientType = OClientTypes.All

            };

            try
            {
                _accountingRuleServices.SaveAccountingRule(rule);
                Assert.Fail("Accounting Rule shouldn't pass validation test while trying to save (Product Type is Null).");
            }
            catch (OctopusAccountingRuleException exception)
            {
                Assert.AreEqual(OctopusAccountingRuleExceptionEnum.ProductTypeIsInvalid, exception.Code);
            }
        }

        //[Test]
        //public void Test_SaveContractAccountingRule_WhenIsCorrect()
        //{
        //    ContractAccountingRule rule = new ContractAccountingRule
        //    {
        //        DebitAccount = new Account { Id = 1, Number = "1150" },
        //        CreditAccount = new Account { Id = 2, Number = "1250" },
        //        ProductType = OProductTypes.All,
        //        ClientType = OClientTypes.All
        //    };

            
        //    _accountingRuleManagerMock.ExpectAndReturn("AddAccountingRule", 1, rule, null);
            
        //    Assert.AreEqual(2, _accountingRuleServices.SaveAccountingRule(rule));
        //}

        [Test]
        public void Test_LateLoanProcessing()
        {
            Loan myContract = _SetContract(1000, 0.03m,  OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false, 1, new DateTime(2010, 6, 6), 6);
            Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI =
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2010, 7, 6), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2010, 8, 6), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2010, 10, 1));
            // good loan to the bad loan
            OverdueEvent e = myContract.GetOverdueEvent(new DateTime(2010, 10, 1));
            Assert.AreEqual(e.Code, "GLBL");
            
            myContract.Repay(1, new DateTime(2010, 10, 20), 500, false, true);
            //bad loan to the late loan
            e = myContract.GetOverdueEvent(new DateTime(2010, 10, 21));
            
            Assert.AreEqual(e.Code, "BLLL");
            myContract.Repay(1, new DateTime(2010, 11, 6), 500, false, true);
            //late loan to the good loan
            e = myContract.GetOverdueEvent(new DateTime(2010, 11, 6));
            
            Assert.AreEqual(e.Code, "LLGL");
            Assert.AreEqual(e.OLB, 243m);
        }

        [Test]
        public void Test_LateLoanProcessingDoCalculationTwoTimes()
        {
            Loan myContract = _SetContract(1000, 0.03m, OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false, 1, new DateTime(2010, 6, 6), 6);
            Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI =
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2010, 7, 6), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2010, 8, 6), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2010, 10, 1));
            // good loan to the bad loan
            OverdueEvent e = myContract.GetOverdueEvent(new DateTime(2010, 10, 1));
            Assert.AreEqual(e.Code, "GLBL");
            e = myContract.GetOverdueEvent(new DateTime(2010, 10, 20));
            Assert.AreEqual(e, null);
            myContract.Repay(1, new DateTime(2010, 10, 20), 500, false, true);
            //bad loan to the late loan
            e = myContract.GetOverdueEvent(new DateTime(2010, 10, 21));
            
            Assert.AreEqual(e.Code, "BLLL");
            myContract.Repay(1, new DateTime(2010, 11, 6), 500, false, true);
            //late loan to the good loan
            e = myContract.GetOverdueEvent(new DateTime(2010, 11, 6));
            
            Assert.AreEqual(e.Code, "LLGL");
            Assert.AreEqual(e.OLB, 243m);
        }

        [Test]
        public void Test_RescheduleGoodLoanProcessing()
        {
            Loan myContract = _SetContract(1000, 0.03m, OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false, 1, new DateTime(2010, 6, 6), 6);
            Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI =
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2010, 7, 6), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2010, 8, 6), myContract.GetInstallment(1).ExpectedDate);

            myContract.Repay(1, new DateTime(2010, 7, 6), 230, false, true);
            myContract.Repay(2, new DateTime(2010, 8, 6), 230, false, true);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2010, 9, 1));
            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 2,
                RepaymentDateOffset = 0,
                ChargeInterestDuringShift = true,
                ReschedulingDate = new DateTime(2010, 9, 1)
            };
            myContract.Reschedule(ro);
            myContract.AddRecheduleTransformationEvent(ro.ReschedulingDate);
            List<OverdueEvent> list = myContract.Events.GetOverdueEvents();
            Assert.AreEqual(list[0].Code, "GLRL");
            // good loan to the bad loan
            OverdueEvent e = myContract.GetOverdueEvent(new DateTime(2010, 10, 1));
            Assert.AreEqual(e.Code, "GLLL");
            e = myContract.GetOverdueEvent(new DateTime(2010, 10, 20));
            Assert.AreEqual(e, null);
            myContract.Repay(1, new DateTime(2010, 10, 20), 500, false, true);
            //bad loan to the late loan
            e = myContract.GetOverdueEvent(new DateTime(2010, 10, 21));

            Assert.AreEqual(e.Code, "LLGL");
            myContract.Repay(1, new DateTime(2010, 11, 6), 500, false, true);
            //late loan to the good loan
        }

        [Test]
        public void Test_RescheduleLateLoanProcessing()
        {
            Loan myContract = _SetContract(1000, 0.03m, OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false, 1, new DateTime(2010, 6, 6), 6);
            Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI =
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2010, 7, 6), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2010, 8, 6), myContract.GetInstallment(1).ExpectedDate);

            myContract.Repay(1, new DateTime(2010, 7, 6), 230, false, true);
            myContract.Repay(2, new DateTime(2010, 8, 6), 230, false, true);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2010, 10, 1));
            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 2,
                RepaymentDateOffset = 0,
                ChargeInterestDuringShift = true,
                ReschedulingDate = new DateTime(2010, 10, 1)
            };
            myContract.Reschedule(ro);
            myContract.AddRecheduleTransformationEvent(ro.ReschedulingDate);
            List<OverdueEvent> list = myContract.Events.GetOverdueEvents();
            Assert.AreEqual(list[0].Code, "LLRL");
            
            // good loan to the bad loan
            OverdueEvent e = myContract.GetOverdueEvent(new DateTime(2010, 10, 1));
            Assert.AreEqual(e.Code, "GLLL");
            e = myContract.GetOverdueEvent(new DateTime(2010, 10, 20));
            Assert.AreEqual(e, null);
            myContract.Repay(1, new DateTime(2010, 10, 20), 500, false, true);
            //bad loan to the late loan
            e = myContract.GetOverdueEvent(new DateTime(2010, 10, 21));

            Assert.AreEqual(e.Code, "LLGL");
            myContract.Repay(1, new DateTime(2010, 11, 6), 500, false, true);
            //late loan to the good loan
        }

        [Test]
        public void Test_RescheduleBadLoanProcessing()
        {
            Loan myContract = _SetContract(1000, 0.03m, OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false, 1, new DateTime(2010, 6, 6), 6);
            Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI =
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2010, 7, 6), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2010, 8, 6), myContract.GetInstallment(1).ExpectedDate);

            myContract.Repay(1, new DateTime(2010, 7, 6), 230, false, true);
            myContract.Repay(2, new DateTime(2010, 8, 6), 230, false, true);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2010, 12, 1));
            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 2,
                RepaymentDateOffset = 0,
                ChargeInterestDuringShift = true,
                ReschedulingDate = new DateTime(2010,12, 1)
            };
            myContract.Reschedule(ro);
            myContract.AddRecheduleTransformationEvent(ro.ReschedulingDate);
            List<OverdueEvent> list = myContract.Events.GetOverdueEvents();
            Assert.AreEqual(list[0].Code, "BLRL");
            // good loan to the bad loan
            OverdueEvent e = myContract.GetOverdueEvent(new DateTime(2010, 10, 1));
            Assert.AreEqual(e.Code, "GLLL");
            e = myContract.GetOverdueEvent(new DateTime(2010, 10, 20));
            Assert.AreEqual(e, null);
            myContract.Repay(1, new DateTime(2010, 10, 20), 500, false, true);
            //bad loan to the late loan
            e = myContract.GetOverdueEvent(new DateTime(2010, 10, 21));

            Assert.AreEqual(e.Code, "LLGL");
            myContract.Repay(1, new DateTime(2010, 11, 6), 500, false, true);
            //late loan to the good loan
        }

        [Test]
        public void Test_ProvisionLoanProcessing()
        {
            Loan myContract = _SetContract(1000, 0.03m, OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false, 1, new DateTime(2010, 6, 6), 6);
            Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI =
                _SetRepaymentOptions(myContract, false);

            ProvisionTable provisionTable = ProvisionTable.GetInstance(new User());
            provisionTable.ProvisioningRates = new List<ProvisioningRate>();
            provisionTable.Add(new ProvisioningRate { Number = 1, NbOfDaysMin = 0, NbOfDaysMax = 0, Rate = 0.5 });
            provisionTable.Add(new ProvisioningRate { Number = 2, NbOfDaysMin = 1, NbOfDaysMax = 30, Rate = 1 });
            provisionTable.Add(new ProvisioningRate { Number = 3, NbOfDaysMin = 31, NbOfDaysMax = 60, Rate = 1.5 });
            provisionTable.Add(new ProvisioningRate { Number = 3, NbOfDaysMin = 60, NbOfDaysMax = 999, Rate = 2 });
            

            Assert.AreEqual(new DateTime(2010, 7, 6), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2010, 8, 6), myContract.GetInstallment(1).ExpectedDate);

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2010, 10, 1));

            ProvisionEvent e = myContract.GetProvisionEvent(new DateTime(2010, 10, 1), provisionTable);
            Assert.AreEqual(e.Code, "LLPE");
            Assert.AreEqual(e.Amount, 2000);
            myContract.Events.Add(e);
            myContract.Repay(1, new DateTime(2010, 10, 20), 500, false, true);
        }

        [Test]
        public void Test_ProvisionRescheduledLoanProcessing()
        {
            Loan myContract = _SetContract(1000, 0.03m, OLoanTypes.Flat, new NonRepaymentPenalties(0, 0, 0.003, 0), false, 1, new DateTime(2010, 6, 6), 6);
            Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI =
                _SetRepaymentOptions(myContract, false);

            ProvisionTable provisionTable = ProvisionTable.GetInstance(new User());
            provisionTable.ProvisioningRates = new List<ProvisioningRate>();
            provisionTable.Add(new ProvisioningRate { Number = 1, NbOfDaysMin = 0, NbOfDaysMax = 0, Rate = 0.5 });
            provisionTable.Add(new ProvisioningRate { Number = 2, NbOfDaysMin = 1, NbOfDaysMax = 30, Rate = 1 });
            provisionTable.Add(new ProvisioningRate { Number = 3, NbOfDaysMin = 31, NbOfDaysMax = 60, Rate = 1.5 });
            provisionTable.Add(new ProvisioningRate { Number = 3, NbOfDaysMin = 60, NbOfDaysMax = 999, Rate = 2 });
            provisionTable.Add(new ProvisioningRate { Number = 4, NbOfDaysMin = -1, NbOfDaysMax = -1, Rate = 1 });


            Assert.AreEqual(new DateTime(2010, 7, 6), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2010, 8, 6), myContract.GetInstallment(1).ExpectedDate);
            myContract.Repay(1, new DateTime(2010, 8, 6), 100, false, false);

            ReschedulingOptions ro = new ReschedulingOptions
            {
                InterestRate = 0.04m,
                NewInstallments = 2,
                RepaymentDateOffset = 0,
                ChargeInterestDuringShift = true,
                ReschedulingDate = new DateTime(2010, 12, 1)
            };
            myContract.Reschedule(ro);
            myContract.Rescheduled = true;

            rLI.CalculateNewInstallmentsWithLateFees(new DateTime(2010, 10, 1));

            ProvisionEvent e = myContract.GetProvisionEvent(new DateTime(2010, 10, 1), provisionTable);
            Assert.AreEqual(e.Code, "LLPE");
            Assert.AreEqual(e.Amount, 1000);
            myContract.Events.Add(e);
        }

        [Test]
        public void Test_AccruedInterestProcessing()
        {
            ApplicationSettings generalSettings = ApplicationSettings.GetInstance("");

            generalSettings.DeleteAllParameters();
            generalSettings.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalSettings.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
            generalSettings.AddParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
            generalSettings.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
            generalSettings.AddParameter(OGeneralSettings.BAD_LOAN_DAYS, 50);
            generalSettings.AddParameter(OGeneralSettings.CEASE_LAIE_DAYS, 100);

            Loan myContract = _SetContract(6000, 0.0833m, OLoanTypes.Flat,
                                           new NonRepaymentPenalties(0, 0, 0, 0), false, 0,
                                           new DateTime(2010, 10, 15), 3);
            Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLi =
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2010, 11, 15), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2010, 12, 15), myContract.GetInstallment(1).ExpectedDate);

            RepaymentEvent rpeEvent = myContract.Repay(1, new DateTime(2010, 11, 15), 2500, false, true);
            Assert.AreEqual(rpeEvent.Interests, 500m);
            rpeEvent = myContract.Repay(2, new DateTime(2010, 12, 10), 300, false, true);
            Assert.AreEqual(rpeEvent.Interests, 300);

            AccruedInterestEvent e = myContract.GetAccruedInterestEvent(new DateTime(2010, 12, 31));
            Assert.AreEqual(e.Code, "LIAE");
            Assert.AreEqual(e.AccruedInterest, 1242);
            myContract.Events.Add(e);
        }

        [Test]
        public void Test_AccruedInterestProcessingCeaseDays()
        {
            ApplicationSettings generalSettings = ApplicationSettings.GetInstance("");

            generalSettings.DeleteAllParameters();
            generalSettings.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalSettings.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
            generalSettings.AddParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
            generalSettings.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
            generalSettings.AddParameter(OGeneralSettings.BAD_LOAN_DAYS, 50);
            generalSettings.AddParameter(OGeneralSettings.CEASE_LAIE_DAYS, 10);

            Loan myContract = _SetContract(6000, 0.0833m, OLoanTypes.Flat,
                                           new NonRepaymentPenalties(0, 0, 0, 0), false, 0,
                                           new DateTime(2010, 10, 15), 3);

            Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLi =
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2010, 11, 15), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2010, 12, 15), myContract.GetInstallment(1).ExpectedDate);

            RepaymentEvent rpeEvent = myContract.Repay(1, new DateTime(2010, 11, 15), 2500, false, true);
            Assert.AreEqual(rpeEvent.Interests, 500m);
            rpeEvent = myContract.Repay(2, new DateTime(2010, 12, 10), 300, false, true);
            Assert.AreEqual(rpeEvent.Interests, 300);

            AccruedInterestEvent e = myContract.GetAccruedInterestEvent(new DateTime(2010, 12, 31));
            Assert.AreEqual(e.Code, "LIAE");
            Assert.AreEqual(e.AccruedInterest, 1151);
            myContract.Events.Add(e);
        }

        [Test]
        public void TestAccruedInterestProcessingNegativeCase()
        {
            Loan myContract = _SetContract(6000, 0.0833m, OLoanTypes.Flat,
                                           new NonRepaymentPenalties(0, 0, 0, 0), false, 0,
                                           new DateTime(2010, 10, 15), 3);
            Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments rLI =
                _SetRepaymentOptions(myContract, false);

            Assert.AreEqual(new DateTime(2010, 11, 15), myContract.GetInstallment(0).ExpectedDate);
            Assert.AreEqual(new DateTime(2010, 12, 15), myContract.GetInstallment(1).ExpectedDate);

            RepaymentEvent rpeEvent = myContract.Repay(1, new DateTime(2010, 11, 15), 2500, false, true);
            Assert.AreEqual(rpeEvent.Interests, 500m);
            rpeEvent = myContract.Repay(2, new DateTime(2010, 12, 15), 2500, false, true);
            Assert.AreEqual(rpeEvent.Interests, 500);
            rpeEvent = myContract.Repay(3, new DateTime(2010, 12, 15), 300, false, true);
            Assert.AreEqual(rpeEvent.Interests, 300);

            AccruedInterestEvent e = myContract.GetAccruedInterestEvent(new DateTime(2010, 12, 31));
            //Assert.AreEqual(e, null);
        }

        private static Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments _SetRepaymentOptions(Loan pContract, bool pCancelFees)
        {
            CreditContractOptions cCO = new CreditContractOptions(pContract.Product.LoanType, pContract.Product.KeepExpectedInstallment, pCancelFees, 0, 0, false, 0,
                                                                  pContract.Product.AnticipatedTotalRepaymentPenaltiesBase);

            return new Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments.CalculateInstallments(cCO, pContract, new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""));
        }

        private static Loan _SetContract(OCurrency amount, decimal rate, OLoanTypes loansType, NonRepaymentPenalties nonRepaymentFees, bool keepExpectedInstallment, int numberOfGracePeriod, DateTime stratDate, int nbInstallment)
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = loansType,
                ChargeInterestWithinGracePeriod = true,
                Currency = new Currency { Id = 1 }
            };

            package.KeepExpectedInstallment = keepExpectedInstallment;
            package.AnticipatedTotalRepaymentPenaltiesBase = OAnticipatedRepaymentPenaltiesBases.RemainingOLB;
            Loan myContract = new Loan(package, amount, rate, nbInstallment, numberOfGracePeriod, stratDate, new User(),
                                       ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""),
                                       ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
            myContract.BadLoan = false;
            myContract.AnticipatedTotalRepaymentPenalties = 0;
            myContract.NonRepaymentPenalties = nonRepaymentFees;
            return myContract;
        }
    }
}
