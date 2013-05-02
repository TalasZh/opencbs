// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Database;
using OpenCBS.GUI.Tools;
using OpenCBS.GUI.UserControl;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Shared.Settings;

namespace OpenCBS.GUI.Database
{
    public partial class FrmDatabaseSettings : SweetBaseForm
    {
        private bool _badServerName;
        private bool _badLoginName;
        private bool _badPasswordName;
        private List<SqlDatabaseSettings> _sqlDatabases;
        private bool _exitApplicationIfClose;
        private readonly bool _showBackupRestoreButtons;
        
        private struct DatabaseOperation
        {
            public SqlDatabaseSettings Settings;
            public string File;
        }
        
        public FrmDatabaseSettings(FrmDatabaseSettingsEnum pDatabaseSettingsEnum, bool pExitApplicationIfClose, bool pShowBackupRestoreButtons)
        {
            InitializeComponent();
            _exitApplicationIfClose = pExitApplicationIfClose;
            _showBackupRestoreButtons = pShowBackupRestoreButtons;
            cbServerName.Text = TechnicalSettings.DatabaseServerName;
            txtLoginName.Text = TechnicalSettings.DatabaseLoginName;
            txtPassword.Text = TechnicalSettings.DatabasePassword;

            InitializeForm(pDatabaseSettingsEnum);
        }

        private void InitializeForm(FrmDatabaseSettingsEnum pFrmDatabaseSettingsEnum)
        {
            lblResultMessage.Text = string.Empty;
            if (pFrmDatabaseSettingsEnum == FrmDatabaseSettingsEnum.SqlServerConnection)
            {
                tabControlDatabase.TabPages.Remove(tabPageSQLServerSettings);
                tabControlDatabase.TabPages.Remove(tabPageSqlDatabaseSettings);
                InitializeTabPageServerConnection();
            }
            else if(pFrmDatabaseSettingsEnum == FrmDatabaseSettingsEnum.SqlServerSettings)
            {
                tabControlDatabase.TabPages.Remove(tabPageSQLServerConnection);
                tabControlDatabase.TabPages.Remove(tabPageSqlDatabaseSettings);
                InitializeTabPageSqlServerSettings();
            }
            else
            {
                tabControlDatabase.TabPages.Remove(tabPageSQLServerConnection);
                tabControlDatabase.TabPages.Remove(tabPageSQLServerSettings);
                InitializeTabPageSqlDatabaseSettings(TechnicalSettings.DatabaseName);
            }
        }

        private void InitializeTabPageServerConnection()
        {
            btnSave.Visible = false;
            btnDatabaseConnection.Visible = true;
            tableLayoutPanelServerSettings.Controls.Add(groupBoxSaveSettings, 0, 2);
        }

        private void buttonGetServersList_Click(object sender, EventArgs e)
        {
            DetectServers();
        }

        private void DetectServers()
        {
            Cursor = Cursors.WaitCursor;

            var sie = new SQLInfoEnumerator();
            cbServerName.Items.Clear();
            cbServerName.Items.AddRange(sie.EnumerateSQLServers());

            Cursor = Cursors.Default;
        }

        private void cbServerName_TextChanged(object sender, EventArgs e)
        {
            cbServerName.BackColor = Color.White;
            _badServerName = false;
            if (string.IsNullOrEmpty(cbServerName.Text))
            {
                cbServerName.BackColor = Color.Red;
                _badServerName = true;
            }
        }

        private void txtLoginName_TextChanged(object sender, EventArgs e)
        {
            txtLoginName.BackColor = Color.White;
            _badLoginName = false;
            if (string.IsNullOrEmpty(txtLoginName.Text))
            {
                txtLoginName.BackColor = Color.Red;
                _badLoginName = true;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
            _badPasswordName = false;
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.BackColor = Color.Red;
                _badPasswordName = true;
            }
        }

        private void btnDatabaseConnection_Click(object sender, EventArgs e)
        {
            if (_badLoginName || _badPasswordName || _badServerName)
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "CorrectLabels.Text"),@"Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            else
            {
                TechnicalSettings.DatabaseServerName = cbServerName.Text;
                TechnicalSettings.DatabaseLoginName = txtLoginName.Text;
                TechnicalSettings.DatabasePassword = txtPassword.Text;
                CheckDatabaseSettings();
            }
        }

