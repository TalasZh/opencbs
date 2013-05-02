// LICENSE PLACEHOLDER

using System;
using System.Windows.Forms;
using OpenCBS.Services;
using OpenCBS.Shared.Settings;

namespace OpenCBS.GUI.Tools
{
    public partial class frmDatabaseMaintenance : Form
    {
        public frmDatabaseMaintenance()
        {
            InitializeComponent();
            timerDatabaseSize.Enabled = true;

            if (TechnicalSettings.UseOnlineMode)
                gbxShrinkDatabase.Visible = false;
        }

        private void timerDatabaseSize_Tick(object sender, EventArgs e)
        {
            lblDatabaseUsage.Text =
                ServicesProvider.GetInstance().GetDatabaseServices().GetSQLDatabaseSettings(
                    TechnicalSettings.DatabaseName).Size;
        }

        private void butShrinkDatabase_Click(object sender, EventArgs e)
        {
            ServicesProvider.GetInstance().GetDatabaseServices().ShrinkDatabase();
        }

        private void buttonRunSQL_Click(object sender, EventArgs e)
        {
            //if (User.CurrentUser.isSuperAdmin)
            //{
                frmSQLTool mySQLForm = new frmSQLTool();
                mySQLForm.ShowDialog();
            /*}
            else
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.DatabaseMaitenance, "messageRights"),
                    MultiLanguageStrings.GetString(Ressource.DatabaseMaitenance, "title"), MessageBoxButtons.OK);
            }*/
        }

        private void frmDatabaseMaintenance_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerDatabaseSize.Enabled = false;
        }

    }
}
