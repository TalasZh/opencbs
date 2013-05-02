namespace OpenCBS.GUI.TellerManagement
{
    partial class FrmTellerOperation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTellerOperation));
            this.lblBranch = new System.Windows.Forms.Label();
            this.cmbBranch = new System.Windows.Forms.ComboBox();
            this.rbtnCashIn = new System.Windows.Forms.RadioButton();
            this.rbtnCashOut = new System.Windows.Forms.RadioButton();
            this.lblTeller = new System.Windows.Forms.Label();
            this.cmbTeller = new System.Windows.Forms.ComboBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.tbxAmount = new System.Windows.Forms.TextBox();
            this.tbxDescription = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBranch
            // 
            this.lblBranch.AccessibleDescription = null;
            this.lblBranch.AccessibleName = null;
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Font = null;
            this.lblBranch.Name = "lblBranch";
            // 
            // cmbBranch
            // 
            this.cmbBranch.AccessibleDescription = null;
            this.cmbBranch.AccessibleName = null;
            resources.ApplyResources(this.cmbBranch, "cmbBranch");
            this.cmbBranch.BackgroundImage = null;
            this.cmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranch.Font = null;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.SelectedIndexChanged += new System.EventHandler(this.cmbBranch_SelectedIndexChanged);
            // 
            // rbtnCashIn
            // 
            this.rbtnCashIn.AccessibleDescription = null;
            this.rbtnCashIn.AccessibleName = null;
            resources.ApplyResources(this.rbtnCashIn, "rbtnCashIn");
            this.rbtnCashIn.Checked = true;
            this.rbtnCashIn.Font = null;
            this.rbtnCashIn.Name = "rbtnCashIn";
            this.rbtnCashIn.TabStop = true;
            // 
            // rbtnCashOut
            // 
            this.rbtnCashOut.AccessibleDescription = null;
            this.rbtnCashOut.AccessibleName = null;
            resources.ApplyResources(this.rbtnCashOut, "rbtnCashOut");
            this.rbtnCashOut.Font = null;
            this.rbtnCashOut.Name = "rbtnCashOut";
            // 
            // lblTeller
            // 
            this.lblTeller.AccessibleDescription = null;
            this.lblTeller.AccessibleName = null;
            resources.ApplyResources(this.lblTeller, "lblTeller");
            this.lblTeller.Font = null;
            this.lblTeller.Name = "lblTeller";
            // 
            // cmbTeller
            // 
            this.cmbTeller.AccessibleDescription = null;
            this.cmbTeller.AccessibleName = null;
            resources.ApplyResources(this.cmbTeller, "cmbTeller");
            this.cmbTeller.BackgroundImage = null;
            this.cmbTeller.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeller.Font = null;
            this.cmbTeller.FormattingEnabled = true;
            this.cmbTeller.Name = "cmbTeller";
            this.cmbTeller.SelectedIndexChanged += new System.EventHandler(this.cmbTeller_SelectedIndexChanged);
            // 
            // lblAmount
            // 
            this.lblAmount.AccessibleDescription = null;
            this.lblAmount.AccessibleName = null;
            resources.ApplyResources(this.lblAmount, "lblAmount");
            this.lblAmount.Font = null;
            this.lblAmount.Name = "lblAmount";
            // 
            // lblDescription
            // 
            this.lblDescription.AccessibleDescription = null;
            this.lblDescription.AccessibleName = null;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Font = null;
            this.lblDescription.Name = "lblDescription";
            // 
            // tbxAmount
            // 
            this.tbxAmount.AccessibleDescription = null;
            this.tbxAmount.AccessibleName = null;
            resources.ApplyResources(this.tbxAmount, "tbxAmount");
            this.tbxAmount.BackgroundImage = null;
            this.tbxAmount.Font = null;
            this.tbxAmount.Name = "tbxAmount";
            this.tbxAmount.TextChanged += new System.EventHandler(this.tbxAmount_TextChanged);
            this.tbxAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxAmount_KeyPress);
            // 
            // tbxDescription
            // 
            this.tbxDescription.AccessibleDescription = null;
            this.tbxDescription.AccessibleName = null;
            resources.ApplyResources(this.tbxDescription, "tbxDescription");
            this.tbxDescription.BackgroundImage = null;
            this.tbxDescription.Font = null;
            this.tbxDescription.Name = "tbxDescription";
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleDescription = null;
            this.btnConfirm.AccessibleName = null;
            resources.ApplyResources(this.btnConfirm, "btnConfirm");
            this.btnConfirm.Font = null;
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Font = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmTellerOperation
            // 
            this.AcceptButton = this.btnConfirm;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.tbxDescription);
            this.Controls.Add(this.tbxAmount);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.rbtnCashOut);
            this.Controls.Add(this.rbtnCashIn);
            this.Controls.Add(this.cmbTeller);
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblTeller);
            this.Controls.Add(this.lblBranch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTellerOperation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cmbBranch;
        private System.Windows.Forms.RadioButton rbtnCashIn;
        private System.Windows.Forms.RadioButton rbtnCashOut;
        private System.Windows.Forms.Label lblTeller;
        private System.Windows.Forms.ComboBox cmbTeller;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox tbxAmount;
        private System.Windows.Forms.TextBox tbxDescription;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}