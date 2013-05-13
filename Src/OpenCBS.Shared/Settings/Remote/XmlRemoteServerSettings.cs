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
using System.Xml;
using System.IO;
using System.Reflection;

namespace OpenCBS.Shared.Settings.Remote
{
    public class XmlRemoteServerSettings : IRemoteServerSettings
    {
        XmlDocument _document;

        public XmlRemoteServerSettings()
        {
            _document = new XmlDocument();
            _document.Load(_path);
        }

        private string _path
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"RemoteServerSettings.xml"); }
        }

        #region IRemoteServerSettings Members

        public string ServerName
        {
            get
            {
                return _document.SelectSingleNode("//ServerName").InnerText;
            }
            set
            {
                _document.SelectSingleNode("//ServerName").InnerText = value;
                _document.Save(_path);
            }
        }

        public string LoginName
        {
            get
            {
                return _document.SelectSingleNode("//LoginName").InnerText;
            }
            set
            {
                _document.SelectSingleNode("//LoginName").InnerText = value;
                _document.Save(_path);
            }
        }

        public string Password
        {
            get
            {
                return _document.SelectSingleNode("//Password").InnerText;
            }
            set
            {
                _document.SelectSingleNode("//Password").InnerText = value;
                _document.Save(_path);
            }
        }

        #endregion
    }
}
