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
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using OpenCBS.MultiLanguageRessources;

namespace OpenCBS.GUI
{
    public partial class frmActivity : Form
    {
        private List<EconomicActivity> _domainList;

        public frmActivity()
        {
            InitializeComponent();
            _Initialization();
        }

        private void _Initialization()
        {
            TreeNode root = new TreeNode(MultiLanguageStrings.GetString(Ressource.FrmActivity, "doa.Text"));
            _domainList = ServicesProvider.GetInstance().GetEconomicActivityServices().FindAllEconomicActivities();
            treeViewActivities.Nodes.Add(root);
            _DisplayAllChildrensNodes(root,_domainList);
            treeViewActivities.EndUpdate();
            root.Expand();
        }

        private void _DisplayAllChildrensNodes(TreeNode pNode,IEnumerable<EconomicActivity> pList)
        {
            treeViewActivities.BeginUpdate();
            pNode.Nodes.Clear();
            foreach (EconomicActivity doa in pList)
            {
                TreeNode newNode = new TreeNode(doa.Name) {Tag = doa};
                pNode.Nodes.Add(newNode);
                if (doa.HasChildrens)
                    _DisplayAllChildrensNodes(newNode, doa.Childrens);
            }
        }

        private void treeViewActivities_DoubleClick(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewActivities.SelectedNode;
            try
            {
                if (selectedNode.Nodes.Count == 0)
                {
                    if (ServicesProvider.GetInstance().GetEconomicActivityServices().NodeEditable(selectedNode.Tag))
                    {
                        _domain = (EconomicActivity) selectedNode.Tag;
                        Close();
                    }
                } else
                {
                    MessageBox.Show(MultiLanguageStrings.GetString(Ressource.FrmActivity, "selectSubitem.Text"), 
                        "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _domain = null;
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private EconomicActivity _domain;
        public EconomicActivity EconomicActivity
        {
            get { return _domain; }
        }

        private void frmActivity_Load(object sender, EventArgs e)
        {

        }
    }
}
