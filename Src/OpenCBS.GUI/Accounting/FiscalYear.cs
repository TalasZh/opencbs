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
