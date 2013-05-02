// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Events.Loan;

namespace OpenCBS.CoreDomain.Events
{
	/// <summary>
	/// This is the event for manual entry
    /// </summary>
    [Serializable]
	public class RegEvent : Event
	{
        public override string Code
        {
            get { return "REGE"; }
            set { _code = value; }
        }

        public override string Description { get; set; }

		public override Event Copy()
		{
			return (RegEvent)MemberwiseClone();;
		}
	}
}
