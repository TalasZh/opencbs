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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Export;
using OpenCBS.Services;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.ExceptionsHandler;
using OpenCBS.CoreDomain.Export.Files;

namespace OpenCBS.GUI.Export
{
    public partial class CustomizableImportForm : Form
    {
        private ReimbursementImportFile _reimbursementFile;

        public CustomizableImportForm(IFile pFile)
        {
            InitializeComponent();
            _reimbursementFile = (ReimbursementImportFile)pFile;
            openDataFileDialog.Filter = "Import|*" + pFile.Extension;
        }

        private void _displayInstallments(List<Installment> installments)
        {
            listViewInstallments.Items.Clear();

            foreach (var installment in installments)
            {
                ListViewItem item = new ListViewItem(installment.ContractCode);
                item.SubItems.Add(installment.InstallmentNumber.ToString());
                item.SubItems.Add(installment.InstallmentDate.ToShortDateString());
                item.SubItems.Add(installment.InstallmentAmount.ToString());
                item.Checked = true;
                item.Tag = installment;

                listViewInstallments.Items.Add(item);
            }

            labelTotalInstallments.Text = installments.Count.ToString();
            _displayCheckedInstallments();
        }

        private void _displayCheckedInstallments()
        {
            labelSelectedInstallments.Text = listViewInstallments.CheckedItems.Count.ToString();
        }

        private void _selectAllInstallments()
        {
            foreach (ListViewItem item in listViewInstallments.Items)
                item.Checked = true;
        }

        private void _deslectAllInstallments()
        {
            foreach (ListViewItem item in listViewInstallments.Items)
                item.Checked = false;
        }

        private void listViewInstallments_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            _displayCheckedInstallments();
        }

        private void buttonExportAccountTiers_Click(object sender, EventArgs e)
        {
            try
            {
                ServicesProvider.GetInstance().GetExportServices().ImportInstallmentRepayment(_getSelectedInstallments());
                MessageBox.Show("Import Successfully");
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private List<Installment> _getSelectedInstallments()
        {
            List<Installment> selectedInstallments = new List<Installment>();
            foreach (ListViewItem item in listViewInstallments.CheckedItems)
            {
                selectedInstallments.Add(item.Tag as Installment);
            }
            return selectedInstallments;
        }

        private void btnSelectAllInstallments_Click(object sender, EventArgs e)
        {
            _selectAllInstallments();
        }

        private void btnDeselectAllInstallments_Click(object sender, EventArgs e)
        {
            _deslectAllInstallments();
        }

        private void buttonOpenData_Click(object sender, EventArgs e)
        {
            if (openDataFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _displayInstallments(_reimbursementFile.GetDataFromFile(openDataFileDialog.FileName));
                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                }
            }
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
