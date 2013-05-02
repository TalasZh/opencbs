namespace OpenCBS.GUI.Tools
{
    partial class PasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordForm));
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelOldPswd = new System.Windows.Forms.Label();
            this.labelNewPswd = new System.Windows.Forms.Label();
            this.labelConfirmNewPswd = new System.Windows.Forms.Label();
            this.textBoxOldPswd = new System.Windows.Forms.TextBox();
            this.textBoxNewPswd = new System.Windows.Forms.TextBox();
            this.textBoxConfirmNewPswd = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxUserName
            // 
            resources.ApplyResources(this.textBoxUserName, "textBoxUserName");
            this.textBoxUserName.Name = "textBoxUserName";
            // 
            // labelUserName
            // 
            resources.ApplyResources(this.labelUserName, "labelUserName");
            this.labelUserName.Name = "labelUserName";
            // 
            // labelOldPswd
            // 
            resources.ApplyResources(this.labelOldPswd, "labelOldPswd");
            this.labelOldPswd.Name = "labelOldPswd";
            // 
            // labelNewPswd
            // 
            resources.ApplyResources(this.labelNewPswd, "labelNewPswd");
            this.labelNewPswd.Name = "labelNewPswd";
            // 
            // labelConfirmNewPswd
            // 
            resources.ApplyResources(this.labelConfirmNewPswd, "labelConfirmNewPswd");
            this.labelConfirmNewPswd.Name = "labelConfirmNewPswd";
            // 
            // textBoxOldPswd
            // 
            resources.ApplyResources(this.textBoxOldPswd, "textBoxOldPswd");
            this.textBoxOldPswd.Name = "textBoxOldPswd";
            // 
            // textBoxNewPswd
            // 
            resources.ApplyResources(this.textBoxNewPswd, "textBoxNewPswd");
            this.textBoxNewPswd.Name = "textBoxNewPswd";
            // 
            // textBoxConfirmNewPswd
            // 
            resources.ApplyResources(this.textBoxConfirmNewPswd, "textBoxConfirmNewPswd");
            this.textBoxConfirmNewPswd.Name = "textBoxConfirmNewPswd";
            // 
            // PasswordForm
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelConfirmNewPswd);
            this.Controls.Add(this.textBoxOldPswd);
            this.Controls.Add(this.textBoxNewPswd);
            this.Controls.Add(this.textBoxConfirmNewPswd);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.labelOldPswd);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.labelNewPswd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PasswordForm_FormClosing);
            this.Controls.SetChildIndex(this.labelNewPswd, 0);
            this.Controls.SetChildIndex(this.labelUserName, 0);
            this.Controls.SetChildIndex(this.labelOldPswd, 0);
            this.Controls.SetChildIndex(this.textBoxUserName, 0);
            this.Controls.SetChildIndex(this.textBoxConfirmNewPswd, 0);
            this.Controls.SetChildIndex(this.textBoxNewPswd, 0);
            this.Controls.SetChildIndex(this.textBoxOldPswd, 0);
            this.Controls.SetChildIndex(this.labelConfirmNewPswd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label labelOldPswd;
        private System.Windows.Forms.Label labelNewPswd;
        private System.Windows.Forms.Label labelConfirmNewPswd;
        private System.Windows.Forms.TextBox textBoxOldPswd;
        private System.Windows.Forms.TextBox textBoxNewPswd;
        private System.Windows.Forms.TextBox textBoxConfirmNewPswd;
    }
}