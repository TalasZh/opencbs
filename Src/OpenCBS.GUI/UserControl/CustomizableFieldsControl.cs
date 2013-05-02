// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using OpenCBS.Shared;

namespace OpenCBS.GUI.UserControl
{
    public partial class CustomizableFieldsControl : System.Windows.Forms.UserControl
    {
        private int _linkId; // LoanId, SavingsId or ClientId
        private readonly char _entityType;
        private readonly OCustomizableFieldEntities _entity;

        private readonly CustomClass _advancedFields;
        private readonly CollectionList _advancedFieldsCollections;
        private List<CustomizableField> _advancedCustomizableFields;

        public int LinkId { get { return _linkId; } set { _linkId = value; } }
        
        public CustomizableFieldsControl(OCustomizableFieldEntities entity, int? linkId, bool showUpdateButton)
        {
            InitializeComponent();

            _entity = entity;

            if (linkId.HasValue)
            {
                _linkId = linkId.Value;
                panelUpdate.Visible = showUpdateButton;
            }

            switch (_entity)
            {
                case OCustomizableFieldEntities.Loan:
                    _entityType = 'L';
                    break;
                case OCustomizableFieldEntities.Savings:
                    _entityType = 'S';
                    break;
                default:
                    _entityType = 'C';
                    break;
            }

            _advancedFields = new CustomClass();
            _advancedFieldsCollections = new CollectionList();
            LoanAdvancedCustomizableFields();
        }

        private CustomizableFieldValue GetValueForField(List<CustomizableFieldValue> values, string fieldName)
        {
            if (values == null || values.Count == 0) 
                return null;

            foreach (CustomizableFieldValue value in values)
            {
                if (value.Field.Name.Equals(fieldName))
                {
                    return value;
                }
            }
            return null;
        }

        private void LoanAdvancedCustomizableFields()
        {
            // fields
            List<CustomizableField> advancedCustomizableFields = ServicesProvider.GetInstance().
                GetCustomizableFieldsServices().
                SelectCustomizableFields(
                    (int) Enum.Parse(typeof (OCustomizableFieldEntities), _entity.ToString()));

            // values
            List<CustomizableFieldValue> values =
                GetCustomizableFieldsServices().SelectCustomizableFieldValues(_linkId, _entityType);

            if (advancedCustomizableFields != null && advancedCustomizableFields.Count > 0)
            {
                foreach (CustomizableField field in advancedCustomizableFields)
                {
                    CustomProperty myField = null;
                    string fieldName = field.Name;
                    CustomizableFieldValue value = GetValueForField(values, fieldName);
                    string fieldDescription = field.Description;
                    switch (field.Type)
                    {
                        case OCustomizableFieldTypes.Boolean:
                            bool boolValue = value != null && bool.Parse(value.Value);
                            myField = new CustomProperty(fieldName, fieldDescription, boolValue, typeof (bool), false, true);
                            break;
                        case OCustomizableFieldTypes.Number:
                            object numberValue = value == null ? string.Empty : value.Value;
                            myField = new CustomProperty(fieldName, fieldDescription, numberValue, typeof (string), false, true);
                            break;
                        case OCustomizableFieldTypes.String:
                            string stringValue = value == null ? string.Empty : value.Value;
                            myField = new CustomProperty(fieldName, fieldDescription, stringValue, typeof(string), false, true);
                            break;
                        case OCustomizableFieldTypes.Date:
                            DateTime dateTime = value == null ? DateTime.Today : Converter.CustomFieldValueToDate(value.Value);
                            myField = new CustomProperty(fieldName, fieldDescription, dateTime, typeof(DateTime), false, true);
                            break;
                        case OCustomizableFieldTypes.Collection:
                            if (value == null)
                            {
                                _advancedFieldsCollections.Add(fieldName, field.Collection);
                                myField = new CustomProperty(fieldName, fieldDescription, string.Empty, typeof(CollectionType), false, true);    
                            }
                            else
                            {
                                Collection.Items = value.Field.Collection;
                                if (value.Value != null)
                                {
                                    _advancedFieldsCollections.Add(value.Field.Name, value.Field.Collection);
                                    myField = new CustomProperty(value.Field.Name, value.Field.Description, Collection.Items[int.Parse(value.Value)], 
                                        typeof(CollectionType), false, true);
                                }
                                else
                                {
                                    _advancedFieldsCollections.Add(value.Field.Name, value.Field.Collection);
                                    myField = new CustomProperty(value.Field.Name, value.Field.Description, string.Empty, typeof(CollectionType), false, true);
                                }
                            }
                            break;
                        case OCustomizableFieldTypes.Client:
                            CustomClientField clientField;
                            if (value == null || value.Value == string.Empty)
                                clientField = CustomClientField.Empty;
                            else
                            {                                
                                int personId;
                                if (!int.TryParse(value.Value, out personId)) clientField = CustomClientField.Empty;
                                else
                                {
                                    var clientService = ServiceProvider.GetClientServices();
                                    var person = clientService.FindPersonById(personId);
                                    clientField = new CustomClientField(person);
                                }
                            }
                            myField = new CustomProperty(fieldName, fieldDescription, clientField, typeof(CustomClientField), false, true);
                            break;
                    }

                    _advancedFields.Add(myField);
                }
            }

            fieldGrid.Refresh();
        }

        public void Save(int id)
        {
            _linkId = id;
            GetCustomizableFieldsServices().SaveValues(
                _entity, _advancedFields, _advancedFieldsCollections, id, _entityType);
        }

        public void Check()
        {
            GetCustomizableFieldsServices()
                .CheckCustomizableFields(_entity, _advancedFields, _advancedFieldsCollections, _linkId, _entityType);
        }

        private static CustomizableFieldsServices GetCustomizableFieldsServices()
        {
            return ServiceProvider.GetCustomizableFieldsServices();
        }

        private static IServices ServiceProvider
        {
            get { return ServicesProvider.GetInstance(); }
        }

        private void AdvancedCustomizableFieldsControl_Load(object sender, EventArgs e)
        {
            fieldGrid.SelectedObject = _advancedFields;
        }

        private void fieldGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            if (_advancedFields.GetPropertyTypeByName(fieldGrid.SelectedGridItem.Label) == typeof(CollectionType))
            {
                _advancedCustomizableFields = GetCustomizableFieldsServices().
                    SelectCustomizableFields((int)Enum.Parse(typeof(OCustomizableFieldEntities), _entity.ToString()));
                
                if (_advancedCustomizableFields != null)
                {
                    foreach (CustomizableField field in _advancedCustomizableFields)
                    {
                        if (field.Name == fieldGrid.SelectedGridItem.Label) Collection.Items = field.Collection;
                    }
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {    
                Check();
                Save(_linkId);
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }
    }    
}
