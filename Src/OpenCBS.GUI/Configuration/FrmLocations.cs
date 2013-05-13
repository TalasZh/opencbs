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
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI
{
    public partial class FrmLocations : SweetBaseForm
    {
        private TreeNode node;
        private Province province;
        private District district;
        private City city;
        
        public FrmLocations()
        {
            InitializeComponent();
            buttonUpdate.Text = GetString("buttonEdit");
        }
          
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmProvinces_Load(object sender, EventArgs e)
        {
            LoadLocations();
            lblType.Text = string.Empty;
            lblAddType.Text = string.Empty;
        }

        /// <summary>
        /// In the treeview nodes tag we store:<br/>
        /// - NULL for the root node<br/>
        /// - Province for the provinces<br/>
        /// - Disctrict for the districts<br/>
        /// - and a string for the cities
        /// </summary>
        private void LoadLocations()
        {
            List<Province> provinces = ServicesProvider.GetInstance().GetLocationServices().GetProvinces();
            List<District> districts = ServicesProvider.GetInstance().GetLocationServices().GetDistricts();

            treeViewLocations.Nodes.Clear();
            treeViewLocations.BeginUpdate();

            TreeNode rootNode = new TreeNode("Locations");
            treeViewLocations.Nodes.Add(rootNode);
            foreach (Province p in provinces)
            {
                TreeNode pnode = new TreeNode(p.Name) {Tag = p};
                rootNode.Nodes.Add(pnode);
                foreach (District d in districts)
                {
                    if ( (d.Province != null) && (d.Province.Id == p.Id))
                    {
                        TreeNode dnode = new TreeNode(d.Name) {Tag = d};
                        pnode.Nodes.Add(dnode);
                        List<City> cities = ServicesProvider.GetInstance().GetLocationServices().FindCityByDistrictId(d.Id);
                        foreach (City city in cities)
                        {
                            TreeNode cnode = new TreeNode(city.Name) {Tag = city};
                            dnode.Nodes.Add(cnode);
                        }
                    }
                }
            }
            rootNode.ExpandAll();
            treeViewLocations.EndUpdate();
            treeViewLocations.Sort();
        }

        private void treeViewLocations_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = treeViewLocations.SelectedNode;
            buttonAdd.Enabled = !((node.Tag != null) && (node.Tag is String));

            if (node.Tag == null)
            {
                lblType.Text = "";
                lblAddType.Text = GetString("province.Text");
                buttonUpdate.Enabled = false;
                buttonDelete.Enabled = false;
            }
            else if ((node.Tag is Province))
            {
                lblType.Text = GetString("province.Text") + ": " + node.Text;
                lblAddType.Text = GetString("district.Text");
                buttonDelete.Enabled = true;
                buttonUpdate.Enabled = true;

            }
            else if ((node.Tag is District))
            {
                lblType.Text = GetString("district.Text") + ": " + node.Text;
                lblAddType.Text = GetString("city.Text");
                buttonDelete.Enabled = true;
                buttonUpdate.Enabled = true;

            }
            else
            {
                lblType.Text = GetString("city.Text") + ": " + node.Text;
                lblAddType.Text = "";
                buttonAdd.Enabled = false;
                buttonDelete.Enabled = true;
                buttonUpdate.Enabled = true;
            }
        }
       
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewLocations.SelectedNode;
            try
            {
                if (node == null)
                {
                    buttonAdd.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonUpdate.Enabled = false;
                    return;
                }
                if (!string.IsNullOrEmpty(tbName.Text))
                {
                    var locationServices = ServicesProvider.GetInstance().GetLocationServices();
                    if (node.Tag == null)
                    {
                        // Add province
                        int newId = locationServices.AddProvince(tbName.Text);
                        Province p = new Province(newId, tbName.Text);
                        TreeNode pnode = new TreeNode(tbName.Text) {Tag = p};
                        node.Nodes.Add(pnode);
                    }
                    else if ((node.Tag is Province))
                    {
                        // Add district
                        Province province = (Province) node.Tag;
                        int newId = locationServices.AddDistrict(tbName.Text, province.Id);
                        District d = new District(newId, tbName.Text, province);
                        TreeNode dnode = new TreeNode(tbName.Text) {Tag = d};
                        node.Nodes.Add(dnode);
                    }
                    else if ((node.Tag is District))
                    {
                        // Add city
                        District district = (District) node.Tag;
                        City city = new City {Name = tbName.Text, DistrictId = district.Id};
                        city.Id = locationServices.AddCity(city);
                        TreeNode cnode = new TreeNode(tbName.Text) {Tag = city};
                        node.Nodes.Add(cnode);
                        buttonAdd.Enabled = false;
                    }
                    tbName.Clear();
                }
            } catch(Exception exception)
            {
                var exceptionStatus = CustomExceptionHandler.ShowExceptionText(exception);
                new frmShowError(exceptionStatus).ShowDialog();
                return;
            }

            node.Expand();
            treeViewLocations.Sort();

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonUpdate.Text.Equals(GetString("buttonEdit")))
                {
                    try
                    {
                        node = treeViewLocations.SelectedNode;
                        if (node == null) return;

                        if ((node.Tag is Province))
                        {
                            province = (Province) node.Tag;
                            tbName.Text = province.Name;
                        }
                        else if ((node.Tag is District))
                        {
                            district = (District) node.Tag;
                            tbName.Text = district.Name;
                        }
                        else if ((node.Tag is City))
                        {
                            city = (City) node.Tag;
                            tbName.Text = city.Name;
                        }
                    } catch (Exception exception)
                    {
                        var exceptionStatus = CustomExceptionHandler.ShowExceptionText(exception);
                        new frmShowError(exceptionStatus).ShowDialog();
                        return;
                    }

                    treeViewLocations.Enabled = false;
                    buttonAdd.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonExit.Enabled = false;
                    buttonUpdate.Text = GetString("buttonSave");
                }
                else
                {
                    try
                    {
                        if ((node.Tag is Province))
                        {
                            province.Name = tbName.Text;
                            ServicesProvider.GetInstance().GetLocationServices().UpdateProvince(province);
                        }
                        else if ((node.Tag is District))
                        {
                            district.Name = tbName.Text;
                            ServicesProvider.GetInstance().GetLocationServices().UpdateDistrict(district);
                        }
                        else if ((node.Tag is City))
                        {
                            city.Name = tbName.Text;
                            ServicesProvider.GetInstance().GetLocationServices().UpdateCity(city);
                        }
                    } catch (Exception exception)
                    {
                        var exceptionStatus = CustomExceptionHandler.ShowExceptionText(exception);
                        new frmShowError(exceptionStatus).ShowDialog();
                        return;
                    }

                    node.Text = tbName.Text;
                    buttonExit.Enabled = true;
                    buttonAdd.Enabled = true;
                    buttonDelete.Enabled = true;
                    treeViewLocations.Enabled = true;
                    buttonUpdate.Text = GetString("buttonEdit");
                }
            } catch (Exception exception)
            {
                var exceptionStatus = CustomExceptionHandler.ShowExceptionText(exception);
                new frmShowError(exceptionStatus).ShowDialog();
                return;
            }

            treeViewLocations.Sort();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewLocations.SelectedNode;
            try
            {
                if (node != null)
                {
                    if (node.Tag == null)
                    {
                        buttonAdd.Enabled = false;
                        buttonDelete.Enabled = false;
                        buttonUpdate.Enabled = false;
                    }
                    else if ((node.Tag is Province))
                    {
                        // Delete province
                        Province province = (Province) node.Tag;
                        bool deleteProvince =
                            ServicesProvider.GetInstance().GetLocationServices().DeleteProvince(province);
                        if (!deleteProvince)
                        {
                            MessageBox.Show(GetString("NeedToDeleteAllDistrinctsAndCities.Text"), "",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                        }
                        else
                            node.Nodes.Remove(node);
                    }
                    else if ((node.Tag is District))
                    {
                        // Delete district
                        District district = (District) node.Tag;
                        bool deleteDistrict =
                            ServicesProvider.GetInstance().GetLocationServices().DeleteDistrict(district.Id);
                        if (!deleteDistrict)
                            MessageBox.Show(GetString("NeedToDeleteAllCities.Text"), "", MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                        else
                        {
                            node.Nodes.Remove(node);
                        }
                    }
                    else
                    {
                        // Delete City
                        City city = (City) node.Tag;
                        ServicesProvider.GetInstance().GetLocationServices().DeleteCity(city.Id);
                        node.Nodes.Remove(node);
                    }
                }
            } catch (Exception exception)
            {
                var exceptionStatus = CustomExceptionHandler.ShowExceptionText(exception);
                new frmShowError(exceptionStatus).ShowDialog();
                return;
            }
            treeViewLocations.Sort();
        }
    }
}
