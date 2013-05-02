// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events
{
    public class ProvisionEvent : Event
    {
        public OCurrency Amount { get; set; }
        public int OverdueDays { get; set; }
        public OCurrency Rate { get; set; }
        public override string Description { get; set; }

        public override string Code { get; set; }
        public override Event Copy()
        {
            return (ProvisionEvent)MemberwiseClone();
        }
    }
}
