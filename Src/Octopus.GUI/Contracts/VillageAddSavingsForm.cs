//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
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
using System.Drawing;
using System.Windows.Forms;
using Octopus.CoreDomain;
using Octopus.CoreDomain.Accounting;
using Octopus.CoreDomain.Clients;
using Octopus.CoreDomain.Products;
using Octopus.Enums;
using Octopus.ExceptionsHandler;
using Octopus.GUI.Clients;
using Octopus.GUI.UserControl;
using Octopus.Services;
using Octopus.Shared;
using Octopus.CoreDomain.Online;
using Octopus.CoreDomain.Contracts.Savings;

namespace Octopus.GUI.Contracts
{
    using LVSI = ListViewItem.ListViewSubItem;
    public partial class VillageAddSavingsForm : SweetBaseForm
    {
        private readonly Village _village;
        private readonly ISavingProduct _product;
        private bool blockItemCheck;
        private bool _hasMember = false;
        private ListViewItem _itemTotal = new ListViewItem("");

        private const int idxInitialAmount = 2;
        private const int idxCurrency = 3;
        private const int idxInterestRate = 4;
        private const int idxEntryFees = 5;
        private const int idxWithdrawFees = 6;
        private const int idxTransferFees = 7;
        private const int idxIbtFees = 8;

        private const int idxDepositFees = 9;
        private const int idxChequeDepositFees = 10;
        private const int idxCloseFees = 11;
        private const int idxManagementFees = 12;
        private const int idxOverdraftFees = 13;
        private const int idxAgioFees = 14;
        private const int idxReopenFees = 15;

        private double _defaultInterestRate;
        private OCurrency _defaultEntryFees;
        private decimal _defaultWithdrawFees;
        private decimal _defaultTransferFees;
        private decimal _defaultIbtFees;

        private decimal _defaultDepositFees;
        private decimal _defaultChequeDepositFees;
        private decimal _defaultCloseFees;
        private decimal _defaultManagementFees;
        private decimal _defaultOverdraftFees;
        private decimal _defaultAgioFees;
        private decimal _defaultReopenFees;

        private readonly NonSolidaryGroupForm _nsgForm;

