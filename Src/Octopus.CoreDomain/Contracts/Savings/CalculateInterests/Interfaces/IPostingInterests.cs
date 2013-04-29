using System;
using System.Collections.Generic;
using Octopus.CoreDomain.Events.Saving;

namespace Octopus.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces
{
    public interface IPostingInterests
    {
        List<SavingInterestsPostingEvent> PostingInterests(DateTime postingDate);
    }
}
