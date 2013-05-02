// LICENSE PLACEHOLDER

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
