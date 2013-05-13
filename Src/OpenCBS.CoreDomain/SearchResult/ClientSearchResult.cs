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
