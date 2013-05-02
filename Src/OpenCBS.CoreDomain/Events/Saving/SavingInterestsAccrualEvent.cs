using OpenCBS.Enums;
using System;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingInterestsAccrualEvent : SavingEvent
    {
        public override string Code
        {
            get { return OSavingEvents.Accrual; }
        }

        public override string Description { get; set; }
    }
}
