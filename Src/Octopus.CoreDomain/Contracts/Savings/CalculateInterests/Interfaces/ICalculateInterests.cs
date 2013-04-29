using System;
using Octopus.CoreDomain.Events.Saving;
using System.Collections.Generic;

namespace Octopus.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces
{
    public interface ICalculateInterests
    {
        List<SavingInterestsAccrualEvent> CalculateInterest(DateTime closureDate);
    }
}
