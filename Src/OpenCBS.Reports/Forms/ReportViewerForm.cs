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
