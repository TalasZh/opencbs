//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

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