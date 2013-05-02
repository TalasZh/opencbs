// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingWithdrawEvent : SavingNegativeEvent
    {
        public SavingWithdrawEvent()
        {
            _isDebit = true;
        }

        public override string Code
        {
            get { return OSavingEvents.Withdraw; }
        }

        public override string Description { get; set; }
    }
}
