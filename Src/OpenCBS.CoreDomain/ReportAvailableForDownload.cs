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

namespace OpenCBS.CoreDomain
{
    [Serializable]
    public class ReportAvailableForDownload
    {
        public string _guid;
        public string _title;
        public string _description;
        public string _size;
        public bool _exist;
        private List<ReportAvailableForDownload> _reportAvailableForDownload;

        public string Guid
        {
            get { return _guid; }
        }

        public string Title
        {
            get { return _title; }
        }

        public string Description
        {
            get { return _description; }
        }

        public string Size
        {
            get { return _size; }
        }

        public bool Exist
        {
            get { return _exist; }
        }

        public ReportAvailableForDownload(string pGuid, string pTitle, string pDescription, string pSize,bool pExist)
        {
            _guid = pGuid;
            _title = pTitle;
            _description = pDescription;
            _size = pSize;
            _exist = pExist;
        }

        public ReportAvailableForDownload()
        {
            _reportAvailableForDownload = new List<ReportAvailableForDownload>();
        }

        public List<ReportAvailableForDownload> AddReport
        {
            get { return _reportAvailableForDownload; }
            set { _reportAvailableForDownload = value; }
        }
    }
}
