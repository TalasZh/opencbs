namespace OpenCBS.GUI.UserControl
{
    partial class EconomicActivityControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EconomicActivityControl));
            this.txbActivity = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbActivity
            // 
            resources.ApplyResources(this.txbActivity, "txbActivity");
            this.txbActivity.Name = "txbActivity";
            this.txbActivity.ReadOnly = true;
            this.txbActivity.Click += new System.EventHandler(this.BtnSelectClick);
            // 
            // btnSelect
            //
            resources.ApplyResources(this.btnSelect, "btnSelect");
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Click += new System.EventHandler(this.BtnSelectClick);
            // 
            // EconomicActivityControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txbActivity);
            this.Controls.Add(this.btnSelect);
            this.Name = "EconomicActivityControl";
            this.Load += new System.EventHandler(this.EconomicActivityControlLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbActivity;
        private System.Windows.Forms.Button btnSelect;

    }
}
