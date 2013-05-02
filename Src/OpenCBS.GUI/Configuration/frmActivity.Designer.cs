namespace OpenCBS.GUI
{
    partial class frmActivity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmActivity));
            this.treeViewActivities = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeViewActivities
            // 
            resources.ApplyResources(this.treeViewActivities, "treeViewActivities");
            this.treeViewActivities.Name = "treeViewActivities";
            this.treeViewActivities.DoubleClick += new System.EventHandler(this.treeViewActivities_DoubleClick);
            // 
            // frmActivity
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeViewActivities);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmActivity";
            this.Load += new System.EventHandler(this.frmActivity_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewActivities;
    }
}