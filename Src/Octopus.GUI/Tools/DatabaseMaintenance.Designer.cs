using Octopus.GUI.UserControl;

namespace Octopus.GUI.Tools
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
            this.btnShrinkDatabase = new Octopus.GUI.UserControl.SweetButton();
            this.timerDatabaseSize = new System.Windows.Forms.Timer(this.components);
            this.gbxRunSql = new System.Windows.Forms.GroupBox();
            this.btnRunSQL = new Octopus.GUI.UserControl.SweetButton();
            this.btnClose = new Octopus.GUI.UserControl.SweetButton();
            this.gbxShrinkDatabase.SuspendLayout();
            this.gbxRunSql.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxShrinkDatabase
            // 
            this.gbxShrinkDatabase.BackColor = System.Drawing.Color.Transparent;
            this.gbxShrinkDatabase.Controls.Add(this.lblDatabaseSize);
            this.gbxShrinkDatabase.Controls.Add(this.lblDatabaseUsage);
            this.gbxShrinkDatabase.Controls.Add(this.btnShrinkDatabase);
            resources.ApplyResources(this.gbxShrinkDatabase, "gbxShrinkDatabase");
            this.gbxShrinkDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.gbxShrinkDatabase.Name = "gbxShrinkDatabase";
            this.gbxShrinkDatabase.TabStop = false;
            // 
            // lblDatabaseSize
            // 
            resources.ApplyResources(this.lblDatabaseSize, "lblDatabaseSize");
            this.lblDatabaseSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblDatabaseSize.Name = "lblDatabaseSize";
            // 
            // lblDatabaseUsage
            // 
            resources.ApplyResources(this.lblDatabaseUsage, "lblDatabaseUsage");
            this.lblDatabaseUsage.Name = "lblDatabaseUsage";
            // 
            // btnShrinkDatabase
            // 
            this.btnShrinkDatabase.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnShrinkDatabase, "btnShrinkDatabase");
            this.btnShrinkDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnShrinkDatabase.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Database;
            this.btnShrinkDatabase.Image = global::Octopus.GUI.Properties.Resources.thame1_1_database;
            this.btnShrinkDatabase.Menu = null;
            this.btnShrinkDatabase.Name = "btnShrinkDatabase";
            this.btnShrinkDatabase.UseVisualStyleBackColor = false;
            this.btnShrinkDatabase.Click += new System.EventHandler(this.butShrinkDatabase_Click);
            // 
            // timerDatabaseSize
            // 
            this.timerDatabaseSize.Interval = 1000;
            this.timerDatabaseSize.Tick += new System.EventHandler(this.timerDatabaseSize_Tick);
            // 
            // gbxRunSql
            // 
            this.gbxRunSql.BackColor = System.Drawing.Color.Transparent;
            this.gbxRunSql.Controls.Add(this.btnRunSQL);
            resources.ApplyResources(this.gbxRunSql, "gbxRunSql");
            this.gbxRunSql.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.gbxRunSql.Name = "gbxRunSql";
            this.gbxRunSql.TabStop = false;
            // 
            // btnRunSQL
            // 
            this.btnRunSQL.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnRunSQL, "btnRunSQL");
            this.btnRunSQL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnRunSQL.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Database;
            this.btnRunSQL.Image = global::Octopus.GUI.Properties.Resources.thame1_1_database;
            this.btnRunSQL.Menu = null;
            this.btnRunSQL.Name = "btnRunSQL";
            this.btnRunSQL.UseVisualStyleBackColor = false;
            this.btnRunSQL.Click += new System.EventHandler(this.buttonRunSQL_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnClose.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.btnClose.Image = global::Octopus.GUI.Properties.Resources.theme1_1_bouton_close;
            this.btnClose.Menu = null;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // frmDatabaseMaintenance
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_gris;
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
        private SweetButton btnShrinkDatabase;
        private System.Windows.Forms.Label lblDatabaseSize;
        private System.Windows.Forms.Timer timerDatabaseSize;
        private System.Windows.Forms.GroupBox gbxRunSql;
        private SweetButton btnRunSQL;
        private Octopus.GUI.UserControl.SweetButton btnClose;
    }
}