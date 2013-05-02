// LICENSE PLACEHOLDER

using OpenCBS.Enums;
using System;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingClosureEvent : SavingEvent
    {
        public override string Code
        {
            get { return OSavingEvents.SavingClosure; }
        }

        public override string Description { get; set; }
    }
}
