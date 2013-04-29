namespace Octopus.GUI.Configuration
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
            this.btnOK = new Octopus.GUI.UserControl.SweetButton();
            this.btnCancel = new Octopus.GUI.UserControl.SweetButton();
            this.gbFields.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkBranchCode
            // 
            resources.ApplyResources(this.chkBranchCode, "chkBranchCode");
            this.chkBranchCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.chkBranchCode.Name = "chkBranchCode";
            this.chkBranchCode.UseVisualStyleBackColor = true;
            this.chkBranchCode.CheckedChanged += new System.EventHandler(this.chkBranchCode_CheckedChanged);
            // 
            // chkDistrict
            // 
            resources.ApplyResources(this.chkDistrict, "chkDistrict");
            this.chkDistrict.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.chkDistrict.Name = "chkDistrict";
            this.chkDistrict.UseVisualStyleBackColor = true;
            this.chkDistrict.CheckedChanged += new System.EventHandler(this.chkDistrict_CheckedChanged);
            // 
            // chkYear
            // 
            resources.ApplyResources(this.chkYear, "chkYear");
            this.chkYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.chkYear.Name = "chkYear";
            this.chkYear.UseVisualStyleBackColor = true;
            this.chkYear.CheckedChanged += new System.EventHandler(this.chkYear_CheckedChanged);
            // 
            // chkLoanOfficer
            // 
            resources.ApplyResources(this.chkLoanOfficer, "chkLoanOfficer");
            this.chkLoanOfficer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.chkLoanOfficer.Name = "chkLoanOfficer";
            this.chkLoanOfficer.UseVisualStyleBackColor = true;
            this.chkLoanOfficer.CheckedChanged += new System.EventHandler(this.chkLoanOfficer_CheckedChanged);
            // 
            // chkProductCode
            // 
            resources.ApplyResources(this.chkProductCode, "chkProductCode");
            this.chkProductCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.chkProductCode.Name = "chkProductCode";
            this.chkProductCode.UseVisualStyleBackColor = true;
            this.chkProductCode.CheckedChanged += new System.EventHandler(this.chkProductCode_CheckedChanged);
            // 
            // chkLoanCycle
            // 
            resources.ApplyResources(this.chkLoanCycle, "chkLoanCycle");
            this.chkLoanCycle.Checked = true;
            this.chkLoanCycle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoanCycle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.chkLoanCycle.Name = "chkLoanCycle";
            this.chkLoanCycle.UseVisualStyleBackColor = true;
            this.chkLoanCycle.CheckedChanged += new System.EventHandler(this.chkLoanCycle_CheckedChanged);
            // 
            // chkProjectCycle
            // 
            resources.ApplyResources(this.chkProjectCycle, "chkProjectCycle");
            this.chkProjectCycle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.chkProjectCycle.Name = "chkProjectCycle";
            this.chkProjectCycle.UseVisualStyleBackColor = true;
            this.chkProjectCycle.CheckedChanged += new System.EventHandler(this.chkProjectCycle_CheckedChanged);
            // 
            // chkClientId
            // 
            resources.ApplyResources(this.chkClientId, "chkClientId");
            this.chkClientId.Checked = true;
            this.chkClientId.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClientId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.chkClientId.Name = "chkClientId";
            this.chkClientId.UseVisualStyleBackColor = true;
            this.chkClientId.CheckedChanged += new System.EventHandler(this.chkID_CheckedChanged);
            // 
            // gbFields
            // 
            this.gbFields.BackColor = System.Drawing.Color.Transparent;
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
            this.gbFields.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.gbFields.Name = "gbFields";
            this.gbFields.TabStop = false;
            // 
            // lblContractCode
            // 
            resources.ApplyResources(this.lblContractCode, "lblContractCode");
            this.lblContractCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
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
            this.chkContractId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.chkContractId.Name = "chkContractId";
            this.chkContractId.UseVisualStyleBackColor = true;
            this.chkContractId.CheckedChanged += new System.EventHandler(this.chkID_CheckedChanged);
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris_180;
            this.pnlButtons.Controls.Add(this.btnOK);
            this.pnlButtons.Controls.Add(this.btnCancel);
            resources.ApplyResources(this.pnlButtons, "pnlButtons");
            this.pnlButtons.Name = "pnlButtons";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnOK.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Validity;
            this.btnOK.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_validity;
            this.btnOK.Menu = null;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = false;
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
            // ContractCodeForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
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
        private Octopus.GUI.UserControl.SweetButton btnCancel;
        private Octopus.GUI.UserControl.SweetButton btnOK;
        private System.Windows.Forms.Label lblContractCode;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.CheckBox chkContractId;
    }
}