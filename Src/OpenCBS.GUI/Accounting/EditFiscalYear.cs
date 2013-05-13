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
