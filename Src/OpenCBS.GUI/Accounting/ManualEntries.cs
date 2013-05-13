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
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Services;

namespace OpenCBS.GUI.Accounting
{
    public partial class ManualEntries : Form
    {
        public ManualEntries()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            AddBooking addBooking =  new AddBooking();
            addBooking.ShowDialog();
            InititaliseEntries();
        }

        private void InititaliseEntries()
        {
            lvlMovements.Items.Clear();
            List<CoreDomain.Accounting.FiscalYear> fiscalYears = ServicesProvider.GetInstance().GetChartOfAccountsServices().SelectFiscalYears();
            List<Booking> bookings = ServicesProvider.GetInstance().GetAccountingServices().SelectMovements(true, null, fiscalYears);

            if(bookings != null)
                foreach (Booking booking in bookings)
                {
                    ListViewItem item = new ListViewItem(booking.Id.ToString());

                    item.SubItems.Add(booking.DebitAccount.ToString());
                    item.SubItems.Add(booking.CreditAccount.ToString());
                    item.SubItems.Add(booking.Amount.GetFormatedValue(booking.Currency.UseCents));
                    item.SubItems.Add(booking.Currency.Code);
                    item.SubItems.Add(booking.Description);
                    item.SubItems.Add(booking.Date.ToShortDateString());
                    item.SubItems.Add(booking.ExchangeRate.ToString());
                    item.SubItems.Add(booking.User.ToString());
                    item.SubItems.Add(booking.Branch.Code);
                    lvlMovements.Items.Add(item);
                }
        }

        private void ManualEntries_Load(object sender, EventArgs e)
        {
            InititaliseEntries();
        }
    }
}
