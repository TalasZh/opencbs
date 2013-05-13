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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenCBS.CoreDomain.Contracts;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Clients
{
    [Serializable]
    public class Village : IClient, IDisposable
    {
        private VillageMember _leader;
        private List<VillageMember> _members;
        private List<VillageMember> _memberHistory;

        public string ImagePath { get; set; }
        public bool IsImageDeleted { get; set; }
        public bool IsImageUpdated { get; set; }
        public bool IsImageAdded { get; set; }

        public string Image2Path { get; set; }
        public bool IsImage2Deleted { get; set; }
        public bool IsImage2Updated { get; set; }
        public bool IsImage2Added { get; set; }
        public DayOfWeek? MeetingDay { get; set; }
        public Branch Branch { get; set; }

        public Village()
        {
            _members = new List<VillageMember>();
            _memberHistory = new List<VillageMember>();
            _leader = new VillageMember();
        }

        public bool Active { get; set; }
        public void AddProject()
        {
        }
        public void AddProject(Project project)
        {
        }
        public void AddProjects(List<Project> projects)
        {
        }
        public IClient Copy()
        {
            Village clonedVillage = (Village)MemberwiseClone();
            return clonedVillage;
        }

        public string Address { get; set; }
        public bool BadClient { get; set; }
        public int? CashReceiptIn { get; set; }
        public int? CashReceiptOut { get; set; }
        public string City { get; set; }
        public OClientStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public District District { get; set; }
        public string Email { get; set; }
        public string FollowUpComment { get; set; }
        public string HomePhone { get; set; }
        public string HomeType { get; set; }
        public int Id { get; set; }
        public int LoanCycle { get; set; }
        public string Name { get; set; }
        public int NbOfGuarantees { get { return 0; } }
        public int NbOfloans { get { return 0; } }
        public int NbOfProjects { get { return 0; } }
        public OCurrency OtherOrgAmount { get; set; }
        public string OtherOrgComment { get; set; }
        public OCurrency OtherOrgDebts { get; set; }
        public string OtherOrgName { get; set; }
        public string PersonalPhone { get; set; }
        public List<Project> Projects { get { return null; } }
        public IList<ISavingsContract> Savings { get { return null; } }
        public double? Scoring { get; set; }
        public string SecondaryAddress { get; set; }
        public string SecondaryCity { get; set; }
        public District SecondaryDistrict{ get; set;}
        public List<Loan> ActiveLoans { get; set; }
        public bool SecondaryAddressIsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(SecondaryAddress) && null == SecondaryDistrict;
            }
        }
        public bool SecondaryAddressPartiallyFilled
        {
            get
            {
                return (string.IsNullOrEmpty(SecondaryAddress) && SecondaryDistrict != null)
                    || (!string.IsNullOrEmpty(SecondaryAddress) && null == SecondaryDistrict);
            }
        }
        public string SecondaryEmail { get; set; }
        public string SecondaryHomePhone { get; set; }
        public string SecondaryHomeType { get; set; }
        public string SecondaryPersonalPhone { get; set; }
        public string SecondaryZipCode { get; set; }
        public Project SelectProject(int id) 
        {
            return null; 
        }
        public void SetStatus()
        {
            
        }
        public string Sponsor1 { get; set; }
        public string Sponsor2 { get; set; }
        public string Sponsor1Comment { get; set; }
        public string Sponsor2Comment { get; set; }
        public OClientTypes Type
        {
            get { return OClientTypes.Village; }
            set {}
        }
        public string ZipCode { get; set; }
        public DateTime? EstablishmentDate { get; set; }

        public void AddMember(VillageMember member)
        {    
            int tierId = member.Tiers.Id;            

            if (member.CurrentlyIn && _members.All(m => m.Tiers.Id != tierId))
                _members.Add(member);

            VillageMember curHistoryMember = _memberHistory.FirstOrDefault(m => m.Tiers.Id == tierId);
            if(curHistoryMember == null) _memberHistory.Add(member);
            else curHistoryMember.CurrentlyIn = member.CurrentlyIn;
        }

        public List<VillageMember> Members
        {
            get { return _members;}
            set { _members = value; }
        }

        public List<VillageMember> MemberHistory
        {
            get { return _memberHistory;}
            set { _memberHistory = value; }
        }

        public VillageMember Leader
        {
            get { return _leader; }
            set
            {
                if (value != null && value.CurrentlyIn)
                    _leader = value;
            }
        }

        public bool IsLeader(VillageMember pMember)
        {
            return ((Person)_leader.Tiers).IdentificationData == ((Person)pMember.Tiers).IdentificationData;
        }

        public void DeleteMember(VillageMember member)
        {
            VillageMember remove = null;
            foreach (VillageMember _member in _members)
            {
                if (_member.Tiers.Id == member.Tiers.Id)
                {
                    remove = _member;
                    break;
                }
            }
            if (remove != null)
            {
                _members.Remove(remove);
                member.LeftDate = TimeProvider.Now;
                member.CurrentlyIn = false;
            }
        }

        public User LoanOfficer { get; set; }

        private class InactiveEnumerator : IEnumerator
        {
            private int _index = -1;
            private readonly List<VillageMember> _members;

            public InactiveEnumerator (List<VillageMember> members)
            {
                _members = members;
            }

            public bool MoveNext()
            {
                _index++;
                for (; _index < _members.Count; _index++)
                {
                    if (_members[_index].IsSaved && 0 == _members[_index].ActiveLoans.Count) break;
                }
                return _index < _members.Count;
            }

            public void Reset()
            {
                _index = -1;
            }

            public object Current
            {
                get { return (_index >= 0 && _index < _members.Count) ? _members[_index] : null; }
            }
        } // InactiveEnumerator

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
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
                    _members = null;
                    _memberHistory = null;
                }
            }
            disposed = true;
        }

        private class InactiveEnumerable : IEnumerable
        {
            private readonly List<VillageMember> _members;
            internal InactiveEnumerable(List<VillageMember> members)
            {
                _members = members;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new InactiveEnumerator(_members);
            }
        } // InactiveEnumerable

        public IEnumerable InactiveMembers
        {
            get { return new InactiveEnumerable(_members);}
        }

        private class NonDisbursedEnumerator : IEnumerator
        {
            private int _index = -1;
            private readonly List<VillageMember> _members;

            public NonDisbursedEnumerator(List<VillageMember> members)
            {
                _members = members;
            }

            public bool MoveNext()
            {
                _index++;
                for (; _index < _members.Count; _index++ )
                {
                    if (_members[_index].ActiveLoans.Count > 0)
                    {
                        for (int i = 0; i < _members[_index].ActiveLoans.Count; ++i)
                        {
                            var activeLoan = _members[_index].ActiveLoans[i];
                            if (activeLoan.ContractStatus != OContractStatus.Active && !activeLoan.PendingOrPostponed())
                                return _index < _members.Count;
                        }
                    }
                }
                return _index < _members.Count;
            }

            public void Reset()
            {
                _index = -1;
            }

            public object Current
            {
                get { return (_index >= 0 && _index < _members.Count) ? _members[_index] : null; }
            }
        } // NonDisbursedEnumerator

        private class NonDisbursedEnumerable : IEnumerable
        {
            private readonly List<VillageMember> _members;
            internal NonDisbursedEnumerable(List<VillageMember> members)
            {
                _members = members;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new NonDisbursedEnumerator(_members);
            }
        } // NonDisbursedEnumerable

        public IEnumerable NonDisbursedMembers
        {
            get { return new NonDisbursedEnumerable(_members); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
