namespace Octopus.GUI.UserControl
{
    partial class SweetOkCancelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SweetOkCancelForm));
            this.btnCancel = new Octopus.GUI.UserControl.SweetButton();
            this.btnOk = new Octopus.GUI.UserControl.SweetButton();
            this.tabButtons = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnCancel.Menu = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnOk.Menu = null;
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // tabButtons
            // 
            resources.ApplyResources(this.tabButtons, "tabButtons");
            this.tabButtons.Controls.Add(this.btnCancel, 1, 0);
            this.tabButtons.Controls.Add(this.btnOk, 0, 0);
            this.tabButtons.Name = "tabButtons";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.tabButtons);
            this.panel1.Name = "panel1";
            // 
            // SweetOkCancelForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.panel1);
            this.Name = "SweetOkCancelForm";
            this.tabButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SweetButton btnCancel;
        protected SweetButton btnOk;
        private System.Windows.Forms.TableLayoutPanel tabButtons;
        private System.Windows.Forms.Panel panel1;
    }
}
