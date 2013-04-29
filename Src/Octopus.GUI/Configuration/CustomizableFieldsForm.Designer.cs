using Octopus.GUI.UserControl;

namespace Octopus.GUI.Configuration
{
    partial class CustomizableFieldsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomizableFieldsForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.olvFieldGroups = new BrightIdeasSoftware.ObjectListView();
            this.colEntityName = new BrightIdeasSoftware.OLVColumn();
            this.colFieldsNum = new BrightIdeasSoftware.OLVColumn();
            this.colFields = new BrightIdeasSoftware.OLVColumn();
            this.panelControls = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new Octopus.GUI.UserControl.SweetButton();
            this.btnEdit = new Octopus.GUI.UserControl.SweetButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvFieldGroups)).BeginInit();
            this.panelControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.olvFieldGroups);
            this.panel1.Controls.Add(this.panelControls);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            // 
            // olvFieldGroups
            // 
            this.olvFieldGroups.AccessibleDescription = null;
            this.olvFieldGroups.AccessibleName = null;
            resources.ApplyResources(this.olvFieldGroups, "olvFieldGroups");
            this.olvFieldGroups.AllColumns.Add(this.colEntityName);
            this.olvFieldGroups.AllColumns.Add(this.colFieldsNum);
            this.olvFieldGroups.AllColumns.Add(this.colFields);
            this.olvFieldGroups.BackgroundImage = null;
            this.olvFieldGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEntityName,
            this.colFieldsNum,
            this.colFields});
            this.olvFieldGroups.EmptyListMsg = null;
            this.olvFieldGroups.Font = null;
            this.olvFieldGroups.FullRowSelect = true;
            this.olvFieldGroups.GridLines = true;
            this.olvFieldGroups.GroupWithItemCountFormat = null;
            this.olvFieldGroups.GroupWithItemCountSingularFormat = null;
            this.olvFieldGroups.HideSelection = false;
            this.olvFieldGroups.Name = "olvFieldGroups";
            this.olvFieldGroups.OverlayText.Text = null;
            this.olvFieldGroups.ShowGroups = false;
            this.olvFieldGroups.UseCompatibleStateImageBehavior = false;
            this.olvFieldGroups.View = System.Windows.Forms.View.Details;
            this.olvFieldGroups.SelectedIndexChanged += new System.EventHandler(this.olvFieldGroups_SelectedIndexChanged);
            this.olvFieldGroups.DoubleClick += new System.EventHandler(this.olvFieldGroups_DoubleClick);
            // 
            // colEntityName
            // 
            this.colEntityName.AspectName = "EntityName";
            this.colEntityName.GroupWithItemCountFormat = null;
            this.colEntityName.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.colEntityName, "colEntityName");
            this.colEntityName.ToolTipText = null;
            // 
            // colFieldsNum
            // 
            this.colFieldsNum.AspectName = "FieldsNumber";
            this.colFieldsNum.GroupWithItemCountFormat = null;
            this.colFieldsNum.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.colFieldsNum, "colFieldsNum");
            this.colFieldsNum.ToolTipText = null;
            // 
            // colFields
            // 
            this.colFields.AspectName = "Fields";
            this.colFields.GroupWithItemCountFormat = null;
            this.colFields.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.colFields, "colFields");
            this.colFields.ToolTipText = null;
            // 
            // panelControls
            // 
            this.panelControls.AccessibleDescription = null;
            this.panelControls.AccessibleName = null;
            resources.ApplyResources(this.panelControls, "panelControls");
            this.panelControls.BackgroundImage = null;
            this.panelControls.Controls.Add(this.btnAdd);
            this.panelControls.Controls.Add(this.btnEdit);
            this.panelControls.Font = null;
            this.panelControls.Name = "panelControls";
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleDescription = null;
            this.btnAdd.AccessibleName = null;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.BackgroundImage = null;
            this.btnAdd.Font = null;
            this.btnAdd.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.btnAdd.Menu = null;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleDescription = null;
            this.btnEdit.AccessibleName = null;
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.BackgroundImage = null;
            this.btnEdit.Font = null;
            this.btnEdit.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Edit;
            this.btnEdit.Menu = null;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // CustomizableFieldsForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.panel1);
            this.Name = "CustomizableFieldsForm";
            this.Load += new System.EventHandler(this.AdvCustomizableFieldsForm_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvFieldGroups)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel panelControls;
        private SweetButton btnAdd;
        private SweetButton btnEdit;
        private BrightIdeasSoftware.ObjectListView olvFieldGroups;
        private BrightIdeasSoftware.OLVColumn colEntityName;
        private BrightIdeasSoftware.OLVColumn colFieldsNum;
        private BrightIdeasSoftware.OLVColumn colFields;
    }
}