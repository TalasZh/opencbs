using System;
using OpenCBS.CoreDomain.Events.Saving;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces
{
    public interface ICalculateInterests
    {
        List<SavingInterestsAccrualEvent> CalculateInterest(DateTime closureDate);
    }
}
