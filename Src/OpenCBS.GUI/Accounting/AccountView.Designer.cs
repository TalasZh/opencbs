using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Accounting
{
    partial class AccountView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountView));
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.spconDesk = new System.Windows.Forms.SplitContainer();
            this.lvBooking = new System.Windows.Forms.ListView();
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDebit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCredit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAmountEC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPurpose = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbBalance = new System.Windows.Forms.GroupBox();
            this.lblAccountBalance = new System.Windows.Forms.Label();
            this.btnRefrech = new System.Windows.Forms.Button();
            this.cbAccounts = new System.Windows.Forms.ComboBox();
            this.cmbCurrencies = new System.Windows.Forms.ComboBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.cmbBranches = new System.Windows.Forms.ComboBox();
            this.cmbDisplayType = new System.Windows.Forms.ComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.dateTimePickerBeginDate = new System.Windows.Forms.DateTimePicker();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblBeginDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.spconDesk.Panel1.SuspendLayout();
            this.spconDesk.Panel2.SuspendLayout();
            this.spconDesk.SuspendLayout();
            this.gbBalance.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer4
            // 
            resources.ApplyResources(this.splitContainer4, "splitContainer4");
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btnClose);
            this.splitContainer4.Panel1.Controls.Add(this.lblTitle);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.spconDesk);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this._buttonExit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(152)))));
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // spconDesk
            // 
            resources.ApplyResources(this.spconDesk, "spconDesk");
            this.spconDesk.Name = "spconDesk";
            // 
            // spconDesk.Panel1
            // 
            this.spconDesk.Panel1.Controls.Add(this.lvBooking);
            this.spconDesk.Panel1.Controls.Add(this.gbBalance);
            // 
            // spconDesk.Panel2
            // 
            this.spconDesk.Panel2.Controls.Add(this.btnRefrech);
            this.spconDesk.Panel2.Controls.Add(this.cbAccounts);
            this.spconDesk.Panel2.Controls.Add(this.cmbCurrencies);
            this.spconDesk.Panel2.Controls.Add(this.lblCurrency);
            this.spconDesk.Panel2.Controls.Add(this.lblAccount);
            this.spconDesk.Panel2.Controls.Add(this.cmbBranches);
            this.spconDesk.Panel2.Controls.Add(this.cmbDisplayType);
            this.spconDesk.Panel2.Controls.Add(this.lblBranch);
            this.spconDesk.Panel2.Controls.Add(this.dateTimePickerBeginDate);
            this.spconDesk.Panel2.Controls.Add(this.lblDisplay);
            this.spconDesk.Panel2.Controls.Add(this.dateTimePickerEndDate);
            this.spconDesk.Panel2.Controls.Add(this.lblBeginDate);
            this.spconDesk.Panel2.Controls.Add(this.lblEndDate);
            // 
            // lvBooking
            // 
            this.lvBooking.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderId,
            this.columnHeaderDebit,
            this.columnHeaderCredit,
            this.columnHeaderRate,
            this.columnHeaderAmountEC,
            this.columnHeaderPurpose});
            resources.ApplyResources(this.lvBooking, "lvBooking");
            this.lvBooking.FullRowSelect = true;
            this.lvBooking.GridLines = true;
            this.lvBooking.Name = "lvBooking";
            this.lvBooking.UseCompatibleStateImageBehavior = false;
            this.lvBooking.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderId
            // 
            resources.ApplyResources(this.columnHeaderId, "columnHeaderId");
            // 
            // columnHeaderDebit
            // 
            resources.ApplyResources(this.columnHeaderDebit, "columnHeaderDebit");
            // 
            // columnHeaderCredit
            // 
            resources.ApplyResources(this.columnHeaderCredit, "columnHeaderCredit");
            // 
            // columnHeaderRate
            // 
            resources.ApplyResources(this.columnHeaderRate, "columnHeaderRate");
            // 
            // columnHeaderAmountEC
            // 
            resources.ApplyResources(this.columnHeaderAmountEC, "columnHeaderAmountEC");
            // 
            // columnHeaderPurpose
            // 
            resources.ApplyResources(this.columnHeaderPurpose, "columnHeaderPurpose");
            // 
            // gbBalance
            // 
            this.gbBalance.Controls.Add(this.lblAccountBalance);
            resources.ApplyResources(this.gbBalance, "gbBalance");
            this.gbBalance.Name = "gbBalance";
            this.gbBalance.TabStop = false;
            // 
            // lblAccountBalance
            // 
            resources.ApplyResources(this.lblAccountBalance, "lblAccountBalance");
            this.lblAccountBalance.Name = "lblAccountBalance";
            // 
            // btnRefrech
            // 
            resources.ApplyResources(this.btnRefrech, "btnRefrech");
            this.btnRefrech.Name = "btnRefrech";
            this.btnRefrech.Click += new System.EventHandler(this.buttonRefrech_Click);
            // 
            // cbAccounts
            // 
            this.cbAccounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAccounts.FormattingEnabled = true;
            resources.ApplyResources(this.cbAccounts, "cbAccounts");
            this.cbAccounts.Name = "cbAccounts";
            this.cbAccounts.SelectionChangeCommitted += new System.EventHandler(this.comboBoxSelectAccount_SelectionChangeCommitted);
            // 
            // cmbCurrencies
            // 
            this.cmbCurrencies.DisplayMember = "Currency.Name";
            this.cmbCurrencies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrencies.FormattingEnabled = true;
            resources.ApplyResources(this.cmbCurrencies, "cmbCurrencies");
            this.cmbCurrencies.Name = "cmbCurrencies";
            this.cmbCurrencies.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurrencies_SelectedIndexChanged);
            // 
            // lblCurrency
            // 
            resources.ApplyResources(this.lblCurrency, "lblCurrency");
            this.lblCurrency.Name = "lblCurrency";
            // 
            // lblAccount
            // 
            resources.ApplyResources(this.lblAccount, "lblAccount");
            this.lblAccount.Name = "lblAccount";
            // 
            // cmbBranches
            // 
            this.cmbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranches.FormattingEnabled = true;
            resources.ApplyResources(this.cmbBranches, "cmbBranches");
            this.cmbBranches.Name = "cmbBranches";
            this.cmbBranches.SelectedIndexChanged += new System.EventHandler(this.cmbBranches_SelectedIndexChanged);
            // 
            // cmbDisplayType
            // 
            this.cmbDisplayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisplayType.FormattingEnabled = true;
            resources.ApplyResources(this.cmbDisplayType, "cmbDisplayType");
            this.cmbDisplayType.Name = "cmbDisplayType";
            this.cmbDisplayType.SelectedIndexChanged += new System.EventHandler(this.cmbDisplayType_SelectedIndexChanged);
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            // 
            // dateTimePickerBeginDate
            // 
            this.dateTimePickerBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dateTimePickerBeginDate, "dateTimePickerBeginDate");
            this.dateTimePickerBeginDate.Name = "dateTimePickerBeginDate";
            this.dateTimePickerBeginDate.ValueChanged += new System.EventHandler(this.dateTimePickerBeginDate_ValueChanged);
            // 
            // lblDisplay
            // 
            resources.ApplyResources(this.lblDisplay, "lblDisplay");
            this.lblDisplay.Name = "lblDisplay";
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dateTimePickerEndDate, "dateTimePickerEndDate");
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.ValueChanged += new System.EventHandler(this.dateTimePickerEndDate_ValueChanged);
            // 
            // lblBeginDate
            // 
            resources.ApplyResources(this.lblBeginDate, "lblBeginDate");
            this.lblBeginDate.Name = "lblBeginDate";
            // 
            // lblEndDate
            // 
            resources.ApplyResources(this.lblEndDate, "lblEndDate");
            this.lblEndDate.Name = "lblEndDate";
            // 
            // AccountView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer4);
            this.Name = "AccountView";
            this.Load += new System.EventHandler(this.AccountView_Load);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.spconDesk.Panel1.ResumeLayout(false);
            this.spconDesk.Panel2.ResumeLayout(false);
            this.spconDesk.Panel2.PerformLayout();
            this.spconDesk.ResumeLayout(false);
            this.gbBalance.ResumeLayout(false);
            this.gbBalance.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SplitContainer spconDesk;
        private System.Windows.Forms.GroupBox gbBalance;
        private System.Windows.Forms.Label lblAccountBalance;
        private System.Windows.Forms.Button btnRefrech;
        private System.Windows.Forms.ComboBox cbAccounts;
        private System.Windows.Forms.ComboBox cmbCurrencies;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.ComboBox cmbDisplayType;
        private System.Windows.Forms.DateTimePicker dateTimePickerBeginDate;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.Label lblBeginDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.ListView lvBooking;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderDebit;
        private System.Windows.Forms.ColumnHeader columnHeaderCredit;
        private System.Windows.Forms.ColumnHeader columnHeaderRate;
        private System.Windows.Forms.ColumnHeader columnHeaderAmountEC;
        private System.Windows.Forms.ColumnHeader columnHeaderPurpose;
        private System.Windows.Forms.ComboBox cmbBranches;
        private System.Windows.Forms.Label lblBranch;
    }
}