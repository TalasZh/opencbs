using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCBS.CoreDomain;

namespace OpenCBS.CoreDomain.Contracts.Loans
{
    public class LoanEntryFee
    {
        public LoanEntryFee()
        {
            ProductEntryFee = null;
            FeeValue = 0;
        }

        public int Id { get; set; }
        public int ProductEntryFeeId { get; set; }
        public EntryFee ProductEntryFee { get; set; }
        public decimal FeeValue { get; set; }
    }
}
