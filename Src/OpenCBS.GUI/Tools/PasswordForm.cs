// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.GUI.UserControl;
using OpenCBS.MultiLanguageRessources;

namespace OpenCBS.GUI.Tools
{
    public partial class PasswordForm : SweetOkCancelForm
    {
        public User User { get; set; }
        public string NewPassword;
        public PasswordForm()
        {
            InitializeComponent();
        }

        public PasswordForm(User user)
        {
            InitializeComponent();
            User = user;
            textBoxUserName.Text = User.UserName;
        }

        private void PasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK) return;
            if (User.Password != textBoxOldPswd.Text)
            {
                Fail("oldPasswordIsWrong");
                e.Cancel = true;
            }
            else if (textBoxNewPswd.Text.Length<4 || textBoxNewPswd.Text.Length>30)
            {
                Fail("littleOrBigPassword");
                e.Cancel = true;
            }
            else if (!string.Equals(textBoxNewPswd.Text, textBoxConfirmNewPswd.Text))
            {
                Fail("newPasswordIsWrong");
                e.Cancel = true;
            }
            NewPassword = textBoxNewPswd.Text;
        }
    }
}
