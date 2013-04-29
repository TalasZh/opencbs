using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octopus.Shared.Settings.Remote
{
    public class RemoteServerSettings
    {
        public static IRemoteServerSettings GetSettings()
        {
            return new XmlRemoteServerSettings();
        }
    }
}
