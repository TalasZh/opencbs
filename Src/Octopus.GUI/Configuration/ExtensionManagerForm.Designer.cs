namespace Octopus.GUI.Configuration
{
    partial class ExtensionManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtensionManagerForm));
            this.olvExtensions = new BrightIdeasSoftware.ObjectListView();
            this.olvcExtensionName = new BrightIdeasSoftware.OLVColumn();
            this.olvcVendor = new BrightIdeasSoftware.OLVColumn();
            this.olvcOctopusVersion = new BrightIdeasSoftware.OLVColumn();
            this.olvcVersion = new BrightIdeasSoftware.OLVColumn();
            this.olvcFileSize = new BrightIdeasSoftware.OLVColumn();
            this.btnAdd = new Octopus.GUI.UserControl.SweetButton();
            this.ofdExtension = new System.Windows.Forms.OpenFileDialog();
            this.btnDelete = new Octopus.GUI.UserControl.SweetButton();
            this.lblChangeEffectOnRestart = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.olvExtensions)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // olvExtensions
            // 
            this.olvExtensions.AllColumns.Add(this.olvcExtensionName);
            this.olvExtensions.AllColumns.Add(this.olvcVendor);
            this.olvExtensions.AllColumns.Add(this.olvcOctopusVersion);
            this.olvExtensions.AllColumns.Add(this.olvcVersion);
            this.olvExtensions.AllColumns.Add(this.olvcFileSize);
            this.olvExtensions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcExtensionName,
            this.olvcVendor,
            this.olvcOctopusVersion,
            this.olvcVersion,
            this.olvcFileSize});
            resources.ApplyResources(this.olvExtensions, "olvExtensions");
            this.olvExtensions.FullRowSelect = true;
            this.olvExtensions.GridLines = true;
            this.olvExtensions.Name = "olvExtensions";
            this.olvExtensions.ShowGroups = false;
            this.olvExtensions.UseCompatibleStateImageBehavior = false;
            this.olvExtensions.View = System.Windows.Forms.View.Details;
            this.olvExtensions.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.OlvExtensionsFormatRow);
            // 
            // olvcExtensionName
            // 
            this.olvcExtensionName.AspectName = "ExtensionName";
            this.olvcExtensionName.AutoCompleteEditor = false;
            this.olvcExtensionName.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcExtensionName.Groupable = false;
            this.olvcExtensionName.IsEditable = false;
            resources.ApplyResources(this.olvcExtensionName, "olvcExtensionName");
            // 
            // olvcVendor
            // 
            this.olvcVendor.AspectName = "Vendor";
            this.olvcVendor.AutoCompleteEditor = false;
            this.olvcVendor.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcVendor.IsEditable = false;
            resources.ApplyResources(this.olvcVendor, "olvcVendor");
            // 
            // olvcOctopusVersion
            // 
            this.olvcOctopusVersion.AspectName = "OctopusVersion";
            this.olvcOctopusVersion.Groupable = false;
            this.olvcOctopusVersion.IsEditable = false;
            resources.ApplyResources(this.olvcOctopusVersion, "olvcOctopusVersion");
            // 
            // olvcVersion
            // 
            this.olvcVersion.AspectName = "Version";
            this.olvcVersion.Groupable = false;
            resources.ApplyResources(this.olvcVersion, "olvcVersion");
            this.olvcVersion.UseFiltering = false;
            // 
            // olvcFileSize
            // 
            this.olvcFileSize.AspectName = "FileSize";
            this.olvcFileSize.AutoCompleteEditor = false;
            this.olvcFileSize.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcFileSize.Sortable = false;
            resources.ApplyResources(this.olvcFileSize, "olvcFileSize");
            // 
            // btnAdd
            // 
            this.btnAdd.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.New;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Menu = null;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // ofdExtension
            // 
            resources.ApplyResources(this.ofdExtension, "ofdExtension");
            // 
            // btnDelete
            // 
            this.btnDelete.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Delete;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Menu = null;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDeleteClick);
            // 
            // lblChangeEffectOnRestart
            // 
            resources.ApplyResources(this.lblChangeEffectOnRestart, "lblChangeEffectOnRestart");
            this.lblChangeEffectOnRestart.ForeColor = System.Drawing.Color.DarkRed;
            this.lblChangeEffectOnRestart.Name = "lblChangeEffectOnRestart";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.btnAdd);
            this.pnlRight.Controls.Add(this.btnDelete);
            resources.ApplyResources(this.pnlRight, "pnlRight");
            this.pnlRight.Name = "pnlRight";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.lblChangeEffectOnRestart);
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            // 
            // ExtensionManagerForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.olvExtensions);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlRight);
            this.Name = "ExtensionManagerForm";
            this.Controls.SetChildIndex(this.pnlRight, 0);
            this.Controls.SetChildIndex(this.pnlBottom, 0);
            this.Controls.SetChildIndex(this.olvExtensions, 0);
            ((System.ComponentModel.ISupportInitialize)(this.olvExtensions)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvExtensions;
        private BrightIdeasSoftware.OLVColumn olvcExtensionName;
        private Octopus.GUI.UserControl.SweetButton btnAdd;
        private System.Windows.Forms.OpenFileDialog ofdExtension;
        private BrightIdeasSoftware.OLVColumn olvcFileSize;
        private Octopus.GUI.UserControl.SweetButton btnDelete;
        private System.Windows.Forms.Label lblChangeEffectOnRestart;
        private BrightIdeasSoftware.OLVColumn olvcOctopusVersion;
        private BrightIdeasSoftware.OLVColumn olvcVersion;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlBottom;
        private BrightIdeasSoftware.OLVColumn olvcVendor;

    }
}