// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingDebitOperationEvent : SavingNegativeEvent
    {
        public SavingDebitOperationEvent()
        {
            _isDebit = true;
        }

        public override string Code
        {
            get { return OSavingEvents.SpecialOperationDebit; }
        }

        public override string Description { get; set; }
    }
}
