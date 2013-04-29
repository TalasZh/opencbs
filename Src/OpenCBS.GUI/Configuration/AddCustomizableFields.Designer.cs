using Octopus.GUI.UserControl;

namespace Octopus.GUI.Configuration
{
    partial class AddCustomizableFields
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCustomizableFields));
            this.fieldGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBoxPropertyDetails = new System.Windows.Forms.GroupBox();
            this.checkBoxNewUnique = new System.Windows.Forms.CheckBox();
            this.checkBoxNewMandatory = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonAddField = new System.Windows.Forms.Button();
            this.comboBoxFieldTypes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFieldDesc = new System.Windows.Forms.TextBox();
            this.labelPropertyDesc = new System.Windows.Forms.Label();
            this.textBoxFieldName = new System.Windows.Forms.TextBox();
            this.labelPropertyName = new System.Windows.Forms.Label();
            this.buttonDeleteField = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            this.groupBoxCollectionDetails = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonDeleteListItem = new System.Windows.Forms.Button();
            this.buttonAddListItem = new System.Windows.Forms.Button();
            this.textBoxListItem = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxEntities = new System.Windows.Forms.ComboBox();
            this.groupBoxAttributes = new System.Windows.Forms.GroupBox();
            this.checkBoxUnique = new System.Windows.Forms.CheckBox();
            this.checkBoxMandatory = new System.Windows.Forms.CheckBox();
            this.groupBoxPropertyDetails.SuspendLayout();
            this.groupBoxCollectionDetails.SuspendLayout();
            this.groupBoxAttributes.SuspendLayout();
            this.SuspendLayout();
            // 
            // fieldGrid
            // 
            resources.ApplyResources(this.fieldGrid, "fieldGrid");
            this.fieldGrid.Name = "fieldGrid";
            this.fieldGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.fieldGrid.ToolbarVisible = false;
            this.fieldGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.fieldGrid_SelectedGridItemChanged);
            // 
            // groupBoxPropertyDetails
            //
            this.groupBoxPropertyDetails.Controls.Add(this.checkBoxNewUnique);
            this.groupBoxPropertyDetails.Controls.Add(this.checkBoxNewMandatory);
            this.groupBoxPropertyDetails.Controls.Add(this.label4);
            this.groupBoxPropertyDetails.Controls.Add(this.buttonAddField);
            this.groupBoxPropertyDetails.Controls.Add(this.comboBoxFieldTypes);
            this.groupBoxPropertyDetails.Controls.Add(this.label1);
            this.groupBoxPropertyDetails.Controls.Add(this.textBoxFieldDesc);
            this.groupBoxPropertyDetails.Controls.Add(this.labelPropertyDesc);
            this.groupBoxPropertyDetails.Controls.Add(this.textBoxFieldName);
            this.groupBoxPropertyDetails.Controls.Add(this.labelPropertyName);
            this.groupBoxPropertyDetails.Controls.Add(this.buttonDeleteField);
            resources.ApplyResources(this.groupBoxPropertyDetails, "groupBoxPropertyDetails");
            this.groupBoxPropertyDetails.Name = "groupBoxPropertyDetails";
            this.groupBoxPropertyDetails.TabStop = false;
            // 
            // checkBoxNewUnique
            // 
            resources.ApplyResources(this.checkBoxNewUnique, "checkBoxNewUnique");
            this.checkBoxNewUnique.Name = "checkBoxNewUnique";
            // 
            // checkBoxNewMandatory
            // 
            resources.ApplyResources(this.checkBoxNewMandatory, "checkBoxNewMandatory");
            this.checkBoxNewMandatory.Name = "checkBoxNewMandatory";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // buttonAddField
            //
            resources.ApplyResources(this.buttonAddField, "buttonAddField");
            this.buttonAddField.Name = "buttonAddField";
            this.buttonAddField.Click += new System.EventHandler(this.buttonAddProperty_Click);
            // 
            // comboBoxFieldTypes
            // 
            this.comboBoxFieldTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFieldTypes.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxFieldTypes, "comboBoxFieldTypes");
            this.comboBoxFieldTypes.Name = "comboBoxFieldTypes";
            this.comboBoxFieldTypes.SelectedValueChanged += new System.EventHandler(this.comboBoxPropertyTypes_SelectedValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // textBoxFieldDesc
            // 
            resources.ApplyResources(this.textBoxFieldDesc, "textBoxFieldDesc");
            this.textBoxFieldDesc.Name = "textBoxFieldDesc";
            this.textBoxFieldDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFieldName_KeyPress);
            // 
            // labelPropertyDesc
            // 
            resources.ApplyResources(this.labelPropertyDesc, "labelPropertyDesc");
            this.labelPropertyDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelPropertyDesc.Name = "labelPropertyDesc";
            // 
            // textBoxFieldName
            // 
            resources.ApplyResources(this.textBoxFieldName, "textBoxFieldName");
            this.textBoxFieldName.Name = "textBoxFieldName";
            this.textBoxFieldName.TextChanged += new System.EventHandler(this.textBoxPropertyName_TextChanged);
            this.textBoxFieldName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFieldName_KeyPress);
            // 
            // labelPropertyName
            // 
            resources.ApplyResources(this.labelPropertyName, "labelPropertyName");
            this.labelPropertyName.BackColor = System.Drawing.Color.Transparent;
            this.labelPropertyName.Name = "labelPropertyName";
            // 
            // buttonDeleteField
            //
            resources.ApplyResources(this.buttonDeleteField, "buttonDeleteField");
            this.buttonDeleteField.Name = "buttonDeleteField";
            this.buttonDeleteField.Click += new System.EventHandler(this.ButtonDeletePropertyClick);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            resources.ApplyResources(this.listBox, "listBox");
            this.listBox.Name = "listBox";
            // 
            // groupBoxCollectionDetails
            // 
            this.groupBoxCollectionDetails.Controls.Add(this.label3);
            this.groupBoxCollectionDetails.Controls.Add(this.buttonDeleteListItem);
            this.groupBoxCollectionDetails.Controls.Add(this.buttonAddListItem);
            this.groupBoxCollectionDetails.Controls.Add(this.textBoxListItem);
            this.groupBoxCollectionDetails.Controls.Add(this.listBox);
            this.groupBoxCollectionDetails.Controls.Add(this.label2);
            resources.ApplyResources(this.groupBoxCollectionDetails, "groupBoxCollectionDetails");
            this.groupBoxCollectionDetails.Name = "groupBoxCollectionDetails";
            this.groupBoxCollectionDetails.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // buttonDeleteListItem
            //
            resources.ApplyResources(this.buttonDeleteListItem, "buttonDeleteListItem");
            this.buttonDeleteListItem.Name = "buttonDeleteListItem";
            this.buttonDeleteListItem.Click += new System.EventHandler(this.buttonDeleteListItem_Click);
            // 
            // buttonAddListItem
            //
            resources.ApplyResources(this.buttonAddListItem, "buttonAddListItem");
            this.buttonAddListItem.Name = "buttonAddListItem";
            this.buttonAddListItem.Click += new System.EventHandler(this.buttonAddListItem_Click);
            // 
            // textBoxListItem
            // 
            resources.ApplyResources(this.textBoxListItem, "textBoxListItem");
            this.textBoxListItem.Name = "textBoxListItem";
            this.textBoxListItem.TextChanged += new System.EventHandler(this.textBoxListItem_TextChanged);
            // 
            // buttonCancel
            //
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // buttonSave
            //
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // comboBoxEntities
            // 
            this.comboBoxEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxEntities, "comboBoxEntities");
            this.comboBoxEntities.FormattingEnabled = true;
            this.comboBoxEntities.Name = "comboBoxEntities";
            // 
            // groupBoxAttributes
            // 
            this.groupBoxAttributes.Controls.Add(this.checkBoxUnique);
            this.groupBoxAttributes.Controls.Add(this.checkBoxMandatory);
            resources.ApplyResources(this.groupBoxAttributes, "groupBoxAttributes");
            this.groupBoxAttributes.Name = "groupBoxAttributes";
            this.groupBoxAttributes.TabStop = false;
            // 
            // checkBoxUnique
            // 
            resources.ApplyResources(this.checkBoxUnique, "checkBoxUnique");
            this.checkBoxUnique.Name = "checkBoxUnique";
            // 
            // checkBoxMandatory
            // 
            resources.ApplyResources(this.checkBoxMandatory, "checkBoxMandatory");
            this.checkBoxMandatory.Name = "checkBoxMandatory";
            // 
            // AddCustomizableFields
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxAttributes);
            this.Controls.Add(this.comboBoxEntities);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBoxCollectionDetails);
            this.Controls.Add(this.fieldGrid);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxPropertyDetails);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCustomizableFields";
            this.groupBoxPropertyDetails.ResumeLayout(false);
            this.groupBoxPropertyDetails.PerformLayout();
            this.groupBoxCollectionDetails.ResumeLayout(false);
            this.groupBoxCollectionDetails.PerformLayout();
            this.groupBoxAttributes.ResumeLayout(false);
            this.groupBoxAttributes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid fieldGrid;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBoxPropertyDetails;
        private System.Windows.Forms.Button buttonDeleteField;
        private System.Windows.Forms.TextBox textBoxFieldName;
        private System.Windows.Forms.Label labelPropertyName;
        private System.Windows.Forms.Label labelPropertyDesc;
        private System.Windows.Forms.TextBox textBoxFieldDesc;
        private System.Windows.Forms.ComboBox comboBoxFieldTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.GroupBox groupBoxCollectionDetails;
        private System.Windows.Forms.Button buttonAddListItem;
        private System.Windows.Forms.TextBox textBoxListItem;
        private System.Windows.Forms.Button buttonDeleteListItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxNewUnique;
        private System.Windows.Forms.CheckBox checkBoxNewMandatory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxEntities;
        private System.Windows.Forms.GroupBox groupBoxAttributes;
        private System.Windows.Forms.CheckBox checkBoxUnique;
        private System.Windows.Forms.CheckBox checkBoxMandatory;
    }
}