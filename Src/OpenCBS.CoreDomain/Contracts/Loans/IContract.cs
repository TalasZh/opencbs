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
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.FundingLines;

namespace OpenCBS.CoreDomain.Contracts
{
    public interface IContract
    {
        int Id { get; set; }
        string Code { get; set; }
        string BranchCode { get; set; }
        DateTime CreationDate { get; set; }
        DateTime StartDate { get; set; }
        DateTime CloseDate { get; set; }
        bool Closed { get; set; }
        OContractStatus ContractStatus { get; set; }
        DateTime? CreditCommiteeDate { get; set; }
        string CreditCommiteeComment { get; set; }
        string CreditCommitteeCode { get; set;}
        LoanProduct Product{ get; set;}
        User LoanOfficer { get; set; }
        FundingLine FundingLine { get; set; }
        //Corporate Corporate { get; set; }
        Project Project { get; set; }
    }

    public static class ContractExtension
    {
        public static bool PendingOrPostponed(this IContract contract)
        {
            return contract.ContractStatus == OContractStatus.Pending ||
                   contract.ContractStatus == OContractStatus.Postponed;
        }
    }
}
