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
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Events.Teller;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;
using OpenCBS.Shared;

namespace OpenCBS.GUI.TellerManagement
{
    public partial class FrmTellerOperation : SweetBaseForm
    {
        private OCurrency amount = 0;
        public TellerCashInEvent TellerCashInEvent { get; set; }
        public TellerCashOutEvent TellerCashOutEvent { get; set; }

        public FrmTellerOperation()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            cmbBranch.SelectedIndexChanged -= cmbBranch_SelectedIndexChanged;
            cmbTeller.SelectedIndexChanged -= cmbTeller_SelectedIndexChanged;
            InitializeBranch();
            InitializeTeller();
            cmbTeller.SelectedIndexChanged -= cmbTeller_SelectedIndexChanged;
            cmbBranch.SelectedIndexChanged += cmbBranch_SelectedIndexChanged;
        }

        private void InitializeBranch()
        {
            cmbBranch.ValueMember = "Name";
            cmbBranch.DisplayMember = "";
            cmbBranch.DataSource =
                ServicesProvider.GetInstance().GetBranchService().FindAllNonDeletedWithVault().OrderBy(item => item.Id).ToList();
        }

        private void InitializeTeller()
        {
            cmbTeller.ValueMember = "Name";
            cmbTeller.DisplayMember = "";
            List<Teller> tellers = ServicesProvider.GetInstance().GetTellerServices().FindAllNonDeletedTellersOfBranch(cmbBranch.SelectedItem as Branch);
            cmbTeller.DataSource = tellers;
        }

        private void GenerateEvents()
        {
            if (rbtnCashIn.Checked)
            {
                TellerCashInEvent = new TellerCashInEvent();
                TellerCashInEvent.Amount = amount;
                TellerCashInEvent.Date = TimeProvider.Now;
                TellerCashInEvent.TellerId = (cmbTeller.SelectedItem as Teller).Id;
                TellerCashInEvent.Description = tbxDescription.Text;
                ServicesProvider.GetInstance().GetEventProcessorServices().FireTellerEvent(TellerCashInEvent);
            }
            else if (rbtnCashOut.Checked)
            {
                TellerCashOutEvent = new TellerCashOutEvent();
                TellerCashOutEvent.Amount = amount;
                TellerCashOutEvent.Date = TimeProvider.Now;
                TellerCashOutEvent.TellerId = (cmbTeller.SelectedItem as Teller).Id;
                TellerCashOutEvent.Description = tbxDescription.Text;
                ServicesProvider.GetInstance().GetEventProcessorServices().FireTellerEvent(TellerCashOutEvent);
            }
        }

        private void cmbTeller_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeTeller();
        }

        private void tbxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = e.KeyChar;

            if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8) || (Char.IsControl(e.KeyChar) && e.KeyChar
                                                                       != ((char)Keys.V | (char)Keys.ControlKey)) ||
                (Char.IsControl(e.KeyChar) && e.KeyChar !=
                 ((char)Keys.C | (char)Keys.ControlKey)) ||
                (e.KeyChar.ToString() == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void tbxAmount_TextChanged(object sender, EventArgs e)
        {
            amount = Convert.ToDecimal(tbxAmount.Text);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateEvents();
                Close();
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
