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
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using OpenCBS.GUI.UserControl;

namespace OpenCBS.GUI
{
    public partial class FrmInstallmentTypes : SweetBaseForm
    {
        private enum Action
        {
            Add = 0,
            Edit = 1,
            Delete = 2
        }
        
        private string _name;
        private int _nbOfDays;
        private int _nbOfMonths;

        public FrmInstallmentTypes()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmProvinces_Load(object sender, EventArgs e)
        {
            LoadInstallmentTypes();
        }

        private void LoadInstallmentTypes()
        {
            listViewInstallmentTypes.Items.Clear();
            List<InstallmentType> list = ServicesProvider.GetInstance().GetProductServices().FindAllInstallmentTypes();

            foreach (InstallmentType installmentType in list)
            {
                ListViewItem listView = new ListViewItem(installmentType.Name);
                listView.SubItems.Add(installmentType.NbOfMonths.ToString());
                listView.SubItems.Add(installmentType.NbOfDays.ToString());
                listView.Tag = installmentType;
                listViewInstallmentTypes.Items.Add(listView);
            }
        }

        private void ModifyInstallmentTypes(Action action, InstallmentType installmentType)
        {
            try
            {
                switch (action)
                {
                    case Action.Add:
                        ServicesProvider.GetInstance().GetProductServices().AddInstallmentType(installmentType);
                        break;
                    case Action.Edit:
                        ServicesProvider.GetInstance().GetProductServices().EditInstallmentType(installmentType);
                        break;
                    case Action.Delete:
                        ServicesProvider.GetInstance().GetProductServices().DeleteInstallmentType(installmentType);
                        break;
                }  
                LoadInstallmentTypes();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length > 0 && (numericUpDownMonths.Value > 0 || numericUpDownDays.Value > 0))
            {
                InstallmentType type = new InstallmentType(_name, _nbOfDays, _nbOfMonths);
                ModifyInstallmentTypes(Action.Add, type);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewInstallmentTypes.SelectedItems.Count > 0)
            {
                if (buttonEdit.Text.Equals(GetString("buttonEdit")))
                {
                    InstallmentType type = (InstallmentType)listViewInstallmentTypes.SelectedItems[0].Tag;
                    textBoxName.Text = type.Name;
                    numericUpDownMonths.Value = type.NbOfMonths;
                    numericUpDownDays.Value = type.NbOfDays;

                    listViewInstallmentTypes.Enabled = false;
                    buttonExit.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonAdd.Enabled = false;
                    buttonEdit.Text = GetString("buttonSave");
                }
                else
                {
                    listViewInstallmentTypes.Enabled = true;
                    buttonExit.Enabled = true;
                    buttonDelete.Enabled = true;
                    buttonAdd.Enabled = true;

                    InstallmentType type = new InstallmentType(((InstallmentType)listViewInstallmentTypes.SelectedItems[0].Tag).Id, _name, _nbOfDays, _nbOfMonths);
                    ModifyInstallmentTypes(Action.Edit, type);

                    textBoxName.Text = numericUpDownMonths.Text = numericUpDownDays.Text = string.Empty;

                    buttonEdit.Text = GetString("buttonEdit");
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewInstallmentTypes.SelectedItems.Count > 0)
            {
                DialogResult res = MessageBox.Show(GetString("ConfirmDelete"), GetString("Confirm"),
                                                   MessageBoxButtons.YesNo);
                if (res != DialogResult.Yes) return;

                InstallmentType type = (InstallmentType) listViewInstallmentTypes.SelectedItems[0].Tag;
                ModifyInstallmentTypes(Action.Delete, type);
            }
        }

        private void numericUpDownMonths_ValueChanged(object sender, EventArgs e)
        {
            _nbOfMonths = (int) numericUpDownMonths.Value;
        }

        private void numericUpDownDays_ValueChanged(object sender, EventArgs e)
        {
            _nbOfDays = (int) numericUpDownDays.Value;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            _name = textBoxName.Text;
        }
    }
}
