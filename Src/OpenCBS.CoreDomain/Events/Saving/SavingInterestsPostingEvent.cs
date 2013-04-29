using System;
using Octopus.Enums;

namespace Octopus.CoreDomain.Events.Saving
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