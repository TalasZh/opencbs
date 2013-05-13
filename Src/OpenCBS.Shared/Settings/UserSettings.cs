// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;
using OpenCBS.Enums;

namespace OpenCBS.Shared.Settings
{
	[Serializable]
	public static class UserSettings
    {
	    public static string GetReportPath
	    {
            get { return Path.Combine(ApplicationDirectory, "Reports"); } 
	    }

        public static string GetTemplatePath
        {
            get { return Path.Combine(ApplicationDirectory, "Template"); }
        }

	    public static string GetInternalReportPath
        {
            get { return Path.Combine(ApplicationDirectory, @"Reports\Internal"); }
        }

	    public static string GetUpdatePath
        {
            get { return Path.Combine(ApplicationDirectory, "Update"); }
        }

	    public static string ApplicationPath
	    {
	        get { return Path.Combine(ApplicationDirectory, "OpenCBS.GUI.exe"); }
	    }

	    public static string ApplicationDirectory
	    {
	        get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
	    }

	    public static string Language
        {
            get { return UserSettingsStore.Default.LANGUAGE; }
            set { _SetSetting("LANGUAGE", value); }
        }

        public static string BackupPath
        {
            get
            {
                if (string.IsNullOrEmpty(UserSettingsStore.Default.BACKUP_PATH))
                {
                    if (Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Backup")))
                    {
                        _SetSetting("BACKUP_PATH", Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Backup"));
                    }
                    else
                    {
                        _SetSetting("BACKUP_PATH", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                    }
                }
                return UserSettingsStore.Default.BACKUP_PATH;
            }
            set { _SetSetting("BACKUP_PATH", value); }
        }

        public static string ExportConsoPath
        {
            get
            {
                if (string.IsNullOrEmpty(UserSettingsStore.Default.EXPORT_PATH))
                {
                    _SetSetting("EXPORT_PATH", Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Export"));
                }
                return UserSettingsStore.Default.EXPORT_PATH;
            }
            set { _SetSetting("EXPORT_PATH", value); }
        }

	    public static bool AutoUpdate
	    {
            get { return UserSettingsStore.Default.AUTO_UPDATE; }
            set { _SetSetting("AUTO_UPDATE", value); }
	    }

        private static void _SetSetting(string pPropertyName, string pValue)
        {
            UserSettingsStore.Default[pPropertyName] = pValue;
            UserSettingsStore.Default.Save();
        }

        private static void _SetSetting(string pPropertyName, bool pValue)
        {
            UserSettingsStore.Default[pPropertyName] = pValue;
            UserSettingsStore.Default.Save();
        }

        private static RegistryKey OctopusKey
        {
            get
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey("Software");
                Debug.Assert(key != null, "Registry: Software key missing");
                key = key.CreateSubKey("OctopusMicroFinanceSuite");
                Debug.Assert(key != null, "Registry: Octopus key missing");
                return key;
            }
        }

        public static byte[] GetAlertState()
        {
            return (byte[]) OctopusKey.GetValue("AlertState", null);
        }

        public static void SetAlertState(byte[] state)
        {
            OctopusKey.SetValue("AlertState", state, RegistryValueKind.Binary);
        }

        public static int GetReportOpenCount(string reportName)
        {
            RegistryKey key = OctopusKey.CreateSubKey(reportName);
            Debug.Assert(key != null, "Registry: report key missing");
            return (int) key.GetValue("OpenCount", 0);
        }

        public static void SetReportOpenCount(string reportName, int count)
        {
            RegistryKey key = OctopusKey.CreateSubKey(reportName);
            Debug.Assert(key != null, "Registry: report key missing");
            key.SetValue("OpenCount", count, RegistryValueKind.DWord);
        }

        public static OReportSortOrder GetReportSortOrder()
        {
            string value = OctopusKey.GetValue("ReportSortOrder", "Alphabet").ToString();
            return (OReportSortOrder) Enum.Parse(typeof (OReportSortOrder), value);
        }

        public static void SetReportSortOrder(OReportSortOrder sortOrder)
        {
            OctopusKey.SetValue("ReportSortOrder", sortOrder.ToString());
        }

        public static string GetUserLanguage()
        {
            return OctopusKey.GetValue("Language", "en-US").ToString();
        }

        public static void SetUserLanguage(string language)
        {
            OctopusKey.SetValue("Language", language);
        }

        private static bool GetBool(string name)
        {
            return Convert.ToBoolean(OctopusKey.GetValue(name, true));
        }

        private static int GetInt(string name, int defaultValue)
        {
            return Convert.ToInt32(OctopusKey.GetValue(name, defaultValue));
        }

        private static void SetBool(string name, bool value)
        {
            OctopusKey.SetValue(name, Convert.ToInt32(value), RegistryValueKind.DWord);
        }

        private static void SetInt(string name, int value)
        {
            OctopusKey.SetValue(name, value, RegistryValueKind.DWord);
        }

        public static bool GetShowAlerts()
        {
            return GetBool("ShowAlerts");
        }

        public static void SetShowAlerts(bool show)
        {
            SetBool("ShowAlerts", show);
        }

        public static bool GetShowLateLoans()
        {
            return GetBool("ShowLateLoans");
        }

        public static void SetShowLateLoans(bool show)
        {
            SetBool("ShowLateLoans", show);
        }

        public static bool GetShowPendingLoans()
        {
            return GetBool("ShowPendingLoans");
        }

        public static void SetShowPendingLoans(bool show)
        {
            SetBool("ShowPendingLoans", show);
        }

        public static bool GetPostponedLoans()
        {
            return GetBool("ShowPostponedLoans");
        }

        public static void SetShowPostponedLoans(bool show)
        {
            SetBool("ShowPostponedLoans", show);
        }

        public static bool GetValidatedLoans()
        {
            return GetBool("ShowValidatedLoans");
        }

        public static void SetShowValidatedLoans(bool show)
        {
            SetBool("ShowValidatedLoans", show);
        }

        public static bool GetShowOverdraftSavings()
        {
            return GetBool("ShowOverdraftSavings");
        }

        public static void SetShowOverdraftSavings(bool show)
        {
            SetBool("ShowOverdraftSavings", show);
        }

        public static bool GetShowPendingSavings()
        {
            return GetBool("ShowPendingSavings");
        }

        public static void SetShowPendingSavings(bool show)
        {
            SetBool("ShowPendingSavings", show);
        }

        public static void SetLoadAlerts(bool load)
        {
            SetBool("LoadAlerts", load);
        }

        public static bool GetLoadAlerts()
        {
            return GetBool("LoadAlerts");
        }

        public static int GetAlertsWidth()
        {
            return GetInt("AlertsWidth", 300);
        }

        public static void SetAlertsWidth(int value)
        {
            SetInt("AlertsWidth", value);
        }

	    private const int ReportStarred = 1;
	    private const int ReportUnstarred = 0;

        public static void SetReportStarred(string reportName, bool starred)
	    {
            RegistryKey key = OctopusKey.CreateSubKey(reportName);
            Debug.Assert(key != null, "Registry: report key missing");
            key.SetValue("Starred", starred ? ReportStarred : ReportUnstarred, RegistryValueKind.DWord);
	    }

        public static bool GetReportStarred(string reportName)
        {
            RegistryKey key = OctopusKey.CreateSubKey(reportName);
            Debug.Assert(key != null, "Registry: report key missing");
            return Convert.ToBoolean(key.GetValue("Starred", ReportStarred));
        }

        public static List<string> GetUnstarredBulk()
        {
            return OctopusKey
                .GetSubKeyNames()
                .Where(r => !GetReportStarred(r))
                .ToList();
        }
    }
}
