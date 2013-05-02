// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Events;
using OpenCBS.ExceptionsHandler;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.Enums;

namespace OpenCBS.GUI.Contracts
{
    public partial class FastRepaymentForm : Form
    {
        private bool _blockItemCheck;
        private readonly Village _village;
        private readonly ListViewItem _itemTotal = new ListViewItem(MultiLanguageStrings.GetString(Ressource.LoanSharesForm, "TotalLabel"));
        
        public FastRepaymentForm(Village pVillage)
        {
            InitializeComponent();
            _village = pVillage;
            LoadContracts();
            
        }

        private void LoadContracts()
        {
            int loans = 0;
            lvContracts.Items.Clear();
            
            PaymentMethod paymentMethod =
                ServicesProvider.GetInstance().GetPaymentMethodServices().GetPaymentMethodById(1);
            
            foreach (VillageMember member in _village.Members)
            {
                foreach (Loan loan in member.ActiveLoans)
                {
                    if (null == loan || loan.ContractStatus != OContractStatus.Active) continue;
                    
                    var result = new KeyValuePair<Loan, RepaymentEvent>();
                    Installment installment = loan.GetFirstUnpaidInstallment();

                    if (loan.Product.LoanType == OLoanTypes.DecliningFixedPrincipalWithRealInterest)
                    {
                        OCurrency amountForToday = loan.CalculateMaximumAmountToRegradingLoan(installment.Number,
                                                                                              TimeProvider.Now.Date,
                                                                                              false,
                                                                                              0,
                                                                                              0,
                                                                                              false,
                                                                                              0,
                                                                                              true);

                        result = ServicesProvider.GetInstance().GetContractServices().ShowNewContract(loan,
                                                                                                      installment.Number,
                                                                                                      TimeProvider.Now,
                                                                                                      amountForToday,
                                                                                                      false,
                                                                                                      0,
                                                                                                      0,
                                                                                                      false,
                                                                                                      0,
                                                                                                      true,
                                                                                                      false,
                                                                                                      paymentMethod,
                                                                                                      false,
                                                                                                      false);
                    }


                    if (null == installment) continue;

                    loans++;

                    Person person = member.Tiers as Person;
                    ListViewItem item = new ListViewItem(person.Name) {Tag = loan};
                    item.SubItems.Add(loan.Code);

                    OCurrency principalHasToPay = result.Value == null
                                                      ? installment.PrincipalHasToPay
                                                      : (result.Value).Principal;

                    ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem
                                                               {
                                                                   Text = principalHasToPay.GetFormatedValue(
                                                                           loan.UseCents),
                                                                   Tag = principalHasToPay
                                                               };
                    item.SubItems.Add(subitem);

                    OCurrency interestHasToPay = result.Value == null
                                                      ? installment.InterestHasToPay
                                                      : (result.Value).Interests;

                    subitem = new ListViewItem.ListViewSubItem
                                  {
                                      Text = interestHasToPay.GetFormatedValue(loan.UseCents),
                                      Tag = interestHasToPay
                                  };
                    item.SubItems.Add(subitem);

                    subitem = new ListViewItem.ListViewSubItem();
                    OCurrency penalties = loan.CalculateDuePenaltiesForInstallment(installment.Number,
                                                                                   TimeProvider.Today);
                    subitem.Text = penalties.GetFormatedValue(loan.UseCents);
                    subitem.Tag = penalties;
                    item.SubItems.Add(subitem);

                    subitem = new ListViewItem.ListViewSubItem();
                    OCurrency total = principalHasToPay + interestHasToPay + penalties;
                    subitem.Text = total.GetFormatedValue(loan.UseCents);
                    subitem.Tag = total;
                    item.SubItems.Add(subitem);

                    subitem = new ListViewItem.ListViewSubItem();
                    OCurrency olb = ServicesProvider.GetInstance().GetGeneralSettings().IsOlbBeforeRepayment
                                        ? installment.OLB
                                        : installment.OLBAfterRepayment;
                    subitem.Text = olb.GetFormatedValue(loan.UseCents);
                    subitem.Tag = olb;
                    item.SubItems.Add(subitem);

                    OCurrency dueInterest = 0;
                    foreach (Installment installmentItem in loan.InstallmentList)
                    {
                        if (!installmentItem.IsRepaid)
                            dueInterest += installmentItem.InterestsRepayment - installmentItem.PaidInterests;
                    }

                    subitem = new ListViewItem.ListViewSubItem
                                  {
                                      Text = dueInterest.GetFormatedValue(loan.UseCents),
                                      Tag = dueInterest
                                  };
                    item.SubItems.Add(subitem);

                    subitem = new ListViewItem.ListViewSubItem
                                  {
                                      Text = loan.Product.Currency.Code,
                                      Tag = loan.Product.Currency
                                  };
                    item.SubItems.Add(subitem);

                    cbItem.SelectedIndex = 0;

                    subitem = new ListViewItem.ListViewSubItem
                                  {
                                      Text = cbItem.SelectedItem.ToString(),
                                      Tag = cbItem.SelectedItem
                                  };
                    item.SubItems.Add(subitem);

                    subitem = new ListViewItem.ListViewSubItem();
                    item.SubItems.Add(subitem);
                    
                    lvContracts.Items.Add(item);
                }
            }

            if (0 == loans)
            {
                btnOK.Enabled = false;
                return;
            }

            _itemTotal.SubItems.Add("");
            _itemTotal.SubItems.Add("0");
            _itemTotal.SubItems.Add("0");
            _itemTotal.SubItems.Add("0");
            _itemTotal.SubItems.Add("0");
            _itemTotal.SubItems.Add("");
            _itemTotal.SubItems.Add("");
            _itemTotal.SubItems.Add("");
            _itemTotal.SubItems.Add("");

            lvContracts.Items.Add(_itemTotal);
        }

