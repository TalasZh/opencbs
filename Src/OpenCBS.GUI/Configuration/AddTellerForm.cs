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
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Services;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.GUI.UserControl;
using OpenCBS.Shared;

namespace OpenCBS.GUI.Configuration
{
    public partial class AddTellerForm : SweetOkCancelForm
    {
        private bool _isNew;
        private Teller _teller;
        private User _user;
        
        public Teller Teller
        {
            get
            {
                return _teller;
            }
        }

        public AddTellerForm()
        {
            InitializeComponent();
            Initialize(null);
        }

        public AddTellerForm(Teller teller)
        {
            InitializeComponent();
            Initialize(teller);
        }

        private void Initialize(Teller teller)
        {
            _isNew = teller == null;
            _teller = teller ?? new Teller();
            _user = _isNew ? null : _teller.User;
            cmbBranch.SelectedIndexChanged -= cmbBranch_SelectedIndexChanged;
            cmbCurrency.SelectedIndexChanged -= cmbCurrency_SelectedIndexChanged;
            cmbAccount.SelectedIndexChanged -= cmbAccount_SelectedIndexChanged;
            cmbUser.SelectedIndexChanged -= cmbUser_SelectedIndexChanged;
            InitializeAccount();
            InitializeCurrency();
            InitializeBranches();
            InitializeUsers();
            InitializeAmounts();
            cmbBranch.SelectedIndexChanged += cmbBranch_SelectedIndexChanged;
            cmbCurrency.SelectedIndexChanged += cmbCurrency_SelectedIndexChanged;
            cmbAccount.SelectedIndexChanged += cmbAccount_SelectedIndexChanged;
            cmbUser.SelectedIndexChanged += cmbUser_SelectedIndexChanged;
        }

        private void InitializeAmounts()
        {
            if (_teller != null)
            {
                bool b = false;
                if (cmbCurrency.Items.Count != 0)
                    b = (cmbCurrency.SelectedItem as Currency).UseCents;
                tbMinAmountTeller.Text = _teller.MinAmountTeller.GetFormatedValue(b);
                tbMaxAmountTeller.Text = _teller.MaxAmountTeller.GetFormatedValue(b);
                tbMinAmountDeposit.Text = _teller.MinAmountDeposit.GetFormatedValue(b);
                tbMaxAmountDeposit.Text = _teller.MaxAmountDeposit.GetFormatedValue(b);
                tbMinAmountWithdrawal.Text = _teller.MinAmountWithdrawal.GetFormatedValue(b);
                tbMaxAmountWithdrawal.Text = _teller.MaxAmountWithdrawal.GetFormatedValue(b);
            }
        }

        private void InitializeAccount()
        {
            int accountId = _teller.Account != null ? _teller.Account.Id : 0;
            cmbAccount.ValueMember = "Number";
            cmbAccount.DisplayMember = "";
            cmbAccount.DataSource = ServicesProvider.GetInstance().GetChartOfAccountsServices().FindAllAccountsWithoutTeller(accountId).OrderBy(item => item.Number).ToList();
            if (_teller != null && _teller.Account != null)
                cmbAccount.SelectedValue = _teller.Account.Number;
        }

        private void InitializeCurrency()
        {
            cmbCurrency.ValueMember = "Name";
            cmbCurrency.DisplayMember = "";
            cmbCurrency.DataSource =
                ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies().OrderBy(item => item.Id).ToList();
            if (_teller != null && _teller.Currency != null)
                cmbCurrency.SelectedValue = _teller.Currency.Name;
        }

        private void InitializeBranches()
        {
            cmbBranch.ValueMember = "Name";
            cmbBranch.DisplayMember = "";
            cmbBranch.DataSource =
                ServicesProvider.GetInstance().GetBranchService().FindAllNonDeleted().OrderBy(item => item.Id).ToList();
            if (_teller != null && _teller.Branch != null)
                cmbBranch.SelectedItem = _teller.Branch;
        }

