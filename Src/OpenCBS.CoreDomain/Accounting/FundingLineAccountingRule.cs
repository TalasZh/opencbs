using System;
using OpenCBS.CoreDomain.Events;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Accounting
{
    [Serializable]
    public class FundingLineAccountingRule : IAccountingRule
    {
        #region IAccountingRule Members
        public int Id { get; set; }
        public Account DebitAccount { get; set; }
        public Account CreditAccount { get; set; }
        public bool Deleted { get; set; }
        public EventType EventType { get; set; }
        public EventAttribute EventAttribute { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        #endregion

        public FundingLine FundingLine { get; set; }
        public OBookingDirections BookingDirection { get; set; }
    }
}
