// LICENSE PLACEHOLDER

using System;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.UserControl;
using OpenCBS.Reports;

namespace OpenCBS.GUI.Report_Browser
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
