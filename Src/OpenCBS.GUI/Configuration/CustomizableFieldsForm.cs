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
using System.Diagnostics;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;

namespace OpenCBS.GUI.Configuration
{
    public partial class CustomizableFieldsForm : SweetForm
    {
        public CustomizableFieldsForm()
        {
            InitializeComponent();
        }

        private void LoadFields()
        {
            List<CustomizableFieldSearchResult> results = new List<CustomizableFieldSearchResult>();
            
            List<CustomizableFieldSearchResult> entities = ServicesProvider.GetInstance().GetCustomizableFieldsServices().SelectCreatedEntites();
            foreach (var entity in entities)
            {
                CustomizableFieldSearchResult result = new CustomizableFieldSearchResult(); 
                result.EntityName = GetString("entity" + entity.EntityName);
                result.Fields = entity.Fields;
                result.FieldsNumber = entity.FieldsNumber;
                results.Add(result);
            }
            
            olvFieldGroups.Items.Clear();
            olvFieldGroups.SetObjects(results);
            CheckSelected();
        }

        private void AdvCustomizableFieldsForm_Load(object sender, EventArgs e)
        {
            LoadFields();
        }

        private void CheckSelected()
        {
            bool sel = olvFieldGroups.SelectedItems.Count > 0;
            btnEdit.Enabled = sel;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCustomizableFields frm = new AddCustomizableFields();
            frm.ShowDialog();
            LoadFields();
        }

        private void Edit()
        {
            CustomizableFieldSearchResult entity = olvFieldGroups.SelectedObject as CustomizableFieldSearchResult;
            Debug.Assert(entity != null, "Group of fields is not selected!");
            int entityId = (int)Enum.Parse(typeof(OCustomizableFieldEntities), GetString(entity.EntityName));

            AddCustomizableFields frm = new AddCustomizableFields(entityId);
            frm.ShowDialog();
            LoadFields();
            
            //if (DialogResult.OK != frm.ShowDialog()) return;

            //ServicesProvider.GetInstance().GetTellerServices().Update(teller);

            olvFieldGroups.RefreshSelectedObjects();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void olvFieldGroups_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void olvFieldGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSelected();
        }

    }
}
