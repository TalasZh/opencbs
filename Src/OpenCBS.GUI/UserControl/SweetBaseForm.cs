// LICENSE PLACEHOLDER

using System.Windows.Forms;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;

namespace OpenCBS.GUI.UserControl
{
    using MBB = MessageBoxButtons;
    using MBI = MessageBoxIcon;

    public partial class SweetBaseForm : Form
    {
        public SweetBaseForm()
        {
            InitializeComponent();
        }

        public string GetString(string key)
        {
            return GetString(Res, key);
        }

        public string GetString(string res, string key)
        {
            return MultiLanguageStrings.GetString(res, key);
        }

        protected virtual string Res
        {
            get
            {
                return GetType().Name;
            }
        }

        #region Notifications
        public void Notify(string key)
        {
            string caption = GetString("SweetBaseForm", "notification");
            string message = GetString(key) ?? key;
            MessageBox.Show(message, caption, MBB.OK, MBI.Information);
        }

        protected void Warn(string key, params object[] args)
        {
            string caption = GetString("SweetBaseForm", "warning");
            string message = GetString(key);
            message = message == null ? key : string.Format(message, args);
            MessageBox.Show(message, caption, MBB.OK, MBI.Warning);
        }

        public bool Confirm(string key)
        {
            string caption = GetString("SweetBaseForm", "confirmation");
            string message = GetString(key) ?? key;
            return DialogResult.Yes == MessageBox.Show(message, caption, MBB.YesNo, MBI.Question);
        }

        public void Fail(string key)
        {
            string caption = GetString("SweetBaseForm", "error");
            string message = GetString(key) ?? key;
            MessageBox.Show(message, caption, MBB.OK, MBI.Error);
        }
        #endregion Notifications

        protected IServices Services { get { return ServicesProvider.GetInstance(); } }
    }
}
