using System;
using Octopus.ExceptionsHandler;
using Octopus.GUI.UserControl;
using Octopus.Reports;

namespace Octopus.GUI.Report_Browser
{
    public partial class ReportBrowserForm : SweetForm
    {
        public ReportBrowserForm()
        {
            InitializeComponent();
            rbReports.Exception += OnException;
        }

        private void ReportBrowserForm_Load(object sender, EventArgs e)
        {
            BranchParam.AllBranches = GetString("AllBranches");
            CurrencyParam.AllCurrencies = GetString("AllCurrencies");
            rbReports.LoadDocument();
        }

        private static void OnException(Exception e)
        {
            new frmShowError(CustomExceptionHandler.ShowExceptionText(e)).ShowDialog();
        }
    }
}
