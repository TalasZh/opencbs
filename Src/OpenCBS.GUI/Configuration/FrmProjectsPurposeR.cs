// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.ExceptionsHandler;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;

namespace OpenCBS.GUI
{
    public partial class FrmProjectPurposesR : Form
    {
        private string _purpose;

        public FrmProjectPurposesR()
        {
            InitializeComponent();
            _DisplayProjectObjects();
        }

        public string Purpose
        {
            get { return _purpose; }
        }

        private void _DisplayProjectObjects()
        {
            listViewProjectPurposes.Items.Clear();
            List<string> purposes = ServicesProvider.GetInstance().GetProjectServices().FindAllProjectPurposes();
            foreach (string s in purposes)
            {
                ListViewItem item = new ListViewItem(s);
                listViewProjectPurposes.Items.Add(item);
            }
        }

        private void listViewProjectPurposes_DoubleClick(object sender, EventArgs e)
        {
            _purpose = listViewProjectPurposes.SelectedItems[0].Text;
            Close();
        }
    }
}
