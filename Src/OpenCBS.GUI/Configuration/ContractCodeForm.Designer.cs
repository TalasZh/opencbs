namespace OpenCBS.GUI.Configuration
{
    partial class ContractCodeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractCodeForm));
            this.chkBranchCode = new System.Windows.Forms.CheckBox();
            this.chkDistrict = new System.Windows.Forms.CheckBox();
            this.chkYear = new System.Windows.Forms.CheckBox();
            this.chkLoanOfficer = new System.Windows.Forms.CheckBox();
            this.chkProductCode = new System.Windows.Forms.CheckBox();
            this.chkLoanCycle = new System.Windows.Forms.CheckBox();
            this.chkProjectCycle = new System.Windows.Forms.CheckBox();
            this.chkClientId = new System.Windows.Forms.CheckBox();
            this.gbFields = new System.Windows.Forms.GroupBox();
            this.lblContractCode = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.chkContractId = new System.Windows.Forms.CheckBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbFields.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkBranchCode
            // 
            resources.ApplyResources(this.chkBranchCode, "chkBranchCode");
            this.chkBranchCode.Name = "chkBranchCode";
            this.chkBranchCode.CheckedChanged += new System.EventHandler(this.chkBranchCode_CheckedChanged);
            // 
            // chkDistrict
            // 
            resources.ApplyResources(this.chkDistrict, "chkDistrict");
            this.chkDistrict.Name = "chkDistrict";
            this.chkDistrict.CheckedChanged += new System.EventHandler(this.chkDistrict_CheckedChanged);
            // 
            // chkYear
            // 
            resources.ApplyResources(this.chkYear, "chkYear");
            this.chkYear.Name = "chkYear";
            this.chkYear.CheckedChanged += new System.EventHandler(this.chkYear_CheckedChanged);
            // 
            // chkLoanOfficer
            // 
            resources.ApplyResources(this.chkLoanOfficer, "chkLoanOfficer");
            this.chkLoanOfficer.Name = "chkLoanOfficer";
            this.chkLoanOfficer.CheckedChanged += new System.EventHandler(this.chkLoanOfficer_CheckedChanged);
            // 
            // chkProductCode
            // 
            resources.ApplyResources(this.chkProductCode, "chkProductCode");
            this.chkProductCode.Name = "chkProductCode";
            this.chkProductCode.CheckedChanged += new System.EventHandler(this.chkProductCode_CheckedChanged);
            // 
            // chkLoanCycle
            // 
            resources.ApplyResources(this.chkLoanCycle, "chkLoanCycle");
            this.chkLoanCycle.Checked = true;
            this.chkLoanCycle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoanCycle.Name = "chkLoanCycle";
            this.chkLoanCycle.CheckedChanged += new System.EventHandler(this.chkLoanCycle_CheckedChanged);
            // 
            // chkProjectCycle
            // 
            resources.ApplyResources(this.chkProjectCycle, "chkProjectCycle");
            this.chkProjectCycle.Name = "chkProjectCycle";
            this.chkProjectCycle.CheckedChanged += new System.EventHandler(this.chkProjectCycle_CheckedChanged);
            // 
            // chkClientId
            // 
            resources.ApplyResources(this.chkClientId, "chkClientId");
            this.chkClientId.Checked = true;
            this.chkClientId.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClientId.Name = "chkClientId";
            this.chkClientId.CheckedChanged += new System.EventHandler(this.chkID_CheckedChanged);
            // 
            // gbFields
            //
            this.gbFields.Controls.Add(this.lblContractCode);
            this.gbFields.Controls.Add(this.tbCode);
            this.gbFields.Controls.Add(this.chkProductCode);
            this.gbFields.Controls.Add(this.chkContractId);
            this.gbFields.Controls.Add(this.chkClientId);
            this.gbFields.Controls.Add(this.chkBranchCode);
            this.gbFields.Controls.Add(this.chkProjectCycle);
            this.gbFields.Controls.Add(this.chkDistrict);
            this.gbFields.Controls.Add(this.chkLoanCycle);
            this.gbFields.Controls.Add(this.chkYear);
            this.gbFields.Controls.Add(this.chkLoanOfficer);
            resources.ApplyResources(this.gbFields, "gbFields");
            this.gbFields.Name = "gbFields";
            this.gbFields.TabStop = false;
            // 
            // lblContractCode
            // 
            resources.ApplyResources(this.lblContractCode, "lblContractCode");
            this.lblContractCode.Name = "lblContractCode";
            // 
            // tbCode
            // 
            resources.ApplyResources(this.tbCode, "tbCode");
            this.tbCode.Name = "tbCode";
            // 
            // chkContractId
            // 
            resources.ApplyResources(this.chkContractId, "chkContractId");
            this.chkContractId.Checked = true;
            this.chkContractId.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkContractId.Name = "chkContractId";
            this.chkContractId.CheckedChanged += new System.EventHandler(this.chkID_CheckedChanged);
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackgroundImage = global::OpenCBS.GUI.Properties.Resources.theme1_1_fond_gris_180;
            this.pnlButtons.Controls.Add(this.btnOK);
            this.pnlButtons.Controls.Add(this.btnCancel);
            resources.ApplyResources(this.pnlButtons, "pnlButtons");
            this.pnlButtons.Name = "pnlButtons";
            // 
            // btnOK
            //
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            // 
            // ContractCodeForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.gbFields);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContractCodeForm";
            this.gbFields.ResumeLayout(false);
            this.gbFields.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkBranchCode;
        private System.Windows.Forms.CheckBox chkDistrict;
        private System.Windows.Forms.CheckBox chkYear;
        private System.Windows.Forms.CheckBox chkLoanOfficer;
        private System.Windows.Forms.CheckBox chkProductCode;
        private System.Windows.Forms.CheckBox chkLoanCycle;
        private System.Windows.Forms.CheckBox chkProjectCycle;
        private System.Windows.Forms.CheckBox chkClientId;
        private System.Windows.Forms.GroupBox gbFields;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblContractCode;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.CheckBox chkContractId;
    }
}