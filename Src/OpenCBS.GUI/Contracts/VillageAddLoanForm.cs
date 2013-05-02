// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.Clients;
using OpenCBS.GUI.UserControl;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.CoreDomain.Online;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Shared.Settings;

namespace OpenCBS.GUI.Contracts
{
    public partial class VillageAddLoanForm : SweetBaseForm
    {
        private readonly Village _village;
        private readonly LoanProduct _product;
        private DateTime _CreditCommitteeDate = TimeProvider.Today;
        private bool _blockItemCheck;
        private bool _hasMember;
        private ListViewItem _itemTotal = new ListViewItem("");

        private const int IdxAmount = 2;
        private const int IdxCurrency = 3;
        private const int IdxInterest = 4;
        private const int IdxGracePeriod = 5;
        private const int IdxInstallments = 6;
        private const int IdxLoanOfficer = 7;
        private const int IdxCreationDate = 8;
        private const int IdxFundingLine = 9;
        private const int IdxRemainingFlMoney = 10;
        private const int IdxCompulsorySavings = 11;
        private const int IdxCompulsoryPercentage = 12;
        //private const int IdxPaymentMethod = 13;

        private int decimalPlaces = 0;

        private readonly FundingLineServices _fLServices;
        private decimal _accumulatedAmount;
        private readonly NonSolidaryGroupForm _nsgForm;

