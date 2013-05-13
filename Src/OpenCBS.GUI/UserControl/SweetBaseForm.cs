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

using System.Windows.Forms;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;

namespace OpenCBS.GUI.UserControl
{
    using MBB = MessageBoxButtons;
    using MBI = MessageBoxIcon;

    public partial class SweetBaseForm : Form
    {
        public SweetBaseForm()
        {
            InitializeComponent();
        }

        public string GetString(string key)
        {
            return GetString(Res, key);
        }

        public string GetString(string res, string key)
        {
            return MultiLanguageStrings.GetString(res, key);
        }

        protected virtual string Res
        {
            get
            {
                return GetType().Name;
            }
        }

        #region Notifications
        public void Notify(string key)
        {
            string caption = GetString("SweetBaseForm", "notification");
            string message = GetString(key) ?? key;
            MessageBox.Show(message, caption, MBB.OK, MBI.Information);
        }

        protected void Warn(string key, params object[] args)
        {
            string caption = GetString("SweetBaseForm", "warning");
            string message = GetString(key);
            message = message == null ? key : string.Format(message, args);
            MessageBox.Show(message, caption, MBB.OK, MBI.Warning);
        }

        public bool Confirm(string key)
        {
            string caption = GetString("SweetBaseForm", "confirmation");
            string message = GetString(key) ?? key;
            return DialogResult.Yes == MessageBox.Show(message, caption, MBB.YesNo, MBI.Question);
        }

        public void Fail(string key)
        {
            string caption = GetString("SweetBaseForm", "error");
            string message = GetString(key) ?? key;
            MessageBox.Show(message, caption, MBB.OK, MBI.Error);
        }
        #endregion Notifications

        protected IServices Services { get { return ServicesProvider.GetInstance(); } }
    }
}
