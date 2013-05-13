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
using OpenCBS.Shared;
using OpenCBS.Services;
using OpenCBS.Enums;
using System.Linq;
using System.Drawing;
using OpenCBS.GUI.Tools;

namespace OpenCBS.GUI.Accounting
{
    public partial class AccountView : Form
    {
        private DateTime _filterBeginDate;
        private DateTime _filterEndDate;
        private Account _filterAccount;
        private OBookingTypes _bookingType = OBookingTypes.All;
        private BookingToViewStock _bookingsStock;
        private Currency _pivotCurrency;
        
        public AccountView()
        {
            InitializeComponent();
        }

        private void _InitializeComboBoxCurrencies()
        {
            List<Currency> currencies = ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies();
            if (currencies.Count < 2)
            {
                lblCurrency.Visible = false;
                cmbCurrencies.Visible = false;
                
                lvBooking.Columns.Remove(columnHeaderAmountEC);
                lvBooking.Columns.Remove(columnHeaderRate);
                _pivotCurrency = currencies[0];
                columnHeaderCredit.Text = MultiLanguageStrings.GetString(Ressource.AccountView, "CreditColumn.Text") + @" " + _pivotCurrency.Name;
                columnHeaderDebit.Text = MultiLanguageStrings.GetString(Ressource.AccountView, "DebitColumn.Text") + @" " + _pivotCurrency.Name;
                _InitializeComboBoxCurrencies(currencies);
            }
            else
            {
                _InitializeComboBoxCurrencies(currencies);
                columnHeaderDebit.Text = MultiLanguageStrings.GetString(Ressource.AccountView, "DebitColumn.Text") + @" " + ((Currency) cmbCurrencies.SelectedItem).Name;
                columnHeaderAmountEC.Text = MultiLanguageStrings.GetString(Ressource.AccountView, "AmountColumn.Text") + @" " + _pivotCurrency.Name;
            }
        }

        private void _InitializeComboBoxBranches()
        {
            List<Branch> branches = ServicesProvider.GetInstance().GetBranchService().FindAllNonDeleted();
            cmbBranches.Items.Clear();
            cmbBranches.ValueMember = "Id";
            cmbBranches.DisplayMember = "";
            cmbBranches.DataSource = branches;
        }

        private void _InitializeComboBoxCurrencies(List<Currency> pCurrencies)
        {
            cmbCurrencies.Items.Clear();
            Currency currency = new Currency { Id = 0, Code = "PIVOT", Name = "PIVOT" };
            cmbCurrencies.Items.Add(currency);
            foreach (Currency cur in pCurrencies)
            {
                cmbCurrencies.Items.Add(cur);
                if (cur.IsPivot)
                {
                    _pivotCurrency = cur;
                    cmbCurrencies.SelectedItem = cur;
                }
            }
            cmbCurrencies.SelectedIndex = 0;
        }

        private void _InitializeComboBoxAccounts()
        {
            Account selectedAccount = cbAccounts.SelectedItem as Account;
            cbAccounts.Items.Clear();
            List<Account> accounts =
                ServicesProvider.GetInstance().GetChartOfAccountsServices().FindAllAccounts();
            
            foreach (Account account in accounts.OrderBy(item => item.Number))
            {
                cbAccounts.Items.Add(account);
                if(selectedAccount!=null)
                {
                    if(account.Number.Equals(selectedAccount.Number))
                    {
                        cbAccounts.SelectedItem = account;
                    }
                }
            }
        }

        private void _InitializeDates()
        {
            dateTimePickerBeginDate.Value = TimeProvider.Today;
            dateTimePickerEndDate.Value = TimeProvider.Today.AddMonths(1);
            _filterBeginDate = TimeProvider.Today;
            _filterEndDate = TimeProvider.Today.AddMonths(1);
        }

        private void _InitializeDisplayType()
        {
            cmbDisplayType.DataBind(typeof(OBookingTypes), Ressource.AccountView, false);
        }

        private void AccountView_Load(object sender, EventArgs e)
        {            
            _InitializeComboBoxCurrencies();
            _InitializeComboBoxBranches();
            _InitializeComboBoxAccounts();
            _InitializeDates();
            _InitializeDisplayType();
        }

        private void dateTimePickerBeginDate_ValueChanged(object sender, EventArgs e)
        {
            _filterBeginDate = dateTimePickerBeginDate.Value;
            dateTimePickerEndDate.MinDate = _filterBeginDate;
            _InitializeListViewBookings(_filterAccount, _filterBeginDate, _filterEndDate, _bookingType);
        }

        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            _filterEndDate = dateTimePickerEndDate.Value;
            _InitializeListViewBookings(_filterAccount, _filterBeginDate, _filterEndDate, _bookingType);
        }

