// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingInitialDepositEvent : SavingDepositEvent
    {
        public override string Code
        {
            get { return OSavingEvents.InitialDeposit; }
        }

        public override OCurrency GetFeeForBalance()
        {
            return 0m;
        }
    }
}
