// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingDebitTransferEvent : SavingTransferEvent
    {
        public SavingDebitTransferEvent()
        {
            _isDebit = true;
        }

        public override string Code
        {
            get { return OSavingEvents.DebitTransfer; }
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
