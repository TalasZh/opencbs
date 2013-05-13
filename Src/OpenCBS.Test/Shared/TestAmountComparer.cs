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
