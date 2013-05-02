// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Events.Loan;

namespace OpenCBS.CoreDomain.Events
{
    public interface IEventProcessor
    {
        AccountingTransaction FireEvent(Event e, int pCurrencyId);
        AccountingTransaction UnDoFireEvent(Event e, int number, int pCurrencyId);
    }
}
