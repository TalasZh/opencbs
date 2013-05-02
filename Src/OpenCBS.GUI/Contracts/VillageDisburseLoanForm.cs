//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.UserControl;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.GUI.Contracts
{
    using OpenCBS.ExceptionsHandler.Exceptions.SavingExceptions;

    public partial class VillageDisburseLoanForm : SweetBaseForm
    {
        private readonly Village _village;
        private DateTime _disbursementDate = TimeProvider.Today;
        private DateTime _repaymentDate;
        private bool _blockItemCheck;
        readonly FundingLineServices _fLServices;
        private decimal _accumulatedAmount;
        private bool _notEnoughMoney;
        private ListViewItem _itemTotal = new ListViewItem("");
        private bool _hasMember;
        //private int _paymentMethodId;

        private const int IdxDDate = 2;
        private const int IdxAmount = 4;
        private const int IdxCurrency = 5;
        private const int IdxDflRemainder = 11;
        private const int IdxPaymentMethod = 12;

        public VillageDisburseLoanForm(Village village)
        {
            _village = village;
            _accumulatedAmount = 0;
            _notEnoughMoney = false;
            _fLServices = new FundingLineServices(User.CurrentUser);
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            lvMembers.Items.Clear();
            _fLServices.EmptyTemporaryFLAmountsStorage();
            ApplicationSettings dataParam = ApplicationSettings.GetInstance(string.Empty);
            int decimalPlaces = dataParam.InterestRateDecimalPlaces;
            foreach (VillageMember member in _village.NonDisbursedMembers)
            {
                foreach (Loan loan in member.ActiveLoans)
                {
                    if (loan.ContractStatus == OContractStatus.Active ||
                        loan.ContractStatus == OContractStatus.Refused ||
                        loan.ContractStatus == OContractStatus.Abandoned
                        ) continue;
                    _hasMember = true;
                    Person person = member.Tiers as Person;
                    ListViewItem item = new ListViewItem(person.Name) {Tag = loan};
                    item.UseItemStyleForSubItems = true;
                    item.SubItems.Add(person.IdentificationData);
                    ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem
                                                               {
                                                                   Text = TimeProvider.Today.ToShortDateString(),
                                                                   Tag = TimeProvider.Today
                                                               };
                    item.SubItems.Add(subitem);
                    item.SubItems.Add(loan.FirstInstallmentDate.ToShortDateString());
                    item.SubItems.Add(loan.Amount.GetFormatedValue(loan.UseCents));
                    item.SubItems.Add(loan.Product.Currency.Code);
                    item.SubItems.Add(Math.Round(loan.InterestRate*100, decimalPlaces).ToString());
                    item.SubItems.Add(loan.GracePeriod.ToString());
                    item.SubItems.Add(loan.NbOfInstallments.ToString());
                    item.SubItems.Add(loan.LoanOfficer.Name);
                    item.SubItems.Add(loan.FundingLine.Name);

                    _accumulatedAmount = _fLServices.CheckIfAmountIsEnough(loan.FundingLine, loan.Amount.Value);
                    if (_accumulatedAmount <= 0) item.BackColor = Color.Red;
                    item.SubItems.Add(_accumulatedAmount.ToString());

                    cbPaymentMethods.Items.Clear();
                    List<PaymentMethod> methods = ServicesProvider.GetInstance().GetPaymentMethodServices().GetAllPaymentMethods();
                    item.SubItems.Add(methods[0].Name);

                    lvMembers.Items.Add(item);
                    item.SubItems[IdxAmount].Tag = loan.Amount.GetFormatedValue(loan.UseCents);
                    item.SubItems[IdxCurrency].Tag = loan.Product.Currency;
                }
            }

            if (_hasMember)
            {
                OCurrency zero = 0m;
                _itemTotal.Text = GetString("total");
                _itemTotal.SubItems.Add("");
                _itemTotal.SubItems.Add("");
                _itemTotal.SubItems.Add("");
                _itemTotal.SubItems.Add(zero.GetFormatedValue(true));
                _itemTotal.SubItems.Add("");
                lvMembers.Items.Add(_itemTotal);
            }

            lvMembers.SubItemClicked += lvMembers_SubItemClicked;
            lvMembers.DoubleClickActivation = true;
        }

        private void UpdateTotal()
        {
            OCurrency total = 0m;
            ExchangeRate customExchangeRate;

            foreach (ListViewItem item in lvMembers.Items)
            {
                if (!item.Checked) continue;
                if (item == _itemTotal) continue;
                item.UseItemStyleForSubItems = false;

                customExchangeRate = ServicesProvider.GetInstance().GetAccountingServices().
                    FindLatestExchangeRate(DateTime.Today, (Currency)item.SubItems[IdxCurrency].Tag);
                
                total += customExchangeRate.Rate == 0 ? 0 : (OCurrency)Convert.ToDecimal(item.SubItems[IdxAmount].Tag) / customExchangeRate.Rate;
            }
                _itemTotal.SubItems[IdxAmount].Text = total.GetFormatedValue(ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().UseCents);
                _itemTotal.SubItems[IdxCurrency].Text = ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().Code;
        }

        private void lvMembers_SubItemClicked(object sender, SubItemEventArgs e)
        {
            if (!e.Item.Checked || e.Item == _itemTotal) return;
            if (e.SubItem.Equals(3)) lvMembers.StartEditing(dtpRepayment, e.Item, e.SubItem);
            if (e.SubItem == IdxDDate) lvMembers.StartEditing(dtDisbursement, e.Item, e.SubItem);
            
            if (e.SubItem == IdxPaymentMethod)
            {
                cbPaymentMethods.Items.Clear();
                List<PaymentMethod> methods = ServicesProvider.GetInstance().GetPaymentMethodServices().GetAllPaymentMethods();

                foreach (PaymentMethod method in methods)
                    cbPaymentMethods.Items.Add(method);
                
                lvMembers.StartEditing(cbPaymentMethods, e.Item, e.SubItem);
            }
        }

        private void lvMembers_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;
            if (e.Clicks >= 2) _blockItemCheck = true;
        }

        private void lvMembers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!_blockItemCheck) return;
            e.NewValue = e.CurrentValue;
            _blockItemCheck = !_blockItemCheck;
        }

        private void lvMembers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ListViewItem item = e.Item;
            item.Font = new Font("Arial", 9F, item.Checked ? FontStyle.Bold : FontStyle.Regular);
            foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
            {
                subitem.Font = item.Font;
                if (item.Checked && item != _itemTotal)
                if (item.SubItems[IdxDflRemainder] != null)
                {
                    if (Convert.ToDecimal(item.SubItems[IdxDflRemainder].Text) < 0)
                        _notEnoughMoney = true;
                }

                /*if (string.IsNullOrEmpty(item.SubItems[IdxPaymentMethod].Text))
                {
                    cbPaymentMethods.Items.Clear();
                    List<PaymentMethod> methods = ServicesProvider.GetInstance().GetPaymentMethodServices().GetAllPaymentMethods();

                    if (methods.Count > 0)
                    {
                        foreach (PaymentMethod method in methods)
                            cbPaymentMethods.Items.Add(method);

                        item.SubItems[IdxPaymentMethod].Text = methods[0].Name;
                        item.SubItems[IdxPaymentMethod].Tag = methods[0];
                    }
                }*/

            }
            if (item == _itemTotal)
            {
                foreach (ListViewItem i in item.ListView.Items)
                {
                    i.Checked = item.Checked;
                }
            }
            UpdateTotal();
            btnSave.Enabled = CanEnableConfirmButton();
        }

        private bool CanEnableConfirmButton()
        {
            foreach (ListViewItem item in lvMembers.Items)
                if (item.Checked && item != _itemTotal) return true;
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            
            try
            {
                if (_notEnoughMoney)
                {
                    if (!MessageBox.Show(MultiLanguageStrings.GetString(Ressource.VillageForm, "MoneyNotEnoughForAll.Text"), "!", 
                        MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
                        return;
                }
                foreach (ListViewItem item in lvMembers.Items)
                {
                    if (!item.Checked || item == _itemTotal) continue;
                    var loan = item.Tag as Loan;
                    DateTime date = Convert.ToDateTime(item.SubItems[IdxDDate].Text);

                    VillageMember activeMember = null;
                    
                    int index = 0;
                    foreach (VillageMember member in _village.NonDisbursedMembers)
                    {
                        int tIndex = member.ActiveLoans.IndexOf(loan);
                        if (tIndex > -1)
                        {
                            activeMember = member;
                            index = tIndex;
                        }
                    }

                    if (loan != null)
                    {
                        loan.CompulsorySavings = ServicesProvider.GetInstance().GetSavingServices().GetSavingForLoan(loan.Id, true);
                        if (loan.Product.UseCompulsorySavings)
                        {
                            if (loan.CompulsorySavings == null)
                            {
                                string text = string.Format("The loan with the code {0} requires a compulsory savings account to be disbursed!", loan.Code);
                                MessageBox.Show(text, @"No compulsory savings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            decimal totalAmountPercentage = 0;
                            decimal savingsBalance = loan.CompulsorySavings.GetBalance().Value;

                            foreach (Loan assosiatedLoan in loan.CompulsorySavings.Loans)
                            {
                                if (assosiatedLoan.ContractStatus != OContractStatus.Closed &&
                                    assosiatedLoan.ContractStatus !=OContractStatus.Abandoned &&
                                    assosiatedLoan.ContractStatus != OContractStatus.Postponed &&
                                    assosiatedLoan.CompulsorySavingsPercentage != null )
                                    totalAmountPercentage += (assosiatedLoan.Amount.Value*((decimal) assosiatedLoan.CompulsorySavingsPercentage/100));
                            }

                            if (totalAmountPercentage > savingsBalance)
                            {
                                MessageBox.Show(String.Format(
                                        "The balance on savings account {2} of {0} is not enough to cover total loans amount percentage of {1}.\nClient name: {3}",
                                        ServicesHelper.ConvertDecimalToString(savingsBalance),
                                        ServicesHelper.ConvertDecimalToString(totalAmountPercentage),
                                        loan.CompulsorySavings.Code,
                                        item.SubItems[0].Text),
                                    @"Insufficient savings balance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    date = DateTime.Parse(item.SubItems[2].Text);

                    loan.StartDate = date;
                    if (loan.AlignDisbursementDate.Date != DateTime.Parse(item.SubItems[3].Text).Date)
                    {
                        loan.FirstInstallmentDate = DateTime.Parse(item.SubItems[3].Text).Date;
                        loan.AlignDisbursementDate = loan.CalculateAlignDisbursementDate(DateTime.Parse(item.SubItems[3].Text).Date);
                        if (!loan.ScheduleChangedManually)
                        {
                            loan.InstallmentList = loan.CalculateInstallments(true);
                            loan.CalculateStartDates();
                        }
                    }

                    if (item.SubItems[IdxPaymentMethod].Tag != null && item.SubItems[IdxPaymentMethod].Tag.ToString() == OPaymentMethods.Savings.ToString())
                    {
                        if (loan.Product.UseCompulsorySavings && loan.CompulsorySavings != null)
                        {
                            if (loan.CompulsorySavings.Status == OSavingsStatus.Active)
                            {
                                ServicesProvider.GetInstance().GetSavingServices().LoanDisbursement(loan.CompulsorySavings, loan,
                                       loan.AlignDisbursementDate, "Disbursement of the loan: " + loan.Code, User.CurrentUser, false);
                            }
                            else
                            {
                                throw new OctopusSavingException(OctopusSavingExceptionEnum.CompulsorySavingsContractIsNotActive);
                            }
                        }
                        else
                        {
                            string text = string.Format(@"The loan of client '{0}' requires a compulsory savings account!", ((VillageMember)item.Tag).Tiers.Name);
                            MessageBox.Show(text, @"No compulsory savings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    PaymentMethod method = 
                        ServicesProvider.GetInstance().GetPaymentMethodServices().GetPaymentMethodByName(item.SubItems[IdxPaymentMethod].Text);
                    activeMember.ActiveLoans[index] = ServicesProvider.GetInstance().GetContractServices().Disburse(loan, date, true, false, method);
                }
            }
            catch (Exception ex)
            {
                isError = true;
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }

            if (!isError) Close();
        }

        private void dtDisbursement_ValueChanged(object sender, EventArgs e)
        {
            _disbursementDate = dtDisbursement.Value;
        }

        private void dtpRepayment_ValueChanged(object sender, EventArgs e)
        {
            _repaymentDate = dtpRepayment.Value;
            if (_disbursementDate > _repaymentDate)
            {
                _repaymentDate = _disbursementDate;
                dtpRepayment.Value = _repaymentDate;
            }
        }

        private void cbPaymentMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<PaymentMethod> methods = ServicesProvider.GetInstance().GetPaymentMethodServices().GetAllPaymentMethods();
            //methods = methods.Where(item => item.Id == (int)cbPaymentMethods.SelectedValue).ToList();


            //savings = savings.Where(item2 => item2 is Saving).ToList();

            //_paymentMethod = (PaymentMethod)cbPaymentMethods.SelectedItem;
        }

        private void lvMembers_SubItemEndEditing(object sender, SubItemEndEditingEventArgs e)
        {
            if (e.SubItem == IdxPaymentMethod)
                e.Item.SubItems[e.SubItem].Tag = cbPaymentMethods.SelectedItem;
        }
    }
}
