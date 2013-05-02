//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI.Configuration
{
    public partial class FrmEconomicActivity : SweetBaseForm
    {
        private TreeNode selectedNode;
        private EconomicActivity _economicActivity;
        private string IsSame;
        
        public FrmEconomicActivity()
        {
            InitializeComponent();
            Initialization();
        }

        private void Initialization()
        {
            TreeNode root = new TreeNode(GetString("doa.Text"));
            tvEconomicActivity.Nodes.Add(root);

            List<EconomicActivity> doaList = ServicesProvider.GetInstance().GetEconomicActivityServices().FindAllEconomicActivities();
            tvEconomicActivity.BeginUpdate();
            foreach (EconomicActivity domainOfApplication in doaList)
            {
                TreeNode node = new TreeNode(domainOfApplication.Name) {Tag = domainOfApplication};
                root.Nodes.Add(node);
                if(domainOfApplication.HasChildrens)
                    _DisplayAllChildrensNodes(node,domainOfApplication);
            }
            tvEconomicActivity.EndUpdate();
            root.Expand();

            tvEconomicActivity.Sort();

            SelectRootNode();
        }

        private void SelectRootNode()
        {
            // Selecting TreeView root node if any
            if (tvEconomicActivity.Nodes.Count > 0)
            {
                tvEconomicActivity.SelectedNode = tvEconomicActivity.Nodes[0];
                tvEconomicActivity.Focus();
            }            
        }

        private static void _DisplayAllChildrensNodes(TreeNode pNode, EconomicActivity pApplication)
        {
            foreach (EconomicActivity domainOfApplication in pApplication.Childrens)
            {
                TreeNode node = new TreeNode(domainOfApplication.Name) {Tag = domainOfApplication};
                pNode.Nodes.Add(node);
                if (domainOfApplication.HasChildrens)
                    _DisplayAllChildrensNodes(node, domainOfApplication);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (tvEconomicActivity.SelectedNode != null)
                AddDomain();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (buttonEdit.Text.Equals(GetString("buttonSave"))) 
                EditDomain();
            else
            {
                if (tvEconomicActivity.SelectedNode != null) EditDomain();
                else MessageBox.Show(GetString("messageBoxNoSelection.Text"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var selectedNode = tvEconomicActivity.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show(GetString("messageBoxNoSelection.Text"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {

                var economicActivityServices = ServicesProvider.GetInstance().GetEconomicActivityServices();
                bool isEditable = economicActivityServices.NodeEditable(selectedNode.Tag);
                if (!isEditable) return;

                var economicActivity = (EconomicActivity)selectedNode.Tag;
                var format = GetString("areYouSureMessage.Text");
                var message = string.Format(format, economicActivity.Name);
                if (MessageBox.Show(message, message, MessageBoxButtons.YesNo) ==
                    DialogResult.Yes)
                {
                    DeleteEconomicActivity(economicActivity);
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void AddDomain()
        {
            TreeNode selectedNode = tvEconomicActivity.SelectedNode;

            //var doan = new FrmEconomicActivityName { DoaName = String.Empty, Text = "Economic activity" };
            //doan.ShowDialog();
            //if (doan.IsClosed) return;

            var doa = new EconomicActivity { Name = textBoxName.Text };
            
            try
            {
                EconomicActivity parent;

                // add economic activity (in the root)
                if (selectedNode.Tag == null)
                {
                    parent = new EconomicActivity();
                    doa.Parent = parent;
                }
                // add in the tree
                else
                {
                    parent = (EconomicActivity)selectedNode.Tag;
                    doa.Parent = parent;
                }
                doa.Id = ServicesProvider.GetInstance().GetEconomicActivityServices().AddEconomicActivity(doa);
                TreeNode node = new TreeNode(doa.Name);
                node.Tag = doa;
                selectedNode.Nodes.Add(node);

                if (parent != null)
                {
                    parent.Childrens.Add(doa);
                    selectedNode.Tag = parent;
                }
                selectedNode.Expand();
            }
            catch (Exception up)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(up)).ShowDialog();
            }

            SelectRootNode();

            tvEconomicActivity.Sort();
        }

        private void EditDomain()
        {
            try
            {
                if (buttonEdit.Text.Equals(GetString("buttonEdit")))
                {
                    selectedNode = tvEconomicActivity.SelectedNode;
                    _economicActivity = (EconomicActivity)selectedNode.Tag;

                    if (_economicActivity != null)
                    {
                        _economicActivity.Parent = (EconomicActivity) selectedNode.Parent.Tag;
                        textBoxName.Text = _economicActivity.Name;
                        IsSame = textBoxName.Text;
                        buttonExit.Enabled = false;
                        buttonAdd.Enabled = false;
                        buttonDelete.Enabled = false;
                        tvEconomicActivity.Enabled = false;

                        buttonEdit.Text = GetString("buttonSave");
                    }
                }
                else
                {
                    if (ServicesProvider.GetInstance().GetEconomicActivityServices().NodeEditable(selectedNode.Tag))
                    {
                        if (selectedNode.Level == 1) 
                            _economicActivity.Parent = new EconomicActivity(); // no parent

                        if (IsSame != textBoxName.Text)
                        if (ServicesProvider.GetInstance().GetEconomicActivityServices().ChangeDomainOfApplicationName(_economicActivity, textBoxName.Text))
                        {
                            tvEconomicActivity.BeginUpdate();
                            selectedNode.Tag = _economicActivity;
                            selectedNode.Text = textBoxName.Text;
                            tvEconomicActivity.EndUpdate();
                        }
                    }

                    buttonExit.Enabled = true;
                    buttonAdd.Enabled = true;
                    buttonDelete.Enabled = true;
                    tvEconomicActivity.Enabled = true;
                    textBoxName.Text = string.Empty;

                    buttonEdit.Text = GetString("buttonEdit");
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }

            SelectRootNode();

            tvEconomicActivity.Sort();
        }

        private void DeleteEconomicActivity(EconomicActivity economicActivity)
        {
            _economicActivity = economicActivity;
            ServicesProvider.GetInstance().GetEconomicActivityServices().DeleteEconomicActivity(_economicActivity);

            var parent = (EconomicActivity) selectedNode.Parent.Tag;
            if (parent != null) parent.RemoveChildren(_economicActivity);

            tvEconomicActivity.BeginUpdate();
            selectedNode.Remove();
            tvEconomicActivity.EndUpdate();

            SelectRootNode();

            tvEconomicActivity.Sort();
        }

    }
}