// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events
{
	/// <summary>
	/// Description r�sum�e de WriteOffEvent.
    /// </summary>
    [Serializable]
	public class WriteOffEvent : Event
	{
	    public override string Code
        {
            get { return "WROE"; }
            set { _code = value; }
        }

	    public int PastDueDays { get; set; }
	    public OCurrency OLB { get; set; }
	    public OCurrency AccruedInterests { get; set; }
	    public OCurrency AccruedPenalties { get; set; }
        public OCurrency OverduePrincipal { get; set; }
        public override string Description { get; set; }

	    public override Event Copy()
		{
			return (WriteOffEvent)MemberwiseClone();
		}
	}
}
