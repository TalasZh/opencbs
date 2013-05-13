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
using System.Windows.Forms;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Services;

namespace OpenCBS.GUI.Configuration
{
    /// <summary>
    /// Summary description for PublicHolidaysWaitingForm.
    /// </summary>
    public partial class PublicHolidaysWaitingForm : Form
    {
        public PublicHolidaysWaitingForm()
        {
            InitializeComponent();
        }

        public void UpdateInstallmentsDate()
        {
            Cursor = Cursors.WaitCursor;
            progressBar.Minimum = 1;
            progressBar.Value = 1;
            
 
            List<KeyValuePair<int,Installment>> list = ServicesProvider.GetInstance().GetContractServices().FindAllInstalments();
            progressBar.Maximum = list.Count;

            ServicesProvider.GetInstance().GetContractServices().UpdateAllInstallmentsDate(list);

            Cursor = Cursors.Default;
            Close();
        }

        public void UpdateInstallmentsDate(DateTime date, Dictionary<int, int> list)
        {
            Cursor = Cursors.WaitCursor;
            progressBar.Value = 1;
            progressBar.Minimum = 1;

            progressBar.Maximum = list.Count;

            ServicesProvider.GetInstance().GetContractServices().UpdateAllInstallmentsDate(date, list);

            Cursor = Cursors.Default;
            Close();
        }

        private void buttonUpdate_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            UpdateInstallmentsDate();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
