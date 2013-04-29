using Octopus.GUI.UserControl;

namespace Octopus.GUI.Configuration
{
    partial class TellersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TellersForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.olvTellers = new BrightIdeasSoftware.ObjectListView();
            this.colName = new BrightIdeasSoftware.OLVColumn();
            this.colAccount = new BrightIdeasSoftware.OLVColumn();
            this.colDesc = new BrightIdeasSoftware.OLVColumn();
            this.colBranch = new BrightIdeasSoftware.OLVColumn();
            this.colCurrency = new BrightIdeasSoftware.OLVColumn();
            this.colUser = new BrightIdeasSoftware.OLVColumn();
            this.colMinAmount = new BrightIdeasSoftware.OLVColumn();
            this.colMaxAmount = new BrightIdeasSoftware.OLVColumn();
            this.colMinAmountDeposit = new BrightIdeasSoftware.OLVColumn();
            this.colMinAmountWithdrawal = new BrightIdeasSoftware.OLVColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.colMaxAmountDeposit = new BrightIdeasSoftware.OLVColumn();
            this.colMaxAmountWithdrawal = new BrightIdeasSoftware.OLVColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvTellers)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.olvTellers);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // olvTellers
            // 
            this.olvTellers.AllColumns.Add(this.colName);
            this.olvTellers.AllColumns.Add(this.colAccount);
            this.olvTellers.AllColumns.Add(this.colDesc);
            this.olvTellers.AllColumns.Add(this.colBranch);
            this.olvTellers.AllColumns.Add(this.colCurrency);
            this.olvTellers.AllColumns.Add(this.colUser);
            this.olvTellers.AllColumns.Add(this.colMinAmount);
            this.olvTellers.AllColumns.Add(this.colMaxAmount);
            this.olvTellers.AllColumns.Add(this.colMinAmountDeposit);
            this.olvTellers.AllColumns.Add(this.colMaxAmountDeposit);
            this.olvTellers.AllColumns.Add(this.colMinAmountWithdrawal);
            this.olvTellers.AllColumns.Add(this.colMaxAmountWithdrawal);
            this.olvTellers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colAccount,
            this.colDesc,
            this.colBranch,
            this.colCurrency,
            this.colUser,
            this.colMinAmount,
            this.colMaxAmount,
            this.colMinAmountDeposit,
            this.colMaxAmountDeposit,
            this.colMinAmountWithdrawal,
            this.colMaxAmountWithdrawal});
            resources.ApplyResources(this.olvTellers, "olvTellers");
            this.olvTellers.FullRowSelect = true;
            this.olvTellers.GridLines = true;
            this.olvTellers.HideSelection = false;
            this.olvTellers.Name = "olvTellers";
            this.olvTellers.ShowGroups = false;
            this.olvTellers.UseCompatibleStateImageBehavior = false;
            this.olvTellers.View = System.Windows.Forms.View.Details;
            this.olvTellers.SelectedIndexChanged += new System.EventHandler(this.olvBranches_SelectedIndexChanged);
            this.olvTellers.DoubleClick += new System.EventHandler(this.olvBranches_DoubleClick);
            // 
            // colName
            // 
            this.colName.AspectName = "Name";
            resources.ApplyResources(this.colName, "colName");
            // 
            // colAccount
            // 
            this.colAccount.AspectName = "Account";
            resources.ApplyResources(this.colAccount, "colAccount");
            // 
            // colDesc
            // 
            this.colDesc.AspectName = "Description";
            resources.ApplyResources(this.colDesc, "colDesc");
            // 
            // colBranch
            // 
            this.colBranch.AspectName = "Branch";
            resources.ApplyResources(this.colBranch, "colBranch");
            // 
            // colCurrency
            // 
            this.colCurrency.AspectName = "Currency";
            resources.ApplyResources(this.colCurrency, "colCurrency");
            // 
            // colUser
            // 
            this.colUser.AspectName = "User";
            resources.ApplyResources(this.colUser, "colUser");
            // 
            // colMinAmount
            // 
            this.colMinAmount.AspectName = "MinAmountTeller";
            resources.ApplyResources(this.colMinAmount, "colMinAmount");
            // 
            // colMaxAmount
            // 
            this.colMaxAmount.AspectName = "MaxAmountTeller";
            resources.ApplyResources(this.colMaxAmount, "colMaxAmount");
            // 
            // colMinAmountDeposit
            // 
            this.colMinAmountDeposit.AspectName = "MinAmountDeposit";
            resources.ApplyResources(this.colMinAmountDeposit, "colMinAmountDeposit");
            // 
            // colMinAmountWithdrawal
            // 
            this.colMinAmountWithdrawal.AspectName = "MinAmountWithdrawal";
            resources.ApplyResources(this.colMinAmountWithdrawal, "colMinAmountWithdrawal");
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // btnAdd
            //
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            //
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            //
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // colMaxAmountDeposit
            // 
            this.colMaxAmountDeposit.AspectName = "MaxAmountDeposit";
            resources.ApplyResources(this.colMaxAmountDeposit, "colMaxAmountDeposit");
            // 
            // colMaxAmountWithdrawal
            // 
            this.colMaxAmountWithdrawal.AspectName = "MaxAmountWithdrawal";
            resources.ApplyResources(this.colMaxAmountWithdrawal, "colMaxAmountWithdrawal");
            // 
            // TellersForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "TellersForm";
            this.Load += new System.EventHandler(this.BranchesForm_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvTellers)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private BrightIdeasSoftware.ObjectListView olvTellers;
        private BrightIdeasSoftware.OLVColumn colName;
        private BrightIdeasSoftware.OLVColumn colDesc;
        private BrightIdeasSoftware.OLVColumn colAccount;
        private BrightIdeasSoftware.OLVColumn colBranch;
        private BrightIdeasSoftware.OLVColumn colCurrency;
        private BrightIdeasSoftware.OLVColumn colUser;
        private BrightIdeasSoftware.OLVColumn colMinAmount;
        private BrightIdeasSoftware.OLVColumn colMaxAmount;
        private BrightIdeasSoftware.OLVColumn colMinAmountWithdrawal;
        private BrightIdeasSoftware.OLVColumn colMinAmountDeposit;
        private BrightIdeasSoftware.OLVColumn colMaxAmountDeposit;
        private BrightIdeasSoftware.OLVColumn colMaxAmountWithdrawal;
    }
}