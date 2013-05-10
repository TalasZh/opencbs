using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Configuration
{
    partial class AddBranchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBranchForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageAddBranches = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.tabPageAddPaymentMethod = new System.Windows.Forms.TabPage();
            this.lvPaymentMethods = new System.Windows.Forms.ListView();
            this.columnHeaderId = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPaymentMethod = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAccount = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.panelPaymentMethods = new System.Windows.Forms.Panel();
            this.btnDeletePaymentMethod = new System.Windows.Forms.Button();
            this.btnEditPaymentMethod = new System.Windows.Forms.Button();
            this.btnAddPaymentMethod = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageAddBranches.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPageAddPaymentMethod.SuspendLayout();
            this.panelPaymentMethods.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageAddBranches);
            this.tabControl.Controls.Add(this.tabPageAddPaymentMethod);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageAddBranches
            // 
            this.tabPageAddBranches.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.tabPageAddBranches, "tabPageAddBranches");
            this.tabPageAddBranches.Name = "tabPageAddBranches";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbDescription, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbAddress, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbCode, 1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // tbDescription
            // 
            resources.ApplyResources(this.tbDescription, "tbDescription");
            this.tbDescription.Name = "tbDescription";
            // 
            // tbName
            // 
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.Name = "tbName";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tbAddress
            // 
            resources.ApplyResources(this.tbAddress, "tbAddress");
            this.tbAddress.Name = "tbAddress";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tbCode
            // 
            resources.ApplyResources(this.tbCode, "tbCode");
            this.tbCode.Name = "tbCode";
            // 
            // tabPageAddPaymentMethod
            // 
            this.tabPageAddPaymentMethod.Controls.Add(this.lvPaymentMethods);
            this.tabPageAddPaymentMethod.Controls.Add(this.panelPaymentMethods);
            resources.ApplyResources(this.tabPageAddPaymentMethod, "tabPageAddPaymentMethod");
            this.tabPageAddPaymentMethod.Name = "tabPageAddPaymentMethod";
            // 
            // lvPaymentMethods
            // 
            this.lvPaymentMethods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderId,
            this.columnHeaderPaymentMethod,
            this.columnHeaderAccount,
            this.columnHeaderDate});
            resources.ApplyResources(this.lvPaymentMethods, "lvPaymentMethods");
            this.lvPaymentMethods.FullRowSelect = true;
            this.lvPaymentMethods.GridLines = true;
            this.lvPaymentMethods.MultiSelect = false;
            this.lvPaymentMethods.Name = "lvPaymentMethods";
            this.lvPaymentMethods.UseCompatibleStateImageBehavior = false;
            this.lvPaymentMethods.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderId
            // 
            resources.ApplyResources(this.columnHeaderId, "columnHeaderId");
            // 
            // columnHeaderPaymentMethod
            // 
            resources.ApplyResources(this.columnHeaderPaymentMethod, "columnHeaderPaymentMethod");
            // 
            // columnHeaderAccount
            // 
            resources.ApplyResources(this.columnHeaderAccount, "columnHeaderAccount");
            // 
            // columnHeaderDate
            // 
            resources.ApplyResources(this.columnHeaderDate, "columnHeaderDate");
            // 
            // panelPaymentMethods
            // 
            resources.ApplyResources(this.panelPaymentMethods, "panelPaymentMethods");
            this.panelPaymentMethods.Controls.Add(this.btnDeletePaymentMethod);
            this.panelPaymentMethods.Controls.Add(this.btnEditPaymentMethod);
            this.panelPaymentMethods.Controls.Add(this.btnAddPaymentMethod);
            this.panelPaymentMethods.Name = "panelPaymentMethods";
            // 
            // btnDeletePaymentMethod
            // 
            resources.ApplyResources(this.btnDeletePaymentMethod, "btnDeletePaymentMethod");
            this.btnDeletePaymentMethod.Name = "btnDeletePaymentMethod";
            this.btnDeletePaymentMethod.Click += new System.EventHandler(this.btnDeletePaymentMethod_Click);
            // 
            // btnEditPaymentMethod
            // 
            resources.ApplyResources(this.btnEditPaymentMethod, "btnEditPaymentMethod");
            this.btnEditPaymentMethod.Name = "btnEditPaymentMethod";
            this.btnEditPaymentMethod.Click += new System.EventHandler(this.btnEditPaymentMethod_Click);
            // 
            // btnAddPaymentMethod
            // 
            resources.ApplyResources(this.btnAddPaymentMethod, "btnAddPaymentMethod");
            this.btnAddPaymentMethod.Name = "btnAddPaymentMethod";
            this.btnAddPaymentMethod.Click += new System.EventHandler(this.btnAddPaymentMethod_Click);
            // 
            // AddBranchForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddBranchForm";
            this.Load += new System.EventHandler(this.AddBranchForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddBranchForm_FormClosing);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.tabControl.ResumeLayout(false);
            this.tabPageAddBranches.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPageAddPaymentMethod.ResumeLayout(false);
            this.panelPaymentMethods.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageAddBranches;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.TabPage tabPageAddPaymentMethod;
        private System.Windows.Forms.Panel panelPaymentMethods;
        private System.Windows.Forms.ListView lvPaymentMethods;
        private System.Windows.Forms.ColumnHeader columnHeaderPaymentMethod;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.Button btnDeletePaymentMethod;
        private System.Windows.Forms.Button btnAddPaymentMethod;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.Button btnEditPaymentMethod;
        private System.Windows.Forms.ColumnHeader columnHeaderAccount;
    }
}