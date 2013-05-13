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
    public class ProjetSearchResult
    {
        private int _id;
        private string _contractCode;
        private string _code;
        private string _aim;
        private DateTime _beginDate;
        private string _abilities;
        private string _experience;
        private string _market;
        private string _concurrence;
        private string _entite = string.Empty;
        private string _companyName = string.Empty;
        private string _firstName=string.Empty;
        private string _lastName = string.Empty;
        private int _tiersId;
        private string _projectName;
        private char _status;
        private string _groupeName=string.Empty;
        private OClientTypes _type=OClientTypes.Person;
        
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }
        public OClientTypes Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public char Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public int TiersId
        {
            get { return _tiersId; }
            set { _tiersId = value; }
        }
        public string FirstName 
        { 
            get { return _firstName; }
            set { _firstName = value; } 
        }
        public string Entite
        {
            get
            {
                if (_status == 'I')
                {
                    _type = OClientTypes.Person;
                    _entite = _firstName + " " + _lastName;
                }
                if (_status == 'C') 
                {
                    _type = OClientTypes.Corporate;
                    _entite = _companyName;
                }
                if (_status == 'G') 
                {
                    _type = OClientTypes.Group;
                    _entite = _groupeName;
                }

                return _entite; } 
            set { _entite = value; }
        }

        public string GroupeName
        {
            get { return _groupeName; }
            set { _groupeName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string ContractCode
        {
            get { return _contractCode; }
            set { _contractCode = value; }
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Aim
        {
            get { return _aim; }
            set { _aim = value; }
        }
        public DateTime BeginDate
        {
            get { return _beginDate; }
            set { _beginDate = value; }
        }
        public string Abilities
        {
            get { return _abilities; }
            set { _abilities = value; }
        }
        public string Experience
        {
            get { return _experience; }
            set { _experience = value; }
        }
        public string Market
        {
            get { return _market; }
            set { _market = value; }
        }
        public string Concurrence
        {
            get { return _concurrence; }
            set { _concurrence = value; }
        }



      
    }
}
