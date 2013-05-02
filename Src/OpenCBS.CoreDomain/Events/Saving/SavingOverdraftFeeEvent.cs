// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingOverdraftFeeEvent : SavingEvent
    {
        public override string Code
        {
            get { return OSavingEvents.OverdraftFees; }
        }

        public override string Description { get; set; }

        public override OCurrency GetFeeForBalance()
        {
            return (decimal)-1 * Fee;
        }
    }
}
