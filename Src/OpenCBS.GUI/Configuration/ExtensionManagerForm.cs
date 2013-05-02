using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using OpenCBS.Extensions;
using OpenCBS.GUI.UserControl;
using OpenCBS.Shared;

namespace OpenCBS.GUI.Configuration
{
    public partial class ExtensionManagerForm : SweetForm
    {
        public ExtensionManagerForm()
        {
            InitializeComponent();
            olvcFileSize.AspectToStringConverter = value =>
            {
                long fileSize = (long) value;
                return string.Format(new FileSizeFormatProvider(), "{0:fs}", fileSize);
            };
        }

        protected override void OnLoad(EventArgs eventArgs)
        {
            base.OnLoad(eventArgs);
            LoadExtensions();
        }

        private string[] _deletedExtensions = new string[] {};

        private void LoadExtensions()
        {
            _deletedExtensions = Extension.GetDeletedExtensions();

            olvExtensions.ClearObjects();
            Extension.Instance.Clear();

            Extension.Instance.LoadExtensions();
            ExtensionInfo[] extensionInfos = Extension.Instance.Extensions.Select(e => new ExtensionInfo(e)).ToArray();
            olvExtensions.SetObjects(extensionInfos);            
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            if (ofdExtension.ShowDialog(this) != DialogResult.OK) return;
            
            try
            {
                string[] selectedFiles = ofdExtension.FileNames;
                bool exists = selectedFiles.Any(Extension.Exists);
                if(!exists || Confirm("ExtensionExistReplace.Text"))
                    Array.ForEach(selectedFiles, Extension.AddExtension);
                LoadExtensions();
            }
            catch (Exception error)
            {
                Fail(error.Message);
            }
        }

        private void BtnDeleteClick(object sender, EventArgs e)
        {
            object selectedObject = olvExtensions.SelectedObject;
            if(selectedObject == null)
            {
                Notify("EmptyExtensionSelection.Text");
                return;
            }

            ExtensionInfo extensionInfo = (ExtensionInfo) selectedObject;
            Extension.DeleteExtension(extensionInfo.FilePath);

            LoadExtensions();
        }

        private void OlvExtensionsFormatRow(object sender, FormatRowEventArgs eventArgs)
        {
            ExtensionInfo extensionInfo = (ExtensionInfo)eventArgs.Model;
            
            string extensionPath = extensionInfo.FilePath;
            if (_deletedExtensions.Contains(extensionPath)) eventArgs.Item.ForeColor = Color.Red;
        }
    }
}
