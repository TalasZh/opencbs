using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Configuration
{
    partial class BranchesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BranchesForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.olvBranches = new BrightIdeasSoftware.ObjectListView();
            this.colName = new BrightIdeasSoftware.OLVColumn();
            this.colCode = new BrightIdeasSoftware.OLVColumn();
            this.colAddress = new BrightIdeasSoftware.OLVColumn();
            this.colDescription = new BrightIdeasSoftware.OLVColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBranches)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.olvBranches);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            // 
            // olvBranches
            // 
            this.olvBranches.AccessibleDescription = null;
            this.olvBranches.AccessibleName = null;
            resources.ApplyResources(this.olvBranches, "olvBranches");
            this.olvBranches.AllColumns.Add(this.colName);
            this.olvBranches.AllColumns.Add(this.colCode);
            this.olvBranches.AllColumns.Add(this.colAddress);
            this.olvBranches.AllColumns.Add(this.colDescription);
            this.olvBranches.BackgroundImage = null;
            this.olvBranches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colCode,
            this.colAddress,
            this.colDescription});
            this.olvBranches.EmptyListMsg = null;
            this.olvBranches.Font = null;
            this.olvBranches.FullRowSelect = true;
            this.olvBranches.GridLines = true;
            this.olvBranches.GroupWithItemCountFormat = null;
            this.olvBranches.GroupWithItemCountSingularFormat = null;
            this.olvBranches.HideSelection = false;
            this.olvBranches.Name = "olvBranches";
            this.olvBranches.OverlayText.Text = null;
            this.olvBranches.ShowGroups = false;
            this.olvBranches.UseCompatibleStateImageBehavior = false;
            this.olvBranches.View = System.Windows.Forms.View.Details;
            this.olvBranches.SelectedIndexChanged += new System.EventHandler(this.olvBranches_SelectedIndexChanged);
            this.olvBranches.DoubleClick += new System.EventHandler(this.olvBranches_DoubleClick);
            // 
            // colName
            // 
            this.colName.AspectName = "Name";
            this.colName.GroupWithItemCountFormat = null;
            this.colName.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.colName, "colName");
            this.colName.ToolTipText = null;
            // 
            // colCode
            // 
            this.colCode.AspectName = "Code";
            this.colCode.GroupWithItemCountFormat = null;
            this.colCode.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.colCode, "colCode");
            this.colCode.ToolTipText = null;
            // 
            // colAddress
            // 
            this.colAddress.AspectName = "Address";
            this.colAddress.GroupWithItemCountFormat = null;
            this.colAddress.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.colAddress, "colAddress");
            this.colAddress.ToolTipText = null;
            // 
            // colDescription
            // 
            this.colDescription.AspectName = "Description";
            this.colDescription.GroupWithItemCountFormat = null;
            this.colDescription.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.colDescription, "colDescription");
            this.colDescription.ToolTipText = null;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AccessibleDescription = null;
            this.flowLayoutPanel1.AccessibleName = null;
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.BackgroundImage = null;
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Font = null;
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleDescription = null;
            this.btnAdd.AccessibleName = null;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Font = null;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleDescription = null;
            this.btnEdit.AccessibleName = null;
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Font = null;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleDescription = null;
            this.btnDelete.AccessibleName = null;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Font = null;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // BranchesForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "BranchesForm";
            this.Load += new System.EventHandler(this.BranchesForm_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBranches)).EndInit();
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
        private BrightIdeasSoftware.ObjectListView olvBranches;
        private BrightIdeasSoftware.OLVColumn colName;
        private BrightIdeasSoftware.OLVColumn colCode;
        private BrightIdeasSoftware.OLVColumn colAddress;
        private BrightIdeasSoftware.OLVColumn colDescription;
    }
}