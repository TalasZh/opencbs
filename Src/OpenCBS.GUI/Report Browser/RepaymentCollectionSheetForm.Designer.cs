namespace OpenCBS.GUI.Report_Browser
{
    partial class RepaymentCollectionSheetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepaymentCollectionSheetForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.cbLoanOfficer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowDelinquentLoans = new System.Windows.Forms.CheckBox();
            this.labelDisbursedIn = new System.Windows.Forms.Label();
            this.labelShowIn = new System.Windows.Forms.Label();
            this.cbDisbursedIn = new System.Windows.Forms.ComboBox();
            this.cbShowIn = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Name = "btnOk";
            // 
            // dtFrom
            // 
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtFrom, "dtFrom");
            this.dtFrom.Name = "dtFrom";
            // 
            // lblFrom
            // 
            this.lblFrom.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblFrom, "lblFrom");
            this.lblFrom.Name = "lblFrom";
            // 
            // lblTo
            // 
            this.lblTo.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblTo, "lblTo");
            this.lblTo.Name = "lblTo";
            // 
            // dtTo
            // 
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtTo, "dtTo");
            this.dtTo.Name = "dtTo";
            // 
            // cbLoanOfficer
            // 
            this.cbLoanOfficer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoanOfficer.FormattingEnabled = true;
            resources.ApplyResources(this.cbLoanOfficer, "cbLoanOfficer");
            this.cbLoanOfficer.Name = "cbLoanOfficer";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chkShowDelinquentLoans
            // 
            resources.ApplyResources(this.chkShowDelinquentLoans, "chkShowDelinquentLoans");
            this.chkShowDelinquentLoans.Checked = true;
            this.chkShowDelinquentLoans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowDelinquentLoans.Name = "chkShowDelinquentLoans";
            // 
            // labelDisbursedIn
            // 
            this.labelDisbursedIn.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelDisbursedIn, "labelDisbursedIn");
            this.labelDisbursedIn.Name = "labelDisbursedIn";
            // 
            // labelShowIn
            // 
            this.labelShowIn.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelShowIn, "labelShowIn");
            this.labelShowIn.Name = "labelShowIn";
            // 
            // cbDisbursedIn
            // 
            this.cbDisbursedIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisbursedIn.FormattingEnabled = true;
            resources.ApplyResources(this.cbDisbursedIn, "cbDisbursedIn");
            this.cbDisbursedIn.Name = "cbDisbursedIn";
            // 
            // cbShowIn
            // 
            this.cbShowIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShowIn.FormattingEnabled = true;
            resources.ApplyResources(this.cbShowIn, "cbShowIn");
            this.cbShowIn.Name = "cbShowIn";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbBranch
            // 
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.FormattingEnabled = true;
            resources.ApplyResources(this.cbBranch, "cbBranch");
            this.cbBranch.Name = "cbBranch";
            // 
            // RepaymentCollectionSheetForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbBranch);
            this.Controls.Add(this.chkShowDelinquentLoans);
            this.Controls.Add(this.labelShowIn);
            this.Controls.Add(this.labelDisbursedIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbShowIn);
            this.Controls.Add(this.cbDisbursedIn);
            this.Controls.Add(this.cbLoanOfficer);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RepaymentCollectionSheetForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.ComboBox cbLoanOfficer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowDelinquentLoans;
        private System.Windows.Forms.Label labelDisbursedIn;
        private System.Windows.Forms.Label labelShowIn;
        private System.Windows.Forms.ComboBox cbDisbursedIn;
        private System.Windows.Forms.ComboBox cbShowIn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbBranch;


    }
}