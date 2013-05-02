using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Events.Saving;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces
{
    public interface IPostingInterests
    {
        List<SavingInterestsPostingEvent> PostingInterests(DateTime postingDate);
    }
}
