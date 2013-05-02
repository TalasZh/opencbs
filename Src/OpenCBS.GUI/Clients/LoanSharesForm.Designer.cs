using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Clients
{
    partial class LoanSharesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanSharesForm));
            this.spcBox = new System.Windows.Forms.SplitContainer();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.lvLoanShares = new OpenCBS.GUI.UserControl.ListViewEx();
            this.colMember = new System.Windows.Forms.ColumnHeader();
            this.colLoanShare = new System.Windows.Forms.ColumnHeader();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.spcBox.Panel1.SuspendLayout();
            this.spcBox.Panel2.SuspendLayout();
            this.spcBox.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // spcBox
            // 
            resources.ApplyResources(this.spcBox, "spcBox");
            this.spcBox.Name = "spcBox";
            // 
            // spcBox.Panel1
            // 
            this.spcBox.Panel1.Controls.Add(this.tbAmount);
            this.spcBox.Panel1.Controls.Add(this.lvLoanShares);
            // 
            // spcBox.Panel2
            // 
            this.spcBox.Panel2.Controls.Add(this.pnlButtons);
            // 
            // tbAmount
            // 
            resources.ApplyResources(this.tbAmount, "tbAmount");
            this.tbAmount.Name = "tbAmount";
            // 
            // lvLoanShares
            // 
            this.lvLoanShares.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMember,
            this.colLoanShare});
            resources.ApplyResources(this.lvLoanShares, "lvLoanShares");
            this.lvLoanShares.DoubleClickActivation = true;
            this.lvLoanShares.FullRowSelect = true;
            this.lvLoanShares.GridLines = true;
            this.lvLoanShares.Name = "lvLoanShares";
            this.lvLoanShares.UseCompatibleStateImageBehavior = false;
            this.lvLoanShares.View = System.Windows.Forms.View.Details;
            this.lvLoanShares.SubItemClicked += new OpenCBS.GUI.UserControl.SubItemEventHandler(this.lvLoanShares_SubItemClicked);
            this.lvLoanShares.SubItemEndEditing += new OpenCBS.GUI.UserControl.SubItemEndEditingEventHandler(this.lvLoanShares_SubItemEndEditing);
            // 
            // colMember
            // 
            resources.ApplyResources(this.colMember, "colMember");
            // 
            // colLoanShare
            // 
            resources.ApplyResources(this.colLoanShare, "colLoanShare");
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.lblDescription);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnOK);
            resources.ApplyResources(this.pnlButtons, "pnlButtons");
            this.pnlButtons.Name = "pnlButtons";
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // LoanSharesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spcBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoanSharesForm";
            this.ShowInTaskbar = false;
            this.spcBox.Panel1.ResumeLayout(false);
            this.spcBox.Panel1.PerformLayout();
            this.spcBox.Panel2.ResumeLayout(false);
            this.spcBox.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spcBox;
        private System.Windows.Forms.TextBox tbAmount;
        private ListViewEx lvLoanShares;
        private System.Windows.Forms.ColumnHeader colMember;
        private System.Windows.Forms.ColumnHeader colLoanShare;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}