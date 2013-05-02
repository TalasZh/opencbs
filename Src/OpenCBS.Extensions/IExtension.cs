//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.IO;
using System.Reflection;

namespace OpenCBS.Extensions
{
    public interface IExtension
    {
        Guid Guid { get; }
        string GetMeta(string key);
        object QueryInterface(Type t);
    }

    public class ExtensionInfo
    {
        private readonly string _extensionName;
        private readonly string _filePath;
        private readonly long _fileSize;
        private readonly string _octopusVersion;
        private readonly Guid _guid;
        private readonly string _version;
        private readonly string _vendor;

        public ExtensionInfo(IExtension extension)
        {
            _extensionName = extension.GetMeta("Name");

            Type extensionType = extension.GetType();
            _octopusVersion = extension.GetMeta("OctopusVersion");
            _guid = extension.Guid;
            
            Assembly extensionAssembly = extensionType.Assembly;
            _version = extensionAssembly.GetName().Version.ToString(3);
            _filePath = extensionAssembly.Location;            
            FileInfo fileInfo = new FileInfo(_filePath);
            _fileSize = fileInfo.Length;
            _vendor = extension.GetMeta("Vendor");
        }

        public string Version
        {
            get { return _version; }
        }

        public Guid Guid
        {
            get { return _guid; }
        }

        public string OctopusVersion
        {
            get { return _octopusVersion; }
        }

        public long FileSize
        {
            get { return _fileSize; }
        }

        public string FilePath
        {
            get { return _filePath; }
        }

        public string ExtensionName
        {
            get { return _extensionName; }
        }

        public string Vendor
        {
            get { return _vendor; }
        }
    }
}
