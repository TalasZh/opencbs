// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain.Clients
{
    [Serializable]
    public class Contact
    {
        public IClient Tiers { get; set; }
        public String Position { get; set; }
        //public ContactRole Role { get; set; }
    }
}
