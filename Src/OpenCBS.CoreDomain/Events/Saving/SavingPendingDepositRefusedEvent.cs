// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingPendingDepositRefusedEvent : SavingPositiveEvent
    {
        public override string Code
        {
            get { return OSavingEvents.PendingDepositRefused; }
        }

        public override string Description { get; set; }
    }
}
