using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.ExceptionsHandler;
using Octopus.Enums;
using Octopus.ExceptionsHandler.Exceptions.CustomFieldExceptions;
using Octopus.GUI.UserControl;
using Octopus.Services;
using Octopus.Shared;

namespace Octopus.GUI.Configuration
{
    public partial class AddCustomizableFields : SweetBaseForm
    {
        private bool _isAddMode;
        private CustomClass _myFields = new CustomClass();
        private List<CustomizableField> _fieldList = new List<CustomizableField>();
        private List<CustomizableField> _editFieldList = new List<CustomizableField>();

        public AddCustomizableFields()
        {
            InitializeComponent();
            fieldGrid.SelectedObject = _myFields;
            SetAddNewFieldsMode(true);
            InitializeEntities(0);
            InitializeFieldTypes();
        }

        public AddCustomizableFields(int entityId)
        {
            InitializeComponent();
            fieldGrid.SelectedObject = _myFields;
            SetAddNewFieldsMode(false);
            InitializeEntities(entityId - 1);
            InitializeFieldTypes();

            _fieldList =
                ServicesProvider.GetInstance().GetCustomizableFieldsServices().SelectCustomizableFields(
                    entityId);
            InitializeFieldValues();
        }

        private void InitializeEntities(int index)
        {
            foreach (var entity in Enum.GetValues(typeof(OCustomizableFieldEntities)))
            {
                comboBoxEntities.Items.Add(GetString("entity" + entity));
            }
            comboBoxEntities.SelectedIndex = index;
        }

        private void InitializeFieldTypes()
        {
            foreach (var fieldType in Enum.GetValues(typeof(OCustomizableFieldTypes)))
            {
                comboBoxFieldTypes.Items.Add(GetString("type" + fieldType));
            }
            comboBoxFieldTypes.SelectedIndex = 0;
        }

        private void SetAddNewFieldsMode(bool state)
        {
            _isAddMode = state;
            comboBoxEntities.Enabled = state;
            Text = GetString(state ? "titleAdd" : "titleEdit");
        }

