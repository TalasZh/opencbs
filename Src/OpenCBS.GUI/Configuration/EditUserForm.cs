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

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using OpenCBS.CoreDomain;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;

namespace OpenCBS.GUI.Configuration
{
    public partial class EditUserForm : SweetOkCancelForm
    {
        private bool _sync = true;
        private ObjectListView _olv;
        private CheckBox _chk;

        public List<User> AllUsers { get; set; }
        public List<Branch> AllBranches { get; set; }
        public User Boss { get; set; }

        public EditUserForm()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, System.EventArgs e)
        {
            _olv = olvUsers;
            _chk = chkSelectAll;
            olvUsers.SetObjects(AllUsers);
            foreach (User user in olvUsers.Objects)
            {
                if (Boss.HasAsSubordinate(user)) olvUsers.CheckObject(user);
            }

            _olv = olvBranches;
            _chk = chkSelectAllBranches;
            olvBranches.SetObjects(AllBranches);
            foreach (Branch b in olvBranches.Objects)
            {
                if (Boss.HasBranch(b))
                {
                    olvBranches.CheckObject(b);
                }
            }

            Text += " — " + Boss.Name;
            _olv = olvUsers;
            _chk = chkSelectAll;
        }

        public IEnumerable<User> NewSubordinates()
        {
            return olvUsers.CheckedObjects.Cast<User>();
        }

        public IEnumerable<Branch> NewBranches()
        {
            return olvBranches.CheckedObjects.Cast<Branch>();
        }

        private void OnSelectAllClick(object sender, System.EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            Debug.Assert(chk != null, "Checkbox is null.");
            if (CheckState.Indeterminate == chk.CheckState)
            {
                chk.CheckState = CheckState.Unchecked;
            }
            _sync = false;
            foreach (object o in _olv.Objects)
            {
                if (chk.Checked)
                {
                    _olv.CheckObject(o);
                }
                else
                {
                    _olv.UncheckObject(o);
                }
            }
            _sync = true;
        }

        private void SyncCheckState()
        {
            if (!_sync) return;

            if (0 == _olv.CheckedObjects.Count)
            {
                _chk.CheckState = CheckState.Unchecked;
            }
            else if (_olv.CheckedObjects.Count < _olv.Items.Count)
            {
                _chk.CheckState = CheckState.Indeterminate;
            }
            else
            {
                _chk.CheckState = CheckState.Checked;
            }
        }

        private void OnUserChecked(object sender, ItemCheckedEventArgs e)
        {
            SyncCheckState();
        }

        private void tabUser_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            _olv = 0 == tabUser.SelectedIndex ? olvUsers : olvBranches;
            _chk = 0 == tabUser.SelectedIndex ? chkSelectAll : chkSelectAllBranches;
        }
    }
}
