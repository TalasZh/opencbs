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
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Services;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Shared;

namespace OpenCBS.GUI.UserControl
{
    public partial class SavingsListUserControl : System.Windows.Forms.UserControl
    {
        public event EventHandler AddSelectedSaving;
        public event EventHandler ViewSelectedSaving;
        ListViewItem totalItem = new ListViewItem("");
        //private bool _alreadyActiveCompulosrySavings;

        public bool ButtonAddSavingsEnabled
        {
            get { return buttonAddSaving.Enabled; }
            set { buttonAddSaving.Enabled = value; }
        }

        public OClientTypes ClientType {get; set;}

        public SavingsListUserControl()
        {
            InitializeComponent();
        }

        public void DisplaySavings(IEnumerable<ISavingsContract> pSavings)
        {
            OCurrency totalBalance = 0;
            OCurrency totalBalanceInPivot = 0;
            ExchangeRate latestExchangeRate;
            bool usedCents = false;
            
            string currencyCodeHolder = null; // to detect, if credits are in different currencies
            bool multiCurrency = false;

            lvSavings.Items.Clear();
            //_alreadyActiveCompulosrySavings = false;
            foreach (ISavingsContract saving in pSavings)
            {
                // it will be done for the first credit
                if (currencyCodeHolder == null) currencyCodeHolder = saving.Product.Currency.Code;

                //if not the first
                if (saving.Product.Currency.Code != currencyCodeHolder) multiCurrency = true;
                currencyCodeHolder = saving.Product.Currency.Code;

                //In case, if there are contracts in different currencies, total value of Balance is displayed in pivot currency
                
                latestExchangeRate =
                    ServicesProvider.GetInstance().GetAccountingServices().FindLatestExchangeRate(TimeProvider.Today,
                                                                                            saving.Product.Currency);
                string status = MultiLanguageStrings.GetString(Ressource.ClientForm, "Savings" + saving.Status + ".Text");
                ListViewItem item = new ListViewItem(saving.Code) { Tag = saving };
                item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.ClientForm, 
                    saving is SavingBookContract ? "SavingsBook.Text" : "CompulsorySavings.Text"));
                item.SubItems.Add(saving.Product.Name);
                item.SubItems.Add(saving.GetFmtBalance(false));
                item.SubItems.Add(saving.Product.Currency.Code);
                item.SubItems.Add(saving.CreationDate.ToShortDateString());
                item.SubItems.Add((saving.Events.Count != 0) ? saving.Events[saving.Events.Count - 1].Date.ToShortDateString() : "");
                item.SubItems.Add(status);
                item.SubItems.Add(saving.ClosedDate.HasValue ? saving.ClosedDate.Value.ToShortDateString() : "");
                
                totalBalance += saving.GetBalance();
                totalBalanceInPivot += latestExchangeRate.Rate == 0
                                           ? 0
                                           : saving.GetBalance()/latestExchangeRate.Rate;

                lvSavings.Items.Add(item);

                if (saving.Product.Currency.UseCents)
                    usedCents = saving.Product.Currency.UseCents;

                //if (saving is CompulsorySavings && saving.Status == OSavingsStatus.Active)
                //    _alreadyActiveCompulosrySavings = true;
            }

            totalItem.Font = new Font(totalItem.Font, FontStyle.Bold);
            totalItem.SubItems.Add("");
            totalItem.SubItems.Add("");
            
            if (multiCurrency)
            {
                totalItem.SubItems.Add(totalBalanceInPivot.GetFormatedValue(ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().UseCents && usedCents));
                totalItem.SubItems.Add(ServicesProvider.GetInstance().GetCurrencyServices().GetPivot().Code);
            }
            else
            {
                totalItem.SubItems.Add(totalBalance.GetFormatedValue(usedCents));
                totalItem.SubItems.Add(currencyCodeHolder);
            }

            lvSavings.Items.Add(totalItem);
        }

        private void buttonAddSaving_Click(object sender, EventArgs e)
        {
            FillDropDownMenuWithSavings(contextMenuStripSaving);
            contextMenuStripSaving.Show(buttonAddSaving, 0 - contextMenuStripSaving.Size.Width, 0);
        }

        private void FillDropDownMenuWithSavings(ContextMenuStrip pContextMenu)
	    {
            pContextMenu.Items.Clear();
            SavingProductServices products = ServicesProvider.GetInstance().GetSavingProductServices();
            List<ISavingProduct> datas = products.FindAllSavingsProducts(false, ClientType);
            foreach (ISavingProduct savingProduct in datas)
            {
                var item = new ToolStripMenuItem(savingProduct.Name) { Tag = savingProduct };
                item.Click += item_Click;
                //if (_alreadyActiveCompulosrySavings && savingProduct is CompulsorySavingsProduct)
                //    item.Enabled = false;
                pContextMenu.Items.Add(item);
            }
	    }

        private void item_Click(object sender, EventArgs e)
        {
            if (AddSelectedSaving != null)
                AddSelectedSaving(((ToolStripMenuItem)sender).Tag, e);
        }

        private void listViewSavings_DoubleClick(object sender, EventArgs e)
        {
            if (lvSavings.SelectedItems.Count > 0 && ViewSelectedSaving != null && lvSavings.SelectedItems[0] != totalItem)
                ViewSelectedSaving(lvSavings.SelectedItems[0].Tag, e);
        }

        private void buttonViewSaving_Click(object sender, EventArgs e)
        {
            if (lvSavings.SelectedItems.Count > 0 && ViewSelectedSaving != null)
                ViewSelectedSaving(lvSavings.SelectedItems[0].Tag, e);
        }
        
    }
}
