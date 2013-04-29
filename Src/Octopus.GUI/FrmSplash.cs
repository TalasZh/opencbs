//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Octopus.CoreDomain;
using Octopus.GUI.Database;
using Octopus.GUI.Login;
using Octopus.GUI.UserControl;
using Octopus.MultiLanguageRessources;
using Octopus.Services;
using Octopus.Shared.Settings;

namespace Octopus.GUI
{
    public partial class FrmSplash : SweetBaseForm
    {
        private bool _settingsAreOk;
        private bool _sqlServerConnectionIsOk;
        private bool _sqlDatabaseConnectionIsOk;
        private bool _sqlDatabaseVersionIsOk;
        private bool _sqlDatabaseSchemaIsOk;
        private bool _crystalReportIsOk;
        private bool _environnementIsOk;
        private bool _applicationSettingsAreOk;
        private bool _accountingSettingsAreOk;
        private string _user;
        private string _password;

        public FrmSplash(string pUserName, string pPassword)
        {
            InitializeComponent();
            _user = pUserName;
            _password = pPassword;
        }

        private void FrmSplash_Shown(object sender, EventArgs e)
        {
            bWOneToSeven.RunWorkerAsync();
        }

        private void _CheckOctopusConfiguration(bool pOneToSeven)
        {
            if(pOneToSeven)
            {
                if (!_settingsAreOk) _CheckTechnicalSettings();

                if (!TechnicalSettings.UseOnlineMode)
                {
                    if (!_sqlServerConnectionIsOk) _CheckSQLServerConnection();
                    if (!_sqlDatabaseConnectionIsOk) _CheckSQLDatabaseConnection();
                    if (!_sqlDatabaseVersionIsOk) _CheckDatabaseVersion();
                    if (!_sqlDatabaseSchemaIsOk) _CheckDatabaseSchema();
                }
                if (!_crystalReportIsOk) _CheckCrystalReports();
                if (!_environnementIsOk) _CheckEnvironnement();
            }
            else
            {
                if (!_applicationSettingsAreOk) 
                    _CheckApplicationSettings();
                if (!_accountingSettingsAreOk)
                    _CheckGeneralSettings();
            }
        }

        private void _CheckApplicationSettings()
        {
            bWSeventToEight.ReportProgress(8, MultiLanguageStrings.GetString(Ressource.FrmSplash, "CheckApplicationSettings.Text"));
            ServicesProvider.GetInstance().GetApplicationSettingsServices().CheckApplicationSettings();

            _applicationSettingsAreOk = true;
        }
        
        private void _CheckGeneralSettings()
        {
            bWSeventToEight.ReportProgress(9, MultiLanguageStrings.GetString(Ressource.FrmSplash, "CheckAccountingSettings.Text"));
            ServicesProvider.GetInstance().GetChartOfAccountsServices().CheckGeneralSettings();
            _accountingSettingsAreOk = true;
        }
        
        private void _LoadLoginForm(bool pUseOnlineMode)
        {
            if (!pUseOnlineMode)
            {
                FrmLogin login = new FrmLogin(_user, _password);
                login.ShowDialog();
            }
            else
                new PasswordFormOnline().ShowDialog();
        }

        private void _CheckEnvironnement()
        {
            bWOneToSeven.ReportProgress(7, MultiLanguageStrings.GetString(Ressource.FrmSplash, "CheckRunningEnvironnement.Text"));
            _environnementIsOk = true;
        }

        private static Dictionary<string, string> GetDataFromRegistryPathWhereCrystalReports(string Path)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(Path))
            {
                if(rk != null)
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            //If the key has value, continue, if not, skip it:
                            if (sk != null)
                                if (!(sk.GetValue("DisplayName") == null))
                                {
                                    if ((sk.GetValue("DisplayName").ToString().Contains("CrystalReport") 
                                        || sk.GetValue("DisplayName").ToString().Contains("Crystal Report")))
                                        dic.Add(sk.GetValue("DisplayName").ToString(), sk.GetValue("InstallLocation").ToString());
                                }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }

