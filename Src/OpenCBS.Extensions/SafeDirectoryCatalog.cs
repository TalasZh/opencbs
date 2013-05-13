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

using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OpenCBS.Extensions
{
    public class SafeDirectoryCatalog : ComposablePartCatalog
    {
        private readonly AggregateCatalog _catalog;
        private readonly string[] _failedAssemblies;

        public SafeDirectoryCatalog(string directory)
        {
            string[] assemblies = Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories);
            _catalog = new AggregateCatalog();
            List<string> failedAssemblies = new List<string>(assemblies.Length);
            foreach (var assembly in assemblies)
            {
                try
                {
                    AssemblyCatalog asmCat = new AssemblyCatalog(assembly);
                    if (asmCat.Parts.Any()) //Workaround to check if assembly is OK
                        _catalog.Catalogs.Add(asmCat);
                }
                catch (ReflectionTypeLoadException exception)
                {
                    Trace.TraceWarning("Could not load assembly: {0}. Message: {1}", assembly, exception.Message);
                    failedAssemblies.Add(assembly);
                }
            }
            _failedAssemblies = failedAssemblies.ToArray();
        }

        public string[] FailedAssemblies
        {
            get { return _failedAssemblies; }
        }

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get { return _catalog.Parts; }
        }
    }
}
