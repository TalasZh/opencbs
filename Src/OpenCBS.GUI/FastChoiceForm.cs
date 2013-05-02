// LICENSE PLACEHOLDER

using System;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.GUI.Report_Browser;
using OpenCBS.Reports;
using OpenCBS.Reports.Forms;
using OpenCBS.Services;
using OpenCBS.GUI.Projets;

namespace OpenCBS.GUI
{
	/// <summary>
	/// Description r�sum�e de fastChoiceForm.
	/// </summary>
    public partial class FastChoiceForm : Form
    {
        public FastChoiceForm()
		{
            InitializeComponent();
		}

		private void linkLabelSearchCreditContract_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
            SearchCreditContractForm searchCreditContractForm = SearchCreditContractForm.GetInstance(Parent.Parent);
            searchCreditContractForm.WindowState = FormWindowState.Normal;
            searchCreditContractForm.BringToFront();
            searchCreditContractForm.Show();
        }

		private void linkLabelNewPerson_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			((LotrasmicMainWindowForm)MdiParent).InitializePersonForm();	
		}

		private void linkLabelGroup_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			((LotrasmicMainWindowForm)MdiParent).InitializeGroupForm();	
		}

		private void linkLabelSearchClient_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
            SearchClientForm searchClientForm = SearchClientForm.GetInstance(Parent.Parent);
		    searchClientForm.WindowState = FormWindowState.Normal;
            searchClientForm.ViewSearchCorporate = false;
            searchClientForm.BringToFront();
            searchClientForm.Show();
		}

		private void linkLabelPrintCashIn_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
            // TODO: implement the event handler using the new report module
		}

		private void linkLabelPrintCashOut_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
            // TODO: implement the event handler
		}

		private void linkLabelRoadMap_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
            RepaymentCollectionSheetForm frm = new RepaymentCollectionSheetForm();
            if (DialogResult.OK != frm.ShowDialog()) return;

            Report report = ReportService.GetInstance().GetReportByName(frm.ShowDelinquentLoans
                                        ? "Collection_Sheet_with_Delinquent_Loans.zip"
                                        : "Collection_Sheet.zip");
            if (!report.IsLoaded) return;
            report.RemoveParams();
            report.AddParam("beginDate", frm.BeginDate);
            report.AddParam("endDate", frm.EndDate);
            report.AddParam("subordinate_id", null == frm.Subordinate ? 0 : frm.Subordinate.Id);
            report.AddParam("disbursed_in", frm.DisbursedIn==null ? 0 : frm.DisbursedIn.Id);
            report.AddParam("display_in", frm.ShowIn.Id);
            report.AddParam("user_Id", User.CurrentUser.Id);
            report.AddParam("branch_id", null == frm.Branch ? 0 : frm.Branch.Id);
		    ReportService rs = ReportService.GetInstance();
		    rs.LoadReport(report);
            ReportViewerForm frmViewer = new ReportViewerForm(report);
            frmViewer.Show();
		}

        private void FastChoiceForm_Load(object sender, EventArgs e)
        {
            //ApplySecurity();
            if (!ServicesProvider.GetInstance().GetGeneralSettings().UseProjects)
            {
                pictureBox7.Visible = false;
                linkLabelSearchProject.Visible = false;
            }
        }

        /*private void ApplySecurity()
        {
            pnlNewClient.Visible = User.CurrentUser.HasLoanOfficerRole;
        }*/

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((LotrasmicMainWindowForm)MdiParent).InitializeCorporateForm();
        }

        private void linkLabelSearchProject_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SearchProjectForm searchProjectForm = SearchProjectForm.GetInstance(Parent.Parent);
            searchProjectForm.WindowState = FormWindowState.Normal;
            searchProjectForm.BringToFront();
            searchProjectForm.Show();
        }

        private void lnklblNewVillage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((LotrasmicMainWindowForm)MdiParent).InitializeVillageForm();
        }
    }
}
