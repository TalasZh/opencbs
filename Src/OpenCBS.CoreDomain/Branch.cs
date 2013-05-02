// LICENSE PLACEHOLDER

namespace OpenCBS.CoreDomain
{
    public class Branch
    {
        public int Id;
        public string Name;
        public bool Deleted;
        public string Address;
        public string Code;
        public string Description;
        
        public override string ToString()
        {
            return Name;
        }
        
    }
}
