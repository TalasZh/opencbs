using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Configuration
{
    partial class FrmSettingsImportExport
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettingsImportExport));
            this.tvSettings = new System.Windows.Forms.TreeView();
            this.imagesSettings = new System.Windows.Forms.ImageList(this.components);
            this.cbPackages = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvSettings
            // 
            resources.ApplyResources(this.tvSettings, "tvSettings");
            this.tvSettings.CheckBoxes = true;
            this.tvSettings.ImageList = this.imagesSettings;
            this.tvSettings.Name = "tvSettings";
            this.tvSettings.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvSettings_AfterCheck);
            // 
            // imagesSettings
            // 
            this.imagesSettings.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesSettings.ImageStream")));
            this.imagesSettings.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesSettings.Images.SetKeyName(0, "PCK");
            this.imagesSettings.Images.SetKeyName(1, "SET");
            this.imagesSettings.Images.SetKeyName(2, "GRP");
            this.imagesSettings.Images.SetKeyName(3, "DEF");
            this.imagesSettings.Images.SetKeyName(4, "BAD");
            // 
            // cbPackages
            // 
            resources.ApplyResources(this.cbPackages, "cbPackages");
            this.cbPackages.Name = "cbPackages";
            this.cbPackages.CheckedChanged += new System.EventHandler(this.cbPackages_CheckedChanged);
            // 
            // groupBox1
            //
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            // 
            // FrmSettingsImportExport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbPackages);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tvSettings);
            this.Name = "FrmSettingsImportExport";
            this.Load += new System.EventHandler(this.frmSettingsImportExport_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbPackages;
        private System.Windows.Forms.ImageList imagesSettings;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}