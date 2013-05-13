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
using System.Windows.Forms;
using System.Collections.Generic;
using OpenCBS.Enums;
using OpenCBS.GUI.UserControl;
using OpenCBS.Shared;
using OpenCBS.Services;
using OpenCBS.ExceptionsHandler;
using OpenCBS.CoreDomain.Products.Collaterals;

namespace OpenCBS.GUI.Products
{
    public partial class FrmAddCollateralProduct : SweetBaseForm
    {
        private bool isAddMode;
        private CollateralProduct collateralProduct;
        private CustomClass myProperties = new CustomClass();
        private List<CollateralProperty> editPropertyList = new List<CollateralProperty>();

        public FrmAddCollateralProduct()
        {
            InitializeComponent();

            propertyGrid.SelectedObject = myProperties;

            collateralProduct = new CollateralProduct();

            InitializeMenuPropertyTypes();
            SetAddNewProductMode(true);
        }

        public FrmAddCollateralProduct(CollateralProduct collateralProduct)
        {
            InitializeComponent();
            this.collateralProduct = collateralProduct;

            propertyGrid.SelectedObject = myProperties;

            InitializeMenuPropertyTypes();
            InitializeProductValues();
            SetAddNewProductMode(false);
        }
        
        private void SetAddNewProductMode(bool state)
        {
            isAddMode = state;

            buttonDeleteProperty.Visible = state;
            buttonDeleteListItem.Visible = state;

            if (state)
            {
                Text = GetString("titleAdd");

                // Adding default properties
                CustomProperty propertyAmount = new CustomProperty(GetString("propertyAmount"), GetString("propertyAmountDescription"),
                    GetString("typeNumber"), typeof(int), true, true);
                myProperties.Add(propertyAmount);

                CustomProperty propertyDesc = new CustomProperty(GetString("propertyDescription"), GetString("propertyDescriptionDescription"),
                    GetString("typeString"), typeof(string), true, true);
                myProperties.Add(propertyDesc);
            }
            else
            {
                Text = GetString("titleEdit");
            }
        }

        private void InitializeProductValues()
        {
            textBoxProductName.Text = collateralProduct.Name;
            textBoxProductDesc.Text = collateralProduct.Description;

            CustomProperty myProp = null;
            foreach (CollateralProperty property in collateralProduct.Properties)
            {
                if (property.Type == OCollateralPropertyTypes.Collection)
                {
                    myProp = new CustomProperty(property.Name, property.Description, property.Collection, typeof(List<string>), true, true);
                }
                else
                {
                    myProp = new CustomProperty(property.Name, property.Description, GetString("type"+property.Type), typeof(string), true, true);
                }
                myProperties.Add(myProp);
            }
            propertyGrid.Refresh();
        }

