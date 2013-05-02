// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events
{
    [Serializable]
    public class LoanCloseEvent : Event
    {
        public override string Code
        {
            get { return "LOCE"; }
            set { _code = value; }
        }
        public override string Description { get; set; }

        public override Event Copy()
        {
            return (LoanCloseEvent)MemberwiseClone();
        }
    }
}
