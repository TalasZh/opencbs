namespace OpenCBS.Reports.Forms
{
    partial class ReportBrowserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportBrowserControl));
            this.wbReports = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbReports
            // 
            resources.ApplyResources(this.wbReports, "wbReports");
            this.wbReports.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbReports.Name = "wbReports";
            // 
            // ReportBrowserControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.wbReports);
            this.Name = "ReportBrowserControl";
            resources.ApplyResources(this, "$this");
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbReports;
    }
}