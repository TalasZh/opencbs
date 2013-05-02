// LICENSE PLACEHOLDER

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
