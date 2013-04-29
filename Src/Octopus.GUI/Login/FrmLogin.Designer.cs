
using System.Windows.Forms;
using Octopus.GUI.UserControl;

namespace Octopus.GUI.Login
{
    public partial class FrmLogin
    {
        private Label label1;
        private Button btnExtend;
        private Label labelVersion;
        private SweetButton buttonOK;
        private TextBox textBoxUserName;
        private TextBox textBoxPassword;
        private Label labelUserName;
        private Label labelPassword;
        private SweetButton buttonExit;
        private LinkLabel llOctopusWeb;
        private PictureBox pictureBox;
        private Label lbDatabase;
        private ComboBox cbDatabase;
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.btnExtend = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.buttonOK = new Octopus.GUI.UserControl.SweetButton();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonExit = new Octopus.GUI.UserControl.SweetButton();
            this.llOctopusWeb = new System.Windows.Forms.LinkLabel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lbDatabase = new System.Windows.Forms.Label();
            this.cbDatabase = new System.Windows.Forms.ComboBox();
            this.bWDetectDatabase = new System.ComponentModel.BackgroundWorker();
            this.panelDatabase = new System.Windows.Forms.Panel();
            this.labelDetectDatabasesInProgress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panelDatabase.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // btnExtend
            // 
            this.btnExtend.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnExtend, "btnExtend");
            this.btnExtend.FlatAppearance.BorderSize = 0;
            this.btnExtend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExtend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExtend.Name = "btnExtend";
            this.btnExtend.UseVisualStyleBackColor = false;
            this.btnExtend.Click += new System.EventHandler(this.btnExtend_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.SystemColors.Control;
            this.labelVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelVersion.Name = "labelVersion";
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonOK.Icon = Octopus.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.buttonOK.Menu = null;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxUserName
            // 
            resources.ApplyResources(this.textBoxUserName, "textBoxUserName");
            this.textBoxUserName.Name = "textBoxUserName";
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.Name = "textBoxPassword";
            // 
            // labelUserName
            // 
            this.labelUserName.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelUserName, "labelUserName");
            this.labelUserName.ForeColor = System.Drawing.Color.Black;
            this.labelUserName.Name = "labelUserName";
            // 
            // labelPassword
            // 
            this.labelPassword.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelPassword, "labelPassword");
            this.labelPassword.ForeColor = System.Drawing.Color.Black;
            this.labelPassword.Name = "labelPassword";
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonExit, "buttonExit");
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // llOctopusWeb
            // 
            resources.ApplyResources(this.llOctopusWeb, "llOctopusWeb");
            this.llOctopusWeb.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.llOctopusWeb.Name = "llOctopusWeb";
            this.llOctopusWeb.TabStop = true;
            this.llOctopusWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOctopusWeb_LinkClicked);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            // 
            // lbDatabase
            // 
            this.lbDatabase.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbDatabase, "lbDatabase");
            this.lbDatabase.ForeColor = System.Drawing.Color.Black;
            this.lbDatabase.Name = "lbDatabase";
            // 
            // cbDatabase
            // 
            this.cbDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabase.FormattingEnabled = true;
            resources.ApplyResources(this.cbDatabase, "cbDatabase");
            this.cbDatabase.Name = "cbDatabase";
            this.cbDatabase.SelectedValueChanged += new System.EventHandler(this.cbDatabase_SelectedValueChanged);
            // 
            // bWDetectDatabase
            // 
            this.bWDetectDatabase.WorkerSupportsCancellation = true;
            this.bWDetectDatabase.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWDisplayDatabase_DoWork);
            // 
            // panelDatabase
            // 
            this.panelDatabase.BackColor = System.Drawing.Color.Transparent;
            this.panelDatabase.Controls.Add(this.lbDatabase);
            this.panelDatabase.Controls.Add(this.cbDatabase);
            resources.ApplyResources(this.panelDatabase, "panelDatabase");
            this.panelDatabase.Name = "panelDatabase";
            // 
            // labelDetectDatabasesInProgress
            // 
            resources.ApplyResources(this.labelDetectDatabasesInProgress, "labelDetectDatabasesInProgress");
            this.labelDetectDatabasesInProgress.BackColor = System.Drawing.Color.Transparent;
            this.labelDetectDatabasesInProgress.Name = "labelDetectDatabasesInProgress";
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonExit;
            this.ControlBox = false;
            this.Controls.Add(this.labelDetectDatabasesInProgress);
            this.Controls.Add(this.panelDatabase);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.llOctopusWeb);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.btnExtend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PasswordForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panelDatabase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.ComponentModel.BackgroundWorker bWDetectDatabase;
        private Panel panelDatabase;
        private Label labelDetectDatabasesInProgress;
    }
}