        public VillageAddLoanForm(Village village, LoanProduct product, NonSolidaryGroupForm nsgForm)
        {
            _village = village;
            _product = product;
            _nsgForm = nsgForm;
            _product.EntryFees = ServicesProvider.GetInstance().GetProductServices().GetProductEntryFees(_product, village);
            _fLServices = new FundingLineServices(User.CurrentUser);
            _accumulatedAmount = 0;
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            lvMembers.Items.Clear();
            Color dfc = Color.Gray;
            Color fc = Color.Black;
            Color bc = Color.White;
            _fLServices.EmptyTemporaryFLAmountsStorage();
           
            ApplicationSettings generalParameters = ApplicationSettings.GetInstance("");
            decimalPlaces = generalParameters.InterestRateDecimalPlaces;
            ProductServices productServices = ServicesProvider.GetInstance().GetProductServices();
            List<Loan> listLoan;
            foreach (VillageMember member in _village.Members)
            {
                listLoan = member.ActiveLoans;
                if (listLoan.Count != 0 && !generalParameters.IsAllowMultipleLoans) continue;

                _hasMember = true;
                Person person = (Person)member.Tiers;
                
                if (_product.CycleId!=null)
                {
                    productServices.SetVillageMemberCycleParams(member, _product.Id, person.LoanCycle);
                }
                else
                    member.Product = _product;

                ListViewItem item = new ListViewItem(person.Name) { Tag = member };
                if (_product.CycleId!=null)
                {
                    item.SubItems.Add(person.IdentificationData);
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", member.Product.Amount.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(member.Product.Currency.Code);
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", member.Product.InterestRate.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", member.Product.GracePeriod.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", member.Product.NbOfInstallments.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", true ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", member.Product.Amount.HasValue ? dfc : fc, bc, item.Font));
                    if (_product.UseCompulsorySavings)
                    {
                        item.SubItems.Add("");
                        item.SubItems.Add(
                            new ListViewItem.ListViewSubItem(item, "", member.Product.CompulsoryAmount.HasValue ? dfc : fc, bc, item.Font));
                    }
                    lvMembers.Items.Add(item);
                    item.SubItems[IdxAmount].Tag = member.Product.AmountMin;
                    item.SubItems[IdxInterest].Tag = member.Product.InterestRateMin;
                    item.SubItems[IdxInstallments].Tag = member.Product.NbOfInstallmentsMin;
                    item.SubItems[IdxCurrency].Tag = member.Product.Currency;
                }
                else
                {
                    item.SubItems.Add(person.IdentificationData);
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", _product.Amount.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(_product.Currency.Code);
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", _product.InterestRate.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", _product.GracePeriod.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", _product.NbOfInstallments.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", true ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", dfc, bc, item.Font));
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", _product.Amount.HasValue ? dfc : fc, bc, item.Font));
                    if (_product.UseCompulsorySavings)
                    {
                        item.SubItems.Add("");
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", _product.CompulsoryAmount.HasValue ? dfc : fc, bc, item.Font));
                    }
                    lvMembers.Items.Add(item);
                    item.SubItems[IdxCurrency].Tag = member.Product.Currency;
                }
            }
            if (_hasMember)
            {
                OCurrency zero = 0m;
                _itemTotal.Text = GetString("Total");
                _itemTotal.SubItems.Add("");
                _itemTotal.SubItems.Add(zero.GetFormatedValue(true));
                _itemTotal.SubItems.Add("");
                lvMembers.Items.Add(_itemTotal);
            }
            lvMembers.SubItemClicked += lvMembers_SubItemClicked;
            lvMembers.SubItemEndEditing += lvMembers_SubItemEndEditing;
            lvMembers.DoubleClickActivation = true;
            
            if (!_product.GracePeriod.HasValue)
            {
                udGracePeriod.Minimum = _product.GracePeriodMin ?? 0;
                udGracePeriod.Maximum = _product.GracePeriodMax ?? 0;
            }

            if (_product.CycleId == null)
            {
                if (!_product.NbOfInstallments.HasValue)
                {
                    udInstallments.Minimum = _product.NbOfInstallmentsMin ?? 0;
                    udInstallments.Maximum = _product.NbOfInstallmentsMax ?? 0;
                }
            }
            
            List<User> users = ServicesProvider.GetInstance().GetUserServices().FindAll(false);
            foreach (User user in users) cbLoanOfficer.Items.Add(user);

            List<FundingLine> lines = ServicesProvider.GetInstance().GetFundingLinesServices().SelectFundingLines();
            foreach (FundingLine line in lines) cbFundingLine.Items.Add(line);

            // Compulsory savings
            if (_product.UseCompulsorySavings)
            {
                if (!_product.CompulsoryAmount.HasValue)
                {
                    udCompulsoryPercentage.Minimum = _product.CompulsoryAmountMin ?? 0;
                    udCompulsoryPercentage.Maximum = _product.CompulsoryAmountMax ?? 0;
                }
            }
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
                
                customExchangeRate =
                       ServicesProvider.GetInstance().GetAccountingServices().FindLatestExchangeRate(
                           DateTime.Today, _product.Currency);
                total += customExchangeRate.Rate == 0
                                          ? 0
                                          : (OCurrency)Convert.ToDecimal(item.SubItems[IdxAmount].Text) / customExchangeRate.Rate;
            }
            _itemTotal.SubItems[IdxAmount].Text = total.GetFormatedValue(ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().UseCents);
            _itemTotal.SubItems[IdxCurrency].Text = ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().Code;
        }

