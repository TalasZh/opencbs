

using System;
namespace Octopus.CoreDomain.Database
{
    [Serializable]
    public class SqlDatabaseSettings
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Version { get; set; }
        public string BranchCode { get; set; }
    }
}