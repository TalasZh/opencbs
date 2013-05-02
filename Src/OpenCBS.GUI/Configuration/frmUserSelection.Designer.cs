using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Configuration
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
            this.btnAssing = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
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
            this.lbLoanOfficers.Name = "lbLoanOfficers";
            // 
            // btnAssing
            //
            resources.ApplyResources(this.btnAssing, "btnAssing");
            this.btnAssing.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAssing.Name = "btnAssing";
            this.btnAssing.Click += new System.EventHandler(this.btnAssing_Click);
            // 
            // btnCancel
            //
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmUserSelection
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
        private System.Windows.Forms.Button btnAssing;
        private System.Windows.Forms.Button btnCancel;
    }
}