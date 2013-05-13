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

using System.Diagnostics;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;

namespace OpenCBS.GUI.Configuration
{
    public partial class TellersForm : SweetForm
    {
        public TellersForm()
        {
            InitializeComponent();
        }

        private void LoadTellers()
        {
            olvTellers.Items.Clear();
            olvTellers.SetObjects(ServicesProvider.GetInstance().GetTellerServices().FindAllNonDeleted());
            CheckSelected();
        }

        private void BranchesForm_Load(object sender, System.EventArgs e)
        {
            LoadTellers();
        }

        private void CheckSelected()
        {
            bool sel = olvTellers.SelectedItems.Count > 0;
            btnDelete.Enabled = sel;
            btnEdit.Enabled = sel;
        }

        private void olvBranches_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            CheckSelected();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            AddTellerForm frm = new AddTellerForm();
            //frm.IsNew = true;
            //frm.Teller = new Teller();
            if (DialogResult.OK != frm.ShowDialog()) return;

            ServicesProvider.GetInstance().GetTellerServices().Add(frm.Teller);
            LoadTellers();
        }

        private void Edit()
        {
            Teller teller = olvTellers.SelectedObject as Teller;
            Debug.Assert(teller != null, "Teller not selected!");
            AddTellerForm frm = new AddTellerForm(teller);
            //frm.IsNew = false;
            //frm.Teller = teller;
            if (DialogResult.OK != frm.ShowDialog()) return;

            ServicesProvider.GetInstance().GetTellerServices().Update(teller);

            olvTellers.RefreshSelectedObjects();
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
            Teller teller = olvTellers.SelectedObject as Teller;
            Debug.Assert(teller != null, "Teller not selected!");
            if (!Confirm("confirmDelete")) return;

            ServicesProvider.GetInstance().GetTellerServices().Delete(teller);
            LoadTellers();
        }
    }
}
