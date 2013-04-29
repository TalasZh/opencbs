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
            this.buttonAddField = new Octopus.GUI.UserControl.SweetButton();
            this.comboBoxFieldTypes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFieldDesc = new System.Windows.Forms.TextBox();
            this.labelPropertyDesc = new System.Windows.Forms.Label();
            this.textBoxFieldName = new System.Windows.Forms.TextBox();
            this.labelPropertyName = new System.Windows.Forms.Label();
            this.buttonDeleteField = new Octopus.GUI.UserControl.SweetButton();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            this.groupBoxCollectionDetails = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonDeleteListItem = new Octopus.GUI.UserControl.SweetButton();
            this.buttonAddListItem = new Octopus.GUI.UserControl.SweetButton();
            this.textBoxListItem = new System.Windows.Forms.TextBox();
            this.buttonCancel = new Octopus.GUI.UserControl.SweetButton();
            this.buttonSave = new Octopus.GUI.UserControl.SweetButton();
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
            this.groupBoxPropertyDetails.BackColor = System.Drawing.Color.Transparent;
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
            this.groupBoxPropertyDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.groupBoxPropertyDetails.Name = "groupBoxPropertyDetails";
            this.groupBoxPropertyDetails.TabStop = false;
            // 
            // checkBoxNewUnique
            // 
            resources.ApplyResources(this.checkBoxNewUnique, "checkBoxNewUnique");
            this.checkBoxNewUnique.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxNewUnique.Name = "checkBoxNewUnique";
            this.checkBoxNewUnique.UseVisualStyleBackColor = true;
            // 
            // checkBoxNewMandatory
            // 
            resources.ApplyResources(this.checkBoxNewMandatory, "checkBoxNewMandatory");
            this.checkBoxNewMandatory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxNewMandatory.Name = "checkBoxNewMandatory";
            this.checkBoxNewMandatory.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.label4.Name = "label4";
            // 
            // buttonAddField
            // 
            this.buttonAddField.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonAddField, "buttonAddField");
            this.buttonAddField.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(56)))));
            this.buttonAddField.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.buttonAddField.Menu = null;
            this.buttonAddField.Name = "buttonAddField";
            this.buttonAddField.UseVisualStyleBackColor = false;
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
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
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
            this.labelPropertyDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
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
            this.labelPropertyName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelPropertyName.Name = "labelPropertyName";
            // 
            // buttonDeleteField
            // 
            this.buttonDeleteField.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonDeleteField, "buttonDeleteField");
            this.buttonDeleteField.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(56)))));
            this.buttonDeleteField.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.buttonDeleteField.Menu = null;
            this.buttonDeleteField.Name = "buttonDeleteField";
            this.buttonDeleteField.UseVisualStyleBackColor = false;
            this.buttonDeleteField.Click += new System.EventHandler(this.ButtonDeletePropertyClick);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
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
            this.groupBoxCollectionDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.groupBoxCollectionDetails.Name = "groupBoxCollectionDetails";
            this.groupBoxCollectionDetails.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.label3.Name = "label3";
            // 
            // buttonDeleteListItem
            // 
            this.buttonDeleteListItem.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonDeleteListItem, "buttonDeleteListItem");
            this.buttonDeleteListItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(56)))));
            this.buttonDeleteListItem.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.buttonDeleteListItem.Menu = null;
            this.buttonDeleteListItem.Name = "buttonDeleteListItem";
            this.buttonDeleteListItem.UseVisualStyleBackColor = false;
            this.buttonDeleteListItem.Click += new System.EventHandler(this.buttonDeleteListItem_Click);
            // 
            // buttonAddListItem
            // 
            this.buttonAddListItem.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonAddListItem, "buttonAddListItem");
            this.buttonAddListItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(56)))));
            this.buttonAddListItem.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.buttonAddListItem.Menu = null;
            this.buttonAddListItem.Name = "buttonAddListItem";
            this.buttonAddListItem.UseVisualStyleBackColor = false;
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
            this.buttonCancel.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(56)))));
            this.buttonCancel.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.buttonCancel.Menu = null;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(56)))));
            this.buttonSave.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            this.buttonSave.Menu = null;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
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
            this.groupBoxAttributes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.groupBoxAttributes.Name = "groupBoxAttributes";
            this.groupBoxAttributes.TabStop = false;
            // 
            // checkBoxUnique
            // 
            resources.ApplyResources(this.checkBoxUnique, "checkBoxUnique");
            this.checkBoxUnique.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxUnique.Name = "checkBoxUnique";
            this.checkBoxUnique.UseVisualStyleBackColor = true;
            // 
            // checkBoxMandatory
            // 
            resources.ApplyResources(this.checkBoxMandatory, "checkBoxMandatory");
            this.checkBoxMandatory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxMandatory.Name = "checkBoxMandatory";
            this.checkBoxMandatory.UseVisualStyleBackColor = true;
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
        private SweetButton buttonCancel;
        private SweetButton buttonSave;
        private System.Windows.Forms.GroupBox groupBoxPropertyDetails;
        private SweetButton buttonDeleteField;
        private System.Windows.Forms.TextBox textBoxFieldName;
        private System.Windows.Forms.Label labelPropertyName;
        private System.Windows.Forms.Label labelPropertyDesc;
        private System.Windows.Forms.TextBox textBoxFieldDesc;
        private System.Windows.Forms.ComboBox comboBoxFieldTypes;
        private System.Windows.Forms.Label label1;
        private SweetButton buttonAddField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.GroupBox groupBoxCollectionDetails;
        private SweetButton buttonAddListItem;
        private System.Windows.Forms.TextBox textBoxListItem;
        private SweetButton buttonDeleteListItem;
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