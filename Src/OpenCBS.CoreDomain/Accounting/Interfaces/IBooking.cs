// LICENSE PLACEHOLDER

using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Accounting.Interfaces
{
    public interface IBooking
    {
        int Number{get;set;}
        Account CreditAccount { get;set;}
        Account DebitAccount { get;set;}
        OCurrency Amount { get;set;} 
    }
}
