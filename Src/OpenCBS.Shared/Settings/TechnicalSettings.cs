// LICENSE PLACEHOLDER

using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Win32;

namespace OpenCBS.Shared.Settings
{
    [Serializable]
    public static class TechnicalSettings
    {
        public const string ASSEMBLY_VERSION = "1.1.1.*";
        public const string COMPANY = "Octopus Micro Finance";
        public const string PRODUCT = "OCTOPUS Micro Finance Suite";
        public const string COPYRIGHT = "Octopus Micro Finance";
        public const string TRADEMARK = "Octopus Micro Finance";
        public const string BaseRegistryPath = @"Software\OctopusMicroFinanceSuite";

        private static bool _useOnlineMode;
        private static bool _useDebugMode;
        private static string _remotingServer;
        private static int _remotingServerPort;

        private static readonly Dictionary<string, string> Settings = new Dictionary<string, string>();

        static TechnicalSettings()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(GetRegistryPath());
            if (key != null)
            {
                key.Close();
                return;
            }

            using (key = Registry.LocalMachine.OpenSubKey(BaseRegistryPath))
            {
                if (null == key) return;
                string[] subkeys = key.GetSubKeyNames();
                Version version = null;
                foreach (string subkey in subkeys)
                {
                    string number = subkey.Split(new[] {' '})[1];
                    Version v = new Version(number);
                    if (null == version || version < v) version = v;
                }

                string databaseName = string.Empty;
                string databaseServerName = "(local)";
                string databaseUser = "sa";
                string databasePassword = "octopus";
                string databaseTimeout = "10";
                string databaseList = string.Empty;
                if (version != null)
                {
                    string currentVersion = string.Format("Octopus {0}.{1}.{2}", 
                        version.Major, 
                        version.Minor,
                        version.Build);
                    string sourceKeyPath = Path.Combine(BaseRegistryPath, currentVersion);
                    using (RegistryKey sourceKey = Registry.LocalMachine.OpenSubKey(sourceKeyPath))
                    {
                        if (sourceKey != null)
                        {
                            databaseServerName = sourceKey.GetValue("DATABASE_SERVER_NAME").ToString();
                            databaseName = sourceKey.GetValue("DATABASE_NAME").ToString();
                            databaseUser = sourceKey.GetValue("DATABASE_LOGIN_NAME").ToString();
                            databasePassword = sourceKey.GetValue("DATABASE_PASSWORD").ToString();
                            databaseTimeout = sourceKey.GetValue("DATABASE_TIMEOUT").ToString();
                            databaseList = sourceKey.GetValue("DATABASE_LIST").ToString();
                        }
                    }
                }

                using (RegistryKey targetKey = Registry.LocalMachine.CreateSubKey(GetRegistryPath()))
                {
                    if (targetKey != null)
                    {
                        targetKey.SetValue("DATABASE_SERVER_NAME", databaseServerName);
                        targetKey.SetValue("DATABASE_NAME", databaseName);
                        targetKey.SetValue("DATABASE_LOGIN_NAME", databaseUser);
                        targetKey.SetValue("DATABASE_PASSWORD", databasePassword);
                        targetKey.SetValue("DATABASE_TIMEOUT", databaseTimeout);
                        targetKey.SetValue("DATABASE_LIST", databaseList);
                    }
                }
            }
        }

        public static void GenerateBaseKey()
        {
            string registryPath = GetRegistryPath();
            Registry.LocalMachine.CreateSubKey(registryPath);
        }

        public static string DatabaseName
        {
            get { return GetValue("DATABASE_NAME", "Octopus"); }
            set { SetValue("DATABASE_NAME", value); }
        }

        public static string DatabaseServerName
        {
            get { return GetValue("DATABASE_SERVER_NAME", "localhost"); }
            set { SetValue("DATABASE_SERVER_NAME", value); }
        }

        public static string DatabaseLoginName
        {
            get { return GetValue("DATABASE_LOGIN_NAME", "sa"); }
            set { SetValue("DATABASE_LOGIN_NAME", value); }
        }

        public static string DatabasePassword
        {
            get { return GetValue("DATABASE_PASSWORD", "octopus"); }
            set { SetValue("DATABASE_PASSWORD", value); }
        }

