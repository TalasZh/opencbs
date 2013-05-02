using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Contracts
{
    partial class VillageCreditCommitteeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VillageCreditCommitteeForm));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.tbEntryFee = new System.Windows.Forms.TextBox();
            this.lvMembers = new OpenCBS.GUI.UserControl.ListViewEx();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chPassport = new System.Windows.Forms.ColumnHeader();
            this.chAmount = new System.Windows.Forms.ColumnHeader();
            this.chCurrency = new System.Windows.Forms.ColumnHeader();
            this.chLoanOfficer = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.chValidationDate = new System.Windows.Forms.ColumnHeader();
            this.chValidationCode = new System.Windows.Forms.ColumnHeader();
            this.chComment = new System.Windows.Forms.ColumnHeader();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.dtCreditCommittee = new System.Windows.Forms.DateTimePicker();
            this.tbValidationCode = new System.Windows.Forms.TextBox();
            this.tbLoanOfficer = new System.Windows.Forms.TextBox();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            resources.ApplyResources(this.pnlButtons, "pnlButtons");
            this.pnlButtons.Name = "pnlButtons";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbAmount
            // 
            resources.ApplyResources(this.tbAmount, "tbAmount");
            this.tbAmount.Name = "tbAmount";
            // 
            // tbEntryFee
            // 
            resources.ApplyResources(this.tbEntryFee, "tbEntryFee");
            this.tbEntryFee.Name = "tbEntryFee";
            // 
            // lvMembers
            // 
            this.lvMembers.CheckBoxes = true;
            this.lvMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chPassport,
            this.chAmount,
            this.chCurrency,
            this.chLoanOfficer,
            this.chStatus,
            this.chValidationDate,
            this.chValidationCode,
            this.chComment});
            resources.ApplyResources(this.lvMembers, "lvMembers");
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
            // chLoanOfficer
            // 
            resources.ApplyResources(this.chLoanOfficer, "chLoanOfficer");
            // 
            // chStatus
            // 
            resources.ApplyResources(this.chStatus, "chStatus");
            // 
            // chValidationDate
            // 
            resources.ApplyResources(this.chValidationDate, "chValidationDate");
            // 
            // chValidationCode
            // 
            resources.ApplyResources(this.chValidationCode, "chValidationCode");
            // 
            // chComment
            // 
            resources.ApplyResources(this.chComment, "chComment");
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            resources.ApplyResources(this.cbStatus, "cbStatus");
            this.cbStatus.Name = "cbStatus";
            // 
            // tbComment
            // 
            resources.ApplyResources(this.tbComment, "tbComment");
            this.tbComment.Name = "tbComment";
            // 
            // dtCreditCommittee
            // 
            this.dtCreditCommittee.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtCreditCommittee, "dtCreditCommittee");
            this.dtCreditCommittee.Name = "dtCreditCommittee";
            this.dtCreditCommittee.ValueChanged += new System.EventHandler(this.dtCreditCommittee_ValueChanged);
            // 
            // tbValidationCode
            // 
            resources.ApplyResources(this.tbValidationCode, "tbValidationCode");
            this.tbValidationCode.Name = "tbValidationCode";
            // 
            // tbLoanOfficer
            // 
            resources.ApplyResources(this.tbLoanOfficer, "tbLoanOfficer");
            this.tbLoanOfficer.Name = "tbLoanOfficer";
            // 
            // VillageCreditCommitteeForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtCreditCommittee);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.tbEntryFee);
            this.Controls.Add(this.tbValidationCode);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.tbLoanOfficer);
            this.Controls.Add(this.tbAmount);
            this.Controls.Add(this.lvMembers);
            this.Controls.Add(this.pnlButtons);
            this.Name = "VillageCreditCommitteeForm";
            this.pnlButtons.ResumeLayout(false);
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
        private System.Windows.Forms.ColumnHeader chLoanOfficer;
        private System.Windows.Forms.TextBox tbEntryFee;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColumnHeader chCurrency;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chValidationDate;
        private System.Windows.Forms.ColumnHeader chComment;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.DateTimePicker dtCreditCommittee;
        private System.Windows.Forms.TextBox tbValidationCode;
        private System.Windows.Forms.ColumnHeader chValidationCode;
        private System.Windows.Forms.TextBox tbLoanOfficer;
    }
}