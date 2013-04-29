using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Octopus.ExceptionsHandler;
using Octopus.Reports;
using Octopus.Reports.Forms;

namespace Octopus.GUI.UserControl
{
    internal partial class PrintButton : SweetButton
    {
        public PrintButton()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            AttachmentPoint = AttachmentPoint.None;
            Visibility = Visibility.All;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        [Browsable(true), DefaultValue(AttachmentPoint.None)]
        public AttachmentPoint AttachmentPoint { get; set; }

        [Browsable(true), DefaultValue(Visibility.All)]
        public Visibility Visibility { get; set; }

        public void LoadReports()
        {
            ToolStripItemCollection items = _menu.Items;
            foreach (ToolStripMenuItem item in items)
                item.Click -= PrintReportFromMenuItem;

            items.Clear();

            ReportService rs = ReportService.GetInstance();
            foreach (Report report in rs.GetInternalReports(AttachmentPoint, Visibility))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(report.Title)
                {
                    ForeColor = Color.FromArgb(0, 0, 88, 56),
                    Tag = report.Guid
                };
                item.Click += PrintReportFromMenuItem;
                items.Add(item);
            }
        }

        private void PrintReportFromMenuItem(object sender, EventArgs e)
        {
            Guid guid = (Guid)((ToolStripMenuItem)sender).Tag;
            PrintReport(guid);
        }

        private void PrintReport(Guid guid)
        {

            ReportService rs = ReportService.GetInstance();
            Report report = rs.GetReport(guid);
            if (null == report) return;

            try
            {
                List<ReportParamV2> additionalParams
                    = report.Params
                        .Where(p => p.Additional && p.Visible)
                        .ToList();
                if (additionalParams.Count != 0)
                {
                    ReportParamsForm reportParamsForm = new ReportParamsForm(additionalParams, report.Title);
                    if (reportParamsForm.ShowDialog() != DialogResult.OK) return;
                }

                ReportInitializer(report);

                try
                {
                    StartProgress();
                    rs.LoadReport(report);
                }
                finally
                {
                    StopProgress();
                }                
            }
            catch (Exception exception)
            {
                new frmShowError(
                    CustomExceptionHandler.ShowExceptionText(exception)
                    ).ShowDialog();
                throw;
            }
            
            ReportViewerForm frm = new ReportViewerForm(report);
            frm.Show();
        }

        [Browsable(false)]
        public Action<Report> ReportInitializer { get; set; }

        [Browsable(false)]
        public override ContextMenuStrip Menu
        {
            get
            {
                return base.Menu;
            }
            set
            {
                base.Menu = value;
            }
        }

        [Browsable(false)]
        public override ButtonIcon Icon
        {
            get
            {
                return base.Icon;
            }
            set
            {
                base.Icon = value;
            }
        }
    }
}
