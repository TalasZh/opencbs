using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Contracts
{
    partial class VillageAddLoanForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VillageAddLoanForm));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.tbInterest = new System.Windows.Forms.TextBox();
            this.udGracePeriod = new System.Windows.Forms.NumericUpDown();
            this.udInstallments = new System.Windows.Forms.NumericUpDown();
            this.cbLoanOfficer = new System.Windows.Forms.ComboBox();
            this.tbEntryFee = new System.Windows.Forms.TextBox();
            this.cbFundingLine = new System.Windows.Forms.ComboBox();
            this.cbDonor = new System.Windows.Forms.ComboBox();
            this.lvMembers = new OpenCBS.GUI.UserControl.ListViewEx();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chPassport = new System.Windows.Forms.ColumnHeader();
            this.chAmount = new System.Windows.Forms.ColumnHeader();
            this.chCurrency = new System.Windows.Forms.ColumnHeader();
            this.chInterest = new System.Windows.Forms.ColumnHeader();
            this.chGracePeriod = new System.Windows.Forms.ColumnHeader();
            this.chInstallments = new System.Windows.Forms.ColumnHeader();
            this.chLoanOfficer = new System.Windows.Forms.ColumnHeader();
            this.chCreationDate = new System.Windows.Forms.ColumnHeader();
            this.chFundingLine = new System.Windows.Forms.ColumnHeader();
            this.chAvailableFunds = new System.Windows.Forms.ColumnHeader();
            this.chCompulsorySavings = new System.Windows.Forms.ColumnHeader();
            this.chCompulsoryPercentage = new System.Windows.Forms.ColumnHeader();
            this.cbCompulsorySavings = new System.Windows.Forms.ComboBox();
            this.udCompulsoryPercentage = new System.Windows.Forms.NumericUpDown();
            this.dtCreationDate = new System.Windows.Forms.DateTimePicker();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udGracePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udInstallments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCompulsoryPercentage)).BeginInit();
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
            // tbAmount
            // 
            this.tbAmount.AccessibleDescription = null;
            this.tbAmount.AccessibleName = null;
            resources.ApplyResources(this.tbAmount, "tbAmount");
            this.tbAmount.BackgroundImage = null;
            this.tbAmount.Name = "tbAmount";
            // 
            // tbInterest
            // 
            this.tbInterest.AccessibleDescription = null;
            this.tbInterest.AccessibleName = null;
            resources.ApplyResources(this.tbInterest, "tbInterest");
            this.tbInterest.BackgroundImage = null;
            this.tbInterest.Font = null;
            this.tbInterest.Name = "tbInterest";
            // 
            // udGracePeriod
            // 
            this.udGracePeriod.AccessibleDescription = null;
            this.udGracePeriod.AccessibleName = null;
            resources.ApplyResources(this.udGracePeriod, "udGracePeriod");
            this.udGracePeriod.Font = null;
            this.udGracePeriod.Name = "udGracePeriod";
            // 
            // udInstallments
            // 
            this.udInstallments.AccessibleDescription = null;
            this.udInstallments.AccessibleName = null;
            resources.ApplyResources(this.udInstallments, "udInstallments");
            this.udInstallments.Font = null;
            this.udInstallments.Name = "udInstallments";
            // 
            // cbLoanOfficer
            // 
            this.cbLoanOfficer.AccessibleDescription = null;
            this.cbLoanOfficer.AccessibleName = null;
            resources.ApplyResources(this.cbLoanOfficer, "cbLoanOfficer");
            this.cbLoanOfficer.BackgroundImage = null;
            this.cbLoanOfficer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoanOfficer.Font = null;
            this.cbLoanOfficer.FormattingEnabled = true;
            this.cbLoanOfficer.Name = "cbLoanOfficer";
            // 
            // tbEntryFee
            // 
            this.tbEntryFee.AccessibleDescription = null;
            this.tbEntryFee.AccessibleName = null;
            resources.ApplyResources(this.tbEntryFee, "tbEntryFee");
            this.tbEntryFee.BackgroundImage = null;
            this.tbEntryFee.Font = null;
            this.tbEntryFee.Name = "tbEntryFee";
            // 
            // cbFundingLine
            // 
            this.cbFundingLine.AccessibleDescription = null;
            this.cbFundingLine.AccessibleName = null;
            resources.ApplyResources(this.cbFundingLine, "cbFundingLine");
            this.cbFundingLine.BackgroundImage = null;
            this.cbFundingLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFundingLine.Font = null;
            this.cbFundingLine.FormattingEnabled = true;
            this.cbFundingLine.Name = "cbFundingLine";
            // 
            // cbDonor
            // 
            this.cbDonor.AccessibleDescription = null;
            this.cbDonor.AccessibleName = null;
            resources.ApplyResources(this.cbDonor, "cbDonor");
            this.cbDonor.BackgroundImage = null;
            this.cbDonor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDonor.Font = null;
            this.cbDonor.FormattingEnabled = true;
            this.cbDonor.Name = "cbDonor";
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
            this.chAmount,
            this.chCurrency,
            this.chInterest,
            this.chGracePeriod,
            this.chInstallments,
            this.chLoanOfficer,
            this.chCreationDate,
            this.chFundingLine,
            this.chAvailableFunds,
            this.chCompulsorySavings,
            this.chCompulsoryPercentage});
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
            // chAmount
            // 
            resources.ApplyResources(this.chAmount, "chAmount");
            // 
            // chCurrency
            // 
            resources.ApplyResources(this.chCurrency, "chCurrency");
            // 
            // chInterest
            // 
            resources.ApplyResources(this.chInterest, "chInterest");
            // 
            // chGracePeriod
            // 
            resources.ApplyResources(this.chGracePeriod, "chGracePeriod");
            // 
            // chInstallments
            // 
            resources.ApplyResources(this.chInstallments, "chInstallments");
            // 
            // chLoanOfficer
            // 
            resources.ApplyResources(this.chLoanOfficer, "chLoanOfficer");
            // 
            // chCreationDate
            // 
            resources.ApplyResources(this.chCreationDate, "chCreationDate");
            // 
            // chFundingLine
            // 
            resources.ApplyResources(this.chFundingLine, "chFundingLine");
            // 
            // chAvailableFunds
            // 
            resources.ApplyResources(this.chAvailableFunds, "chAvailableFunds");
            // 
            // chCompulsorySavings
            // 
            resources.ApplyResources(this.chCompulsorySavings, "chCompulsorySavings");
            // 
            // chCompulsoryPercentage
            // 
            resources.ApplyResources(this.chCompulsoryPercentage, "chCompulsoryPercentage");
            // 
            // cbCompulsorySavings
            // 
            this.cbCompulsorySavings.AccessibleDescription = null;
            this.cbCompulsorySavings.AccessibleName = null;
            resources.ApplyResources(this.cbCompulsorySavings, "cbCompulsorySavings");
            this.cbCompulsorySavings.BackgroundImage = null;
            this.cbCompulsorySavings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompulsorySavings.Font = null;
            this.cbCompulsorySavings.FormattingEnabled = true;
            this.cbCompulsorySavings.Name = "cbCompulsorySavings";
            // 
            // udCompulsoryPercentage
            // 
            this.udCompulsoryPercentage.AccessibleDescription = null;
            this.udCompulsoryPercentage.AccessibleName = null;
            resources.ApplyResources(this.udCompulsoryPercentage, "udCompulsoryPercentage");
            this.udCompulsoryPercentage.Font = null;
            this.udCompulsoryPercentage.Name = "udCompulsoryPercentage";
            // 
            // dtCreationDate
            // 
            this.dtCreationDate.AccessibleDescription = null;
            this.dtCreationDate.AccessibleName = null;
            resources.ApplyResources(this.dtCreationDate, "dtCreationDate");
            this.dtCreationDate.BackgroundImage = null;
            this.dtCreationDate.CalendarFont = null;
            this.dtCreationDate.CustomFormat = null;
            this.dtCreationDate.Font = null;
            this.dtCreationDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtCreationDate.Name = "dtCreationDate";
            this.dtCreationDate.ValueChanged += new System.EventHandler(this.dtCreationDate_ValueChanged);
            // 
            // VillageAddLoanForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtCreationDate);
            this.Controls.Add(this.udCompulsoryPercentage);
            this.Controls.Add(this.cbCompulsorySavings);
            this.Controls.Add(this.cbDonor);
            this.Controls.Add(this.cbFundingLine);
            this.Controls.Add(this.tbEntryFee);
            this.Controls.Add(this.cbLoanOfficer);
            this.Controls.Add(this.udInstallments);
            this.Controls.Add(this.udGracePeriod);
            this.Controls.Add(this.tbInterest);
            this.Controls.Add(this.tbAmount);
            this.Controls.Add(this.lvMembers);
            this.Controls.Add(this.pnlButtons);
            this.Font = null;
            this.Name = "VillageAddLoanForm";
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udGracePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udInstallments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCompulsoryPercentage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private OpenCBS.GUI.UserControl.ListViewEx lvMembers;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chPassport;
        private System.Windows.Forms.ColumnHeader chAmount;
        private System.Windows.Forms.TextBox tbAmount;
        private System.Windows.Forms.ColumnHeader chInterest;
        private System.Windows.Forms.TextBox tbInterest;
        private System.Windows.Forms.ColumnHeader chGracePeriod;
        private System.Windows.Forms.NumericUpDown udGracePeriod;
        private System.Windows.Forms.ColumnHeader chInstallments;
        private System.Windows.Forms.NumericUpDown udInstallments;
        private System.Windows.Forms.ColumnHeader chLoanOfficer;
        private System.Windows.Forms.ComboBox cbLoanOfficer;
        private System.Windows.Forms.TextBox tbEntryFee;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbFundingLine;
        private System.Windows.Forms.ColumnHeader chFundingLine;
        private System.Windows.Forms.ComboBox cbDonor;
        private System.Windows.Forms.ColumnHeader chAvailableFunds;
        private System.Windows.Forms.ColumnHeader chCompulsorySavings;
        private System.Windows.Forms.ColumnHeader chCompulsoryPercentage;
        private System.Windows.Forms.ComboBox cbCompulsorySavings;
        private System.Windows.Forms.NumericUpDown udCompulsoryPercentage;
        private System.Windows.Forms.ColumnHeader chCurrency;
        private System.Windows.Forms.ColumnHeader chCreationDate;
        private System.Windows.Forms.DateTimePicker dtCreationDate;
    }
}