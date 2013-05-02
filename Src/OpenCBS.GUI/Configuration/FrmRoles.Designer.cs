using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Configuration
{
    partial class FrmRoles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRoles));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
            this.listViewRoles = new System.Windows.Forms.ListView();
            this.chRoleName = new System.Windows.Forms.ColumnHeader();
            this.chRoleDescription = new System.Windows.Forms.ColumnHeader();
            this.stateLabel = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lbRoleDescription = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblRoleTitle = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.tabControlActions = new System.Windows.Forms.TabControl();
            this.tabPageMenu = new System.Windows.Forms.TabPage();
            this.trvMenuItems = new System.Windows.Forms.TreeView();
            this.tabPageGUIActions = new System.Windows.Forms.TabPage();
            this.trwActionItems = new System.Windows.Forms.TreeView();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.trvOptions = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelRoles = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRoles = new System.Windows.Forms.Label();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainerLeft.Panel1.SuspendLayout();
            this.splitContainerLeft.Panel2.SuspendLayout();
            this.splitContainerLeft.SuspendLayout();
            this.tabControlActions.SuspendLayout();
            this.tabPageMenu.SuspendLayout();
            this.tabPageGUIActions.SuspendLayout();
            this.tabPageOptions.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackgroundImage = global::OpenCBS.GUI.Properties.Resources.theme1_1_fond_gris;
            resources.ApplyResources(this.splitContainer2.Panel1, "splitContainer2.Panel1");
            this.splitContainer2.Panel1.Controls.Add(this.splitContainerLeft);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackgroundImage = global::OpenCBS.GUI.Properties.Resources.theme1_1_fond_gris;
            resources.ApplyResources(this.splitContainer2.Panel2, "splitContainer2.Panel2");
            this.splitContainer2.Panel2.Controls.Add(this.tabControlActions);
            // 
            // splitContainerLeft
            // 
            resources.ApplyResources(this.splitContainerLeft, "splitContainerLeft");
            this.splitContainerLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerLeft.Name = "splitContainerLeft";
            // 
            // splitContainerLeft.Panel1
            // 
            this.splitContainerLeft.Panel1.Controls.Add(this.listViewRoles);
            // 
            // splitContainerLeft.Panel2
            // 
            this.splitContainerLeft.Panel2.BackgroundImage = global::OpenCBS.GUI.Properties.Resources.theme1_1_fond_gris;
            resources.ApplyResources(this.splitContainerLeft.Panel2, "splitContainerLeft.Panel2");
            this.splitContainerLeft.Panel2.Controls.Add(this.stateLabel);
            this.splitContainerLeft.Panel2.Controls.Add(this.btnNew);
            this.splitContainerLeft.Panel2.Controls.Add(this.txtDescription);
            this.splitContainerLeft.Panel2.Controls.Add(this.lbRoleDescription);
            this.splitContainerLeft.Panel2.Controls.Add(this.btnDelete);
            this.splitContainerLeft.Panel2.Controls.Add(this.lblRoleTitle);
            this.splitContainerLeft.Panel2.Controls.Add(this.btnAdd);
            this.splitContainerLeft.Panel2.Controls.Add(this.txtTitle);
            this.splitContainerLeft.Panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            // 
            // listViewRoles
            // 
            this.listViewRoles.BackColor = System.Drawing.SystemColors.Window;
            this.listViewRoles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chRoleName,
            this.chRoleDescription});
            resources.ApplyResources(this.listViewRoles, "listViewRoles");
            this.listViewRoles.FullRowSelect = true;
            this.listViewRoles.GridLines = true;
            this.listViewRoles.HideSelection = false;
            this.listViewRoles.MultiSelect = false;
            this.listViewRoles.Name = "listViewRoles";
            this.listViewRoles.UseCompatibleStateImageBehavior = false;
            this.listViewRoles.View = System.Windows.Forms.View.Details;
            this.listViewRoles.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewRolesItemCheck);
            this.listViewRoles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewRolesItemSelectionChanged1);
            this.listViewRoles.Click += new System.EventHandler(this.ListViewRolesClick);
            // 
            // chRoleName
            // 
            resources.ApplyResources(this.chRoleName, "chRoleName");
            // 
            // chRoleDescription
            // 
            resources.ApplyResources(this.chRoleDescription, "chRoleDescription");
            // 
            // stateLabel
            //
            resources.ApplyResources(this.stateLabel, "stateLabel");
            this.stateLabel.Name = "stateLabel";
            // 
            // btnNew
            //
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Name = "btnNew";
            this.btnNew.Click += new System.EventHandler(this.BtnNewClick);
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            // 
            // lbRoleDescription
            // 
            resources.ApplyResources(this.lbRoleDescription, "lbRoleDescription");
            this.lbRoleDescription.Name = "lbRoleDescription";
            // 
            // btnDelete
            //
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.BtnDeleteClick);
            // 
            // lblRoleTitle
            // 
            resources.ApplyResources(this.lblRoleTitle, "lblRoleTitle");
            this.lblRoleTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblRoleTitle.Name = "lblRoleTitle";
            // 
            // btnAdd
            //
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // txtTitle
            // 
            resources.ApplyResources(this.txtTitle, "txtTitle");
            this.txtTitle.Name = "txtTitle";
            // 
            // tabControlActions
            // 
            this.tabControlActions.Controls.Add(this.tabPageMenu);
            this.tabControlActions.Controls.Add(this.tabPageGUIActions);
            this.tabControlActions.Controls.Add(this.tabPageOptions);
            resources.ApplyResources(this.tabControlActions, "tabControlActions");
            this.tabControlActions.Name = "tabControlActions";
            this.tabControlActions.SelectedIndex = 0;
            // 
            // tabPageMenu
            // 
            this.tabPageMenu.Controls.Add(this.trvMenuItems);
            resources.ApplyResources(this.tabPageMenu, "tabPageMenu");
            this.tabPageMenu.Name = "tabPageMenu";
            // 
            // trvMenuItems
            // 
            this.trvMenuItems.CheckBoxes = true;
            resources.ApplyResources(this.trvMenuItems, "trvMenuItems");
            this.trvMenuItems.Name = "trvMenuItems";
            this.trvMenuItems.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewMenuItemsAfterCheck);
            // 
            // tabPageGUIActions
            // 
            this.tabPageGUIActions.Controls.Add(this.trwActionItems);
            resources.ApplyResources(this.tabPageGUIActions, "tabPageGUIActions");
            this.tabPageGUIActions.Name = "tabPageGUIActions";
            // 
            // trwActionItems
            // 
            this.trwActionItems.CheckBoxes = true;
            resources.ApplyResources(this.trwActionItems, "trwActionItems");
            this.trwActionItems.Name = "trwActionItems";
            this.trwActionItems.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewActionItemsAfterCheck);
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.Controls.Add(this.trvOptions);
            resources.ApplyResources(this.tabPageOptions, "tabPageOptions");
            this.tabPageOptions.Name = "tabPageOptions";
            // 
            // trvOptions
            // 
            this.trvOptions.CheckBoxes = true;
            resources.ApplyResources(this.trvOptions, "trvOptions");
            this.trvOptions.Name = "trvOptions";
            this.trvOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("trvOptions.Nodes"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("trvOptions.Nodes1"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("trvOptions.Nodes2")))});
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.labelRoles);
            this.splitContainer1.Panel1.Controls.Add(this.buttonAdd);
            // 
            // labelRoles
            // 
            this.labelRoles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.labelRoles, "labelRoles");
            this.labelRoles.Name = "labelRoles";
            // 
            // buttonAdd
            //
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.Name = "buttonAdd";
            // 
            // splitContainerMain
            // 
            resources.ApplyResources(this.splitContainerMain, "splitContainerMain");
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.btnCancel);
            this.splitContainerMain.Panel1.Controls.Add(this.lblRoles);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainer2);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // lblRoles
            // 
            this.lblRoles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.lblRoles, "lblRoles");
            this.lblRoles.Name = "lblRoles";
            // 
            // FrmRoles
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "FrmRoles";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainerLeft.Panel1.ResumeLayout(false);
            this.splitContainerLeft.Panel2.ResumeLayout(false);
            this.splitContainerLeft.Panel2.PerformLayout();
            this.splitContainerLeft.ResumeLayout(false);
            this.tabControlActions.ResumeLayout(false);
            this.tabPageMenu.ResumeLayout(false);
            this.tabPageGUIActions.ResumeLayout(false);
            this.tabPageOptions.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelRoles;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lblRoleTitle;
        private System.Windows.Forms.ListView listViewRoles;
        private System.Windows.Forms.ColumnHeader chRoleName;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblRoles;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TreeView trvMenuItems;
        private System.Windows.Forms.TabControl tabControlActions;
        private System.Windows.Forms.TabPage tabPageMenu;
        private System.Windows.Forms.TabPage tabPageGUIActions;
        private System.Windows.Forms.SplitContainer splitContainerLeft;
        private System.Windows.Forms.TreeView trwActionItems;
        private System.Windows.Forms.ColumnHeader chRoleDescription;
        private System.Windows.Forms.Label lbRoleDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TabPage tabPageOptions;
        private System.Windows.Forms.TreeView trvOptions;
        private System.Windows.Forms.Label stateLabel;
    }
}