        private void CheckDatabaseSettings()
        {
            if (DatabaseServices.CheckSQLServerConnection())
            {
                tabControlDatabase.TabPages.Add(tabPageSQLServerSettings);
                tabControlDatabase.TabPages.Remove(tabPageSQLServerConnection);
                InitializeTabPageSqlServerSettings();
            }
            else
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "ConnectionWasNotMade.Text"), @"Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void InitializeTabPageSqlDatabaseSettings(string pDatabaseName)
        {
            SqlDatabaseSettings sqlDatabaseSettings = DatabaseServices.GetSQLDatabaseSettings(pDatabaseName);
            InitializeTabPageSqlDatabaseSettings(sqlDatabaseSettings);
        }

        private void InitializeTabPageSqlDatabaseSettings(SqlDatabaseSettings pSqlDatabaseSettings)
        {
            lblSQLServerSettings.Text = string.Format(" {0}: {1}     {2}:  {3}     {4}:  ****", 
                MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "Server.Text"), TechnicalSettings.DatabaseServerName,
                MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "Login.Text"), TechnicalSettings.DatabaseLoginName,
                MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "Password.Text"));

            lblSQLDatabaseSettingsName.Text = string.Format("{0}:  {1}            {2}:  {3}            {4}:  {5}", 
                MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "Database.Text"), pSqlDatabaseSettings.Name,
                 MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "BranchCode.Text"), pSqlDatabaseSettings.BranchCode,
                 MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "Size.Text"), pSqlDatabaseSettings.Size);
            lblSQLDatabaseSettingsVersion.Text = string.Format("{0}:  {1}",  MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "Version.Text"), pSqlDatabaseSettings.Version);

            btnSave.Visible = true;
            btnDatabaseConnection.Visible = false;
            if (pSqlDatabaseSettings.Version != TechnicalSettings.SoftwareVersion)
            {
                tBDatabaseSettingsSchemaResult.Text = "";
                lblDatabaseSettingsMessage.Visible = true;
                lblDatabaseSettingsMessage.Text = MultiLanguageStrings.GetString(
                    Ressource.FrmDatabaseSettings, "UpgradeYourDatabase.Text");
                btnSQLDatabaseSettingsUpgrade.Text = string.Format("{0} {1} {2}", MultiLanguageStrings.GetString(
                    Ressource.FrmDatabaseSettings, "Upgrade.Text"), 
                    MultiLanguageStrings.GetString(
                    Ressource.FrmDatabaseSettings, "To.Text"), TechnicalSettings.SoftwareVersion);
                btnSQLDatabaseSettingsUpgrade.Enabled = true;
                btnSQLDatabaseSettingsUpgrade.Tag = pSqlDatabaseSettings;
            }
            else
            {
                lblDatabaseSettingsMessage.Visible = false;
                btnSQLDatabaseSettingsUpgrade.Text = string.Format(MultiLanguageStrings.GetString(
                    Ressource.FrmDatabaseSettings, "Upgrade.Text"));
                btnSQLDatabaseSettingsUpgrade.Enabled = false;
                CheckDatabaseStructure(pSqlDatabaseSettings.Name);
            }

            lblResultMessage.Text = string.Empty;
            tableLayoutPanelDatabaseSettings.Controls.Add(groupBoxSQLSettings, 0, 0);
            tableLayoutPanelDatabaseSettings.Controls.Add(groupBoxSaveSettings, 0, 3);
        }

        private void CheckDatabaseStructure(string pDatabaseName)
        {
            string result = DatabaseServices.CheckSQLDatabaseSchema(pDatabaseName);
            tBDatabaseSettingsSchemaResult.Text = !string.IsNullOrEmpty(result) 
                ? string.Format(result, 
                MultiLanguageStrings.GetString(
                    Ressource.FrmDatabaseSettings, "AdditionalColumns.Text"),
                     MultiLanguageStrings.GetString(
                    Ressource.FrmDatabaseSettings, "MissingColumns.Text"))
                : MultiLanguageStrings.GetString(
                    Ressource.FrmDatabaseSettings, "StructureConforms.Text");

            if(TechnicalSettings.UseDebugMode)
                if(!string.IsNullOrEmpty(result)) btnContinue.Visible = true;
        }

        private void InitializeTabPageSqlServerSettings()
        {
            lblSQLServerSettings.Text = string.Format("{0}:  {1}     {2}:  {3}     {4}:  ****", 
                MultiLanguageStrings.GetString(
                    Ressource.FrmDatabaseSettings, "Server.Text"), TechnicalSettings.DatabaseServerName,
                    MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "Login.Text"),TechnicalSettings.DatabaseLoginName,
                    MultiLanguageStrings.GetString(
                    Ressource.FrmDatabaseSettings, "Password.Text"));
            tableLayoutPanelSQLSettings.Controls.Add(groupBoxSQLSettings, 0, 0);
            tableLayoutPanelSQLSettings.Controls.Add(groupBoxSaveSettings, 0, 2);
            lblResultMessage.Text = string.Empty;

            btnBackup.Visible = _showBackupRestoreButtons;
            btnRestore.Visible = _showBackupRestoreButtons;

            btnSave.Visible = true;
            btnDatabaseConnection.Visible = false;
            btnSQLServerChangeSettings.Enabled = false;
            bWDatabasesDetection.RunWorkerAsync();
            DisplayDatabases();
        }

        private void DisplayDatabases()
        {
            if (_sqlDatabases == null)
            {
                groupBoxDatabaseManagement.Enabled = false;
                lblDetectDatabasesInProgress.Visible = true;
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                listViewDatabases.Items.Clear();
                lblDatabases.Text = string.Format("{0} ({1})", 
                    MultiLanguageStrings.GetString(
                    Ressource.FrmDatabaseSettings, "Databases.Text"),
                    _sqlDatabases.Count);

                _sqlDatabases.Sort((x,y) => x.Name.CompareTo(y.Name));
                foreach (SqlDatabaseSettings sqlDatabase in _sqlDatabases)
                {
                    ListViewItem item = new ListViewItem(sqlDatabase.Name);
                    item.SubItems.Add(sqlDatabase.BranchCode);
                    item.SubItems.Add(sqlDatabase.Version);
                    item.SubItems.Add(sqlDatabase.Size);
                    item.Tag = sqlDatabase;
                    if (sqlDatabase.Name == TechnicalSettings.DatabaseName)
                    {
                        item.BackColor = Color.Green;
                        listViewDatabases.Items.Insert(0, item);
                        item.Selected = true;
                    }
                    else
                        listViewDatabases.Items.Add(item);
                }

            }
            Cursor = Cursors.Default;
            listViewDatabases.Focus();
        }

        private void linkLabelSQLServerInstallInstruction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.octopusnetwork.org/dmdocuments/Octopus_Installation_Guide-EN.pdf");
        }

        private void FrmDatabaseSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(_exitApplicationIfClose) Environment.Exit(0);
        }

        private void listViewDatabases_DoubleClick(object sender, EventArgs e)
        {
            _ShowDatabaseDetails();
        }

        private void buttonSetAsDefault_Click(object sender, EventArgs e)
        {
            if (listViewDatabases.SelectedItems.Count == 0)
            {
                lblResultMessage.ForeColor = Color.Red;
                lblResultMessage.Text = string.Format(MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "SelectInList.Text")); 
                return;
            }

            TechnicalSettings.DatabaseName = listViewDatabases.SelectedItems[0].Text;
            DisplayDatabases();
            _exitApplicationIfClose = true;
            lblResultMessage.ForeColor = Color.Black;
            lblResultMessage.Text = string.Format("{0} {1}", TechnicalSettings.DatabaseName, MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "DefaultDatabase.Text"));
        }

        private void buttonSQLServerChangeSettings_Click(object sender, EventArgs e)
        {
            tabControlDatabase.TabPages.Remove(tabPageSqlDatabaseSettings);
            tabControlDatabase.TabPages.Remove(tabPageSQLServerSettings);
            tabControlDatabase.TabPages.Add(tabPageSQLServerConnection);
            InitializeTabPageServerConnection();
            _exitApplicationIfClose = false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TechnicalSettings.DatabaseName))
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "NoSelectBase.Text"), @"Error", MessageBoxButtons.OK,
                                   MessageBoxIcon.Information); 
                return;
            }
            if (_exitApplicationIfClose)
            {
                Restart.LaunchRestarter();
            }
            else
            {
                Close();
            }
        }

        private void buttonBackup_Click(object sender, EventArgs e)
        {
            if (listViewDatabases.SelectedItems.Count == 0)
            {
                lblResultMessage.ForeColor = Color.Red;
                lblResultMessage.Text = 
                    string.Format(MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "SelectInList.Text"));
                return;
            }

            folderBrowserDialog.SelectedPath = UserSettings.BackupPath;
            if (DialogResult.OK != folderBrowserDialog.ShowDialog()) return;

            UserSettings.BackupPath = folderBrowserDialog.SelectedPath;

            SqlDatabaseSettings settings = (SqlDatabaseSettings) listViewDatabases.SelectedItems[0].Tag;

            groupBoxDatabaseManagement.Enabled = false;
            lblResultMessage.ForeColor = Color.Black;
            lblResultMessage.Text =
                string.Format("{0} {1} {2} ", MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings,
                                                                             "BackUpDatabase.Text"),
                              settings.Name, MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings,
                                                                               "InProgress.Text"));

            bWDatabaseBackup.RunWorkerAsync(settings);
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            if (listViewDatabases.SelectedItems.Count == 0)
            {
                lblResultMessage.ForeColor = Color.Red;
                lblResultMessage.Text = string.Format(MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "SelectInList.Text"));
                return;
            }

            openFileDialog.FileName = "";
            if (DialogResult.OK != openFileDialog.ShowDialog()) return;
            SqlDatabaseSettings sqlDatabase = (SqlDatabaseSettings) listViewDatabases.SelectedItems[0].Tag;
            btnRestore.Tag = sqlDatabase;

            DatabaseOperation dbo = new DatabaseOperation {Settings = sqlDatabase, File = openFileDialog.FileName};

            _exitApplicationIfClose = true;
            groupBoxDatabaseManagement.Enabled = false;
            groupBoxSQLSettings.Enabled = false;
            btnSave.Enabled = false;
            lblResultMessage.ForeColor = Color.Black;
            lblResultMessage.Text = string.Format("{0} {1} {2}",
                                             MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings,
                                                                            "RestoreDatabase.Text.Text"),
                                             sqlDatabase.Name,
                                             MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings,
                                                                            "InProgress.Text"));

            bWDatabaseRestore.RunWorkerAsync(dbo);
        }

        private void buttonSQLServerSettingsShowDetails_Click(object sender, EventArgs e)
        {
            _ShowDatabaseDetails();
        }

        private void _ShowDatabaseDetails()
        {
            if (listViewDatabases.SelectedItems.Count == 0)
            {
                lblResultMessage.ForeColor = Color.Red;
                lblResultMessage.Text = string.Format(MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "SelectInList.Text"));
                return;
            }

            SqlDatabaseSettings sqlDatabaseSettings = (SqlDatabaseSettings) listViewDatabases.SelectedItems[0].Tag;
            tabControlDatabase.TabPages.Remove(tabPageSQLServerSettings);
            tabControlDatabase.TabPages.Add(tabPageSqlDatabaseSettings);
            InitializeTabPageSqlDatabaseSettings(sqlDatabaseSettings);
        }

        private void buttonSQLDatabaseSettingsChangeName_Click(object sender, EventArgs e)
        {
            tabControlDatabase.TabPages.Remove(tabPageSqlDatabaseSettings);
            tabControlDatabase.TabPages.Add(tabPageSQLServerSettings);
            InitializeTabPageSqlServerSettings();
            _exitApplicationIfClose = true;
        }

        private void FrmDatabaseSettings_UpdateDatabaseEvent(string pCurrentDatabase, string pExpectedDatabase)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(UserSettings.Language);

            if (pCurrentDatabase == string.Empty && pExpectedDatabase == string.Empty)
            {
                string st = MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "UpdatingDatabase.Text");
                bWDatabaseUpdate.ReportProgress(1, st);
            }
            else
            {
                string st = string.Format("{0} {1} {2} {3}", MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "UpdateDBFrom.Text"), pCurrentDatabase,
                    MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "To.Text"), pExpectedDatabase);
                bWDatabaseUpdate.ReportProgress(1, st);
            }
        }

        private void buttonSQLDatabaseSettingsUpgrade_Click(object sender, EventArgs e)
        {
            groupBoxSQLDatabaseSettings.Enabled = false;
            groupBoxSQLSettings.Enabled = false;
            btnSave.Enabled = false;
            _exitApplicationIfClose = true;
            lblDatabaseSettingsMessage.Visible = false;
            groupBoxSQLDatabaseStructure.Enabled = false;
            lblResultMessage.Text = MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "UpgradeDatabaseInProgress.Text");
            var sqlDatabaseSettings = (SqlDatabaseSettings)btnSQLDatabaseSettingsUpgrade.Tag;
            bWDatabaseUpdate.RunWorkerAsync(sqlDatabaseSettings);
        }

        public bool FileExists(string pDir, string pFile)
        {
            return File.Exists(Path.Combine(pDir, pFile));
        }
        
        private void buttonCreateNewDatabase_Click(object sender, EventArgs e)
        {
            var frmDatabaseName = new FrmDatabaseName();
            if (DialogResult.OK != frmDatabaseName.ShowDialog()) return;

            string dbName = frmDatabaseName.Result;
            if (DbExists(dbName))
            {
                lblResultMessage.Text = MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "FileException.Text");
            }
            else
            {
                groupBoxDatabaseManagement.Enabled = false;
                lblResultMessage.ForeColor = Color.Black;
                lblResultMessage.Text = MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "DatabaseCreation.Text");
                bWDatabaseCreation.RunWorkerAsync(dbName);
            }
        }

        private bool DbExists(string dbName)
        {
            string dbDefaultPath = DatabaseServices.GetDatabaseDefaultPath();
            string attachmentDbName = dbName + "_attachments";
            return 
                FileExists(dbDefaultPath, dbName + ".mdf") ||
                FileExists(dbDefaultPath, dbName + "_log.ldf") ||
                FileExists(dbDefaultPath, attachmentDbName + ".mdf") ||
                FileExists(dbDefaultPath, attachmentDbName + "_log.ldf");
        }

        private static DatabaseServices DatabaseServices
        {
            get { return ServicesProvider.GetInstance().GetDatabaseServices(); }
        }

        private void backgroundWorkerDetectDatabases_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw e.Error;
            btnSQLServerChangeSettings.Enabled = true;
            groupBoxDatabaseManagement.Enabled = true;
            lblDetectDatabasesInProgress.Visible = false;
            DisplayDatabases();
        }

        private void backgroundWorkerDetectDatabases_DoWork(object sender, DoWorkEventArgs e)
        {
            _sqlDatabases = DatabaseServices.GetSQLDatabasesSettings(
                    TechnicalSettings.DatabaseServerName, TechnicalSettings.DatabaseLoginName,
                    TechnicalSettings.DatabasePassword);
        }

        private void bWDatabaseCreation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            groupBoxDatabaseManagement.Enabled = true;
            lblDetectDatabasesInProgress.Visible = false;
            if(e.Result == null)
            {
                lblResultMessage.ForeColor = Color.Black;
                lblResultMessage.Text = MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "DatabaseCreationCancelled.Text");
            }
            else if ((bool)e.Result)
            {
                lblResultMessage.ForeColor = Color.Black;
                lblResultMessage.Text = string.Format("{0} {1} {2}", MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "Database.Text"), TechnicalSettings.DatabaseName, MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "Created.Text"));
                DisplayDatabases();
                _exitApplicationIfClose = true;
            }
            else
            {
                lblResultMessage.ForeColor = Color.Red;
                lblResultMessage.Text =MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "CreationFailed.Text");
            }
        }

        private void bWDatabaseCreation_DoWork(object sender, DoWorkEventArgs e)
        {
            string name = e.Argument.ToString();
            e.Result = DatabaseServices.CreateDatabase(
                name
                , TechnicalSettings.SoftwareVersion
                , UserSettings.GetUpdatePath
            );
            if (!(bool)e.Result) return;

            TechnicalSettings.DatabaseName = name;
            _sqlDatabases.Add(new SqlDatabaseSettings { Name = TechnicalSettings.DatabaseName, Version = TechnicalSettings.SoftwareVersion, Size = "-" });
            TechnicalSettings.AddAvailableDatabase(name);
        }

        private void bWDatabaseBackup_DoWork(object sender, DoWorkEventArgs e)
        {
            var settings = (SqlDatabaseSettings) e.Argument;
            e.Result = DatabaseServices.RawBackup(
                settings.Name, settings.Version
                , settings.BranchCode
                , UserSettings.BackupPath
            );
        }

        private void bWDatabaseBackup_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            groupBoxDatabaseManagement.Enabled = true;

            if (e.Result == null)
            {
                lblResultMessage.ForeColor = Color.Black;
                lblResultMessage.Text = MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "BackupCancelled.Text");
            }
            else if (e.Result.ToString() != string.Empty)
            {
                lblResultMessage.ForeColor = Color.Black;
                lblResultMessage.Text = string.Format(MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "FileBackedUp.Text"), e.Result);
            }
            else
            {
                lblResultMessage.ForeColor = Color.Red;
                lblResultMessage.Text = MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "BackupCancelled.Text");
            }
        }

        private void bWDatabaseRestore_DoWork(object sender, DoWorkEventArgs e)
        {
            var dbo = (DatabaseOperation) e.Argument;
            e.Result = ServicesProvider.GetInstance()
                .GetDatabaseServices()
                .Restore(dbo.File, dbo.Settings.Name);
        }

        private void bWDatabaseRestore_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            groupBoxDatabaseManagement.Enabled = true;
            lblDetectDatabasesInProgress.Visible = false;
            groupBoxSQLSettings.Enabled = true;
            btnSave.Enabled = true;

            if (e.Result == null)
            {
                lblResultMessage.ForeColor = Color.Black;
                lblResultMessage.Text = MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "RestoreCancelled.Text");
            }
            else if ((bool) e.Result)
            {
                lblResultMessage.ForeColor = Color.Black;
                var sqlDatabase = (SqlDatabaseSettings)btnRestore.Tag;
                lblResultMessage.Text = string.Format(" {0} {1} {2}", MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "RestoreDatabase.Text"), sqlDatabase.Name, MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "Successful.Text"));
                _sqlDatabases.Remove(sqlDatabase);
                _sqlDatabases.Add(DatabaseServices.GetSQLDatabaseSettings(sqlDatabase.Name));
                DisplayDatabases();
            }
            else
            {
                lblResultMessage.ForeColor = Color.Red;
                lblResultMessage.Text =MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "RestoreFailed.Text");
            }
        }

        private void bWDatabaseUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //We must create a databaseService here, otherwise we can't invoking the event
            DatabaseServices databaseServices = DatabaseServices;
            databaseServices.UpdateDatabaseEvent += new DatabaseServices.ExecuteUpgradeSqlDatabaseFile(FrmDatabaseSettings_UpdateDatabaseEvent);

            var sqlDatabaseSettings = (SqlDatabaseSettings) e.Argument;
            e.Result = databaseServices.UpdateDatabase(TechnicalSettings.SoftwareVersion, sqlDatabaseSettings.Name, UserSettings.GetUpdatePath);
        }

        private void bWDatabaseUpdate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            groupBoxSQLDatabaseSettings.Enabled = true;
            groupBoxSQLSettings.Enabled = true;
            btnSave.Enabled = true;
            groupBoxSQLDatabaseStructure.Enabled = true;
            if((bool)e.Result)
            {
                var sqlDatabaseSettings = (SqlDatabaseSettings)btnSQLDatabaseSettingsUpgrade.Tag;
                lblResultMessage.ForeColor = Color.Black;
                lblResultMessage.Text = string.Format("{0} {1} {2}", MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "UpgradeDatabase.Text"), sqlDatabaseSettings.Name, MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "Successful.Text"));
                _sqlDatabases.Remove(sqlDatabaseSettings);
                sqlDatabaseSettings = DatabaseServices.GetSQLDatabaseSettings(sqlDatabaseSettings.Name);
                _sqlDatabases.Add(sqlDatabaseSettings);
                InitializeTabPageSqlDatabaseSettings(sqlDatabaseSettings);
            }
            else
            {
                lblResultMessage.ForeColor = Color.Red;
                lblResultMessage.Text = MultiLanguageStrings.GetString(Ressource.FrmDatabaseSettings, "UpgradeFailed.Text");
            }
        }

        private void bWDatabaseUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblResultMessage.Text = e.UserState.ToString();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            if (_exitApplicationIfClose)
            {
                TechnicalSettings.UseDebugMode = true;
                Restart.LaunchRestarter();
            }
            else
            {
                Close();
            }
        }

        private void buttonDefault_Click(object sender, EventArgs e)
        {
            cbServerName.Text = Environment.MachineName + "\\SQLEXPRESS";
        }
        
    }
}
