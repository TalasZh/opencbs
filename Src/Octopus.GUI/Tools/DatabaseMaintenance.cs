//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Windows.Forms;
using Octopus.Services;
using Octopus.Shared.Settings;

namespace Octopus.GUI.Tools
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