// LICENSE PLACEHOLDER

using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Accounting
{
    public class ClosureOptions
    {
        public bool DoOverdue{ get; set;}
        public bool DoProvision { get; set; }
        public bool DoAccrued { get; set; }
        public bool DoLoanClosure { get; set; }
        public bool DoSavingClosure { get; set; }
        public bool DoTellerManagementClosure { get; set; }
        public bool DoReversalTransactions { get; set; }
        public bool DoSavingEvents { get; set; }
        public bool DoManualEntries { get; set; }
    }

    public class ClosureOption : object
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ClosureItems
    {
        public ClosureItems()
        {
            Items = new List<ClosureOption>();
        }

        public List<ClosureOption> Items { get; set; }
    }
}
