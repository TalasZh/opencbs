// LICENSE PLACEHOLDER

using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace OpenCBS.Reports.Forms
{
    public partial class ReportViewerForm : Form
    {
        private readonly Report _report;

        public ReportViewerForm(Report report)
        {
            _report = report;
            InitializeComponent();
        }

        private void ReportViewerForm_Load(object sender, System.EventArgs e)
        {
            ReportDocument doc = _report.GetDocument();
            SetFormattingOptions();
            ReportService rs = ReportService.GetInstance();
            crvReport.ParameterFieldInfo = rs.GetParameterFields(_report);
            crvReport.ReportSource = doc;
            crvReport.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            Text = _report.Title;
        }

        private void SetFormattingOptions()
        {
            int decimals = _report.UseCents ? 2 : 0;
            foreach (ReportObject robj in _report.GetObjectIterator())
            {
                if (!(robj is FieldObject)) continue;
                FieldObject fobj = robj as FieldObject;
                if (null == fobj.DataSource) continue;

                if (!(fobj.DataSource.ValueType == FieldValueType.CurrencyField 
                    || fobj.DataSource.ValueType == FieldValueType.NumberField)) continue;
                if (!fobj.Name.StartsWith("cur_") && !fobj.Name.StartsWith("cur_acc_")) continue;

                FieldFormat ff = fobj.FieldFormat;
                ff.NumericFormat.CurrencySymbolFormat = CurrencySymbolFormat.NoSymbol;
                if (fobj.Name.StartsWith("cur_acc_"))
                {
                    ff.NumericFormat.NegativeFormat = NegativeFormat.Bracketed;
                }
                ff.CommonFormat.EnableUseSystemDefaults = false;
                ff.NumericFormat.DecimalPlaces = (short)decimals;
            }
        }
    }
}
