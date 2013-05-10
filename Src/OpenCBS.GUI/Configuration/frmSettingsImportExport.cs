// LICENSE PLACEHOLDER

using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.Tools;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;

namespace OpenCBS.GUI.Configuration
{
    /// <summary>
    /// This form displays settings and packages to import or export.
    /// </summary>
    public partial class FrmSettingsImportExport : SweetBaseForm
    {
        /// <summary>
        /// Constructor for settins EXPORT
        /// </summary>
        public FrmSettingsImportExport()
        {
            InitializeComponent();
            _importation = false;
        }

        /// <summary>
        /// Constructor for settings IMPORT
        /// </summary>
        /// <param name="pFile"></param>
        public FrmSettingsImportExport(string pFile)
        {
            InitializeComponent();
            _importation = true;
            cbPackages.Visible = false;

            LoadSettings(pFile);
        }

        private void LoadSettings(string pFile)
        {
            Settings settings;
            XmlSerializer xml = new XmlSerializer(typeof(Settings));
            using (StreamReader file = File.OpenText(pFile))
            {
                settings = (Settings)xml.Deserialize(file);
            }
            BindTreeView(settings, true);
        }

        private bool _importation;

        private void frmSettingsImportExport_Load(object sender, EventArgs e)
        {
            if (!_importation)
            {
                LoadSettings();
            }
        }

        private void LoadSettings()
        {
            Settings s = ServicesProvider.GetInstance().GetSettingsImportExportServices().GetCurrentSettings(cbPackages.Checked);
            BindTreeView(s, false);
        }

        private void BindTreeView(Settings pSettings, bool pCompareWithCurrent)
        {
            Settings currentSettings = null;
            if (pCompareWithCurrent)
                currentSettings =
                    ServicesProvider.GetInstance().GetSettingsImportExportServices().GetCurrentSettings(true);

            tvSettings.Nodes.Clear();
            tvSettings.BeginUpdate();
            foreach (SettingGroup group in pSettings.Groups)
            {
                string groupLabel = group.Name;
                if (!pCompareWithCurrent) groupLabel += " (" + group.Settings.Length + ")";
                TreeNode groupNode = tvSettings.Nodes.Add(group.Name, groupLabel, "GRP");
                groupNode.Checked = true;
                groupNode.Tag = group;
                foreach (Setting s in group.Settings)
                {
                    TreeNode node = s.Value != null
                                        ? groupNode.Nodes.Add(s.Name, s.Name + " = " + s.Value, "SET")
                                        : groupNode.Nodes.Add(s.Name, s.Name, "PCK");
                    
                    node.Checked = true;
                    if (pCompareWithCurrent)
                    {
                        Setting foundsetting = currentSettings.MatchNames(group, s);
                        if (foundsetting != null)
                        {
                            node.ImageKey = @"BAD";
                            node.Checked = true;
                            if (foundsetting.Value != s.Value)
                            {
                                node.Text += @" ( =" + foundsetting.Value + @")";
                            }
                        }
                    }                    
                    node.Tag = s;
                }
            }
            tvSettings.EndUpdate();
        }

        private void tvSettings_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Application.DoEvents();
            if (e.Node.ImageKey == @"GRP")
            {
                tvSettings.BeginUpdate();
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
                tvSettings.EndUpdate();
            }
        }

        private Settings GetSelectedSettings()
        {
            Settings settings = new Settings();
            foreach (TreeNode gnode in tvSettings.Nodes)
            {
                if ((gnode.Checked) || (NodeChildChecked(gnode)))
                {
                    SettingGroup group = new SettingGroup(gnode.Name);
                    settings.Add(group);
                    foreach (TreeNode node in gnode.Nodes)
                    {
                        if (node.Checked)
                        {
                            Setting s = (Setting)node.Tag;
                            group.Add(s);
                        }
                    }
                }
            }
            return settings;
        }

        private static bool NodeChildChecked(TreeNode pNode)
        {
            foreach (TreeNode node in pNode.Nodes)
            {
                if (node.Checked) return true;
            }
            return false;
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (_importation)
            {
                try
                {
                    if (MessageBox.Show(GetString("Message.Text"), Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        ServicesProvider.GetInstance().GetSettingsImportExportServices().ApplySettings(
                            GetSelectedSettings());
                        MessageBox.Show(GetString("SettingsImported.Text"));
                    }
                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                }
            }
            else
            {
                FrmSaveFile frm = new FrmSaveFile("Settings export", null, "OpenCBS.Settings");
                DialogResult dresult =  frm.ShowDialog();
                if (dresult == DialogResult.OK)
                {
                    try
                    {
                        XmlSerializer xml = new XmlSerializer(typeof (Settings));
                        StreamWriter writer = new StreamWriter(frm.FileFullPath, false, Encoding.UTF8);
                        xml.Serialize(writer, GetSelectedSettings());
                        writer.Close();
                        Close();
                    }
                    catch(InvalidOperationException ex)
                    {
                        new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                    }
                }
            }
        }

        private void cbPackages_CheckedChanged(object sender, EventArgs e)
        {
            LoadSettings();
        }

    }
}