        public static List<string> AvailableDatabases
        {
            get
            {
                List<string> retval = new List<string>();
                string val = GetValue("DATABASE_LIST", string.Empty);
                val = val.Replace(" ", "");
                if (string.IsNullOrEmpty(val)) return retval;

                retval.AddRange(val.Split(','));
                return retval;
            }
        }

        public static void AddAvailableDatabase(string database)
        {
            string val = GetValue("DATABASE_LIST", string.Empty);
            if (string.IsNullOrEmpty(val))
            {
                val = database;
            }
            else
            {
                val += "," + database;
            }
            SetValue("DATABASE_LIST", val);
        }

        public static string CurrentVersion
        {
            get { return GlobalSettings.Default.Version; }
        }

        public static string SoftwareVersion
        {
            get { return "v" + CurrentVersion; }
        }

        public static bool IsThisVersionNewer(string version)
        {
            try
            {
                Version v = new Version(version.ToLower().Replace("v", ""));
                Version c = new Version(CurrentVersion);
                return (v > c);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public static bool UseOnlineMode
        {
            get { return _useOnlineMode; }
            set { _useOnlineMode = value;}
        }

        public static bool UseDebugMode
        {
            get { return _useDebugMode; }
            set { _useDebugMode = value; }
        }

        public static bool SentQuestionnaire
        {
            get { return Convert.ToBoolean(GetValue("SENT_QUESTIONNAIRE", "True")); }
            set { SetValue("SENT_QUESTIONNAIRE", value ? "True" : "False"); }
        }

        public static string RemotingServer
        {
            get
            {
                return _remotingServer;
            }
            set
            {
                _remotingServer = value;
            }
        }

        public static int RemotingServerPort
        {
            get { return _remotingServerPort; }
            set { _remotingServerPort = value; }
        }

        public static int DatabaseTimeout
        {
            get
            {
                string dbTimeoutStr = GetValue("DATABASE_TIMEOUT", "300");
                int dbTimeout;
                return int.TryParse(dbTimeoutStr, out dbTimeout) ? dbTimeout : 300;
            }
        }

        public static int DebugLevel
        {
            get { return 0; }
        }

        public static string SoftwareVersionWithBuild
        {
            get
            {
                string build = SoftwareBuild;
                if (string.IsNullOrEmpty(build)) return SoftwareVersion;
                return string.Format("{0}.{1}", SoftwareVersion, build);
            }
        }

        public static string SoftwareBuild
        {
            get { return GetRevisionNumberFromFile(); }
        }

        private static string GetRevisionNumberFromFile()
        {
            string revisionNumber;

            try
            {
                TextReader textReader = new StreamReader(Application.StartupPath + "\\" + "BuildLabel.txt");
                revisionNumber = textReader.ReadLine();
                if (string.IsNullOrEmpty(revisionNumber))
                    return string.Empty;
            }
            catch (FileNotFoundException)
            {
                return string.Empty;
            }

            try
            {
                Convert.ToInt32(revisionNumber);
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return revisionNumber;
        }

        public static bool CheckSettings()
        {
            List<string> values = new List<string>();
            values.Add(DatabaseLoginName);
            values.Add(DatabaseName);
            values.Add(DatabasePassword);
            values.Add(DatabaseServerName);
            foreach (string value in values)
            {
                if (string.IsNullOrEmpty(value)) return false;
            }
            return true;
        }

        private static string GetRegistryPath()
        {
            return Path.Combine(BaseRegistryPath, string.Format("Octopus {0}", CurrentVersion));
        }

        private static void SetValue(string key, string value)
        {
            Settings[key] = value;

            string path = GetRegistryPath();
            using (RegistryKey reg = Registry.LocalMachine.OpenSubKey(path, true))
            {
                if (null == reg) return;
                reg.SetValue(key, value);
            }
        }

        private static string GetValue(string key, string defaultValue)
        {
            if (Settings.ContainsKey(key))
            {
                return Settings[key];
            }

            string path = GetRegistryPath();
            using (RegistryKey reg = Registry.LocalMachine.OpenSubKey(path, true))
            {
                if (null == reg) return defaultValue;
                string value = reg.GetValue(key, defaultValue).ToString();
                Settings[key] = value;
                return value;
            }
        }
    }
}
