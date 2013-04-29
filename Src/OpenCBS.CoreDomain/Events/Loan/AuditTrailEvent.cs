using Octopus.CoreDomain.Events.Loan;

namespace Octopus.CoreDomain.Events
{
    public class AuditTrailEvent : Event
    {
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string CodeId { get; set; }
        public string BranchName { get; set; }
    }
}