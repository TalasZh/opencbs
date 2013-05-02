using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Contracts
{
    partial class ContractCollateralForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractCollateralForm));
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBoxOwnerDetails = new System.Windows.Forms.GroupBox();
            this.buttonClearOwner = new System.Windows.Forms.Button();
            this.buttonSelectOwner = new System.Windows.Forms.Button();
            this.buttonAddOwner = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxOwnerDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid
            // 
            resources.ApplyResources(this.propertyGrid, "propertyGrid");
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid.ToolbarVisible = false;
            this.propertyGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.propertyGrid_SelectedGridItemChanged);
            // 
            // groupBoxOwnerDetails
            // 
            this.groupBoxOwnerDetails.Controls.Add(this.buttonClearOwner);
            this.groupBoxOwnerDetails.Controls.Add(this.buttonSelectOwner);
            this.groupBoxOwnerDetails.Controls.Add(this.buttonAddOwner);
            resources.ApplyResources(this.groupBoxOwnerDetails, "groupBoxOwnerDetails");
            this.groupBoxOwnerDetails.Name = "groupBoxOwnerDetails";
            this.groupBoxOwnerDetails.TabStop = false;
            // 
            // buttonClearOwner
            //
            resources.ApplyResources(this.buttonClearOwner, "buttonClearOwner");
            this.buttonClearOwner.Name = "buttonClearOwner";
            this.buttonClearOwner.Click += new System.EventHandler(this.buttonClearOwner_Click);
            // 
            // buttonSelectOwner
            //
            resources.ApplyResources(this.buttonSelectOwner, "buttonSelectOwner");
            this.buttonSelectOwner.Name = "buttonSelectOwner";
            this.buttonSelectOwner.Click += new System.EventHandler(this.buttonSelectOwner_Click);
            // 
            // buttonAddOwner
            //
            resources.ApplyResources(this.buttonAddOwner, "buttonAddOwner");
            this.buttonAddOwner.Name = "buttonAddOwner";
            this.buttonAddOwner.Click += new System.EventHandler(this.buttonAddOwner_Click);
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
            // ContractCollateralForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxOwnerDetails);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.propertyGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContractCollateralForm";
            this.Load += new System.EventHandler(this.ContractCollateralForm_Load);
            this.groupBoxOwnerDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBoxOwnerDetails;
        private System.Windows.Forms.Button buttonSelectOwner;
        private System.Windows.Forms.Button buttonAddOwner;
        private System.Windows.Forms.Button buttonClearOwner;
    }
}