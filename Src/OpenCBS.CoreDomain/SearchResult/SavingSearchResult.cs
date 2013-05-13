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
using System.Diagnostics;
using OpenCBS.Enums;
using OpenCBS.Shared;

// TODO: merge this class with the same for loans
namespace OpenCBS.CoreDomain.SearchResult
{
    [Serializable]
    public class SavingSearchResult
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ContractCode { get; set; }
        public OClientTypes ClientType { get; set; }
        public OSavingsStatus Status { get; set; }
        public string ClientTypeCode { get; set; }
        public string ClientName { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string ContractType { get; set; }
        public User LoanOfficer { get; set; }
        public int CurrencyId { get; set; }

        public bool IsViewableBy(User user)
        {
            Debug.Assert(LoanOfficer != null, "Loan officer is null.");
            return LoanOfficer.Equals(user)
                ? true
                : user.HasAsSubordinate(LoanOfficer);
        }
    }
}
