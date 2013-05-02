// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingCreditOperationEvent : SavingPositiveEvent
    {
        public override string Code
        {
            get { return OSavingEvents.SpecialOperationCredit; }
        }

        public override string Description { get; set; }
    }
}
