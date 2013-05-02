namespace OpenCBS.GUI.Export
{
    partial class FieldListUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FieldListUserControl));
            this.buttonProperties = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.comboBoxDefaultFields = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.regroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxHeader = new System.Windows.Forms.TextBox();
            this.olvSelectedFields = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnField = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnHeader = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnLength = new BrightIdeasSoftware.OLVColumn();
            this.textNumericUserControl1 = new OpenCBS.GUI.UserControl.TextNumericUserControl();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvSelectedFields)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonProperties
            // 
            this.buttonProperties.AccessibleDescription = null;
            this.buttonProperties.AccessibleName = null;
            resources.ApplyResources(this.buttonProperties, "buttonProperties");
            this.buttonProperties.BackgroundImage = null;
            this.buttonProperties.Font = null;
            this.buttonProperties.Name = "buttonProperties";
            this.buttonProperties.UseVisualStyleBackColor = true;
            this.buttonProperties.Click += new System.EventHandler(this.buttonProperties_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.AccessibleDescription = null;
            this.buttonDown.AccessibleName = null;
            resources.ApplyResources(this.buttonDown, "buttonDown");
            this.buttonDown.BackgroundImage = null;
            this.buttonDown.Font = null;
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.AccessibleDescription = null;
            this.buttonDelete.AccessibleName = null;
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.BackgroundImage = null;
            this.buttonDelete.Font = null;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.AccessibleDescription = null;
            this.buttonUp.AccessibleName = null;
            resources.ApplyResources(this.buttonUp, "buttonUp");
            this.buttonUp.BackgroundImage = null;
            this.buttonUp.Font = null;
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleDescription = null;
            this.buttonAdd.AccessibleName = null;
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.BackgroundImage = null;
            this.buttonAdd.Font = null;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // comboBoxDefaultFields
            // 
            this.comboBoxDefaultFields.AccessibleDescription = null;
            this.comboBoxDefaultFields.AccessibleName = null;
            resources.ApplyResources(this.comboBoxDefaultFields, "comboBoxDefaultFields");
            this.comboBoxDefaultFields.BackgroundImage = null;
            this.comboBoxDefaultFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDefaultFields.Font = null;
            this.comboBoxDefaultFields.FormattingEnabled = true;
            this.comboBoxDefaultFields.Name = "comboBoxDefaultFields";
            this.comboBoxDefaultFields.SelectedIndexChanged += new System.EventHandler(this.comboBoxDefaultFields_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.AccessibleDescription = null;
            this.contextMenuStrip1.AccessibleName = null;
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.BackgroundImage = null;
            this.contextMenuStrip1.Font = null;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regroupToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // regroupToolStripMenuItem
            // 
            this.regroupToolStripMenuItem.AccessibleDescription = null;
            this.regroupToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.regroupToolStripMenuItem, "regroupToolStripMenuItem");
            this.regroupToolStripMenuItem.BackgroundImage = null;
            this.regroupToolStripMenuItem.Name = "regroupToolStripMenuItem";
            this.regroupToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.regroupToolStripMenuItem.Click += new System.EventHandler(this.regroupToolStripMenuItem_Click);
            // 
            // textBoxHeader
            // 
            this.textBoxHeader.AccessibleDescription = null;
            this.textBoxHeader.AccessibleName = null;
            resources.ApplyResources(this.textBoxHeader, "textBoxHeader");
            this.textBoxHeader.BackgroundImage = null;
            this.textBoxHeader.Font = null;
            this.textBoxHeader.Name = "textBoxHeader";
            // 
            // olvSelectedFields
            // 
            this.olvSelectedFields.AccessibleDescription = null;
            this.olvSelectedFields.AccessibleName = null;
            resources.ApplyResources(this.olvSelectedFields, "olvSelectedFields");
            this.olvSelectedFields.AllColumns.Add(this.olvColumnField);
            this.olvSelectedFields.AllColumns.Add(this.olvColumnHeader);
            this.olvSelectedFields.AllColumns.Add(this.olvColumnLength);
            this.olvSelectedFields.BackgroundImage = null;
            this.olvSelectedFields.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.olvSelectedFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnField,
            this.olvColumnHeader,
            this.olvColumnLength});
            this.olvSelectedFields.ContextMenuStrip = this.contextMenuStrip1;
            this.olvSelectedFields.EmptyListMsg = null;
            this.olvSelectedFields.Font = null;
            this.olvSelectedFields.FullRowSelect = true;
            this.olvSelectedFields.GroupWithItemCountFormat = null;
            this.olvSelectedFields.GroupWithItemCountSingularFormat = null;
            this.olvSelectedFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.olvSelectedFields.HideSelection = false;
            this.olvSelectedFields.Name = "olvSelectedFields";
            this.olvSelectedFields.OverlayText.Text = null;
            this.olvSelectedFields.ShowGroups = false;
            this.olvSelectedFields.UseCompatibleStateImageBehavior = false;
            this.olvSelectedFields.View = System.Windows.Forms.View.Details;
            this.olvSelectedFields.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.olvSelectedFields_CellEditStarting);
            this.olvSelectedFields.SelectedIndexChanged += new System.EventHandler(this.listViewSelectedFields_SelectedIndexChanged);
            // 
            // olvColumnField
            // 
            this.olvColumnField.AspectName = "DisplayName";
            this.olvColumnField.GroupWithItemCountFormat = null;
            this.olvColumnField.GroupWithItemCountSingularFormat = null;
            this.olvColumnField.IsEditable = false;
            resources.ApplyResources(this.olvColumnField, "olvColumnField");
            this.olvColumnField.ToolTipText = null;
            // 
            // olvColumnHeader
            // 
            this.olvColumnHeader.AspectName = "Header";
            this.olvColumnHeader.GroupWithItemCountFormat = null;
            this.olvColumnHeader.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumnHeader, "olvColumnHeader");
            this.olvColumnHeader.ToolTipText = null;
            // 
            // olvColumnLength
            // 
            this.olvColumnLength.AspectName = "Length";
            this.olvColumnLength.GroupWithItemCountFormat = null;
            this.olvColumnLength.GroupWithItemCountSingularFormat = null;
            resources.ApplyResources(this.olvColumnLength, "olvColumnLength");
            this.olvColumnLength.ToolTipText = null;
            // 
            // textNumericUserControl1
            // 
            this.textNumericUserControl1.AccessibleDescription = null;
            this.textNumericUserControl1.AccessibleName = null;
            resources.ApplyResources(this.textNumericUserControl1, "textNumericUserControl1");
            this.textNumericUserControl1.BackgroundImage = null;
            this.textNumericUserControl1.Font = null;
            this.textNumericUserControl1.Name = "textNumericUserControl1";
            // 
            // FieldListUserControl
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.olvSelectedFields);
            this.Controls.Add(this.textBoxHeader);
            this.Controls.Add(this.textNumericUserControl1);
            this.Controls.Add(this.buttonProperties);
            this.Controls.Add(this.buttonDown);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUp);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.comboBoxDefaultFields);
            this.Font = null;
            this.Name = "FieldListUserControl";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvSelectedFields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonProperties;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ComboBox comboBoxDefaultFields;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem regroupToolStripMenuItem;
        private OpenCBS.GUI.UserControl.TextNumericUserControl textNumericUserControl1;
        private System.Windows.Forms.TextBox textBoxHeader;
        private BrightIdeasSoftware.ObjectListView olvSelectedFields;
        private BrightIdeasSoftware.OLVColumn olvColumnField;
        private BrightIdeasSoftware.OLVColumn olvColumnHeader;
        private BrightIdeasSoftware.OLVColumn olvColumnLength;
    }
}
