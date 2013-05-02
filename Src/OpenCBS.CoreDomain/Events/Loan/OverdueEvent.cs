// LICENSE PLACEHOLDER

using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events.Loan
{
    public class OverdueEvent : Event
    {
        public OCurrency OLB { get; set; }
        public int OverdueDays { get; set; }
        public OCurrency OverduePrincipal { get; set; }
        public override string Description { get; set; }

        public override string Code { get; set; }
        public override Event Copy()
        {
            return (OverdueEvent)MemberwiseClone();
        }
    }
}
