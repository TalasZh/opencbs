
using System;

namespace OpenCBS.CoreDomain.Dashboard
{
    public class Action
    {
        public DateTime PerformedAt { get; set; }
        public string ContractCode { get; set; }
        public string LoanOfficer { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}
