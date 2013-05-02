namespace OpenCBS.GUI.Contracts
{
    using OpenCBS.GUI.UserControl;

    partial class SavingsOperationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SavingsOperationForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbxMainWindow = new System.Windows.Forms.GroupBox();
            this.nudTotalAmount = new System.Windows.Forms.NumericUpDown();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.rbxCredit = new System.Windows.Forms.RadioButton();
            this.rbxDebit = new System.Windows.Forms.RadioButton();
            this.lblAmountFeesMinMax = new System.Windows.Forms.Label();
            this.updAmountFees = new System.Windows.Forms.NumericUpDown();
            this.lblTotalSavingCurrency = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblFees = new System.Windows.Forms.Label();
            this.lblSavingCurrencyFees = new System.Windows.Forms.Label();
            this.lbAmountMinMaxCurrencyPivot = new System.Windows.Forms.Label();
            this.lblSavingCurrency = new System.Windows.Forms.Label();
            this.lbAmountMinMax = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.tbxSavingCode = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.plTransfer = new System.Windows.Forms.Panel();
            this.lblInterBranch = new System.Windows.Forms.Label();
            this.tbTargetAccount = new System.Windows.Forms.TextBox();
            this.btnSearchContract = new System.Windows.Forms.Button();
            this.lbTargetSavings = new System.Windows.Forms.Label();
            this.lblClientName = new System.Windows.Forms.Label();
            this.cbBookings = new System.Windows.Forms.ComboBox();
            this.pnlSavingPending = new System.Windows.Forms.Panel();
            this.cbSavingsMethod = new System.Windows.Forms.ComboBox();
            this.cbxPending = new System.Windows.Forms.CheckBox();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.gbxMainWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updAmountFees)).BeginInit();
            this.plTransfer.SuspendLayout();
            this.pnlSavingPending.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            //
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.BSaveClick);
            // 
            // btnCancel
            //
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // gbxMainWindow
            //
            resources.ApplyResources(this.gbxMainWindow, "gbxMainWindow");
            this.gbxMainWindow.Controls.Add(this.nudTotalAmount);
            this.gbxMainWindow.Controls.Add(this.nudAmount);
            this.gbxMainWindow.Controls.Add(this.rbxCredit);
            this.gbxMainWindow.Controls.Add(this.rbxDebit);
            this.gbxMainWindow.Controls.Add(this.lblAmountFeesMinMax);
            this.gbxMainWindow.Controls.Add(this.updAmountFees);
            this.gbxMainWindow.Controls.Add(this.lblTotalSavingCurrency);
            this.gbxMainWindow.Controls.Add(this.lblTotalAmount);
            this.gbxMainWindow.Controls.Add(this.btnSave);
            this.gbxMainWindow.Controls.Add(this.btnCancel);
            this.gbxMainWindow.Controls.Add(this.lblFees);
            this.gbxMainWindow.Controls.Add(this.lblSavingCurrencyFees);
            this.gbxMainWindow.Controls.Add(this.lbAmountMinMaxCurrencyPivot);
            this.gbxMainWindow.Controls.Add(this.lblSavingCurrency);
            this.gbxMainWindow.Controls.Add(this.lbAmountMinMax);
            this.gbxMainWindow.Controls.Add(this.dtpDate);
            this.gbxMainWindow.Controls.Add(this.lblDate);
            this.gbxMainWindow.Controls.Add(this.lblAmount);
            this.gbxMainWindow.Controls.Add(this.tbxSavingCode);
            this.gbxMainWindow.Controls.Add(this.lblDescription);
            this.gbxMainWindow.Controls.Add(this.plTransfer);
            this.gbxMainWindow.Controls.Add(this.pnlSavingPending);
            this.gbxMainWindow.Name = "gbxMainWindow";
            this.gbxMainWindow.TabStop = false;
            // 
            // nudTotalAmount
            // 
            this.nudTotalAmount.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            resources.ApplyResources(this.nudTotalAmount, "nudTotalAmount");
            this.nudTotalAmount.Name = "nudTotalAmount";
            this.nudTotalAmount.ReadOnly = true;
            // 
            // nudAmount
            // 
            resources.ApplyResources(this.nudAmount, "nudAmount");
            this.nudAmount.Name = "nudAmount";
            this.nudAmount.ValueChanged += new System.EventHandler(this.nudAmount_ValueChanged);
            // 
            // rbxCredit
            // 
            resources.ApplyResources(this.rbxCredit, "rbxCredit");
            this.rbxCredit.Name = "rbxCredit";
            // 
            // rbxDebit
            // 
            resources.ApplyResources(this.rbxDebit, "rbxDebit");
            this.rbxDebit.Checked = true;
            this.rbxDebit.Name = "rbxDebit";
            this.rbxDebit.TabStop = true;
            // 
            // lblAmountFeesMinMax
            // 
            this.lblAmountFeesMinMax.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblAmountFeesMinMax, "lblAmountFeesMinMax");
            this.lblAmountFeesMinMax.Name = "lblAmountFeesMinMax";
            // 
            // updAmountFees
            // 
            resources.ApplyResources(this.updAmountFees, "updAmountFees");
            this.updAmountFees.Name = "updAmountFees";
            this.updAmountFees.ValueChanged += new System.EventHandler(this.nudAmount_ValueChanged);
            // 
            // lblTotalSavingCurrency
            // 
            resources.ApplyResources(this.lblTotalSavingCurrency, "lblTotalSavingCurrency");
            this.lblTotalSavingCurrency.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalSavingCurrency.Name = "lblTotalSavingCurrency";
            // 
            // lblTotalAmount
            // 
            resources.ApplyResources(this.lblTotalAmount, "lblTotalAmount");
            this.lblTotalAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalAmount.Name = "lblTotalAmount";
            // 
            // lblFees
            // 
            resources.ApplyResources(this.lblFees, "lblFees");
            this.lblFees.BackColor = System.Drawing.Color.Transparent;
            this.lblFees.Name = "lblFees";
            // 
            // lblSavingCurrencyFees
            // 
            resources.ApplyResources(this.lblSavingCurrencyFees, "lblSavingCurrencyFees");
            this.lblSavingCurrencyFees.BackColor = System.Drawing.Color.Transparent;
            this.lblSavingCurrencyFees.Name = "lblSavingCurrencyFees";
            // 
            // lbAmountMinMaxCurrencyPivot
            //
            resources.ApplyResources(this.lbAmountMinMaxCurrencyPivot, "lbAmountMinMaxCurrencyPivot");
            this.lbAmountMinMaxCurrencyPivot.Name = "lbAmountMinMaxCurrencyPivot";
            // 
            // lblSavingCurrency
            // 
            resources.ApplyResources(this.lblSavingCurrency, "lblSavingCurrency");
            this.lblSavingCurrency.BackColor = System.Drawing.Color.Transparent;
            this.lblSavingCurrency.Name = "lblSavingCurrency";
            // 
            // lbAmountMinMax
            //
            resources.ApplyResources(this.lbAmountMinMax, "lbAmountMinMax");
            this.lbAmountMinMax.Name = "lbAmountMinMax";
            // 
            // dtpDate
            // 
            resources.ApplyResources(this.dtpDate, "dtpDate");
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Name = "dtpDate";
            // 
            // lblDate
            // 
            resources.ApplyResources(this.lblDate, "lblDate");
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Name = "lblDate";
            // 
            // lblAmount
            // 
            resources.ApplyResources(this.lblAmount, "lblAmount");
            this.lblAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblAmount.Name = "lblAmount";
            // 
            // tbxSavingCode
            // 
            resources.ApplyResources(this.tbxSavingCode, "tbxSavingCode");
            this.tbxSavingCode.Name = "tbxSavingCode";
            this.tbxSavingCode.TextChanged += new System.EventHandler(this.TbSavingCodeTextChanged);
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Name = "lblDescription";
            // 
            // plTransfer
            // 
            this.plTransfer.Controls.Add(this.lblInterBranch);
            this.plTransfer.Controls.Add(this.tbTargetAccount);
            this.plTransfer.Controls.Add(this.btnSearchContract);
            this.plTransfer.Controls.Add(this.lbTargetSavings);
            this.plTransfer.Controls.Add(this.lblClientName);
            this.plTransfer.Controls.Add(this.cbBookings);
            resources.ApplyResources(this.plTransfer, "plTransfer");
            this.plTransfer.Name = "plTransfer";
            // 
            // lblInterBranch
            // 
            resources.ApplyResources(this.lblInterBranch, "lblInterBranch");
            this.lblInterBranch.Name = "lblInterBranch";
            // 
            // tbTargetAccount
            // 
            resources.ApplyResources(this.tbTargetAccount, "tbTargetAccount");
            this.tbTargetAccount.Name = "tbTargetAccount";
            this.tbTargetAccount.TextChanged += new System.EventHandler(this.TbSavingCodeTextChanged);
            // 
            // btnSearchContract
            //
            resources.ApplyResources(this.btnSearchContract, "btnSearchContract");
            this.btnSearchContract.Name = "btnSearchContract";
            this.btnSearchContract.Click += new System.EventHandler(this.btSearchContract_Click);
            // 
            // lbTargetSavings
            // 
            resources.ApplyResources(this.lbTargetSavings, "lbTargetSavings");
            this.lbTargetSavings.Name = "lbTargetSavings";
            // 
            // lblClientName
            // 
            this.lblClientName.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblClientName, "lblClientName");
            this.lblClientName.Name = "lblClientName";
            // 
            // cbBookings
            // 
            this.cbBookings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBookings.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbBookings.FormattingEnabled = true;
            resources.ApplyResources(this.cbBookings, "cbBookings");
            this.cbBookings.Name = "cbBookings";
            // 
            // pnlSavingPending
            // 
            this.pnlSavingPending.Controls.Add(this.cbSavingsMethod);
            this.pnlSavingPending.Controls.Add(this.cbxPending);
            this.pnlSavingPending.Controls.Add(this.lblPaymentMethod);
            resources.ApplyResources(this.pnlSavingPending, "pnlSavingPending");
            this.pnlSavingPending.Name = "pnlSavingPending";
            // 
            // cbSavingsMethod
            // 
            this.cbSavingsMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSavingsMethod.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbSavingsMethod.FormattingEnabled = true;
            resources.ApplyResources(this.cbSavingsMethod, "cbSavingsMethod");
            this.cbSavingsMethod.Name = "cbSavingsMethod";
            this.cbSavingsMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxSavingsMethod_SelectedIndexChanged);
            // 
            // cbxPending
            // 
            resources.ApplyResources(this.cbxPending, "cbxPending");
            this.cbxPending.Checked = true;
            this.cbxPending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxPending.Name = "cbxPending";
            // 
            // lblPaymentMethod
            // 
            resources.ApplyResources(this.lblPaymentMethod, "lblPaymentMethod");
            this.lblPaymentMethod.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            // 
            // SavingsOperationForm
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.gbxMainWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SavingsOperationForm";
            this.gbxMainWindow.ResumeLayout(false);
            this.gbxMainWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updAmountFees)).EndInit();
            this.plTransfer.ResumeLayout(false);
            this.plTransfer.PerformLayout();
            this.pnlSavingPending.ResumeLayout(false);
            this.pnlSavingPending.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxMainWindow;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox tbxSavingCode;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lbAmountMinMax;
        private System.Windows.Forms.Label lbAmountMinMaxCurrencyPivot;
        private System.Windows.Forms.Label lblSavingCurrency;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.TextBox tbTargetAccount;
        private System.Windows.Forms.Label lbTargetSavings;
        private System.Windows.Forms.Button btnSearchContract;
        private System.Windows.Forms.Panel plTransfer;
        private System.Windows.Forms.Label lblSavingCurrencyFees;
        private System.Windows.Forms.Label lblFees;
        private System.Windows.Forms.ComboBox cbSavingsMethod;
        private System.Windows.Forms.CheckBox cbxPending;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.Panel pnlSavingPending;
        private System.Windows.Forms.Label lblTotalSavingCurrency;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.NumericUpDown updAmountFees;
        private System.Windows.Forms.Label lblAmountFeesMinMax;
        private System.Windows.Forms.RadioButton rbxCredit;
        private System.Windows.Forms.RadioButton rbxDebit;
        private System.Windows.Forms.ComboBox cbBookings;
        private System.Windows.Forms.NumericUpDown nudAmount;
        private System.Windows.Forms.NumericUpDown nudTotalAmount;
        private System.Windows.Forms.Label lblInterBranch;
    }
}