using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Database
{
    partial class FrmDatabaseSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDatabaseSettings));
            this.groupBoxSQLServerConnection = new System.Windows.Forms.GroupBox();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnGetServersList = new System.Windows.Forms.Button();
            this.lblInstallInstructionSQLServer = new System.Windows.Forms.Label();
            this.lblHelpServerName = new System.Windows.Forms.Label();
            this.cbServerName = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.lblServerName = new System.Windows.Forms.Label();
            this.lblFindDBInfo = new System.Windows.Forms.Label();
            this.groupBoxSQLSettings = new System.Windows.Forms.GroupBox();
            this.btnSQLServerChangeSettings = new System.Windows.Forms.Button();
            this.lblSQLServerSettings = new System.Windows.Forms.Label();
            this.groupBoxDatabaseManagement = new System.Windows.Forms.GroupBox();
            this.lblDetectDatabasesInProgress = new System.Windows.Forms.Label();
            this.btnSQLServerSettingsShowDetails = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnSetAsDefault = new System.Windows.Forms.Button();
            this.btnCreateNewDatabase = new System.Windows.Forms.Button();
            this.lblDatabases = new System.Windows.Forms.Label();
            this.listViewDatabases = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderBranchCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxSaveSettings = new System.Windows.Forms.GroupBox();
            this.btnDatabaseConnection = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblResultMessage = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControlDatabase = new System.Windows.Forms.TabControl();
            this.tabPageSQLServerConnection = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelServerSettings = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageSQLServerSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelSQLSettings = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageSqlDatabaseSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelDatabaseSettings = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSQLDatabaseSettings = new System.Windows.Forms.GroupBox();
            this.btnSQLDatabaseSettingsUpgrade = new System.Windows.Forms.Button();
            this.btnSQLDatabaseSettingsChangeName = new System.Windows.Forms.Button();
            this.lblSQLDatabaseSettingsVersion = new System.Windows.Forms.Label();
            this.lblSQLDatabaseSettingsName = new System.Windows.Forms.Label();
            this.groupBoxSQLDatabaseStructure = new System.Windows.Forms.GroupBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.lblDatabaseSettingsMessage = new System.Windows.Forms.Label();
            this.tBDatabaseSettingsSchemaResult = new System.Windows.Forms.RichTextBox();
            this.bWDatabasesDetection = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bWDatabaseCreation = new System.ComponentModel.BackgroundWorker();
            this.bWDatabaseBackup = new System.ComponentModel.BackgroundWorker();
            this.bWDatabaseRestore = new System.ComponentModel.BackgroundWorker();
            this.bWDatabaseUpdate = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxSQLServerConnection.SuspendLayout();
            this.groupBoxSQLSettings.SuspendLayout();
            this.groupBoxDatabaseManagement.SuspendLayout();
            this.groupBoxSaveSettings.SuspendLayout();
            this.tabControlDatabase.SuspendLayout();
            this.tabPageSQLServerConnection.SuspendLayout();
            this.tableLayoutPanelServerSettings.SuspendLayout();
            this.tabPageSQLServerSettings.SuspendLayout();
            this.tableLayoutPanelSQLSettings.SuspendLayout();
            this.tabPageSqlDatabaseSettings.SuspendLayout();
            this.tableLayoutPanelDatabaseSettings.SuspendLayout();
            this.groupBoxSQLDatabaseSettings.SuspendLayout();
            this.groupBoxSQLDatabaseStructure.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSQLServerConnection
            // 
            this.groupBoxSQLServerConnection.Controls.Add(this.btnDefault);
            this.groupBoxSQLServerConnection.Controls.Add(this.btnGetServersList);
            this.groupBoxSQLServerConnection.Controls.Add(this.lblInstallInstructionSQLServer);
            this.groupBoxSQLServerConnection.Controls.Add(this.lblHelpServerName);
            this.groupBoxSQLServerConnection.Controls.Add(this.cbServerName);
            this.groupBoxSQLServerConnection.Controls.Add(this.txtPassword);
            this.groupBoxSQLServerConnection.Controls.Add(this.lblPassword);
            this.groupBoxSQLServerConnection.Controls.Add(this.txtLoginName);
            this.groupBoxSQLServerConnection.Controls.Add(this.lblLoginName);
            this.groupBoxSQLServerConnection.Controls.Add(this.lblServerName);
            this.groupBoxSQLServerConnection.Controls.Add(this.lblFindDBInfo);
            resources.ApplyResources(this.groupBoxSQLServerConnection, "groupBoxSQLServerConnection");
            this.groupBoxSQLServerConnection.Name = "groupBoxSQLServerConnection";
            this.groupBoxSQLServerConnection.TabStop = false;
            // 
            // btnDefault
            // 
            resources.ApplyResources(this.btnDefault, "btnDefault");
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Click += new System.EventHandler(this.buttonDefault_Click);
            // 
            // btnGetServersList
            // 
            resources.ApplyResources(this.btnGetServersList, "btnGetServersList");
            this.btnGetServersList.Name = "btnGetServersList";
            this.btnGetServersList.Click += new System.EventHandler(this.buttonGetServersList_Click);
            // 
            // lblInstallInstructionSQLServer
            // 
            resources.ApplyResources(this.lblInstallInstructionSQLServer, "lblInstallInstructionSQLServer");
            this.lblInstallInstructionSQLServer.Name = "lblInstallInstructionSQLServer";
            // 
            // lblHelpServerName
            // 
            resources.ApplyResources(this.lblHelpServerName, "lblHelpServerName");
            this.lblHelpServerName.Name = "lblHelpServerName";
            // 
            // cbServerName
            // 
            this.cbServerName.FormattingEnabled = true;
            resources.ApplyResources(this.cbServerName, "cbServerName");
            this.cbServerName.Name = "cbServerName";
            this.cbServerName.TextChanged += new System.EventHandler(this.cbServerName_TextChanged);
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // lblPassword
            // 
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.Name = "lblPassword";
            // 
            // txtLoginName
            // 
            resources.ApplyResources(this.txtLoginName, "txtLoginName");
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.TextChanged += new System.EventHandler(this.txtLoginName_TextChanged);
            // 
            // lblLoginName
            // 
            resources.ApplyResources(this.lblLoginName, "lblLoginName");
            this.lblLoginName.Name = "lblLoginName";
            // 
            // lblServerName
            // 
            resources.ApplyResources(this.lblServerName, "lblServerName");
            this.lblServerName.Name = "lblServerName";
            // 
            // lblFindDBInfo
            // 
            resources.ApplyResources(this.lblFindDBInfo, "lblFindDBInfo");
            this.lblFindDBInfo.Name = "lblFindDBInfo";
            // 
            // groupBoxSQLSettings
            // 
            this.groupBoxSQLSettings.Controls.Add(this.btnSQLServerChangeSettings);
            this.groupBoxSQLSettings.Controls.Add(this.lblSQLServerSettings);
            resources.ApplyResources(this.groupBoxSQLSettings, "groupBoxSQLSettings");
            this.groupBoxSQLSettings.Name = "groupBoxSQLSettings";
            this.groupBoxSQLSettings.TabStop = false;
            // 
            // btnSQLServerChangeSettings
            // 
            resources.ApplyResources(this.btnSQLServerChangeSettings, "btnSQLServerChangeSettings");
            this.btnSQLServerChangeSettings.Name = "btnSQLServerChangeSettings";
            this.btnSQLServerChangeSettings.Click += new System.EventHandler(this.buttonSQLServerChangeSettings_Click);
            // 
            // lblSQLServerSettings
            // 
            resources.ApplyResources(this.lblSQLServerSettings, "lblSQLServerSettings");
            this.lblSQLServerSettings.Name = "lblSQLServerSettings";
            // 
            // groupBoxDatabaseManagement
            // 
            this.groupBoxDatabaseManagement.Controls.Add(this.lblDetectDatabasesInProgress);
            this.groupBoxDatabaseManagement.Controls.Add(this.btnSQLServerSettingsShowDetails);
            this.groupBoxDatabaseManagement.Controls.Add(this.btnRestore);
            this.groupBoxDatabaseManagement.Controls.Add(this.btnBackup);
            this.groupBoxDatabaseManagement.Controls.Add(this.btnSetAsDefault);
            this.groupBoxDatabaseManagement.Controls.Add(this.btnCreateNewDatabase);
            this.groupBoxDatabaseManagement.Controls.Add(this.lblDatabases);
            this.groupBoxDatabaseManagement.Controls.Add(this.listViewDatabases);
            resources.ApplyResources(this.groupBoxDatabaseManagement, "groupBoxDatabaseManagement");
            this.groupBoxDatabaseManagement.Name = "groupBoxDatabaseManagement";
            this.groupBoxDatabaseManagement.TabStop = false;
            // 
            // lblDetectDatabasesInProgress
            // 
            resources.ApplyResources(this.lblDetectDatabasesInProgress, "lblDetectDatabasesInProgress");
            this.lblDetectDatabasesInProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblDetectDatabasesInProgress.Name = "lblDetectDatabasesInProgress";
            // 
            // btnSQLServerSettingsShowDetails
            // 
            resources.ApplyResources(this.btnSQLServerSettingsShowDetails, "btnSQLServerSettingsShowDetails");
            this.btnSQLServerSettingsShowDetails.Name = "btnSQLServerSettingsShowDetails";
            this.btnSQLServerSettingsShowDetails.Click += new System.EventHandler(this.buttonSQLServerSettingsShowDetails_Click);
            // 
            // btnRestore
            // 
            resources.ApplyResources(this.btnRestore, "btnRestore");
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // btnBackup
            // 
            resources.ApplyResources(this.btnBackup, "btnBackup");
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Click += new System.EventHandler(this.buttonBackup_Click);
            // 
            // btnSetAsDefault
            // 
            resources.ApplyResources(this.btnSetAsDefault, "btnSetAsDefault");
            this.btnSetAsDefault.Name = "btnSetAsDefault";
            this.btnSetAsDefault.Click += new System.EventHandler(this.buttonSetAsDefault_Click);
            // 
            // btnCreateNewDatabase
            // 
            resources.ApplyResources(this.btnCreateNewDatabase, "btnCreateNewDatabase");
            this.btnCreateNewDatabase.Name = "btnCreateNewDatabase";
            this.btnCreateNewDatabase.Click += new System.EventHandler(this.buttonCreateNewDatabase_Click);
            // 
            // lblDatabases
            // 
            resources.ApplyResources(this.lblDatabases, "lblDatabases");
            this.lblDatabases.Name = "lblDatabases";
            // 
            // listViewDatabases
            // 
            this.listViewDatabases.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewDatabases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderBranchCode,
            this.columnHeaderVersion,
            this.columnHeaderSize});
            this.listViewDatabases.FullRowSelect = true;
            this.listViewDatabases.GridLines = true;
            this.listViewDatabases.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewDatabases.HideSelection = false;
            resources.ApplyResources(this.listViewDatabases, "listViewDatabases");
            this.listViewDatabases.Name = "listViewDatabases";
            this.listViewDatabases.UseCompatibleStateImageBehavior = false;
            this.listViewDatabases.View = System.Windows.Forms.View.Details;
            this.listViewDatabases.DoubleClick += new System.EventHandler(this.listViewDatabases_DoubleClick);
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderBranchCode
            // 
            resources.ApplyResources(this.columnHeaderBranchCode, "columnHeaderBranchCode");
            // 
            // columnHeaderVersion
            // 
            resources.ApplyResources(this.columnHeaderVersion, "columnHeaderVersion");
            // 
            // columnHeaderSize
            // 
            resources.ApplyResources(this.columnHeaderSize, "columnHeaderSize");
            // 
            // groupBoxSaveSettings
            // 
            this.groupBoxSaveSettings.Controls.Add(this.btnDatabaseConnection);
            this.groupBoxSaveSettings.Controls.Add(this.btnExit);
            this.groupBoxSaveSettings.Controls.Add(this.lblResultMessage);
            this.groupBoxSaveSettings.Controls.Add(this.btnSave);
            resources.ApplyResources(this.groupBoxSaveSettings, "groupBoxSaveSettings");
            this.groupBoxSaveSettings.Name = "groupBoxSaveSettings";
            this.groupBoxSaveSettings.TabStop = false;
            // 
            // btnDatabaseConnection
            // 
            resources.ApplyResources(this.btnDatabaseConnection, "btnDatabaseConnection");
            this.btnDatabaseConnection.Name = "btnDatabaseConnection";
            this.btnDatabaseConnection.Click += new System.EventHandler(this.btnDatabaseConnection_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Name = "btnExit";
            // 
            // lblResultMessage
            // 
            resources.ApplyResources(this.lblResultMessage, "lblResultMessage");
            this.lblResultMessage.Name = "lblResultMessage";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // tabControlDatabase
            // 
            this.tabControlDatabase.Controls.Add(this.tabPageSQLServerConnection);
            this.tabControlDatabase.Controls.Add(this.tabPageSQLServerSettings);
            this.tabControlDatabase.Controls.Add(this.tabPageSqlDatabaseSettings);
            resources.ApplyResources(this.tabControlDatabase, "tabControlDatabase");
            this.tabControlDatabase.Name = "tabControlDatabase";
            this.tabControlDatabase.SelectedIndex = 0;
            // 
            // tabPageSQLServerConnection
            // 
            this.tabPageSQLServerConnection.Controls.Add(this.tableLayoutPanelServerSettings);
            resources.ApplyResources(this.tabPageSQLServerConnection, "tabPageSQLServerConnection");
            this.tabPageSQLServerConnection.Name = "tabPageSQLServerConnection";
            // 
            // tableLayoutPanelServerSettings
            // 
            resources.ApplyResources(this.tableLayoutPanelServerSettings, "tableLayoutPanelServerSettings");
            this.tableLayoutPanelServerSettings.Controls.Add(this.groupBoxSQLServerConnection, 0, 0);
            this.tableLayoutPanelServerSettings.Name = "tableLayoutPanelServerSettings";
            // 
            // tabPageSQLServerSettings
            // 
            this.tabPageSQLServerSettings.Controls.Add(this.tableLayoutPanelSQLSettings);
            resources.ApplyResources(this.tabPageSQLServerSettings, "tabPageSQLServerSettings");
            this.tabPageSQLServerSettings.Name = "tabPageSQLServerSettings";
            // 
            // tableLayoutPanelSQLSettings
            // 
            resources.ApplyResources(this.tableLayoutPanelSQLSettings, "tableLayoutPanelSQLSettings");
            this.tableLayoutPanelSQLSettings.Controls.Add(this.groupBoxSQLSettings, 0, 0);
            this.tableLayoutPanelSQLSettings.Controls.Add(this.groupBoxSaveSettings, 0, 2);
            this.tableLayoutPanelSQLSettings.Controls.Add(this.groupBoxDatabaseManagement, 0, 1);
            this.tableLayoutPanelSQLSettings.Name = "tableLayoutPanelSQLSettings";
            // 
            // tabPageSqlDatabaseSettings
            // 
            this.tabPageSqlDatabaseSettings.Controls.Add(this.tableLayoutPanelDatabaseSettings);
            resources.ApplyResources(this.tabPageSqlDatabaseSettings, "tabPageSqlDatabaseSettings");
            this.tabPageSqlDatabaseSettings.Name = "tabPageSqlDatabaseSettings";
            // 
            // tableLayoutPanelDatabaseSettings
            // 
            resources.ApplyResources(this.tableLayoutPanelDatabaseSettings, "tableLayoutPanelDatabaseSettings");
            this.tableLayoutPanelDatabaseSettings.Controls.Add(this.groupBoxSQLDatabaseSettings, 0, 1);
            this.tableLayoutPanelDatabaseSettings.Controls.Add(this.groupBoxSQLDatabaseStructure, 0, 2);
            this.tableLayoutPanelDatabaseSettings.Name = "tableLayoutPanelDatabaseSettings";
            // 
            // groupBoxSQLDatabaseSettings
            // 
            this.groupBoxSQLDatabaseSettings.Controls.Add(this.btnSQLDatabaseSettingsUpgrade);
            this.groupBoxSQLDatabaseSettings.Controls.Add(this.btnSQLDatabaseSettingsChangeName);
            this.groupBoxSQLDatabaseSettings.Controls.Add(this.lblSQLDatabaseSettingsVersion);
            this.groupBoxSQLDatabaseSettings.Controls.Add(this.lblSQLDatabaseSettingsName);
            resources.ApplyResources(this.groupBoxSQLDatabaseSettings, "groupBoxSQLDatabaseSettings");
            this.groupBoxSQLDatabaseSettings.Name = "groupBoxSQLDatabaseSettings";
            this.groupBoxSQLDatabaseSettings.TabStop = false;
            // 
            // btnSQLDatabaseSettingsUpgrade
            // 
            resources.ApplyResources(this.btnSQLDatabaseSettingsUpgrade, "btnSQLDatabaseSettingsUpgrade");
            this.btnSQLDatabaseSettingsUpgrade.Name = "btnSQLDatabaseSettingsUpgrade";
            this.btnSQLDatabaseSettingsUpgrade.Click += new System.EventHandler(this.buttonSQLDatabaseSettingsUpgrade_Click);
            // 
            // btnSQLDatabaseSettingsChangeName
            // 
            resources.ApplyResources(this.btnSQLDatabaseSettingsChangeName, "btnSQLDatabaseSettingsChangeName");
            this.btnSQLDatabaseSettingsChangeName.Name = "btnSQLDatabaseSettingsChangeName";
            this.btnSQLDatabaseSettingsChangeName.Click += new System.EventHandler(this.buttonSQLDatabaseSettingsChangeName_Click);
            // 
            // lblSQLDatabaseSettingsVersion
            // 
            resources.ApplyResources(this.lblSQLDatabaseSettingsVersion, "lblSQLDatabaseSettingsVersion");
            this.lblSQLDatabaseSettingsVersion.Name = "lblSQLDatabaseSettingsVersion";
            // 
            // lblSQLDatabaseSettingsName
            // 
            resources.ApplyResources(this.lblSQLDatabaseSettingsName, "lblSQLDatabaseSettingsName");
            this.lblSQLDatabaseSettingsName.Name = "lblSQLDatabaseSettingsName";
            // 
            // groupBoxSQLDatabaseStructure
            // 
            this.groupBoxSQLDatabaseStructure.Controls.Add(this.btnContinue);
            this.groupBoxSQLDatabaseStructure.Controls.Add(this.lblDatabaseSettingsMessage);
            this.groupBoxSQLDatabaseStructure.Controls.Add(this.tBDatabaseSettingsSchemaResult);
            resources.ApplyResources(this.groupBoxSQLDatabaseStructure, "groupBoxSQLDatabaseStructure");
            this.groupBoxSQLDatabaseStructure.Name = "groupBoxSQLDatabaseStructure";
            this.groupBoxSQLDatabaseStructure.TabStop = false;
            // 
            // btnContinue
            // 
            resources.ApplyResources(this.btnContinue, "btnContinue");
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // lblDatabaseSettingsMessage
            // 
            resources.ApplyResources(this.lblDatabaseSettingsMessage, "lblDatabaseSettingsMessage");
            this.lblDatabaseSettingsMessage.Name = "lblDatabaseSettingsMessage";
            // 
            // tBDatabaseSettingsSchemaResult
            // 
            this.tBDatabaseSettingsSchemaResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.tBDatabaseSettingsSchemaResult, "tBDatabaseSettingsSchemaResult");
            this.tBDatabaseSettingsSchemaResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.tBDatabaseSettingsSchemaResult.Name = "tBDatabaseSettingsSchemaResult";
            // 
            // bWDatabasesDetection
            // 
            this.bWDatabasesDetection.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDetectDatabases_DoWork);
            this.bWDatabasesDetection.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDetectDatabases_RunWorkerCompleted);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // bWDatabaseCreation
            // 
            this.bWDatabaseCreation.WorkerSupportsCancellation = true;
            this.bWDatabaseCreation.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWDatabaseCreation_DoWork);
            this.bWDatabaseCreation.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWDatabaseCreation_RunWorkerCompleted);
            // 
            // bWDatabaseBackup
            // 
            this.bWDatabaseBackup.WorkerSupportsCancellation = true;
            this.bWDatabaseBackup.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWDatabaseBackup_DoWork);
            this.bWDatabaseBackup.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWDatabaseBackup_RunWorkerCompleted);
            // 
            // bWDatabaseRestore
            // 
            this.bWDatabaseRestore.WorkerSupportsCancellation = true;
            this.bWDatabaseRestore.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWDatabaseRestore_DoWork);
            this.bWDatabaseRestore.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWDatabaseRestore_RunWorkerCompleted);
            // 
            // bWDatabaseUpdate
            // 
            this.bWDatabaseUpdate.WorkerReportsProgress = true;
            this.bWDatabaseUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWDatabaseUpdate_DoWork);
            this.bWDatabaseUpdate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bWDatabaseUpdate_ProgressChanged);
            this.bWDatabaseUpdate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWDatabaseUpdate_RunWorkerCompleted);
            // 
            // FrmDatabaseSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlDatabase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDatabaseSettings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDatabaseSettings_FormClosed);
            this.groupBoxSQLServerConnection.ResumeLayout(false);
            this.groupBoxSQLServerConnection.PerformLayout();
            this.groupBoxSQLSettings.ResumeLayout(false);
            this.groupBoxSQLSettings.PerformLayout();
            this.groupBoxDatabaseManagement.ResumeLayout(false);
            this.groupBoxDatabaseManagement.PerformLayout();
            this.groupBoxSaveSettings.ResumeLayout(false);
            this.groupBoxSaveSettings.PerformLayout();
            this.tabControlDatabase.ResumeLayout(false);
            this.tabPageSQLServerConnection.ResumeLayout(false);
            this.tableLayoutPanelServerSettings.ResumeLayout(false);
            this.tabPageSQLServerSettings.ResumeLayout(false);
            this.tableLayoutPanelSQLSettings.ResumeLayout(false);
            this.tabPageSqlDatabaseSettings.ResumeLayout(false);
            this.tableLayoutPanelDatabaseSettings.ResumeLayout(false);
            this.groupBoxSQLDatabaseSettings.ResumeLayout(false);
            this.groupBoxSQLDatabaseSettings.PerformLayout();
            this.groupBoxSQLDatabaseStructure.ResumeLayout(false);
            this.groupBoxSQLDatabaseStructure.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSQLServerConnection;
        private System.Windows.Forms.Label lblFindDBInfo;
        private System.Windows.Forms.ComboBox cbServerName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.GroupBox groupBoxSQLSettings;
        private System.Windows.Forms.Button btnSQLServerChangeSettings;
        private System.Windows.Forms.Label lblSQLServerSettings;
        private System.Windows.Forms.GroupBox groupBoxDatabaseManagement;
        private System.Windows.Forms.Button btnCreateNewDatabase;
        private System.Windows.Forms.Label lblDatabases;
        private System.Windows.Forms.ListView listViewDatabases;
        private System.Windows.Forms.Button btnSetAsDefault;
        private System.Windows.Forms.GroupBox groupBoxSaveSettings;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tabControlDatabase;
        private System.Windows.Forms.TabPage tabPageSQLServerConnection;
        private System.Windows.Forms.TabPage tabPageSQLServerSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSQLSettings;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label lblResultMessage;
        private System.Windows.Forms.ColumnHeader columnHeaderVersion;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.ComponentModel.BackgroundWorker bWDatabasesDetection;
        private System.Windows.Forms.Label lblDetectDatabasesInProgress;
        private System.Windows.Forms.ColumnHeader columnHeaderBranchCode;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabPage tabPageSqlDatabaseSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDatabaseSettings;
        private System.Windows.Forms.GroupBox groupBoxSQLDatabaseSettings;
        private System.Windows.Forms.Label lblSQLDatabaseSettingsVersion;
        private System.Windows.Forms.Label lblSQLDatabaseSettingsName;
        private System.Windows.Forms.Button btnSQLDatabaseSettingsUpgrade;
        private System.Windows.Forms.Button btnSQLDatabaseSettingsChangeName;
        private System.Windows.Forms.Button btnSQLServerSettingsShowDetails;
        private System.Windows.Forms.GroupBox groupBoxSQLDatabaseStructure;
        private System.Windows.Forms.Label lblDatabaseSettingsMessage;
        private System.ComponentModel.BackgroundWorker bWDatabaseCreation;
        private System.ComponentModel.BackgroundWorker bWDatabaseBackup;
        private System.ComponentModel.BackgroundWorker bWDatabaseRestore;
        private System.ComponentModel.BackgroundWorker bWDatabaseUpdate;
        private System.Windows.Forms.RichTextBox tBDatabaseSettingsSchemaResult;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelServerSettings;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDatabaseConnection;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Label lblHelpServerName;
        private System.Windows.Forms.Label lblInstallInstructionSQLServer;
        private System.Windows.Forms.Button btnGetServersList;
        private System.Windows.Forms.Button btnDefault;
    }
}