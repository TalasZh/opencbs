using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Contracts
{
    partial class VillageDisburseLoanForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VillageDisburseLoanForm));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.disburseLoansStatusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtDisbursement = new System.Windows.Forms.DateTimePicker();
            this.dtpRepayment = new System.Windows.Forms.DateTimePicker();
            this.lvMembers = new OpenCBS.GUI.UserControl.ListViewEx();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colPassport = new System.Windows.Forms.ColumnHeader();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.colRepaymentDate = new System.Windows.Forms.ColumnHeader();
            this.colAmount = new System.Windows.Forms.ColumnHeader();
            this.colCurrency = new System.Windows.Forms.ColumnHeader();
            this.colInterest = new System.Windows.Forms.ColumnHeader();
            this.colGracePeriod = new System.Windows.Forms.ColumnHeader();
            this.colInstallments = new System.Windows.Forms.ColumnHeader();
            this.colLoanOfficer = new System.Windows.Forms.ColumnHeader();
            this.colFundingLine = new System.Windows.Forms.ColumnHeader();
            this.colNewFLAmount = new System.Windows.Forms.ColumnHeader();
            this.colPaymentMethod = new System.Windows.Forms.ColumnHeader();
            this.cbPaymentMethods = new System.Windows.Forms.ComboBox();
            this.pnlButtons.SuspendLayout();
            this.disburseLoansStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.disburseLoansStatusBar);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            resources.ApplyResources(this.pnlButtons, "pnlButtons");
            this.pnlButtons.Name = "pnlButtons";
            // 
            // disburseLoansStatusBar
            // 
            this.disburseLoansStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.disburseLoansStatusBar, "disburseLoansStatusBar");
            this.disburseLoansStatusBar.Name = "disburseLoansStatusBar";
            this.disburseLoansStatusBar.SizingGrip = false;
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtDisbursement
            // 
            this.dtDisbursement.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtDisbursement, "dtDisbursement");
            this.dtDisbursement.Name = "dtDisbursement";
            this.dtDisbursement.ValueChanged += new System.EventHandler(this.dtDisbursement_ValueChanged);
            // 
            // dtpRepayment
            // 
            this.dtpRepayment.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtpRepayment, "dtpRepayment");
            this.dtpRepayment.Name = "dtpRepayment";
            this.dtpRepayment.ValueChanged += new System.EventHandler(this.dtpRepayment_ValueChanged);
            // 
            // lvMembers
            // 
            this.lvMembers.CheckBoxes = true;
            this.lvMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colPassport,
            this.colDate,
            this.colRepaymentDate,
            this.colAmount,
            this.colCurrency,
            this.colInterest,
            this.colGracePeriod,
            this.colInstallments,
            this.colLoanOfficer,
            this.colFundingLine,
            this.colNewFLAmount,
            this.colPaymentMethod});
            resources.ApplyResources(this.lvMembers, "lvMembers");
            this.lvMembers.DoubleClickActivation = false;
            this.lvMembers.FullRowSelect = true;
            this.lvMembers.GridLines = true;
            this.lvMembers.MultiSelect = false;
            this.lvMembers.Name = "lvMembers";
            this.lvMembers.UseCompatibleStateImageBehavior = false;
            this.lvMembers.View = System.Windows.Forms.View.Details;
            this.lvMembers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvMembers_ItemChecked);
            this.lvMembers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvMembers_ItemCheck);
            this.lvMembers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvMembers_MouseDown);
            this.lvMembers.SubItemEndEditing += new OpenCBS.GUI.UserControl.SubItemEndEditingEventHandler(this.lvMembers_SubItemEndEditing);
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            // 
            // colPassport
            // 
            resources.ApplyResources(this.colPassport, "colPassport");
            // 
            // colDate
            // 
            resources.ApplyResources(this.colDate, "colDate");
            // 
            // colRepaymentDate
            // 
            resources.ApplyResources(this.colRepaymentDate, "colRepaymentDate");
            // 
            // colAmount
            // 
            resources.ApplyResources(this.colAmount, "colAmount");
            // 
            // colCurrency
            // 
            resources.ApplyResources(this.colCurrency, "colCurrency");
            // 
            // colInterest
            // 
            resources.ApplyResources(this.colInterest, "colInterest");
            // 
            // colGracePeriod
            // 
            resources.ApplyResources(this.colGracePeriod, "colGracePeriod");
            // 
            // colInstallments
            // 
            resources.ApplyResources(this.colInstallments, "colInstallments");
            // 
            // colLoanOfficer
            // 
            resources.ApplyResources(this.colLoanOfficer, "colLoanOfficer");
            // 
            // colFundingLine
            // 
            resources.ApplyResources(this.colFundingLine, "colFundingLine");
            // 
            // colNewFLAmount
            // 
            resources.ApplyResources(this.colNewFLAmount, "colNewFLAmount");
            // 
            // colPaymentMethod
            // 
            resources.ApplyResources(this.colPaymentMethod, "colPaymentMethod");
            // 
            // cbPaymentMethods
            // 
            this.cbPaymentMethods.DisplayMember = "Name";
            this.cbPaymentMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPaymentMethods.FormattingEnabled = true;
            resources.ApplyResources(this.cbPaymentMethods, "cbPaymentMethods");
            this.cbPaymentMethods.Name = "cbPaymentMethods";
            this.cbPaymentMethods.ValueMember = "Id";
            this.cbPaymentMethods.SelectedIndexChanged += new System.EventHandler(this.cbPaymentMethods_SelectedIndexChanged);
            // 
            // VillageDisburseLoanForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvMembers);
            this.Controls.Add(this.cbPaymentMethods);
            this.Controls.Add(this.dtpRepayment);
            this.Controls.Add(this.dtDisbursement);
            this.Controls.Add(this.pnlButtons);
            this.Name = "VillageDisburseLoanForm";
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.disburseLoansStatusBar.ResumeLayout(false);
            this.disburseLoansStatusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colPassport;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.DateTimePicker dtDisbursement;
        private OpenCBS.GUI.UserControl.ListViewEx lvMembers;
        private System.Windows.Forms.ColumnHeader colAmount;
        private System.Windows.Forms.ColumnHeader colInterest;
        private System.Windows.Forms.ColumnHeader colGracePeriod;
        private System.Windows.Forms.ColumnHeader colInstallments;
        private System.Windows.Forms.ColumnHeader colLoanOfficer;
        private System.Windows.Forms.ColumnHeader colFundingLine;
        private System.Windows.Forms.ColumnHeader colNewFLAmount;
        private System.Windows.Forms.ColumnHeader colRepaymentDate;
        private System.Windows.Forms.DateTimePicker dtpRepayment;
        private System.Windows.Forms.StatusStrip disburseLoansStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ColumnHeader colCurrency;
        private System.Windows.Forms.ComboBox cbPaymentMethods;
        private System.Windows.Forms.ColumnHeader colPaymentMethod;
    }
}