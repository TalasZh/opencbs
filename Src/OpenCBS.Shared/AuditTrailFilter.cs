using System;

namespace Octopus.Shared
{
    public struct AuditTrailFilter
    {
        public bool IncludeDeleted;
        public DateTime From;
        public DateTime To;
        public string Types;
        public int UserId;
        public int BranchId;
    }
}