        private void UpdateTotal()
        {
            ExchangeRate customExchangeRate;
            OCurrency principal = 0m;
            OCurrency interest = 0m;
            OCurrency penalty = 0m;
            OCurrency olb = 0m;
            OCurrency total = 0m;
            OCurrency totalDueInterest = 0m;
            
            foreach (ListViewItem item in lvContracts.Items)
            {
                if (!item.Checked) 
                    continue;
                
                if (item == _itemTotal) 
                    continue;

                customExchangeRate =
                        ServicesProvider.GetInstance().GetAccountingServices().FindLatestExchangeRate(
                            DateTime.Today, (Currency)item.SubItems[8].Tag);

                principal += customExchangeRate.Rate == 0
                                          ? 0
                                          : Convert.ToDecimal(item.SubItems[2].Text) / (decimal)customExchangeRate.Rate;
                interest += customExchangeRate.Rate == 0
                                          ? 0
                                          : Convert.ToDecimal(item.SubItems[3].Text) / (decimal)customExchangeRate.Rate;
                penalty += customExchangeRate.Rate == 0
                                          ? 0
                                          : Convert.ToDecimal(item.SubItems[4].Text) / (decimal)customExchangeRate.Rate;
                olb += customExchangeRate.Rate == 0
                                          ? 0
                                          : Convert.ToDecimal(item.SubItems[6].Text) / (decimal)customExchangeRate.Rate;
                totalDueInterest += customExchangeRate.Rate == 0
                                          ? 0
                                          : Convert.ToDecimal(item.SubItems[7].Text) / (decimal)customExchangeRate.Rate;
            }

            total += principal + interest + penalty;
            if (_itemTotal!=null)
            {
                bool useCents = ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().UseCents;
                _itemTotal.SubItems[2].Text = principal.GetFormatedValue(useCents);
                _itemTotal.SubItems[3].Text = interest.GetFormatedValue(useCents);
                _itemTotal.SubItems[4].Text = penalty.GetFormatedValue(useCents);
                _itemTotal.SubItems[5].Text = total.GetFormatedValue(useCents);
                _itemTotal.SubItems[6].Text = olb.GetFormatedValue(useCents);
                _itemTotal.SubItems[7].Text = totalDueInterest.GetFormatedValue(useCents);
                _itemTotal.SubItems[8].Text = ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().Code;
            }
        }