        public VillageAddSavingsForm(Village village, ISavingProduct product, NonSolidaryGroupForm nsgForm)
        {
            _village = village;
            _product = product;
            _nsgForm = nsgForm;
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            udInitialAmount.Minimum = _product.InitialAmountMin.Value;
            udInitialAmount.Maximum = _product.InitialAmountMax.Value;

            if (!_product.InterestRate.HasValue)
            {
                _defaultInterestRate = _product.InterestRateMin.Value * 100;
                udInterestRate.Minimum = (decimal)_product.InterestRateMin * 100;
                udInterestRate.Maximum = (decimal)_product.InterestRateMax * 100;
            }
            else
                _defaultInterestRate = _product.InterestRate.Value * 100;

            if (!_product.EntryFees.HasValue)
            {
                _defaultEntryFees = _product.EntryFeesMin;
                udEntryFees.Minimum = _product.EntryFeesMin.Value;
                udEntryFees.Maximum = _product.EntryFeesMax.Value;
            }
            else
                _defaultEntryFees = _product.EntryFees;

            if (_product is SavingsBookProduct)
            {
                SavingsBookProduct sbp = (SavingsBookProduct) _product;
                lvMembers.Columns.Remove(chLoan);
                if (sbp.WithdrawFeesType == OSavingsFeesType.Flat)
                {
                    if (!sbp.FlatWithdrawFees.HasValue)
                    {
                        _defaultWithdrawFees = sbp.FlatWithdrawFeesMin.Value;
                        udWithdrawFees.Minimum = sbp.FlatWithdrawFeesMin.Value;
                        udWithdrawFees.Maximum = sbp.FlatWithdrawFeesMax.Value;
                    }
                    else
                    {
                        _defaultWithdrawFees = sbp.FlatWithdrawFees.Value;
                    }
                }
                else
                {
                    if (!sbp.RateWithdrawFees.HasValue)
                    {
                        _defaultWithdrawFees = (decimal)sbp.RateWithdrawFeesMin.Value * 100;
                        udWithdrawFees.Minimum = (decimal)sbp.RateWithdrawFeesMin.Value * 100;
                        udWithdrawFees.Maximum = (decimal)sbp.RateWithdrawFeesMax.Value * 100;
                    }
                    else
                        _defaultWithdrawFees = (decimal)sbp.RateWithdrawFees.Value * 100;
                }

                if (sbp.TransferFeesType == OSavingsFeesType.Flat)
                {
                    if (!sbp.FlatTransferFees.HasValue)
                    {
                        _defaultTransferFees = sbp.FlatTransferFeesMin.Value;
                        udTransferFees.Minimum = sbp.FlatTransferFeesMin.Value;
                        udTransferFees.Maximum = sbp.FlatTransferFeesMax.Value;
                    }
                    else
                        _defaultTransferFees = sbp.FlatTransferFees.Value;
                }
                else
                {
                    if (!sbp.RateTransferFees.HasValue)
                    {
                        _defaultTransferFees = (decimal)sbp.RateTransferFeesMin.Value * 100;
                        udTransferFees.Minimum = (decimal)sbp.RateTransferFeesMin.Value * 100;
                        udTransferFees.Maximum = (decimal)sbp.RateTransferFeesMax.Value * 100;
                    }
                    else
                        _defaultTransferFees = (decimal)sbp.RateTransferFees.Value * 100;
                }

                if (sbp.InterBranchTransferFee.IsRange)
                {
                    _defaultIbtFees = sbp.InterBranchTransferFee.Min.Value;
                    nudIbtFees.Minimum = sbp.InterBranchTransferFee.Min.Value;
                    nudIbtFees.Maximum = sbp.InterBranchTransferFee.Max.Value;
                    nudIbtFees.Increment = sbp.InterBranchTransferFee.IsFlat ? 1 : 0.01m;
                }
                else
                {
                    _defaultIbtFees = sbp.InterBranchTransferFee.Value.Value;
                    nudIbtFees.Minimum = _defaultIbtFees;
                    nudIbtFees.Maximum = _defaultIbtFees;
                }

                // Deposit fees
                if (!sbp.DepositFees.HasValue)
                {
                    _defaultDepositFees = sbp.DepositFeesMin.Value;
                    udDepositFees.Minimum = sbp.DepositFeesMin.Value;
                    udDepositFees.Maximum = sbp.DepositFeesMax.Value;
                }
                else
                    _defaultDepositFees = sbp.DepositFees.Value;

                // Cheque deposit fees
                if (!sbp.ChequeDepositFees.HasValue)
                {
                    _defaultChequeDepositFees = sbp.ChequeDepositFeesMin.Value;
                    udChequeDepositFees.Minimum = sbp.ChequeDepositFeesMin.Value;
                    udChequeDepositFees.Maximum = sbp.ChequeDepositFeesMax.Value;
                }
                else
                    _defaultChequeDepositFees = sbp.ChequeDepositFees.Value;

                // Close fees
                if (!sbp.CloseFees.HasValue)
                {
                    _defaultCloseFees = sbp.CloseFeesMin.Value;
                    udCloseFees.Minimum = sbp.CloseFeesMin.Value;
                    udCloseFees.Maximum = sbp.CloseFeesMax.Value;
                }
                else
                    _defaultCloseFees = sbp.CloseFees.Value;

                // Management
                if (!sbp.ManagementFees.HasValue)
                {
                    _defaultManagementFees = sbp.ManagementFeesMin.Value;
                    udManagementFees.Minimum = sbp.ManagementFeesMin.Value;
                    udManagementFees.Maximum = sbp.ManagementFeesMax.Value;
                }
                else
                    _defaultManagementFees = sbp.ManagementFees.Value;

                // Overdraft
                if (!sbp.OverdraftFees.HasValue)
                {
                    _defaultOverdraftFees = sbp.OverdraftFeesMin.Value;
                    udOverdraftFees.Minimum = sbp.OverdraftFeesMin.Value;
                    udOverdraftFees.Maximum = sbp.OverdraftFeesMax.Value;
                }
                else
                    _defaultOverdraftFees = sbp.OverdraftFees.Value;

                // Agio
                if (!sbp.AgioFees.HasValue)
                {
                    _defaultAgioFees = (decimal) sbp.AgioFeesMin.Value * 100;
                    udAgioFees.Minimum = (decimal) sbp.AgioFeesMin.Value * 100;
                    udAgioFees.Maximum = (decimal) sbp.AgioFeesMax.Value * 100;
                }
                else
                    _defaultAgioFees = (decimal) sbp.AgioFees.Value * 100;

                // Reopen
                if (!sbp.ReopenFees.HasValue)
                {
                    _defaultReopenFees = sbp.ReopenFeesMin.Value;
                    udReopenFees.Minimum = sbp.ReopenFeesMin.Value;
                    udReopenFees.Maximum = sbp.ReopenFeesMax.Value;
                }
                else
                    _defaultReopenFees = sbp.ReopenFees.Value;
            }

            lvMembers.Items.Clear();
            Color dfc = Color.Gray;
            Color fc = Color.Black;
            Color bc = Color.White;

            foreach (VillageMember member in _village.Members)
            {
                Person person = (Person) member.Tiers;
                ListViewItem item = new ListViewItem(person.Name) {Tag = member};
                item.UseItemStyleForSubItems = false;
                item.SubItems.Add(person.IdentificationData);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", fc, bc, item.Font));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", fc, bc, item.Font));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", _product.InterestRate.HasValue ? dfc : fc, bc, item.Font));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", _product.EntryFees.HasValue ? dfc : fc, bc, item.Font));
                if (_product is SavingsBookProduct)
                {
                    SavingsBookProduct sbp = (SavingsBookProduct) _product;
                    if (sbp.WithdrawFeesType == OSavingsFeesType.Flat)
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "",
                            sbp.FlatWithdrawFees.HasValue ? dfc : fc, bc, item.Font));
                    else
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "",
                            sbp.RateWithdrawFees.HasValue ? dfc : fc, bc, item.Font));

                    if (sbp.TransferFeesType == OSavingsFeesType.Flat)
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "",
                            sbp.FlatTransferFees.HasValue ? dfc : fc, bc, item.Font));
                    else
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "",
                            sbp.RateTransferFees.HasValue ? dfc : fc, bc, item.Font));
                    LVSI lvsi = new LVSI(item, "", sbp.InterBranchTransferFee.IsRange ? fc : dfc, bc, item.Font);
                    item.SubItems.Add(lvsi);
                    
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", sbp.DepositFees.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", sbp.ChequeDepositFees.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", sbp.CloseFees.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", sbp.ManagementFees.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", sbp.OverdraftFees.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", sbp.AgioFees.HasValue ? dfc : fc, bc, item.Font));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "", sbp.ReopenFees.HasValue ? dfc : fc, bc, item.Font));
                }
                lvMembers.Items.Add(item);
                _hasMember = true;
            }

            if (0 == lvMembers.Items.Count)
            {
                btnSave.Enabled = false;
                return;
            }

            if (_hasMember)
            {
                OCurrency zero = 0m;
                _itemTotal.Text = GetString("total");
                _itemTotal.SubItems.Add("");
                _itemTotal.SubItems.Add("");
                _itemTotal.SubItems.Add(zero.GetFormatedValue(true));
                lvMembers.Items.Add(_itemTotal);
            }
            lvMembers.SubItemClicked += lvMembers_SubItemClicked;
            lvMembers.SubItemEndEditing += lvMembers_SubItemEndEditing;
            lvMembers.DoubleClickActivation = true;
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
                if (string.IsNullOrEmpty(item.SubItems[idxInitialAmount].Text)) // Initial Amount
                {
                    item.SubItems[idxInitialAmount].Text = _product.InitialAmountMin.GetFormatedValue(_product.Currency.UseCents);
                    item.SubItems[idxInitialAmount].Tag = _product.InitialAmountMin;
                }
                if (string.IsNullOrEmpty(item.SubItems[idxCurrency].Text)) // Currency
                {
                    item.SubItems[idxCurrency].Text = _product.Currency.Code;
                    item.SubItems[idxCurrency].Tag = _product.Currency;
                }
                
                if (string.IsNullOrEmpty(item.SubItems[idxInterestRate].Text)) // Interest Rate
                {
                    item.SubItems[idxInterestRate].Text = _defaultInterestRate.ToString();
                    item.SubItems[idxInterestRate].Tag = _defaultInterestRate;
                }
                if (string.IsNullOrEmpty(item.SubItems[idxEntryFees].Text)) // Entry Fees
                {
                    item.SubItems[idxEntryFees].Text = _defaultEntryFees.GetFormatedValue(_product.Currency.UseCents);
                    item.SubItems[idxEntryFees].Tag = _defaultEntryFees;
                }
                if (_product is SavingsBookProduct)
                {
                    if (string.IsNullOrEmpty(item.SubItems[idxWithdrawFees].Text)) // Withdraw Fees
                    {
                        item.SubItems[idxWithdrawFees].Text = _defaultWithdrawFees.ToString();
                        item.SubItems[idxWithdrawFees].Tag = _defaultWithdrawFees;
                    }
                    if (string.IsNullOrEmpty(item.SubItems[idxTransferFees].Text)) // Transfer Fees
                    {
                        item.SubItems[idxTransferFees].Text = _defaultTransferFees.ToString();
                        item.SubItems[idxTransferFees].Tag = _defaultTransferFees;
                    }
                    if (string.IsNullOrEmpty(item.SubItems[idxIbtFees].Text))
                    {
                        item.SubItems[idxIbtFees].Text = _defaultIbtFees.ToString();
                        item.SubItems[idxIbtFees].Tag = _defaultIbtFees;
                    }
                    if (string.IsNullOrEmpty(item.SubItems[idxDepositFees].Text)) // Deposit Fees
                    {
                        item.SubItems[idxDepositFees].Text = _defaultDepositFees.ToString();
                        item.SubItems[idxDepositFees].Tag = _defaultDepositFees;
                    }
                    if (string.IsNullOrEmpty(item.SubItems[idxChequeDepositFees].Text)) // Cheque Deposit Fees
                    {
                        item.SubItems[idxChequeDepositFees].Text = _defaultChequeDepositFees.ToString();
                        item.SubItems[idxChequeDepositFees].Tag = _defaultChequeDepositFees;
                    }
                    if (string.IsNullOrEmpty(item.SubItems[idxCloseFees].Text)) // Close Fees
                    {
                        item.SubItems[idxCloseFees].Text = _defaultCloseFees.ToString();
                        item.SubItems[idxCloseFees].Tag = _defaultCloseFees;
                    }
                    if (string.IsNullOrEmpty(item.SubItems[idxManagementFees].Text)) // Management Fees
                    {
                        item.SubItems[idxManagementFees].Text = _defaultManagementFees.ToString();
                        item.SubItems[idxManagementFees].Tag = _defaultManagementFees;
                    }
                    if (string.IsNullOrEmpty(item.SubItems[idxOverdraftFees].Text)) // Overdraft Fees
                    {
                        item.SubItems[idxOverdraftFees].Text = _defaultOverdraftFees.ToString();
                        item.SubItems[idxOverdraftFees].Tag = _defaultOverdraftFees;
                    }
                    if (string.IsNullOrEmpty(item.SubItems[idxAgioFees].Text)) // Agio Fees
                    {
                        item.SubItems[idxAgioFees].Text = _defaultAgioFees.ToString();
                        item.SubItems[idxAgioFees].Tag = _defaultAgioFees;
                    }
                    if (string.IsNullOrEmpty(item.SubItems[idxReopenFees].Text)) // Reopen Fees
                    {
                        item.SubItems[idxReopenFees].Text = _defaultReopenFees.ToString();
                        item.SubItems[idxReopenFees].Tag = _defaultReopenFees;
                    }
                }
            }
            else
            {
                for (int i = 2; i < item.SubItems.Count; i++ )
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
            if (_product is SavingsBookProduct && e.Item != _itemTotal)
            {
                SavingsBookProduct sbp = (SavingsBookProduct) _product;
                if (e.Item.Checked)
                {
                    if (e.SubItem == idxInitialAmount) // Initial Amount
                    {
                        lvMembers.StartEditing(udInitialAmount, e.Item, e.SubItem);
                    }
                    else if (e.SubItem == idxInterestRate) // Interest Rate
                    {
                        if (!_product.InterestRate.HasValue)
                        {
                            lvMembers.StartEditing(udInterestRate, e.Item, e.SubItem);
                        }
                    }
                    else if (e.SubItem == idxEntryFees) // Entry Fees
                    {
                        if (!_product.EntryFees.HasValue)
                        {
                            lvMembers.StartEditing(udEntryFees, e.Item, e.SubItem);
                        }
                    }
                    else if (e.SubItem == idxWithdrawFees && _product is SavingsBookProduct) // Withdraw Fees
                    {
                        if (e.Item.SubItems[e.SubItem].ForeColor == Color.Black)
                        {
                            lvMembers.StartEditing(udWithdrawFees, e.Item, e.SubItem);
                        }
                    }
                    else if (e.SubItem == idxTransferFees && _product is SavingsBookProduct) // Transfer Fees
                    {
                        if (e.Item.SubItems[e.SubItem].ForeColor == Color.Black)
                        {
                            lvMembers.StartEditing(udTransferFees, e.Item, e.SubItem);
                        }
                    }
                    else if (idxIbtFees == e.SubItem && _product is SavingsBookProduct)
                    {
                        if (sbp.InterBranchTransferFee.IsRange)
                        {
                            lvMembers.StartEditing(nudIbtFees, e.Item, e.SubItem);
                        }
                    }
                    else if (e.SubItem == idxDepositFees && _product is SavingsBookProduct) // Deposit Fees
                    {
                        if (e.Item.SubItems[e.SubItem].ForeColor == Color.Black)
                        {
                            lvMembers.StartEditing(udDepositFees, e.Item, e.SubItem);
                        }
                    }
                    else if (e.SubItem == idxChequeDepositFees && _product is SavingsBookProduct) // Cheque Deposit Fees
                    {
                        if (e.Item.SubItems[e.SubItem].ForeColor == Color.Black)
                        {
                            lvMembers.StartEditing(udChequeDepositFees, e.Item, e.SubItem);
                        }
                    }
                    else if (e.SubItem == idxCloseFees && _product is SavingsBookProduct) // Close Fees
                    {
                        if (e.Item.SubItems[e.SubItem].ForeColor == Color.Black)
                        {
                            lvMembers.StartEditing(udCloseFees, e.Item, e.SubItem);
                        }
                    }
                    else if (e.SubItem == idxManagementFees && _product is SavingsBookProduct) // Management Fees
                    {
                        if (e.Item.SubItems[e.SubItem].ForeColor == Color.Black)
                        {
                            lvMembers.StartEditing(udManagementFees, e.Item, e.SubItem);
                        }
                    }
                    else if (e.SubItem == idxOverdraftFees && _product is SavingsBookProduct) // Overdraft Fees
                    {
                        if (e.Item.SubItems[e.SubItem].ForeColor == Color.Black)
                        {
                            lvMembers.StartEditing(udOverdraftFees, e.Item, e.SubItem);
                        }
                    }
                    else if (e.SubItem == idxAgioFees && _product is SavingsBookProduct) // Agio Fees
                    {
                        if (e.Item.SubItems[e.SubItem].ForeColor == Color.Black)
                        {
                            lvMembers.StartEditing(udAgioFees, e.Item, e.SubItem);
                        }
                    }
                    else if (e.SubItem == idxReopenFees && _product is SavingsBookProduct) // Reopen Fees
                    {
                        if (e.Item.SubItems[e.SubItem].ForeColor == Color.Black)
                        {
                            lvMembers.StartEditing(udReopenFees, e.Item, e.SubItem);
                        }
                    }
                }
            }
            else if (e.Item != _itemTotal)
            {
                if (e.SubItem == idxInitialAmount) // Initial Amount
                {
                    lvMembers.StartEditing(udInitialAmount, e.Item, e.SubItem);
                }
                else if (e.SubItem == idxInterestRate) // Interest Rate
                {
                    if (!_product.InterestRate.HasValue)
                    {
                        lvMembers.StartEditing(udInterestRate, e.Item, e.SubItem);
                    }
                }
                else if (e.SubItem == idxEntryFees) // Entry Fees
                {
                    if (!_product.EntryFees.HasValue)
                    {
                        lvMembers.StartEditing(udEntryFees, e.Item, e.SubItem);
                    }
                }
            }
        }

        private void lvMembers_SubItemEndEditing(object sender, SubItemEndEditingEventArgs e)
        {
            if (e.SubItem == idxInitialAmount) // Initial Amount
            {
                OCurrency temp;
                try
                {
                    temp = decimal.Parse(e.DisplayText);
                }
                catch
                {
                    temp = (OCurrency) e.Item.SubItems[e.SubItem].Tag;
                }
                temp = temp < _product.InitialAmountMin ? _product.InitialAmountMin : temp;
                temp = temp > _product.InitialAmountMax ? _product.InitialAmountMax : temp;
                e.DisplayText = temp.GetFormatedValue(_product.Currency.UseCents);
                e.Item.SubItems[e.SubItem].Tag = temp;
            }
            else if (e.SubItem == idxInterestRate) // Interest Rate
                e.Item.SubItems[e.SubItem].Tag = double.Parse(e.DisplayText);
            else if (e.SubItem == idxEntryFees) // Entry Fees
                e.Item.SubItems[e.SubItem].Tag = new OCurrency(decimal.Parse(e.DisplayText));
            else if (e.SubItem == idxWithdrawFees && _product is SavingsBookProduct) // Withdraw Fees
                e.Item.SubItems[e.SubItem].Tag = decimal.Parse(e.DisplayText);
            else if (e.SubItem == idxTransferFees && _product is SavingsBookProduct) // Transfer Fees
                e.Item.SubItems[e.SubItem].Tag = decimal.Parse(e.DisplayText);
            else if (idxIbtFees == e.SubItem && _product is SavingsBookProduct)
            {
                e.Item.SubItems[e.SubItem].Tag = Convert.ToDecimal(e.DisplayText);
            }
            else if (e.SubItem == idxDepositFees && _product is SavingsBookProduct) // Deposit Fees
                e.Item.SubItems[e.SubItem].Tag = decimal.Parse(e.DisplayText);
            else if (e.SubItem == idxChequeDepositFees && _product is SavingsBookProduct) // Cheque Deposit Fees
                e.Item.SubItems[e.SubItem].Tag = decimal.Parse(e.DisplayText);
            else if (e.SubItem == idxCloseFees && _product is SavingsBookProduct) // Close Fees
                e.Item.SubItems[e.SubItem].Tag = decimal.Parse(e.DisplayText);
            else if (e.SubItem == idxManagementFees && _product is SavingsBookProduct) // Management Fees
                e.Item.SubItems[e.SubItem].Tag = decimal.Parse(e.DisplayText);
            else if (e.SubItem == idxOverdraftFees && _product is SavingsBookProduct) // Overdraft Fees
                e.Item.SubItems[e.SubItem].Tag = decimal.Parse(e.DisplayText);
            else if (e.SubItem == idxAgioFees && _product is SavingsBookProduct) // Agio Fees
                e.Item.SubItems[e.SubItem].Tag = decimal.Parse(e.DisplayText);
            else if (e.SubItem == idxReopenFees && _product is SavingsBookProduct) // Reopen Fees
                e.Item.SubItems[e.SubItem].Tag = decimal.Parse(e.DisplayText);

            if (e.SubItem == idxInitialAmount) UpdateTotal();
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
                    blockItemCheck = true;
                }
            }
        }

        private void lvMembers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (blockItemCheck)
            {
                e.NewValue = e.CurrentValue;
                blockItemCheck = !blockItemCheck;
                return;
            }
        }

        private void UpdateTotal()
        {
            OCurrency total = 0m;
            bool _itemChecked = false;
            ExchangeRate customExchangeRate;

            foreach (ListViewItem item in lvMembers.Items)
            {
                if (!item.Checked) continue;
                if (item == _itemTotal) continue;

                _itemChecked = true;
                item.UseItemStyleForSubItems = false;

                customExchangeRate =
                        ServicesProvider.GetInstance().GetAccountingServices().FindLatestExchangeRate(
                            DateTime.Today, (Currency)item.SubItems[idxCurrency].Tag);

                total += customExchangeRate.Rate == 0
                                          ? 0
                                          : (OCurrency)item.SubItems[idxInitialAmount].Tag / customExchangeRate.Rate;
            }
            if (_itemChecked)
            {
                _itemTotal.SubItems[idxInitialAmount].Text = total.GetFormatedValue(ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().UseCents);
                _itemTotal.SubItems[idxCurrency].Text =
                    ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().Code;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!_nsgForm.Save()) return;

            bool isError = false;
            ISavingsContract saving = null;
            try
            {
                foreach (ListViewItem item in lvMembers.Items)
                {
                    if (item == _itemTotal) continue;
                    if (!item.Checked) continue;
                    var member = item.Tag as VillageMember;
                    IClient client = member.Tiers;

                    OCurrency initialAmount = (OCurrency)item.SubItems[idxInitialAmount].Tag;
                    double interestRate = (double)item.SubItems[idxInterestRate].Tag;
                    OCurrency entryFees = (OCurrency)item.SubItems[idxEntryFees].Tag;

                    if (_product is SavingsBookProduct)
                    {
                        SavingsBookProduct sbp = (SavingsBookProduct)_product;
                        decimal withdrawFees = (decimal)item.SubItems[idxWithdrawFees].Tag;
                        decimal transferFees = (decimal)item.SubItems[idxTransferFees].Tag;
                        decimal ibtFees = Convert.ToDecimal(item.SubItems[idxIbtFees].Tag);
                        decimal depositFees = (decimal)item.SubItems[idxDepositFees].Tag;
                        decimal chequeDepositFees = (decimal)item.SubItems[idxChequeDepositFees].Tag;
                        decimal closeFees = (decimal)item.SubItems[idxCloseFees].Tag;
                        decimal managementFees = (decimal)item.SubItems[idxManagementFees].Tag;
                        decimal overdraftFees = (decimal)item.SubItems[idxOverdraftFees].Tag;
                        decimal agioFees = (decimal)item.SubItems[idxAgioFees].Tag;
                        decimal reopenFees = (decimal)item.SubItems[idxReopenFees].Tag;

                        saving = new SavingBookContract(ServicesProvider.GetInstance().GetGeneralSettings(),
                                                        User.CurrentUser, TimeProvider.Today, sbp, client)
                                     {InterestRate = interestRate/100};
                        SavingBookContract s = (SavingBookContract)saving;

                        if (sbp.WithdrawFeesType == OSavingsFeesType.Flat)
                            s.FlatWithdrawFees = withdrawFees;
                        else
                            s.RateWithdrawFees = (double)withdrawFees / 100;

                        if (sbp.TransferFeesType == OSavingsFeesType.Flat)
                            s.FlatTransferFees = transferFees;
                        else
                            s.RateTransferFees = (double)transferFees / 100;

                        if (sbp.InterBranchTransferFee.IsFlat)
                        {
                            s.FlatInterBranchTransferFee = ibtFees;
                        }
                        else
                        {
                            s.RateInterBranchTransferFee = Convert.ToDouble(ibtFees);
                        }

                        s.DepositFees = depositFees;
                        s.ChequeDepositFees = chequeDepositFees;
                        s.CloseFees = closeFees;
                        s.ManagementFees = managementFees;
                        s.OverdraftFees = overdraftFees;
                        s.AgioFees = (double)agioFees / 100;
                        s.ReopenFees = reopenFees;
                    }
                    
                    saving.SavingsOfficer = _village.LoanOfficer;
                    saving.InitialAmount = initialAmount;
                    saving.EntryFees = entryFees;
                    saving.NsgID = _village.Id;
                    saving.Id = ServicesProvider.GetInstance().GetSavingServices().SaveContract(saving, (Client)member.Tiers);
                    saving = ServicesProvider.GetInstance().GetSavingServices().GetSaving(saving.Id);

                    ServicesProvider.GetInstance().GetSavingServices().FirstDeposit(saving, initialAmount, TimeProvider.Now, entryFees, 
                        User.CurrentUser, Teller.CurrentTeller);
                    ServicesProvider.GetInstance().GetSavingServices().UpdateInitialData(saving.Id, initialAmount, entryFees);
                    
                    saving = ServicesProvider.GetInstance().GetSavingServices().GetSaving(saving.Id);
                    ((Client)member.Tiers).AddSaving(saving);
                }
            }
            catch (Exception ex)
            {
                isError = true;
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            finally
            {
                InitializeControls();
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

        private void VillageAddSavingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}