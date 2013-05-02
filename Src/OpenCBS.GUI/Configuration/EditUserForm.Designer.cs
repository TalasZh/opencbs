namespace OpenCBS.GUI.Configuration
{
    partial class EditUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditUserForm));
            this.olvColumn1 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn2 = new BrightIdeasSoftware.OLVColumn();
            this.pageBranches = new System.Windows.Forms.TabPage();
            this.chkSelectAllBranches = new System.Windows.Forms.CheckBox();
            this.olvBranches = new BrightIdeasSoftware.ObjectListView();
            this.colBranchName = new BrightIdeasSoftware.OLVColumn();
            this.pageSubordinates = new System.Windows.Forms.TabPage();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.olvUsers = new BrightIdeasSoftware.ObjectListView();
            this.colUsers_User = new BrightIdeasSoftware.OLVColumn();
            this.tabUser = new System.Windows.Forms.TabControl();
            this.pageBranches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBranches)).BeginInit();
            this.pageSubordinates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvUsers)).BeginInit();
            this.tabUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            resources.ApplyResources(this.olvColumn1, "olvColumn1");
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Name";
            resources.ApplyResources(this.olvColumn2, "olvColumn2");
            // 
            // pageBranches
            // 
            this.pageBranches.Controls.Add(this.olvBranches);
            this.pageBranches.Controls.Add(this.chkSelectAllBranches);
            resources.ApplyResources(this.pageBranches, "pageBranches");
            this.pageBranches.Name = "pageBranches";
            // 
            // chkSelectAllBranches
            // 
            resources.ApplyResources(this.chkSelectAllBranches, "chkSelectAllBranches");
            this.chkSelectAllBranches.Name = "chkSelectAllBranches";
            this.chkSelectAllBranches.Click += new System.EventHandler(this.OnSelectAllClick);
            // 
            // olvBranches
            // 
            this.olvBranches.AllColumns.Add(this.colBranchName);
            this.olvBranches.CheckBoxes = true;
            this.olvBranches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colBranchName});
            resources.ApplyResources(this.olvBranches, "olvBranches");
            this.olvBranches.FullRowSelect = true;
            this.olvBranches.GridLines = true;
            this.olvBranches.HideSelection = false;
            this.olvBranches.Name = "olvBranches";
            this.olvBranches.ShowGroups = false;
            this.olvBranches.UseCompatibleStateImageBehavior = false;
            this.olvBranches.View = System.Windows.Forms.View.Details;
            this.olvBranches.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.OnUserChecked);
            // 
            // colBranchName
            // 
            this.colBranchName.AspectName = "Name";
            resources.ApplyResources(this.colBranchName, "colBranchName");
            // 
            // pageSubordinates
            // 
            this.pageSubordinates.Controls.Add(this.olvUsers);
            this.pageSubordinates.Controls.Add(this.chkSelectAll);
            resources.ApplyResources(this.pageSubordinates, "pageSubordinates");
            this.pageSubordinates.Name = "pageSubordinates";
            // 
            // chkSelectAll
            // 
            resources.ApplyResources(this.chkSelectAll, "chkSelectAll");
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.ThreeState = true;
            this.chkSelectAll.Click += new System.EventHandler(this.OnSelectAllClick);
            // 
            // olvUsers
            // 
            this.olvUsers.AllColumns.Add(this.colUsers_User);
            this.olvUsers.CheckBoxes = true;
            this.olvUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colUsers_User});
            resources.ApplyResources(this.olvUsers, "olvUsers");
            this.olvUsers.FullRowSelect = true;
            this.olvUsers.GridLines = true;
            this.olvUsers.HasCollapsibleGroups = false;
            this.olvUsers.Name = "olvUsers";
            this.olvUsers.ShowGroups = false;
            this.olvUsers.UseCompatibleStateImageBehavior = false;
            this.olvUsers.View = System.Windows.Forms.View.Details;
            this.olvUsers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.OnUserChecked);
            // 
            // colUsers_User
            // 
            this.colUsers_User.AspectName = "Name";
            resources.ApplyResources(this.colUsers_User, "colUsers_User");
            // 
            // tabUser
            // 
            this.tabUser.Controls.Add(this.pageSubordinates);
            this.tabUser.Controls.Add(this.pageBranches);
            resources.ApplyResources(this.tabUser, "tabUser");
            this.tabUser.Name = "tabUser";
            this.tabUser.SelectedIndex = 0;
            this.tabUser.SelectedIndexChanged += new System.EventHandler(this.tabUser_SelectedIndexChanged);
            // 
            // EditUserForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditUserForm";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Controls.SetChildIndex(this.tabUser, 0);
            this.pageBranches.ResumeLayout(false);
            this.pageBranches.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBranches)).EndInit();
            this.pageSubordinates.ResumeLayout(false);
            this.pageSubordinates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvUsers)).EndInit();
            this.tabUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.TabPage pageBranches;
        private BrightIdeasSoftware.ObjectListView olvBranches;
        private BrightIdeasSoftware.OLVColumn colBranchName;
        private System.Windows.Forms.CheckBox chkSelectAllBranches;
        private System.Windows.Forms.TabPage pageSubordinates;
        private BrightIdeasSoftware.ObjectListView olvUsers;
        private BrightIdeasSoftware.OLVColumn colUsers_User;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.TabControl tabUser;
    }
}
