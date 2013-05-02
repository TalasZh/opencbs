// LICENSE PLACEHOLDER

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
