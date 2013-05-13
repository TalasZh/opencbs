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
using System.Drawing;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;
using OpenCBS.Shared;
using OpenCBS.Enums;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.CoreDomain.Contracts.Savings;
using System.Linq;
using System.Collections.Generic;

namespace OpenCBS.GUI.Contracts
{
    public partial class FastDepositForm : SweetBaseForm
    {
        private Village _village;
        private bool _blockItemCheck;
        private const int idxAmount = 3;
        private const int idxCurrency = 4;
        private ListViewItem _itemTotal = new ListViewItem("");
        private bool _hasMember = false;

        public FastDepositForm(Village pVillage)
        {
            InitializeComponent();
            _village = pVillage;
            LoadSavings();
        }

        private void LoadSavings()
        {
            foreach (var member in _village.Members)
            {
                //List<ISavingsContract> savings = member.Tiers.Savings.Where(item => item.Status != OSavingsStatus.Closed).ToList();
                List<ISavingsContract> savings = ServicesProvider.GetInstance().GetSavingServices().GetSavingsByClientId(member.Tiers.Id);

                if (savings.Count > 0)
                {
                    _hasMember = true;
                    ListViewGroup group = new ListViewGroup(member.Tiers.Name);
                    group.Tag = member.Tiers;
                    lvContracts.Groups.Add(group);

                    foreach (var saving in savings.Where(item => item.Status != OSavingsStatus.Closed))
                    {
                        ListViewItem item = new ListViewItem(saving.Code) { Tag = saving };
                        item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.ClientForm, 
                            saving is SavingBookContract ? "SavingsBook.Text" : "CompulsorySavings.Text"));
                        item.SubItems.Add(saving.GetFmtBalance(false));
                        item.SubItems.Add("");
                        item.SubItems.Add("");
                        item.Group = group;
                        lvContracts.Items.Add(item);
                    }
                }
            }
            if (_hasMember)
            {
                OCurrency zero = 0m;
                ListViewGroup totalgroup = new ListViewGroup(GetString("total"));
                totalgroup.Tag = "Total";
                lvContracts.Groups.Add(totalgroup);
                _itemTotal.SubItems.Add("");
                _itemTotal.SubItems.Add("");
                _itemTotal.SubItems.Add(zero.GetFormatedValue(false));
                _itemTotal.SubItems.Add("");
                _itemTotal.Group = totalgroup;
                lvContracts.Items.Add(_itemTotal);
            }
        }

        private void UpdateTotal()
        {
            OCurrency total = 0m;
            ExchangeRate customExchangeRate;

            foreach (ListViewItem item in lvContracts.Items)
            {
                if (!item.Checked) continue;
                if (item == _itemTotal) continue;
                item.UseItemStyleForSubItems = false;

                customExchangeRate =
                        ServicesProvider.GetInstance().GetAccountingServices().FindLatestExchangeRate(
                            DateTime.Today, (Currency)item.SubItems[idxCurrency].Tag);

                total += customExchangeRate.Rate == 0
                                          ? 0
                                          : (OCurrency)item.SubItems[idxAmount].Tag / customExchangeRate.Rate;
            }
            _itemTotal.SubItems[idxAmount].Text = total.GetFormatedValue(ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().UseCents);
            _itemTotal.SubItems[idxCurrency].Text = ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().Code;
        }

        private void lvContracts_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button)
                if (e.Clicks >= 2)
                    _blockItemCheck = true;
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
                subitem.Font = item.Font;
            if (item != _itemTotal)
            {
                if (item.Checked)
                {
                    item.SubItems[idxAmount].Text =
                        ((ISavingsContract) e.Item.Tag).Product.DepositMin.GetFormatedValue(
                            ((ISavingsContract) e.Item.Tag).Product.Currency.UseCents);
                    item.SubItems[idxAmount].Tag = ((ISavingsContract) e.Item.Tag).Product.DepositMin;
                    item.SubItems[idxCurrency].Text = ((ISavingsContract) e.Item.Tag).Product.Currency.Code;
                    item.SubItems[idxCurrency].Tag = ((ISavingsContract) e.Item.Tag).Product.Currency;
                }
                else
                {
                    item.SubItems[idxAmount].Text = string.Empty;
                    item.SubItems[idxAmount].Tag = null;
                    item.SubItems[idxCurrency].Text = string.Empty;
                    item.SubItems[idxCurrency].Tag = null;
                }
            }
            else
            {
                foreach (ListViewItem i in item.ListView.Items)
                {
                    i.Checked = item.Checked;
                }
            }
            UpdateTotal();
        }

        private void lvContracts_SubItemClicked(object sender, SubItemEventArgs e)
        {
            if (e.Item.Checked && e.Item != _itemTotal)
            {
                if (idxAmount == e.SubItem)
                {
                    udAmount.ResetText();
                    if (((ISavingsContract)e.Item.Tag).Product.Currency.UseCents)
                        udAmount.DecimalPlaces = 2;
                    else
                        udAmount.DecimalPlaces = 0;
                    udAmount.Minimum = ((ISavingsContract)e.Item.Tag).Product.DepositMin.Value;
                    udAmount.Maximum = ((ISavingsContract)e.Item.Tag).Product.DepositMax.Value;
                    lvContracts.StartEditing(udAmount, e.Item, e.SubItem);
                }
            }
        }

        private void lvContracts_SubItemEndEditing(object sender, SubItemEndEditingEventArgs e)
        {
            if (idxAmount == e.SubItem)
            {
                OCurrency temp;
                try
                {
                    temp = decimal.Parse(e.DisplayText);
                }
                catch
                {
                    temp = (OCurrency)e.Item.SubItems[e.SubItem].Tag;
                }
                temp = temp < ((ISavingsContract)e.Item.Tag).Product.DepositMin ? ((ISavingsContract)e.Item.Tag).Product.DepositMin : temp;
                temp = temp > ((ISavingsContract)e.Item.Tag).Product.DepositMax ? ((ISavingsContract)e.Item.Tag).Product.DepositMax : temp;

                e.DisplayText = temp.GetFormatedValue(((ISavingsContract)e.Item.Tag).Product.Currency.UseCents);
                e.Item.SubItems[e.SubItem].Tag = temp;
            }
            UpdateTotal();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {        
            foreach (ListViewItem item in lvContracts.Items)
            {
                if (!item.Checked) continue;
                if (item == _itemTotal) continue;

                ISavingsContract saving = item.Tag as ISavingsContract;
                OCurrency amount = (OCurrency)item.SubItems[idxAmount].Tag;
                

//                ServicesProvider.GetInstance().GetSavingServices().Deposit(saving, TimeProvider.Today, amount, 
//                    "", User.CurrentUser, false, OPaymentMethods.Cash, null, null);
                ServicesProvider.GetInstance().GetSavingServices().Deposit(saving, TimeProvider.Now, amount, 
                    "", User.CurrentUser, false, OSavingsMethods.Cash, null, null);
            }
        }
    }
}
