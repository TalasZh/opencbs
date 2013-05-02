namespace OpenCBS.GUI
{
    partial class FrmProjectPurposesR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProjectPurposesR));
            this.listViewProjectPurposes = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listViewProjectPurposes
            // 
            this.listViewProjectPurposes.AccessibleDescription = null;
            this.listViewProjectPurposes.AccessibleName = null;
            resources.ApplyResources(this.listViewProjectPurposes, "listViewProjectPurposes");
            this.listViewProjectPurposes.BackgroundImage = null;
            this.listViewProjectPurposes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewProjectPurposes.Font = null;
            this.listViewProjectPurposes.FullRowSelect = true;
            this.listViewProjectPurposes.GridLines = true;
            this.listViewProjectPurposes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewProjectPurposes.MultiSelect = false;
            this.listViewProjectPurposes.Name = "listViewProjectPurposes";
            this.listViewProjectPurposes.UseCompatibleStateImageBehavior = false;
            this.listViewProjectPurposes.View = System.Windows.Forms.View.Details;
            this.listViewProjectPurposes.DoubleClick += new System.EventHandler(this.listViewProjectPurposes_DoubleClick);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // FrmProjectPurposesR
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewProjectPurposes);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmProjectPurposesR";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewProjectPurposes;
        private System.Windows.Forms.ColumnHeader columnHeader1;

    }
}