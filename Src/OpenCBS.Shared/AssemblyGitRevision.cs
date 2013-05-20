using System;

namespace OpenCBS.Shared
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AssemblyGitRevision : Attribute
    {
        public string Revision { get; set; }
        public AssemblyGitRevision(string revision)
        {
            Revision = revision;
        }
    }
}
