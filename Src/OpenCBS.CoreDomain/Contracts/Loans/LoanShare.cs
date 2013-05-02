// LICENSE PLACEHOLDER

using OpenCBS.Shared;
using System;

namespace OpenCBS.CoreDomain.Contracts.Loans
{
    [Serializable]
    public class LoanShare
    {
        public int PersonId { get; set; }
        //public int GroupID { get; set; }
        public OCurrency Amount { get; set; }
        public string PersonName{ get; set;}
    }
}
