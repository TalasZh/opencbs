// LICENSE PLACEHOLDER

using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Saving
{
    public class SavingBlockCompulsarySavingsEvent : SavingEvent
    {
        public override string Code
        {
            get { return OSavingEvents.BlockCompulsarySavings; }
        }
    }
}
