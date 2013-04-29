//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright � 2006,2007 OCTO Technology & OXUS Development Network
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
using Octopus.CoreDomain;
using Octopus.ExceptionsHandler;
using Octopus.MultiLanguageRessources;
using Octopus.Services;

namespace Octopus.GUI
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