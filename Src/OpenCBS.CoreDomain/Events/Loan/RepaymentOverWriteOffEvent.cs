// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Events.Loan;

namespace OpenCBS.CoreDomain.Events
{
    [Serializable]
    public class RepaymentOverWriteOffEvent : RepaymentEvent
    {
		public override string Code
		{
			get{return "ROWO";}
		}

		public override Event Copy()
		{
            return (RepaymentOverWriteOffEvent)MemberwiseClone();
		}
	}
}