        private void InitializeMenuPropertyTypes()
        {
            foreach (var propertyType in Enum.GetValues(typeof(OCollateralPropertyTypes)))
                comboBoxPropertyTypes.Items.Add(GetString("type" + propertyType));
            comboBoxPropertyTypes.SelectedIndex = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonDeleteProperty_Click(object sender, EventArgs e)
        {
            if (myProperties.GetSize() > 0)
            {
                //if (propertyGrid.SelectedGridItem.Label.Equals("Amount") || propertyGrid.SelectedGridItem.Label.Equals("Description"))
                if (propertyGrid.SelectedGridItem.Label.Equals(GetString("propertyAmount")) || 
                    propertyGrid.SelectedGridItem.Label.Equals(GetString("propertyDescription")))
                {
                    MessageBox.Show("This field is mandatory and cannot be deleted!");
                }
                else
                {
                    if (MessageBox.Show("Are you sure?", "Remove property", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        myProperties.Remove(propertyGrid.SelectedGridItem.Label);
                        propertyGrid.Refresh();
                    }
                }
            }
        }

        private void textBoxPropertyName_TextChanged(object sender, EventArgs e)
        {
            buttonAddProperty.Enabled = textBoxPropertyName.Text.Length > 0;
        }

        private void buttonAddProperty_Click(object sender, EventArgs e)
        {
            if (myProperties.Contains(textBoxPropertyName.Text))
            {
                MessageBox.Show("Property name must be unique!");
            }
            else
            {
                CustomProperty myProp = null;
                List<string> list = new List<string>();
                var type = (OCollateralPropertyTypes) Enum.Parse(typeof(OCollateralPropertyTypes), GetString(comboBoxPropertyTypes.Text), true);

                if (type == OCollateralPropertyTypes.Collection)
                {
                    if (listBox.Items.Count < 1)
                    {
                        MessageBox.Show("Please add at least one item to the collection!");
                        return;
                    }
                    
                    foreach (string item in listBox.Items) list.Add(item);
                    myProp = new CustomProperty(textBoxPropertyName.Text, textBoxPropertyDesc.Text, list, typeof(List<string>), true, true);
                }
                else
                {
                    myProp = new CustomProperty(textBoxPropertyName.Text, textBoxPropertyDesc.Text, GetString("type"+type), typeof(string), true, true);
                }

                myProperties.Add(myProp);
                propertyGrid.Refresh();

                if (!isAddMode)
                {
                    CollateralProperty property = new CollateralProperty();
                    property.Name = textBoxPropertyName.Text;
                    property.Description = textBoxPropertyDesc.Text;

                    if (type == OCollateralPropertyTypes.Collection)
                    {
                        property.Type = OCollateralPropertyTypes.Collection;
                        property.Collection = list;
                    }
                    else
                    {
                        property.Type = (OCollateralPropertyTypes)Enum.Parse(typeof(OCollateralPropertyTypes), GetString(comboBoxPropertyTypes.Text), true);
                    }

                    editPropertyList.Add(property);
                }

                textBoxPropertyName.Text = string.Empty;
                textBoxPropertyDesc.Text = string.Empty;
                listBox.Items.Clear();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxProductName.Text.Length == 0)
            {
                MessageBox.Show("Please, enter collateral product name!");
                return;
            }
            if (textBoxProductDesc.Text.Length == 0)
            {
                MessageBox.Show("Please, enter collateral product description!");
                return;
            }
            if (myProperties.GetSize() == 0)
            {
                MessageBox.Show("Please, enter at least one property!");
                return;
            }
            try
            {
                if (isAddMode)
                {

                    collateralProduct.Name = textBoxProductName.Text;
                    collateralProduct.Description = textBoxProductDesc.Text;

                    List<CollateralProperty> propertiesList = new List<CollateralProperty>();

                    foreach (CustomProperty property in myProperties)
                    {
                        CollateralProperty collateralProperty = new CollateralProperty();
                        collateralProperty.Name = property.Name;
                        collateralProperty.Description = property.Description;

                        if (property.Value is List<string>)
                        {
                            collateralProperty.Type = OCollateralPropertyTypes.Collection;
                            collateralProperty.Collection = (List<string>)property.Value;
                        }
                        else
                        {
                            //collateralProperty.Type = (OCollateralPropertyTypes) Enum.Parse(typeof(OCollateralPropertyTypes), property.Value.ToString(), true);
                            collateralProperty.Type = (OCollateralPropertyTypes) Enum.Parse(typeof(OCollateralPropertyTypes), 
                                GetString(property.Value.ToString()), true);
                        }

                        propertiesList.Add(collateralProperty);
                    }

                    collateralProduct.Properties = propertiesList;

                    ServicesProvider.GetInstance().GetCollateralProductServices().AddCollateralProduct(collateralProduct);
                }
                else
                {
                    ServicesProvider.GetInstance().GetCollateralProductServices().UpdateCollateralProduct(
                        collateralProduct.Id, collateralProduct.Name, textBoxProductName.Text, textBoxProductDesc.Text);

                    foreach (CollateralProperty property in editPropertyList)
                        ServicesProvider.GetInstance().GetCollateralProductServices().AddCollateralProperty(collateralProduct.Id, property);
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
                MessageBox.Show("Each value in the collection should be unique!");
                return;
            }
            
            listBox.Items.Add(textBoxListItem.Text);
            textBoxListItem.Text = string.Empty;
        }

        private void comboBoxPropertyTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            groupBoxCollectionDetails.Enabled = GetString(comboBoxPropertyTypes.Text) == OCollateralPropertyTypes.Collection.ToString();
        }

        private void textBoxListItem_TextChanged(object sender, EventArgs e)
        {
            buttonAddListItem.Enabled = textBoxListItem.Text.Length > 0;
        }

        private void buttonDeleteListItem_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex > -1) listBox.Items.RemoveAt(listBox.SelectedIndex);
        }
    }

}
