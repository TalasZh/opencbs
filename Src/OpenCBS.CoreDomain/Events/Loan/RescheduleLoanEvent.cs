// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events
{
	/// <summary>
	/// Description resumee de ReschedulingOfALoan.
    /// </summary>
    [Serializable]
	public class RescheduleLoanEvent : Event
	{
		public override string Code
		{
			get{return "ROLE";}
			set{_code = value;}
		}

	    public OCurrency Amount { get; set; }
        public OCurrency Interest { get; set; }
	    public int NbOfMaturity { get; set; }
        public int DateOffset { get; set; }
	    public bool BadLoan { get; set; }
        public override string Description { get; set; }
        public int GracePeriod { get; set; }
        public bool ChargeInterestDuringShift { get; set; }
        public bool ChargeInterestDuringGracePeriod { get; set; }

	    public override Event Copy()
		{
			return (RescheduleLoanEvent)MemberwiseClone();
		}
	}
}
