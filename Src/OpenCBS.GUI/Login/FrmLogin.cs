//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright � 2006,2007 OCTO Technology & OXUS Development Network
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
using OpenCBS.CoreDomain;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;

namespace OpenCBS.GUI.Login
{
    public partial class FrmLogin : Form
    {
        private readonly string _userName;
        private readonly string _password;

        public FrmLogin(string pUserName, string pPassword)
        {
            InitializeComponent();

            _userName = pUserName;
            _password = pPassword;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (_userName != null)
                textBoxUserName.Text = _userName;

            if (_password != null)
                textBoxPassword.Text = _password;

            textBoxUserName.Focus();
        }

        private void OnExitButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            if (!(UserIsValid()))
                SetUser();
            else
                MessageBox.Show(MultiLanguageStrings.GetString(
                    Ressource.PasswordForm,
                    "messageBoxUserPasswordBlank.Text"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetUser()
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

        private bool UserIsValid()
        {
            return string.IsNullOrEmpty(textBoxUserName.Text) || string.IsNullOrEmpty(textBoxPassword.Text);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
