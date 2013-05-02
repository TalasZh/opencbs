// LICENSE PLACEHOLDER

using System;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events.Loan
{
    [Serializable]
    public class AccruedInterestEvent : Event
    {
        public override string Code
        {
            get { return "LIAE"; }
            set { _code = value; }
        }

        public bool Rescheduled { get; set; }
        
        public override string Description { get; set; }
        public OCurrency Interest { get; set; }
        public OCurrency AccruedInterest { get; set; }
        public override Event Copy()
        {
            return (AccruedInterestEvent)MemberwiseClone();
        }
    }
}
