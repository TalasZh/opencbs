namespace Octopus.GUI.UserControl
{
    partial class CustomizableFieldsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomizableFieldsControl));
            this.panelAdvancedFields = new System.Windows.Forms.FlowLayoutPanel();
            this.fieldGrid = new System.Windows.Forms.PropertyGrid();
            this.panelUpdate = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonUpdate = new Octopus.GUI.UserControl.SweetButton();
            this.panelUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAdvancedFields
            // 
            this.panelAdvancedFields.AccessibleDescription = null;
            this.panelAdvancedFields.AccessibleName = null;
            resources.ApplyResources(this.panelAdvancedFields, "panelAdvancedFields");
            this.panelAdvancedFields.BackgroundImage = null;
            this.panelAdvancedFields.Font = null;
            this.panelAdvancedFields.Name = "panelAdvancedFields";
            // 
            // fieldGrid
            // 
            this.fieldGrid.AccessibleDescription = null;
            this.fieldGrid.AccessibleName = null;
            resources.ApplyResources(this.fieldGrid, "fieldGrid");
            this.fieldGrid.BackgroundImage = null;
            this.fieldGrid.Name = "fieldGrid";
            this.fieldGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.fieldGrid.ToolbarVisible = false;
            this.fieldGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.fieldGrid_SelectedGridItemChanged);
            // 
            // panelUpdate
            // 
            this.panelUpdate.AccessibleDescription = null;
            this.panelUpdate.AccessibleName = null;
            resources.ApplyResources(this.panelUpdate, "panelUpdate");
            this.panelUpdate.BackgroundImage = null;
            this.panelUpdate.Controls.Add(this.buttonUpdate);
            this.panelUpdate.Font = null;
            this.panelUpdate.Name = "panelUpdate";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.AccessibleDescription = null;
            this.buttonUpdate.AccessibleName = null;
            resources.ApplyResources(this.buttonUpdate, "buttonUpdate");
            this.buttonUpdate.BackgroundImage = null;
            this.buttonUpdate.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            this.buttonUpdate.Menu = null;
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // CustomizableFieldsControl
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.fieldGrid);
            this.Controls.Add(this.panelUpdate);
            this.Controls.Add(this.panelAdvancedFields);
            this.Font = null;
            this.Name = "CustomizableFieldsControl";
            this.Load += new System.EventHandler(this.AdvancedCustomizableFieldsControl_Load);
            this.panelUpdate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelAdvancedFields;
        private System.Windows.Forms.PropertyGrid fieldGrid;
        private System.Windows.Forms.FlowLayoutPanel panelUpdate;
        private SweetButton buttonUpdate;
    }
}
