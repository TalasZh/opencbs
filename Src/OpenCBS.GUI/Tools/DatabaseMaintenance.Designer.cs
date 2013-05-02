using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Tools
{
    partial class frmDatabaseMaintenance
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatabaseMaintenance));
            this.gbxShrinkDatabase = new System.Windows.Forms.GroupBox();
            this.lblDatabaseSize = new System.Windows.Forms.Label();
            this.lblDatabaseUsage = new System.Windows.Forms.Label();
            this.btnShrinkDatabase = new System.Windows.Forms.Button();
            this.timerDatabaseSize = new System.Windows.Forms.Timer(this.components);
            this.gbxRunSql = new System.Windows.Forms.GroupBox();
            this.btnRunSQL = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbxShrinkDatabase.SuspendLayout();
            this.gbxRunSql.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxShrinkDatabase
            //
            this.gbxShrinkDatabase.Controls.Add(this.lblDatabaseSize);
            this.gbxShrinkDatabase.Controls.Add(this.lblDatabaseUsage);
            this.gbxShrinkDatabase.Controls.Add(this.btnShrinkDatabase);
            resources.ApplyResources(this.gbxShrinkDatabase, "gbxShrinkDatabase");
            this.gbxShrinkDatabase.Name = "gbxShrinkDatabase";
            this.gbxShrinkDatabase.TabStop = false;
            // 
            // lblDatabaseSize
            // 
            resources.ApplyResources(this.lblDatabaseSize, "lblDatabaseSize");
            this.lblDatabaseSize.Name = "lblDatabaseSize";
            // 
            // lblDatabaseUsage
            // 
            resources.ApplyResources(this.lblDatabaseUsage, "lblDatabaseUsage");
            this.lblDatabaseUsage.Name = "lblDatabaseUsage";
            // 
            // btnShrinkDatabase
            //
            resources.ApplyResources(this.btnShrinkDatabase, "btnShrinkDatabase");
            this.btnShrinkDatabase.Name = "btnShrinkDatabase";
            this.btnShrinkDatabase.Click += new System.EventHandler(this.butShrinkDatabase_Click);
            // 
            // timerDatabaseSize
            // 
            this.timerDatabaseSize.Interval = 1000;
            this.timerDatabaseSize.Tick += new System.EventHandler(this.timerDatabaseSize_Tick);
            // 
            // gbxRunSql
            //
            this.gbxRunSql.Controls.Add(this.btnRunSQL);
            resources.ApplyResources(this.gbxRunSql, "gbxRunSql");
            this.gbxRunSql.Name = "gbxRunSql";
            this.gbxRunSql.TabStop = false;
            // 
            // btnRunSQL
            //
            resources.ApplyResources(this.btnRunSQL, "btnRunSQL");
            this.btnRunSQL.Name = "btnRunSQL";
            this.btnRunSQL.Click += new System.EventHandler(this.buttonRunSQL_Click);
            // 
            // btnClose
            //
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            // 
            // frmDatabaseMaintenance
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gbxRunSql);
            this.Controls.Add(this.gbxShrinkDatabase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDatabaseMaintenance";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDatabaseMaintenance_FormClosed);
            this.gbxShrinkDatabase.ResumeLayout(false);
            this.gbxShrinkDatabase.PerformLayout();
            this.gbxRunSql.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxShrinkDatabase;
        private System.Windows.Forms.Label lblDatabaseUsage;
        private System.Windows.Forms.Button btnShrinkDatabase;
        private System.Windows.Forms.Label lblDatabaseSize;
        private System.Windows.Forms.Timer timerDatabaseSize;
        private System.Windows.Forms.GroupBox gbxRunSql;
        private System.Windows.Forms.Button btnRunSQL;
        private System.Windows.Forms.Button btnClose;
    }
}