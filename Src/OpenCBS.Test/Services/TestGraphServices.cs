// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Mocks;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.Enums;
using OpenCBS.Manager.Accounting;
using OpenCBS.Manager.Contracts;
using OpenCBS.Services;
using OpenCBS.CoreDomain;
using System.Collections;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Shared.Settings;
using OpenCBS.Test.Manager;
using OpenCBS.Shared;

namespace OpenCBS.Test.Services
{
	/// <summary>
	/// Summary description for TestGraphServices.
	/// </summary>
	[TestFixture]
	public class TestGraphServices
	{
		private GraphServices graphServices;
		private DynamicMock mockContractManagement;
      
		private LoanManager contractManagement;
		private ChartOfAccounts chartOfAccounts;

      private readonly DataHelper addDataForTesting = new DataHelper();

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			mockContractManagement = new DynamicMock(typeof(LoanManager));
            chartOfAccounts = ChartOfAccounts.GetInstance(new User{Id = 5});
		}

		[SetUp]
		public void SetUp()
		{
            chartOfAccounts.Accounts = new List<Account>();
			Account cash = new Account();
			cash.DebitPlus = true;
			cash.Number = "1011";
			cash.Balance = 10000;
			cash.TypeCode = "CASH";
			chartOfAccounts.AddAccount(cash);
		}

		[Test]
		public void TestIfRealPrevisionCurveCorrectlyCalculate()
		{
			int forecastDays = 10;
			DateTime date = new DateTime(2006,1,1);

		    List<KeyValuePair<DateTime, decimal>> cashToDisburseByDay = new List<KeyValuePair<DateTime, decimal>>
		                                                                    {
		                                                                        new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 1), 100),
		                                                                        new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 2), 200),
		                                                                        new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 4), 400),
		                                                                        new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 6), 1233),
		                                                                        new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 9), 4876)
		                                                                    };

		    List<KeyValuePair<DateTime, decimal>> cashToRepayByDay = new List<KeyValuePair<DateTime, decimal>>
		                                                                 {
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 1), 100),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 2), 184),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 4), 3944),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 5), 5978),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 7), 6000),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 8), 6001),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 10), 6876)
		                                                                 };

			mockContractManagement.SetReturnValue("CalculateCashToDisburseByDay",cashToDisburseByDay);
			mockContractManagement.SetReturnValue("CalculateCashToRepayByDay",cashToRepayByDay);
		
			contractManagement = (LoanManager)mockContractManagement.MockInstance;
			this.graphServices = new GraphServices(contractManagement);
			double[] realPrevision = new double[forecastDays];

			realPrevision = this.graphServices.CalculateRealPrevisionCurve(date,forecastDays);

			Assert.AreEqual(10000,realPrevision[0]);
			Assert.AreEqual(9984,realPrevision[1]);
			Assert.AreEqual(9984,realPrevision[2]);
			Assert.AreEqual(13528,realPrevision[3]);
			Assert.AreEqual(19506,realPrevision[4]);
			Assert.AreEqual(18273,realPrevision[5]);
			Assert.AreEqual(24273,realPrevision[6]);
			Assert.AreEqual(30274,realPrevision[7]);
			Assert.AreEqual(25398,realPrevision[8]);
			Assert.AreEqual(32274,realPrevision[9]);
		}

		[Test]
		public void TestIfRealDisbursmentCurveCorrectlyCalculate()
		{
			int forecastDays = 8;
			DateTime date = new DateTime(2006,1,1);

            List<KeyValuePair<DateTime, decimal>> list = new List<KeyValuePair<DateTime, decimal>>
		                                                                    {
		                                                                        new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 1), 100),
		                                                                        new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 2), 200),
		                                                                        new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 4), 400),
		                                                                        new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 6), 1233),
		                                                                        new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 9), 4876)
		                                                                    };

			mockContractManagement.SetReturnValue("CalculateCashToDisburseByDay",list);
			contractManagement = (LoanManager)mockContractManagement.MockInstance;
			this.graphServices = new GraphServices(contractManagement);
			double[] disbursmentState = new double[forecastDays];

			disbursmentState = this.graphServices.CalculateRealDisbursmentCurve(date,forecastDays);
			Assert.AreEqual(100,disbursmentState[0]);
			Assert.AreEqual(300,disbursmentState[1]);
			Assert.AreEqual(300,disbursmentState[2]);
			Assert.AreEqual(700,disbursmentState[3]);
			Assert.AreEqual(700,disbursmentState[4]);
			Assert.AreEqual(1933,disbursmentState[5]);
			Assert.AreEqual(1933,disbursmentState[6]);
			Assert.AreEqual(1933,disbursmentState[7]);
		}

		[Test]
		public void TestIfRealRepayCurveCorrectlyCalculate()
		{
			int forecastDays = 10;
			DateTime date = new DateTime(2006,1,1);

            List<KeyValuePair<DateTime, decimal>> cashToRepayByDay = new List<KeyValuePair<DateTime, decimal>>
		                                                                 {
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 1), 100),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 2), 184),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 4), 3944),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 5), 5978),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 7), 6000),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 8), 6001),
		                                                                     new KeyValuePair<DateTime, decimal>(new DateTime(2006, 1, 10), 6876)
		                                                                 };
			mockContractManagement.SetReturnValue("CalculateCashToRepayByDay",cashToRepayByDay);
		
			contractManagement = (LoanManager)mockContractManagement.MockInstance;
			this.graphServices = new GraphServices(contractManagement);
			double[] realPrevision = new double[forecastDays];
			realPrevision = this.graphServices.CalculateRealRepayCurve(date,forecastDays);

			Assert.AreEqual(100,realPrevision[0]);
			Assert.AreEqual(284,realPrevision[1]);
			Assert.AreEqual(284,realPrevision[2]);
			Assert.AreEqual(4228,realPrevision[3]);
			Assert.AreEqual(10206,realPrevision[4]);
			Assert.AreEqual(10206,realPrevision[5]);
			Assert.AreEqual(16206,realPrevision[6]);
			Assert.AreEqual(22207,realPrevision[7]);
			Assert.AreEqual(22207,realPrevision[8]);
			Assert.AreEqual(29083,realPrevision[9]);
		}
	}
}
