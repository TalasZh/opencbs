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
using System.Text;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Export.Files
{
    [Serializable]
    public class Installment
    {
        public DateTime InstallmentDate { get; set; }
        public int ContractId { get; set; }
        public int ClientId { get; set; }
        public string ContractCode { get; set; }
        public int InstallmentNumber { get; set; }
        public OCurrency InstallmentAmount { get; set; }
        public string PersonalBankName { get; set; }
        public string PersonalBankBic { get; set; }
        public string PersonalBankIban1 { get; set; }
        public string PersonalBankIban2 { get; set; }
        public string BusinessBankName { get; set; }
        public string BusinessBankBic { get; set; }
        public string BusinessBankIban1 { get; set; }
        public string BusinessBankIban2 { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ClientName { get; set; }
        public int RepaymentStatus { get; set; }
    }
}
