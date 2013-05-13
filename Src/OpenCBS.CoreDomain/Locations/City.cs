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

namespace OpenCBS.CoreDomain
{
    /// <summary>
    /// City.
    /// </summary>
    [Serializable]
    public class City
    {
        private string _name;

        public int DistrictId { get; set; }
        public bool Deleted { get; set; }
        public int Id { get; set; }

        public City()
        {

        }

        //public City(string pName, int pDistrictId)
        //{
        //    _name = pName;
        //    DistrictId = pDistrictId;
        //}

        //public City(int pId, string pName, int pDistrictId)
        //{
        //    Id = pId;
        //    _name = pName;
        //    DistrictId = pDistrictId;
        //}

        public string Name
        {
            get {return _name;}
            set {_name = value;}
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
