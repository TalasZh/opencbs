// LICENSE PLACEHOLDER

using OpenCBS.Shared.Settings;

namespace OpenCBS.Shared
{
    public class SqlServerSettings
    {
        public string DatabaseName { get; set; }
        public string ServerName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public SqlServerSettings()
        {
            DatabaseName = TechnicalSettings.DatabaseName;
            ServerName = TechnicalSettings.DatabaseServerName;
            Username = TechnicalSettings.DatabaseLoginName;
            Password = TechnicalSettings.DatabasePassword;
        }
    }
}
