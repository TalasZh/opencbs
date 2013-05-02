// LICENSE PLACEHOLDER

using System;
using System.Diagnostics;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;

namespace OpenCBS.GUI.Configuration
{
    public partial class BranchesForm : SweetForm
    {
        public BranchesForm()
        {
            InitializeComponent();
        }

        private void LoadBranches()
        {
            olvBranches.Items.Clear();
            olvBranches.SetObjects(ServicesProvider.GetInstance().GetBranchService().FindAllNonDeleted());
            CheckSelected();
        }

        private void BranchesForm_Load(object sender, EventArgs e)
        {
            LoadBranches();
        }

        private void CheckSelected()
        {
            bool sel = olvBranches.SelectedItems.Count > 0;
            btnDelete.Enabled = sel;
            btnEdit.Enabled = sel;
        }

        private void olvBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSelected();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddBranchForm frm = new AddBranchForm();
            frm.IsNew = true;
            frm.Branch = new Branch();
            if (DialogResult.OK != frm.ShowDialog()) return;
            LoadBranches();
        }

        private void Edit()
        {
            Branch b = olvBranches.SelectedObject as Branch;
            Debug.Assert(b != null, "Branch not selected");
            AddBranchForm frm = new AddBranchForm();
            frm.IsNew = false;
            frm.Branch = b;
            if (DialogResult.OK != frm.ShowDialog()) return;
            olvBranches.RefreshSelectedObjects();
            LoadBranches();
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            Edit();
        }

        private void olvBranches_DoubleClick(object sender, System.EventArgs e)
        {
            Edit();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            Branch b = olvBranches.SelectedObject as Branch;
            Debug.Assert(b != null, "Branch not selected");
            if (!Confirm("confirmDelete")) return;

            ServicesProvider.GetInstance().GetBranchService().Delete(b);
            LoadBranches();
        }
    }
}
