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
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Events;

namespace OpenCBS.Test.CoreDomain
{
	/// <summary>
	/// Summary description for TestBadLoanRepaymentEvent.
	/// </summary>
	[TestFixture]
	public class TestBadLoanRepaymentEvent
	{
		[Test]
		public void TestCodeCorrectlyGetAndRetrieved()
		{
			BadLoanRepaymentEvent BLDE = new BadLoanRepaymentEvent();
			Assert.AreEqual("RBLE", BLDE.Code);
		}

		[Test]
		public void TestAccruedProvisionCorrectlyGetAndRetrieved()
		{
			BadLoanRepaymentEvent BLDE = new BadLoanRepaymentEvent();
			BLDE.AccruedProvision = 100.367m;
			Assert.AreEqual(100.367m, BLDE.AccruedProvision.Value);
		}

		[Test]
		public void TestOLBCorrectlyGetAndRetrieved()
		{
			BadLoanRepaymentEvent BLDE = new BadLoanRepaymentEvent();
			BLDE.OLB = 100.367m;
			Assert.AreEqual(100.367m, BLDE.OLB.Value);
		}

		[Test]
		public void TestCancelableCorrectlySetAndRetrieved()
		{
			BadLoanRepaymentEvent BLDE = new BadLoanRepaymentEvent();
			BLDE.Cancelable = true;
			Assert.IsTrue(BLDE.Cancelable);
		}

		[Test]
		public void TestCopyBadLoanRepaymentEvent()
		{
            BadLoanRepaymentEvent BLDE = new BadLoanRepaymentEvent{InstallmentNumber = 1,Principal = 100.34m,Interests = 100,Commissions = 200.34m,Penalties = 0,AccruedProvision = 294.34m,ClientType = OClientTypes.Group};
			BadLoanRepaymentEvent BLDECopy = BLDE.Copy() as BadLoanRepaymentEvent;
			Assert.AreEqual(BLDE.AccruedProvision.Value, BLDECopy.AccruedProvision.Value);
			Assert.AreEqual(BLDE.Code, BLDECopy.Code);
            Assert.AreEqual(BLDE.Fees.Value, BLDECopy.Fees.Value);
			Assert.AreEqual(BLDE.Id, BLDECopy.Id);
			Assert.AreEqual(BLDE.InstallmentNumber, BLDECopy.InstallmentNumber);
            Assert.AreEqual(BLDE.Interests.Value, BLDECopy.Interests.Value);
            if(BLDE.OLB.HasValue)
                Assert.AreEqual(BLDE.OLB.Value, BLDECopy.OLB.Value);
            if(BLDE.Principal.HasValue)
			Assert.AreEqual(BLDE.Principal.Value, BLDECopy.Principal.Value);
		}
	}
}
