// LICENSE PLACEHOLDER

using System;
using System.Diagnostics;

// TODO: merge this class with the same for savings
namespace OpenCBS.CoreDomain.SearchResult
{
    [Serializable]
    public class CreditSearchResult
    {
        public int Id { get; set; }

        public string ContractCode { get; set; }
        public string ClientType { get; set; }
        public string ClientName { get; set; }
        public string ContractStartDate { get; set; }
        public string ContractEndDate { get; set; }
        public string ContractStatus { get; set; }
        public User LoanOfficer { get; set; }

        public bool IsViewableBy(User user)
        {
            Debug.Assert(LoanOfficer != null, "Loan officer is null.");
            return LoanOfficer.Equals(user)
                ? true
                : user.HasAsSubordinate(LoanOfficer);
        }
    }
}
