using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Octopus.Shared;

namespace Octopus.CoreDomain.Export.Files
{
    [Serializable]
    public class Installment
    {
        public DateTime InstallmentDate { get; set; }
        public int ContractId { get; set; }
        public int ClientId { get; set; }
        public string ContractCode { get; set; }
        public int InstallmentNumber { get; set; }
        public OCurrency InstallmentAmount { get; set; }
        public string PersonalBankName { get; set; }
        public string PersonalBankBic { get; set; }
        public string PersonalBankIban1 { get; set; }
        public string PersonalBankIban2 { get; set; }
        public string BusinessBankName { get; set; }
        public string BusinessBankBic { get; set; }
        public string BusinessBankIban1 { get; set; }
        public string BusinessBankIban2 { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ClientName { get; set; }
        public int RepaymentStatus { get; set; }
    }
}
