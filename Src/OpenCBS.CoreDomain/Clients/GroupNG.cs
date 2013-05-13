// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

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
