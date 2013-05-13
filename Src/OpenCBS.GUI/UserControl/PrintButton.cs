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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Reports;
using OpenCBS.Reports.Forms;

namespace OpenCBS.GUI.UserControl
{
    internal partial class PrintButton : Button
    {
        private ContextMenuStrip Menu;

        public PrintButton()
        {
            InitializeComponent();
            Init();
            Menu = new ContextMenuStrip();
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
            foreach (ToolStripMenuItem item in Menu.Items)
                item.Click -= PrintReportFromMenuItem;

            Menu.Items.Clear();

            ReportService rs = ReportService.GetInstance();
            foreach (Report report in rs.GetInternalReports(AttachmentPoint, Visibility))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(report.Title)
                {
                    ForeColor = Color.FromArgb(0, 81, 152),
                    Tag = report.Guid
                };
                item.Click += PrintReportFromMenuItem;
                Menu.Items.Add(item);
            }
        }

        private void PrintReportFromMenuItem(object sender, EventArgs e)
        {
            Guid guid = (Guid)((ToolStripMenuItem)sender).Tag;
            PrintReport(guid);
        }

        private void StartProgress()
        {}

        private void StopProgress()
        {}

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Menu.Show(this, 0, Height);
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
    }
}
