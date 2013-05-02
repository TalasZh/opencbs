// LICENSE PLACEHOLDER

using System;
using System.Windows.Forms;
using OpenCBS.Services;

namespace OpenCBS.GUI.Accounting
{
    public partial class EditFiscalYear : Form
    {
        private CoreDomain.Accounting.FiscalYear Get()
        {
            return new CoreDomain.Accounting.FiscalYear
                       {
                           Name = tbxName.Text,
                           OpenDate = dpkOpenDate.Enabled ? dpkOpenDate.Value.Date : _fiscalYear.OpenDate,
                           CloseDate = dpkCloseDate.Enabled ? dpkCloseDate.Value.Date : _fiscalYear.CloseDate,
                           Id = _fiscalYear.Id
                       };
        }

        private readonly CoreDomain.Accounting.FiscalYear _fiscalYear;

        public EditFiscalYear()
        {
            InitializeComponent();
        }

        public EditFiscalYear(bool isNameActive, bool isOpenDateActive, bool isCloseDateActive, CoreDomain.Accounting.FiscalYear fiscalYear)
        {
            InitializeComponent();
            tbxName.Enabled = isNameActive;
            dpkOpenDate.Enabled = isOpenDateActive;
            dpkCloseDate.Enabled = isCloseDateActive;
            _fiscalYear = fiscalYear;
            tbxName.Text = fiscalYear.Name;
            
            if (fiscalYear.OpenDate != null)
                dpkOpenDate.Value = (DateTime) fiscalYear.OpenDate;

            if (fiscalYear.CloseDate != null)
                dpkCloseDate.Value = (DateTime) fiscalYear.CloseDate;
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            CoreDomain.Accounting.FiscalYear fiscalYear = Get();
            if (fiscalYear.Id == 0)
            {
                ServicesProvider.GetInstance().GetChartOfAccountsServices().CreateFiscalYear(fiscalYear);
            }
            else
            {
                ServicesProvider.GetInstance().GetChartOfAccountsServices().UpdateFiscalYear(fiscalYear);
            }
            Close();
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
