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
using OpenCBS.CoreDomain;
using NUnit.Framework;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Summary description for TestAmountCycle.
	/// </summary>
	[TestFixture]
	public class TestAmountCycle
	{
//		AmountCycle amountCycle = new AmountCycle();

		[Test]
		public void TestIfNumberIsCorrectlySetAndRetrieved()
		{
			Assert.Ignore();
//            amountCycle.Number = 2;
//			Assert.AreEqual(2,amountCycle.Number);
		}

		[Test]
		public void TestIfAmountMinIsCorrectlySetAndRetrieved()
		{
            Assert.Ignore();
//			amountCycle.Min = 100;
//			Assert.AreEqual(100m,amountCycle.Min.Value);
		}

		[Test]
		public void TestIfAmountMaxIsCorrectlySetAndRetrieved()
		{
            Assert.Ignore();
//			amountCycle.Max = 200.5m;
//			Assert.AreEqual(200.5m,amountCycle.Max.Value);
		}

		[Test]
		public void TestIfToStringReturnNumber()
		{
			Assert.Ignore();
//            amountCycle.Number = 2;
//			Assert.AreEqual("2",amountCycle.ToString());
		}
	}
}
