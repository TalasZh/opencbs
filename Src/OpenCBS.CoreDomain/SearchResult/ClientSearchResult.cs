// LICENSE PLACEHOLDER

using System;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.SearchResult
{
    [Serializable]
    public class ClientSearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MemberOf { get; set; }
        public bool Active { get; set; }
        public string Siret { get; set; }
        public int LoanCycle { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public bool BadClient { get; set; }
        public string PassportNumber { get; set; }
        public OClientTypes Type { get; set; } 
        
        public ClientSearchResult(){}

        public ClientSearchResult(int pId, string pName, OClientTypes pType, bool pActive, int pLoanCycle, string pDistrict, string pCity,
            bool pBadClient, string pPassportNumber,string pPersonGroupBelongTo)
        {
            Id = pId;
            Type = pType;
            PassportNumber = pPassportNumber;
            BadClient = pBadClient;
            City = pCity;
            District = pDistrict;
            LoanCycle = pLoanCycle;
            Active = pActive;
            Name = pName;
            MemberOf = pPersonGroupBelongTo;
        }
    }
}
