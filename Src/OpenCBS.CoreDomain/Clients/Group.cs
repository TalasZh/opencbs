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
using System.Linq;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Clients  
{
    /// <summary>
    /// Description r�sum�e de ClientsGroup.
    /// </summary>
    [Serializable]
    public class Group : Client, IDisposable
    {
        private Member _leader;
        private List<Member> _members;
        private List<Member> _historyMembers;

        private bool _badClient;

        public override string Name { get; set; }
        public DateTime? EstablishmentDate { get; set; }
        public string Comments { get; set; }
        public string FirstName { get; set; }
        public DayOfWeek? MeetingDay { get; set; }
        public User FavouriteLoanOfficer { get; set; }
        public int? FavouriteLoanOfficerId { get; set; }

        public OCurrency TotalContractAmount { get; set; }

        public Group()
            : base(new List<Project>(), OClientTypes.Group)
        {
            _members = new List<Member>();
            _historyMembers = new List<Member>();
            _leader = new Member();
            TotalContractAmount = 0;
        }

        public override string ToString()
        {
            return Name;
        }

        public override IClient Copy()
        {
            Group clonedGroup = (Group)MemberwiseClone();
            clonedGroup.Members = new List<Member>();
            foreach (Member member in _members)
            {
                clonedGroup.AddMember(member.Copy());
            }
            return clonedGroup;
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

        public List<Member> HistoryMembers
        {
            get { return _historyMembers; }
            set { _historyMembers = value; }
        }

        public Member Leader
        {
            get { return _leader; }
            set
            {
                if (value != null)
                {
                    _leader = value;
                    DeleteMember(value);
                    AddMember(_leader);
                }
            }
        }

        public bool HasOtherOrganization()
        {
            return OtherOrgName != null || OtherOrgDebts.HasValue || OtherOrgAmount.HasValue;
        }

        public void AddMember(Member pMember)
        {
            if (pMember.CurrentlyIn)
            {
                _members.Add(pMember);
                if (pMember.Tiers.BadClient)
                {
                    _badClient = true;
                }
            }
            else
            {
                _historyMembers.Add(pMember);
            }
        }

        public bool IsLeader(Member pMember)
        {
            return ((Person)_leader.Tiers).IdentificationData == ((Person)pMember.Tiers).IdentificationData;
        }

        public void DeleteMember(Member pMember)
        {
            Member tempMember = _members.FirstOrDefault(member => ((Person)member.Tiers).IdentificationData == ((Person)pMember.Tiers).IdentificationData);
            if (tempMember != null)
            {
                _members.Remove(tempMember);
                pMember.LeftDate = TimeProvider.Today;
                pMember.Tiers.Active = false;
            }
            IsBadClient();
        }

        public void IsBadClient()
        {
            _badClient = false;
            foreach (Member member in _members)
            {
                if (member.Tiers.BadClient)
                {
                    _badClient = true;
                    break;
                }
            }
        }

        public int GetNumberOfMembers
        {
            get { return _members.Count; }
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

        public string ImagePath { get; set; }
        public bool IsImageAdded { get; set; }
        public bool IsImageUpdated { get; set; }
        public bool IsImageDeleted { get; set; }

        public string Image2Path { get; set; }
        public bool IsImage2Added { get; set; }
        public bool IsImage2Updated { get; set; }
        public bool IsImage2Deleted { get; set; }
        public EconomicActivity Activity { get; set; }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                }
            }
            disposed = true;
        }
    }
}
