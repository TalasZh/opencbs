// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Events.Loan;

namespace OpenCBS.CoreDomain.Events
{
    [Serializable]
	public class RescheduledLoanRepaymentEvent : RepaymentEvent
	{
		public override string Code
		{
			get{return "RRLE";}
		}

		public override Event Copy()
		{
			return (RescheduledLoanRepaymentEvent)MemberwiseClone();
		}
	}
}
