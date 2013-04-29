using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Octopus.Shared;
using Octopus.CoreDomain.Export;
using Octopus.Services;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Octopus.MultiLanguageRessources;
using Octopus.ExceptionsHandler;
using Octopus.CoreDomain.Export.Files;

namespace Octopus.GUI.Export
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
