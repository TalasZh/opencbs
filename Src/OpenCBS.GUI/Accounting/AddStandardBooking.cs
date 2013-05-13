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
using System.Linq;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Services;

namespace OpenCBS.GUI.Accounting
{
    public partial class AddStandardBooking : Form
    {
        public AddStandardBooking()
        {
            InitializeComponent();
            InitializeAccounts();
        }

        public Booking StandardBooking
        {
            get { return GetStandardBooking(); }
            set { SetStandardBooking(value); }
        }

        private void InitializeAccounts()
        {
            List<Account> accounts =
                ServicesProvider.GetInstance().GetChartOfAccountsServices().FindAllAccounts().ToList();
            comboBoxCredit.DataSource = accounts;
            comboBoxDebit.DataSource = accounts.ToList();
        }

        private void SetStandardBooking(Booking pBooking)
        {
            textBoxName.Text = pBooking.Name;
            comboBoxCredit.SelectedItem = comboBoxCredit.Items.OfType<Account>().FirstOrDefault(item => item.Number == pBooking.CreditAccount.Number);
            comboBoxDebit.SelectedItem = comboBoxDebit.Items.OfType<Account>().FirstOrDefault(item => item.Number == pBooking.DebitAccount.Number);
        }

        private Booking GetStandardBooking()
        {
            return new Booking
            {
                Name = textBoxName.Text,
                CreditAccount = comboBoxCredit.SelectedItem as Account,
                DebitAccount = comboBoxDebit.SelectedItem as Account
            }; 
        }
    }
}
