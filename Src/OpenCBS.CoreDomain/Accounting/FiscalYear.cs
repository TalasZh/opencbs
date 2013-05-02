// LICENSE PLACEHOLDER

using System;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Accounting
{
    public class FiscalYear
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool Open
        {
            get
            {
                if (!OpenDate.HasValue) return false;
                if (!CloseDate.HasValue) return true;
                return TimeProvider.Now >= OpenDate.Value && TimeProvider.Now <= CloseDate.Value ;
            }
        }
    }
}
