// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class LoanRepaymentFromSavingEvent:SavingNegativeEvent
    {
        public LoanRepaymentFromSavingEvent()
        {
            _isDebit = true;
        }
        public override string Code
        {
            get { return OSavingEvents.SavingLoanRepayment; }
        }

        public override string Description { get; set; }
    }
}
