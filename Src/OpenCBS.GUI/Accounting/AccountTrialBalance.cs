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
using System.Drawing.Drawing2D;
using System.Linq;
using BrightIdeasSoftware;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;
using OpenCBS.GUI.UserControl;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Shared;

namespace OpenCBS.GUI.Accounting
{
    public partial class AccountTrialBalance : SweetBaseForm
    {
        private int _contractId;
        private int? _mode;

        public AccountTrialBalance()
        {
            InitializeComponent();
            cbContractCode.SelectedIndex = 0;
            Branch branch = new Branch { Id = 0, Code = GetString("all"), Name = GetString("all") };
            List<Branch> branches = new List<Branch> {branch};
            branches.AddRange(ServicesProvider.GetInstance().GetBranchService().FindAllNonDeleted());

            _InitializeComboBoxBranches(branches);
            List<Currency> currencies = ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies();
            _InitializeComboBoxCurrencies(currencies);
            IntializeTreeViewChartOfAccounts();
        }

        private void _InitializeComboBoxCurrencies(List<Currency> pCurrencies)
        {
            cbCurrencies.Items.Clear();
            Currency currency = new Currency { Id = 0, Code = "PIVOT", Name = "PIVOT" };
            cbCurrencies.Items.Add(currency);
            foreach (Currency cur in pCurrencies)
            {
                cbCurrencies.Items.Add(cur);
                if (cur.IsPivot)
                {
                    cbCurrencies.SelectedItem = cur;
                }
            }
            cbCurrencies.SelectedIndex = 0;
        }

        private void _InitializeComboBoxBranches(List<Branch> pBranches)
        {
            cbBranches.Items.Clear();
            cbBranches.ValueMember = "Id";
            cbBranches.DisplayMember = "";
            cbBranches.DataSource = pBranches;
            cbBranches.SelectedIndex = 0;
        }
               
        private void IntializeTreeViewChartOfAccounts()
        {
            //Retrive data accounts and balances
            if (cbBranches.SelectedItem != null && cbCurrencies.SelectedItem != null)
            {

                List<Account> accounts = ServicesProvider.GetInstance().GetChartOfAccountsServices().FindAllAccounts();

                foreach (Account account in accounts)
                {
                    account.Balance =
                        ServicesProvider.GetInstance().GetAccountingServices().GetAccountBalance(account.Id,
                                                                                                 ((Currency)
                                                                                                  cbCurrencies.
                                                                                                      SelectedItem).Id,
                                                                                                 _contractId, _mode, 1,
                                                                                                 ((Branch)
                                                                                                  cbBranches.
                                                                                                      SelectedItem).
                                                                                                     Id);

                    account.CurrencyCode = ((Currency) cbCurrencies.SelectedItem).Code;
                }

                List<AccountCategory> accountCategories =
                    ServicesProvider.GetInstance().GetChartOfAccountsServices().SelectAccountCategories();
                /////////////////////////////////////////////////////////////////////////////////////////

                tlvBalances.CanExpandGetter = delegate(object o)
                                                  {
                                                      Account account = (Account) o;
                                                      if (account.Id == -1)
                                                          return true;

                                                      return
                                                          accounts.FirstOrDefault(
                                                              item => item.ParentAccountId == account.Id) != null;
                                                  };

                tlvBalances.ChildrenGetter = delegate(object o)
                                                 {
                                                     Account account = (Account) o;
                                                     if (account.Id == -1)
                                                         return
                                                             accounts.Where(
                                                                 item =>
                                                                 item.AccountCategory == account.AccountCategory &&
                                                                 item.ParentAccountId == null);

                                                     return accounts.Where(item => item.ParentAccountId == account.Id);
                                                 };

                tlvBalances.RowFormatter = delegate(OLVListItem o)
                                               {
                                                   Account account = (Account) o.RowObject;
                                                   if (account.Id == -1)
                                                   {
                                                       o.ForeColor = Color.FromArgb(0, 88, 56);
                                                       o.Font = new Font("Arial", 9, FontStyle.Bold);
                                                   }
                                               };

                TreeListView.TreeRenderer renderer = tlvBalances.TreeColumnRenderer;
                renderer.LinePen = new Pen(Color.Gray, 0.5f);
                renderer.LinePen.DashStyle = DashStyle.Dot;

                List<Account> list = new List<Account>();

                foreach (AccountCategory accountCategory in accountCategories)
                {
                    string name = MultiLanguageStrings.GetString(Ressource.ChartOfAccountsForm,
                                                                 accountCategory.Name + ".Text");
                    name = name ?? accountCategory.Name;

                    Account account = new Account
                                          {
                                              Number = name,
                                              Balance =
                                                  ServicesProvider.GetInstance().GetAccountingServices().
                                                  GetAccountCategoryBalance(accountCategory.Id,
                                                                            ((Currency) cbCurrencies.SelectedItem).Id,
                                                                            _contractId, _mode),
                                              AccountCategory = (OAccountCategories) accountCategory.Id,
                                              CurrencyCode = ((Currency) cbCurrencies.SelectedItem).Code,
                                              Id = -1
                                          };

                    list.Add(account);
                }

                olvColumnLACBalance.AspectToStringConverter = delegate(object value)
                                                                  {
                                                                      if (value.ToString().Length > 0)
                                                                      {
                                                                          OCurrency amount = (OCurrency) value;
                                                                          return amount.GetFormatedValue(true);
                                                                      }
                                                                      return null;
                                                                  };

                tlvBalances.Roots = list;
                tlvBalances.ExpandAll();
            }
        }

        private void cbCurrencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntializeTreeViewChartOfAccounts();
        }

        private void cbContractCode_DropDownClosed(object sender, EventArgs e)
        {
            if (cbContractCode.SelectedIndex > 0)
            {
                cbContractCode.Items.Clear();
                cbContractCode.Items.Add(GetString("all"));
                cbContractCode.Items.Add(GetString("newCode"));
                SearchCreditContractForm searchForm = SearchCreditContractForm.GetInstance(null);
                
                searchForm.SearchContracts(null);
                
                if (searchForm.SelectedLoanContract != null)
                {
                    cbContractCode.Items.Add(searchForm.SelectedLoanContract.ContractCode);
                    cbContractCode.SelectedIndex = cbContractCode.Items.Count - 1;
                    cbContractCode.SelectedText = searchForm.SelectedLoanContract.ContractCode;
                    _contractId = searchForm.SelectedLoanContract.Id;
                    _mode = 0;
                }
                else if (searchForm.SelectedSavingContract != null)
                {
                    cbContractCode.Items.Add(searchForm.SelectedSavingContract.ContractCode);
                    cbContractCode.SelectedIndex = cbContractCode.Items.Count - 1;
                    cbContractCode.SelectedText = searchForm.SelectedSavingContract.ContractCode;
                    _contractId = searchForm.SelectedSavingContract.Id;
                    _mode = 1;
                }
                else
                {
                    _contractId = 0;
                    _mode = null;
                }
            }
            else
            {
                _contractId = 0;
                _mode = null;
            }

            IntializeTreeViewChartOfAccounts();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntializeTreeViewChartOfAccounts();
        }
    }
}
