
using System.Windows.Forms;
using Octopus.GUI.UserControl;
using Octopus.Enums;
using Octopus.Services;
using Octopus.CoreDomain;

namespace Octopus.GUI
{
    partial class LotrasmicMainWindowForm
    {
        private System.Windows.Forms.ToolStripMenuItem mnuClients;
        private System.Windows.Forms.ToolStripMenuItem mnuContracts;
        private System.Windows.Forms.ToolStripMenuItem mnuAccounting;
        private System.Windows.Forms.ToolStripMenuItem mnuNewClient;
        private System.Windows.Forms.ToolStripMenuItem mnuSearchClient;
        private System.Windows.Forms.ToolStripMenuItem mnuSearchContract;
        private System.Windows.Forms.ToolStripMenuItem mnuChartOfAccounts;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripMenuItem mnuNewPerson;
        private System.Windows.Forms.ToolStripMenuItem mnuNewGroup;
        private System.Windows.Forms.ToolStripMenuItem mnuPackages;
        private System.Windows.Forms.ImageList imageListAlert;
        private System.Windows.Forms.ToolStripMenuItem mnuConfiguration;
        private System.Windows.Forms.ToolStripMenuItem mnuDomainOfApplication;
        private System.Windows.Forms.ToolStripMenuItem menuItemExportTransaction;
        private System.Windows.Forms.ToolStripMenuItem menuItemExchangeRate;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem menuItemSetting;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemAboutOctopus;
        private System.Windows.Forms.ToolStripMenuItem menuItemApplicationDate;
        private System.Windows.Forms.ToolStripMenuItem menuItemAdvancedSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuDatamanagement;
        private System.Windows.Forms.ToolStripMenuItem menuItemDatabaseControlPanel;
        private System.Windows.Forms.ToolStripMenuItem menuItemDatabaseMaintenance;
        private ToolStripSeparator toolStripSeparatorConfig1;
        private ToolStripSeparator toolStripSeparatorConfig2;
        private ToolStripSeparator toolStripSeparatorConfig3;
        private ToolStrip mainStripToolBar;
        private ToolStripButton toolBarButtonSearchPerson;
        private ToolStripButton toolBarButtonSearchContract;
        private ToolStripSplitButton toolBarButNew;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolBarButtonPerson;
        private ToolStripMenuItem toolBarButtonNewGroup;
        private ToolStripButton toolBarButtonReports;
        private StatusStrip mainStatusBar;
        private CollapsibleSplitter splitter6;
        private ToolStripLabel toolBarLblVersion;
        private ToolStripStatusLabel mainStatusBarLblUserName;
        private ToolStripStatusLabel mainStatusBarLblDate;
        private ToolStripStatusLabel mainStatusBarLblUpdateVersion;
        private ToolStripLabel toolStripLabel1;
        private ToolStripStatusLabel toolStripStatusLblBranchCode;
        private ToolStripMenuItem toolStripMenuItemAccountView;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuItemLocations;
        private ToolStripMenuItem toolStripMenuItemFundingLines;
        private ToolStripMenuItem languagesToolStripMenuItem;
        private ToolStripMenuItem frenchToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;
        private ToolStripMenuItem russianToolStripMenuItem;
        private ToolStripMenuItem kyrgyzToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemInstallmentTypes;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem octopusForumToolStripMenuItem;


        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LotrasmicMainWindowForm));
            this.mnuClients = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewClient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewVillage = new System.Windows.Forms.ToolStripMenuItem();
            this.newCorporateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearchClient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContracts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearchContract = new System.Windows.Forms.ToolStripMenuItem();
            this.reasignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAccounting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartOfAccounts = new System.Windows.Forms.ToolStripMenuItem();
            this.accountingRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trialBalanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAccountView = new System.Windows.Forms.ToolStripMenuItem();
            this.manualEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExportTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewclosure = new System.Windows.Forms.ToolStripMenuItem();
            this.fiscalYearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.rolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tellersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.branchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kyrgyzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spanishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portugueseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorConfig1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPackages = new System.Windows.Forms.ToolStripMenuItem();
            this.savingProductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCollateralProducts = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemFundingLines = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorConfig2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDomainOfApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInstallmentTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.miContractCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorConfig3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExchangeRate = new System.Windows.Forms.ToolStripMenuItem();
            this.currenciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemApplicationDate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAdvancedSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomizableFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExtensionManager = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatamanagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDatabaseControlPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDatabaseMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAboutOctopus = new System.Windows.Forms.ToolStripMenuItem();
            this.octopusForumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questionnaireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wIKIHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListAlert = new System.Windows.Forms.ImageList(this.components);
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mView = new System.Windows.Forms.ToolStripMenuItem();
            this.miAuditTrail = new System.Windows.Forms.ToolStripMenuItem();
            this.miReports = new System.Windows.Forms.ToolStripMenuItem();
            this.tellerManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTellerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tellerOperationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.mainStripToolBar = new System.Windows.Forms.ToolStrip();
            this.toolBarButNew = new System.Windows.Forms.ToolStripSplitButton();
            this.toolBarButtonPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarButtonNewGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tbbtnNewVillage = new System.Windows.Forms.ToolStripMenuItem();
            this.corporateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBarButtonSearchPerson = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonSearchContract = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonReports = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonSearchProject = new System.Windows.Forms.ToolStripButton();
            this.toolBarLblVersion = new System.Windows.Forms.ToolStripLabel();
            this.mainStatusBar = new System.Windows.Forms.StatusStrip();
            this.mainStatusBarLblUpdateVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusBarLblUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusBarLblDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblBranchCode = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusBarLblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelTeller = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblDB = new System.Windows.Forms.ToolStripStatusLabel();
            this.bwAlerts = new System.ComponentModel.BackgroundWorker();
            this.nIUpdateAvailable = new System.Windows.Forms.NotifyIcon(this.components);
            this.openCustomizableFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.colAlerts_Address = new BrightIdeasSoftware.OLVColumn();
            this.colAlerts_City = new BrightIdeasSoftware.OLVColumn();
            this.colAlerts_Phone = new BrightIdeasSoftware.OLVColumn();
            this.splitter6 = new Octopus.GUI.UserControl.CollapsibleSplitter();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.olvAlerts = new BrightIdeasSoftware.ObjectListView();
            this.colAlerts_ContractCode = new BrightIdeasSoftware.OLVColumn();
            this.colAlerts_Status = new BrightIdeasSoftware.OLVColumn();
            this.colAlerts_Client = new BrightIdeasSoftware.OLVColumn();
            this.colAlerts_LoanOfficer = new BrightIdeasSoftware.OLVColumn();
            this.colAlerts_Date = new BrightIdeasSoftware.OLVColumn();
            this.colAlerts_Amount = new BrightIdeasSoftware.OLVColumn();
            this.colAlerts_BranchName = new BrightIdeasSoftware.OLVColumn();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabFilter = new System.Windows.Forms.TableLayoutPanel();
            this.chkPostponedLoans = new System.Windows.Forms.CheckBox();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.chkLateLoans = new System.Windows.Forms.CheckBox();
            this.chkPendingLoans = new System.Windows.Forms.CheckBox();
            this.chkPendingSavings = new System.Windows.Forms.CheckBox();
            this.chkOverdraftSavings = new System.Windows.Forms.CheckBox();
            this.chkValidatedLoan = new System.Windows.Forms.CheckBox();
            this.alertBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bwUserInformation = new System.ComponentModel.BackgroundWorker();
            this.mainMenu.SuspendLayout();
            this.mainStripToolBar.SuspendLayout();
            this.mainStatusBar.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvAlerts)).BeginInit();
            this.tabFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuClients
            // 
            this.mnuClients.AccessibleDescription = null;
            this.mnuClients.AccessibleName = null;
            resources.ApplyResources(this.mnuClients, "mnuClients");
            this.mnuClients.BackgroundImage = null;
            this.mnuClients.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewClient,
            this.mnuSearchClient});
            this.mnuClients.Name = "mnuClients";
            this.mnuClients.ShortcutKeyDisplayString = null;
            // 
            // mnuNewClient
            // 
            this.mnuNewClient.AccessibleDescription = null;
            this.mnuNewClient.AccessibleName = null;
            resources.ApplyResources(this.mnuNewClient, "mnuNewClient");
            this.mnuNewClient.BackgroundImage = null;
            this.mnuNewClient.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewPerson,
            this.mnuNewGroup,
            this.mnuNewVillage,
            this.newCorporateToolStripMenuItem});
            this.mnuNewClient.Name = "mnuNewClient";
            this.mnuNewClient.ShortcutKeyDisplayString = null;
            // 
            // mnuNewPerson
            // 
            this.mnuNewPerson.AccessibleDescription = null;
            this.mnuNewPerson.AccessibleName = null;
            resources.ApplyResources(this.mnuNewPerson, "mnuNewPerson");
            this.mnuNewPerson.BackgroundImage = null;
            this.mnuNewPerson.Name = "mnuNewPerson";
            this.mnuNewPerson.ShortcutKeyDisplayString = null;
            this.mnuNewPerson.Click += new System.EventHandler(this.mnuNewPerson_Click);
            // 
            // mnuNewGroup
            // 
            this.mnuNewGroup.AccessibleDescription = null;
            this.mnuNewGroup.AccessibleName = null;
            resources.ApplyResources(this.mnuNewGroup, "mnuNewGroup");
            this.mnuNewGroup.BackgroundImage = null;
            this.mnuNewGroup.Name = "mnuNewGroup";
            this.mnuNewGroup.ShortcutKeyDisplayString = null;
            this.mnuNewGroup.Click += new System.EventHandler(this.mnuNewGroup_Click);
            // 
            // mnuNewVillage
            // 
            this.mnuNewVillage.AccessibleDescription = null;
            this.mnuNewVillage.AccessibleName = null;
            resources.ApplyResources(this.mnuNewVillage, "mnuNewVillage");
            this.mnuNewVillage.BackgroundImage = null;
            this.mnuNewVillage.Name = "mnuNewVillage";
            this.mnuNewVillage.ShortcutKeyDisplayString = null;
            this.mnuNewVillage.Click += new System.EventHandler(this.mnuNewVillage_Click);
            // 
            // newCorporateToolStripMenuItem
            // 
            this.newCorporateToolStripMenuItem.AccessibleDescription = null;
            this.newCorporateToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.newCorporateToolStripMenuItem, "newCorporateToolStripMenuItem");
            this.newCorporateToolStripMenuItem.BackgroundImage = null;
            this.newCorporateToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.theme1_1_import;
            this.newCorporateToolStripMenuItem.Name = "newCorporateToolStripMenuItem";
            this.newCorporateToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.newCorporateToolStripMenuItem.Click += new System.EventHandler(this.newCorporateToolStripMenuItem_Click);
            // 
            // mnuSearchClient
            // 
            this.mnuSearchClient.AccessibleDescription = null;
            this.mnuSearchClient.AccessibleName = null;
            resources.ApplyResources(this.mnuSearchClient, "mnuSearchClient");
            this.mnuSearchClient.BackgroundImage = null;
            this.mnuSearchClient.Name = "mnuSearchClient";
            this.mnuSearchClient.ShortcutKeyDisplayString = null;
            this.mnuSearchClient.Click += new System.EventHandler(this.mnuSearchClient_Click);
            // 
            // mnuContracts
            // 
            this.mnuContracts.AccessibleDescription = null;
            this.mnuContracts.AccessibleName = null;
            resources.ApplyResources(this.mnuContracts, "mnuContracts");
            this.mnuContracts.BackgroundImage = null;
            this.mnuContracts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSearchContract,
            this.reasignToolStripMenuItem});
            this.mnuContracts.Name = "mnuContracts";
            this.mnuContracts.ShortcutKeyDisplayString = null;
            // 
            // mnuSearchContract
            // 
            this.mnuSearchContract.AccessibleDescription = null;
            this.mnuSearchContract.AccessibleName = null;
            resources.ApplyResources(this.mnuSearchContract, "mnuSearchContract");
            this.mnuSearchContract.BackgroundImage = null;
            this.mnuSearchContract.Name = "mnuSearchContract";
            this.mnuSearchContract.ShortcutKeyDisplayString = null;
            this.mnuSearchContract.Click += new System.EventHandler(this.mnuSearchContract_Click);
            // 
            // reasignToolStripMenuItem
            // 
            this.reasignToolStripMenuItem.AccessibleDescription = null;
            this.reasignToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.reasignToolStripMenuItem, "reasignToolStripMenuItem");
            this.reasignToolStripMenuItem.BackgroundImage = null;
            this.reasignToolStripMenuItem.Name = "reasignToolStripMenuItem";
            this.reasignToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.reasignToolStripMenuItem.Click += new System.EventHandler(this.reasignToolStripMenuItem_Click);
            // 
            // mnuAccounting
            // 
            this.mnuAccounting.AccessibleDescription = null;
            this.mnuAccounting.AccessibleName = null;
            resources.ApplyResources(this.mnuAccounting, "mnuAccounting");
            this.mnuAccounting.BackgroundImage = null;
            this.mnuAccounting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChartOfAccounts,
            this.accountingRulesToolStripMenuItem,
            this.trialBalanceToolStripMenuItem,
            this.toolStripMenuItemAccountView,
            this.manualEntriesToolStripMenuItem,
            this.standardToolStripMenuItem,
            this.toolStripSeparator2,
            this.menuItemExportTransaction,
            this.mnuNewclosure,
            this.fiscalYearToolStripMenuItem});
            this.mnuAccounting.Name = "mnuAccounting";
            this.mnuAccounting.ShortcutKeyDisplayString = null;
            // 
            // mnuChartOfAccounts
            // 
            this.mnuChartOfAccounts.AccessibleDescription = null;
            this.mnuChartOfAccounts.AccessibleName = null;
            resources.ApplyResources(this.mnuChartOfAccounts, "mnuChartOfAccounts");
            this.mnuChartOfAccounts.BackgroundImage = null;
            this.mnuChartOfAccounts.Name = "mnuChartOfAccounts";
            this.mnuChartOfAccounts.ShortcutKeyDisplayString = null;
            // 
            // accountingRulesToolStripMenuItem
            // 
            this.accountingRulesToolStripMenuItem.AccessibleDescription = null;
            this.accountingRulesToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.accountingRulesToolStripMenuItem, "accountingRulesToolStripMenuItem");
            this.accountingRulesToolStripMenuItem.BackgroundImage = null;
            this.accountingRulesToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.theme1_1_doc;
            this.accountingRulesToolStripMenuItem.Name = "accountingRulesToolStripMenuItem";
            this.accountingRulesToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.accountingRulesToolStripMenuItem.Click += new System.EventHandler(this.accountingRulesToolStripMenuItem_Click);
            // 
            // trialBalanceToolStripMenuItem
            // 
            this.trialBalanceToolStripMenuItem.AccessibleDescription = null;
            this.trialBalanceToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.trialBalanceToolStripMenuItem, "trialBalanceToolStripMenuItem");
            this.trialBalanceToolStripMenuItem.BackgroundImage = null;
            this.trialBalanceToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.financial_information_bouton_files_16x16;
            this.trialBalanceToolStripMenuItem.Name = "trialBalanceToolStripMenuItem";
            this.trialBalanceToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.trialBalanceToolStripMenuItem.Click += new System.EventHandler(this.trialBalanceToolStripMenuItem_Click);
            // 
            // toolStripMenuItemAccountView
            // 
            this.toolStripMenuItemAccountView.AccessibleDescription = null;
            this.toolStripMenuItemAccountView.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemAccountView, "toolStripMenuItemAccountView");
            this.toolStripMenuItemAccountView.BackgroundImage = null;
            this.toolStripMenuItemAccountView.Name = "toolStripMenuItemAccountView";
            this.toolStripMenuItemAccountView.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemAccountView.Click += new System.EventHandler(this.toolStripMenuItemAccountView_Click);
            // 
            // manualEntriesToolStripMenuItem
            // 
            this.manualEntriesToolStripMenuItem.AccessibleDescription = null;
            this.manualEntriesToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.manualEntriesToolStripMenuItem, "manualEntriesToolStripMenuItem");
            this.manualEntriesToolStripMenuItem.BackgroundImage = null;
            this.manualEntriesToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.theme1_1_pastille_personne;
            this.manualEntriesToolStripMenuItem.Name = "manualEntriesToolStripMenuItem";
            this.manualEntriesToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.manualEntriesToolStripMenuItem.Click += new System.EventHandler(this.manualEntriesToolStripMenuItem_Click);
            // 
            // standardToolStripMenuItem
            // 
            this.standardToolStripMenuItem.AccessibleDescription = null;
            this.standardToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.standardToolStripMenuItem, "standardToolStripMenuItem");
            this.standardToolStripMenuItem.BackgroundImage = null;
            this.standardToolStripMenuItem.Name = "standardToolStripMenuItem";
            this.standardToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.standardToolStripMenuItem.Click += new System.EventHandler(this.standardToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AccessibleDescription = null;
            this.toolStripSeparator2.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // menuItemExportTransaction
            // 
            this.menuItemExportTransaction.AccessibleDescription = null;
            this.menuItemExportTransaction.AccessibleName = null;
            resources.ApplyResources(this.menuItemExportTransaction, "menuItemExportTransaction");
            this.menuItemExportTransaction.BackgroundImage = null;
            this.menuItemExportTransaction.Name = "menuItemExportTransaction";
            this.menuItemExportTransaction.ShortcutKeyDisplayString = null;
            this.menuItemExportTransaction.Click += new System.EventHandler(this.menuItemExportTransaction_Click);
            // 
            // mnuNewclosure
            // 
            this.mnuNewclosure.AccessibleDescription = null;
            this.mnuNewclosure.AccessibleName = null;
            resources.ApplyResources(this.mnuNewclosure, "mnuNewclosure");
            this.mnuNewclosure.BackgroundImage = null;
            this.mnuNewclosure.Name = "mnuNewclosure";
            this.mnuNewclosure.ShortcutKeyDisplayString = null;
            this.mnuNewclosure.Click += new System.EventHandler(this.newClosureToolStripMenuItem_Click_1);
            // 
            // fiscalYearToolStripMenuItem
            // 
            this.fiscalYearToolStripMenuItem.AccessibleDescription = null;
            this.fiscalYearToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.fiscalYearToolStripMenuItem, "fiscalYearToolStripMenuItem");
            this.fiscalYearToolStripMenuItem.BackgroundImage = null;
            this.fiscalYearToolStripMenuItem.Name = "fiscalYearToolStripMenuItem";
            this.fiscalYearToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.fiscalYearToolStripMenuItem.Click += new System.EventHandler(this.fiscalYearToolStripMenuItem_Click);
            // 
            // mnuConfiguration
            // 
            this.mnuConfiguration.AccessibleDescription = null;
            this.mnuConfiguration.AccessibleName = null;
            resources.ApplyResources(this.mnuConfiguration, "mnuConfiguration");
            this.mnuConfiguration.BackgroundImage = null;
            this.mnuConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rolesToolStripMenuItem,
            this.menuItemAddUser,
            this.tellersToolStripMenuItem,
            this.branchesToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.languagesToolStripMenuItem,
            this.toolStripSeparatorConfig1,
            this.mnuPackages,
            this.savingProductsToolStripMenuItem,
            this.menuItemCollateralProducts,
            this.toolStripSeparator3,
            this.toolStripMenuItemFundingLines,
            this.toolStripSeparatorConfig2,
            this.mnuDomainOfApplication,
            this.menuItemLocations,
            this.toolStripMenuItemInstallmentTypes,
            this.miContractCode,
            this.toolStripSeparatorConfig3,
            this.menuItemExchangeRate,
            this.currenciesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.menuItemApplicationDate,
            this.menuItemSetting,
            this.menuItemAdvancedSettings,
            this.CustomizableFieldsToolStripMenuItem,
            this.toolStripSeparator4,
            this.mnuExtensionManager});
            this.mnuConfiguration.Name = "mnuConfiguration";
            this.mnuConfiguration.ShortcutKeyDisplayString = null;
            // 
            // rolesToolStripMenuItem
            // 
            this.rolesToolStripMenuItem.AccessibleDescription = null;
            this.rolesToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.rolesToolStripMenuItem, "rolesToolStripMenuItem");
            this.rolesToolStripMenuItem.BackgroundImage = null;
            this.rolesToolStripMenuItem.Name = "rolesToolStripMenuItem";
            this.rolesToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.rolesToolStripMenuItem.Click += new System.EventHandler(this.rolesToolStripMenuItem_Click);
            // 
            // menuItemAddUser
            // 
            this.menuItemAddUser.AccessibleDescription = null;
            this.menuItemAddUser.AccessibleName = null;
            resources.ApplyResources(this.menuItemAddUser, "menuItemAddUser");
            this.menuItemAddUser.BackgroundImage = null;
            this.menuItemAddUser.Name = "menuItemAddUser";
            this.menuItemAddUser.ShortcutKeyDisplayString = null;
            this.menuItemAddUser.Click += new System.EventHandler(this.menuItemAddUser_Click);
            // 
            // tellersToolStripMenuItem
            // 
            this.tellersToolStripMenuItem.AccessibleDescription = null;
            this.tellersToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.tellersToolStripMenuItem, "tellersToolStripMenuItem");
            this.tellersToolStripMenuItem.BackgroundImage = null;
            this.tellersToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.theme1_1_pastille_contrat;
            this.tellersToolStripMenuItem.Name = "tellersToolStripMenuItem";
            this.tellersToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.tellersToolStripMenuItem.Click += new System.EventHandler(this.tellersToolStripMenuItem_Click);
            // 
            // branchesToolStripMenuItem
            // 
            this.branchesToolStripMenuItem.AccessibleDescription = null;
            this.branchesToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.branchesToolStripMenuItem, "branchesToolStripMenuItem");
            this.branchesToolStripMenuItem.BackgroundImage = null;
            this.branchesToolStripMenuItem.Name = "branchesToolStripMenuItem";
            this.branchesToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.branchesToolStripMenuItem.Click += new System.EventHandler(this.branchesToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.AccessibleDescription = null;
            this.changePasswordToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.changePasswordToolStripMenuItem, "changePasswordToolStripMenuItem");
            this.changePasswordToolStripMenuItem.BackgroundImage = null;
            this.changePasswordToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.theme1_1_doc;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // languagesToolStripMenuItem
            // 
            this.languagesToolStripMenuItem.AccessibleDescription = null;
            this.languagesToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.languagesToolStripMenuItem, "languagesToolStripMenuItem");
            this.languagesToolStripMenuItem.BackgroundImage = null;
            this.languagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frenchToolStripMenuItem,
            this.englishToolStripMenuItem,
            this.russianToolStripMenuItem,
            this.kyrgyzToolStripMenuItem,
            this.spanishToolStripMenuItem,
            this.portugueseToolStripMenuItem});
            this.languagesToolStripMenuItem.Name = "languagesToolStripMenuItem";
            this.languagesToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.languagesToolStripMenuItem.DropDownOpening += new System.EventHandler(this.languagesToolStripMenuItem_DropDownOpening);
            // 
            // frenchToolStripMenuItem
            // 
            this.frenchToolStripMenuItem.AccessibleDescription = null;
            this.frenchToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.frenchToolStripMenuItem, "frenchToolStripMenuItem");
            this.frenchToolStripMenuItem.BackgroundImage = null;
            this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
            this.frenchToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.frenchToolStripMenuItem.Tag = "fr";
            this.frenchToolStripMenuItem.Click += new System.EventHandler(this.LanguageToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.AccessibleDescription = null;
            this.englishToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.BackgroundImage = null;
            this.englishToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.en_small;
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.englishToolStripMenuItem.Tag = "en-US";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.LanguageToolStripMenuItem_Click);
            // 
            // russianToolStripMenuItem
            // 
            this.russianToolStripMenuItem.AccessibleDescription = null;
            this.russianToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.russianToolStripMenuItem, "russianToolStripMenuItem");
            this.russianToolStripMenuItem.BackgroundImage = null;
            this.russianToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.ru_small;
            this.russianToolStripMenuItem.Name = "russianToolStripMenuItem";
            this.russianToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.russianToolStripMenuItem.Tag = "ru-RU";
            this.russianToolStripMenuItem.Click += new System.EventHandler(this.LanguageToolStripMenuItem_Click);
            // 
            // kyrgyzToolStripMenuItem
            // 
            this.kyrgyzToolStripMenuItem.AccessibleDescription = null;
            this.kyrgyzToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.kyrgyzToolStripMenuItem, "kyrgyzToolStripMenuItem");
            this.kyrgyzToolStripMenuItem.BackgroundImage = null;
            this.kyrgyzToolStripMenuItem.Name = "kyrgyzToolStripMenuItem";
            this.kyrgyzToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.kyrgyzToolStripMenuItem.Tag = "ky-KG";
            this.kyrgyzToolStripMenuItem.Click += new System.EventHandler(this.LanguageToolStripMenuItem_Click);
            // 
            // spanishToolStripMenuItem
            // 
            this.spanishToolStripMenuItem.AccessibleDescription = null;
            this.spanishToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.spanishToolStripMenuItem, "spanishToolStripMenuItem");
            this.spanishToolStripMenuItem.BackgroundImage = null;
            this.spanishToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.Spanish_Flag1;
            this.spanishToolStripMenuItem.Name = "spanishToolStripMenuItem";
            this.spanishToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.spanishToolStripMenuItem.Tag = "es-ES";
            this.spanishToolStripMenuItem.Click += new System.EventHandler(this.LanguageToolStripMenuItem_Click);
            // 
            // portugueseToolStripMenuItem
            // 
            this.portugueseToolStripMenuItem.AccessibleDescription = null;
            this.portugueseToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.portugueseToolStripMenuItem, "portugueseToolStripMenuItem");
            this.portugueseToolStripMenuItem.BackgroundImage = null;
            this.portugueseToolStripMenuItem.Name = "portugueseToolStripMenuItem";
            this.portugueseToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.portugueseToolStripMenuItem.Click += new System.EventHandler(this.LanguageToolStripMenuItem_Click);
            // 
            // toolStripSeparatorConfig1
            // 
            this.toolStripSeparatorConfig1.AccessibleDescription = null;
            this.toolStripSeparatorConfig1.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparatorConfig1, "toolStripSeparatorConfig1");
            this.toolStripSeparatorConfig1.Name = "toolStripSeparatorConfig1";
            // 
            // mnuPackages
            // 
            this.mnuPackages.AccessibleDescription = null;
            this.mnuPackages.AccessibleName = null;
            resources.ApplyResources(this.mnuPackages, "mnuPackages");
            this.mnuPackages.BackgroundImage = null;
            this.mnuPackages.Name = "mnuPackages";
            this.mnuPackages.ShortcutKeyDisplayString = null;
            this.mnuPackages.Click += new System.EventHandler(this.menuItemPackages_Click);
            // 
            // savingProductsToolStripMenuItem
            // 
            this.savingProductsToolStripMenuItem.AccessibleDescription = null;
            this.savingProductsToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.savingProductsToolStripMenuItem, "savingProductsToolStripMenuItem");
            this.savingProductsToolStripMenuItem.BackgroundImage = null;
            this.savingProductsToolStripMenuItem.Name = "savingProductsToolStripMenuItem";
            this.savingProductsToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.savingProductsToolStripMenuItem.Click += new System.EventHandler(this.savingProductsToolStripMenuItem_Click);
            // 
            // menuItemCollateralProducts
            // 
            this.menuItemCollateralProducts.AccessibleDescription = null;
            this.menuItemCollateralProducts.AccessibleName = null;
            resources.ApplyResources(this.menuItemCollateralProducts, "menuItemCollateralProducts");
            this.menuItemCollateralProducts.BackgroundImage = null;
            this.menuItemCollateralProducts.Name = "menuItemCollateralProducts";
            this.menuItemCollateralProducts.ShortcutKeyDisplayString = null;
            this.menuItemCollateralProducts.Click += new System.EventHandler(this.menuItemCollateralProducts_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AccessibleDescription = null;
            this.toolStripSeparator3.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // toolStripMenuItemFundingLines
            // 
            this.toolStripMenuItemFundingLines.AccessibleDescription = null;
            this.toolStripMenuItemFundingLines.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemFundingLines, "toolStripMenuItemFundingLines");
            this.toolStripMenuItemFundingLines.BackgroundImage = null;
            this.toolStripMenuItemFundingLines.Image = global::Octopus.GUI.Properties.Resources.theme1_1_doc;
            this.toolStripMenuItemFundingLines.Name = "toolStripMenuItemFundingLines";
            this.toolStripMenuItemFundingLines.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemFundingLines.Click += new System.EventHandler(this.toolStripMenuItemFundingLines_Click);
            // 
            // toolStripSeparatorConfig2
            // 
            this.toolStripSeparatorConfig2.AccessibleDescription = null;
            this.toolStripSeparatorConfig2.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparatorConfig2, "toolStripSeparatorConfig2");
            this.toolStripSeparatorConfig2.Name = "toolStripSeparatorConfig2";
            // 
            // mnuDomainOfApplication
            // 
            this.mnuDomainOfApplication.AccessibleDescription = null;
            this.mnuDomainOfApplication.AccessibleName = null;
            resources.ApplyResources(this.mnuDomainOfApplication, "mnuDomainOfApplication");
            this.mnuDomainOfApplication.BackgroundImage = null;
            this.mnuDomainOfApplication.Name = "mnuDomainOfApplication";
            this.mnuDomainOfApplication.ShortcutKeyDisplayString = null;
            this.mnuDomainOfApplication.Click += new System.EventHandler(this.mnuDomainOfApplication_Click);
            // 
            // menuItemLocations
            // 
            this.menuItemLocations.AccessibleDescription = null;
            this.menuItemLocations.AccessibleName = null;
            resources.ApplyResources(this.menuItemLocations, "menuItemLocations");
            this.menuItemLocations.BackgroundImage = null;
            this.menuItemLocations.Name = "menuItemLocations";
            this.menuItemLocations.ShortcutKeyDisplayString = null;
            this.menuItemLocations.Click += new System.EventHandler(this.menuItemLocations_Click);
            // 
            // toolStripMenuItemInstallmentTypes
            // 
            this.toolStripMenuItemInstallmentTypes.AccessibleDescription = null;
            this.toolStripMenuItemInstallmentTypes.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemInstallmentTypes, "toolStripMenuItemInstallmentTypes");
            this.toolStripMenuItemInstallmentTypes.BackgroundImage = null;
            this.toolStripMenuItemInstallmentTypes.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_validity;
            this.toolStripMenuItemInstallmentTypes.Name = "toolStripMenuItemInstallmentTypes";
            this.toolStripMenuItemInstallmentTypes.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemInstallmentTypes.Click += new System.EventHandler(this.toolStripMenuItemInstallmentTypes_Click);
            // 
            // miContractCode
            // 
            this.miContractCode.AccessibleDescription = null;
            this.miContractCode.AccessibleName = null;
            resources.ApplyResources(this.miContractCode, "miContractCode");
            this.miContractCode.BackgroundImage = null;
            this.miContractCode.Name = "miContractCode";
            this.miContractCode.ShortcutKeyDisplayString = null;
            this.miContractCode.Click += new System.EventHandler(this.miContractCode_Click);
            // 
            // toolStripSeparatorConfig3
            // 
            this.toolStripSeparatorConfig3.AccessibleDescription = null;
            this.toolStripSeparatorConfig3.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparatorConfig3, "toolStripSeparatorConfig3");
            this.toolStripSeparatorConfig3.Name = "toolStripSeparatorConfig3";
            // 
            // menuItemExchangeRate
            // 
            this.menuItemExchangeRate.AccessibleDescription = null;
            this.menuItemExchangeRate.AccessibleName = null;
            resources.ApplyResources(this.menuItemExchangeRate, "menuItemExchangeRate");
            this.menuItemExchangeRate.BackgroundImage = null;
            this.menuItemExchangeRate.Name = "menuItemExchangeRate";
            this.menuItemExchangeRate.ShortcutKeyDisplayString = null;
            this.menuItemExchangeRate.Click += new System.EventHandler(this.menuItemExchangeRate_Click);
            // 
            // currenciesToolStripMenuItem
            // 
            this.currenciesToolStripMenuItem.AccessibleDescription = null;
            this.currenciesToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.currenciesToolStripMenuItem, "currenciesToolStripMenuItem");
            this.currenciesToolStripMenuItem.BackgroundImage = null;
            this.currenciesToolStripMenuItem.Name = "currenciesToolStripMenuItem";
            this.currenciesToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.currenciesToolStripMenuItem.Click += new System.EventHandler(this.currenciesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.AccessibleDescription = null;
            this.toolStripMenuItem1.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // menuItemApplicationDate
            // 
            this.menuItemApplicationDate.AccessibleDescription = null;
            this.menuItemApplicationDate.AccessibleName = null;
            resources.ApplyResources(this.menuItemApplicationDate, "menuItemApplicationDate");
            this.menuItemApplicationDate.BackgroundImage = null;
            this.menuItemApplicationDate.Name = "menuItemApplicationDate";
            this.menuItemApplicationDate.ShortcutKeyDisplayString = null;
            this.menuItemApplicationDate.Click += new System.EventHandler(this.OnChangeApplicationDateClick);
            // 
            // menuItemSetting
            // 
            this.menuItemSetting.AccessibleDescription = null;
            this.menuItemSetting.AccessibleName = null;
            resources.ApplyResources(this.menuItemSetting, "menuItemSetting");
            this.menuItemSetting.BackgroundImage = null;
            this.menuItemSetting.Name = "menuItemSetting";
            this.menuItemSetting.ShortcutKeyDisplayString = null;
            this.menuItemSetting.Click += new System.EventHandler(this.menuItemSetting_Click);
            // 
            // menuItemAdvancedSettings
            // 
            this.menuItemAdvancedSettings.AccessibleDescription = null;
            this.menuItemAdvancedSettings.AccessibleName = null;
            resources.ApplyResources(this.menuItemAdvancedSettings, "menuItemAdvancedSettings");
            this.menuItemAdvancedSettings.BackgroundImage = null;
            this.menuItemAdvancedSettings.Name = "menuItemAdvancedSettings";
            this.menuItemAdvancedSettings.ShortcutKeyDisplayString = null;
            this.menuItemAdvancedSettings.Click += new System.EventHandler(this.menuItemAdvancedSettings_Click);
            // 
            // CustomizableFieldsToolStripMenuItem
            // 
            this.CustomizableFieldsToolStripMenuItem.AccessibleDescription = null;
            this.CustomizableFieldsToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.CustomizableFieldsToolStripMenuItem, "CustomizableFieldsToolStripMenuItem");
            this.CustomizableFieldsToolStripMenuItem.BackgroundImage = null;
            this.CustomizableFieldsToolStripMenuItem.Name = "CustomizableFieldsToolStripMenuItem";
            this.CustomizableFieldsToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.CustomizableFieldsToolStripMenuItem.Click += new System.EventHandler(this.CustomizableFieldsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AccessibleDescription = null;
            this.toolStripSeparator4.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // mnuExtensionManager
            // 
            this.mnuExtensionManager.AccessibleDescription = null;
            this.mnuExtensionManager.AccessibleName = null;
            resources.ApplyResources(this.mnuExtensionManager, "mnuExtensionManager");
            this.mnuExtensionManager.BackgroundImage = null;
            this.mnuExtensionManager.Image = global::Octopus.GUI.Properties.Resources.closure;
            this.mnuExtensionManager.Name = "mnuExtensionManager";
            this.mnuExtensionManager.ShortcutKeyDisplayString = null;
            this.mnuExtensionManager.Click += new System.EventHandler(this.MnuExtensionManagerClick);
            // 
            // mnuDatamanagement
            // 
            this.mnuDatamanagement.AccessibleDescription = null;
            this.mnuDatamanagement.AccessibleName = null;
            resources.ApplyResources(this.mnuDatamanagement, "mnuDatamanagement");
            this.mnuDatamanagement.BackgroundImage = null;
            this.mnuDatamanagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDatabaseControlPanel,
            this.menuItemDatabaseMaintenance});
            this.mnuDatamanagement.Name = "mnuDatamanagement";
            this.mnuDatamanagement.ShortcutKeyDisplayString = null;
            // 
            // menuItemDatabaseControlPanel
            // 
            this.menuItemDatabaseControlPanel.AccessibleDescription = null;
            this.menuItemDatabaseControlPanel.AccessibleName = null;
            resources.ApplyResources(this.menuItemDatabaseControlPanel, "menuItemDatabaseControlPanel");
            this.menuItemDatabaseControlPanel.BackgroundImage = null;
            this.menuItemDatabaseControlPanel.Image = global::Octopus.GUI.Properties.Resources.thame1_1_database;
            this.menuItemDatabaseControlPanel.Name = "menuItemDatabaseControlPanel";
            this.menuItemDatabaseControlPanel.ShortcutKeyDisplayString = null;
            this.menuItemDatabaseControlPanel.Click += new System.EventHandler(this.menuItemBackupData_Click);
            // 
            // menuItemDatabaseMaintenance
            // 
            this.menuItemDatabaseMaintenance.AccessibleDescription = null;
            this.menuItemDatabaseMaintenance.AccessibleName = null;
            resources.ApplyResources(this.menuItemDatabaseMaintenance, "menuItemDatabaseMaintenance");
            this.menuItemDatabaseMaintenance.BackgroundImage = null;
            this.menuItemDatabaseMaintenance.Name = "menuItemDatabaseMaintenance";
            this.menuItemDatabaseMaintenance.ShortcutKeyDisplayString = null;
            this.menuItemDatabaseMaintenance.Click += new System.EventHandler(this.menuItemDatabaseMaintenance_Click);
            // 
            // mnuWindow
            // 
            this.mnuWindow.AccessibleDescription = null;
            this.mnuWindow.AccessibleName = null;
            resources.ApplyResources(this.mnuWindow, "mnuWindow");
            this.mnuWindow.BackgroundImage = null;
            this.mnuWindow.Name = "mnuWindow";
            this.mnuWindow.ShortcutKeyDisplayString = null;
            // 
            // mnuHelp
            // 
            this.mnuHelp.AccessibleDescription = null;
            this.mnuHelp.AccessibleName = null;
            resources.ApplyResources(this.mnuHelp, "mnuHelp");
            this.mnuHelp.BackgroundImage = null;
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAboutOctopus,
            this.octopusForumToolStripMenuItem,
            this.questionnaireToolStripMenuItem,
            this.userGuideToolStripMenuItem,
            this.wIKIHelpToolStripMenuItem});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.ShortcutKeyDisplayString = null;
            // 
            // menuItemAboutOctopus
            // 
            this.menuItemAboutOctopus.AccessibleDescription = null;
            this.menuItemAboutOctopus.AccessibleName = null;
            resources.ApplyResources(this.menuItemAboutOctopus, "menuItemAboutOctopus");
            this.menuItemAboutOctopus.BackgroundImage = null;
            this.menuItemAboutOctopus.Name = "menuItemAboutOctopus";
            this.menuItemAboutOctopus.ShortcutKeyDisplayString = null;
            this.menuItemAboutOctopus.Click += new System.EventHandler(this.menuItemAboutOctopus_Click);
            // 
            // octopusForumToolStripMenuItem
            // 
            this.octopusForumToolStripMenuItem.AccessibleDescription = null;
            this.octopusForumToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.octopusForumToolStripMenuItem, "octopusForumToolStripMenuItem");
            this.octopusForumToolStripMenuItem.BackgroundImage = null;
            this.octopusForumToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.theme1_1_pastille_personne;
            this.octopusForumToolStripMenuItem.Name = "octopusForumToolStripMenuItem";
            this.octopusForumToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.octopusForumToolStripMenuItem.Click += new System.EventHandler(this.octopusForumToolStripMenuItem_Click);
            // 
            // questionnaireToolStripMenuItem
            // 
            this.questionnaireToolStripMenuItem.AccessibleDescription = null;
            this.questionnaireToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.questionnaireToolStripMenuItem, "questionnaireToolStripMenuItem");
            this.questionnaireToolStripMenuItem.BackgroundImage = null;
            this.questionnaireToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.theme1_1_doc;
            this.questionnaireToolStripMenuItem.Name = "questionnaireToolStripMenuItem";
            this.questionnaireToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.questionnaireToolStripMenuItem.Click += new System.EventHandler(this.questionnaireToolStripMenuItem_Click);
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.AccessibleDescription = null;
            this.userGuideToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.userGuideToolStripMenuItem, "userGuideToolStripMenuItem");
            this.userGuideToolStripMenuItem.BackgroundImage = null;
            this.userGuideToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.theme1_1_doc;
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.userGuideToolStripMenuItem.Click += new System.EventHandler(this.userGuideToolStripMenuItem_Click);
            // 
            // wIKIHelpToolStripMenuItem
            // 
            this.wIKIHelpToolStripMenuItem.AccessibleDescription = null;
            this.wIKIHelpToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.wIKIHelpToolStripMenuItem, "wIKIHelpToolStripMenuItem");
            this.wIKIHelpToolStripMenuItem.BackgroundImage = null;
            this.wIKIHelpToolStripMenuItem.Image = global::Octopus.GUI.Properties.Resources.languages;
            this.wIKIHelpToolStripMenuItem.Name = "wIKIHelpToolStripMenuItem";
            this.wIKIHelpToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.wIKIHelpToolStripMenuItem.Click += new System.EventHandler(this.wIKIHelpToolStripMenuItem_Click);
            // 
            // imageListAlert
            // 
            this.imageListAlert.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListAlert.ImageStream")));
            this.imageListAlert.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListAlert.Images.SetKeyName(0, "");
            this.imageListAlert.Images.SetKeyName(1, "");
            this.imageListAlert.Images.SetKeyName(2, "");
            this.imageListAlert.Images.SetKeyName(3, "");
            this.imageListAlert.Images.SetKeyName(4, "");
            // 
            // mainMenu
            // 
            this.mainMenu.AccessibleDescription = null;
            this.mainMenu.AccessibleName = null;
            resources.ApplyResources(this.mainMenu, "mainMenu");
            this.mainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.mainMenu.BackgroundImage = null;
            this.mainMenu.Font = null;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClients,
            this.mnuContracts,
            this.mnuAccounting,
            this.mnuConfiguration,
            this.mView,
            this.mnuDatamanagement,
            this.tellerManagementToolStripMenuItem,
            this.mnuWindow,
            this.mnuHelp});
            this.mainMenu.MdiWindowListItem = this.mnuWindow;
            this.mainMenu.Name = "mainMenu";
            // 
            // mView
            // 
            this.mView.AccessibleDescription = null;
            this.mView.AccessibleName = null;
            resources.ApplyResources(this.mView, "mView");
            this.mView.BackgroundImage = null;
            this.mView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAuditTrail,
            this.miReports});
            this.mView.Name = "mView";
            this.mView.ShortcutKeyDisplayString = null;
            // 
            // miAuditTrail
            // 
            this.miAuditTrail.AccessibleDescription = null;
            this.miAuditTrail.AccessibleName = null;
            resources.ApplyResources(this.miAuditTrail, "miAuditTrail");
            this.miAuditTrail.BackgroundImage = null;
            this.miAuditTrail.Name = "miAuditTrail";
            this.miAuditTrail.ShortcutKeyDisplayString = null;
            this.miAuditTrail.Click += new System.EventHandler(this.eventsToolStripMenuItem_Click);
            // 
            // miReports
            // 
            this.miReports.AccessibleDescription = null;
            this.miReports.AccessibleName = null;
            resources.ApplyResources(this.miReports, "miReports");
            this.miReports.BackgroundImage = null;
            this.miReports.Name = "miReports";
            this.miReports.ShortcutKeyDisplayString = null;
            this.miReports.Click += new System.EventHandler(this.miReports_Click);
            // 
            // tellerManagementToolStripMenuItem
            // 
            this.tellerManagementToolStripMenuItem.AccessibleDescription = null;
            this.tellerManagementToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.tellerManagementToolStripMenuItem, "tellerManagementToolStripMenuItem");
            this.tellerManagementToolStripMenuItem.BackgroundImage = null;
            this.tellerManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeTellerToolStripMenuItem,
            this.tellerOperationsToolStripMenuItem});
            this.tellerManagementToolStripMenuItem.Name = "tellerManagementToolStripMenuItem";
            this.tellerManagementToolStripMenuItem.ShortcutKeyDisplayString = null;
            // 
            // closeTellerToolStripMenuItem
            // 
            this.closeTellerToolStripMenuItem.AccessibleDescription = null;
            this.closeTellerToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.closeTellerToolStripMenuItem, "closeTellerToolStripMenuItem");
            this.closeTellerToolStripMenuItem.BackgroundImage = null;
            this.closeTellerToolStripMenuItem.Name = "closeTellerToolStripMenuItem";
            this.closeTellerToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.closeTellerToolStripMenuItem.Click += new System.EventHandler(this.closeTellerToolStripMenuItem_Click);
            // 
            // tellerOperationsToolStripMenuItem
            // 
            this.tellerOperationsToolStripMenuItem.AccessibleDescription = null;
            this.tellerOperationsToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.tellerOperationsToolStripMenuItem, "tellerOperationsToolStripMenuItem");
            this.tellerOperationsToolStripMenuItem.BackgroundImage = null;
            this.tellerOperationsToolStripMenuItem.Name = "tellerOperationsToolStripMenuItem";
            this.tellerOperationsToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.tellerOperationsToolStripMenuItem.Click += new System.EventHandler(this.tellerOperationsToolStripMenuItem_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // mainStripToolBar
            // 
            this.mainStripToolBar.AccessibleDescription = null;
            this.mainStripToolBar.AccessibleName = null;
            resources.ApplyResources(this.mainStripToolBar, "mainStripToolBar");
            this.mainStripToolBar.BackColor = System.Drawing.SystemColors.Control;
            this.mainStripToolBar.BackgroundImage = null;
            this.mainStripToolBar.Font = null;
            this.mainStripToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainStripToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarButNew,
            this.toolStripSeparator1,
            this.toolBarButtonSearchPerson,
            this.toolBarButtonSearchContract,
            this.toolBarButtonReports,
            this.toolStripLabel1,
            this.toolStripButtonSearchProject});
            this.mainStripToolBar.Name = "mainStripToolBar";
            // 
            // toolBarButNew
            // 
            this.toolBarButNew.AccessibleDescription = null;
            this.toolBarButNew.AccessibleName = null;
            resources.ApplyResources(this.toolBarButNew, "toolBarButNew");
            this.toolBarButNew.BackgroundImage = null;
            this.toolBarButNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarButtonPerson,
            this.toolBarButtonNewGroup,
            this.tbbtnNewVillage,
            this.corporateToolStripMenuItem});
            this.toolBarButNew.Name = "toolBarButNew";
            this.toolBarButNew.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // toolBarButtonPerson
            // 
            this.toolBarButtonPerson.AccessibleDescription = null;
            this.toolBarButtonPerson.AccessibleName = null;
            resources.ApplyResources(this.toolBarButtonPerson, "toolBarButtonPerson");
            this.toolBarButtonPerson.BackgroundImage = null;
            this.toolBarButtonPerson.Name = "toolBarButtonPerson";
            this.toolBarButtonPerson.ShortcutKeyDisplayString = null;
            this.toolBarButtonPerson.Click += new System.EventHandler(this.toolBarButtonPerson_Click);
            // 
            // toolBarButtonNewGroup
            // 
            this.toolBarButtonNewGroup.AccessibleDescription = null;
            this.toolBarButtonNewGroup.AccessibleName = null;
            resources.ApplyResources(this.toolBarButtonNewGroup, "toolBarButtonNewGroup");
            this.toolBarButtonNewGroup.BackgroundImage = null;
            this.toolBarButtonNewGroup.Name = "toolBarButtonNewGroup";
            this.toolBarButtonNewGroup.ShortcutKeyDisplayString = null;
            this.toolBarButtonNewGroup.Click += new System.EventHandler(this.toolBarButtonNewGroup_Click);
            // 
            // tbbtnNewVillage
            // 
            this.tbbtnNewVillage.AccessibleDescription = null;
            this.tbbtnNewVillage.AccessibleName = null;
            resources.ApplyResources(this.tbbtnNewVillage, "tbbtnNewVillage");
            this.tbbtnNewVillage.BackgroundImage = null;
            this.tbbtnNewVillage.Name = "tbbtnNewVillage";
            this.tbbtnNewVillage.ShortcutKeyDisplayString = null;
            this.tbbtnNewVillage.Click += new System.EventHandler(this.tbbtnNewVillage_Click);
            // 
            // corporateToolStripMenuItem
            // 
            this.corporateToolStripMenuItem.AccessibleDescription = null;
            this.corporateToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.corporateToolStripMenuItem, "corporateToolStripMenuItem");
            this.corporateToolStripMenuItem.BackgroundImage = null;
            this.corporateToolStripMenuItem.Name = "corporateToolStripMenuItem";
            this.corporateToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.corporateToolStripMenuItem.Click += new System.EventHandler(this.corporateToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AccessibleDescription = null;
            this.toolStripSeparator1.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolBarButtonSearchPerson
            // 
            this.toolBarButtonSearchPerson.AccessibleDescription = null;
            this.toolBarButtonSearchPerson.AccessibleName = null;
            resources.ApplyResources(this.toolBarButtonSearchPerson, "toolBarButtonSearchPerson");
            this.toolBarButtonSearchPerson.BackgroundImage = null;
            this.toolBarButtonSearchPerson.Name = "toolBarButtonSearchPerson";
            this.toolBarButtonSearchPerson.Click += new System.EventHandler(this.toolBarButtonSearchPerson_Click);
            // 
            // toolBarButtonSearchContract
            // 
            this.toolBarButtonSearchContract.AccessibleDescription = null;
            this.toolBarButtonSearchContract.AccessibleName = null;
            resources.ApplyResources(this.toolBarButtonSearchContract, "toolBarButtonSearchContract");
            this.toolBarButtonSearchContract.BackgroundImage = null;
            this.toolBarButtonSearchContract.Name = "toolBarButtonSearchContract";
            this.toolBarButtonSearchContract.Click += new System.EventHandler(this.toolBarButtonSearchContract_Click);
            // 
            // toolBarButtonReports
            // 
            this.toolBarButtonReports.AccessibleDescription = null;
            this.toolBarButtonReports.AccessibleName = null;
            this.toolBarButtonReports.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolBarButtonReports, "toolBarButtonReports");
            this.toolBarButtonReports.BackgroundImage = null;
            this.toolBarButtonReports.Name = "toolBarButtonReports";
            this.toolBarButtonReports.Click += new System.EventHandler(this.toolBarButtonReports_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AccessibleDescription = null;
            this.toolStripLabel1.AccessibleName = null;
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.BackgroundImage = null;
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // toolStripButtonSearchProject
            // 
            this.toolStripButtonSearchProject.AccessibleDescription = null;
            this.toolStripButtonSearchProject.AccessibleName = null;
            resources.ApplyResources(this.toolStripButtonSearchProject, "toolStripButtonSearchProject");
            this.toolStripButtonSearchProject.BackgroundImage = null;
            this.toolStripButtonSearchProject.Name = "toolStripButtonSearchProject";
            this.toolStripButtonSearchProject.Click += new System.EventHandler(this.toolStripButtonSearchProject_Click);
            // 
            // toolBarLblVersion
            // 
            this.toolBarLblVersion.AccessibleDescription = null;
            this.toolBarLblVersion.AccessibleName = null;
            this.toolBarLblVersion.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolBarLblVersion, "toolBarLblVersion");
            this.toolBarLblVersion.BackgroundImage = null;
            this.toolBarLblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(156)))));
            this.toolBarLblVersion.Name = "toolBarLblVersion";
            // 
            // mainStatusBar
            // 
            this.mainStatusBar.AccessibleDescription = null;
            this.mainStatusBar.AccessibleName = null;
            resources.ApplyResources(this.mainStatusBar, "mainStatusBar");
            this.mainStatusBar.BackgroundImage = null;
            this.mainStatusBar.Font = null;
            this.mainStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainStatusBarLblUpdateVersion,
            this.mainStatusBarLblUserName,
            this.mainStatusBarLblDate,
            this.toolStripStatusLblBranchCode,
            this.mainStatusBarLblInfo,
            this.toolStripStatusLabelTeller,
            this.toolStripStatusLblDB});
            this.mainStatusBar.Name = "mainStatusBar";
            this.mainStatusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.mainStatusBar.ShowItemToolTips = true;
            this.mainStatusBar.SizingGrip = false;
            // 
            // mainStatusBarLblUpdateVersion
            // 
            this.mainStatusBarLblUpdateVersion.AccessibleDescription = null;
            this.mainStatusBarLblUpdateVersion.AccessibleName = null;
            resources.ApplyResources(this.mainStatusBarLblUpdateVersion, "mainStatusBarLblUpdateVersion");
            this.mainStatusBarLblUpdateVersion.BackgroundImage = null;
            this.mainStatusBarLblUpdateVersion.Name = "mainStatusBarLblUpdateVersion";
            this.mainStatusBarLblUpdateVersion.Spring = true;
            // 
            // mainStatusBarLblUserName
            // 
            this.mainStatusBarLblUserName.AccessibleDescription = null;
            this.mainStatusBarLblUserName.AccessibleName = null;
            resources.ApplyResources(this.mainStatusBarLblUserName, "mainStatusBarLblUserName");
            this.mainStatusBarLblUserName.BackgroundImage = null;
            this.mainStatusBarLblUserName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mainStatusBarLblUserName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mainStatusBarLblUserName.Name = "mainStatusBarLblUserName";
            // 
            // mainStatusBarLblDate
            // 
            this.mainStatusBarLblDate.AccessibleDescription = null;
            this.mainStatusBarLblDate.AccessibleName = null;
            resources.ApplyResources(this.mainStatusBarLblDate, "mainStatusBarLblDate");
            this.mainStatusBarLblDate.BackgroundImage = null;
            this.mainStatusBarLblDate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mainStatusBarLblDate.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mainStatusBarLblDate.Name = "mainStatusBarLblDate";
            // 
            // toolStripStatusLblBranchCode
            // 
            this.toolStripStatusLblBranchCode.AccessibleDescription = null;
            this.toolStripStatusLblBranchCode.AccessibleName = null;
            resources.ApplyResources(this.toolStripStatusLblBranchCode, "toolStripStatusLblBranchCode");
            this.toolStripStatusLblBranchCode.BackgroundImage = null;
            this.toolStripStatusLblBranchCode.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLblBranchCode.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLblBranchCode.Name = "toolStripStatusLblBranchCode";
            // 
            // mainStatusBarLblInfo
            // 
            this.mainStatusBarLblInfo.AccessibleDescription = null;
            this.mainStatusBarLblInfo.AccessibleName = null;
            resources.ApplyResources(this.mainStatusBarLblInfo, "mainStatusBarLblInfo");
            this.mainStatusBarLblInfo.BackColor = System.Drawing.Color.White;
            this.mainStatusBarLblInfo.BackgroundImage = null;
            this.mainStatusBarLblInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mainStatusBarLblInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mainStatusBarLblInfo.ForeColor = System.Drawing.Color.Black;
            this.mainStatusBarLblInfo.Name = "mainStatusBarLblInfo";
            // 
            // toolStripStatusLabelTeller
            // 
            this.toolStripStatusLabelTeller.AccessibleDescription = null;
            this.toolStripStatusLabelTeller.AccessibleName = null;
            resources.ApplyResources(this.toolStripStatusLabelTeller, "toolStripStatusLabelTeller");
            this.toolStripStatusLabelTeller.BackgroundImage = null;
            this.toolStripStatusLabelTeller.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelTeller.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabelTeller.Name = "toolStripStatusLabelTeller";
            // 
            // toolStripStatusLblDB
            // 
            this.toolStripStatusLblDB.AccessibleDescription = null;
            this.toolStripStatusLblDB.AccessibleName = null;
            resources.ApplyResources(this.toolStripStatusLblDB, "toolStripStatusLblDB");
            this.toolStripStatusLblDB.BackgroundImage = null;
            this.toolStripStatusLblDB.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLblDB.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLblDB.Image = global::Octopus.GUI.Properties.Resources.thame1_1_database;
            this.toolStripStatusLblDB.Name = "toolStripStatusLblDB";
            // 
            // bwAlerts
            // 
            this.bwAlerts.WorkerSupportsCancellation = true;
            this.bwAlerts.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnAlertsLoading);
            this.bwAlerts.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnAlertsLoaded);
            // 
            // nIUpdateAvailable
            // 
            this.nIUpdateAvailable.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.nIUpdateAvailable, "nIUpdateAvailable");
            this.nIUpdateAvailable.BalloonTipClicked += new System.EventHandler(this.nIUpdateAvailable_BalloonTipClicked);
            // 
            // openCustomizableFileDialog
            // 
            resources.ApplyResources(this.openCustomizableFileDialog, "openCustomizableFileDialog");
            // 
            // colAlerts_Address
            // 
            this.colAlerts_Address.AspectName = "Address";
            resources.ApplyResources(this.colAlerts_Address, "colAlerts_Address");
            this.colAlerts_Address.GroupWithItemCountFormat = null;
            this.colAlerts_Address.GroupWithItemCountSingularFormat = null;
            this.colAlerts_Address.IsEditable = false;
            this.colAlerts_Address.IsVisible = false;
            this.colAlerts_Address.ToolTipText = null;
            // 
            // colAlerts_City
            // 
            this.colAlerts_City.AspectName = "City";
            resources.ApplyResources(this.colAlerts_City, "colAlerts_City");
            this.colAlerts_City.GroupWithItemCountFormat = null;
            this.colAlerts_City.GroupWithItemCountSingularFormat = null;
            this.colAlerts_City.IsEditable = false;
            this.colAlerts_City.IsVisible = false;
            this.colAlerts_City.ToolTipText = null;
            // 
            // colAlerts_Phone
            // 
            this.colAlerts_Phone.AspectName = "Phone";
            resources.ApplyResources(this.colAlerts_Phone, "colAlerts_Phone");
            this.colAlerts_Phone.GroupWithItemCountFormat = null;
            this.colAlerts_Phone.GroupWithItemCountSingularFormat = null;
            this.colAlerts_Phone.IsEditable = false;
            this.colAlerts_Phone.IsVisible = false;
            this.colAlerts_Phone.ToolTipText = null;
            // 
            // splitter6
            // 
            this.splitter6.AccessibleDescription = null;
            this.splitter6.AccessibleName = null;
            resources.ApplyResources(this.splitter6, "splitter6");
            this.splitter6.AnimationDelay = 20;
            this.splitter6.AnimationStep = 20;
            this.splitter6.BackgroundImage = null;
            this.splitter6.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.splitter6.ControlToHide = this.panelLeft;
            this.splitter6.ExpandParentForm = false;
            this.splitter6.Font = null;
            this.splitter6.Name = "splitter6";
            this.splitter6.TabStop = false;
            this.splitter6.UseAnimations = false;
            this.splitter6.VisualStyle = Octopus.GUI.UserControl.VisualStyles.Mozilla;
            // 
            // panelLeft
            // 
            this.panelLeft.AccessibleDescription = null;
            this.panelLeft.AccessibleName = null;
            resources.ApplyResources(this.panelLeft, "panelLeft");
            this.panelLeft.BackColor = System.Drawing.Color.Transparent;
            this.panelLeft.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
            this.panelLeft.Controls.Add(this.olvAlerts);
            this.panelLeft.Controls.Add(this.lblTitle);
            this.panelLeft.Controls.Add(this.tabFilter);
            this.panelLeft.Font = null;
            this.panelLeft.Name = "panelLeft";
            // 
            // olvAlerts
            // 
            this.olvAlerts.AccessibleDescription = null;
            this.olvAlerts.AccessibleName = null;
            resources.ApplyResources(this.olvAlerts, "olvAlerts");
            this.olvAlerts.AllColumns.Add(this.colAlerts_ContractCode);
            this.olvAlerts.AllColumns.Add(this.colAlerts_Status);
            this.olvAlerts.AllColumns.Add(this.colAlerts_Client);
            this.olvAlerts.AllColumns.Add(this.colAlerts_LoanOfficer);
            this.olvAlerts.AllColumns.Add(this.colAlerts_Date);
            this.olvAlerts.AllColumns.Add(this.colAlerts_Amount);
            this.olvAlerts.AllColumns.Add(this.colAlerts_Address);
            this.olvAlerts.AllColumns.Add(this.colAlerts_City);
            this.olvAlerts.AllColumns.Add(this.colAlerts_Phone);
            this.olvAlerts.AllColumns.Add(this.colAlerts_BranchName);
            this.olvAlerts.BackgroundImage = null;
            this.olvAlerts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAlerts_ContractCode,
            this.colAlerts_Status,
            this.colAlerts_Client,
            this.colAlerts_LoanOfficer,
            this.colAlerts_Date,
            this.colAlerts_Amount});
            this.olvAlerts.EmptyListMsg = null;
            this.olvAlerts.Font = null;
            this.olvAlerts.FullRowSelect = true;
            this.olvAlerts.GridLines = true;
            this.olvAlerts.GroupWithItemCountFormat = null;
            this.olvAlerts.GroupWithItemCountSingularFormat = null;
            this.olvAlerts.HasCollapsibleGroups = false;
            this.olvAlerts.Name = "olvAlerts";
            this.olvAlerts.OverlayText.Text = null;
            this.olvAlerts.ShowGroups = false;
            this.olvAlerts.SmallImageList = this.imageListAlert;
            this.olvAlerts.UseCompatibleStateImageBehavior = false;
            this.olvAlerts.View = System.Windows.Forms.View.Details;
            this.olvAlerts.ItemsChanged += new System.EventHandler<BrightIdeasSoftware.ItemsChangedEventArgs>(this.OnAlertItemsChanged);
            this.olvAlerts.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.OnFormatAlertRow);
            this.olvAlerts.DoubleClick += new System.EventHandler(this.OnAlertDoubleClicked);
            // 
            // colAlerts_ContractCode
            // 
            this.colAlerts_ContractCode.AspectName = "ContractCode";
            this.colAlerts_ContractCode.GroupWithItemCountFormat = null;
            this.colAlerts_ContractCode.GroupWithItemCountSingularFormat = null;
            this.colAlerts_ContractCode.IsEditable = false;
            resources.ApplyResources(this.colAlerts_ContractCode, "colAlerts_ContractCode");
            this.colAlerts_ContractCode.ToolTipText = null;
            // 
            // colAlerts_Status
            // 
            this.colAlerts_Status.AspectName = "Status";
            this.colAlerts_Status.GroupWithItemCountFormat = null;
            this.colAlerts_Status.GroupWithItemCountSingularFormat = null;
            this.colAlerts_Status.IsEditable = false;
            resources.ApplyResources(this.colAlerts_Status, "colAlerts_Status");
            this.colAlerts_Status.ToolTipText = null;
            // 
            // colAlerts_Client
            // 
            this.colAlerts_Client.AspectName = "ClientName";
            this.colAlerts_Client.GroupWithItemCountFormat = null;
            this.colAlerts_Client.GroupWithItemCountSingularFormat = null;
            this.colAlerts_Client.IsEditable = false;
            resources.ApplyResources(this.colAlerts_Client, "colAlerts_Client");
            this.colAlerts_Client.ToolTipText = null;
            // 
            // colAlerts_LoanOfficer
            // 
            this.colAlerts_LoanOfficer.AspectName = "LoanOfficer";
            this.colAlerts_LoanOfficer.GroupWithItemCountFormat = null;
            this.colAlerts_LoanOfficer.GroupWithItemCountSingularFormat = null;
            this.colAlerts_LoanOfficer.IsEditable = false;
            resources.ApplyResources(this.colAlerts_LoanOfficer, "colAlerts_LoanOfficer");
            this.colAlerts_LoanOfficer.ToolTipText = null;
            // 
            // colAlerts_Date
            // 
            this.colAlerts_Date.AspectName = "Date";
            this.colAlerts_Date.GroupWithItemCountFormat = null;
            this.colAlerts_Date.GroupWithItemCountSingularFormat = null;
            this.colAlerts_Date.IsEditable = false;
            resources.ApplyResources(this.colAlerts_Date, "colAlerts_Date");
            this.colAlerts_Date.ToolTipText = null;
            // 
            // colAlerts_Amount
            // 
            this.colAlerts_Amount.AspectName = "Amount";
            this.colAlerts_Amount.GroupWithItemCountFormat = null;
            this.colAlerts_Amount.GroupWithItemCountSingularFormat = null;
            this.colAlerts_Amount.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colAlerts_Amount.IsEditable = false;
            resources.ApplyResources(this.colAlerts_Amount, "colAlerts_Amount");
            this.colAlerts_Amount.ToolTipText = null;
            // 
            // colAlerts_BranchName
            // 
            this.colAlerts_BranchName.AspectName = "BranchName";
            resources.ApplyResources(this.colAlerts_BranchName, "colAlerts_BranchName");
            this.colAlerts_BranchName.GroupWithItemCountFormat = null;
            this.colAlerts_BranchName.GroupWithItemCountSingularFormat = null;
            this.colAlerts_BranchName.IsEditable = false;
            this.colAlerts_BranchName.IsVisible = false;
            this.colAlerts_BranchName.ToolTipText = null;
            // 
            // lblTitle
            // 
            this.lblTitle.AccessibleDescription = null;
            this.lblTitle.AccessibleName = null;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTitle.Name = "lblTitle";
            // 
            // tabFilter
            // 
            this.tabFilter.AccessibleDescription = null;
            this.tabFilter.AccessibleName = null;
            resources.ApplyResources(this.tabFilter, "tabFilter");
            this.tabFilter.BackgroundImage = null;
            this.tabFilter.Controls.Add(this.chkPostponedLoans, 0, 3);
            this.tabFilter.Controls.Add(this.tbFilter, 1, 0);
            this.tabFilter.Controls.Add(this.lblFilter, 0, 0);
            this.tabFilter.Controls.Add(this.chkLateLoans, 0, 1);
            this.tabFilter.Controls.Add(this.chkPendingLoans, 0, 2);
            this.tabFilter.Controls.Add(this.chkPendingSavings, 0, 5);
            this.tabFilter.Controls.Add(this.chkOverdraftSavings, 0, 6);
            this.tabFilter.Controls.Add(this.chkValidatedLoan, 0, 4);
            this.tabFilter.Font = null;
            this.tabFilter.Name = "tabFilter";
            // 
            // chkPostponedLoans
            // 
            this.chkPostponedLoans.AccessibleDescription = null;
            this.chkPostponedLoans.AccessibleName = null;
            resources.ApplyResources(this.chkPostponedLoans, "chkPostponedLoans");
            this.chkPostponedLoans.Checked = true;
            this.chkPostponedLoans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tabFilter.SetColumnSpan(this.chkPostponedLoans, 2);
            this.chkPostponedLoans.Font = null;
            this.chkPostponedLoans.Name = "chkPostponedLoans";
            this.chkPostponedLoans.CheckedChanged += new System.EventHandler(this.OnAlertCheckChanged);
            // 
            // tbFilter
            // 
            this.tbFilter.AccessibleDescription = null;
            this.tbFilter.AccessibleName = null;
            resources.ApplyResources(this.tbFilter, "tbFilter");
            this.tbFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.tbFilter.BackgroundImage = null;
            this.tbFilter.Font = null;
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.TextChanged += new System.EventHandler(this.OnFilterChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AccessibleDescription = null;
            this.lblFilter.AccessibleName = null;
            resources.ApplyResources(this.lblFilter, "lblFilter");
            this.lblFilter.Font = null;
            this.lblFilter.Name = "lblFilter";
            // 
            // chkLateLoans
            // 
            this.chkLateLoans.AccessibleDescription = null;
            this.chkLateLoans.AccessibleName = null;
            resources.ApplyResources(this.chkLateLoans, "chkLateLoans");
            this.chkLateLoans.Checked = true;
            this.chkLateLoans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tabFilter.SetColumnSpan(this.chkLateLoans, 2);
            this.chkLateLoans.Font = null;
            this.chkLateLoans.Name = "chkLateLoans";
            this.chkLateLoans.CheckedChanged += new System.EventHandler(this.OnAlertCheckChanged);
            // 
            // chkPendingLoans
            // 
            this.chkPendingLoans.AccessibleDescription = null;
            this.chkPendingLoans.AccessibleName = null;
            resources.ApplyResources(this.chkPendingLoans, "chkPendingLoans");
            this.chkPendingLoans.Checked = true;
            this.chkPendingLoans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tabFilter.SetColumnSpan(this.chkPendingLoans, 2);
            this.chkPendingLoans.Font = null;
            this.chkPendingLoans.Name = "chkPendingLoans";
            this.chkPendingLoans.CheckedChanged += new System.EventHandler(this.OnAlertCheckChanged);
            // 
            // chkPendingSavings
            // 
            this.chkPendingSavings.AccessibleDescription = null;
            this.chkPendingSavings.AccessibleName = null;
            resources.ApplyResources(this.chkPendingSavings, "chkPendingSavings");
            this.chkPendingSavings.Checked = true;
            this.chkPendingSavings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tabFilter.SetColumnSpan(this.chkPendingSavings, 2);
            this.chkPendingSavings.Font = null;
            this.chkPendingSavings.Name = "chkPendingSavings";
            this.chkPendingSavings.CheckedChanged += new System.EventHandler(this.OnAlertCheckChanged);
            // 
            // chkOverdraftSavings
            // 
            this.chkOverdraftSavings.AccessibleDescription = null;
            this.chkOverdraftSavings.AccessibleName = null;
            resources.ApplyResources(this.chkOverdraftSavings, "chkOverdraftSavings");
            this.chkOverdraftSavings.Checked = true;
            this.chkOverdraftSavings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tabFilter.SetColumnSpan(this.chkOverdraftSavings, 2);
            this.chkOverdraftSavings.Font = null;
            this.chkOverdraftSavings.Name = "chkOverdraftSavings";
            this.chkOverdraftSavings.CheckedChanged += new System.EventHandler(this.OnAlertCheckChanged);
            // 
            // chkValidatedLoan
            // 
            this.chkValidatedLoan.AccessibleDescription = null;
            this.chkValidatedLoan.AccessibleName = null;
            resources.ApplyResources(this.chkValidatedLoan, "chkValidatedLoan");
            this.chkValidatedLoan.Checked = true;
            this.chkValidatedLoan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tabFilter.SetColumnSpan(this.chkValidatedLoan, 2);
            this.chkValidatedLoan.Font = null;
            this.chkValidatedLoan.Name = "chkValidatedLoan";
            this.chkValidatedLoan.CheckedChanged += new System.EventHandler(this.OnAlertCheckChanged);
            // 
            // alertBindingSource
            // 
            this.alertBindingSource.DataSource = typeof(Octopus.CoreDomain.Alert);
            // 
            // LotrasmicMainWindowForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitter6);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.mainStatusBar);
            this.Controls.Add(this.mainStripToolBar);
            this.Controls.Add(this.mainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "LotrasmicMainWindowForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LotrasmicMainWindowForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LotrasmicMainWindowForm_FormClosing);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainStripToolBar.ResumeLayout(false);
            this.mainStripToolBar.PerformLayout();
            this.mainStatusBar.ResumeLayout(false);
            this.mainStatusBar.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvAlerts)).EndInit();
            this.tabFilter.ResumeLayout(false);
            this.tabFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private ToolStripMenuItem reasignToolStripMenuItem;


        private ToolStripMenuItem corporateToolStripMenuItem;
        private ToolStripMenuItem newCorporateToolStripMenuItem;
        private ToolStripButton toolStripButtonSearchProject;
        private ToolStripMenuItem questionnaireToolStripMenuItem;
        private ToolStripMenuItem savingProductsToolStripMenuItem;
        private ToolStripMenuItem tbbtnNewVillage;
        private ToolStripMenuItem mnuNewVillage;
        private ToolStripMenuItem miContractCode;
        private ToolStripMenuItem spanishToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private System.ComponentModel.BackgroundWorker bwAlerts;
        private ToolStripMenuItem standardToolStripMenuItem;
        private ToolStripMenuItem currenciesToolStripMenuItem;
        private ToolStripMenuItem mView;
        private ToolStripMenuItem miAuditTrail;
        private Panel panelLeft;
        private Label lblTitle;
        private ToolStripMenuItem accountingRulesToolStripMenuItem;
        private NotifyIcon nIUpdateAvailable;
        private ToolStripMenuItem rolesToolStripMenuItem;
        private OpenFileDialog openCustomizableFileDialog;
        private BindingSource alertBindingSource;
        private ToolStripMenuItem menuItemCollateralProducts;
        private ToolStripMenuItem userGuideToolStripMenuItem;
        private ToolStripMenuItem wIKIHelpToolStripMenuItem;
        private ToolStripMenuItem miReports;
        private ToolStripMenuItem trialBalanceToolStripMenuItem;
        private BrightIdeasSoftware.ObjectListView olvAlerts;
        private BrightIdeasSoftware.OLVColumn colAlerts_ContractCode;
        private BrightIdeasSoftware.OLVColumn colAlerts_Status;
        private BrightIdeasSoftware.OLVColumn colAlerts_Client;
        private BrightIdeasSoftware.OLVColumn colAlerts_LoanOfficer;
        private BrightIdeasSoftware.OLVColumn colAlerts_Date;
        private BrightIdeasSoftware.OLVColumn colAlerts_Amount;
        private BrightIdeasSoftware.OLVColumn colAlerts_Address;
        private BrightIdeasSoftware.OLVColumn colAlerts_City;
        private BrightIdeasSoftware.OLVColumn colAlerts_Phone;
        private Label lblFilter;
        private TextBox tbFilter;
        private CheckBox chkLateLoans;
        private TableLayoutPanel tabFilter;
        private ToolStripMenuItem changePasswordToolStripMenuItem;
        private CheckBox chkPendingLoans;
        private ToolStripMenuItem manualEntriesToolStripMenuItem;
        private ToolStripMenuItem portugueseToolStripMenuItem;
        private ToolStripMenuItem branchesToolStripMenuItem;
        private ToolStripMenuItem tellerManagementToolStripMenuItem;
        private ToolStripMenuItem closeTellerToolStripMenuItem;
        private ToolStripStatusLabel mainStatusBarLblInfo;
        private ToolStripStatusLabel toolStripStatusLabelTeller;
        private ToolStripStatusLabel toolStripStatusLblDB;
        private BrightIdeasSoftware.OLVColumn colAlerts_BranchName;
        private ToolStripMenuItem CustomizableFieldsToolStripMenuItem;
        private ToolStripMenuItem menuItemAddUser;
        private ToolStripSeparator toolStripSeparator4;
        private CheckBox chkOverdraftSavings;
        private CheckBox chkPostponedLoans;
        private CheckBox chkPendingSavings;
        private CheckBox chkValidatedLoan;
        private ToolStripMenuItem mnuExtensionManager;
        private ToolStripMenuItem mnuNewclosure;
        private System.ComponentModel.BackgroundWorker bwUserInformation;
        private ToolStripMenuItem fiscalYearToolStripMenuItem;
        private ToolStripMenuItem tellersToolStripMenuItem;
        private ToolStripMenuItem tellerOperationsToolStripMenuItem;


    }
}
