// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCBS.CoreDomain.Database
{
    public class SqlAccountsDatabase
    {
        public string Account { get; set; }
        public string Database { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Version { get; set; }
        public bool Active { get; set; }
    }
}
