using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Export;
using OpenCBS.CoreDomain.Export.Files;
using OpenCBS.MultiLanguageRessources;

namespace OpenCBS.GUI.Export
{
    public partial class CreateCustomizableFileForm : Form
    {
        public CreateCustomizableFileForm(bool pExportMode)
        {
            InitializeComponent();

            labelText.Text = MultiLanguageStrings.GetString(Ressource.CustomizableExport, pExportMode ? "CreateExportFileQuestion.Text" : "CreateImportFileQuestion.Text");
            if (pExportMode)
                _initializeExportFileList();
            else
                _initializeImportFileList();
        }

        private void _initializeExportFileList()
        {
            comboBoxFile.DisplayMember = "Display";
            comboBoxFile.ValueMember = "Value";

            comboBoxFile.DataSource = new[] { new { Display = MultiLanguageStrings.GetString(Ressource.CustomizableExport, "InstallmentFile.Text"), Value = (IFile)new InstallmentExportFile() }};
        }

        private void _initializeImportFileList()
        {
            comboBoxFile.DisplayMember = "Display";
            comboBoxFile.ValueMember = "Value";

            comboBoxFile.DataSource = new[] { new { Display = MultiLanguageStrings.GetString(Ressource.CustomizableExport, "ReimbursementFile.Text"), Value = (IFile)new ReimbursementImportFile() }};
        }

        public IFile File
        {
            get
            {
               return (IFile)comboBoxFile.SelectedValue;
            }
        }
    }
}
