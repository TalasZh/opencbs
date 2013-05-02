using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Configuration
{
    partial class FrmAddSavingBookProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddSavingBookProduct));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlSaving = new System.Windows.Forms.TabControl();
            this.tabPageMainParameters = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbFrequency = new System.Windows.Forms.GroupBox();
            this.lbCalculAmount = new System.Windows.Forms.Label();
            this.cbCalculAmount = new System.Windows.Forms.ComboBox();
            this.lbPosting = new System.Windows.Forms.Label();
            this.lbAccrual = new System.Windows.Forms.Label();
            this.cbPosting = new System.Windows.Forms.ComboBox();
            this.cbAccrual = new System.Windows.Forms.ComboBox();
            this.groupBoxCurrency = new System.Windows.Forms.GroupBox();
            this.cbCurrency = new System.Windows.Forms.ComboBox();
            this.lbCodeSavingProduct = new System.Windows.Forms.Label();
            this.lbNameSavingProduct = new System.Windows.Forms.Label();
            this.gbClientType = new System.Windows.Forms.GroupBox();
            this.clientTypeCorpCheckBox = new System.Windows.Forms.CheckBox();
            this.clientTypeIndivCheckBox = new System.Windows.Forms.CheckBox();
            this.clientTypeVillageCheckBox = new System.Windows.Forms.CheckBox();
            this.clientTypeGroupCheckBox = new System.Windows.Forms.CheckBox();
            this.clientTypeAllCheckBox = new System.Windows.Forms.CheckBox();
            this.tbCodeSavingProduct = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.gbInitialAmount = new System.Windows.Forms.GroupBox();
            this.tbInitialAmountMax = new System.Windows.Forms.TextBox();
            this.tbInitialAmountMin = new System.Windows.Forms.TextBox();
            this.lbInitialAmonutMax = new System.Windows.Forms.Label();
            this.lbInitialAmountMin = new System.Windows.Forms.Label();
            this.gbBalance = new System.Windows.Forms.GroupBox();
            this.tbBalanceMax = new System.Windows.Forms.TextBox();
            this.tbBalanceMin = new System.Windows.Forms.TextBox();
            this.lbBalanceMax = new System.Windows.Forms.Label();
            this.lbBalanceMin = new System.Windows.Forms.Label();
            this.gbInterestRate = new System.Windows.Forms.GroupBox();
            this.lbYearlyInterestRate = new System.Windows.Forms.Label();
            this.lbYearlyInterestRateMax = new System.Windows.Forms.Label();
            this.lbYearlyInterestRateMin = new System.Windows.Forms.Label();
            this.tbInterestRateValue = new System.Windows.Forms.TextBox();
            this.lbInterestRateValue = new System.Windows.Forms.Label();
            this.tbInterestRateMax = new System.Windows.Forms.TextBox();
            this.tbInterestRateMin = new System.Windows.Forms.TextBox();
            this.lbInterestRateMax = new System.Windows.Forms.Label();
            this.lbInterestRateMin = new System.Windows.Forms.Label();
            this.tabPageFees = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTransactionIn = new System.Windows.Forms.ComboBox();
            this.gbDeposit = new System.Windows.Forms.GroupBox();
            this.tbDepositMax = new System.Windows.Forms.TextBox();
            this.tbDepositMin = new System.Windows.Forms.TextBox();
            this.lbDepositMax = new System.Windows.Forms.Label();
            this.lbDepositMin = new System.Windows.Forms.Label();
            this.gtDepositFees = new System.Windows.Forms.GroupBox();
            this.rbRateDepositFees = new System.Windows.Forms.RadioButton();
            this.rbFlatDepositFees = new System.Windows.Forms.RadioButton();
            this.lbDepositFeesType = new System.Windows.Forms.Label();
            this.tbDepositFees = new System.Windows.Forms.TextBox();
            this.lbDepositFees = new System.Windows.Forms.Label();
            this.tbDepositFeesMax = new System.Windows.Forms.TextBox();
            this.tbDepositFeesMin = new System.Windows.Forms.TextBox();
            this.lbDepositFeesMax = new System.Windows.Forms.Label();
            this.lbDepositFeesMin = new System.Windows.Forms.Label();
            this.gbTransferFees = new System.Windows.Forms.GroupBox();
            this.cbTransferType = new System.Windows.Forms.ComboBox();
            this.rbRateTransferFees = new System.Windows.Forms.RadioButton();
            this.tbTransferFees = new System.Windows.Forms.TextBox();
            this.rbFlatTransferFees = new System.Windows.Forms.RadioButton();
            this.lbTransferFeesValue = new System.Windows.Forms.Label();
            this.tbTransferFeesMax = new System.Windows.Forms.TextBox();
            this.tbTransferFeesMin = new System.Windows.Forms.TextBox();
            this.lbTransferFeesMax = new System.Windows.Forms.Label();
            this.lbTransferFeesMin = new System.Windows.Forms.Label();
            this.lbTransferFeesType = new System.Windows.Forms.Label();
            this.gbWithdrawFees = new System.Windows.Forms.GroupBox();
            this.rbRateWithdrawFees = new System.Windows.Forms.RadioButton();
            this.rbFlatWithdrawFees = new System.Windows.Forms.RadioButton();
            this.tbWithdrawFees = new System.Windows.Forms.TextBox();
            this.lbWithdrawFeesValue = new System.Windows.Forms.Label();
            this.tbWithdrawFeesMax = new System.Windows.Forms.TextBox();
            this.tbWithdrawFeesMin = new System.Windows.Forms.TextBox();
            this.lbWithdrawFeesType = new System.Windows.Forms.Label();
            this.lbWithdrawFeesMax = new System.Windows.Forms.Label();
            this.lbWithdrawFeesMin = new System.Windows.Forms.Label();
            this.gbTransfer = new System.Windows.Forms.GroupBox();
            this.tbTransferMax = new System.Windows.Forms.TextBox();
            this.tbTransferMin = new System.Windows.Forms.TextBox();
            this.lbTransferMax = new System.Windows.Forms.Label();
            this.lbTransferMin = new System.Windows.Forms.Label();
            this.gbWithDrawing = new System.Windows.Forms.GroupBox();
            this.tbDrawingMax = new System.Windows.Forms.TextBox();
            this.tbWithDrawingMin = new System.Windows.Forms.TextBox();
            this.lbWithDrawingMax = new System.Windows.Forms.Label();
            this.lbWithDrawingMin = new System.Windows.Forms.Label();
            this.tabPageManagement = new System.Windows.Forms.TabPage();
            this.gbReopenFees = new System.Windows.Forms.GroupBox();
            this.rbRateReopenFees = new System.Windows.Forms.RadioButton();
            this.rbFlatReopenFees = new System.Windows.Forms.RadioButton();
            this.lbReopenFeesType = new System.Windows.Forms.Label();
            this.tbReopenFeesValue = new System.Windows.Forms.TextBox();
            this.lbReopenFeesValue = new System.Windows.Forms.Label();
            this.tbReopenFeesMax = new System.Windows.Forms.TextBox();
            this.tbReopenFeesMin = new System.Windows.Forms.TextBox();
            this.lbReopenFeesMax = new System.Windows.Forms.Label();
            this.lbReopenFeesMin = new System.Windows.Forms.Label();
            this.gtManagementFees = new System.Windows.Forms.GroupBox();
            this.cbManagementFeeFreq = new System.Windows.Forms.ComboBox();
            this.rbRateManagementFees = new System.Windows.Forms.RadioButton();
            this.rbFlatManagementFees = new System.Windows.Forms.RadioButton();
            this.lbManagementFeesType = new System.Windows.Forms.Label();
            this.tbManagementFees = new System.Windows.Forms.TextBox();
            this.lbManagementFees = new System.Windows.Forms.Label();
            this.tbManagementFeesMax = new System.Windows.Forms.TextBox();
            this.tbManagementFeesMin = new System.Windows.Forms.TextBox();
            this.lbManagementFeesMax = new System.Windows.Forms.Label();
            this.lbManagementFeesMin = new System.Windows.Forms.Label();
            this.gtCloseFees = new System.Windows.Forms.GroupBox();
            this.rbRateCloseFees = new System.Windows.Forms.RadioButton();
            this.rbFlatCloseFees = new System.Windows.Forms.RadioButton();
            this.lbCloseFeesType = new System.Windows.Forms.Label();
            this.tbCloseFees = new System.Windows.Forms.TextBox();
            this.lbCloseFees = new System.Windows.Forms.Label();
            this.tbCloseFeesMax = new System.Windows.Forms.TextBox();
            this.tbCloseFeesMin = new System.Windows.Forms.TextBox();
            this.lbCloseFeesMax = new System.Windows.Forms.Label();
            this.lbCloseFeesMin = new System.Windows.Forms.Label();
            this.gbEntryFees = new System.Windows.Forms.GroupBox();
            this.rbRateEntryFees = new System.Windows.Forms.RadioButton();
            this.rbFlatEntryFees = new System.Windows.Forms.RadioButton();
            this.lbEntryFeesType = new System.Windows.Forms.Label();
            this.tbEntryFeesValue = new System.Windows.Forms.TextBox();
            this.lbEntryFeesValue = new System.Windows.Forms.Label();
            this.tbEntryFeesMax = new System.Windows.Forms.TextBox();
            this.tbEntryFeesMin = new System.Windows.Forms.TextBox();
            this.lbEntryFeesMax = new System.Windows.Forms.Label();
            this.lbEntryFeesMin = new System.Windows.Forms.Label();
            this.tabPageOverdraft = new System.Windows.Forms.TabPage();
            this.gtAgioFees = new System.Windows.Forms.GroupBox();
            this.rbRateAgioFees = new System.Windows.Forms.RadioButton();
            this.rbFlatAgioFees = new System.Windows.Forms.RadioButton();
            this.lbAgioFeesType = new System.Windows.Forms.Label();
            this.cbAgioFeesFreq = new System.Windows.Forms.ComboBox();
            this.tbAgioFees = new System.Windows.Forms.TextBox();
            this.lbAgioFeesValue = new System.Windows.Forms.Label();
            this.tbAgioFeesMax = new System.Windows.Forms.TextBox();
            this.tbAgioFeesMin = new System.Windows.Forms.TextBox();
            this.lbAgioFeesMax = new System.Windows.Forms.Label();
            this.lbAgioFeesMin = new System.Windows.Forms.Label();
            this.gtOverdraftFees = new System.Windows.Forms.GroupBox();
            this.rbRateOverdraftFees = new System.Windows.Forms.RadioButton();
            this.rbFlatOverdraftFees = new System.Windows.Forms.RadioButton();
            this.lbOverdraftFeesType = new System.Windows.Forms.Label();
            this.tbOverdraftFees = new System.Windows.Forms.TextBox();
            this.lbOverdraftFeesValue = new System.Windows.Forms.Label();
            this.tbOverdraftFeesMax = new System.Windows.Forms.TextBox();
            this.tbOverdraftFeesMin = new System.Windows.Forms.TextBox();
            this.lbOverdraftFeesMax = new System.Windows.Forms.Label();
            this.lbOverdraftFeesMin = new System.Windows.Forms.Label();
            this.tabPageTermDeposit = new System.Windows.Forms.TabPage();
            this.termDepositPanel = new System.Windows.Forms.Panel();
            this.gpPostingFrequency = new System.Windows.Forms.GroupBox();
            this.cbxPostingfrequency = new System.Windows.Forms.ComboBox();
            this.gbNumberOfPeriods = new System.Windows.Forms.GroupBox();
            this.tbTermDepositPeriodMax = new System.Windows.Forms.TextBox();
            this.tbTermDepositPeriodMin = new System.Windows.Forms.TextBox();
            this.lblMaxOfTermDepositPeriods = new System.Windows.Forms.Label();
            this.lblMinOfTermDepositPeriods = new System.Windows.Forms.Label();
            this.checkBoxUseTermDeposit = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btSavingProduct = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlSaving.SuspendLayout();
            this.tabPageMainParameters.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbFrequency.SuspendLayout();
            this.groupBoxCurrency.SuspendLayout();
            this.gbClientType.SuspendLayout();
            this.gbInitialAmount.SuspendLayout();
            this.gbBalance.SuspendLayout();
            this.gbInterestRate.SuspendLayout();
            this.tabPageFees.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbDeposit.SuspendLayout();
            this.gtDepositFees.SuspendLayout();
            this.gbTransferFees.SuspendLayout();
            this.gbWithdrawFees.SuspendLayout();
            this.gbTransfer.SuspendLayout();
            this.gbWithDrawing.SuspendLayout();
            this.tabPageManagement.SuspendLayout();
            this.gbReopenFees.SuspendLayout();
            this.gtManagementFees.SuspendLayout();
            this.gtCloseFees.SuspendLayout();
            this.gbEntryFees.SuspendLayout();
            this.tabPageOverdraft.SuspendLayout();
            this.gtAgioFees.SuspendLayout();
            this.gtOverdraftFees.SuspendLayout();
            this.tabPageTermDeposit.SuspendLayout();
            this.termDepositPanel.SuspendLayout();
            this.gpPostingFrequency.SuspendLayout();
            this.gbNumberOfPeriods.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControlSaving);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            // 
            // tabControlSaving
            // 
            this.tabControlSaving.Controls.Add(this.tabPageMainParameters);
            this.tabControlSaving.Controls.Add(this.tabPageFees);
            this.tabControlSaving.Controls.Add(this.tabPageManagement);
            this.tabControlSaving.Controls.Add(this.tabPageOverdraft);
            this.tabControlSaving.Controls.Add(this.tabPageTermDeposit);
            resources.ApplyResources(this.tabControlSaving, "tabControlSaving");
            this.tabControlSaving.Name = "tabControlSaving";
            this.tabControlSaving.SelectedIndex = 0;
            // 
            // tabPageMainParameters
            // 
            this.tabPageMainParameters.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabPageMainParameters, "tabPageMainParameters");
            this.tabPageMainParameters.Name = "tabPageMainParameters";
            // 
            // groupBox1
            //
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.gbFrequency);
            this.groupBox1.Controls.Add(this.groupBoxCurrency);
            this.groupBox1.Controls.Add(this.lbCodeSavingProduct);
            this.groupBox1.Controls.Add(this.lbNameSavingProduct);
            this.groupBox1.Controls.Add(this.gbClientType);
            this.groupBox1.Controls.Add(this.tbCodeSavingProduct);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.gbInitialAmount);
            this.groupBox1.Controls.Add(this.gbBalance);
            this.groupBox1.Controls.Add(this.gbInterestRate);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // gbFrequency
            // 
            this.gbFrequency.Controls.Add(this.lbCalculAmount);
            this.gbFrequency.Controls.Add(this.cbCalculAmount);
            this.gbFrequency.Controls.Add(this.lbPosting);
            this.gbFrequency.Controls.Add(this.lbAccrual);
            this.gbFrequency.Controls.Add(this.cbPosting);
            this.gbFrequency.Controls.Add(this.cbAccrual);
            resources.ApplyResources(this.gbFrequency, "gbFrequency");
            this.gbFrequency.Name = "gbFrequency";
            this.gbFrequency.TabStop = false;
            // 
            // lbCalculAmount
            // 
            resources.ApplyResources(this.lbCalculAmount, "lbCalculAmount");
            this.lbCalculAmount.Name = "lbCalculAmount";
            // 
            // cbCalculAmount
            // 
            this.cbCalculAmount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCalculAmount.DropDownWidth = 180;
            resources.ApplyResources(this.cbCalculAmount, "cbCalculAmount");
            this.cbCalculAmount.FormattingEnabled = true;
            this.cbCalculAmount.Name = "cbCalculAmount";
            // 
            // lbPosting
            // 
            resources.ApplyResources(this.lbPosting, "lbPosting");
            this.lbPosting.Name = "lbPosting";
            // 
            // lbAccrual
            // 
            resources.ApplyResources(this.lbAccrual, "lbAccrual");
            this.lbAccrual.Name = "lbAccrual";
            // 
            // cbPosting
            // 
            this.cbPosting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbPosting, "cbPosting");
            this.cbPosting.FormattingEnabled = true;
            this.cbPosting.Name = "cbPosting";
            // 
            // cbAccrual
            // 
            this.cbAccrual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbAccrual, "cbAccrual");
            this.cbAccrual.FormattingEnabled = true;
            this.cbAccrual.Name = "cbAccrual";
            this.cbAccrual.SelectedIndexChanged += new System.EventHandler(this.cbAccrual_SelectedIndexChanged);
            // 
            // groupBoxCurrency
            // 
            this.groupBoxCurrency.Controls.Add(this.cbCurrency);
            resources.ApplyResources(this.groupBoxCurrency, "groupBoxCurrency");
            this.groupBoxCurrency.Name = "groupBoxCurrency";
            this.groupBoxCurrency.TabStop = false;
            // 
            // cbCurrency
            // 
            this.cbCurrency.DisplayMember = "Currency.Name";
            this.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbCurrency, "cbCurrency");
            this.cbCurrency.FormattingEnabled = true;
            this.cbCurrency.Name = "cbCurrency";
            // 
            // lbCodeSavingProduct
            // 
            resources.ApplyResources(this.lbCodeSavingProduct, "lbCodeSavingProduct");
            this.lbCodeSavingProduct.Name = "lbCodeSavingProduct";
            // 
            // lbNameSavingProduct
            // 
            resources.ApplyResources(this.lbNameSavingProduct, "lbNameSavingProduct");
            this.lbNameSavingProduct.Name = "lbNameSavingProduct";
            // 
            // gbClientType
            //
            this.gbClientType.Controls.Add(this.clientTypeCorpCheckBox);
            this.gbClientType.Controls.Add(this.clientTypeIndivCheckBox);
            this.gbClientType.Controls.Add(this.clientTypeVillageCheckBox);
            this.gbClientType.Controls.Add(this.clientTypeGroupCheckBox);
            this.gbClientType.Controls.Add(this.clientTypeAllCheckBox);
            resources.ApplyResources(this.gbClientType, "gbClientType");
            this.gbClientType.Name = "gbClientType";
            this.gbClientType.TabStop = false;
            // 
            // clientTypeCorpCheckBox
            // 
            resources.ApplyResources(this.clientTypeCorpCheckBox, "clientTypeCorpCheckBox");
            this.clientTypeCorpCheckBox.Name = "clientTypeCorpCheckBox";
            this.clientTypeCorpCheckBox.CheckedChanged += new System.EventHandler(this.clientTypeGroupCheckBox_CheckedChanged);
            // 
            // clientTypeIndivCheckBox
            // 
            resources.ApplyResources(this.clientTypeIndivCheckBox, "clientTypeIndivCheckBox");
            this.clientTypeIndivCheckBox.Name = "clientTypeIndivCheckBox";
            this.clientTypeIndivCheckBox.CheckedChanged += new System.EventHandler(this.clientTypeGroupCheckBox_CheckedChanged);
            // 
            // clientTypeVillageCheckBox
            // 
            resources.ApplyResources(this.clientTypeVillageCheckBox, "clientTypeVillageCheckBox");
            this.clientTypeVillageCheckBox.Name = "clientTypeVillageCheckBox";
            this.clientTypeVillageCheckBox.CheckedChanged += new System.EventHandler(this.clientTypeGroupCheckBox_CheckedChanged);
            // 
            // clientTypeGroupCheckBox
            // 
            resources.ApplyResources(this.clientTypeGroupCheckBox, "clientTypeGroupCheckBox");
            this.clientTypeGroupCheckBox.Name = "clientTypeGroupCheckBox";
            this.clientTypeGroupCheckBox.CheckedChanged += new System.EventHandler(this.clientTypeGroupCheckBox_CheckedChanged);
            // 
            // clientTypeAllCheckBox
            // 
            resources.ApplyResources(this.clientTypeAllCheckBox, "clientTypeAllCheckBox");
            this.clientTypeAllCheckBox.Name = "clientTypeAllCheckBox";
            this.clientTypeAllCheckBox.Click += new System.EventHandler(this.clientTypeAllCheckBox_Click);
            this.clientTypeAllCheckBox.CheckedChanged += new System.EventHandler(this.clientTypeAllCheckBox_CheckedChanged);
            // 
            // tbCodeSavingProduct
            // 
            resources.ApplyResources(this.tbCodeSavingProduct, "tbCodeSavingProduct");
            this.tbCodeSavingProduct.Name = "tbCodeSavingProduct";
            // 
            // tbName
            // 
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.Name = "tbName";
            // 
            // gbInitialAmount
            //
            this.gbInitialAmount.Controls.Add(this.tbInitialAmountMax);
            this.gbInitialAmount.Controls.Add(this.tbInitialAmountMin);
            this.gbInitialAmount.Controls.Add(this.lbInitialAmonutMax);
            this.gbInitialAmount.Controls.Add(this.lbInitialAmountMin);
            resources.ApplyResources(this.gbInitialAmount, "gbInitialAmount");
            this.gbInitialAmount.Name = "gbInitialAmount";
            this.gbInitialAmount.TabStop = false;
            // 
            // tbInitialAmountMax
            // 
            resources.ApplyResources(this.tbInitialAmountMax, "tbInitialAmountMax");
            this.tbInitialAmountMax.Name = "tbInitialAmountMax";
            this.tbInitialAmountMax.TextChanged += new System.EventHandler(this.tbInitialAmountMax_TextChanged);
            // 
            // tbInitialAmountMin
            // 
            resources.ApplyResources(this.tbInitialAmountMin, "tbInitialAmountMin");
            this.tbInitialAmountMin.Name = "tbInitialAmountMin";
            this.tbInitialAmountMin.TextChanged += new System.EventHandler(this.tbInitialAmountMin_TextChanged);
            // 
            // lbInitialAmonutMax
            // 
            resources.ApplyResources(this.lbInitialAmonutMax, "lbInitialAmonutMax");
            this.lbInitialAmonutMax.Name = "lbInitialAmonutMax";
            // 
            // lbInitialAmountMin
            // 
            resources.ApplyResources(this.lbInitialAmountMin, "lbInitialAmountMin");
            this.lbInitialAmountMin.Name = "lbInitialAmountMin";
            // 
            // gbBalance
            //
            this.gbBalance.Controls.Add(this.tbBalanceMax);
            this.gbBalance.Controls.Add(this.tbBalanceMin);
            this.gbBalance.Controls.Add(this.lbBalanceMax);
            this.gbBalance.Controls.Add(this.lbBalanceMin);
            resources.ApplyResources(this.gbBalance, "gbBalance");
            this.gbBalance.Name = "gbBalance";
            this.gbBalance.TabStop = false;
            // 
            // tbBalanceMax
            // 
            resources.ApplyResources(this.tbBalanceMax, "tbBalanceMax");
            this.tbBalanceMax.Name = "tbBalanceMax";
            this.tbBalanceMax.TextChanged += new System.EventHandler(this.tbBalanceMax_TextChanged);
            // 
            // tbBalanceMin
            // 
            resources.ApplyResources(this.tbBalanceMin, "tbBalanceMin");
            this.tbBalanceMin.Name = "tbBalanceMin";
            this.tbBalanceMin.TextChanged += new System.EventHandler(this.tbBalanceMin_TextChanged);
            // 
            // lbBalanceMax
            // 
            resources.ApplyResources(this.lbBalanceMax, "lbBalanceMax");
            this.lbBalanceMax.Name = "lbBalanceMax";
            // 
            // lbBalanceMin
            // 
            resources.ApplyResources(this.lbBalanceMin, "lbBalanceMin");
            this.lbBalanceMin.Name = "lbBalanceMin";
            // 
            // gbInterestRate
            //
            this.gbInterestRate.Controls.Add(this.lbYearlyInterestRate);
            this.gbInterestRate.Controls.Add(this.lbYearlyInterestRateMax);
            this.gbInterestRate.Controls.Add(this.lbYearlyInterestRateMin);
            this.gbInterestRate.Controls.Add(this.tbInterestRateValue);
            this.gbInterestRate.Controls.Add(this.lbInterestRateValue);
            this.gbInterestRate.Controls.Add(this.tbInterestRateMax);
            this.gbInterestRate.Controls.Add(this.tbInterestRateMin);
            this.gbInterestRate.Controls.Add(this.lbInterestRateMax);
            this.gbInterestRate.Controls.Add(this.lbInterestRateMin);
            resources.ApplyResources(this.gbInterestRate, "gbInterestRate");
            this.gbInterestRate.Name = "gbInterestRate";
            this.gbInterestRate.TabStop = false;
            // 
            // lbYearlyInterestRate
            // 
            resources.ApplyResources(this.lbYearlyInterestRate, "lbYearlyInterestRate");
            this.lbYearlyInterestRate.Name = "lbYearlyInterestRate";
            // 
            // lbYearlyInterestRateMax
            // 
            resources.ApplyResources(this.lbYearlyInterestRateMax, "lbYearlyInterestRateMax");
            this.lbYearlyInterestRateMax.Name = "lbYearlyInterestRateMax";
            // 
            // lbYearlyInterestRateMin
            // 
            resources.ApplyResources(this.lbYearlyInterestRateMin, "lbYearlyInterestRateMin");
            this.lbYearlyInterestRateMin.Name = "lbYearlyInterestRateMin";
            // 
            // tbInterestRateValue
            // 
            resources.ApplyResources(this.tbInterestRateValue, "tbInterestRateValue");
            this.tbInterestRateValue.Name = "tbInterestRateValue";
            this.tbInterestRateValue.TextChanged += new System.EventHandler(this.tbInterestRateValue_TextChanged);
            // 
            // lbInterestRateValue
            // 
            resources.ApplyResources(this.lbInterestRateValue, "lbInterestRateValue");
            this.lbInterestRateValue.Name = "lbInterestRateValue";
            // 
            // tbInterestRateMax
            // 
            resources.ApplyResources(this.tbInterestRateMax, "tbInterestRateMax");
            this.tbInterestRateMax.Name = "tbInterestRateMax";
            this.tbInterestRateMax.TextChanged += new System.EventHandler(this.tbInterestRateMax_TextChanged);
            // 
            // tbInterestRateMin
            // 
            resources.ApplyResources(this.tbInterestRateMin, "tbInterestRateMin");
            this.tbInterestRateMin.Name = "tbInterestRateMin";
            this.tbInterestRateMin.TextChanged += new System.EventHandler(this.tbInterestRateMin_TextChanged);
            // 
            // lbInterestRateMax
            // 
            resources.ApplyResources(this.lbInterestRateMax, "lbInterestRateMax");
            this.lbInterestRateMax.Name = "lbInterestRateMax";
            // 
            // lbInterestRateMin
            // 
            resources.ApplyResources(this.lbInterestRateMin, "lbInterestRateMin");
            this.lbInterestRateMin.Name = "lbInterestRateMin";
            // 
            // tabPageFees
            //
            resources.ApplyResources(this.tabPageFees, "tabPageFees");
            this.tabPageFees.Controls.Add(this.groupBox3);
            this.tabPageFees.Controls.Add(this.gbTransferFees);
            this.tabPageFees.Controls.Add(this.gbWithdrawFees);
            this.tabPageFees.Controls.Add(this.gbTransfer);
            this.tabPageFees.Controls.Add(this.gbWithDrawing);
            this.tabPageFees.Name = "tabPageFees";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cbTransactionIn);
            this.groupBox3.Controls.Add(this.gbDeposit);
            this.groupBox3.Controls.Add(this.gtDepositFees);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cbTransactionIn
            // 
            this.cbTransactionIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbTransactionIn, "cbTransactionIn");
            this.cbTransactionIn.FormattingEnabled = true;
            this.cbTransactionIn.Name = "cbTransactionIn";
            this.cbTransactionIn.SelectedIndexChanged += new System.EventHandler(this.cbTransactionIn_SelectedIndexChanged);
            // 
            // gbDeposit
            //
            this.gbDeposit.Controls.Add(this.tbDepositMax);
            this.gbDeposit.Controls.Add(this.tbDepositMin);
            this.gbDeposit.Controls.Add(this.lbDepositMax);
            this.gbDeposit.Controls.Add(this.lbDepositMin);
            resources.ApplyResources(this.gbDeposit, "gbDeposit");
            this.gbDeposit.Name = "gbDeposit";
            this.gbDeposit.TabStop = false;
            // 
            // tbDepositMax
            // 
            resources.ApplyResources(this.tbDepositMax, "tbDepositMax");
            this.tbDepositMax.Name = "tbDepositMax";
            this.tbDepositMax.TextChanged += new System.EventHandler(this.tbDepositMax_TextChanged);
            // 
            // tbDepositMin
            // 
            resources.ApplyResources(this.tbDepositMin, "tbDepositMin");
            this.tbDepositMin.Name = "tbDepositMin";
            this.tbDepositMin.TextChanged += new System.EventHandler(this.tbDepositMin_TextChanged);
            // 
            // lbDepositMax
            // 
            resources.ApplyResources(this.lbDepositMax, "lbDepositMax");
            this.lbDepositMax.Name = "lbDepositMax";
            // 
            // lbDepositMin
            // 
            resources.ApplyResources(this.lbDepositMin, "lbDepositMin");
            this.lbDepositMin.Name = "lbDepositMin";
            // 
            // gtDepositFees
            //
            this.gtDepositFees.Controls.Add(this.rbRateDepositFees);
            this.gtDepositFees.Controls.Add(this.rbFlatDepositFees);
            this.gtDepositFees.Controls.Add(this.lbDepositFeesType);
            this.gtDepositFees.Controls.Add(this.tbDepositFees);
            this.gtDepositFees.Controls.Add(this.lbDepositFees);
            this.gtDepositFees.Controls.Add(this.tbDepositFeesMax);
            this.gtDepositFees.Controls.Add(this.tbDepositFeesMin);
            this.gtDepositFees.Controls.Add(this.lbDepositFeesMax);
            this.gtDepositFees.Controls.Add(this.lbDepositFeesMin);
            resources.ApplyResources(this.gtDepositFees, "gtDepositFees");
            this.gtDepositFees.Name = "gtDepositFees";
            this.gtDepositFees.TabStop = false;
            // 
            // rbRateDepositFees
            // 
            resources.ApplyResources(this.rbRateDepositFees, "rbRateDepositFees");
            this.rbRateDepositFees.Name = "rbRateDepositFees";
            // 
            // rbFlatDepositFees
            // 
            resources.ApplyResources(this.rbFlatDepositFees, "rbFlatDepositFees");
            this.rbFlatDepositFees.Checked = true;
            this.rbFlatDepositFees.Name = "rbFlatDepositFees";
            this.rbFlatDepositFees.TabStop = true;
            // 
            // lbDepositFeesType
            // 
            resources.ApplyResources(this.lbDepositFeesType, "lbDepositFeesType");
            this.lbDepositFeesType.Name = "lbDepositFeesType";
            // 
            // tbDepositFees
            // 
            resources.ApplyResources(this.tbDepositFees, "tbDepositFees");
            this.tbDepositFees.Name = "tbDepositFees";
            this.tbDepositFees.TextChanged += new System.EventHandler(this.tbDepositFees_TextChanged);
            // 
            // lbDepositFees
            // 
            resources.ApplyResources(this.lbDepositFees, "lbDepositFees");
            this.lbDepositFees.Name = "lbDepositFees";
            // 
            // tbDepositFeesMax
            // 
            resources.ApplyResources(this.tbDepositFeesMax, "tbDepositFeesMax");
            this.tbDepositFeesMax.Name = "tbDepositFeesMax";
            this.tbDepositFeesMax.TextChanged += new System.EventHandler(this.tbDepositFeesMax_TextChanged);
            // 
            // tbDepositFeesMin
            // 
            resources.ApplyResources(this.tbDepositFeesMin, "tbDepositFeesMin");
            this.tbDepositFeesMin.Name = "tbDepositFeesMin";
            this.tbDepositFeesMin.TextChanged += new System.EventHandler(this.tbDepositFeesMin_TextChanged);
            // 
            // lbDepositFeesMax
            // 
            resources.ApplyResources(this.lbDepositFeesMax, "lbDepositFeesMax");
            this.lbDepositFeesMax.Name = "lbDepositFeesMax";
            // 
            // lbDepositFeesMin
            // 
            resources.ApplyResources(this.lbDepositFeesMin, "lbDepositFeesMin");
            this.lbDepositFeesMin.Name = "lbDepositFeesMin";
            // 
            // gbTransferFees
            //
            this.gbTransferFees.Controls.Add(this.cbTransferType);
            this.gbTransferFees.Controls.Add(this.rbRateTransferFees);
            this.gbTransferFees.Controls.Add(this.tbTransferFees);
            this.gbTransferFees.Controls.Add(this.rbFlatTransferFees);
            this.gbTransferFees.Controls.Add(this.lbTransferFeesValue);
            this.gbTransferFees.Controls.Add(this.tbTransferFeesMax);
            this.gbTransferFees.Controls.Add(this.tbTransferFeesMin);
            this.gbTransferFees.Controls.Add(this.lbTransferFeesMax);
            this.gbTransferFees.Controls.Add(this.lbTransferFeesMin);
            this.gbTransferFees.Controls.Add(this.lbTransferFeesType);
            resources.ApplyResources(this.gbTransferFees, "gbTransferFees");
            this.gbTransferFees.Name = "gbTransferFees";
            this.gbTransferFees.TabStop = false;
            // 
            // cbTransferType
            // 
            this.cbTransferType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbTransferType, "cbTransferType");
            this.cbTransferType.FormattingEnabled = true;
            this.cbTransferType.Name = "cbTransferType";
            this.cbTransferType.SelectedIndexChanged += new System.EventHandler(this.cbTransferType_SelectedIndexChanged);
            // 
            // rbRateTransferFees
            // 
            resources.ApplyResources(this.rbRateTransferFees, "rbRateTransferFees");
            this.rbRateTransferFees.Name = "rbRateTransferFees";
            this.rbRateTransferFees.CheckedChanged += new System.EventHandler(this.rbRateTransferFees_CheckedChanged);
            // 
            // tbTransferFees
            // 
            resources.ApplyResources(this.tbTransferFees, "tbTransferFees");
            this.tbTransferFees.Name = "tbTransferFees";
            this.tbTransferFees.TextChanged += new System.EventHandler(this.tbTransferFees_TextChanged);
            // 
            // rbFlatTransferFees
            // 
            resources.ApplyResources(this.rbFlatTransferFees, "rbFlatTransferFees");
            this.rbFlatTransferFees.Checked = true;
            this.rbFlatTransferFees.Name = "rbFlatTransferFees";
            this.rbFlatTransferFees.TabStop = true;
            this.rbFlatTransferFees.CheckedChanged += new System.EventHandler(this.rbFlatTransferFees_CheckedChanged);
            // 
            // lbTransferFeesValue
            // 
            resources.ApplyResources(this.lbTransferFeesValue, "lbTransferFeesValue");
            this.lbTransferFeesValue.Name = "lbTransferFeesValue";
            // 
            // tbTransferFeesMax
            // 
            resources.ApplyResources(this.tbTransferFeesMax, "tbTransferFeesMax");
            this.tbTransferFeesMax.Name = "tbTransferFeesMax";
            this.tbTransferFeesMax.TextChanged += new System.EventHandler(this.tbTransferFeesMax_TextChanged);
            // 
            // tbTransferFeesMin
            // 
            resources.ApplyResources(this.tbTransferFeesMin, "tbTransferFeesMin");
            this.tbTransferFeesMin.Name = "tbTransferFeesMin";
            this.tbTransferFeesMin.TextChanged += new System.EventHandler(this.tbTransferFeesMin_TextChanged);
            // 
            // lbTransferFeesMax
            // 
            resources.ApplyResources(this.lbTransferFeesMax, "lbTransferFeesMax");
            this.lbTransferFeesMax.Name = "lbTransferFeesMax";
            // 
            // lbTransferFeesMin
            // 
            resources.ApplyResources(this.lbTransferFeesMin, "lbTransferFeesMin");
            this.lbTransferFeesMin.Name = "lbTransferFeesMin";
            // 
            // lbTransferFeesType
            // 
            resources.ApplyResources(this.lbTransferFeesType, "lbTransferFeesType");
            this.lbTransferFeesType.Name = "lbTransferFeesType";
            // 
            // gbWithdrawFees
            //
            this.gbWithdrawFees.Controls.Add(this.rbRateWithdrawFees);
            this.gbWithdrawFees.Controls.Add(this.rbFlatWithdrawFees);
            this.gbWithdrawFees.Controls.Add(this.tbWithdrawFees);
            this.gbWithdrawFees.Controls.Add(this.lbWithdrawFeesValue);
            this.gbWithdrawFees.Controls.Add(this.tbWithdrawFeesMax);
            this.gbWithdrawFees.Controls.Add(this.tbWithdrawFeesMin);
            this.gbWithdrawFees.Controls.Add(this.lbWithdrawFeesType);
            this.gbWithdrawFees.Controls.Add(this.lbWithdrawFeesMax);
            this.gbWithdrawFees.Controls.Add(this.lbWithdrawFeesMin);
            resources.ApplyResources(this.gbWithdrawFees, "gbWithdrawFees");
            this.gbWithdrawFees.Name = "gbWithdrawFees";
            this.gbWithdrawFees.TabStop = false;
            // 
            // rbRateWithdrawFees
            // 
            resources.ApplyResources(this.rbRateWithdrawFees, "rbRateWithdrawFees");
            this.rbRateWithdrawFees.Name = "rbRateWithdrawFees";
            this.rbRateWithdrawFees.CheckedChanged += new System.EventHandler(this.rbRateWithdrawFees_CheckedChanged);
            // 
            // rbFlatWithdrawFees
            // 
            resources.ApplyResources(this.rbFlatWithdrawFees, "rbFlatWithdrawFees");
            this.rbFlatWithdrawFees.Checked = true;
            this.rbFlatWithdrawFees.Name = "rbFlatWithdrawFees";
            this.rbFlatWithdrawFees.TabStop = true;
            this.rbFlatWithdrawFees.CheckedChanged += new System.EventHandler(this.rbFlatWithdrawFees_CheckedChanged);
            // 
            // tbWithdrawFees
            // 
            resources.ApplyResources(this.tbWithdrawFees, "tbWithdrawFees");
            this.tbWithdrawFees.Name = "tbWithdrawFees";
            this.tbWithdrawFees.TextChanged += new System.EventHandler(this.tbWithdrawFees_TextChanged);
            // 
            // lbWithdrawFeesValue
            // 
            resources.ApplyResources(this.lbWithdrawFeesValue, "lbWithdrawFeesValue");
            this.lbWithdrawFeesValue.Name = "lbWithdrawFeesValue";
            // 
            // tbWithdrawFeesMax
            // 
            resources.ApplyResources(this.tbWithdrawFeesMax, "tbWithdrawFeesMax");
            this.tbWithdrawFeesMax.Name = "tbWithdrawFeesMax";
            this.tbWithdrawFeesMax.TextChanged += new System.EventHandler(this.tbWithdrawFeesMax_TextChanged);
            // 
            // tbWithdrawFeesMin
            // 
            resources.ApplyResources(this.tbWithdrawFeesMin, "tbWithdrawFeesMin");
            this.tbWithdrawFeesMin.Name = "tbWithdrawFeesMin";
            this.tbWithdrawFeesMin.TextChanged += new System.EventHandler(this.tbWithdrawFeesMin_TextChanged);
            // 
            // lbWithdrawFeesType
            // 
            resources.ApplyResources(this.lbWithdrawFeesType, "lbWithdrawFeesType");
            this.lbWithdrawFeesType.Name = "lbWithdrawFeesType";
            // 
            // lbWithdrawFeesMax
            // 
            resources.ApplyResources(this.lbWithdrawFeesMax, "lbWithdrawFeesMax");
            this.lbWithdrawFeesMax.Name = "lbWithdrawFeesMax";
            // 
            // lbWithdrawFeesMin
            // 
            resources.ApplyResources(this.lbWithdrawFeesMin, "lbWithdrawFeesMin");
            this.lbWithdrawFeesMin.Name = "lbWithdrawFeesMin";
            // 
            // gbTransfer
            //
            this.gbTransfer.Controls.Add(this.tbTransferMax);
            this.gbTransfer.Controls.Add(this.tbTransferMin);
            this.gbTransfer.Controls.Add(this.lbTransferMax);
            this.gbTransfer.Controls.Add(this.lbTransferMin);
            resources.ApplyResources(this.gbTransfer, "gbTransfer");
            this.gbTransfer.Name = "gbTransfer";
            this.gbTransfer.TabStop = false;
            // 
            // tbTransferMax
            // 
            resources.ApplyResources(this.tbTransferMax, "tbTransferMax");
            this.tbTransferMax.Name = "tbTransferMax";
            this.tbTransferMax.TextChanged += new System.EventHandler(this.tbTransferMax_TextChanged);
            // 
            // tbTransferMin
            // 
            resources.ApplyResources(this.tbTransferMin, "tbTransferMin");
            this.tbTransferMin.Name = "tbTransferMin";
            this.tbTransferMin.TextChanged += new System.EventHandler(this.tbTransferMin_TextChanged);
            // 
            // lbTransferMax
            // 
            resources.ApplyResources(this.lbTransferMax, "lbTransferMax");
            this.lbTransferMax.Name = "lbTransferMax";
            // 
            // lbTransferMin
            // 
            resources.ApplyResources(this.lbTransferMin, "lbTransferMin");
            this.lbTransferMin.Name = "lbTransferMin";
            // 
            // gbWithDrawing
            //
            this.gbWithDrawing.Controls.Add(this.tbDrawingMax);
            this.gbWithDrawing.Controls.Add(this.tbWithDrawingMin);
            this.gbWithDrawing.Controls.Add(this.lbWithDrawingMax);
            this.gbWithDrawing.Controls.Add(this.lbWithDrawingMin);
            resources.ApplyResources(this.gbWithDrawing, "gbWithDrawing");
            this.gbWithDrawing.Name = "gbWithDrawing";
            this.gbWithDrawing.TabStop = false;
            // 
            // tbDrawingMax
            // 
            resources.ApplyResources(this.tbDrawingMax, "tbDrawingMax");
            this.tbDrawingMax.Name = "tbDrawingMax";
            this.tbDrawingMax.TextChanged += new System.EventHandler(this.tbDrawingMax_TextChanged);
            // 
            // tbWithDrawingMin
            // 
            resources.ApplyResources(this.tbWithDrawingMin, "tbWithDrawingMin");
            this.tbWithDrawingMin.Name = "tbWithDrawingMin";
            this.tbWithDrawingMin.TextChanged += new System.EventHandler(this.tbWithDrawingMin_TextChanged);
            // 
            // lbWithDrawingMax
            // 
            resources.ApplyResources(this.lbWithDrawingMax, "lbWithDrawingMax");
            this.lbWithDrawingMax.Name = "lbWithDrawingMax";
            // 
            // lbWithDrawingMin
            // 
            resources.ApplyResources(this.lbWithDrawingMin, "lbWithDrawingMin");
            this.lbWithDrawingMin.Name = "lbWithDrawingMin";
            // 
            // tabPageManagement
            //
            resources.ApplyResources(this.tabPageManagement, "tabPageManagement");
            this.tabPageManagement.Controls.Add(this.gbReopenFees);
            this.tabPageManagement.Controls.Add(this.gtManagementFees);
            this.tabPageManagement.Controls.Add(this.gtCloseFees);
            this.tabPageManagement.Controls.Add(this.gbEntryFees);
            this.tabPageManagement.Name = "tabPageManagement";
            // 
            // gbReopenFees
            //
            this.gbReopenFees.Controls.Add(this.rbRateReopenFees);
            this.gbReopenFees.Controls.Add(this.rbFlatReopenFees);
            this.gbReopenFees.Controls.Add(this.lbReopenFeesType);
            this.gbReopenFees.Controls.Add(this.tbReopenFeesValue);
            this.gbReopenFees.Controls.Add(this.lbReopenFeesValue);
            this.gbReopenFees.Controls.Add(this.tbReopenFeesMax);
            this.gbReopenFees.Controls.Add(this.tbReopenFeesMin);
            this.gbReopenFees.Controls.Add(this.lbReopenFeesMax);
            this.gbReopenFees.Controls.Add(this.lbReopenFeesMin);
            resources.ApplyResources(this.gbReopenFees, "gbReopenFees");
            this.gbReopenFees.Name = "gbReopenFees";
            this.gbReopenFees.TabStop = false;
            // 
            // rbRateReopenFees
            // 
            resources.ApplyResources(this.rbRateReopenFees, "rbRateReopenFees");
            this.rbRateReopenFees.Name = "rbRateReopenFees";
            // 
            // rbFlatReopenFees
            // 
            resources.ApplyResources(this.rbFlatReopenFees, "rbFlatReopenFees");
            this.rbFlatReopenFees.Checked = true;
            this.rbFlatReopenFees.Name = "rbFlatReopenFees";
            this.rbFlatReopenFees.TabStop = true;
            // 
            // lbReopenFeesType
            // 
            resources.ApplyResources(this.lbReopenFeesType, "lbReopenFeesType");
            this.lbReopenFeesType.Name = "lbReopenFeesType";
            // 
            // tbReopenFeesValue
            // 
            resources.ApplyResources(this.tbReopenFeesValue, "tbReopenFeesValue");
            this.tbReopenFeesValue.Name = "tbReopenFeesValue";
            this.tbReopenFeesValue.TextChanged += new System.EventHandler(this.tbReopenFeesValue_TextChanged);
            // 
            // lbReopenFeesValue
            // 
            resources.ApplyResources(this.lbReopenFeesValue, "lbReopenFeesValue");
            this.lbReopenFeesValue.Name = "lbReopenFeesValue";
            // 
            // tbReopenFeesMax
            // 
            resources.ApplyResources(this.tbReopenFeesMax, "tbReopenFeesMax");
            this.tbReopenFeesMax.Name = "tbReopenFeesMax";
            this.tbReopenFeesMax.TextChanged += new System.EventHandler(this.tbReopenFeesMax_TextChanged);
            // 
            // tbReopenFeesMin
            // 
            resources.ApplyResources(this.tbReopenFeesMin, "tbReopenFeesMin");
            this.tbReopenFeesMin.Name = "tbReopenFeesMin";
            this.tbReopenFeesMin.TextChanged += new System.EventHandler(this.tbReopenFeesMin_TextChanged);
            // 
            // lbReopenFeesMax
            // 
            resources.ApplyResources(this.lbReopenFeesMax, "lbReopenFeesMax");
            this.lbReopenFeesMax.Name = "lbReopenFeesMax";
            // 
            // lbReopenFeesMin
            // 
            resources.ApplyResources(this.lbReopenFeesMin, "lbReopenFeesMin");
            this.lbReopenFeesMin.Name = "lbReopenFeesMin";
            // 
            // gtManagementFees
            //
            this.gtManagementFees.Controls.Add(this.cbManagementFeeFreq);
            this.gtManagementFees.Controls.Add(this.rbRateManagementFees);
            this.gtManagementFees.Controls.Add(this.rbFlatManagementFees);
            this.gtManagementFees.Controls.Add(this.lbManagementFeesType);
            this.gtManagementFees.Controls.Add(this.tbManagementFees);
            this.gtManagementFees.Controls.Add(this.lbManagementFees);
            this.gtManagementFees.Controls.Add(this.tbManagementFeesMax);
            this.gtManagementFees.Controls.Add(this.tbManagementFeesMin);
            this.gtManagementFees.Controls.Add(this.lbManagementFeesMax);
            this.gtManagementFees.Controls.Add(this.lbManagementFeesMin);
            resources.ApplyResources(this.gtManagementFees, "gtManagementFees");
            this.gtManagementFees.Name = "gtManagementFees";
            this.gtManagementFees.TabStop = false;
            // 
            // cbManagementFeeFreq
            // 
            this.cbManagementFeeFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbManagementFeeFreq, "cbManagementFeeFreq");
            this.cbManagementFeeFreq.FormattingEnabled = true;
            this.cbManagementFeeFreq.Name = "cbManagementFeeFreq";
            // 
            // rbRateManagementFees
            // 
            resources.ApplyResources(this.rbRateManagementFees, "rbRateManagementFees");
            this.rbRateManagementFees.Name = "rbRateManagementFees";
            // 
            // rbFlatManagementFees
            // 
            resources.ApplyResources(this.rbFlatManagementFees, "rbFlatManagementFees");
            this.rbFlatManagementFees.Checked = true;
            this.rbFlatManagementFees.Name = "rbFlatManagementFees";
            this.rbFlatManagementFees.TabStop = true;
            // 
            // lbManagementFeesType
            // 
            resources.ApplyResources(this.lbManagementFeesType, "lbManagementFeesType");
            this.lbManagementFeesType.Name = "lbManagementFeesType";
            // 
            // tbManagementFees
            // 
            resources.ApplyResources(this.tbManagementFees, "tbManagementFees");
            this.tbManagementFees.Name = "tbManagementFees";
            this.tbManagementFees.TextChanged += new System.EventHandler(this.tbManagementFees_TextChanged);
            // 
            // lbManagementFees
            // 
            resources.ApplyResources(this.lbManagementFees, "lbManagementFees");
            this.lbManagementFees.Name = "lbManagementFees";
            // 
            // tbManagementFeesMax
            // 
            resources.ApplyResources(this.tbManagementFeesMax, "tbManagementFeesMax");
            this.tbManagementFeesMax.Name = "tbManagementFeesMax";
            this.tbManagementFeesMax.TextChanged += new System.EventHandler(this.tbManagementFeesMax_TextChanged);
            // 
            // tbManagementFeesMin
            // 
            resources.ApplyResources(this.tbManagementFeesMin, "tbManagementFeesMin");
            this.tbManagementFeesMin.Name = "tbManagementFeesMin";
            this.tbManagementFeesMin.TextChanged += new System.EventHandler(this.tbManagementFeesMin_TextChanged);
            // 
            // lbManagementFeesMax
            // 
            resources.ApplyResources(this.lbManagementFeesMax, "lbManagementFeesMax");
            this.lbManagementFeesMax.Name = "lbManagementFeesMax";
            // 
            // lbManagementFeesMin
            // 
            resources.ApplyResources(this.lbManagementFeesMin, "lbManagementFeesMin");
            this.lbManagementFeesMin.Name = "lbManagementFeesMin";
            // 
            // gtCloseFees
            //
            this.gtCloseFees.Controls.Add(this.rbRateCloseFees);
            this.gtCloseFees.Controls.Add(this.rbFlatCloseFees);
            this.gtCloseFees.Controls.Add(this.lbCloseFeesType);
            this.gtCloseFees.Controls.Add(this.tbCloseFees);
            this.gtCloseFees.Controls.Add(this.lbCloseFees);
            this.gtCloseFees.Controls.Add(this.tbCloseFeesMax);
            this.gtCloseFees.Controls.Add(this.tbCloseFeesMin);
            this.gtCloseFees.Controls.Add(this.lbCloseFeesMax);
            this.gtCloseFees.Controls.Add(this.lbCloseFeesMin);
            resources.ApplyResources(this.gtCloseFees, "gtCloseFees");
            this.gtCloseFees.Name = "gtCloseFees";
            this.gtCloseFees.TabStop = false;
            // 
            // rbRateCloseFees
            // 
            resources.ApplyResources(this.rbRateCloseFees, "rbRateCloseFees");
            this.rbRateCloseFees.Name = "rbRateCloseFees";
            // 
            // rbFlatCloseFees
            // 
            resources.ApplyResources(this.rbFlatCloseFees, "rbFlatCloseFees");
            this.rbFlatCloseFees.Checked = true;
            this.rbFlatCloseFees.Name = "rbFlatCloseFees";
            this.rbFlatCloseFees.TabStop = true;
            // 
            // lbCloseFeesType
            // 
            resources.ApplyResources(this.lbCloseFeesType, "lbCloseFeesType");
            this.lbCloseFeesType.Name = "lbCloseFeesType";
            // 
            // tbCloseFees
            // 
            resources.ApplyResources(this.tbCloseFees, "tbCloseFees");
            this.tbCloseFees.Name = "tbCloseFees";
            this.tbCloseFees.TextChanged += new System.EventHandler(this.tbCloseFees_TextChanged);
            // 
            // lbCloseFees
            // 
            resources.ApplyResources(this.lbCloseFees, "lbCloseFees");
            this.lbCloseFees.Name = "lbCloseFees";
            // 
            // tbCloseFeesMax
            // 
            resources.ApplyResources(this.tbCloseFeesMax, "tbCloseFeesMax");
            this.tbCloseFeesMax.Name = "tbCloseFeesMax";
            this.tbCloseFeesMax.TextChanged += new System.EventHandler(this.tbCloseFeesMax_TextChanged);
            // 
            // tbCloseFeesMin
            // 
            resources.ApplyResources(this.tbCloseFeesMin, "tbCloseFeesMin");
            this.tbCloseFeesMin.Name = "tbCloseFeesMin";
            this.tbCloseFeesMin.TextChanged += new System.EventHandler(this.tbCloseFeesMin_TextChanged);
            // 
            // lbCloseFeesMax
            // 
            resources.ApplyResources(this.lbCloseFeesMax, "lbCloseFeesMax");
            this.lbCloseFeesMax.Name = "lbCloseFeesMax";
            // 
            // lbCloseFeesMin
            // 
            resources.ApplyResources(this.lbCloseFeesMin, "lbCloseFeesMin");
            this.lbCloseFeesMin.Name = "lbCloseFeesMin";
            // 
            // gbEntryFees
            //
            this.gbEntryFees.Controls.Add(this.rbRateEntryFees);
            this.gbEntryFees.Controls.Add(this.rbFlatEntryFees);
            this.gbEntryFees.Controls.Add(this.lbEntryFeesType);
            this.gbEntryFees.Controls.Add(this.tbEntryFeesValue);
            this.gbEntryFees.Controls.Add(this.lbEntryFeesValue);
            this.gbEntryFees.Controls.Add(this.tbEntryFeesMax);
            this.gbEntryFees.Controls.Add(this.tbEntryFeesMin);
            this.gbEntryFees.Controls.Add(this.lbEntryFeesMax);
            this.gbEntryFees.Controls.Add(this.lbEntryFeesMin);
            resources.ApplyResources(this.gbEntryFees, "gbEntryFees");
            this.gbEntryFees.Name = "gbEntryFees";
            this.gbEntryFees.TabStop = false;
            // 
            // rbRateEntryFees
            // 
            resources.ApplyResources(this.rbRateEntryFees, "rbRateEntryFees");
            this.rbRateEntryFees.Name = "rbRateEntryFees";
            // 
            // rbFlatEntryFees
            // 
            resources.ApplyResources(this.rbFlatEntryFees, "rbFlatEntryFees");
            this.rbFlatEntryFees.Checked = true;
            this.rbFlatEntryFees.Name = "rbFlatEntryFees";
            this.rbFlatEntryFees.TabStop = true;
            // 
            // lbEntryFeesType
            // 
            resources.ApplyResources(this.lbEntryFeesType, "lbEntryFeesType");
            this.lbEntryFeesType.Name = "lbEntryFeesType";
            // 
            // tbEntryFeesValue
            // 
            resources.ApplyResources(this.tbEntryFeesValue, "tbEntryFeesValue");
            this.tbEntryFeesValue.Name = "tbEntryFeesValue";
            this.tbEntryFeesValue.TextChanged += new System.EventHandler(this.tbEntryFeesValue_TextChanged);
            // 
            // lbEntryFeesValue
            // 
            resources.ApplyResources(this.lbEntryFeesValue, "lbEntryFeesValue");
            this.lbEntryFeesValue.Name = "lbEntryFeesValue";
            // 
            // tbEntryFeesMax
            // 
            resources.ApplyResources(this.tbEntryFeesMax, "tbEntryFeesMax");
            this.tbEntryFeesMax.Name = "tbEntryFeesMax";
            this.tbEntryFeesMax.TextChanged += new System.EventHandler(this.tbEntryFeesMax_TextChanged);
            // 
            // tbEntryFeesMin
            // 
            resources.ApplyResources(this.tbEntryFeesMin, "tbEntryFeesMin");
            this.tbEntryFeesMin.Name = "tbEntryFeesMin";
            this.tbEntryFeesMin.TextChanged += new System.EventHandler(this.tbEntryFeesMin_TextChanged);
            // 
            // lbEntryFeesMax
            // 
            resources.ApplyResources(this.lbEntryFeesMax, "lbEntryFeesMax");
            this.lbEntryFeesMax.Name = "lbEntryFeesMax";
            // 
            // lbEntryFeesMin
            // 
            resources.ApplyResources(this.lbEntryFeesMin, "lbEntryFeesMin");
            this.lbEntryFeesMin.Name = "lbEntryFeesMin";
            // 
            // tabPageOverdraft
            //
            resources.ApplyResources(this.tabPageOverdraft, "tabPageOverdraft");
            this.tabPageOverdraft.Controls.Add(this.gtAgioFees);
            this.tabPageOverdraft.Controls.Add(this.gtOverdraftFees);
            this.tabPageOverdraft.Name = "tabPageOverdraft";
            // 
            // gtAgioFees
            //
            this.gtAgioFees.Controls.Add(this.rbRateAgioFees);
            this.gtAgioFees.Controls.Add(this.rbFlatAgioFees);
            this.gtAgioFees.Controls.Add(this.lbAgioFeesType);
            this.gtAgioFees.Controls.Add(this.cbAgioFeesFreq);
            this.gtAgioFees.Controls.Add(this.tbAgioFees);
            this.gtAgioFees.Controls.Add(this.lbAgioFeesValue);
            this.gtAgioFees.Controls.Add(this.tbAgioFeesMax);
            this.gtAgioFees.Controls.Add(this.tbAgioFeesMin);
            this.gtAgioFees.Controls.Add(this.lbAgioFeesMax);
            this.gtAgioFees.Controls.Add(this.lbAgioFeesMin);
            resources.ApplyResources(this.gtAgioFees, "gtAgioFees");
            this.gtAgioFees.Name = "gtAgioFees";
            this.gtAgioFees.TabStop = false;
            // 
            // rbRateAgioFees
            // 
            resources.ApplyResources(this.rbRateAgioFees, "rbRateAgioFees");
            this.rbRateAgioFees.Checked = true;
            this.rbRateAgioFees.Name = "rbRateAgioFees";
            this.rbRateAgioFees.TabStop = true;
            // 
            // rbFlatAgioFees
            // 
            resources.ApplyResources(this.rbFlatAgioFees, "rbFlatAgioFees");
            this.rbFlatAgioFees.Name = "rbFlatAgioFees";
            // 
            // lbAgioFeesType
            // 
            resources.ApplyResources(this.lbAgioFeesType, "lbAgioFeesType");
            this.lbAgioFeesType.Name = "lbAgioFeesType";
            // 
            // cbAgioFeesFreq
            // 
            this.cbAgioFeesFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbAgioFeesFreq, "cbAgioFeesFreq");
            this.cbAgioFeesFreq.FormattingEnabled = true;
            this.cbAgioFeesFreq.Items.AddRange(new object[] {
            resources.GetString("cbAgioFeesFreq.Items")});
            this.cbAgioFeesFreq.Name = "cbAgioFeesFreq";
            // 
            // tbAgioFees
            // 
            resources.ApplyResources(this.tbAgioFees, "tbAgioFees");
            this.tbAgioFees.Name = "tbAgioFees";
            this.tbAgioFees.TextChanged += new System.EventHandler(this.tbAgioFees_TextChanged);
            // 
            // lbAgioFeesValue
            // 
            resources.ApplyResources(this.lbAgioFeesValue, "lbAgioFeesValue");
            this.lbAgioFeesValue.Name = "lbAgioFeesValue";
            // 
            // tbAgioFeesMax
            // 
            resources.ApplyResources(this.tbAgioFeesMax, "tbAgioFeesMax");
            this.tbAgioFeesMax.Name = "tbAgioFeesMax";
            this.tbAgioFeesMax.TextChanged += new System.EventHandler(this.tbAgioFeesMax_TextChanged);
            // 
            // tbAgioFeesMin
            // 
            resources.ApplyResources(this.tbAgioFeesMin, "tbAgioFeesMin");
            this.tbAgioFeesMin.Name = "tbAgioFeesMin";
            this.tbAgioFeesMin.TextChanged += new System.EventHandler(this.tbAgioFeesMin_TextChanged);
            // 
            // lbAgioFeesMax
            // 
            resources.ApplyResources(this.lbAgioFeesMax, "lbAgioFeesMax");
            this.lbAgioFeesMax.Name = "lbAgioFeesMax";
            // 
            // lbAgioFeesMin
            // 
            resources.ApplyResources(this.lbAgioFeesMin, "lbAgioFeesMin");
            this.lbAgioFeesMin.Name = "lbAgioFeesMin";
            // 
            // gtOverdraftFees
            //
            this.gtOverdraftFees.Controls.Add(this.rbRateOverdraftFees);
            this.gtOverdraftFees.Controls.Add(this.rbFlatOverdraftFees);
            this.gtOverdraftFees.Controls.Add(this.lbOverdraftFeesType);
            this.gtOverdraftFees.Controls.Add(this.tbOverdraftFees);
            this.gtOverdraftFees.Controls.Add(this.lbOverdraftFeesValue);
            this.gtOverdraftFees.Controls.Add(this.tbOverdraftFeesMax);
            this.gtOverdraftFees.Controls.Add(this.tbOverdraftFeesMin);
            this.gtOverdraftFees.Controls.Add(this.lbOverdraftFeesMax);
            this.gtOverdraftFees.Controls.Add(this.lbOverdraftFeesMin);
            resources.ApplyResources(this.gtOverdraftFees, "gtOverdraftFees");
            this.gtOverdraftFees.Name = "gtOverdraftFees";
            this.gtOverdraftFees.TabStop = false;
            // 
            // rbRateOverdraftFees
            // 
            resources.ApplyResources(this.rbRateOverdraftFees, "rbRateOverdraftFees");
            this.rbRateOverdraftFees.Name = "rbRateOverdraftFees";
            // 
            // rbFlatOverdraftFees
            // 
            resources.ApplyResources(this.rbFlatOverdraftFees, "rbFlatOverdraftFees");
            this.rbFlatOverdraftFees.Checked = true;
            this.rbFlatOverdraftFees.Name = "rbFlatOverdraftFees";
            this.rbFlatOverdraftFees.TabStop = true;
            // 
            // lbOverdraftFeesType
            // 
            resources.ApplyResources(this.lbOverdraftFeesType, "lbOverdraftFeesType");
            this.lbOverdraftFeesType.Name = "lbOverdraftFeesType";
            // 
            // tbOverdraftFees
            // 
            resources.ApplyResources(this.tbOverdraftFees, "tbOverdraftFees");
            this.tbOverdraftFees.Name = "tbOverdraftFees";
            this.tbOverdraftFees.TextChanged += new System.EventHandler(this.tbOverdraftFees_TextChanged);
            // 
            // lbOverdraftFeesValue
            // 
            resources.ApplyResources(this.lbOverdraftFeesValue, "lbOverdraftFeesValue");
            this.lbOverdraftFeesValue.Name = "lbOverdraftFeesValue";
            // 
            // tbOverdraftFeesMax
            // 
            resources.ApplyResources(this.tbOverdraftFeesMax, "tbOverdraftFeesMax");
            this.tbOverdraftFeesMax.Name = "tbOverdraftFeesMax";
            this.tbOverdraftFeesMax.TextChanged += new System.EventHandler(this.tbOverdraftFeesMax_TextChanged);
            // 
            // tbOverdraftFeesMin
            // 
            resources.ApplyResources(this.tbOverdraftFeesMin, "tbOverdraftFeesMin");
            this.tbOverdraftFeesMin.Name = "tbOverdraftFeesMin";
            this.tbOverdraftFeesMin.TextChanged += new System.EventHandler(this.tbOverdraftFeesMin_TextChanged);
            // 
            // lbOverdraftFeesMax
            // 
            resources.ApplyResources(this.lbOverdraftFeesMax, "lbOverdraftFeesMax");
            this.lbOverdraftFeesMax.Name = "lbOverdraftFeesMax";
            // 
            // lbOverdraftFeesMin
            // 
            resources.ApplyResources(this.lbOverdraftFeesMin, "lbOverdraftFeesMin");
            this.lbOverdraftFeesMin.Name = "lbOverdraftFeesMin";
            // 
            // tabPageTermDeposit
            //
            resources.ApplyResources(this.tabPageTermDeposit, "tabPageTermDeposit");
            this.tabPageTermDeposit.Controls.Add(this.termDepositPanel);
            this.tabPageTermDeposit.Controls.Add(this.checkBoxUseTermDeposit);
            this.tabPageTermDeposit.Name = "tabPageTermDeposit";
            // 
            // termDepositPanel
            // 
            this.termDepositPanel.Controls.Add(this.gpPostingFrequency);
            this.termDepositPanel.Controls.Add(this.gbNumberOfPeriods);
            resources.ApplyResources(this.termDepositPanel, "termDepositPanel");
            this.termDepositPanel.Name = "termDepositPanel";
            // 
            // gpPostingFrequency
            // 
            this.gpPostingFrequency.Controls.Add(this.cbxPostingfrequency);
            resources.ApplyResources(this.gpPostingFrequency, "gpPostingFrequency");
            this.gpPostingFrequency.Name = "gpPostingFrequency";
            this.gpPostingFrequency.TabStop = false;
            // 
            // cbxPostingfrequency
            // 
            this.cbxPostingfrequency.DisplayMember = "Name";
            this.cbxPostingfrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbxPostingfrequency, "cbxPostingfrequency");
            this.cbxPostingfrequency.FormattingEnabled = true;
            this.cbxPostingfrequency.Name = "cbxPostingfrequency";
            this.cbxPostingfrequency.ValueMember = "Id";
            this.cbxPostingfrequency.SelectedIndexChanged += new System.EventHandler(this.cbxPostingfrequency_SelectedIndexChanged);
            // 
            // gbNumberOfPeriods
            // 
            this.gbNumberOfPeriods.Controls.Add(this.tbTermDepositPeriodMax);
            this.gbNumberOfPeriods.Controls.Add(this.tbTermDepositPeriodMin);
            this.gbNumberOfPeriods.Controls.Add(this.lblMaxOfTermDepositPeriods);
            this.gbNumberOfPeriods.Controls.Add(this.lblMinOfTermDepositPeriods);
            resources.ApplyResources(this.gbNumberOfPeriods, "gbNumberOfPeriods");
            this.gbNumberOfPeriods.Name = "gbNumberOfPeriods";
            this.gbNumberOfPeriods.TabStop = false;
            // 
            // tbTermDepositPeriodMax
            // 
            resources.ApplyResources(this.tbTermDepositPeriodMax, "tbTermDepositPeriodMax");
            this.tbTermDepositPeriodMax.Name = "tbTermDepositPeriodMax";
            this.tbTermDepositPeriodMax.TextChanged += new System.EventHandler(this.tbTermDepositPeriodMax_TextChanged);
            // 
            // tbTermDepositPeriodMin
            // 
            resources.ApplyResources(this.tbTermDepositPeriodMin, "tbTermDepositPeriodMin");
            this.tbTermDepositPeriodMin.Name = "tbTermDepositPeriodMin";
            this.tbTermDepositPeriodMin.TextChanged += new System.EventHandler(this.tbTermDepositPeriodMin_TextChanged);
            // 
            // lblMaxOfTermDepositPeriods
            // 
            resources.ApplyResources(this.lblMaxOfTermDepositPeriods, "lblMaxOfTermDepositPeriods");
            this.lblMaxOfTermDepositPeriods.Name = "lblMaxOfTermDepositPeriods";
            // 
            // lblMinOfTermDepositPeriods
            // 
            resources.ApplyResources(this.lblMinOfTermDepositPeriods, "lblMinOfTermDepositPeriods");
            this.lblMinOfTermDepositPeriods.Name = "lblMinOfTermDepositPeriods";
            // 
            // checkBoxUseTermDeposit
            // 
            resources.ApplyResources(this.checkBoxUseTermDeposit, "checkBoxUseTermDeposit");
            this.checkBoxUseTermDeposit.Name = "checkBoxUseTermDeposit";
            this.checkBoxUseTermDeposit.CheckedChanged += new System.EventHandler(this.checkBoxUseTermDeposit_CheckedChanged);
            // 
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.btSavingProduct);
            this.groupBox2.Controls.Add(this.bClose);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btSavingProduct
            // 
            resources.ApplyResources(this.btSavingProduct, "btSavingProduct");
            this.btSavingProduct.Name = "btSavingProduct";
            this.btSavingProduct.Click += new System.EventHandler(this.btSavingProduct_Click);
            // 
            // bClose
            // 
            resources.ApplyResources(this.bClose, "bClose");
            this.bClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bClose.Name = "bClose";
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // FrmAddSavingBookProduct
            // 
            this.AcceptButton = this.btSavingProduct;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bClose;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddSavingBookProduct";
            this.Load += new System.EventHandler(this.FrmAddSavingBookProduct_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControlSaving.ResumeLayout(false);
            this.tabPageMainParameters.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbFrequency.ResumeLayout(false);
            this.gbFrequency.PerformLayout();
            this.groupBoxCurrency.ResumeLayout(false);
            this.gbClientType.ResumeLayout(false);
            this.gbClientType.PerformLayout();
            this.gbInitialAmount.ResumeLayout(false);
            this.gbInitialAmount.PerformLayout();
            this.gbBalance.ResumeLayout(false);
            this.gbBalance.PerformLayout();
            this.gbInterestRate.ResumeLayout(false);
            this.gbInterestRate.PerformLayout();
            this.tabPageFees.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbDeposit.ResumeLayout(false);
            this.gbDeposit.PerformLayout();
            this.gtDepositFees.ResumeLayout(false);
            this.gtDepositFees.PerformLayout();
            this.gbTransferFees.ResumeLayout(false);
            this.gbTransferFees.PerformLayout();
            this.gbWithdrawFees.ResumeLayout(false);
            this.gbWithdrawFees.PerformLayout();
            this.gbTransfer.ResumeLayout(false);
            this.gbTransfer.PerformLayout();
            this.gbWithDrawing.ResumeLayout(false);
            this.gbWithDrawing.PerformLayout();
            this.tabPageManagement.ResumeLayout(false);
            this.gbReopenFees.ResumeLayout(false);
            this.gbReopenFees.PerformLayout();
            this.gtManagementFees.ResumeLayout(false);
            this.gtManagementFees.PerformLayout();
            this.gtCloseFees.ResumeLayout(false);
            this.gtCloseFees.PerformLayout();
            this.gbEntryFees.ResumeLayout(false);
            this.gbEntryFees.PerformLayout();
            this.tabPageOverdraft.ResumeLayout(false);
            this.gtAgioFees.ResumeLayout(false);
            this.gtAgioFees.PerformLayout();
            this.gtOverdraftFees.ResumeLayout(false);
            this.gtOverdraftFees.PerformLayout();
            this.tabPageTermDeposit.ResumeLayout(false);
            this.tabPageTermDeposit.PerformLayout();
            this.termDepositPanel.ResumeLayout(false);
            this.gpPostingFrequency.ResumeLayout(false);
            this.gbNumberOfPeriods.ResumeLayout(false);
            this.gbNumberOfPeriods.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbNameSavingProduct;
        private System.Windows.Forms.GroupBox gbClientType;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.GroupBox gbInitialAmount;
        private System.Windows.Forms.GroupBox gbBalance;
        private System.Windows.Forms.GroupBox gbInterestRate;
        private System.Windows.Forms.TextBox tbInitialAmountMax;
        private System.Windows.Forms.TextBox tbInitialAmountMin;
        private System.Windows.Forms.Label lbInitialAmonutMax;
        private System.Windows.Forms.Label lbInitialAmountMin;
        private System.Windows.Forms.TextBox tbBalanceMax;
        private System.Windows.Forms.TextBox tbBalanceMin;
        private System.Windows.Forms.Label lbBalanceMax;
        private System.Windows.Forms.Label lbBalanceMin;
        private System.Windows.Forms.TextBox tbInterestRateMax;
        private System.Windows.Forms.TextBox tbInterestRateMin;
        private System.Windows.Forms.Label lbInterestRateMax;
        private System.Windows.Forms.Label lbInterestRateMin;
        private System.Windows.Forms.TextBox tbInterestRateValue;
        private System.Windows.Forms.Label lbInterestRateValue;
        private System.Windows.Forms.Button btSavingProduct;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbFrequency;
        private System.Windows.Forms.Label lbPosting;
        private System.Windows.Forms.Label lbAccrual;
        private System.Windows.Forms.ComboBox cbPosting;
        private System.Windows.Forms.ComboBox cbAccrual;
        private System.Windows.Forms.GroupBox groupBoxCurrency;
        private System.Windows.Forms.ComboBox cbCurrency;
        private System.Windows.Forms.Label lbCalculAmount;
        private System.Windows.Forms.ComboBox cbCalculAmount;
        private System.Windows.Forms.TabControl tabControlSaving;
        private System.Windows.Forms.TabPage tabPageMainParameters;
        private System.Windows.Forms.TabPage tabPageFees;
        private System.Windows.Forms.Label lbCodeSavingProduct;
        private System.Windows.Forms.TextBox tbCodeSavingProduct;
        private System.Windows.Forms.GroupBox gbTransfer;
        private System.Windows.Forms.TextBox tbTransferMax;
        private System.Windows.Forms.TextBox tbTransferMin;
        private System.Windows.Forms.Label lbTransferMax;
        private System.Windows.Forms.Label lbTransferMin;
        private System.Windows.Forms.GroupBox gbDeposit;
        private System.Windows.Forms.TextBox tbDepositMax;
        private System.Windows.Forms.TextBox tbDepositMin;
        private System.Windows.Forms.Label lbDepositMax;
        private System.Windows.Forms.Label lbDepositMin;
        private System.Windows.Forms.GroupBox gbWithDrawing;
        private System.Windows.Forms.TextBox tbDrawingMax;
        private System.Windows.Forms.TextBox tbWithDrawingMin;
        private System.Windows.Forms.Label lbWithDrawingMax;
        private System.Windows.Forms.Label lbWithDrawingMin;
        private System.Windows.Forms.GroupBox gbTransferFees;
        private System.Windows.Forms.TextBox tbTransferFees;
        private System.Windows.Forms.Label lbTransferFeesValue;
        private System.Windows.Forms.TextBox tbTransferFeesMax;
        private System.Windows.Forms.TextBox tbTransferFeesMin;
        private System.Windows.Forms.Label lbTransferFeesMax;
        private System.Windows.Forms.Label lbTransferFeesMin;
        private System.Windows.Forms.GroupBox gbWithdrawFees;
        private System.Windows.Forms.TextBox tbWithdrawFees;
        private System.Windows.Forms.Label lbWithdrawFeesValue;
        private System.Windows.Forms.TextBox tbWithdrawFeesMax;
        private System.Windows.Forms.TextBox tbWithdrawFeesMin;
        private System.Windows.Forms.Label lbWithdrawFeesMax;
        private System.Windows.Forms.Label lbWithdrawFeesMin;
        private System.Windows.Forms.RadioButton rbFlatWithdrawFees;
        private System.Windows.Forms.Label lbWithdrawFeesType;
        private System.Windows.Forms.RadioButton rbRateWithdrawFees;
        private System.Windows.Forms.Label lbYearlyInterestRate;
        private System.Windows.Forms.Label lbYearlyInterestRateMax;
        private System.Windows.Forms.Label lbYearlyInterestRateMin;
        private System.Windows.Forms.RadioButton rbRateTransferFees;
        private System.Windows.Forms.RadioButton rbFlatTransferFees;
        private System.Windows.Forms.Label lbTransferFeesType;
        private System.Windows.Forms.GroupBox gtDepositFees;
        private System.Windows.Forms.TextBox tbDepositFees;
        private System.Windows.Forms.Label lbDepositFees;
        private System.Windows.Forms.TextBox tbDepositFeesMax;
        private System.Windows.Forms.TextBox tbDepositFeesMin;
        private System.Windows.Forms.Label lbDepositFeesMax;
        private System.Windows.Forms.Label lbDepositFeesMin;
        private System.Windows.Forms.TabPage tabPageManagement;
        private System.Windows.Forms.GroupBox gbEntryFees;
        private System.Windows.Forms.TextBox tbEntryFeesValue;
        private System.Windows.Forms.Label lbEntryFeesValue;
        private System.Windows.Forms.TextBox tbEntryFeesMax;
        private System.Windows.Forms.TextBox tbEntryFeesMin;
        private System.Windows.Forms.Label lbEntryFeesMax;
        private System.Windows.Forms.Label lbEntryFeesMin;
        private System.Windows.Forms.TabPage tabPageOverdraft;
        private System.Windows.Forms.GroupBox gtCloseFees;
        private System.Windows.Forms.TextBox tbCloseFees;
        private System.Windows.Forms.Label lbCloseFees;
        private System.Windows.Forms.TextBox tbCloseFeesMax;
        private System.Windows.Forms.TextBox tbCloseFeesMin;
        private System.Windows.Forms.Label lbCloseFeesMax;
        private System.Windows.Forms.Label lbCloseFeesMin;
        private System.Windows.Forms.RadioButton rbRateDepositFees;
        private System.Windows.Forms.RadioButton rbFlatDepositFees;
        private System.Windows.Forms.Label lbDepositFeesType;
        private System.Windows.Forms.RadioButton rbRateCloseFees;
        private System.Windows.Forms.RadioButton rbFlatCloseFees;
        private System.Windows.Forms.Label lbCloseFeesType;
        private System.Windows.Forms.RadioButton rbRateEntryFees;
        private System.Windows.Forms.RadioButton rbFlatEntryFees;
        private System.Windows.Forms.Label lbEntryFeesType;
        private System.Windows.Forms.GroupBox gtManagementFees;
        private System.Windows.Forms.RadioButton rbRateManagementFees;
        private System.Windows.Forms.RadioButton rbFlatManagementFees;
        private System.Windows.Forms.Label lbManagementFeesType;
        private System.Windows.Forms.TextBox tbManagementFees;
        private System.Windows.Forms.Label lbManagementFees;
        private System.Windows.Forms.TextBox tbManagementFeesMax;
        private System.Windows.Forms.TextBox tbManagementFeesMin;
        private System.Windows.Forms.Label lbManagementFeesMax;
        private System.Windows.Forms.Label lbManagementFeesMin;
        private System.Windows.Forms.CheckBox clientTypeCorpCheckBox;
        private System.Windows.Forms.CheckBox clientTypeIndivCheckBox;
        private System.Windows.Forms.CheckBox clientTypeGroupCheckBox;
        private System.Windows.Forms.CheckBox clientTypeAllCheckBox;
        private System.Windows.Forms.ComboBox cbManagementFeeFreq;
        private System.Windows.Forms.CheckBox clientTypeVillageCheckBox;
        private System.Windows.Forms.GroupBox gtOverdraftFees;
        private System.Windows.Forms.RadioButton rbRateOverdraftFees;
        private System.Windows.Forms.RadioButton rbFlatOverdraftFees;
        private System.Windows.Forms.Label lbOverdraftFeesType;
        private System.Windows.Forms.TextBox tbOverdraftFees;
        private System.Windows.Forms.Label lbOverdraftFeesValue;
        private System.Windows.Forms.TextBox tbOverdraftFeesMax;
        private System.Windows.Forms.TextBox tbOverdraftFeesMin;
        private System.Windows.Forms.Label lbOverdraftFeesMax;
        private System.Windows.Forms.Label lbOverdraftFeesMin;
        private System.Windows.Forms.GroupBox gtAgioFees;
        private System.Windows.Forms.TextBox tbAgioFees;
        private System.Windows.Forms.Label lbAgioFeesValue;
        private System.Windows.Forms.TextBox tbAgioFeesMax;
        private System.Windows.Forms.TextBox tbAgioFeesMin;
        private System.Windows.Forms.Label lbAgioFeesMax;
        private System.Windows.Forms.Label lbAgioFeesMin;
        private System.Windows.Forms.ComboBox cbAgioFeesFreq;
        private System.Windows.Forms.RadioButton rbRateAgioFees;
        private System.Windows.Forms.RadioButton rbFlatAgioFees;
        private System.Windows.Forms.Label lbAgioFeesType;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTransactionIn;
        private System.Windows.Forms.GroupBox gbReopenFees;
        private System.Windows.Forms.RadioButton rbRateReopenFees;
        private System.Windows.Forms.RadioButton rbFlatReopenFees;
        private System.Windows.Forms.Label lbReopenFeesType;
        private System.Windows.Forms.TextBox tbReopenFeesValue;
        private System.Windows.Forms.Label lbReopenFeesValue;
        private System.Windows.Forms.TextBox tbReopenFeesMax;
        private System.Windows.Forms.TextBox tbReopenFeesMin;
        private System.Windows.Forms.Label lbReopenFeesMax;
        private System.Windows.Forms.Label lbReopenFeesMin;
        private System.Windows.Forms.ComboBox cbTransferType;
        private System.Windows.Forms.TabPage tabPageTermDeposit;
        private System.Windows.Forms.CheckBox checkBoxUseTermDeposit;
        private System.Windows.Forms.Panel termDepositPanel;
        private System.Windows.Forms.GroupBox gbNumberOfPeriods;
        private System.Windows.Forms.Label lblMaxOfTermDepositPeriods;
        private System.Windows.Forms.Label lblMinOfTermDepositPeriods;
        private System.Windows.Forms.GroupBox gpPostingFrequency;
        private System.Windows.Forms.TextBox tbTermDepositPeriodMax;
        private System.Windows.Forms.TextBox tbTermDepositPeriodMin;
        private System.Windows.Forms.ComboBox cbxPostingfrequency;
    }
}
