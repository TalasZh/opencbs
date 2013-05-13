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
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Guarantees;
using OpenCBS.CoreDomain.Contracts;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain
{
    [Serializable]
    public class Project: IDisposable
    {
        private int _id;
        private OProjectStatus _projectStatus;
        private List<Loan> _credits;
        private string _clientCode;
        private ApplicationSettings param;
        public string CorporateName { get; set; }
        public string CorporateJuridicStatus { get; set; }
        public string CorporateFiscalStatus { get; set; }
        public string CorporateRegistre { get; set; }
        public string CorporateSIRET { get; set; }
        public OCurrency CorporateCA { get; set; }
        public int? CorporateNbOfJobs { get; set; }
        public string CorporateFinancialPlanType { get; set; }
        public OCurrency CorporateFinancialPlanAmount { get; set; }
        public OCurrency CorporateFinancialPlanTotalAmount { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public District District { get; set; }
        public string HomePhone { get; set; }
        public string PersonalPhone { get; set; }
        public string Email { get; set; }
        public string HomeType { get; set; }
        public List<FollowUp> FollowUps { get; set; }
        public IClient Client { get; set; }

        public Project()
        {
            _credits = new List<Loan>();
            FollowUps = new List<FollowUp>();
        }

        public Project(string pClientCode)
        {
            _credits = new List<Loan>();
            _clientCode = pClientCode;
            FollowUps = new List<FollowUp>();
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime BeginDate { get; set; }

        public OProjectStatus ProjectStatus
        {
            get { return _projectStatus; }
            set { _projectStatus = value; }
        }

        public void SetStatus()
        {
            _projectStatus = _id == 0 ? OProjectStatus.Pending : _GetContractsStatus();
        }

        private OProjectStatus _GetContractsStatus()
        {
            if (_credits.Count == 0) return OProjectStatus.Pending;

            var active = false;
            var abandoned = false;
            var pending = false;

            foreach (Loan credit in _credits)
            {
                if(credit.ContractStatus == OContractStatus.Validated || credit.ContractStatus == OContractStatus.Active)
                {
                    active = true;
                    break;
                }
                if(credit.ContractStatus == OContractStatus.Abandoned)
                {
                    abandoned = true;
                    break;
                }
                if (credit.PendingOrPostponed() || credit.ContractStatus == OContractStatus.Refused)
                {
                    pending = true;
                    break;
                }            
            }
            return active
                       ? OProjectStatus.Active
                       : abandoned
                             ? OProjectStatus.Abandoned
                             : pending ? OProjectStatus.Pending : OProjectStatus.Inactive;
        }

        public List<Loan> Credits
        {
            get { return _credits; }
        }

        
        public void AddCredit(Loan pCredit,OClientTypes pClientType)
        {
            pCredit.ClientType = pClientType;
            pCredit.Project = this;
            _credits.Add(pCredit);
        }

        
        public void AddCredits(List<Loan> pCredits)
        {
            foreach (Loan loan in pCredits)
                loan.Project = this;
            _credits.AddRange(pCredits);
        }

        public string ClientCode
        {
            get { return _clientCode; }
            set { _clientCode = value; }
        }

        public string Name { get; set; }
        public string Purpose { get; set; }
        public string Abilities { get; set; }
        public string Concurrence { get; set; }
        public string Experience { get; set; }
        public string Market { get; set; }
        public string Code { get; set; }
        public string Aim { get; set; }

        public Loan SelectCredit(int pCreditId)
        {
            foreach (Loan credit in _credits)
            {
                if (credit.Id == pCreditId) return credit;
            }
            return null;
        }

        public int NbOfLoans
        {
            get { return _credits.Count; }
        }

        public string GenerateProjectCode(string userBranchCode)
        {
            string year = BeginDate.Year.ToString().Substring(2, 2);
            return string.Format("{0}/{1}/{2}", userBranchCode, year, _clientCode);
        }

        private bool disposed;

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
                    _credits = null;
                    FollowUps = null;
                }
            }
            disposed = true;
        }
    }
}
