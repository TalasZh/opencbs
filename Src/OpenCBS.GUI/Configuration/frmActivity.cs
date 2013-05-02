// LICENSE PLACEHOLDER

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
