// LICENSE PLACEHOLDER

using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Saving
{
    public class SavingUnblockCompulsorySavingsEvent : SavingEvent
    {
        public override string Code
        {
            get { return OSavingEvents.UnblockCompulsorySavings; }
        }
    }
}
