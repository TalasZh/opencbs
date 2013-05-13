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
using OpenCBS.CoreDomain;

namespace OpenCBS.GUI
{
    public partial class FrmProjectObjectsCUD : Form
    {
        private string _name;

        public FrmProjectObjectsCUD()
        {
            InitializeComponent();
            //_InitializeSecurity();
        }

        /*private void _InitializeSecurity()
        {
            if (User.CurrentUser.isVisitor || User.CurrentUser.isCashier)
            {
                buttonAdd.Visible = false;
            }
        }*/

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmProvinces_Load(object sender, EventArgs e)
        {
            _LoadInstallmentTypes();
        }

        private void _LoadInstallmentTypes()
        {
            //listViewProjectObjects.Items.Clear();
            //List<PostingFrequencyType> list = _packageServices.FindAllInstallmentTypes();
            //foreach (PostingFrequencyType installmentType in list)
            //{
            //    ListViewItem listView = new ListViewItem(installmentType.Name);
            //    listView.Tag = installmentType;
            //    listViewProjectObjects.Items.Add(listView);
            //}
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //try
            //{
            //_packageServices.AddInstallmentType(new PostingFrequencyType(_name,_nbOfDays,_nbOfMonths));
            //_LoadInstallmentTypes();
            //}
            //catch(Exception ex)
            //{
            //    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            //}
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //if (listViewInstallmentTypes.SelectedItems.Count != 0)
            //{
            //    FundingLine line = (FundingLine) listViewInstallmentTypes.SelectedItems[0].Tag;
            //    line.Deleted = true;
            //    _contractServices.UpdateFundingLine(line);
            //    _LoadFundingLine();
            //}
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            _name = textBoxName.Text;
        }
    }
}
