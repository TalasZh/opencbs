namespace Octopus.GUI.TellerManagement
{
    partial class FrmOpenCloseTeller
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOpenCloseTeller));
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.cmbTellers = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.labelCurrency = new System.Windows.Forms.Label();
            this.btnClose = new Octopus.GUI.UserControl.SweetButton();
            this.btnConfirm = new Octopus.GUI.UserControl.SweetButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.label1.Name = "label1";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblName.Name = "lblName";
            // 
            // cmbTellers
            // 
            this.cmbTellers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbTellers, "cmbTellers");
            this.cmbTellers.FormattingEnabled = true;
            this.cmbTellers.Name = "cmbTellers";
            this.cmbTellers.SelectedValueChanged += new System.EventHandler(this.cmbTellers_SelectedValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.label2.Name = "label2";
            // 
            // tbAmount
            // 
            resources.ApplyResources(this.tbAmount, "tbAmount");
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.TextChanged += new System.EventHandler(this.tbAmount_TextChanged);
            this.tbAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAmount_KeyPress);
            // 
            // labelCurrency
            // 
            resources.ApplyResources(this.labelCurrency, "labelCurrency");
            this.labelCurrency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelCurrency.Name = "labelCurrency";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Menu = null;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            resources.ApplyResources(this.btnConfirm, "btnConfirm");
            this.btnConfirm.Menu = null;
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // FrmOpenCloseTeller
            // 
            this.AcceptButton = this.btnConfirm;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.labelCurrency);
            this.Controls.Add(this.tbAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbTellers);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOpenCloseTeller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOpenCloseTeller_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ComboBox cmbTellers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAmount;
        private System.Windows.Forms.Label labelCurrency;
        private Octopus.GUI.UserControl.SweetButton btnClose;
        private Octopus.GUI.UserControl.SweetButton btnConfirm;
    }
}