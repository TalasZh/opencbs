namespace Octopus.GUI.Report_Browser
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
            this.btnCancel = new Octopus.GUI.UserControl.SweetButton();
            this.btnOk = new Octopus.GUI.UserControl.SweetButton();
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
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnCancel.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.btnCancel.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.btnCancel.Menu = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.BackColor = System.Drawing.Color.Gainsboro;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnOk.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Validity;
            this.btnOk.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_validity;
            this.btnOk.Menu = null;
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = false;
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
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblFrom.Name = "lblFrom";
            // 
            // lblTo
            // 
            this.lblTo.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblTo, "lblTo");
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
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
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.label1.Name = "label1";
            // 
            // chkShowDelinquentLoans
            // 
            resources.ApplyResources(this.chkShowDelinquentLoans, "chkShowDelinquentLoans");
            this.chkShowDelinquentLoans.BackColor = System.Drawing.Color.Transparent;
            this.chkShowDelinquentLoans.Checked = true;
            this.chkShowDelinquentLoans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowDelinquentLoans.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.chkShowDelinquentLoans.Name = "chkShowDelinquentLoans";
            this.chkShowDelinquentLoans.UseVisualStyleBackColor = false;
            // 
            // labelDisbursedIn
            // 
            this.labelDisbursedIn.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelDisbursedIn, "labelDisbursedIn");
            this.labelDisbursedIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelDisbursedIn.Name = "labelDisbursedIn";
            // 
            // labelShowIn
            // 
            this.labelShowIn.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelShowIn, "labelShowIn");
            this.labelShowIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
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
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
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
            this.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
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

        private Octopus.GUI.UserControl.SweetButton btnCancel;
        private Octopus.GUI.UserControl.SweetButton btnOk;
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