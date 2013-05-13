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

namespace OpenCBS.CoreDomain
{
    public class MyInformation
    {
        public string MfiName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string PositionInCompany { get; set; }
        public string Comment  { get; set; }
        public string GrossPortfolio { get; set; }
        public string NumberOfClients { get; set; }
        public string PersonName { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string PurposeOfUsage { get; set; }
        public bool IsSent { get; set; }
    }
}
