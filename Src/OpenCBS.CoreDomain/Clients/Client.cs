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
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Contracts.Savings;

namespace OpenCBS.CoreDomain.Clients
{
    [Serializable]
    public abstract class Client : IClient
    {
        private int _id;
		private readonly List<Project> _projects;
        private readonly List<ISavingsContract> _savings = new List<ISavingsContract>();
        private int _loanCycle;
        private string _secondaryCity;
        private District _secondaryDistrict;
        private OClientStatus _clientStatus;

        protected Client(List<Project> pProjects, OClientTypes pClientTypes)
        {
            foreach (Project project in pProjects)
                project.Client = this;

            _projects = pProjects;
            Type = pClientTypes;
        }

        #region Getter/Setter
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public OClientTypes Type { get; set; }
        public virtual string Name { get; set; }
        public double? Scoring { get; set; }
        
        public int LoanCycle
        {
            get { return _loanCycle; }
            set { _loanCycle = value; }
        }

        public bool Active { get; set; }

        public virtual bool BadClient { get; set; }
        public string OtherOrgName { get; set; }
        public OCurrency OtherOrgAmount { get; set; }
        public OCurrency OtherOrgDebts { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public District District { get; set; }
        public string SecondaryAddress { get; set; }
        
        public string SecondaryCity
        {
            get { return _secondaryCity; }
            set { _secondaryCity = value; }
        }

        public string HomePhone { get; set; }
        public string PersonalPhone { get; set; }
        public string Email { get; set; }
        public string SecondaryEmail { get; set; }
        public string SecondaryHomeType { get; set; }
        public string SecondaryZipCode { get; set; }
        public string HomeType { get; set; }
        public string OtherOrgComment { get; set; }
        public string SecondaryHomePhone { get; set; }
        public string SecondaryPersonalPhone { get; set; }
        public District SecondaryDistrict
        {
            get { return _secondaryDistrict; }
            set { _secondaryDistrict = value; }
        }

        public int NbOfProjects 
        { 
            get { return _projects.Count; } 
        }
        public int NbOfloans { get; private set; }
        public int NbOfGuarantees { get; private set; }
        
        public OClientStatus Status
        {
            get { return _clientStatus; }
            set { _clientStatus = value; }
        }

        public string Sponsor1 { get; set; }
        public string Sponsor2 { get; set; }
        public string Sponsor1Comment { get; set; }
        public string Sponsor2Comment { get; set; }
        public string FollowUpComment { get; set; }
        public DateTime CreationDate { get; set; }
        public int? CashReceiptIn { get; set; }
        public int? CashReceiptOut { get; set; }

        public bool HasOtherOrganization()
        {
            return OtherOrgName != null || OtherOrgDebts.HasValue || OtherOrgAmount.HasValue;
        }

        public List<Project> Projects
        {
            get { return _projects; }
        }

        public IList<ISavingsContract> Savings
        {
            get { return _savings; }
        }

        public List<Loan> ActiveLoans { get; set; }
        public Branch Branch { get; set; }
        #endregion

        public bool SecondaryAddressIsEmpty
        {
            get { return (_secondaryDistrict == null && _secondaryCity == null &&
                string.IsNullOrEmpty(SecondaryEmail) && string.IsNullOrEmpty(SecondaryHomePhone)
                && string.IsNullOrEmpty(SecondaryPersonalPhone) &&
                string.IsNullOrEmpty(SecondaryZipCode)); }
        }

        public bool SecondaryAddressPartiallyFilled
        {
            get
            {
                return (_secondaryDistrict != null && _secondaryCity == null) ||
                     (_secondaryDistrict == null && _secondaryCity != null) ||
                     (_secondaryDistrict==null && 
                     (SecondaryHomePhone!=null || SecondaryPersonalPhone!=null ||SecondaryEmail!=null));
            }
        }

        public virtual IClient Copy()
        {
            return this;
        }

        public void AddProject()
        {
            _projects.Add(new Project(string.Format("{0}/{1}", _loanCycle, _id)) { Client = this });
        }

        public void AddProject(Project pProject)
        {
            pProject.Client = this;
            _projects.Add(pProject);
        }

        public void AddProjects(List<Project> pProjects)
        {
            foreach (Project project in pProjects)
                project.Client = this;
            _projects.AddRange(pProjects);
        }

        public void AddSaving(ISavingsContract pSavings)
        {
            pSavings.Client = this;
            _savings.Add(pSavings);
        }

        public void AddSavings(IEnumerable<ISavingsContract> pSavings)
        {
            foreach (ISavingsContract saving in pSavings)
                saving.Client = this;
            _savings.AddRange(pSavings);
        }

        public Project SelectProject(int pContractId)
        {
            foreach (Project project in _projects)
            {
                foreach (Loan credit in project.Credits)
                {
                    if (credit.Id == pContractId)
                        return project;
                }
            }
            return null;
        }

        public void SetStatus()
        {
            _clientStatus = _id == 0 ? OClientStatus.Prospect : GetProjectsStatus();
        }

        private OClientStatus GetProjectsStatus()
        {
            if (_projects.Count == 0) return OClientStatus.Prospect;

            bool pending = false;
            bool active = false;

            foreach (Project project in _projects)
            {
                if (project.ProjectStatus == OProjectStatus.Active)
                {
                    active = true;
                    break;
                }
                if (project.ProjectStatus != OProjectStatus.Pending)
                {
                    continue;
                }
                pending = true;
                break;
            }
            return active ? OClientStatus.Active : pending ? OClientStatus.Pending : OClientStatus.Inactive;
        }

       
    }
}
