namespace OpenCBS.GUI.Accounting
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
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
            this.lblOpenDate.Name = "lblOpenDate";
            // 
            // lblCloseDate
            // 
            resources.ApplyResources(this.lblCloseDate, "lblCloseDate");
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
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
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
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnClose;
    }
}