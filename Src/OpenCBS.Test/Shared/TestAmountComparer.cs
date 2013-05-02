// LICENSE PLACEHOLDER

using System;
using OpenCBS.Shared;
using NUnit.Framework;

namespace OpenCBS.Test.Shared
{
	[TestFixture]
	public class TestAmountComparer
	{
		[Test]
		public void TestEqualsWhenThe2NumbersAreReallyEquals()
		{
			Assert.IsTrue(AmountComparer.Equals(123.3456m,123.3456m));
		}

		[Test]
		public void TestEqualsWhenFirstNumberGreaterThanSecondOne()
		{
			Assert.IsFalse(AmountComparer.Equals(243m,242.97m));
		}

		[Test]
		public void TestEqualsWhenSecondNumberGreaterThanFirstOne()
		{
			Assert.IsFalse(AmountComparer.Equals(242.974321m,243.0001m));
		}

		[Test]
		public void TestEqualsWhenFirstNumberGreaterThanSecondOneButDifferenceLowerThan002()
		{
			Assert.IsTrue(AmountComparer.Equals(243.001m,243.02m));
		}

		[Test]
		public void TestEqualsWhenSecondNumberGreaterThanFirstOneButDifferenceLowerThan002()
		{
			Assert.IsTrue(AmountComparer.Equals(1567.1234m,1567.1238m));
		}

		[Test]
		public void TestCompareWhenAmount1MinusAmount2GreaterThan002()
		{
			Assert.IsTrue(AmountComparer.Compare(154.033m,154.001m) > 0);
		}

		[Test]
		public void TestCompareWhenAmount1MinusAmount2LowerThan002()
		{
			Assert.IsTrue(AmountComparer.Compare(154.033m,154.014m) == 0);
		}

		[Test]
		public void TestCompareWhenAmount2MinusAmount1LowerThan002()
		{
			Assert.IsTrue(AmountComparer.Compare(154.014m,154.033m) == 0);
		}

		[Test]
		public void TestCompareWhenAmount2MinusAmount1GreaterThan002()
		{
			Assert.IsTrue(AmountComparer.Compare(154.011m,154.033m) < 0);
		}
	}
}
