// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingPendingDepositEvent : SavingPositiveEvent
    {
        public override string Code
        {
            get { return OSavingEvents.PendingDeposit; }
        }

        public override string Description { get; set; }
    }
}
