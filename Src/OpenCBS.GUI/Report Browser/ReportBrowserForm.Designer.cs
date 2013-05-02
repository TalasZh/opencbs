namespace OpenCBS.GUI.Report_Browser
{
    partial class ReportBrowserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportBrowserForm));
            this.rbReports = new OpenCBS.Reports.Forms.ReportBrowserControl();
            this.SuspendLayout();
            // 
            // rbReports
            // 
            this.rbReports.AccessibleDescription = null;
            this.rbReports.AccessibleName = null;
            resources.ApplyResources(this.rbReports, "rbReports");
            this.rbReports.BackColor = System.Drawing.SystemColors.Window;
            this.rbReports.BackgroundImage = null;
            this.rbReports.Font = null;
            this.rbReports.Name = "rbReports";
            // 
            // ReportBrowserForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbReports);
            this.Name = "ReportBrowserForm";
            this.Load += new System.EventHandler(this.ReportBrowserForm_Load);
            this.Controls.SetChildIndex(this.rbReports, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenCBS.Reports.Forms.ReportBrowserControl rbReports;

    }
}