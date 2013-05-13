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
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;

namespace OpenCBS.GUI.Report_Browser
{
    public partial class RepaymentCollectionSheetForm : Form
    {
        public RepaymentCollectionSheetForm()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            LoadLoanOfficers();
            LoadBranches();
            GetCurrencies();
            DateTime now = DateTime.Now;
            dtFrom.Value = now.AddDays(-1*Convert.ToInt16(now.DayOfWeek) + 1);
            dtTo.Value = dtFrom.Value.AddDays(4);
        }

        private void GetCurrencies()
        {
            cbDisbursedIn.Items.Clear();
            cbShowIn.Items.Clear();
            List<Currency> currencies = ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies();
            cbDisbursedIn.ValueMember = "id";
            cbDisbursedIn.DisplayMember = "name";
            cbShowIn.ValueMember = "id";
            cbShowIn.DisplayMember = "name";
            cbDisbursedIn.Items.Insert(0,MultiLanguageStrings.GetString(Ressource.InternalReportsForm, "allCurrencies"));
           foreach (Currency currency in currencies)
            {
                cbDisbursedIn.Items.Add(currency);
                cbShowIn.Items.Add(currency);
            }
            cbDisbursedIn.SelectedIndex = 0;
            cbShowIn.SelectedIndex = 0;
        }

        private void LoadLoanOfficers()
        {
            cbLoanOfficer.Items.Clear();
            cbLoanOfficer.Items.Insert(0, MultiLanguageStrings.GetString(Ressource.InternalReportsForm, "all.Text"));
            foreach (User user in User.CurrentUser.Subordinates)
                cbLoanOfficer.Items.Add(user);

            cbLoanOfficer.SelectedIndex = 0;
        }

        private void LoadBranches()
        {
            cbBranch.Items.Clear();
            cbBranch.Items.Insert(0, MultiLanguageStrings.GetString(Ressource.InternalReportsForm, "all.Text"));
            foreach (Branch branch in User.CurrentUser.Branches)
                cbBranch.Items.Add(branch);

            cbBranch.SelectedIndex = 0;
        }

        public DateTime BeginDate
        {
            get { return dtFrom.Value; }
        }

        public DateTime EndDate
        {
            get { return dtTo.Value; }
        }

        public User Subordinate
        {
            get { return cbLoanOfficer.SelectedItem as User; }
        }

        public Branch Branch
        {
            get { return cbBranch.SelectedItem as Branch; }
        }

        public bool ShowDelinquentLoans
        {
            get { return chkShowDelinquentLoans.Checked; }
        }

        public Currency DisbursedIn
        {
            get { return cbDisbursedIn.SelectedItem as Currency; }
        }
        public Currency ShowIn
        {
            get { return cbShowIn.SelectedItem as Currency; }
        }
    }
}
