// LICENSE PLACEHOLDER

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
