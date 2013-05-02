// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events
{
    public class LoanEntryFeeEvent: Event
    {
        public LoanEntryFeeEvent()
        {
            Id = -1;
            Fee = 0;
            DisbursementEventId = -1;
            Date = TimeProvider.Today;
        }
        public OCurrency Fee { get; set; }
        public int DisbursementEventId { get; set; }
        public override string Code
        {
            get {return _code;}
            set {_code = value;}
        }
    }
}
