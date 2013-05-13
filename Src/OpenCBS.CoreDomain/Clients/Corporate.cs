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
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Clients
{
    [Serializable]
    public class Corporate : Client
    {
        public Corporate() : base(new List<Project>(), OClientTypes.Corporate)
        {
            Contacts = new List<Contact>();
        }

        public List<Contact> Contacts { get; set; }
        public EconomicActivity Activity { get; set; }
        public string LegalForm { get; set; }
        public string InsertionType { get; set; }
        public string Sigle { get; set; }
        public string SmallName { get; set; }
        public int? VolunteerCount { get; set; }
        public DateTime? AgrementDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool? AgrementSolidarity { get; set; }
        public int? EmployeeCount { get; set; }
        public string Siret { get; set; }
        public bool IsDeleted { get; set; }
        public string Comments { get; set; }
        public string FiscalStatus { get; set; }
        public string Registre { get; set; }

        public string ImagePath { get; set; }
        public bool IsImageAdded { get; set; }
        public bool IsImageUpdated { get; set; }
        public bool IsImageDeleted { get; set; }

        public string Image2Path { get; set; }
        public bool IsImage2Added { get; set; }
        public bool IsImage2Updated { get; set; }
        public bool IsImage2Deleted { get; set; }

        public User FavouriteLoanOfficer { get; set; }
        public int? FavouriteLoanOfficerId { get; set; }        

        public override string ToString()
        {
            return Name;
        }
    }
}
