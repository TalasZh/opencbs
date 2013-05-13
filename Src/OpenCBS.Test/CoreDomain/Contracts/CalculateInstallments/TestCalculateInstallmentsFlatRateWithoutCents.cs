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
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Test.CoreDomain.Contracts.CalculateInstallments
{
    [TestFixture]
    public class FlatRateWithoutCents
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            NonWorkingDateSingleton holidays = NonWorkingDateSingleton.GetInstance("");
            holidays.WeekEndDay1 = 6;
            holidays.WeekEndDay2 = 0;
            holidays.PublicHolidays = new Dictionary<DateTime, string>
                                          {
                                              {new DateTime(2006, 1, 1), "New Year Eve"},
                                              {new DateTime(2006, 12, 25), "Christmas"}
                                          };
            ApplicationSettings generalSettings = ApplicationSettings.GetInstance("");

            generalSettings.DeleteAllParameters();
            generalSettings.AddParameter(OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE, false);
            generalSettings.AddParameter(OGeneralSettings.INCREMENTALDURINGDAYOFF, true);
            generalSettings.AddParameter(OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE, true);
            generalSettings.AddParameter(OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS, true);
        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_NoGracePeriod_RoundingOnCapital()
        {
            Loan myContract = GetContract(1000, 6, 0.03m);

            Assert.AreEqual(6, myContract.InstallmentList.Count);

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 30, 167, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 30, 167, 833);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 30, 167, 666);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 30, 166, 499);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 30, 167, 333);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 30, 166, 166);
        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_NoGracePeriod()
        {
            Loan myContract = GetContract(1000, 5, 0.03m);

            Assert.AreEqual(5, myContract.InstallmentList.Count);

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 30, 200, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 30, 200, 800);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 30, 200, 600);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 30, 200, 400);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 30, 200, 200);
        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_NoGracePeriod_RoundingOnInterest()
        {
            Loan myContract = GetContract(1000, 5, 0.0033m);

            Assert.AreEqual(5, myContract.InstallmentList.Count);

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 3, 200, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 4, 200, 800);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 3, 200, 600);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 4, 200, 400);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 3, 200, 200);
        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_NoGracePeriod_RoundingInterest_RoundingCapital()
        {
            Loan myContract = GetContract(1000, 6, 0.0033m);

            Assert.AreEqual(6, myContract.InstallmentList.Count);

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 3, 167, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 3, 167, 833);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 4, 167, 666);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 3, 166, 499);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 4, 167, 333);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 3, 166, 166);

        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_GracePeriod_ChargeInterestDuringGracePeriod()
        {
            Loan myContract = GetContract(1000, 6, 0.03m, 1, true);

            Assert.AreEqual(6, myContract.InstallmentList.Count);

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 30, 0, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 30, 200, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 30, 200, 800);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 30, 200, 600);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 30, 200, 400);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 30, 200, 200);
        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_GracePeriod_DontChargeInterestDuringGracePeriod()
        {
            Loan myContract = GetContract(1000, 6, 0.03m, 1, false);

            Assert.AreEqual(6, myContract.InstallmentList.Count);

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 0, 0, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 30, 200, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 30, 200, 800);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 30, 200, 600);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 30, 200, 400);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 30, 200, 200);
        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_GracePeriod_DontChargeInterestDuringGracePeriod_RoundingInterest()
        {
            Loan myContract = GetContract(1000, 6, 0.0033m, 1, false);

            Assert.AreEqual(6, myContract.InstallmentList.Count);

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 0, 0, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 3, 200, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 4, 200, 800);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 3, 200, 600);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 4, 200, 400);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 3, 200, 200);
        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_GracePeriod_DontChargeInterestDuringGracePeriod_RoundingInterest_RoundingCapital()
        {
            Loan myContract = GetContract(1000, 7, 0.03m, 1, false);

            Assert.AreEqual(7, myContract.InstallmentList.Count);

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 0, 0, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 30, 167, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 30, 167, 833);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 30, 167, 666);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 30, 166, 499);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 30, 167, 333);

        }


        [Test]
        public void FlatRate_MonthlyInstallmentType_ExoticInstallments_GracePeriod()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                ExoticProduct = new ExoticInstallmentsTable
                                                              {
                                                                  new ExoticInstallment(1, 0.1, 0),
                                                                  new ExoticInstallment(2, 0.1, 0),
                                                                  new ExoticInstallment(3, 0.1, 0),
                                                                  new ExoticInstallment(4, 0.1, 0),
                                                                  new ExoticInstallment(5, 0.1, 0.5),
                                                                  new ExoticInstallment(6, 0.5, 0.5)
                                                              },
                Currency = new Currency { Id = 1 }
            };

            Loan myContract = new Loan(package, 1000, 0.03m, 7, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 30, 0, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 0, 100, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 0, 100, 900);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 0, 100, 800);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 0, 100, 700);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 90, 100, 600);
            _AssertSpecifiedInstallment(myContract.GetInstallment(6), 7, new DateTime(2006, 8, 1), 90, 500, 500);
        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_ExoticInstallments_GracePeriod_50PercentMiddle_50PercentEnd()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                ExoticProduct = new ExoticInstallmentsTable
                                                              {
                                                                  new ExoticInstallment(1, 0, 0),
                                                                  new ExoticInstallment(2, 0, 0),
                                                                  new ExoticInstallment(3, 0.5, 0.5),
                                                                  new ExoticInstallment(4, 0, 0),
                                                                  new ExoticInstallment(5, 0, 0),
                                                                  new ExoticInstallment(6, 0.5, 0.5)
                                                              },
                Currency = new Currency { Id = 1 }
            };

            Loan myContract = new Loan(package, 1000, 0.02m, 7, 1, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 20, 0, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 0, 0, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 0, 0, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 60, 500, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 0, 0, 500);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 0, 0, 500);
            _AssertSpecifiedInstallment(myContract.GetInstallment(6), 7, new DateTime(2006, 8, 1), 60, 500, 500);
        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_ExoticInstallments_NoGracePeriod()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                ExoticProduct = new ExoticInstallmentsTable
                                                              {
                                                                  new ExoticInstallment(1, 0.1, 0),
                                                                  new ExoticInstallment(2, 0.1, 0),
                                                                  new ExoticInstallment(3, 0.1, 0),
                                                                  new ExoticInstallment(4, 0.1, 0),
                                                                  new ExoticInstallment(5, 0.1, 0),
                                                                  new ExoticInstallment(6, 0.5, 1)
                                                              },
                Currency = new Currency { Id = 1 }
            };

            Loan myContract = new Loan(package, 1000, 0.03m, 6, 0, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 0, 100, 1000);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 0, 100, 900);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 0, 100, 800);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 0, 100, 700);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 0, 100, 600);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 180, 500, 500);
        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_ExoticInstallments_NoGracePeriod_RoundingInterest_RoundingCapital()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                ExoticProduct = new ExoticInstallmentsTable
                                                              {
                                                                  new ExoticInstallment(1, 0.1, 0.1),
                                                                  new ExoticInstallment(2, 0.1, 0.1),
                                                                  new ExoticInstallment(3, 0.1, 0.5),
                                                                  new ExoticInstallment(4, 0.1, 0.1),
                                                                  new ExoticInstallment(5, 0.1, 0.1),
                                                                  new ExoticInstallment(6, 0.5, 0.1)
                                                              },
                Currency = new Currency { Id = 1 }
            };

            Loan myContract = new Loan(package, 987, 0.03m, 6, 0, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 18, 99, 987);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 18, 98, 888);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 88, 99, 790);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 18, 99, 691);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 18, 98, 592);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 18, 494, 494);
        }

        [Test]
        public void FlatRate_MonthlyInstallmentType_ExoticInstallments_GracePeriod_RoundingInterest_RoundingCapital()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = true,
                ExoticProduct = new ExoticInstallmentsTable
                                                              {
                                                                  new ExoticInstallment(1, 0.1, 0.1),
                                                                  new ExoticInstallment(2, 0.1, 0.1),
                                                                  new ExoticInstallment(3, 0.1, 0.5),
                                                                  new ExoticInstallment(4, 0.1, 0.1),
                                                                  new ExoticInstallment(5, 0.1, 0.1),
                                                                  new ExoticInstallment(6, 0.5, 0.1)
                                                              },
                Currency = new Currency { Id = 1 }
            };

            Loan myContract = new Loan(package, 987, 0.03m, 8, 2, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 30, 0, 987);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 29, 0, 987);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 18, 99, 987);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 18, 98, 888);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 89, 99, 790);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 18, 99, 691);
            _AssertSpecifiedInstallment(myContract.GetInstallment(6), 7, new DateTime(2006, 8, 1), 18, 98, 592);
            _AssertSpecifiedInstallment(myContract.GetInstallment(7), 8, new DateTime(2006, 9, 1), 17, 494, 494);
        }



        private static void _AssertSpecifiedInstallment(Installment pInstallment, int pNumber, DateTime pExpectedDate, OCurrency pInterestRepayment, OCurrency pCapitalRepayment, OCurrency pOLB)
        {
            Assert.AreEqual(pNumber, pInstallment.Number);
            Assert.AreEqual(pExpectedDate, pInstallment.ExpectedDate);
            Assert.AreEqual(pInterestRepayment.Value, pInstallment.InterestsRepayment.Value);
            Assert.AreEqual(pCapitalRepayment.Value, pInstallment.CapitalRepayment.Value);
            Assert.AreEqual(pOLB.Value, pInstallment.OLB.Value);
        }

        private static Loan GetContract(OCurrency pAmount, int pNumberOfInstallments, decimal pRate)
        {
            return GetContract(pAmount, pNumberOfInstallments, pRate, 0, false);
        }

        private static Loan GetContract(OCurrency pAmount, int pNumberOfInstallments, decimal pRate, int pGracePeriod, bool pChargeInterestDuringGracePeriod)
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = pChargeInterestDuringGracePeriod,
                Currency = new Currency { Id = 1 }
            };
            return new Loan(package, pAmount, pRate, pNumberOfInstallments, pGracePeriod, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));
        }

        [Test]
        public void FlatRate_NewRoundCalculation_MonthlyInstallmentType_ExoticInstallments_NoGracePeriod()
        {
            LoanProduct package = new LoanProduct
            {
                InstallmentType = new InstallmentType(1, "Monthly", 0, 1),
                LoanType = OLoanTypes.Flat,
                ChargeInterestWithinGracePeriod = false,
                ExoticProduct = new ExoticInstallmentsTable
                                                              {
                                                                  new ExoticInstallment(1, 0, 0),
                                                                  new ExoticInstallment(2, 0, 0.5),
                                                                  new ExoticInstallment(3, 0.5, 0),
                                                                  new ExoticInstallment(4, 0, 0),
                                                                  new ExoticInstallment(5, 0, 0),
                                                                  new ExoticInstallment(6, 0.5, 0.5)
                                                              },
                Currency = new Currency { Id = 1 }
            };

            Loan myContract = new Loan(package, 1001, 0.03m, 6, 0, new DateTime(2006, 1, 1), new User(), ApplicationSettings.GetInstance(""), NonWorkingDateSingleton.GetInstance(""), ProvisionTable.GetInstance(new User()), ChartOfAccounts.GetInstance(new User()));

            _AssertSpecifiedInstallment(myContract.GetInstallment(0), 1, new DateTime(2006, 2, 1), 0, 0, 1001);
            _AssertSpecifiedInstallment(myContract.GetInstallment(1), 2, new DateTime(2006, 3, 1), 90, 0, 1001);
            _AssertSpecifiedInstallment(myContract.GetInstallment(2), 3, new DateTime(2006, 4, 3), 0, 500, 1001);
            _AssertSpecifiedInstallment(myContract.GetInstallment(3), 4, new DateTime(2006, 5, 1), 0, 0, 501);
            _AssertSpecifiedInstallment(myContract.GetInstallment(4), 5, new DateTime(2006, 6, 1), 0, 0, 501);
            _AssertSpecifiedInstallment(myContract.GetInstallment(5), 6, new DateTime(2006, 7, 3), 90, 501, 501);
        }
    }
}
