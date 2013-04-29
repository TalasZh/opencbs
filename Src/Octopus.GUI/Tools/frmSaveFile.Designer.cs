using Octopus.GUI.UserControl;

namespace Octopus.GUI.Tools
{
    partial class FrmSaveFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSaveFile));
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnCancel = new Octopus.GUI.UserControl.SweetButton();
            this.btnSave = new Octopus.GUI.UserControl.SweetButton();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowse = new Octopus.GUI.UserControl.SweetButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.timerInput = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblWarning = new System.Windows.Forms.Label();
            this.btnDefault = new Octopus.GUI.UserControl.SweetButton();
            this.SuspendLayout();
            // 
            // tbPath
            // 
            resources.ApplyResources(this.tbPath, "tbPath");
            this.tbPath.Name = "tbPath";
            this.tbPath.TextChanged += new System.EventHandler(this.tbPath_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnCancel.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Close;
            this.btnCancel.Menu = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnSave.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.Save;
            this.btnSave.Menu = null;
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.label3.Name = "label3";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBrowse.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnBrowse.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.View;
            this.btnBrowse.Image = global::Octopus.GUI.Properties.Resources.theme1_1_search;
            this.btnBrowse.Menu = null;
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.label1.Name = "label1";
            // 
            // tbFileName
            // 
            resources.ApplyResources(this.tbFileName, "tbFileName");
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.TextChanged += new System.EventHandler(this.tbFileName_TextChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Name = "lblTitle";
            // 
            // timerInput
            // 
            this.timerInput.Interval = 300;
            this.timerInput.Tick += new System.EventHandler(this.timerInput_Tick);
            // 
            // lblWarning
            // 
            resources.ApplyResources(this.lblWarning, "lblWarning");
            this.lblWarning.BackColor = System.Drawing.Color.Transparent;
            this.lblWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblWarning.Name = "lblWarning";
            // 
            // btnDefault
            // 
            this.btnDefault.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDefault.BackgroundImage = global::Octopus.GUI.Properties.Resources.theme1_1_fond_bouton;
            resources.ApplyResources(this.btnDefault, "btnDefault");
            this.btnDefault.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnDefault.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnDefault.Menu = null;
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.UseVisualStyleBackColor = false;
            // 
            // FrmSaveFile
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Name = "FrmSaveFile";
            this.Load += new System.EventHandler(this.frmSaveFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Timer timerInput;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label lblWarning;
        private SweetButton btnCancel;
        private SweetButton btnSave;
        private SweetButton btnBrowse;
        private SweetButton btnDefault;
    }
}