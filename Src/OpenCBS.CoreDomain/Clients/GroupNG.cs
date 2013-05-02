// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.Enums;
using OpenCBS.Shared;


namespace OpenCBS.CoreDomain.Clients
{
    /// <summary>
    /// Description résumée de ClientsGroup.
    /// </summary>
    [Serializable]
    public class GroupNG : Client
    {
        private List<Member> _members;
        private bool _badClient;
        public DateTime? EstablishmentDate { get; set; }
        public string Comments { get; set; }
        public string FirstName { get; set; }

        public OCurrency TotalContractAmount { get; set; }

        public GroupNG() : base(new List<Project>(),OClientTypes.Group)
        {
            _members = new List<Member>();
            TotalContractAmount = 0;
        }

        public override bool BadClient
        {
            get { return _badClient; }
            set
            {
                _badClient = value;
                foreach (Member member in _members)
                {
                    member.Tiers.BadClient = value;
                }
            }
        }
 
        public List<Member> Members
        {
            get { return _members; }
            set { _members = value; }
        }

        public Member Leader
        {
            get
            {
                foreach (Member member in _members)
                {
                    if (member.IsLeader) return member;
                }
                return null;
            }
            set
            {
                foreach (Member member in _members)
                {
                    member.IsLeader = member != value;
                }
            }
        }

        public bool HasOtherOrganization()
        {
            return OtherOrgName != null || OtherOrgDebts.HasValue || OtherOrgAmount.HasValue;
        }

        public void AddMember(Member pMember)
        {
            _members.Add(pMember);

            if (pMember.Tiers.BadClient) _badClient = true;
        }

        public void DeleteMember(Member pMember)
        {
            _badClient = false;
            foreach (Member member in _members)
            {
                if (member == pMember)
                {
                    member.CurrentlyIn = false;
                    member.LeftDate = TimeProvider.Today;
                }
                else 
                {
                    if (member.Tiers.BadClient) _badClient = true;
                }
            }
        }

        public int GetNumberOfMembers
        {
            get { return _members.Count; }
        }

        public OCurrency GetLoanShareAmount(int memberId)
        {
            foreach (Member member in _members)
            {
                if (member.Tiers.Id.Equals(memberId))
                    return member.LoanShareAmount;
            }
            return 0;
        }

        public OCurrency GetTotalLoanAmount
        {
            get
            {
                OCurrency amount = 0;
                foreach (Member member in _members)
                {
                    amount += member.LoanShareAmount;
                }
                return amount;
            }
        }
    }
}