        private void lvMembers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ListViewItem item = e.Item;
            item.Font = new Font("Arial", 9F, item.Checked ? FontStyle.Bold : FontStyle.Regular);
            foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
            {
                subitem.Font = item.Font;
            }
            if (item.Checked && item != _itemTotal)
            {
                if (string.IsNullOrEmpty(item.SubItems[IdxAmount].Text)) // Amount
                {
                    if (_product.CycleId == null)
                    {
                        if (_product.Amount.HasValue)
                        {
                            item.SubItems[IdxAmount].Text = _product.Amount.GetFormatedValue(_product.Currency.UseCents);
                            item.SubItems[IdxAmount].Tag = _product.Amount;
                        }
                        else
                        {
                            item.SubItems[IdxAmount].Text = _product.AmountMin.GetFormatedValue(_product.Currency.UseCents);
                            item.SubItems[IdxAmount].Tag = _product.AmountMin;
                        }
                    }
                    else
                    {
                        item.SubItems[IdxAmount].Text = ((VillageMember)item.Tag).Product.AmountMin.GetFormatedValue(_product.Currency.UseCents);
                    }
                    
                }
                if (string.IsNullOrEmpty(item.SubItems[IdxCurrency].Text)) // Currency
                {
                    if (_product.CycleId == null)
                    {
                        if (_product.Amount.HasValue)
                        {
                            item.SubItems[IdxCurrency].Text = _product.Currency.Code;
                            item.SubItems[IdxCurrency].Tag = _product.Currency.Code;
                        }
                        else
                        {
                            item.SubItems[IdxCurrency].Text = _product.Currency.Code;
                            item.SubItems[IdxCurrency].Tag = _product.Currency.Code;
                        }
                    }
                    else
                    {
                        item.SubItems[IdxCurrency].Text = ((VillageMember) item.Tag).Product.Currency.Code;
                    }

                }
                if (string.IsNullOrEmpty(item.SubItems[IdxInterest].Text)) // Interest
                {
                    if (_product.CycleId == null)
                    {
                        if (_product.InterestRate.HasValue)
                        {
                            item.SubItems[IdxInterest].Text = Math.Round(_product.InterestRate.Value * 100, decimalPlaces).ToString();
                            item.SubItems[IdxInterest].Tag = _product.InterestRate;
                        }
                        else
                        {
                            item.SubItems[IdxInterest].Text = Math.Round(_product.InterestRateMin.Value * 100, decimalPlaces).ToString();
                            item.SubItems[IdxInterest].Tag = _product.InterestRateMin;
                        }
                    }
                    else
                    {
                        item.SubItems[IdxInterest].Text = Math.Round(((VillageMember)item.Tag).Product.InterestRateMin.Value * 100, decimalPlaces).ToString();
                    }
                }
                if (string.IsNullOrEmpty(item.SubItems[IdxGracePeriod].Text)) // Grace period
                {
                    if (_product.GracePeriod.HasValue)
                    {
                        item.SubItems[IdxGracePeriod].Text = _product.GracePeriod.ToString();
                        item.SubItems[IdxGracePeriod].Tag = _product.GracePeriod;
                    }
                    else
                    {
                        item.SubItems[IdxGracePeriod].Text = _product.GracePeriodMin.ToString();
                        item.SubItems[IdxGracePeriod].Tag = _product.GracePeriodMin;
                    }
                }
                if (string.IsNullOrEmpty(item.SubItems[IdxInstallments].Text)) // Installments
                {
                    if (_product.CycleId == null)
                    {
                        if (_product.NbOfInstallments.HasValue)
                        {
                            item.SubItems[IdxInstallments].Text = _product.NbOfInstallments.ToString();
                            item.SubItems[IdxInstallments].Tag = _product.NbOfInstallments;
                        }
                        else
                        {
                            item.SubItems[IdxInstallments].Text = _product.NbOfInstallmentsMin.ToString();
                            item.SubItems[IdxInstallments].Tag = _product.NbOfInstallmentsMin;
                        }
                    }
                    else
                    {
                        item.SubItems[IdxInstallments].Text = ((VillageMember)item.Tag).Product.NbOfInstallmentsMin.ToString();
                    }
                    
                }
                if (string.IsNullOrEmpty(item.SubItems[IdxLoanOfficer].Text)) // Loan officer
                {
                    item.SubItems[IdxLoanOfficer].Text = _village.LoanOfficer.ToString();
                    item.SubItems[IdxLoanOfficer].Tag = _village.LoanOfficer;
                }

                if (string.IsNullOrEmpty(item.SubItems[IdxCreationDate].Text)) // Credit Committee date
                {
                    item.SubItems[IdxCreationDate].Text = TimeProvider.Today.ToShortDateString();
                    item.SubItems[IdxCreationDate].Tag = TimeProvider.Today.ToShortDateString();
                }

                if (string.IsNullOrEmpty(item.SubItems[IdxFundingLine].Text)) // Funding line
                {
                    FundingLine fl;
                    string flName;
                    if (_product.FundingLine != null)
                    {
                        flName = _product.FundingLine.Name;
                        fl = _product.FundingLine;
                    }
                    else if (cbFundingLine.Items.Count > 0)
                    {
                        flName = ((FundingLine)cbFundingLine.Items[0]).Name;
                        fl = (FundingLine)cbFundingLine.Items[0];
                    }
                    else
                    {
                        flName = string.Empty;
                        fl = null;
                    }
                    item.SubItems[IdxFundingLine].Text = flName;
                    item.SubItems[IdxFundingLine].Tag = fl;
                    _accumulatedAmount = _fLServices.CheckIfAmountIsEnough(fl, decimal.Parse(item.SubItems[2].Text));
                    item.SubItems[IdxRemainingFlMoney].Text = _accumulatedAmount.ToString();
                    if (_accumulatedAmount <= 0)
                    {
                        item.BackColor = Color.Red;
                    }
                    else
                        item.BackColor = Color.Transparent;
                }
                if (_product.UseCompulsorySavings && string.IsNullOrEmpty(item.SubItems[IdxCompulsorySavings].Text))
                {
                    //item.SubItems[IdxCompulsorySavings]. = _village.
                    //item.SubItems[IdxCompulsorySavings].Tag = _village.LoanOfficer;

                    cbCompulsorySavings.Items.Clear();
                    List<ISavingsContract> savings = ServicesProvider.GetInstance().GetSavingServices().GetSavingsByClientId(((VillageMember)e.Item.Tag).Tiers.Id);
                    savings = savings.Where(item2 => item2 is SavingBookContract).ToList(); // Only savings book

                    if (savings.Count > 0)
                    {
                        foreach (SavingBookContract saving in savings) cbCompulsorySavings.Items.Add(saving);

                        item.SubItems[IdxCompulsorySavings].Text = savings[0].Code;
                        item.SubItems[IdxCompulsorySavings].Tag = savings[0];
                    }
                }
                if (_product.UseCompulsorySavings && string.IsNullOrEmpty(item.SubItems[IdxCompulsoryPercentage].Text))
                {
                    if (_product.CompulsoryAmount.HasValue)
                    {
                        item.SubItems[IdxCompulsoryPercentage].Text = _product.CompulsoryAmount.ToString();
                        item.SubItems[IdxCompulsoryPercentage].Tag = _product.CompulsoryAmount;
                    }
                    else
                    {
                        item.SubItems[IdxCompulsoryPercentage].Text = _product.CompulsoryAmountMin.ToString();
                        item.SubItems[IdxCompulsoryPercentage].Tag = _product.CompulsoryAmountMin;
                    }
                }
                /*if (string.IsNullOrEmpty(item.SubItems[IdxPaymentMethod].Text))
                {
                    cbPaymentMethods.Items.Clear();
                    List<PaymentMethod> methods = ServicesProvider.GetInstance().GetPaymentMethodServices().GetAllPaymentMethods();

                    if (methods.Count > 0)
                    {
                        foreach (PaymentMethod method in methods)
                            cbPaymentMethods.Items.Add(method.Name);

                        item.SubItems[IdxPaymentMethod].Text = methods[0].Name;
                        item.SubItems[IdxPaymentMethod].Tag = methods[0].Method;
                    }
                }*/
            }
            else
            {
                for (int i = 2; i < item.SubItems.Count; i++)
                {
                    item.SubItems[i].Text = "";
                }
            }
            if (item == _itemTotal)
            {
                foreach (ListViewItem i in item.ListView.Items)
                {
                    i.Checked = item.Checked;
                }
            }
            UpdateTotal();
        }

        private void lvMembers_SubItemClicked(object sender, SubItemEventArgs e)
        {
            if (e.Item.Checked && e.Item != _itemTotal)
            {
                switch (e.SubItem)
                {
                    case IdxAmount: // Amount
                        if (_product.CycleId != null)
                        {
                            lvMembers.StartEditing(tbAmount, e.Item, e.SubItem);
                        }
                        if (!_product.Amount.HasValue)
                        {
                            lvMembers.StartEditing(tbAmount, e.Item, e.SubItem);
                        }
                        break;

                    case IdxInterest: // Interest
                        if (_product.CycleId!=null)
                        {
                            lvMembers.StartEditing(tbInterest, e.Item, e.SubItem);
                        }
                        else if (!_product.InterestRate.HasValue)
                        {
                            lvMembers.StartEditing(tbInterest, e.Item, e.SubItem);
                        }
                        break;

                    case IdxGracePeriod: // Grace period
                        if (!_product.GracePeriod.HasValue)
                        {
                            lvMembers.StartEditing(udGracePeriod, e.Item, e.SubItem);
                        }
                        break;

                    case IdxInstallments: // Installments
                        if (_product.CycleId != null)
                        {
                            udInstallments.Minimum = ((VillageMember) e.Item.Tag).Product.NbOfInstallmentsMin.Value;
                            udInstallments.Maximum = ((VillageMember)e.Item.Tag).Product.NbOfInstallmentsMax.Value;
                            lvMembers.StartEditing(udInstallments, e.Item, e.SubItem);
                        }
                        else if (!_product.NbOfInstallments.HasValue)
                        {
                            lvMembers.StartEditing(udInstallments, e.Item, e.SubItem);
                        }
                        break;

                    case IdxLoanOfficer:
                        lvMembers.StartEditing(cbLoanOfficer, e.Item, e.SubItem);
                        break;
   
                    case IdxCreationDate:
                        lvMembers.StartEditing(dtCreationDate, e.Item, e.SubItem);
                        break;

                    case IdxFundingLine:
                        lvMembers.StartEditing(cbFundingLine, e.Item, e.SubItem);
                        break;

                    case IdxCompulsorySavings:
                        if (_product.UseCompulsorySavings)
                        {
                            cbCompulsorySavings.Items.Clear();
                            List<ISavingsContract> savings = ServicesProvider.GetInstance().GetSavingServices().
                                GetSavingsByClientId(((VillageMember)e.Item.Tag).Tiers.Id);
                            savings = savings.Where(item2 => item2 is SavingBookContract && 
                                item2.Status!=OSavingsStatus.Closed).ToList(); // Only savings book
                            
                            foreach (SavingBookContract saving in savings) cbCompulsorySavings.Items.Add(saving);

                            lvMembers.StartEditing(cbCompulsorySavings, e.Item, e.SubItem);
                        }
                        break;

                    case IdxCompulsoryPercentage:
                        if (_product.UseCompulsorySavings && !_product.CompulsoryAmount.HasValue)
                        {
                            lvMembers.StartEditing(udCompulsoryPercentage, e.Item, e.SubItem);
                        }
                        break;
                    /*
                    case IdxPaymentMethod:
                        cbPaymentMethods.Items.Clear();
                        List<PaymentMethod> methods = ServicesProvider.GetInstance().GetPaymentMethodServices().GetAllPaymentMethods();

                        foreach (PaymentMethod method in methods) 
                            cbPaymentMethods.Items.Add(method.Name);

                        lvMembers.StartEditing(cbPaymentMethods, e.Item, e.SubItem);
                        break;
                        */
                    default:
                        break;
                }
            }
        }

        private void lvMembers_SubItemEndEditing(object sender, SubItemEndEditingEventArgs e)
        {
            switch (e.SubItem)
            {
                case IdxAmount: // Amount
                    
                    OCurrency temp;
                    try
                    {
                        temp = decimal.Parse(e.DisplayText);
                    }
                    catch
                    {
                        temp = (OCurrency)e.Item.SubItems[e.SubItem].Tag;
                    }

                    if (_product.CycleId != null)
                    {
                        LoanProduct product = ((VillageMember) e.Item.Tag).Product;
                        temp = temp < product.AmountMin ? product.AmountMin : temp;
                        temp = temp > product.AmountMax ? product.AmountMax : temp;
                    }
                    else
                    {
                        temp = temp < _product.AmountMin ? _product.AmountMin : temp;
                        temp = temp > _product.AmountMax ? _product.AmountMax : temp;
                    }
                    e.DisplayText = temp.GetFormatedValue(_product.Currency.UseCents);
                    e.Item.SubItems[e.SubItem].Tag = temp;
                    e.Item.SubItems[e.SubItem].Text = e.DisplayText;
                    _accumulatedAmount = _fLServices.CheckIfAmountIsEnough(e.Item.SubItems[IdxFundingLine].Tag as FundingLine, temp.Value);
                    e.Item.SubItems[IdxRemainingFlMoney].Text = _accumulatedAmount.ToString();

                    if (_accumulatedAmount <= 0)
                    {
                        lvMembers.Items[e.Item.Index].BackColor = Color.Red;
                    }
                    else
                    {
                        lvMembers.Items[e.Item.Index].BackColor = Color.Transparent;
                    }
                    UpdateTotal();
                    break;

                case IdxInterest: // Interest
                    decimal i;
                    try
                    {
                        i = decimal.Parse(e.DisplayText);
                        i = i / 100;
                    }
                    catch
                    {
                        i = (decimal)e.Item.SubItems[e.SubItem].Tag;
                    }
                    if (_product.CycleId == null)
                    {
                        i = i < _product.InterestRateMin.Value ? _product.InterestRateMin.Value : i;
                        i = i > _product.InterestRateMax.Value ? _product.InterestRateMax.Value : i;
                    }
                    else
                    {
                        i = i < ((VillageMember) e.Item.Tag).Product.InterestRateMin.Value
                                ? ((VillageMember) e.Item.Tag).Product.InterestRateMin.Value
                                : i;
                        i = i > ((VillageMember)e.Item.Tag).Product.InterestRateMax.Value
                                ? ((VillageMember)e.Item.Tag).Product.InterestRateMax.Value
                                : i;

                    }
                    
                    e.DisplayText = Math.Round(i * 100, decimalPlaces).ToString();
                    e.Item.SubItems[e.SubItem].Tag = i;
                    break;

                case IdxGracePeriod: // Grace period
                    e.Item.SubItems[e.SubItem].Tag = int.Parse(e.DisplayText);
                    break;

                case IdxInstallments: // Number of installments
                    int? temp1;
                    try
                    {
                        temp1 = int.Parse(e.DisplayText);
                    }
                    catch
                    {
                        temp1 = (int)e.Item.SubItems[e.SubItem].Tag;
                    }

                    if (_product.CycleId != null)
                    {
                        LoanProduct product = ((VillageMember)e.Item.Tag).Product;
                        temp1 = temp1 < product.NbOfInstallmentsMin ? product.NbOfInstallmentsMin : temp1;
                        temp1 = temp1 > product.NbOfInstallmentsMax ? product.NbOfInstallmentsMax : temp1;
                    }
                    else
                    {
                        temp1 = temp1 < _product.NbOfInstallmentsMin ? _product.NbOfInstallmentsMin : temp1;
                        temp1 = temp1 > _product.NbOfInstallmentsMax ? _product.NbOfInstallmentsMax : temp1;
                    }
                    e.DisplayText = temp1.ToString();
                    e.Item.SubItems[e.SubItem].Tag = temp1;
                    break;
                    
                case IdxLoanOfficer: // Loan officer
                    e.Item.SubItems[e.SubItem].Tag = cbLoanOfficer.SelectedItem;
                    break;

                case IdxFundingLine: //Funding line
                    FundingLine fl = (FundingLine)cbFundingLine.SelectedItem;
                    if (fl != (e.Item.SubItems[e.SubItem].Tag as FundingLine))
                    {
                        e.Item.SubItems[e.SubItem].Tag = cbFundingLine.SelectedItem;

                        _accumulatedAmount = _fLServices.CheckIfAmountIsEnough(fl, decimal.Parse(e.Item.SubItems[2].Text));
                        e.Item.SubItems[IdxRemainingFlMoney].Text = _accumulatedAmount.ToString();

                        if (_accumulatedAmount <= 0)
                        {
                            lvMembers.Items[e.Item.Index].BackColor = Color.Red;
                        }
                        else
                        {
                            lvMembers.Items[e.Item.Index].BackColor = Color.Transparent;
                        }
                    }
                    break;
                
                case IdxCreationDate:
                    e.Item.SubItems[e.SubItem].Tag = _CreditCommitteeDate;
                    break;

                case IdxCompulsorySavings: // Compulsory savings
                    if (_product.UseCompulsorySavings)
                        e.Item.SubItems[e.SubItem].Tag = cbCompulsorySavings.SelectedItem;
                    break;

                case IdxCompulsoryPercentage: // Compulsory percentage
                    if (_product.UseCompulsorySavings)
                        e.Item.SubItems[e.SubItem].Tag = int.Parse(e.DisplayText);
                    break;
                /*
                case IdxPaymentMethod:
                        e.Item.SubItems[e.SubItem].Tag = cbPaymentMethods.SelectedItem;
                    break;
                    */
                default:
                    break;
            }
        }

        // The two events below are necessary to prevent
        // the list view from ticking / unticking the checkbox
        // when the user double-clicks the row. The trick was found
        // here:
        // http://www.ncsystems.ru/en/programming/ListView_ItemCheck
        private void lvMembers_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button)
            {
                if (e.Clicks >= 2)
                {
                    _blockItemCheck = true;
                }
            }
        }

        private void lvMembers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_blockItemCheck)
            {
                e.NewValue = e.CurrentValue;
                _blockItemCheck = !_blockItemCheck;
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            
            if (_CheckIfMoneyIsEnough())
                if (!MessageBox.Show(MultiLanguageStrings.GetString(Ressource.VillageForm, "MoneyNotEnoughForAll.Text"), @"!",
                    MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
                        return;

            if (_product.GracePeriod == null && !_CheckGracePeriod())
            {
                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.VillageForm, "GracePeriodNotCorrect"),
                    MultiLanguageStrings.GetString(Ressource.VillageForm, "GracePeriod"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (ListViewItem item in lvMembers.Items)
            {
                if (item == _itemTotal) continue;
                if (!item.Checked) continue;
                if (_product.UseCompulsorySavings && item.SubItems[IdxCompulsorySavings].Tag == null)
                {
                    string text = string.Format(@"The loan of client '{0}' requires a compulsory savings account!", ((VillageMember)item.Tag).Tiers.Name);
                    MessageBox.Show(text, @"No compulsory savings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                /*if (item.SubItems[IdxPaymentMethod].Tag != null && 
                    item.SubItems[IdxPaymentMethod].Tag.ToString() == OPaymentMethods.Savings.ToString())
                {
                    if (_product.UseCompulsorySavings && item.SubItems[IdxCompulsorySavings].Tag == null)
                    {
                        string text = string.Format(@"The loan of client '{0}' requires a compulsory savings account!", ((VillageMember)item.Tag).Tiers.Name);
                        MessageBox.Show(text, @"No compulsory savings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }*/
            }

            if(!_nsgForm.Save()) return;

            Loan loan = null;
            VillageMember member;

            try
            {
                foreach (ListViewItem item in lvMembers.Items)
                {
                    if (!item.Checked || item == _itemTotal) continue;
                    member = item.Tag as VillageMember;
                    Project project;
                    if (null == member) continue;

                    OCurrency amount = (OCurrency)item.SubItems[IdxAmount].Tag;
                    decimal interest = (decimal)item.SubItems[IdxInterest].Tag;
                    int gracePeriod = (int)item.SubItems[IdxGracePeriod].Tag;
                    int installments = (int)item.SubItems[IdxInstallments].Tag;
                    DateTime date = Convert.ToDateTime(item.SubItems[IdxCreationDate].Tag);

                    //creation of loan
                    loan = new Loan(_product,
                                    amount,
                                    interest,
                                    installments,
                                    gracePeriod,
                                    date,
                                    _village.MeetingDay,
                                    User.CurrentUser,
                                    ServicesProvider.GetInstance().GetGeneralSettings(),
                                    ServicesProvider.GetInstance().GetNonWorkingDate(),
                                    CoreDomainProvider.GetInstance().GetProvisioningTable(),
                                    CoreDomainProvider.GetInstance().GetChartOfAccounts())
                    {
                        NonRepaymentPenalties =
                        {
                            InitialAmount = _product.NonRepaymentPenalties.InitialAmount ?? 0,
                            OLB = _product.NonRepaymentPenalties.OLB ?? 0,
                            OverDuePrincipal = _product.NonRepaymentPenalties.OverDuePrincipal ?? 0,
                            OverDueInterest = _product.NonRepaymentPenalties.OverDueInterest ?? 0
                        },
                        InstallmentType = _product.InstallmentType,
                        AnticipatedTotalRepaymentPenalties = 0,
                        FundingLine = item.SubItems[IdxFundingLine].Tag as FundingLine,
                        LoanOfficer = (User)item.SubItems[IdxLoanOfficer].Tag,
                        Synchronize = false,
                        ContractStatus = OContractStatus.Pending,
                        CreditCommitteeCode = string.Empty,
                        GracePeriod = gracePeriod,
                        GracePeriodOfLateFees = _product.GracePeriodOfLateFees,
                        AmountMin = member.Product.AmountMin,
                        AmountMax = member.Product.AmountMax,
                        InterestRateMin = member.Product.InterestRateMin,
                        InterestRateMax = member.Product.InterestRateMax,
                        NmbOfInstallmentsMin = member.Product.NbOfInstallmentsMin,
                        NmbOfInstallmentsMax = member.Product.NbOfInstallmentsMax
                    };

                    loan.LoanEntryFeesList = new List<LoanEntryFee>();
                    foreach (EntryFee fee in loan.Product.EntryFees)
                    {
                        LoanEntryFee loanEntryFee = new LoanEntryFee();
                        loanEntryFee.ProductEntryFee = fee;
                        loanEntryFee.ProductEntryFeeId = (int)fee.Id;
                        if (fee.Value.HasValue)
                            loanEntryFee.FeeValue = (decimal)fee.Value;
                        else
                            loanEntryFee.FeeValue = (decimal)fee.Min;
                        loan.LoanEntryFeesList.Add(loanEntryFee);
                    }

                    var client = member.Tiers;
                    if (0 == client.Projects.Count)
                    {
                        project = new Project("Village");
                        project.Name = "Village";
                        project.Code = "Village";
                        project.Aim = "Village";
                        project.BeginDate = date;
                        project.Id = ServicesProvider.GetInstance().GetProjectServices().SaveProject(project, client);
                        member.Tiers.AddProject(project);
                    }
                    project = client.Projects[0];

                    // Compulsory savings
                    if (_product.UseCompulsorySavings)
                    {
                        loan.CompulsorySavings = (SavingBookContract)item.SubItems[IdxCompulsorySavings].Tag;
                        loan.CompulsorySavingsPercentage = (int)item.SubItems[IdxCompulsoryPercentage].Tag;
                    }
                    var person = client as Person;
                    if (person == null) throw new ApplicationException("Member can not be other than person.");
                    loan.EconomicActivity = person.Activity;

                    ServicesProvider.GetInstance().GetContractServices().CheckLoanFilling(loan);
                    loan.NsgID = _village.Id;
                    ServicesProvider.GetInstance().GetContractServices().SaveLoan(ref loan, project.Id, ref client);
                    project.AddCredit(loan, loan.ClientType);
                    if (!_village.Active)
                    {
                        _village.Active = true;
                        ServicesProvider.GetInstance().GetContractServices().UpdateVillageStatus(_village);
                    }
                    loan.Closed = false;
                    client.Active = true;
                    client.Status = OClientStatus.Active;
                    member.ActiveLoans.Add(loan);
                }
            }
            catch (Exception ex)
            {
                isError = true;
                loan.ContractStatus = OContractStatus.Pending;
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
           
            if (!isError)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool _CheckIfMoneyIsEnough()
        {
            foreach (ListViewItem item in lvMembers.Items)
            {
                if (!item.Checked || item == _itemTotal) continue;
                if (Convert.ToDecimal(item.SubItems[IdxRemainingFlMoney].Text) < 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool _CheckGracePeriod()
        {
            foreach (ListViewItem item in lvMembers.Items)
            {
                if (!item.Checked || item == _itemTotal) continue;
                if (Convert.ToDecimal(item.SubItems[IdxGracePeriod].Text) >= _product.GracePeriodMin &&
                    Convert.ToDecimal(item.SubItems[IdxGracePeriod].Text) <= _product.GracePeriodMax)
                    return true;
            }
            return false;
        }

        private void dtCreationDate_ValueChanged(object sender, EventArgs e)
        {
            _CreditCommitteeDate = dtCreationDate.Value;
        }
    }
}
