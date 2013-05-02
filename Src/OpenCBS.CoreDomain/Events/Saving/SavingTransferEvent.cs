// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain.Events.Saving
{
    [Serializable]
    public abstract class SavingTransferEvent : SavingEvent
    {
        public override string Description { get; set; }
        public string RelatedContractCode { get; set; }
    }
}
