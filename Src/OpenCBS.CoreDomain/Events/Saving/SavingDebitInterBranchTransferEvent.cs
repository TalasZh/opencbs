using System;
using Octopus.Enums;
using Octopus.Shared;

namespace Octopus.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingDebitInterBranchTransferEvent : SavingTransferEvent
    {
        public SavingDebitInterBranchTransferEvent()
        {
            _isDebit = true;
        }

        public override string Code
        {
            get { return OSavingEvents.InterBranchDebitTransfer; }
        }

        public override OCurrency GetAmountForBalance()
        {
            return (decimal)-1 * Amount;
        }

        public override OCurrency GetFeeForBalance()
        {
            return (decimal)-1 * Fee;
        }

        public override string ExtraInfo
        {
            get
            {
                return string.Format("to {0}", RelatedContractCode);
            }
        }
    }
}