// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Products;

namespace OpenCBS.CoreDomain.Accounting
{
    [Serializable]
    public class AccountingRule
    {
        public int Id { get; set; }
        public Account GenericAccount { get; set; }
        public Account SpecificAccount { get; set; }
        public OProductTypes ProductType { get; set; }
        public LoanProduct LoanProduct { get; set; }
        public GuaranteeProduct GuaranteeProduct { get; set; }
        public ISavingProduct SavingProduct { get; set; }
        public OClientTypes ClientType { get; set; }
        public EconomicActivity EconomicActivity { get; set; }
        public bool Deleted { get; set; }
    }
}
