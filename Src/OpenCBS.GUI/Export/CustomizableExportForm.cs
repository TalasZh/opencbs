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
using OpenCBS.CoreDomain.Export.Files;
using OpenCBS.CoreDomain.Export.Fields;
using OpenCBS.ExceptionsHandler;

namespace OpenCBS.GUI.Export
{
    public partial class CustomizableExportForm : Form
    {
        private InstallmentExportFile _installmentFile;

        public CustomizableExportForm(IFile pFile)
        {
            InitializeComponent();
            _initializeDates();
            _installmentFile = (InstallmentExportFile)pFile;
            _initializeInstallmentFile();
        }

        private void _initializeDates()
        {
            dateTimePickerBeginDateInstallments.Value = new DateTime(TimeProvider.Today.Year, TimeProvider.Today.Month, 01);
            dateTimePickerEndDateInstallments.Value = new DateTime(TimeProvider.Today.Year, TimeProvider.Today.Month, DateTime.DaysInMonth(TimeProvider.Today.Year, TimeProvider.Today.Month));
        }

        private void _initializeInstallmentFile()
        {
            if (!_installmentFile.TagInstallmentAsPending)
                contextMenuStrip1.Enabled = false;
            _initializeInstallmentsHeader();
        }

        private void _displayInstallments()
        {
            listViewInstallments.ItemChecked -= listViewInstallments_ItemChecked;
            listViewInstallments.Items.Clear();
            List<Installment> listInstallment = ServicesProvider.GetInstance().GetExportServices().GetInstallmentData(dateTimePickerBeginDateInstallments.Value, dateTimePickerEndDateInstallments.Value);

            foreach (var installment in listInstallment)
            {
                ListViewItem item = new ListViewItem(installment.ContractCode);
                item.SubItems.Add(installment.ClientName);
                item.SubItems.Add(installment.InstallmentNumber.ToString());
                item.SubItems.Add(installment.InstallmentDate.ToShortDateString());
                item.SubItems.Add(installment.InstallmentAmount.ToString());
                item.Checked = true;
                item.Tag = installment;

                listViewInstallments.Items.Add(item);
            }

            labelTotalInstallments.Text = listInstallment.Count.ToString();
            listViewInstallments.ItemChecked += listViewInstallments_ItemChecked;
            _displayCheckedInstallments();
        }

        private void buttonRefreshInstallments_Click(object sender, EventArgs e)
        {
            _displayInstallments();
        }

