namespace Octopus.GUI.Accounting
{
    partial class EditFiscalYear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFiscalYear));
            this.lblName = new System.Windows.Forms.Label();
            this.dpkOpenDate = new System.Windows.Forms.DateTimePicker();
            this.lblOpenDate = new System.Windows.Forms.Label();
            this.lblCloseDate = new System.Windows.Forms.Label();
            this.dpkCloseDate = new System.Windows.Forms.DateTimePicker();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.btnClose = new Octopus.GUI.UserControl.SweetButton();
            this.btnOk = new Octopus.GUI.UserControl.SweetButton();
            this.SuspendLayout();
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblName.Name = "lblName";
            // 
            // dpkOpenDate
            // 
            this.dpkOpenDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dpkOpenDate, "dpkOpenDate");
            this.dpkOpenDate.Name = "dpkOpenDate";
            // 
            // lblOpenDate
            // 
            resources.ApplyResources(this.lblOpenDate, "lblOpenDate");
            this.lblOpenDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblOpenDate.Name = "lblOpenDate";
            // 
            // lblCloseDate
            // 
            resources.ApplyResources(this.lblCloseDate, "lblCloseDate");
            this.lblCloseDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblCloseDate.Name = "lblCloseDate";
            // 
            // dpkCloseDate
            // 
            this.dpkCloseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dpkCloseDate, "dpkCloseDate");
            this.dpkCloseDate.Name = "dpkCloseDate";
            // 
            // tbxName
            // 
            resources.ApplyResources(this.tbxName, "tbxName");
            this.tbxName.Name = "tbxName";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnClose.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.btnClose.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_delete;
            this.btnClose.Menu = null;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.BackColor = System.Drawing.Color.Gainsboro;
            this.btnOk.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnOk.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            this.btnOk.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_delete;
            this.btnOk.Menu = null;
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // EditFiscalYear
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.dpkCloseDate);
            this.Controls.Add(this.lblCloseDate);
            this.Controls.Add(this.lblOpenDate);
            this.Controls.Add(this.dpkOpenDate);
            this.Controls.Add(this.lblName);
            this.Name = "EditFiscalYear";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.DateTimePicker dpkOpenDate;
        private System.Windows.Forms.Label lblOpenDate;
        private System.Windows.Forms.Label lblCloseDate;
        private System.Windows.Forms.DateTimePicker dpkCloseDate;
        private System.Windows.Forms.TextBox tbxName;
        private Octopus.GUI.UserControl.SweetButton btnOk;
        private Octopus.GUI.UserControl.SweetButton btnClose;
    }
}