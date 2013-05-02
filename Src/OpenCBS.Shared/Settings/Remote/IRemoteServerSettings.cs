using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCBS.Shared.Settings.Remote
{
    public interface IRemoteServerSettings
    {
        string ServerName { get; set; }
        string LoginName { get; set; }
        string Password { get; set; }
    }
}