        private void lvContracts_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button)
            {
                if (e.Clicks >= 2)
                {
                    _blockItemCheck = true;
                }
            }
        }

        private void lvContracts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_blockItemCheck)
            {
                e.NewValue = e.CurrentValue;
                _blockItemCheck = !_blockItemCheck;
                return;
            }
        }

        private void lvContracts_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ListViewItem item = e.Item;
            item.Font = new Font("Arial", 9F, item.Checked ? FontStyle.Bold : FontStyle.Regular);
            foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
            {
                subitem.Font = item.Font;
            }
            
            if (item == _itemTotal)
            {
                for (int i = 0; i < e.Item.ListView.Items.Count - 1; i++)
                {
                    e.Item.ListView.Items[i].Checked = item.Checked;
                }
            }
            UpdateTotal();
        }

        private void lvContracts_SubItemClicked(object sender, UserControl.SubItemEventArgs e)
        {
            if (!e.Item.Checked) return;
            if (4 == e.SubItem && e.Item.Tag != null)
            {
                lvContracts.StartEditing(tbTotal, e.Item, e.SubItem);
            }

            if (5 == e.SubItem && e.Item.Tag != null)
            {
                lvContracts.StartEditing(tbTotal, e.Item, e.SubItem);
            } 

            if (9 == e.SubItem && e.Item.Tag != null)
            {
                lvContracts.StartEditing(cbItem, e.Item, e.SubItem);
            }

            if (10 == e.SubItem && e.Item.Tag != null)
            {
                lvContracts.StartEditing(tbTotal, e.Item, e.SubItem);
            }
        }

        private void lvContracts_SubItemEndEditing(object sender, UserControl.SubItemEndEditingEventArgs e)
        {
            if (4 == e.SubItem && e.Item.Tag != null)
            {
                e.Item.SubItems[e.SubItem].Tag = (OCurrency)decimal.Parse(e.DisplayText);
                e.Item.SubItems[e.SubItem].Text = e.DisplayText;
            }
            if (5 == e.SubItem && e.Item.Tag != null)
            {
                e.Item.SubItems[e.SubItem].Tag = (OCurrency)decimal.Parse(e.DisplayText);
                e.Item.SubItems[e.SubItem].Text = e.DisplayText;
            }

            var loan = new Loan();
            if (4 == e.SubItem || 5 == e.SubItem || 9 == e.SubItem && e.Item.Tag != null)
            {
                loan = e.Item.Tag as Loan;
            }

            if (loan != null && loan.Id != 0)
            {
                Installment i = loan.GetFirstUnpaidInstallment();
                bool disableFees = false;
                var result = new KeyValuePair<Loan, RepaymentEvent>();

                if (loan.Product.LoanType == OLoanTypes.DecliningFixedPrincipalWithRealInterest)
                {
                    OCurrency amountForToday = loan.CalculateMaximumAmountToRegradingLoan(i.Number,
                                                                                          TimeProvider.Now,
                                                                                          false,
                                                                                          0,
                                                                                          0,
                                                                                          false,
                                                                                          0,
                                                                                          true);
                    PaymentMethod paymentMethod =
                        ServicesProvider.GetInstance().GetPaymentMethodServices().GetPaymentMethodById(1);
                    result = ServicesProvider.GetInstance().GetContractServices().ShowNewContract(loan,
                                                                                                  i.Number,
                                                                                                  TimeProvider.Now.Date,
                                                                                                  amountForToday,
                                                                                                  false,
                                                                                                  0,
                                                                                                  0,
                                                                                                  false,
                                                                                                  0,
                                                                                                  true,
                                                                                                  false,
                                                                                                  paymentMethod,
                                                                                                  false,
                                                                                                  false);
                }

                OCurrency penalties = (OCurrency)e.Item.SubItems[4].Tag;
                OCurrency principal = result.Value == null ? i.PrincipalHasToPay : result.Value.Principal;
                OCurrency interest = result.Value == null ? i.InterestHasToPay : result.Value.InterestPrepayment;
                OCurrency total;

                if (penalties != loan.CalculateDuePenaltiesForInstallment(i.Number, TimeProvider.Today))
                    disableFees = true;

                total = (OCurrency)e.Item.SubItems[5].Tag;
                if (e.SubItem == 5)
                {
                    if (total < 0)
                        throw new ArithmeticException("Total cannot be negative.");
                    if (total > penalties)
                    {
                        OCurrency remainder = total - penalties;
                        if (remainder > interest)
                        {
                            remainder -= interest;
                            principal = remainder > principal ? principal : remainder;
                            remainder -= principal;
                            total = principal + interest + penalties + remainder;
                        }
                        else
                        {
                            interest = remainder;
                            principal = 0;
                        }
                    }
                    else
                    {
                        penalties = total;
                        principal = 0;
                        interest = 0;
                    }
                }

                e.Item.SubItems[2].Text = principal.GetFormatedValue(loan.UseCents);
                e.Item.SubItems[2].Tag = principal;
                e.Item.SubItems[3].Text = interest.GetFormatedValue(loan.UseCents);
                e.Item.SubItems[3].Tag = interest;
                e.Item.SubItems[4].Text = penalties.GetFormatedValue(loan.UseCents);
                e.Item.SubItems[4].Tag = penalties;
                e.Item.SubItems[5].Text = total.GetFormatedValue(loan.UseCents);
                e.Item.SubItems[5].Tag = total;

                if (e.Item.SubItems.Count > 9)
                    e.Item.SubItems[9].Tag = cbItem.SelectedItem;

                if (5 == e.SubItem)
                    e.DisplayText = total.GetFormatedValue(loan.UseCents);

                if (9 == e.SubItem)
                    e.DisplayText = cbItem.SelectedItem.ToString();
                if (10 == e.SubItem)
                    e.Item.SubItems[10].Text = e.DisplayText;
                
                
                int paymentOption = cbItem.SelectedIndex + 1;

                KeyValuePair<Loan, RepaymentEvent> keyValuePair = CalculatePrincipalAndInterest(loan,
                                                                                                total.Value, 
                                                                                                disableFees, 
                                                                                                penalties.Value, 
                                                                                                paymentOption);
                if (keyValuePair.Value != null)
                {
                    e.Item.SubItems[2].Text =
                        keyValuePair.Value.Principal.GetFormatedValue(loan.Product.UseCents);
                    e.Item.SubItems[2].Tag = keyValuePair.Value.Principal;
                    e.Item.SubItems[3].Text =
                        keyValuePair.Value.Interests.GetFormatedValue(loan.Product.UseCents);
                    e.Item.SubItems[3].Tag = keyValuePair.Value.Interests;
                    e.Item.SubItems[4].Text =
                        keyValuePair.Value.Penalties.GetFormatedValue(loan.Product.UseCents);
                    e.Item.SubItems[4].Tag = keyValuePair.Value.Penalties;
                }
            }
            UpdateTotal();
        }

        private KeyValuePair<Loan, RepaymentEvent> CalculatePrincipalAndInterest(Loan loan, decimal amount, bool disableFees, decimal manualFees, int paymentOption)
        {
            try
            {
                int installmentNumber = loan.GetFirstUnpaidInstallment().Number;
                DateTime date = TimeProvider.Today;

                PaymentMethod paymentMethod =
                    ServicesProvider.GetInstance().GetPaymentMethodServices().GetPaymentMethodById(1);

                bool keepIntialSchedule = true;
                bool payProportion = false;

                switch (paymentOption)
                {
                    case 1:
                        keepIntialSchedule = true;
                        payProportion = false;
                        break;
                    case 2:
                        keepIntialSchedule = false;
                        payProportion = false;
                        break;
                    case 3:
                        keepIntialSchedule = false;
                        payProportion = true;
                        break;
                }

                return ServicesProvider.GetInstance().GetContractServices().ShowNewContract(loan,
                                                                                            installmentNumber,
                                                                                            date,
                                                                                            amount,
                                                                                            disableFees,
                                                                                            manualFees,
                                                                                            0,
                                                                                            false,
                                                                                            0,
                                                                                            keepIntialSchedule,
                                                                                            payProportion,
                                                                                            paymentMethod,
                                                                                            false,
                                                                                            false);
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                LoadContracts();
            }

            return new KeyValuePair<Loan, RepaymentEvent>();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvContracts.Items)
            {
                if (item == _itemTotal) continue;
                if (!item.Checked) continue;
                var loan = item.Tag as Loan;
                VillageMember activeMember = null;
                int index = 0;
                foreach (VillageMember member in _village.Members)
                {
                    int tIndex = member.ActiveLoans.IndexOf(loan);
                    if (tIndex > -1)
                    {
                        activeMember = member;
                        index = tIndex;
                    }
                }

                if (activeMember != null)
                {
                    Person person = activeMember.Tiers as Person;
                    if (loan != null)
                    {
                        int number = loan.GetFirstUnpaidInstallment().Number;
                        OCurrency total = (OCurrency) item.SubItems[5].Tag;
                        bool doProportionPayment = cbItem.SelectedIndex == 2;
                        OCurrency penalties =(OCurrency)item.SubItems[4].Tag;
                        bool disableFees = penalties != loan.CalculateDuePenaltiesForInstallment(number, TimeProvider.Today);
                        string comment = item.SubItems[10].Text;

                        PaymentMethod paymentMethod = ServicesProvider.GetInstance().GetPaymentMethodServices().GetPaymentMethodById(1);

                        try
                        {
                            activeMember.ActiveLoans[index] =
                                ServicesProvider.GetInstance().GetContractServices().Repay(loan,
                                                                                           person,
                                                                                           number,
                                                                                           TimeProvider.Today, 
                                                                                           total,
                                                                                           disableFees,
                                                                                           penalties, 
                                                                                           0,
                                                                                           false,
                                                                                           0,
                                                                                           true,
                                                                                           doProportionPayment,
                                                                                           paymentMethod, 
                                                                                           comment,
                                                                                           false);
                        }
                        catch (Exception ex)
                        {
                            new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                        }
                    }
                }

                if (loan != null) if (loan.Closed) if (activeMember != null) activeMember.ActiveLoans[index] = null;
            }

            if(_AllContractsClosed())
            {
                _village.Active = false;
                ServicesProvider.GetInstance().GetContractServices().UpdateVillageStatus(_village);
            }
        }

        private bool _AllContractsClosed()
        {
            foreach (VillageMember member in _village.Members)
            {
                foreach (Loan loan in member.ActiveLoans)
                {
                    if (null == loan) continue;
                    if (!loan.Closed) return false;
                }
            }
            return true;
        }
    }
}
