// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenCBS.Reports.Forms
{
    public partial class ReportParamsForm : Form
    {
        private readonly List<ReportParamV2> _reportParams;

        public ReportParamsForm(List<ReportParamV2> reportParams, string title)
        {
            _reportParams = reportParams;
            InitializeComponent();
            Text = title;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SuspendLayout();

            var rowCount = 0;
            _reportParams.ForEach(p =>
            {
                p.InitControl();
                if (!p.Visible) return;

                tlpParams.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tlpParams.RowCount = tlpParams.RowStyles.Count;

                if (p.AddLabel)
                {
                    var l = new Label
                    {
                        Dock = DockStyle.Fill,
                        Text = p.Label,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Name = p.Name + "_label",
                        ForeColor = Color.FromArgb(0, 88, 56),
                        BackColor = Color.Transparent,
                        Font = new Font("Arial", 9F, FontStyle.Bold)
                    };
                    tlpParams.Controls.Add(l, 0, rowCount);
                }

                tlpParams.Controls.Add(p.Control, 1, rowCount);
                rowCount++;
            });
            ActiveControl = btnOK;

            ResumeLayout();
            CenterToScreen();
        }
    }
}
