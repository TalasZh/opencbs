// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingInterestsPostingEvent : SavingPositiveEvent
    {
        public override string Code
        {
            get { return OSavingEvents.Posting; }
        }

        public override string Description { get; set; }
    }
}
