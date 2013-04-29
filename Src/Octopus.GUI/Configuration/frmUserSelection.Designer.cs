using Octopus.GUI.UserControl;

namespace Octopus.GUI.Configuration
{
    partial class frmUserSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserSelection));
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.lbLoanOfficers = new System.Windows.Forms.Label();
            this.btnAssing = new Octopus.GUI.UserControl.SweetButton();
            this.btnCancel = new Octopus.GUI.UserControl.SweetButton();
            this.SuspendLayout();
            // 
            // cbUsers
            // 
            this.cbUsers.FormattingEnabled = true;
            resources.ApplyResources(this.cbUsers, "cbUsers");
            this.cbUsers.Name = "cbUsers";
            // 
            // lbLoanOfficers
            // 
            resources.ApplyResources(this.lbLoanOfficers, "lbLoanOfficers");
            this.lbLoanOfficers.BackColor = System.Drawing.Color.Transparent;
            this.lbLoanOfficers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lbLoanOfficers.Name = "lbLoanOfficers";
            // 
            // btnAssing
            // 
            this.btnAssing.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnAssing, "btnAssing");
            this.btnAssing.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAssing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnAssing.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            this.btnAssing.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_validity;
            this.btnAssing.Menu = null;
            this.btnAssing.Name = "btnAssing";
            this.btnAssing.UseVisualStyleBackColor = false;
            this.btnAssing.Click += new System.EventHandler(this.btnAssing_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnCancel.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.btnCancel.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.btnCancel.Menu = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmUserSelection
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAssing);
            this.Controls.Add(this.lbLoanOfficers);
            this.Controls.Add(this.cbUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserSelection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.Label lbLoanOfficers;
        private SweetButton btnAssing;
        private SweetButton btnCancel;
    }
}