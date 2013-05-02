// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCBS.DatabaseConnection
{
    public static class ServerSettings
    {
        public static string ServerName
        {
            get { return Server.Default.SERVER; }
        }
        public static string Username
        {
            get { return Server.Default.USERNAME; }
        }
        public static string Password
        {
            get { return Server.Default.PASSWORD; }
        }
        public static int DebugLevel
        {
            get { return Server.Default.DEBUG_LEVEL; }
        }
        public static int Timeout
        {
            get { return Server.Default.TIMEOUT; }
        }
    }
}
