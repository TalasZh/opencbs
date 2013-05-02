using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Contracts.Collaterals;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Products.Collaterals;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.Clients;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Contracts
{
    public partial class ContractCollateralForm : SweetBaseForm
    {
        private CollateralProduct product;
        private ContractCollateral contractCollateral;
        private CustomClass myProperties;
        private readonly Form _mdiParent;
        private CollectionList collections;

        public ContractCollateral ContractCollateral
        {
            get
            {
                return contractCollateral;
            }
        }

        public ContractCollateralForm(CollateralProduct product)
        {
            this.product = product;
            contractCollateral = new ContractCollateral();
            myProperties = new CustomClass();
            collections = new CollectionList();

            InitializeComponent();
            FillCollateralProperties();
        }

        public ContractCollateralForm(CollateralProduct product, ContractCollateral contractCollateral, bool isView)
        {
            this.product = product;
            this.contractCollateral = contractCollateral;
            myProperties = new CustomClass();
            collections = new CollectionList();

            InitializeComponent();
            FillCollateralPropertyValues(contractCollateral);

            if(isView)
            {
                propertyGrid.Enabled = false;
                groupBoxOwnerDetails.Enabled = false;
                buttonSave.Enabled = false;
            }
        }

        private void FillCollateralProperties()
        {
            foreach(CollateralProperty property in product.Properties)
            {
                CustomProperty myProp = null;
                if (property.Type == OCollateralPropertyTypes.Number)
                {
                    myProp = new CustomProperty(property.Name, property.Description, new decimal(), typeof(decimal), false, true);
                }
                else if (property.Type == OCollateralPropertyTypes.String)
                {
                    myProp = new CustomProperty(property.Name, property.Description, string.Empty, typeof(string), false, true);
                }
                else if (property.Type == OCollateralPropertyTypes.Date)
                {
                    myProp = new CustomProperty(property.Name, property.Description, new DateTime(), typeof(DateTime), false, true);
                } 
                else if (property.Type == OCollateralPropertyTypes.Collection)
                {
                    collections.Add(property.Name, property.Collection);
                    myProp = new CustomProperty(property.Name, property.Description, string.Empty, typeof(CollectionType), false, true);
                } 
                else if (property.Type == OCollateralPropertyTypes.Owner)
                {
                    myProp = new CustomProperty(property.Name, property.Description, string.Empty, typeof(Person), false, true);
                }

                myProperties.Add(myProp);
            }

            propertyGrid.Refresh();

            /*
            // guarantor
            IClient client = SelectGuarantor();
            if (client != null)
                AddCollateralProperty("Guarantor", client);
            */

            /*CustomProperty myProp = new CustomProperty(propName, client, typeof(IClient), true, true);
            myProperties.Add(myProp);
            propertyGrid.Refresh();*/

            /*private void toolStripMenuItemString_Click(object sender, EventArgs e)
            {
                var myclient = (IClient)propertyGrid.SelectedGridItem.Value;
                MessageBox.Show(((Person)myclient).FirstName + " " + ((Person)myclient).LastName);
            }*/
        }

        private void FillCollateralPropertyValues(ContractCollateral contractCollateral)
        {
            foreach (CollateralPropertyValue propertyValue in contractCollateral.PropertyValues)
            {
                CustomProperty myProp = null;
                if (propertyValue.Property.Type == OCollateralPropertyTypes.Number)
                {
                    myProp = new CustomProperty(propertyValue.Property.Name, propertyValue.Property.Description, 
                        Converter.CustomFieldValueToDecimal(propertyValue.Value), typeof(decimal), false, true);
                }
                else if (propertyValue.Property.Type == OCollateralPropertyTypes.String)
                {
                    myProp = new CustomProperty(propertyValue.Property.Name, propertyValue.Property.Description, 
                        propertyValue.Value, typeof(string), false, true);
                }
                else if (propertyValue.Property.Type == OCollateralPropertyTypes.Date)
                {
                    myProp = new CustomProperty(propertyValue.Property.Name, propertyValue.Property.Description,
                        Converter.CustomFieldValueToDate(propertyValue.Value), typeof(DateTime), false, true);
                }
                else if (propertyValue.Property.Type == OCollateralPropertyTypes.Collection)
                {
                    if (propertyValue.Value != null)
                    {
                        Collection.Items = propertyValue.Property.Collection;
                        collections.Add(propertyValue.Property.Name, propertyValue.Property.Collection);
                        myProp = new CustomProperty(propertyValue.Property.Name, propertyValue.Property.Description,
                            Collection.Items[int.Parse(propertyValue.Value)], typeof(CollectionType), false, true);
                    }
                    else
                    {
                        Collection.Items = propertyValue.Property.Collection;
                        collections.Add(propertyValue.Property.Name, propertyValue.Property.Collection);
                        myProp = new CustomProperty(propertyValue.Property.Name, propertyValue.Property.Description,
                            string.Empty, typeof(CollectionType), false, true);
                    }
                }
                else if (propertyValue.Property.Type == OCollateralPropertyTypes.Owner)
                {
                    if (propertyValue.Value != null)
                    {
                        Person client = (Person)ServicesProvider.GetInstance().GetClientServices().FindTiers(int.Parse(propertyValue.Value), OClientTypes.Person);
                        myProp = new CustomProperty(propertyValue.Property.Name, propertyValue.Property.Description, client, typeof(Person), false, true);
                    }
                    else
                    {
                        myProp = new CustomProperty(propertyValue.Property.Name, propertyValue.Property.Description, string.Empty, typeof(Person), false, true);
                    }
                }

                myProperties.Add(myProp);
            }
            propertyGrid.Refresh();
        }

        private void ContractCollateralForm_Load(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = myProperties;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            contractCollateral = null;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string amountProperty = string.Empty;
            if (myProperties.GetPropertyValueByName("Montant") != null) amountProperty = "Montant";
            if (myProperties.GetPropertyValueByName("Сумма") != null) amountProperty = "Сумма";
            if (myProperties.GetPropertyValueByName("Amount") != null) amountProperty = "Amount";

            if (decimal.Parse(myProperties.GetPropertyValueByName(amountProperty).ToString()) <= 0)
            {
                MessageBox.Show("Please, enter collateral amount!");
                return;
            }
            
            List<CollateralPropertyValue> propertyValues = new List<CollateralPropertyValue>();

            foreach(CollateralProperty collateralProperty in product.Properties)
            {
                CollateralPropertyValue contractCollateralProperty = new CollateralPropertyValue();
                contractCollateralProperty.Property = collateralProperty;
                
                if (collateralProperty.Type == OCollateralPropertyTypes.Number)
                {
                    var decimalValue = (decimal) myProperties.GetPropertyValueByName(collateralProperty.Name);
                    contractCollateralProperty.Value = Converter.CustomFieldDecimalToString(decimalValue);
                }
                else if (collateralProperty.Type == OCollateralPropertyTypes.String)
                {
                    //if ((string)myProperties.GetPropertyValueByName(collateralProperty.Name) == string.Empty) MessageBox.Show("String is empty!");
                    
                    contractCollateralProperty.Value = (string) myProperties.GetPropertyValueByName(collateralProperty.Name);
                }
                else if (collateralProperty.Type == OCollateralPropertyTypes.Date)
                {
                    DateTime dateValue = (DateTime) myProperties.GetPropertyValueByName(collateralProperty.Name);
                    contractCollateralProperty.Value = Converter.CustomFieldDateToString(dateValue);
                }
                else if (collateralProperty.Type == OCollateralPropertyTypes.Collection)
                {
                    //if ((string)myProperties.GetPropertyValueByName(collateralProperty.Name) == null) MessageBox.Show("collection is null!");
                    
                    //int index = Collection.GetItemIndexByName((string)myProperties.GetPropertyValueByName(collateralProperty.Name));
                    //if (index != -1) contractCollateralProperty.Value = index.ToString();

                    int index = collections.GetItemIndexByName(collateralProperty.Name, (string)myProperties.GetPropertyValueByName(collateralProperty.Name));
                    if (index != -1) contractCollateralProperty.Value = index.ToString();
                }
                else if (collateralProperty.Type == OCollateralPropertyTypes.Owner)
                {
                    //if (myProperties.GetPropertyValueByName(collateralProperty.Name) == null) MessageBox.Show("Person is null!");

                    //MessageBox.Show(myProperties.GetPropertyValueByName(collateralProperty.Name).GetType().ToString());
                    if (myProperties.GetPropertyValueByName(collateralProperty.Name).GetType() == typeof(Person))
                        contractCollateralProperty.Value = ((Person)myProperties.GetPropertyValueByName(collateralProperty.Name)).Id.ToString();
                }

                propertyValues.Add(contractCollateralProperty);
            }

            contractCollateral.PropertyValues = propertyValues;

            Close();
        }

        private Person AddOwner()
        {
            var personForm = new ClientForm(OClientTypes.Person, this.MdiParent, true);
            personForm.ShowDialog();
            //client = personForm.Person;

            try
            {
                //textBoxName.Text = ServicesProvider.GetInstance().GetClientServices().ClientIsAPerson(client) ? client.Name : String.Empty;
                if (ServicesProvider.GetInstance().GetClientServices().ClientIsAPerson(personForm.Person)) return personForm.Person;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            return null;
        }

        private void buttonAddOwner_Click(object sender, EventArgs e)
        {
            Person owner = AddOwner();
            if (owner != null)
            {
                //textBoxOwner.Text = owner.Name;
                myProperties.SetPropertyValueByName(propertyGrid.SelectedGridItem.Label, owner);
                propertyGrid.Refresh();
            }
        }

        private Person SelectOwner()
        {
            using (SearchClientForm searchClientForm = SearchClientForm.GetInstance(OClientTypes.Person, true))
            {
                searchClientForm.ShowDialog();

                try
                {
                    if (ServicesProvider.GetInstance().GetClientServices().ClientIsAPerson(searchClientForm.Client)
                        && !searchClientForm.Client.Active)
                        return (Person)searchClientForm.Client;
                    else
                        return null;

                    //else
                    //  textBoxName.Text = String.Empty;

                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                    return null;
                }
            }
        }

        private void buttonSelectOwner_Click(object sender, EventArgs e)
        {
            IClient owner = SelectOwner();
            if (owner != null)
            {
                //textBoxOwner.Text = ((Person)owner).Name;
                myProperties.SetPropertyValueByName(propertyGrid.SelectedGridItem.Label, owner);
                propertyGrid.Refresh();
            }
        }

        private void propertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            if (myProperties.GetPropertyTypeByName(propertyGrid.SelectedGridItem.Label) == typeof(Person))
            {
                groupBoxOwnerDetails.Enabled = true;
                //MessageBox.Show(myProperties.GetPropertyValueByName(propertyGrid.SelectedGridItem.Label).ToString());
                //if (myProperties.GetPropertyValueByName(propertyGrid.SelectedGridItem.Label) != null)
                //textBoxOwner.Text = ((Person) myProperties.GetPropertyValueByName(propertyGrid.SelectedGridItem.Label)).Name;
            }
            else if (myProperties.GetPropertyTypeByName(propertyGrid.SelectedGridItem.Label) == typeof(CollectionType))
            {
                foreach (CollateralProperty property in product.Properties)
                {
                    if (property.Name == propertyGrid.SelectedGridItem.Label) 
                        Collection.Items = property.Collection;
                }
            }
            else
            {
                //textBoxOwner.Text = string.Empty;
                groupBoxOwnerDetails.Enabled = false;
            }
        }

        private void buttonClearOwner_Click(object sender, EventArgs e)
        {
            myProperties.SetPropertyValueByName(propertyGrid.SelectedGridItem.Label, string.Empty);
            propertyGrid.Refresh();
        }
    }

    public class CollectionList
    {
        private Dictionary<string, List<string>> Collections;

        public CollectionList()
        {
            Collections = new Dictionary<string, List<string>>();
        }

        public void Add(string collectionName, List<string> collection)
        {
            Collections.Add(collectionName, collection);
        }

        public int GetItemIndexByName(string collectionName, string itemValue)
        {
            List<string> collection;
            if (Collections.ContainsKey(collectionName))
            {
                collection = Collections[collectionName];
                for (int i = 0; i < collection.Count; i++)
                    if (collection[i] == itemValue)
                        return i;
            }
            return -1;
        }
    }

    internal class Collection
    {
        internal static List<string> Items;

        /*public static int GetItemIndexByName(string itemValue)
        {
            for (int i = 0; i < Items.Count; i++)
                if (Items[i] == itemValue)
                    return i;
            return -1;
        }*/
    }

    [Browsable(true)]
    [TypeConverter(typeof(CollectionConverter))]
    public class CollectionType
    {
    }

    public class CollectionConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // true means show a combobox
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // true will limit to list. false will show the list, but allow free-form entry
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(Collection.Items);
        }
    }


}
