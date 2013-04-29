namespace Octopus.GUI.UserControl
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
            this.btnSelect = new Octopus.GUI.UserControl.SweetButton();
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
            this.btnSelect.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            resources.ApplyResources(this.btnSelect, "btnSelect");
            this.btnSelect.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.View;
            this.btnSelect.Image = global::Octopus.GUI.Properties.Resources.theme1_1_view;
            this.btnSelect.Menu = null;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.UseVisualStyleBackColor = true;
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
        private SweetButton btnSelect;

    }
}
