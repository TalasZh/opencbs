using Octopus.GUI.UserControl;

namespace Octopus.GUI.Contracts
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
            this.buttonClearOwner = new Octopus.GUI.UserControl.SweetButton();
            this.buttonSelectOwner = new Octopus.GUI.UserControl.SweetButton();
            this.buttonAddOwner = new Octopus.GUI.UserControl.SweetButton();
            this.buttonCancel = new Octopus.GUI.UserControl.SweetButton();
            this.buttonSave = new Octopus.GUI.UserControl.SweetButton();
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
            this.groupBoxOwnerDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.groupBoxOwnerDetails.Name = "groupBoxOwnerDetails";
            this.groupBoxOwnerDetails.TabStop = false;
            // 
            // buttonClearOwner
            // 
            this.buttonClearOwner.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonClearOwner, "buttonClearOwner");
            this.buttonClearOwner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(56)))));
            this.buttonClearOwner.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            this.buttonClearOwner.Menu = null;
            this.buttonClearOwner.Name = "buttonClearOwner";
            this.buttonClearOwner.UseVisualStyleBackColor = false;
            this.buttonClearOwner.Click += new System.EventHandler(this.buttonClearOwner_Click);
            // 
            // buttonSelectOwner
            // 
            this.buttonSelectOwner.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonSelectOwner, "buttonSelectOwner");
            this.buttonSelectOwner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(56)))));
            this.buttonSelectOwner.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Search;
            this.buttonSelectOwner.Menu = null;
            this.buttonSelectOwner.Name = "buttonSelectOwner";
            this.buttonSelectOwner.UseVisualStyleBackColor = false;
            this.buttonSelectOwner.Click += new System.EventHandler(this.buttonSelectOwner_Click);
            // 
            // buttonAddOwner
            // 
            this.buttonAddOwner.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonAddOwner, "buttonAddOwner");
            this.buttonAddOwner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(56)))));
            this.buttonAddOwner.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            this.buttonAddOwner.Menu = null;
            this.buttonAddOwner.Name = "buttonAddOwner";
            this.buttonAddOwner.UseVisualStyleBackColor = false;
            this.buttonAddOwner.Click += new System.EventHandler(this.buttonAddOwner_Click);
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
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
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
        private SweetButton buttonCancel;
        private SweetButton buttonSave;
        private System.Windows.Forms.GroupBox groupBoxOwnerDetails;
        private SweetButton buttonSelectOwner;
        private SweetButton buttonAddOwner;
        private SweetButton buttonClearOwner;
    }
}