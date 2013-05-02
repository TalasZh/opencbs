// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingLoanDisbursementEvent : SavingEvent
    {
        public override string Code
        {
            get { return OSavingEvents.LoanDisbursement; }
        }

        public override string Description { get; set; }

        public override sealed OCurrency GetAmountForBalance()
        {
            if (!Amount.HasValue) return 0m;
            return Amount;
        }

        public override OCurrency GetFeeForBalance()
        {
            return (decimal)-1 * Fee;
        }
    }
}
