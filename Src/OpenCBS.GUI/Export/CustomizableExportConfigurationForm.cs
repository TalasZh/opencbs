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
using OpenCBS.CoreDomain.Export;
using OpenCBS.MultiLanguageRessources;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using OpenCBS.Services;
using OpenCBS.ExceptionsHandler;
using OpenCBS.CoreDomain.Export.Files;
using OpenCBS.CoreDomain.Export.Fields;
using OpenCBS.Enums;

namespace OpenCBS.GUI.Export
{
    public partial class CustomizableExportConfigurationForm : Form
    {
        private IFile _file;
        public CustomizableExportConfigurationForm(IFile pFile)
        {
            _file = pFile;
            InitializeComponent();
            fieldListUserControl1.ExportMode = _file.IsExportFile;
            _initializeDefaultFields(pFile.DefaultList);
            SetFile();
        }

        public void _initializeDefaultFields(List<IField> pDefaultList)
        {
            pDefaultList.ForEach(item => item.DisplayName = MultiLanguageStrings.GetString(Ressource.CustomizableExport, item.Name + ".Text"));
            pDefaultList.ForEach(item => item.Header = item.DisplayName.ToString());
            if (!_file.IsExportFile)
                pDefaultList.OfType<Field>().Where(item => item.IsRequired).ToList().ForEach(item => item.DisplayName = "* " + item.DisplayName);
            else
                pDefaultList.Add(new CustomField { DisplayName = MultiLanguageStrings.GetString(Ressource.CustomizableExport, "CustomField.Text"), Length = 0 });

            fieldListUserControl1.DefaultList = pDefaultList;
        }

        private IFile GetFile()
        {
            _file.Name = textBoxName.Text;
            _file.DisplayHeader = checkBoxDisplayHeader.Checked;
            _file.SelectedFields = fieldListUserControl1.SelectedFields;
            _file.Extension = textBoxExtension.Text;
            _file.HasFieldsDelimiter = checkBoxHasDelimiter.Checked;
            _file.FieldsDelimiter = _file.HasFieldsDelimiter ?
                comboBoxFieldDelimiter.SelectedIndex == 2
                    ? (char)9
                    : comboBoxFieldDelimiter.SelectedIndex == 3
                        ? (char)32
                        : Convert.ToChar(comboBoxFieldDelimiter.Text)
                : new char();
            _file.HasFieldsSpecificLength = checkBoxSpecificLength.Checked;
            _file.HasStringEncloseChar = checkBoxEncloseChar.Checked;
            _file.EncloseChar = _file.HasStringEncloseChar ? Convert.ToChar(comboBoxEncloseChar.Text) : new char();

            if (_file is InstallmentExportFile)
            {
                var installmentExportFile = (InstallmentExportFile)_file;
                installmentExportFile.TagInstallmentAsPending = checkBoxTagAsPending.Checked;
//                if (installmentExportFile.TagInstallmentAsPending)
//                    installmentExportFile.PaymentMethod = (OPaymentMethods)Enum.Parse(typeof(OPaymentMethods), comboBoxPaymentMethods.SelectedValue.ToString());
            }

            return _file;
        }

        private void checkBoxHasDelimiter_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxFieldDelimiter.Enabled = checkBoxHasDelimiter.Checked;
        }

        private void checkBoxEncloseChar_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEncloseChar.Enabled = checkBoxEncloseChar.Checked;
        }

        private void checkBoxSpecificLength_CheckedChanged(object sender, EventArgs e)
        {
            fieldListUserControl1.UseSpecificLenght = checkBoxSpecificLength.Checked;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                GetFile();
                ServicesProvider.GetInstance().GetExportServices().ValidateFile(_file);
                if (saveExportFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(saveExportFileDialog.FileName, FileMode.Create);
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, _file);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void SetFile()
        {
            textBoxName.Text = _file.Name;
            checkBoxDisplayHeader.Checked = _file.DisplayHeader;
            fieldListUserControl1.ClearSelectedFields();
            textBoxExtension.Text = _file.Extension;
            checkBoxHasDelimiter.Checked = _file.HasFieldsDelimiter;
            comboBoxFieldDelimiter.Text = _file.HasFieldsDelimiter ?
                _file.FieldsDelimiter == (char)9
                    ? comboBoxFieldDelimiter.Items[2].ToString()
                    : _file.FieldsDelimiter == (char)32
                        ? comboBoxFieldDelimiter.Items[3].ToString()
                        : _file.FieldsDelimiter.ToString()
                : string.Empty;
            checkBoxSpecificLength.Checked = _file.HasFieldsSpecificLength;
            checkBoxEncloseChar.Checked = _file.HasStringEncloseChar;
            comboBoxEncloseChar.Text = _file.HasStringEncloseChar ? _file.EncloseChar.ToString() : string.Empty;
            fieldListUserControl1.ExportMode = _file.IsExportFile;
            fieldListUserControl1.SelectedFields = _file.SelectedFields;
            _initializeDefaultFields(_file.DefaultList);

            if (_file is InstallmentExportFile)
            {
                panelInstallmentExportFile.Visible = true;
                var installmentExportFile = (InstallmentExportFile)_file;
                checkBoxTagAsPending.Checked = installmentExportFile.TagInstallmentAsPending;
                if (installmentExportFile.TagInstallmentAsPending)
                    comboBoxPaymentMethods.SelectedValue = installmentExportFile.PaymentMethod.Name;
            }
        }

        private void checkBoxTagAsPending_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxPaymentMethods.Enabled = checkBoxTagAsPending.Checked;
        }
    }
}
