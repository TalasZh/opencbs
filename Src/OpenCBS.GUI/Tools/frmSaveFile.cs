// LICENSE PLACEHOLDER

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OpenCBS.GUI.Tools
{
    /// <summary>
    /// This dialog is used for selecting a target location when saving a file.
    /// 
    /// </summary>
    public partial class FrmSaveFile : Form
    {
        /// <summary>
        /// If <paramref name="pDefaultPath"/> is null, the "default" button is not displayed.
        /// </summary>
        /// <param name="pTitle">Dialog title</param>
        /// <param name="pTargetPath">Target path</param>
        /// <param name="pDefaultPath">Default path (if user selects the "use default" option)</param>
        /// <param name="pFileName">File name</param>
        public FrmSaveFile(string pTitle, string pTargetPath, string pDefaultPath, string pFileName)
        {
            InitializeComponent();

            Init(pTitle, pTargetPath, pDefaultPath, pFileName);
        }

        /// <summary>
        /// See <see cref="FrmSaveFile"/>
        /// </summary>
        /// <param name="pTitle"></param>
        /// <param name="pTargetPath"></param>
        /// <param name="pFileName"></param>
        public FrmSaveFile(string pTitle, string pTargetPath, string pFileName)
        {
            InitializeComponent();
            Init(pTitle, pTargetPath, null, pFileName);
        }

        private void Init(string pTitle, string pTargetPath, string pDefaultPath, string pFileName)
        {
            _path = pTargetPath;
            _defaultPath = pDefaultPath;
            _fileName = pFileName;
            _title = pTitle;

            if ((_defaultPath == null) || (_defaultPath.Length == 0))
            {
                btnDefault.Visible = false;
            }
            else
            {
                if ((_path == null) || (_path.Length == 0)) _path = _defaultPath;              
            }
        }

        private string _path;
        private string _defaultPath;
        private string _fileName;
        private string _title;

        private void frmSaveFile_Load(object sender, EventArgs e)
        {
            lblTitle.Text = _title;
            tbPath.Text = _path;
            tbFileName.Text = _fileName;
        }

        /// <summary>
        /// Target folder (folder containing the file)
        /// </summary>
        public string TargetFolder { get { return _path;} }
        /// <summary>
        /// File name
        /// </summary>
        public string FileName { get { return _fileName; } }
        /// <summary>
        /// File full path
        /// </summary>
        public string FileFullPath { get { return Path.Combine(_path, _fileName); } }

        private void tbPath_TextChanged(object sender, EventArgs e)
        {
            timerInput.Enabled = true;
        }

        private void tbFileName_TextChanged(object sender, EventArgs e)
        {
            timerInput.Enabled = true;

        }

        private void timerInput_Tick(object sender, EventArgs e)
        {
            timerInput.Enabled = false;
            if (!Directory.Exists(tbPath.Text))
            {
                tbPath.BackColor = Color.Red;
                btnSave.Enabled = false;
            }
            else
            {
                tbPath.BackColor = Color.White;
                btnSave.Enabled = true;
            }

            lblWarning.Visible = File.Exists(Path.Combine(tbPath.Text, tbFileName.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = tbPath.Text;
            DialogResult diagr = folderBrowserDialog.ShowDialog();
            if (diagr == DialogResult.OK)
            {
                tbPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            _path = tbPath.Text;
            _fileName = tbFileName.Text;
        }
    }
}
