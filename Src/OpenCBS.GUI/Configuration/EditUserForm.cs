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