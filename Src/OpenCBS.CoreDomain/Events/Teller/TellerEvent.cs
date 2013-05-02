// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.Events.Loan;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events.Teller
{
    public class TellerEvent:Event
    {
        public OCurrency Amount { get; set; }
    }
}