        private void _initializeInstallmentsHeader()
        {
            listViewFormatedInstallments.Columns.Clear();
            foreach (IField field in _installmentFile.SelectedFields)
            {
                listViewFormatedInstallments.Columns.Add(field.Header);
            }
            listViewFormatedInstallments.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void _displayCheckedInstallments()
        {
            if (_installmentFile != null)
            {
                listViewFormatedInstallments.Items.Clear();
                foreach (ListViewItem checkedItem in listViewInstallments.CheckedItems)
                {
                    string[] formatedData = _installmentFile.GetFormatedRowData(checkedItem.Tag as Installment).ToArray();
                    if (formatedData.Length > 0)
                    {
                        ListViewItem item = new ListViewItem(formatedData[0]);
                        item.SubItems.AddRange(formatedData.Skip(1).ToArray());
                        item.Tag = checkedItem.Tag;
                        listViewFormatedInstallments.Items.Add(item);
                    }
                }
                if (listViewFormatedInstallments.Items.Count > 0)
                    listViewFormatedInstallments.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            labelSelectedInstallments.Text = listViewInstallments.CheckedItems.Count.ToString();
        }

        private void _selectAllInstallments()
        {
            listViewInstallments.ItemChecked -= listViewInstallments_ItemChecked;
            foreach (ListViewItem item in listViewInstallments.Items)
                item.Checked = true;
            listViewInstallments.ItemChecked += listViewInstallments_ItemChecked;
            _displayCheckedInstallments();
        }

        private void _deslectAllInstallments()
        {
            listViewInstallments.ItemChecked -= listViewInstallments_ItemChecked;
            foreach (ListViewItem item in listViewInstallments.Items)
                item.Checked = false;
            listViewInstallments.ItemChecked += listViewInstallments_ItemChecked;
            _displayCheckedInstallments();
        }

        private void listViewInstallments_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            _displayCheckedInstallments();
        }

        private void buttonExportAccountTiers_Click(object sender, EventArgs e)
        {
            _exportInstallments();
        }

        private void _exportInstallments()
        {
            if (_installmentFile != null)
            {
                try
                {
                    List<Installment> selectedInstallments = new List<Installment>();
                    foreach (ListViewItem item in listViewInstallments.CheckedItems)
                    {
                        selectedInstallments.Add(item.Tag as Installment);
                    }

                    saveFileDialogInstallments.Filter = "Export|*" + _installmentFile.Extension;

                    if (saveFileDialogInstallments.ShowDialog() == DialogResult.OK)
                    {
                        string path = saveFileDialogInstallments.FileName;
                        _installmentFile.ExportData(path, selectedInstallments);
                        MessageBox.Show(MultiLanguageStrings.GetString(Ressource.CustomizableExport, "InstallmentsExportSuccess.Text"));
                        if (_installmentFile.TagInstallmentAsPending)
                            if (MessageBox.Show(MultiLanguageStrings.GetString(Ressource.CustomizableExport, "SetInstallmentAsPending.Text"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                _setInstallmentAsPending();
                            }
                    }
                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                }
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

        private void _setInstallmentAsPending()
        {
            RunPendingInstallmentForm run = new RunPendingInstallmentForm(_getSelectedInstallments(), _installmentFile.PaymentMethod);
            if (run.ShowDialog() == DialogResult.OK)
            {
                List<Installment> failedInstallment = run.FailedInstallments;
                if (failedInstallment.Count == 0)
                {
                    MessageBox.Show(MultiLanguageStrings.GetString(Ressource.CustomizableExport, "InstallmentAsPendingSuccess.Text"));
                }
                else
                {
                    MessageBox.Show(string.Format("{0} : {1}",
                           MultiLanguageStrings.GetString(Ressource.CustomizableExport, "PendingInstallmentFailed.Text"),
                           string.Join(", ", failedInstallment.Select(item => item.ContractCode).ToArray())));
                }
                _displayInstallments();
            }
        }

        private void tagInstallmentsAsPendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _setInstallmentAsPending();
        }

        private void btnSelectAllInstallments_Click(object sender, EventArgs e)
        {
            _selectAllInstallments();
        }

        private void btnDeselectAllInstallments_Click(object sender, EventArgs e)
        {
            _deslectAllInstallments();
        }

        private void listViewFormatedInstallments_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            for (int i = 0; i < listViewInstallments.Items.Count; i++)
            {
                if (listViewInstallments.Items[i].Tag == e.Item.Tag)
                {
                    if (e.IsSelected)
                    {
                        listViewInstallments.Items[i].Selected = false;
                        listViewInstallments.Items[i].BackColor = Color.Yellow;
                        listViewInstallments.TopItem = listViewInstallments.Items[i];
                    }
                    else
                    {
                        listViewInstallments.Items[i].BackColor = Color.White;
                    }
                    break;
                }
            }
        }

        private void listViewFormatedInstallments_Leave(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewInstallments.Items.Count; i++)
            {
                if (listViewInstallments.Items[i].BackColor == Color.Yellow)
                {
                    listViewInstallments.Items[i].BackColor = Color.White;
                    break;
                }
            }
        }

        private void listViewInstallments_Leave(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewFormatedInstallments.Items.Count; i++)
            {
                if (listViewFormatedInstallments.Items[i].BackColor == Color.Yellow)
                {
                    listViewFormatedInstallments.Items[i].BackColor = Color.White;
                    break;
                }
            }
        }

        private void listViewInstallments_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item.Checked)
            {
                for (int i = 0; i < listViewFormatedInstallments.Items.Count; i++)
                {
                    if (listViewFormatedInstallments.Items[i].Tag == e.Item.Tag)
                    {
                        if (e.IsSelected)
                        {
                            listViewFormatedInstallments.Items[i].Selected = false;
                            listViewFormatedInstallments.Items[i].BackColor = Color.Yellow;
                            listViewFormatedInstallments.TopItem = listViewFormatedInstallments.Items[i];
                        }
                        else
                        {
                            listViewFormatedInstallments.Items[i].BackColor = Color.White;
                        }
                        break;
                    }
                }
            }
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