        private void InitializeUsers()
        {
            cmbUser.ValueMember = "UserName";
            cmbUser.DisplayMember = "";
            List<User> list =ServicesProvider.GetInstance().GetUserServices().FindAllWithoutTellerOfBranch(
            cmbBranch.SelectedItem as Branch, _user);
            cmbUser.DataSource = list.OrderBy(item => item.Id).ToList();
            
            if (_teller != null && _teller.User != null && _teller.User.Id != 0)
                cmbUser.SelectedValue = _teller.User.UserName;
        }

        private void CheckName()
        {
            SetOkButtonEnabled(tbName.Text.Length > 0);
        }

        private void tbName_TextChanged(object sender, System.EventArgs e)
        {
            CheckName();
        }

        private static OCurrency CheckAmount(TextBox textBox, bool useOccurrency, bool allowNegativeNumber)
        {
            textBox.BackColor = Color.White;
            try
            {
                if (String.IsNullOrEmpty(textBox.Text.Trim()))
                    return null;
                if (!allowNegativeNumber && Convert.ToDecimal(textBox.Text) < 0)
                    throw new Exception();

                return Convert.ToDecimal(textBox.Text);
            }
            catch (Exception)
            {
                textBox.BackColor = Color.Red;
                return null;
            }
        }

        private void AddTellerForm_Load(object sender, System.EventArgs e)
        {
            //Debug.Assert(_teller != null, "Teller is null!");
            Text = _isNew ? GetString("add") : GetString("edit");
            if (!_isNew)
            {
                tbName.Text = _teller.Name;
                tbDesc.Text = _teller.Description;
            }
        }

        private void AddTellerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.OK != DialogResult) return;

            try
            {
                _teller.Name = tbName.Text;
                _teller.Description = tbDesc.Text;
                _teller.Branch = cmbBranch.SelectedItem as Branch;
                _teller.Account = cmbAccount.SelectedItem as Account;
                _teller.Currency = cmbCurrency.SelectedItem as Currency;
                ServicesProvider.GetInstance().GetTellerServices().ValidateTeller(_teller);
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void tbMinAmountTeller_TextChanged(object sender, EventArgs e)
        {
            _teller.MinAmountTeller = CheckAmount(tbMinAmountTeller, true, false);
        }

        private void tbMaxAmountTeller_TextChanged(object sender, EventArgs e)
        {
            _teller.MaxAmountTeller = CheckAmount(tbMaxAmountTeller, true, false);
        }

        private void tbMinAmountDeposit_TextChanged(object sender, EventArgs e)
        {
            _teller.MinAmountDeposit = CheckAmount(tbMinAmountDeposit, true, false);
        }

        private void tbMinAmountWithdrawal_TextChanged(object sender, EventArgs e)
        {
            _teller.MinAmountWithdrawal = CheckAmount(tbMinAmountWithdrawal, true, false);
        }

        private void tbMaxAmountWithdrawal_TextChanged(object sender, EventArgs e)
        {
            _teller.MaxAmountWithdrawal = CheckAmount(tbMaxAmountWithdrawal, true, false);
        }

        private void tbMaxAmountDeposit_TextChanged(object sender, EventArgs e)
        {
            _teller.MaxAmountDeposit = CheckAmount(tbMaxAmountDeposit, true, false);
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            _teller.Branch = cmbBranch.SelectedItem as Branch;
            InitializeUsers();
        }

        private void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            _teller.Currency = cmbCurrency.SelectedItem as Currency;
            InitializeAmounts();
        }

        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            _teller.User = cmbUser.SelectedItem as User;
        }

        private void cmbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            _teller.Account = cmbAccount.SelectedItem as Account;
        }

        private void chxIsVault_CheckedChanged(object sender, EventArgs e)
        {
            cmbUser.Enabled = !chxIsVault.Checked;
            if (chxIsVault.Checked)
            {
                cmbUser.DataSource = null;
                cmbUser.Items.Clear();
                _teller.User = new User();
                _teller.User.Id = 0;
                tabctrlAddTeller.TabPages.Remove(tabAdvancedTeller);
            }
            else
            {
                tabctrlAddTeller.TabPages.Add(tabAdvancedTeller);
                InitializeUsers();
            }
        }
    }
}