        private void comboBoxSelectAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lvBooking.Items.Clear();
            if (cbAccounts.SelectedItem != null)
            {
                _filterAccount = ((Account) cbAccounts.SelectedItem);
                if (cmbCurrencies.SelectedItem == null) 
                    _filterAccount.CurrencyId = _pivotCurrency.Id;
                else 
                    _filterAccount.CurrencyId = ((Currency) cmbCurrencies.SelectedItem).Id;
                _InitializeListViewBookings(_filterAccount, _filterBeginDate, _filterEndDate, _bookingType);
            }
            else
                _filterAccount = null;
        }

        private void _InitializeListViewBookings(Account pAccount, DateTime pBeginDate, DateTime pEndDate, OBookingTypes pBookingType)
        {
            lvBooking.Items.Clear();
            lblAccountBalance.Text = String.Empty;
            if (pAccount != null)
            {
                _bookingsStock = ServicesProvider.GetInstance().GetAccountingServices().FindAllBookings(pAccount, pBeginDate, pEndDate, ((Currency) (cmbCurrencies.SelectedItem)).Id , pBookingType, ((Branch) cmbBranches.SelectedItem).Id);
                if (_bookingsStock.Count != 0)
                {
                    foreach (BookingToView bookingToView in _bookingsStock)
                    {
                        ListViewItem listViewItem = new ListViewItem(bookingToView.Date.ToShortDateString());
                        if (bookingToView.Direction == OBookingDirections.Debit)
                        {
                            listViewItem.SubItems.Add(bookingToView.AmountInternal.GetFormatedValue(true));
                            listViewItem.SubItems.Add("");
                        }
                        else
                        {
                            listViewItem.SubItems.Add("");
                            listViewItem.SubItems.Add(bookingToView.AmountInternal.GetFormatedValue(true));
                        }

                        if (cmbCurrencies.Items.Count > 0)
                        {
                            listViewItem.SubItems.Add(bookingToView.ExchangeRate.ToString());
                            if (!bookingToView.ExchangeRate.HasValue)
                                listViewItem.BackColor = Color.Red;
                            listViewItem.SubItems.Add(bookingToView.ExternalAmount.GetFormatedValue(true));
                        }

                        if (bookingToView.IsExported)
                            listViewItem.ForeColor = Color.Gray;
                            
                        string purpose = string.Format("{0} {1} {2}", 
                            MultiLanguageStrings.GetString(Ressource.AccountView, bookingToView.EventCode + @".Text"), 
                            bookingToView.ContractCode, 
                            MultiLanguageStrings.GetString(Ressource.AccountView, bookingToView.AccountingLabel + @".Text"));

                        listViewItem.SubItems.Add(purpose);
                        lvBooking.Items.Add(listViewItem);
                    }
                }

                if (((Currency) cmbCurrencies.SelectedItem).Id != 0)
                {
                    Currency selectedcur = cmbCurrencies.SelectedItem as Currency;
                    OCurrency balance = ServicesProvider.GetInstance().GetAccountingServices().
                        GetAccountBalance(pAccount.Id, selectedcur.Id, 0, null, 0, (cmbBranches.SelectedItem as Branch).Id);
                    lblAccountBalance.Text = string.Format("{0}  {1}", balance.GetFormatedValue(selectedcur.UseCents), selectedcur);
                    
                }
                else
                {
                    Currency selectedcur = new Currency();
                    foreach (var item in cmbCurrencies.Items)
                    {
                        if(((Currency) item).IsPivot)
                        {
                            selectedcur = (Currency) item;
                        }
                    }
                    OCurrency balance = ServicesProvider.GetInstance().GetAccountingServices().
                        GetAccountBalance(pAccount.Id, 0, 0, null, 0, (cmbBranches.SelectedItem as Branch).Id);
                    lblAccountBalance.Text = string.Format("{0} {1}", balance.GetFormatedValue(selectedcur.UseCents), selectedcur.Name);
                }
            }
            else _bookingsStock = null;
        }

        private void buttonRefrech_Click(object sender, EventArgs e)
        {
            _InitializeListViewBookings(_filterAccount, _filterBeginDate, _filterEndDate, _bookingType);
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            _InitializeComboBoxAccounts();
            columnHeaderCredit.Text = MultiLanguageStrings.GetString(Ressource.AccountView, @"CreditColumn.Text") + @" " + ((Currency) cmbCurrencies.SelectedItem).Name;
            columnHeaderDebit.Text = MultiLanguageStrings.GetString(Ressource.AccountView, @"DebitColumn.Text") + @" " + (cmbCurrencies.SelectedItem as Currency).Name;
            if(columnHeaderAmountEC!=null) columnHeaderAmountEC.Text = MultiLanguageStrings.GetString(Ressource.AccountView, "AmountColumn.Text") + @" " + _pivotCurrency.Name;

            if (cbAccounts.SelectedItem != null)
            {
                _filterAccount = ((Account) cbAccounts.SelectedItem);
                _InitializeListViewBookings(_filterAccount, _filterBeginDate, _filterEndDate, _bookingType);
            }
            else
                _filterAccount = null;
            
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void cmbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            _InitializeListViewBookings(_filterAccount, _filterBeginDate, _filterEndDate, _bookingType);
        }

        private void cmbDisplayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _bookingType = (OBookingTypes)Enum.Parse(typeof(OBookingTypes), cmbDisplayType.SelectedValue.ToString());
            _InitializeListViewBookings(_filterAccount, _filterBeginDate, _filterEndDate, _bookingType);
        }
        
    }
}
