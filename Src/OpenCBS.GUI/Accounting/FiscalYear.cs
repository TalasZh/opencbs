// LICENSE PLACEHOLDER

using System;
using System.Windows.Forms;
using OpenCBS.Services;
using OpenCBS.Shared;

namespace OpenCBS.GUI.Accounting
{
    public partial class FiscalYear : Form
    {
        public FiscalYear()
        {
            InitializeComponent();
        }

        private void BtnGenerateEventsClick(object sender, EventArgs e)
        {
            EditFiscalYear editFiscalYear = new EditFiscalYear(true, false, false,
                                                               new CoreDomain.Accounting.FiscalYear()
                                                                   {
                                                                       OpenDate = null,
                                                                       CloseDate = null,
                                                                       Name = "",
                                                                       Id = 0
                                                                   });
            editFiscalYear.ShowDialog();
            LoadData();
        }

        private void SweetButton3Click(object sender, EventArgs e)
        {
            CoreDomain.Accounting.FiscalYear fiscalYear = (CoreDomain.Accounting.FiscalYear) olvYears.SelectedObject;
            if (fiscalYear != null)
            {
                EditFiscalYear editFiscalYear = new EditFiscalYear(false, true, false, fiscalYear);
                editFiscalYear.ShowDialog();
                LoadData();
            }
        }

        private void SweetButton2Click(object sender, EventArgs e)
        {
            CoreDomain.Accounting.FiscalYear fiscalYear = (CoreDomain.Accounting.FiscalYear)olvYears.SelectedObject;
            if (fiscalYear != null)
            {
                EditFiscalYear editFiscalYear = new EditFiscalYear(false, false, true, fiscalYear);
                editFiscalYear.ShowDialog();
                LoadData();
            }
        }

        private void SweetButton1Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadData()
        {
            olvYears.SetObjects(ServicesProvider.GetInstance().GetChartOfAccountsServices().SelectFiscalYears());

        }

        private void FiscalYearLoad(object sender, EventArgs e)
        {
            olvColumn_EndDate.AspectToStringConverter = delegate(object value)
            {
                if (value != null && value.ToString().Length > 0)
                {
                    return (((DateTime)value).Date).ToString();
                }
                return null;
            };

            olvColumn_EndDate.AspectToStringConverter = delegate(object value)
            {
                if (value != null && value.ToString().Length > 0)
                {
                    return (((DateTime)value).Date).ToString();
                }
                return null;
            };
            LoadData();
        }
    }
}
