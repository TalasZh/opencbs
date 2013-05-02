// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Shared;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Shared.Settings;


namespace OpenCBS.CoreDomain.Contracts.Guarantees
{
    [Serializable]
    public class Guarantee : IContract
    {
        public string CreditCommitteeCode{ get; set;}
        public string Banque{ get; set;}

       public Guarantee()
        {

        }

        public Guarantee(User pUser, ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNwds, ProvisionTable pPt)
        {
        }

        public OCurrency Amount { get; set; }
        public OCurrency AmountLimit { get; set; }

        public OCurrency AmountGuaranted { get; set; }

        public double? GuaranteeFees { get; set; }

        public OClientTypes ClientType { get; set; }

        public bool Activated { get; set; }


        public string GenerateGuaranteeCode()
        {
            return null;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string BranchCode { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public bool Closed { get; set; }
        public OContractStatus ContractStatus { get; set; }
        public DateTime? CreditCommiteeDate { get; set; }
        public string CreditCommiteeComment { get; set; }
        public string CreditCommiteeCode { get; set; }

        public Project Project { get; set; }

        public LoanProduct Product { get; set; }
        public User LoanOfficer { get; set; }
        public FundingLine FundingLine { get; set; }
    }
}
