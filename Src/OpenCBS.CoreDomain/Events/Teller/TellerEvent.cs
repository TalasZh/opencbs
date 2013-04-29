using Octopus.CoreDomain.Events.Loan;
using Octopus.Shared;

namespace Octopus.CoreDomain.Events.Teller
{
    public class TellerEvent:Event
    {
        public OCurrency Amount { get; set; }
    }
}
