// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain;
using NUnit.Framework;
using OpenCBS.CoreDomain.Accounting;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Summary description for TestExchangeRate.
	/// </summary>
	[TestFixture]
	public class TestExchangeRate
	{
		ExchangeRate exchangeRate = new ExchangeRate();

		[Test]
		public void TestIfDateIsCorrectlySetAndRetrieved()
		{
			exchangeRate.Date = new DateTime(2006,7,7);
			Assert.AreEqual(new DateTime(2006,7,7),exchangeRate.Date);
		}

		[Test]
		public void TestIfRateIsCorrectlySetAndRetrieved()
		{
			exchangeRate.Rate = 3.23;
			Assert.AreEqual(3.23,exchangeRate.Rate);
		}
	}
}