            return dic;
        }

        private Dictionary<string, string> GetCrystalReportPaths()
        {
            //The registry keys:
            const string SoftwareKey32 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            const string SoftwareKey64 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            Dictionary<string, string> TotalDict = new Dictionary<string, string>();

            Dictionary<string, string> dict32 = GetDataFromRegistryPathWhereCrystalReports(SoftwareKey32);
            Dictionary<string, string> dict64 = GetDataFromRegistryPathWhereCrystalReports(SoftwareKey64);

            foreach (KeyValuePair<string, string> pair in dict32)
            {
                if (!TotalDict.ContainsKey(pair.Key))
                    TotalDict.Add(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<string, string> pair in dict64)
            {
                if (!TotalDict.ContainsKey(pair.Key))
                    TotalDict.Add(pair.Key, pair.Value);
            }

            return TotalDict;
        }

        private void _CheckCrystalReports()
        {
            bWOneToSeven.ReportProgress(6, MultiLanguageStrings.GetString(Ressource.FrmSplash, "CheckCrystalReportInstall.Text"));

            Dictionary<string, string> dictPaths = GetCrystalReportPaths();

            if (dictPaths.Count == 0)
            {
                _crystalReportIsOk = false;
            }
            else
            {
                _crystalReportIsOk = true;
            }

            if(!_crystalReportIsOk)
                MessageBox.Show(@MultiLanguageStrings.GetString(Ressource.FrmSplash, "NoCrystalReportsInstalled.Text"),
                @"Octopus Control Panel");

            _crystalReportIsOk = true;
        }
       
        private void _CheckDatabaseSchema()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(UserSettings.Language);
            bWOneToSeven.ReportProgress(5, MultiLanguageStrings.GetString(Ressource.FrmSplash, "CheckDatabaseSchema.Text"));
            if (!string.IsNullOrEmpty(ServicesProvider.GetInstance().GetDatabaseServices().CheckSQLDatabaseSchema(TechnicalSettings.DatabaseName)))
                _LoadDatabaseSettingsForm(FrmDatabaseSettingsEnum.SqlDatabaseSettings);

            _sqlDatabaseSchemaIsOk = true;
        }

        private void _CheckDatabaseVersion()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(UserSettings.Language);
            if (!ServicesProvider.GetInstance().GetDatabaseServices().CheckSQLDatabaseVersion(TechnicalSettings.SoftwareVersion, TechnicalSettings.DatabaseName))
            {
                // Automatic backup of database
                
                if (Confirm("BackupProcess.Text"))
                {
                    bWOneToSeven.ReportProgress(4, MultiLanguageStrings.GetString(Ressource.FrmSplash, "Backup.Text") + " Path: " + UserSettings.BackupPath);
                    ServicesProvider.GetInstance()
                        .GetDatabaseServices()
                        .RawBackup(TechnicalSettings.DatabaseName,
                                TechnicalSettings.SoftwareVersion
                                , "Upgrade"
                                , UserSettings.BackupPath
                        );
                }

                bWOneToSeven.ReportProgress(4, MultiLanguageStrings.GetString(Ressource.FrmSplash, "CheckDatabaseVersion.Text"));
                _DatabaseUpdateScript();
            }

            _sqlDatabaseVersionIsOk = true;
        }

        private void _CheckSQLDatabaseConnection()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(UserSettings.Language);
            bWOneToSeven.ReportProgress(3, MultiLanguageStrings.GetString(Ressource.FrmSplash, "CheckDatabaseConnection.Text"));
            if (!ServicesProvider.GetInstance().GetDatabaseServices().CheckSQLDatabaseConnection())
                _LoadDatabaseSettingsForm(FrmDatabaseSettingsEnum.SqlServerSettings);

            _sqlDatabaseConnectionIsOk = true;
        }

        private void _CheckSQLServerConnection()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(UserSettings.Language);
            bWOneToSeven.ReportProgress(2, MultiLanguageStrings.GetString(Ressource.FrmSplash, "CheckSQLServerConnection.Text"));
            if (!ServicesProvider.GetInstance().GetDatabaseServices().CheckSQLServerConnection())
                _LoadDatabaseSettingsForm(FrmDatabaseSettingsEnum.SqlServerConnection);

            _sqlServerConnectionIsOk = true;
        }

        private void _CheckTechnicalSettings()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(UserSettings.Language);
            bWOneToSeven.ReportProgress(1, MultiLanguageStrings.GetString(Ressource.FrmSplash, "CheckTechnicalSettings.Text"));
            if (!TechnicalSettings.CheckSettings())
                _LoadDatabaseSettingsForm(FrmDatabaseSettingsEnum.SqlServerConnection);

            _settingsAreOk = true;
        }

        private void _LoadDatabaseSettingsForm(FrmDatabaseSettingsEnum pFrmDatabaseSettingsEnum)
        {
            FrmDatabaseSettings databaseSettings = new FrmDatabaseSettings(pFrmDatabaseSettingsEnum,true,false);
            databaseSettings.ShowDialog();

            _CheckOctopusConfiguration(true);
        }

        private void _DatabaseUpdateScript()
        {
            oPBarMicroProgression.Value = 0;
            oPBMacroProgression.Step = 10;

            DatabaseServices databaseServices = ServicesProvider.GetInstance().GetDatabaseServices();

            databaseServices.UpdateDatabaseEvent += FrmSplash_UpdateDatabaseEvent;

            databaseServices.UpdateDatabase(TechnicalSettings.SoftwareVersion, TechnicalSettings.DatabaseName, UserSettings.GetUpdatePath);

            _CheckOctopusConfiguration(true);
        }

        private void FrmSplash_UpdateDatabaseEvent(string pCurrentDatabase, string pExpectedDatabase)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(UserSettings.Language);
            string updateText = pCurrentDatabase == string.Empty && pExpectedDatabase == string.Empty
                                                 ? string.Format(MultiLanguageStrings.GetString(Ressource.FrmSplash, "UpdateDetails.Text"))
                                                 : string.Format((MultiLanguageStrings.GetString(Ressource.FrmSplash, "UpdateFrom.Text")), pCurrentDatabase,
                                                                 pExpectedDatabase);
            bWOneToSeven.ReportProgress(24, updateText); 
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _CheckOctopusConfiguration(true);
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage == 24)
            {
                oPBarMicroProgression.Text = e.UserState.ToString();
                oPBarMicroProgression.PerformStep();
            }
            else
            {
                oPBMacroProgression.Text = string.Format("{0} / 9", e.ProgressPercentage);
                labelConfigurationValue.Text = e.UserState.ToString();
                oPBMacroProgression.PerformStep();
            }
        }

      
        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Hide();
            ServicesProvider.GetInstance().GetApplicationSettingsServices().SetBuildNumber(TechnicalSettings.SoftwareBuild);
            _LoadLoginForm(TechnicalSettings.UseOnlineMode);

            if (User.CurrentUser.Id == 0)
            {
                Close();
                Application.Exit();
                return;
            }

            WindowState = FormWindowState.Normal;
            bWSeventToEight.RunWorkerAsync();
            Show();
        }

        private void bWSeventToEight_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _CheckOctopusConfiguration(false);
        }

        private void bWSeventToEight_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            oPBMacroProgression.Text = string.Format("{0} / 9", e.ProgressPercentage);
            labelConfigurationValue.Text = e.UserState.ToString();
            oPBMacroProgression.PerformStep();
        }

        private void bWSeventToEight_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Close();
        }
    }
}