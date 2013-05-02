using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Contracts
{
    partial class VillageAddSavingsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VillageAddSavingsForm));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.udInitialAmount = new System.Windows.Forms.NumericUpDown();
            this.udInterestRate = new System.Windows.Forms.NumericUpDown();
            this.udEntryFees = new System.Windows.Forms.NumericUpDown();
            this.udWithdrawFees = new System.Windows.Forms.NumericUpDown();
            this.udTransferFees = new System.Windows.Forms.NumericUpDown();
            this.cbLoan = new System.Windows.Forms.ComboBox();
            this.lvMembers = new OpenCBS.GUI.UserControl.ListViewEx();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chPassport = new System.Windows.Forms.ColumnHeader();
            this.chInitialAmount = new System.Windows.Forms.ColumnHeader();
            this.chCurrency = new System.Windows.Forms.ColumnHeader();
            this.chInterest = new System.Windows.Forms.ColumnHeader();
            this.chEntryFees = new System.Windows.Forms.ColumnHeader();
            this.chWithdrawFees = new System.Windows.Forms.ColumnHeader();
            this.chTransferFees = new System.Windows.Forms.ColumnHeader();
            this.colIbtFees = new System.Windows.Forms.ColumnHeader();
            this.chDepositFees = new System.Windows.Forms.ColumnHeader();
            this.chChequeDepositFees = new System.Windows.Forms.ColumnHeader();
            this.chCloseFees = new System.Windows.Forms.ColumnHeader();
            this.chManagementFees = new System.Windows.Forms.ColumnHeader();
            this.chOverdraftFees = new System.Windows.Forms.ColumnHeader();
            this.chAgioFees = new System.Windows.Forms.ColumnHeader();
            this.chReopenFees = new System.Windows.Forms.ColumnHeader();
            this.chLoan = new System.Windows.Forms.ColumnHeader();
            this.udDepositFees = new System.Windows.Forms.NumericUpDown();
            this.udCloseFees = new System.Windows.Forms.NumericUpDown();
            this.udManagementFees = new System.Windows.Forms.NumericUpDown();
            this.udOverdraftFees = new System.Windows.Forms.NumericUpDown();
            this.udAgioFees = new System.Windows.Forms.NumericUpDown();
            this.udChequeDepositFees = new System.Windows.Forms.NumericUpDown();
            this.udReopenFees = new System.Windows.Forms.NumericUpDown();
            this.nudIbtFees = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udInitialAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udInterestRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udEntryFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWithdrawFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTransferFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDepositFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCloseFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udManagementFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udOverdraftFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAgioFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udChequeDepositFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udReopenFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIbtFees)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.AccessibleDescription = null;
            this.pnlButtons.AccessibleName = null;
            resources.ApplyResources(this.pnlButtons, "pnlButtons");
            this.pnlButtons.BackgroundImage = null;
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Font = null;
            this.pnlButtons.Name = "pnlButtons";
            // 
            // udInitialAmount
            // 
            this.udInitialAmount.AccessibleDescription = null;
            this.udInitialAmount.AccessibleName = null;
            resources.ApplyResources(this.udInitialAmount, "udInitialAmount");
            this.udInitialAmount.DecimalPlaces = 2;
            this.udInitialAmount.Font = null;
            this.udInitialAmount.Name = "udInitialAmount";
            // 
            // udInterestRate
            // 
            this.udInterestRate.AccessibleDescription = null;
            this.udInterestRate.AccessibleName = null;
            resources.ApplyResources(this.udInterestRate, "udInterestRate");
            this.udInterestRate.DecimalPlaces = 2;
            this.udInterestRate.Font = null;
            this.udInterestRate.Name = "udInterestRate";
            // 
            // udEntryFees
            // 
            this.udEntryFees.AccessibleDescription = null;
            this.udEntryFees.AccessibleName = null;
            resources.ApplyResources(this.udEntryFees, "udEntryFees");
            this.udEntryFees.DecimalPlaces = 2;
            this.udEntryFees.Font = null;
            this.udEntryFees.Name = "udEntryFees";
            // 
            // udWithdrawFees
            // 
            this.udWithdrawFees.AccessibleDescription = null;
            this.udWithdrawFees.AccessibleName = null;
            resources.ApplyResources(this.udWithdrawFees, "udWithdrawFees");
            this.udWithdrawFees.DecimalPlaces = 2;
            this.udWithdrawFees.Font = null;
            this.udWithdrawFees.Name = "udWithdrawFees";
            // 
            // udTransferFees
            // 
            this.udTransferFees.AccessibleDescription = null;
            this.udTransferFees.AccessibleName = null;
            resources.ApplyResources(this.udTransferFees, "udTransferFees");
            this.udTransferFees.DecimalPlaces = 2;
            this.udTransferFees.Font = null;
            this.udTransferFees.Name = "udTransferFees";
            // 
            // cbLoan
            // 
            this.cbLoan.AccessibleDescription = null;
            this.cbLoan.AccessibleName = null;
            resources.ApplyResources(this.cbLoan, "cbLoan");
            this.cbLoan.BackgroundImage = null;
            this.cbLoan.Font = null;
            this.cbLoan.FormattingEnabled = true;
            this.cbLoan.Items.AddRange(new object[] {
            resources.GetString("cbLoan.Items"),
            resources.GetString("cbLoan.Items1")});
            this.cbLoan.Name = "cbLoan";
            // 
            // lvMembers
            // 
            this.lvMembers.AccessibleDescription = null;
            this.lvMembers.AccessibleName = null;
            resources.ApplyResources(this.lvMembers, "lvMembers");
            this.lvMembers.BackgroundImage = null;
            this.lvMembers.CheckBoxes = true;
            this.lvMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chPassport,
            this.chInitialAmount,
            this.chCurrency,
            this.chInterest,
            this.chEntryFees,
            this.chWithdrawFees,
            this.chTransferFees,
            this.colIbtFees,
            this.chDepositFees,
            this.chChequeDepositFees,
            this.chCloseFees,
            this.chManagementFees,
            this.chOverdraftFees,
            this.chAgioFees,
            this.chReopenFees,
            this.chLoan});
            this.lvMembers.DoubleClickActivation = false;
            this.lvMembers.FullRowSelect = true;
            this.lvMembers.GridLines = true;
            this.lvMembers.Name = "lvMembers";
            this.lvMembers.UseCompatibleStateImageBehavior = false;
            this.lvMembers.View = System.Windows.Forms.View.Details;
            this.lvMembers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvMembers_ItemChecked);
            this.lvMembers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvMembers_ItemCheck);
            this.lvMembers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvMembers_MouseDown);
            // 
            // chName
            // 
            resources.ApplyResources(this.chName, "chName");
            // 
            // chPassport
            // 
            resources.ApplyResources(this.chPassport, "chPassport");
            // 
            // chInitialAmount
            // 
            resources.ApplyResources(this.chInitialAmount, "chInitialAmount");
            // 
            // chCurrency
            // 
            resources.ApplyResources(this.chCurrency, "chCurrency");
            // 
            // chInterest
            // 
            resources.ApplyResources(this.chInterest, "chInterest");
            // 
            // chEntryFees
            // 
            resources.ApplyResources(this.chEntryFees, "chEntryFees");
            // 
            // chWithdrawFees
            // 
            resources.ApplyResources(this.chWithdrawFees, "chWithdrawFees");
            // 
            // chTransferFees
            // 
            resources.ApplyResources(this.chTransferFees, "chTransferFees");
            // 
            // colIbtFees
            // 
            resources.ApplyResources(this.colIbtFees, "colIbtFees");
            // 
            // chDepositFees
            // 
            resources.ApplyResources(this.chDepositFees, "chDepositFees");
            // 
            // chChequeDepositFees
            // 
            resources.ApplyResources(this.chChequeDepositFees, "chChequeDepositFees");
            // 
            // chCloseFees
            // 
            resources.ApplyResources(this.chCloseFees, "chCloseFees");
            // 
            // chManagementFees
            // 
            resources.ApplyResources(this.chManagementFees, "chManagementFees");
            // 
            // chOverdraftFees
            // 
            resources.ApplyResources(this.chOverdraftFees, "chOverdraftFees");
            // 
            // chAgioFees
            // 
            resources.ApplyResources(this.chAgioFees, "chAgioFees");
            // 
            // chReopenFees
            // 
            resources.ApplyResources(this.chReopenFees, "chReopenFees");
            // 
            // chLoan
            // 
            resources.ApplyResources(this.chLoan, "chLoan");
            // 
            // udDepositFees
            // 
            this.udDepositFees.AccessibleDescription = null;
            this.udDepositFees.AccessibleName = null;
            resources.ApplyResources(this.udDepositFees, "udDepositFees");
            this.udDepositFees.DecimalPlaces = 2;
            this.udDepositFees.Font = null;
            this.udDepositFees.Name = "udDepositFees";
            // 
            // udCloseFees
            // 
            this.udCloseFees.AccessibleDescription = null;
            this.udCloseFees.AccessibleName = null;
            resources.ApplyResources(this.udCloseFees, "udCloseFees");
            this.udCloseFees.DecimalPlaces = 2;
            this.udCloseFees.Font = null;
            this.udCloseFees.Name = "udCloseFees";
            // 
            // udManagementFees
            // 
            this.udManagementFees.AccessibleDescription = null;
            this.udManagementFees.AccessibleName = null;
            resources.ApplyResources(this.udManagementFees, "udManagementFees");
            this.udManagementFees.DecimalPlaces = 2;
            this.udManagementFees.Font = null;
            this.udManagementFees.Name = "udManagementFees";
            // 
            // udOverdraftFees
            // 
            this.udOverdraftFees.AccessibleDescription = null;
            this.udOverdraftFees.AccessibleName = null;
            resources.ApplyResources(this.udOverdraftFees, "udOverdraftFees");
            this.udOverdraftFees.DecimalPlaces = 2;
            this.udOverdraftFees.Font = null;
            this.udOverdraftFees.Name = "udOverdraftFees";
            // 
            // udAgioFees
            // 
            this.udAgioFees.AccessibleDescription = null;
            this.udAgioFees.AccessibleName = null;
            resources.ApplyResources(this.udAgioFees, "udAgioFees");
            this.udAgioFees.DecimalPlaces = 2;
            this.udAgioFees.Font = null;
            this.udAgioFees.Name = "udAgioFees";
            // 
            // udChequeDepositFees
            // 
            this.udChequeDepositFees.AccessibleDescription = null;
            this.udChequeDepositFees.AccessibleName = null;
            resources.ApplyResources(this.udChequeDepositFees, "udChequeDepositFees");
            this.udChequeDepositFees.DecimalPlaces = 2;
            this.udChequeDepositFees.Font = null;
            this.udChequeDepositFees.Name = "udChequeDepositFees";
            // 
            // udReopenFees
            // 
            this.udReopenFees.AccessibleDescription = null;
            this.udReopenFees.AccessibleName = null;
            resources.ApplyResources(this.udReopenFees, "udReopenFees");
            this.udReopenFees.DecimalPlaces = 2;
            this.udReopenFees.Font = null;
            this.udReopenFees.Name = "udReopenFees";
            // 
            // nudIbtFees
            // 
            this.nudIbtFees.AccessibleDescription = null;
            this.nudIbtFees.AccessibleName = null;
            resources.ApplyResources(this.nudIbtFees, "nudIbtFees");
            this.nudIbtFees.DecimalPlaces = 2;
            this.nudIbtFees.Font = null;
            this.nudIbtFees.Name = "nudIbtFees";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleDescription = null;
            this.btnSave.AccessibleName = null;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // VillageAddSavingsForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudIbtFees);
            this.Controls.Add(this.udReopenFees);
            this.Controls.Add(this.udChequeDepositFees);
            this.Controls.Add(this.udAgioFees);
            this.Controls.Add(this.udOverdraftFees);
            this.Controls.Add(this.udManagementFees);
            this.Controls.Add(this.udCloseFees);
            this.Controls.Add(this.udDepositFees);
            this.Controls.Add(this.cbLoan);
            this.Controls.Add(this.udTransferFees);
            this.Controls.Add(this.udWithdrawFees);
            this.Controls.Add(this.udEntryFees);
            this.Controls.Add(this.udInterestRate);
            this.Controls.Add(this.udInitialAmount);
            this.Controls.Add(this.lvMembers);
            this.Controls.Add(this.pnlButtons);
            this.Font = null;
            this.Name = "VillageAddSavingsForm";
            this.Load += new System.EventHandler(this.VillageAddSavingsForm_Load);
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udInitialAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udInterestRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udEntryFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWithdrawFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTransferFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDepositFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCloseFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udManagementFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udOverdraftFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAgioFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udChequeDepositFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udReopenFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIbtFees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private OpenCBS.GUI.UserControl.ListViewEx lvMembers;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chPassport;
        private System.Windows.Forms.ColumnHeader chInitialAmount;
        private System.Windows.Forms.ColumnHeader chInterest;
        private System.Windows.Forms.ColumnHeader chEntryFees;
        private System.Windows.Forms.NumericUpDown udInitialAmount;
        private System.Windows.Forms.ColumnHeader chWithdrawFees;
        private System.Windows.Forms.NumericUpDown udInterestRate;
        private System.Windows.Forms.ColumnHeader chTransferFees;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown udEntryFees;
        private System.Windows.Forms.NumericUpDown udWithdrawFees;
        private System.Windows.Forms.NumericUpDown udTransferFees;
        private System.Windows.Forms.ColumnHeader chLoan;
        private System.Windows.Forms.ComboBox cbLoan;
        private System.Windows.Forms.NumericUpDown udDepositFees;
        private System.Windows.Forms.NumericUpDown udCloseFees;
        private System.Windows.Forms.NumericUpDown udManagementFees;
        private System.Windows.Forms.NumericUpDown udOverdraftFees;
        private System.Windows.Forms.NumericUpDown udAgioFees;
        private System.Windows.Forms.ColumnHeader chDepositFees;
        private System.Windows.Forms.ColumnHeader chCloseFees;
        private System.Windows.Forms.ColumnHeader chManagementFees;
        private System.Windows.Forms.ColumnHeader chOverdraftFees;
        private System.Windows.Forms.ColumnHeader chAgioFees;
        private System.Windows.Forms.ColumnHeader chChequeDepositFees;
        private System.Windows.Forms.NumericUpDown udChequeDepositFees;
        private System.Windows.Forms.NumericUpDown udReopenFees;
        private System.Windows.Forms.ColumnHeader chReopenFees;
        private System.Windows.Forms.ColumnHeader colIbtFees;
        private System.Windows.Forms.NumericUpDown nudIbtFees;
        private System.Windows.Forms.ColumnHeader chCurrency;
    }
}