        private void InitializeFieldValues()
        {
            foreach (CustomizableField field in _fieldList)
            {
                CustomProperty myField = field.Type == OCustomizableFieldTypes.Collection
                                             ? new CustomProperty(field.Name, field.Description, field.Collection,
                                                                  typeof (CustomCollection), true, true)
                                             : new CustomProperty(field.Name, field.Description, GetString("type" + field.Type),
                                                                  typeof (string), true, true);
                _myFields.Add(myField);
            }

            fieldGrid.Refresh();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonDeletePropertyClick(object sender, EventArgs e)
        {
            if (_myFields.GetSize() > 0)
            {
                if (MessageBox.Show(GetString("textRemoveFieldAreYouSure"), GetString("textRemoveField"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (_isAddMode)
                    {
                        foreach (CustomizableField field in _fieldList)
                        {
                            if (field.Name.Equals(fieldGrid.SelectedGridItem.Label))
                            {
                                _fieldList.Remove(field);
                                break;
                            }
                        }
                    }
                    else
                    {
                        int fieldId = 0;
                        foreach (CustomizableField field in _fieldList)
                            if (field.Name.Equals(fieldGrid.SelectedGridItem.Label))
                                fieldId = field.Id;

                        if (fieldId != 0)
                        {
                            if (ServicesProvider.GetInstance().GetCustomizableFieldsServices().FieldValuesExistForFieldId(fieldId))
                            {
                                MessageBox.Show(GetString("textValuesExist"), @"Warning!", MessageBoxButtons.OK);
                                return;
                            }
                        }

                        foreach (CustomizableField field in _fieldList)
                        {
                            if (field.Name.Equals(fieldGrid.SelectedGridItem.Label))
                            {
                                _fieldList.Remove(field);
                                ServicesProvider.GetInstance().GetCustomizableFieldsServices().DeleteField(field);
                                break;
                            }
                        }

                        foreach (CustomizableField field in _editFieldList)
                        {
                            if (field.Name.Equals(fieldGrid.SelectedGridItem.Label))
                            {
                                _editFieldList.Remove(field);
                                break;
                            }
                        }
                    }

                    _myFields.Remove(fieldGrid.SelectedGridItem.Label);
                    fieldGrid.Refresh();
                }
            }
        }

        private void textBoxPropertyName_TextChanged(object sender, EventArgs e)
        {
            buttonAddField.Enabled = textBoxFieldName.Text.Length > 0;
        }

        private void buttonAddProperty_Click(object sender, EventArgs e)
        {
            try
            {
                if (_myFields.Contains(textBoxFieldName.Text) || _myFields.Contains(textBoxFieldName.Text + "*"))
                {
                    MessageBox.Show(GetString("textUniqueField"));
                    
                }
                
                string fieldName = textBoxFieldName.Text;
                if (checkBoxNewMandatory.Checked) fieldName += "*";

                CustomProperty myField;
                List<string> list = new List<string>();
                var type = (OCustomizableFieldTypes)Enum.Parse(typeof(OCustomizableFieldTypes), GetString(comboBoxFieldTypes.Text), true);

                if (type == OCustomizableFieldTypes.Collection)
                {
                    if (listBox.Items.Count < 1)
                    {
                        MessageBox.Show(GetString("textAddOneItem"));
                        return;
                    }

                    foreach (string item in listBox.Items) list.Add(item);
                    {
                        myField = new CustomProperty(fieldName, textBoxFieldDesc.Text, list, typeof(CustomCollection), true,
                                                     true);
                    }
                }
                else
                {
                    myField = new CustomProperty(fieldName, textBoxFieldDesc.Text, GetString("type" + type),
                                                 typeof(string), true, true);
                }

                _myFields.Add(myField);
                fieldGrid.Refresh();

                CustomizableField field = new CustomizableField
                {
                    Name = fieldName,
                    Description = textBoxFieldDesc.Text,
                    Entity =
                        (OCustomizableFieldEntities)
                        Enum.Parse(typeof(OCustomizableFieldEntities),
                                   GetString(comboBoxEntities.Text), true),
                    IsMandatory = checkBoxNewMandatory.Checked,
                    IsUnique = checkBoxNewUnique.Checked
                };

                if (type == OCustomizableFieldTypes.Collection)
                {
                    field.Type = OCustomizableFieldTypes.Collection;
                    field.Collection = list;
                }
                else
                {
                    field.Type = (OCustomizableFieldTypes)Enum.Parse(typeof(OCustomizableFieldTypes), GetString(comboBoxFieldTypes.Text), true);
                }

                if (_isAddMode)
                    _fieldList.Add(field);
                else
                    _editFieldList.Add(field);

                textBoxFieldName.Text = string.Empty;
                textBoxFieldDesc.Text = string.Empty;
                listBox.Items.Clear();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            bool fieldsExist = ServicesProvider.GetInstance().GetCustomizableFieldsServices().
                CustomizableFieldsExistFor(
                    (OCustomizableFieldEntities) Enum.Parse(typeof (OCustomizableFieldEntities),
                                                                    GetString(comboBoxEntities.Text), true));

            if (_isAddMode && fieldsExist)
            {
                MessageBox.Show(string.Format(GetString("FieldsAlreadyCreated"), comboBoxEntities.Text));
                return;
            }
            
            if (_myFields.GetSize() == 0)
            {
                MessageBox.Show(GetString("NoFieldsWasAdded"));
                return;
            }

            try
            {
                ServicesProvider.GetInstance().GetCustomizableFieldsServices().AddCustomizableFields(
                    _isAddMode
                        ? _fieldList
                        : _editFieldList);
                //update collecion
                if (!_isAddMode)
                {
                    foreach (CustomizableField field in
                        _fieldList.Where(field => field.Type == OCustomizableFieldTypes.Collection))
                    {
                        ServicesProvider.GetInstance().GetCustomizableFieldsServices().
                            UpdateCollectionField(field);
                    }
                }
                Close();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void buttonAddListItem_Click(object sender, EventArgs e)
        {
            if (listBox.Items.Contains(textBoxListItem.Text))
            {
                MessageBox.Show(GetString("CollectionValueMustBeUnique"));
                return;
            }
            
            listBox.Items.Add(textBoxListItem.Text);
            textBoxListItem.Text = string.Empty;
        }

        private void comboBoxPropertyTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            groupBoxCollectionDetails.Enabled = GetString(comboBoxFieldTypes.Text) == OCustomizableFieldTypes.Collection.ToString();

            if (GetString(comboBoxFieldTypes.Text) == OCustomizableFieldTypes.Boolean.ToString())
            {
                checkBoxNewMandatory.Visible = false;
                checkBoxNewUnique.Visible = false;
            }
            else if (GetString(comboBoxFieldTypes.Text) == OCustomizableFieldTypes.Collection.ToString())
            {
                checkBoxNewMandatory.Visible = true;
                checkBoxNewUnique.Visible = false;
            }
            else
            {
                checkBoxNewMandatory.Visible = true;
                checkBoxNewUnique.Visible = true;                
            }
            
            checkBoxNewMandatory.Checked = false;
            checkBoxNewUnique.Checked = false;
        }

        private void textBoxListItem_TextChanged(object sender, EventArgs e)
        {
            buttonAddListItem.Enabled = textBoxListItem.Text.Length > 0;
        }

        private void buttonDeleteListItem_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex > -1) listBox.Items.RemoveAt(listBox.SelectedIndex);
        }

        private void fieldGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            List<CustomizableField> list = new List<CustomizableField>();
            list.AddRange(_fieldList);
            if (!_isAddMode) list.AddRange(_editFieldList);

            foreach (CustomizableField field in list)
            {
                if (field.Name.Equals(fieldGrid.SelectedGridItem.Label))
                {
                    checkBoxMandatory.Checked = field.IsMandatory;
                    checkBoxUnique.Checked = field.IsUnique;
                }
            }
        }

        private void textBoxFieldName_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = e.KeyChar;
            e.Handled = keyCode==44;
        }
    }
}
