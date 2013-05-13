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

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Shared;

namespace OpenCBS.GUI.Accounting
{
    public partial class TrialBalancePreview : Form
    {
        public TrialBalancePreview()
        {
            InitializeComponent();
            IntializeTreeViewChartOfAccounts(null);
        }

        public TrialBalancePreview(List<Booking> bookings)
        {
            InitializeComponent();
            IntializeTreeViewChartOfAccounts(bookings);
        }

        private void IntializeTreeViewChartOfAccounts(List<Booking> bookings)
        {
            Currency currency = ServicesProvider.GetInstance().GetCurrencyServices().GetPivot();
            List<Account> accounts =
                ServicesProvider.GetInstance().GetAccountingServices().GetTrialBalance(TimeProvider.Now,
                                                                                       TimeProvider.Now, currency.Id, currency.Id);
            if (accounts != null)
            {
                foreach (Account account in accounts)
                {
                    OCurrency debit =
                        bookings.Sum(item => item.DebitAccount.Number == account.Number ? item.Amount.Value : 0);
                    OCurrency credit =
                        bookings.Sum(item => item.CreditAccount.Number == account.Number ? item.Amount.Value : 0);
                    account.CloseBalance = account.DebitPlus
                                               ? account.OpenBalance + debit - credit
                                               : account.OpenBalance + credit - debit;
                    account.CurrencyCode = currency.Code;
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
                renderer.LinePen = new Pen(Color.Gray, 0.5f) {DashStyle = DashStyle.Dot};

                List<Account> list = new List<Account>();

                foreach (AccountCategory accountCategory in accountCategories)
                {
                    string name = MultiLanguageStrings.GetString(Ressource.ChartOfAccountsForm,
                                                                 accountCategory.Name + ".Text");
                    name = name ?? accountCategory.Name;

                    Account account = new Account
                                          {
                                              Number = name,
                                              Balance = 0,
                                              AccountCategory = (OAccountCategories) accountCategory.Id,
                                              CurrencyCode = "",
                                              Id = -1
                                          };

                    list.Add(account);
                }

                olvColumn_CloseBalance.AspectToStringConverter = delegate(object value)
                                                                     {
                                                                         if (value.ToString().Length > 0)
                                                                         {
                                                                             OCurrency amount = (OCurrency) value;
                                                                             return amount.GetFormatedValue(true);
                                                                         }
                                                                         return null;
                                                                     };
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
    }
}
