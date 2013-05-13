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
