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

using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.Services;
using OpenCBS.Shared.Settings;

namespace OpenCBS.GUI.Configuration
{
    public partial class ContractCodeForm : Form
    {
        private readonly User _user;
        
        public ContractCodeForm()
        {
            InitializeComponent();
            _user = new User();
            ApplicationSettings appSettings = ApplicationSettings.GetInstance(_user.Md5);
            string code = ServicesProvider.GetInstance().GetGeneralSettings().ContractCodeTemplate;
            chkBranchCode.Checked = code.IndexOf("BC") > -1;
            chkDistrict.Checked = code.IndexOf("DT") > -1;
            chkYear.Checked = code.IndexOf("YY") > -1;
            chkLoanOfficer.Checked = code.IndexOf("LO") > -1;
            chkProductCode.Checked = code.IndexOf("PC") > -1;
            chkLoanCycle.Checked = code.IndexOf("LC") > -1;
            chkProjectCycle.Checked = code.IndexOf("JC") > -1;
            chkClientId.Checked = code.IndexOf("ID") > -1;
            tbCode.Text = appSettings.ContractCodeTemplate;
        }

        public bool BranchCode
        {
            get { return chkBranchCode.Checked; }
        }

        public bool District
        {
            get { return chkDistrict.Checked; }
        }

        public bool Year
        {
            get { return chkYear.Checked; }
        }

        public bool LoanOfficer
        {
            get { return chkLoanOfficer.Checked; }
        }

        public bool ProductCode
        {
            get { return chkProductCode.Checked; }
        }

        public bool LoanCycle
        {
            get { return chkLoanCycle.Checked; }
        }

        public bool ProjectCycle
        {
            get { return chkProjectCycle.Checked; }
        }

        public bool ID
        {
            get { return chkClientId.Checked; }
        }

        public string code
        {
            get { return tbCode.Text; }
        }

        private void chkBranchCode_CheckedChanged(object sender, System.EventArgs e)
        {
            tbCode.Text = GenerateCode();
        }

        private string GenerateCode()
        {
            string code = "";
            if (chkBranchCode.Checked)
                code += "BC/";
            if (chkDistrict.Checked)
                code += "DT/";
            if (chkYear.Checked)
                code += "YY/";
            if (chkLoanOfficer.Checked)
                code += "LO/";
            if (chkProductCode.Checked)
                code += "PC/";
            if (chkLoanCycle.Checked)
                code += "LC/";
            if (chkProjectCycle.Checked)
                code += "JC/";
            if (chkClientId.Checked)
                code += "ID/";
            return code.Remove(code.Length - 1, 1);
        }

        private void chkDistrict_CheckedChanged(object sender, System.EventArgs e)
        {
            tbCode.Text = GenerateCode();
        }

        private void chkYear_CheckedChanged(object sender, System.EventArgs e)
        {
            tbCode.Text = GenerateCode();
        }

        private void chkLoanOfficer_CheckedChanged(object sender, System.EventArgs e)
        {
            tbCode.Text = GenerateCode();
        }

        private void chkProductCode_CheckedChanged(object sender, System.EventArgs e)
        {
            tbCode.Text = GenerateCode();
        }

        private void chkLoanCycle_CheckedChanged(object sender, System.EventArgs e)
        {
            tbCode.Text = GenerateCode();
        }

        private void chkProjectCycle_CheckedChanged(object sender, System.EventArgs e)
        {
            tbCode.Text = GenerateCode();
        }

        private void chkID_CheckedChanged(object sender, System.EventArgs e)
        {
            tbCode.Text = GenerateCode();
        }

        
    }
}
