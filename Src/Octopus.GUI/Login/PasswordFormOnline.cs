//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.Services;
using Octopus.MultiLanguageRessources;
using Octopus.Shared;
using Octopus.Shared.Settings;

namespace Octopus.GUI
{
	/// <summary>
	/// Description résumée de PasswordForm.
	/// </summary>
	public class PasswordFormOnline : Form
	{
		private string _userName;
	    private string _password;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
        private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TextBox textBoxUserName;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Label labelUserName;
		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.LinkLabel llOctopusWeb;
        private Label label1;
        private TextBox textBoxAccountName;
        private Label labelAccountName;
        private LinkLabel linkLabelExpend;
        private GroupBox groupBoxConfiguration;
        private NumericUpDown numericUpDownServerPort;
        private Label labelServerPort;
        private Label labelServerName;
        private TextBox textBoxServerName;
        private Button buttonSaveConfig;
        private LinkLabel linkLabelHide;
        private PictureBox pictureBox;

        public PasswordFormOnline()
		{
			InitializeComponent();
			_InitializeVersion();
            _initializeConfiguration();
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordFormOnline));
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBoxConfiguration = new System.Windows.Forms.GroupBox();
            this.numericUpDownServerPort = new System.Windows.Forms.NumericUpDown();
            this.labelServerPort = new System.Windows.Forms.Label();
            this.labelServerName = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.buttonSaveConfig = new System.Windows.Forms.Button();
            this.linkLabelExpend = new System.Windows.Forms.LinkLabel();
            this.textBoxAccountName = new System.Windows.Forms.TextBox();
            this.labelAccountName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.llOctopusWeb = new System.Windows.Forms.LinkLabel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.linkLabelHide = new System.Windows.Forms.LinkLabel();
            this.panel3.SuspendLayout();
            this.groupBoxConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.groupBoxConfiguration);
            this.panel3.Controls.Add(this.linkLabelExpend);
            this.panel3.Controls.Add(this.textBoxAccountName);
            this.panel3.Controls.Add(this.labelAccountName);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.labelVersion);
            this.panel3.Controls.Add(this.buttonOK);
            this.panel3.Controls.Add(this.textBoxUserName);
            this.panel3.Controls.Add(this.textBoxPassword);
            this.panel3.Controls.Add(this.labelUserName);
            this.panel3.Controls.Add(this.labelPassword);
            this.panel3.Controls.Add(this.buttonExit);
            this.panel3.Controls.Add(this.llOctopusWeb);
            this.panel3.Controls.Add(this.pictureBox);
            this.panel3.Controls.Add(this.linkLabelHide);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // groupBoxConfiguration
            // 
            this.groupBoxConfiguration.Controls.Add(this.numericUpDownServerPort);
            this.groupBoxConfiguration.Controls.Add(this.labelServerPort);
            this.groupBoxConfiguration.Controls.Add(this.labelServerName);
            this.groupBoxConfiguration.Controls.Add(this.textBoxServerName);
            this.groupBoxConfiguration.Controls.Add(this.buttonSaveConfig);
            resources.ApplyResources(this.groupBoxConfiguration, "groupBoxConfiguration");
            this.groupBoxConfiguration.Name = "groupBoxConfiguration";
            this.groupBoxConfiguration.TabStop = false;
            // 
            // numericUpDownServerPort
            // 
            resources.ApplyResources(this.numericUpDownServerPort, "numericUpDownServerPort");
            this.numericUpDownServerPort.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDownServerPort.Name = "numericUpDownServerPort";
            // 
            // labelServerPort
            // 
            resources.ApplyResources(this.labelServerPort, "labelServerPort");
            this.labelServerPort.ForeColor = System.Drawing.Color.Black;
            this.labelServerPort.Name = "labelServerPort";
            // 
            // labelServerName
            // 
            resources.ApplyResources(this.labelServerName, "labelServerName");
            this.labelServerName.ForeColor = System.Drawing.Color.Black;
            this.labelServerName.Name = "labelServerName";
            // 
            // textBoxServerName
            // 
            resources.ApplyResources(this.textBoxServerName, "textBoxServerName");
            this.textBoxServerName.Name = "textBoxServerName";
            // 
            // buttonSaveConfig
            // 
            resources.ApplyResources(this.buttonSaveConfig, "buttonSaveConfig");
            this.buttonSaveConfig.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSaveConfig.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonSaveConfig.Name = "buttonSaveConfig";
            this.buttonSaveConfig.UseVisualStyleBackColor = false;
            this.buttonSaveConfig.Click += new System.EventHandler(this.buttonSaveConfig_Click);
            // 
            // linkLabelExpend
            // 
            resources.ApplyResources(this.linkLabelExpend, "linkLabelExpend");
            this.linkLabelExpend.LinkColor = System.Drawing.Color.White;
            this.linkLabelExpend.Name = "linkLabelExpend";
            this.linkLabelExpend.TabStop = true;
            this.linkLabelExpend.VisitedLinkColor = System.Drawing.Color.White;
            this.linkLabelExpend.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // textBoxAccountName
            // 
            resources.ApplyResources(this.textBoxAccountName, "textBoxAccountName");
            this.textBoxAccountName.Name = "textBoxAccountName";
            // 
            // labelAccountName
            // 
            resources.ApplyResources(this.labelAccountName, "labelAccountName");
            this.labelAccountName.ForeColor = System.Drawing.Color.Black;
            this.labelAccountName.Name = "labelAccountName";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // labelVersion
            // 
            this.labelVersion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelVersion.Name = "labelVersion";
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
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
            resources.ApplyResources(this.labelUserName, "labelUserName");
            this.labelUserName.ForeColor = System.Drawing.Color.Black;
            this.labelUserName.Name = "labelUserName";
            // 
            // labelPassword
            // 
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
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            // 
            // linkLabelHide
            // 
            resources.ApplyResources(this.linkLabelHide, "linkLabelHide");
            this.linkLabelHide.LinkColor = System.Drawing.Color.Green;
            this.linkLabelHide.Name = "linkLabelHide";
            this.linkLabelHide.TabStop = true;
            this.linkLabelHide.VisitedLinkColor = System.Drawing.Color.Green;
            this.linkLabelHide.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHide_LinkClicked);
            // 
            // PasswordFormOnline
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonExit;
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PasswordFormOnline";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PasswordForm_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBoxConfiguration.ResumeLayout(false);
            this.groupBoxConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void _InitializeVersion()
		{
            labelVersion.Text = TechnicalSettings.SoftwareVersion;
		}

        private void _initializeConfiguration()
        {
            textBoxServerName.Text = TechnicalSettings.RemotingServer;
            numericUpDownServerPort.Value = TechnicalSettings.RemotingServerPort;
        }

        private void _saveConfiguration()
        {
            try
            {
                TechnicalSettings.RemotingServer = textBoxServerName.Text;
                TechnicalSettings.RemotingServerPort = Convert.ToInt32(numericUpDownServerPort.Value);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

	    private void _InitializeUser()
        {
            ServicesProvider.GetServiceProvider().InitOnlineConnection(textBoxUserName.Text, textBoxPassword.Text,
                                                                       textBoxAccountName.Text, Environment.MachineName,
                                                                       Environment.UserName);
            User user = ServicesProvider.GetInstance().GetUserServices().Find(textBoxUserName.Text,
                                                                                  textBoxPassword.Text,
                                                                                  textBoxAccountName.Text);
            if (user == null)
            {
                MessageBox.Show(
                    MultiLanguageStrings.GetString(Ressource.PasswordFormOnline, "messageBoxUserPasswordIncorrect.Text"),
                    "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxPassword.Clear();
                textBoxUserName.Clear();
                textBoxAccountName.Clear();
            }
            else
            {
                user.Md5 = User.CurrentUser.Md5;
                User.CurrentUser = user;

                Close();
            }
        }

	    private bool _TestFieldEmpty()
		{
			bool empty = false;
            if (textBoxUserName.Text == string.Empty)
				empty = true;
            else if (textBoxPassword.Text == string.Empty)
				empty = true;
            else if(textBoxAccountName.Text == string.Empty)
                empty = true;

			return empty;
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			Environment.Exit(1);

		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!(_TestFieldEmpty()))
                    _InitializeUser();
                else
    		        MessageBox.Show(MultiLanguageStrings.GetString(Ressource.PasswordFormOnline, "messageBoxUserPasswordBlank.Text"), "",
		                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                textBoxAccountName.Focus();

                Cursor.Current = Cursors.Default;
                string error = MultiLanguageStrings.GetString(Ressource.PasswordFormOnline, ex.Message);
                if (error == null)
                    error = ex.Message;
                MessageBox.Show(error, "",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Cursor.Current = Cursors.Default;
		}

		private void PasswordForm_Load(object sender, System.EventArgs e)
		{
		    if (_userName != null)
		        textBoxUserName.Text = _userName;

		    if (_password != null)
		        textBoxPassword.Text = _password;
		}

		private void llOctopusWeb_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.octopusnetwork.org");
		}

        private void _showConfiguration()
        {
            Height += 140;
            linkLabelExpend.Visible = false;
            linkLabelHide.Visible = true;
        }

        private void _hideConfiguration()
        {
            Height -= 140;
            linkLabelExpend.Visible = true;
            linkLabelHide.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _showConfiguration();
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            _saveConfiguration();
        }

        private void linkLabelHide_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _hideConfiguration();
        }
	}
}
