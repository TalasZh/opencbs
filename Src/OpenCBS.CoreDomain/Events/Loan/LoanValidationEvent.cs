// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events
{
    [Serializable]
    public class LoanValidationEvent : Event
    {
        public OCurrency Amount { get; set; }

        public override string Code
        {
            get { return "LOVE"; }
            set { _code = value; }
        }

        public override string Description { get; set; }

        public override Event Copy()
        {
            return (LoanValidationEvent)MemberwiseClone();
        }
    }
}
