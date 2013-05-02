// LICENSE PLACEHOLDER

using System;
using NUnit.Framework;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Events;

namespace OpenCBS.Test.CoreDomain
{
	[TestFixture]
	public class TestEvent
	{
		private LoanDisbursmentEvent loanDisbursmentEvent;

		[SetUp]
		public void SetUp()
		{		
			loanDisbursmentEvent = new LoanDisbursmentEvent();
		}

		[Test]
		public void TestIfEventIdCorrectlySetAndRetrieved()
		{
			loanDisbursmentEvent.Id =1;
			Assert.AreEqual(1,loanDisbursmentEvent.Id);
		}

		[Test]
		public void TestIfEventCancelableCorrectlySetAndRetrieved()
		{
			loanDisbursmentEvent.Cancelable =true;
			Assert.IsTrue(loanDisbursmentEvent.Cancelable);
		}

		[Test]
		public void TestIfLoanDisbursmentEventCodeCorrectlySetAndRetrieved()
		{
			LoanDisbursmentEvent lODE = new LoanDisbursmentEvent();
			Assert.AreEqual("LODE",lODE.Code);
		}

		[Test]
		public void TestIfReschedulingOfALoanCodeCorrectlySetAndRetrieved()
		{
			RescheduleLoanEvent rOLE = new RescheduleLoanEvent();
			Assert.AreEqual("ROLE",rOLE.Code);
		}

		[Test]
		public void TestIfWriteOffEventCodeCorrectlySetAndRetrieved()
		{
			WriteOffEvent wROE = new WriteOffEvent();
			Assert.AreEqual("WROE",wROE.Code);
		}

		[Test]
		public void TestRepaymentEventCodeCorrectlyGeneratedWhenGoodLoan()
		{
            RepaymentEvent rPE = new RepaymentEvent();
			Assert.AreEqual("RGLE",rPE.Code);
		}

		[Test]
		public void TestRepaymentEventCodeCorrectlyGeneratedWhenBadLoanAndRescheduled()
		{
            BadLoanRepaymentEvent bRE = new BadLoanRepaymentEvent();
			Assert.AreEqual("RBLE",bRE.Code);
		}

		[Test]
		public void TestRepaymentEventCodeCorrectlyGeneratedWhenBadLoanAndNotRescheduled()
		{
            RescheduledLoanRepaymentEvent rPE = new RescheduledLoanRepaymentEvent();
			Assert.AreEqual("RRLE",rPE.Code);
		}

		[Test]
		public void TestIfEventDateCorrectlySetAndRetrieved()
		{
			loanDisbursmentEvent.Date = new DateTime(2004,2,23);
			DateTime date = new DateTime(2004,2,23);
			Assert.AreEqual(date,loanDisbursmentEvent.Date);
		}

		[Test]
		public void TestIfEventUserCorrectlySetAndRetrieved()
		{
			User user = new User {UserName = "NBA"};
		    loanDisbursmentEvent.User = user;
			Assert.AreEqual("NBA",loanDisbursmentEvent.User.UserName);
		}
		

		[Test]
		public void TestLoanDisbursmentEventAmount()
		{
			LoanDisbursmentEvent LDIE = new LoanDisbursmentEvent {Amount = 432.3m};
		    Assert.AreEqual(432.3m,LDIE.Amount.Value);
		}

		[Test]
		public void TestReschedulingOfALoanEventAmount()
		{
			RescheduleLoanEvent rSLE = new RescheduleLoanEvent();
			rSLE.Amount = 32.45m;
			Assert.AreEqual(32.45m,rSLE.Amount.Value);
		}

		[Test]
		public void TestWriteOffEventOutstandingAmount()
		{
			WriteOffEvent wROE = new WriteOffEvent();
			wROE.OLB = 3456.1m;
			Assert.AreEqual(3456.1m,wROE.OLB.Value);
		}

		[Test]
		public void TestWriteOffEventAccruedInterests()
		{
			WriteOffEvent wROE = new WriteOffEvent();
			wROE.AccruedInterests = 3456.1m;
			Assert.AreEqual(3456.1m,wROE.AccruedInterests.Value);
		}

		[Test]
		public void TestWriteOffEventAccruedPenalties()
		{
			WriteOffEvent wROE = new WriteOffEvent();
			wROE.AccruedPenalties = 3456.1m;
			Assert.AreEqual(3456.1m,wROE.AccruedPenalties.Value);
		}

		[Test]
		public void TestRepaymentEventPrincipal()
		{
			RepaymentEvent rPE = new RepaymentEvent();
			rPE.Principal = 100.43m;
			Assert.AreEqual(100.43m,rPE.Principal.Value);
		}

		[Test]
		public void TestRepaymentEventInterests()
		{
			RepaymentEvent rPE = new RepaymentEvent();
			rPE.Interests = 50.32m;
			Assert.AreEqual(50.32m,rPE.Interests.Value);
		}

		[Test]
		public void TestRepaymentEventFees()
		{
			RepaymentEvent rPE = new RepaymentEvent();
			rPE.Commissions = 20.53m;
			Assert.AreEqual(20.53m,rPE.Fees.Value);
		}

		[Test]
		public void TestRepaymentEventInstallmentNumber()
		{
			RepaymentEvent rPE = new RepaymentEvent();
			rPE.InstallmentNumber = 3;
			Assert.AreEqual(3,rPE.InstallmentNumber);
		}

		[Test]
		public void TestBadLoanRepaymentEventAccruedProvision()
		{
            BadLoanRepaymentEvent bRE = new BadLoanRepaymentEvent{Interests = 100,AccruedProvision = 23};
			Assert.AreEqual(100m,bRE.Interests.Value);
			Assert.AreEqual(23m,bRE.AccruedProvision.Value);
		}

		[Test]
		public void TestReschedulingOfALoanEventBadLoanBoolean()
		{
			RescheduleLoanEvent rSE = new RescheduleLoanEvent {BadLoan = true};
		    Assert.IsTrue(rSE.BadLoan);
		}

		[Test]
		public void TestEventCopy()
		{
            RepaymentEvent rE = new RepaymentEvent{InstallmentNumber = 1,Principal = 100,Interests = 230,Commissions = 4,Penalties = 0,ClientType = OClientTypes.Group};
			RepaymentEvent rEFakeCopy = rE;
			RepaymentEvent rECopy = rE.Copy() as RepaymentEvent;
			rE.Principal = 3456;
			Assert.AreEqual(3456m,rEFakeCopy.Principal.Value);
			Assert.AreEqual(100m,rECopy.Principal.Value);
		}
	}
}
