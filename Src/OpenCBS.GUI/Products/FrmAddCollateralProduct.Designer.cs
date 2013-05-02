using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Products
{
    partial class FrmAddCollateralProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddCollateralProduct));
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.labelProductName = new System.Windows.Forms.Label();
            this.textBoxProductDesc = new System.Windows.Forms.TextBox();
            this.labelProductDesc = new System.Windows.Forms.Label();
            this.textBoxProductName = new System.Windows.Forms.TextBox();
            this.groupBoxPropertyDetails = new System.Windows.Forms.GroupBox();
            this.buttonAddProperty = new System.Windows.Forms.Button();
            this.comboBoxPropertyTypes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPropertyDesc = new System.Windows.Forms.TextBox();
            this.labelPropertyDesc = new System.Windows.Forms.Label();
            this.textBoxPropertyName = new System.Windows.Forms.TextBox();
            this.labelPropertyName = new System.Windows.Forms.Label();
            this.buttonDeleteProperty = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            this.groupBoxCollectionDetails = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonDeleteListItem = new System.Windows.Forms.Button();
            this.buttonAddListItem = new System.Windows.Forms.Button();
            this.textBoxListItem = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxPropertyDetails.SuspendLayout();
            this.groupBoxCollectionDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid
            // 
            resources.ApplyResources(this.propertyGrid, "propertyGrid");
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // labelProductName
            // 
            resources.ApplyResources(this.labelProductName, "labelProductName");
            this.labelProductName.BackColor = System.Drawing.Color.Transparent;
            this.labelProductName.Name = "labelProductName";
            // 
            // textBoxProductDesc
            // 
            resources.ApplyResources(this.textBoxProductDesc, "textBoxProductDesc");
            this.textBoxProductDesc.Name = "textBoxProductDesc";
            // 
            // labelProductDesc
            // 
            resources.ApplyResources(this.labelProductDesc, "labelProductDesc");
            this.labelProductDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelProductDesc.Name = "labelProductDesc";
            // 
            // textBoxProductName
            // 
            resources.ApplyResources(this.textBoxProductName, "textBoxProductName");
            this.textBoxProductName.Name = "textBoxProductName";
            // 
            // groupBoxPropertyDetails
            //
            this.groupBoxPropertyDetails.Controls.Add(this.buttonAddProperty);
            this.groupBoxPropertyDetails.Controls.Add(this.comboBoxPropertyTypes);
            this.groupBoxPropertyDetails.Controls.Add(this.label1);
            this.groupBoxPropertyDetails.Controls.Add(this.textBoxPropertyDesc);
            this.groupBoxPropertyDetails.Controls.Add(this.labelPropertyDesc);
            this.groupBoxPropertyDetails.Controls.Add(this.textBoxPropertyName);
            this.groupBoxPropertyDetails.Controls.Add(this.labelPropertyName);
            this.groupBoxPropertyDetails.Controls.Add(this.buttonDeleteProperty);
            resources.ApplyResources(this.groupBoxPropertyDetails, "groupBoxPropertyDetails");
            this.groupBoxPropertyDetails.Name = "groupBoxPropertyDetails";
            this.groupBoxPropertyDetails.TabStop = false;
            // 
            // buttonAddProperty
            //
            resources.ApplyResources(this.buttonAddProperty, "buttonAddProperty");
            this.buttonAddProperty.Name = "buttonAddProperty";
            this.buttonAddProperty.Click += new System.EventHandler(this.buttonAddProperty_Click);
            // 
            // comboBoxPropertyTypes
            // 
            this.comboBoxPropertyTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPropertyTypes.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxPropertyTypes, "comboBoxPropertyTypes");
            this.comboBoxPropertyTypes.Name = "comboBoxPropertyTypes";
            this.comboBoxPropertyTypes.SelectedValueChanged += new System.EventHandler(this.comboBoxPropertyTypes_SelectedValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // textBoxPropertyDesc
            // 
            resources.ApplyResources(this.textBoxPropertyDesc, "textBoxPropertyDesc");
            this.textBoxPropertyDesc.Name = "textBoxPropertyDesc";
            // 
            // labelPropertyDesc
            // 
            resources.ApplyResources(this.labelPropertyDesc, "labelPropertyDesc");
            this.labelPropertyDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelPropertyDesc.Name = "labelPropertyDesc";
            // 
            // textBoxPropertyName
            // 
            resources.ApplyResources(this.textBoxPropertyName, "textBoxPropertyName");
            this.textBoxPropertyName.Name = "textBoxPropertyName";
            this.textBoxPropertyName.TextChanged += new System.EventHandler(this.textBoxPropertyName_TextChanged);
            // 
            // labelPropertyName
            // 
            resources.ApplyResources(this.labelPropertyName, "labelPropertyName");
            this.labelPropertyName.BackColor = System.Drawing.Color.Transparent;
            this.labelPropertyName.Name = "labelPropertyName";
            // 
            // buttonDeleteProperty
            //
            resources.ApplyResources(this.buttonDeleteProperty, "buttonDeleteProperty");
            this.buttonDeleteProperty.Name = "buttonDeleteProperty";
            this.buttonDeleteProperty.Click += new System.EventHandler(this.buttonDeleteProperty_Click);
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
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            //
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FrmAddCollateralProduct
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxCollectionDetails);
            this.Controls.Add(this.textBoxProductDesc);
            this.Controls.Add(this.labelProductDesc);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.textBoxProductName);
            this.Controls.Add(this.labelProductName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxPropertyDetails);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddCollateralProduct";
            this.groupBoxPropertyDetails.ResumeLayout(false);
            this.groupBoxPropertyDetails.PerformLayout();
            this.groupBoxCollectionDetails.ResumeLayout(false);
            this.groupBoxCollectionDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.TextBox textBoxProductName;
        private System.Windows.Forms.GroupBox groupBoxPropertyDetails;
        private System.Windows.Forms.TextBox textBoxProductDesc;
        private System.Windows.Forms.Label labelProductDesc;
        private System.Windows.Forms.Button buttonDeleteProperty;
        private System.Windows.Forms.TextBox textBoxPropertyName;
        private System.Windows.Forms.Label labelPropertyName;
        private System.Windows.Forms.Label labelPropertyDesc;
        private System.Windows.Forms.TextBox textBoxPropertyDesc;
        private System.Windows.Forms.ComboBox comboBoxPropertyTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddProperty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.GroupBox groupBoxCollectionDetails;
        private System.Windows.Forms.Button buttonAddListItem;
        private System.Windows.Forms.TextBox textBoxListItem;
        private System.Windows.Forms.Button buttonDeleteListItem;
        private System.Windows.Forms.Label label3;
    }
}