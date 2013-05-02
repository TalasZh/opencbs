// LICENSE PLACEHOLDER

using System;
using OpenCBS.Shared;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingManagementFeeEvent : SavingEvent
    {
        public override string Code
        {
            get { return OSavingEvents.ManagementFee; }
        }

        public override string Description { get; set; }

        public override OCurrency GetFeeForBalance()
        {
            return (decimal) -1*Fee;
        }
    }
}
