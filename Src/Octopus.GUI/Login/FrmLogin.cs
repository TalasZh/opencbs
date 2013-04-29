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
using System.Collections.Generic;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Database;
using Octopus.GUI.Tools;
using Octopus.Services;
using Octopus.MultiLanguageRessources;
using System.Drawing;
using Octopus.Shared.Settings;

namespace Octopus.GUI.Login
{
	/// <summary>
	/// Description résumée de PasswordForm.
	/// </summary>
	public partial class FrmLogin : Form
	{
		private readonly string _userName;
        private readonly string _password;
        private List<SqlDatabaseSettings> _sqlDatabases;

        public FrmLogin(string pUserName, string pPassword)
		{
			InitializeComponent();

			_userName = pUserName;
		    _password = pPassword;
            labelVersion.Text = TechnicalSettings.SoftwareVersion;
            _DetectAllDatabase();
            Height = 270;
		}

	    private void _DetectAllDatabase()
	    {
            bWDetectDatabase.RunWorkerAsync();
	    }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            if (_userName != null)
                textBoxUserName.Text = _userName;

            if (_password != null)
                textBoxPassword.Text = _password;

            textBoxUserName.Focus();
            
            btnExtend.Tag = 1;
        }

	    private void llOctopusWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.octopusnetwork.org");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bWDisplayDatabase_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _sqlDatabases = ServicesProvider.GetInstance().GetDatabaseServices().GetSQLDatabasesSettings(
                    TechnicalSettings.DatabaseServerName, TechnicalSettings.DatabaseLoginName,
                    TechnicalSettings.DatabasePassword);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!(_CheckUserAndPasswordTextBox()))
                _SetUser();
            else
                MessageBox.Show(MultiLanguageStrings.GetString(
                    Ressource.PasswordForm, 
                    "messageBoxUserPasswordBlank.Text"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

	    private void _SetUser()
        {
            User user = ServicesProvider.GetInstance().GetUserServices().Find(textBoxUserName.Text, textBoxPassword.Text);

            if (user == null)
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.PasswordForm, "messageBoxUserPasswordIncorrect.Text"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBoxPassword.Focus();
                textBoxPassword.SelectAll();
            }
            else
            {
                User.CurrentUser = user;
                Close();
            }
	    }

	    private bool _CheckUserAndPasswordTextBox()
	    {
	        return string.IsNullOrEmpty(textBoxUserName.Text) || string.IsNullOrEmpty(textBoxPassword.Text);
	    }

        private void btnExtend_Click(object sender, EventArgs e)
        {
            _DisplayDatabases();
        }

	    private void _DisplayDatabases()
	    {
	        Height = Height == 315 ? 270 : 315;
	        label1.ForeColor = label1.ForeColor == Color.White ? Color.Black : Color.White;

	        btnExtend.BackgroundImage = (int) btnExtend.Tag == 1 ? 
                Properties.Resources.uparrow : Properties.Resources.dowarrow;

            btnExtend.Tag = (int)btnExtend.Tag == 1 ? 2 : 1;

	        if(Height == 270) return;

	        if (_sqlDatabases == null)
	        {
	            labelDetectDatabasesInProgress.Visible = true;
	            panelDatabase.Enabled = false;
	        }
	        else
	        {
	            labelDetectDatabasesInProgress.Visible = false;
	            panelDatabase.Enabled = true; 
                cbDatabase.Items.Clear();
	            foreach (SqlDatabaseSettings database in _sqlDatabases)
	            {
	                cbDatabase.Items.Add(database.Name);
	            }

	            cbDatabase.Text = TechnicalSettings.DatabaseName;
	        }
	    }

        private void cbDatabase_SelectedValueChanged(object sender, EventArgs e)
        {
            string databaseName = TechnicalSettings.DatabaseName;
            
            TechnicalSettings.DatabaseName = cbDatabase.SelectedItem.ToString();
            if (databaseName != cbDatabase.Text)
                Restart.LaunchRestarter();
        }
	}
}
