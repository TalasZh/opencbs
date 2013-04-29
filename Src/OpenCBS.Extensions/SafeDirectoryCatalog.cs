using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Octopus.Extensions
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
