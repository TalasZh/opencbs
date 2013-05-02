// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Products;

namespace OpenCBS.CoreDomain.Clients
{
    [Serializable]
    public class VillageMember
    {
        public VillageMember()
        {
            Tiers = new Person();
        }
        public IClient Tiers { get; set; }
        public DateTime JoinedDate { get; set; }
        public DateTime? LeftDate { get; set; }
        public bool CurrentlyIn { get; set; }
        public List<Loan> ActiveLoans { get; set; }
        public LoanProduct Product { get; set; }
        public bool IsLeader { get; set; }
        public bool IsSaved { get; set; }
    }
}
