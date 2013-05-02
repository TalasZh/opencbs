using OpenCBS.CoreDomain.Events;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Accounting
{
    public interface IAccountingRule
    {
        int Id { get; set; }
        EventType EventType {get;set;}
        EventAttribute EventAttribute { get; set; }
        Account DebitAccount { get; set; }
        Account CreditAccount { get; set; }
        OBookingDirections BookingDirection { get; set; }
        int Order { get; set; }
        string Description { get; set; }
    }
}
