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
using System.Linq;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.Events;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.Enums;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.CoreDomain.Products;
using OpenCBS.GUI.Tools;

namespace OpenCBS.GUI.Accounting
{
    public partial class FrmAddContractAccountingRule : SweetBaseForm
    {
        public ContractAccountingRule AccountingRule
        {
            get { return GetAccountingRule(); }
            set { SetAccountingRule(value); }
        }
        
        public FrmAddContractAccountingRule()
        {
            InitializeComponent();
            Initialize();
        }

        public FrmAddContractAccountingRule(int order)
        {
            MaxOrder = order + 1;
            InitializeComponent();
            Initialize();
        }

        private int _id;
        private int MaxOrder{ get; set;}

        

        private void InitializeComboboxEventAttribute()
        {
            //Event attribute field init
            cmbEventAttribute.ValueMember = "Id";
            cmbEventAttribute.DisplayMember = "";
            List<EventAttribute> eventAttributes = ServicesProvider.GetInstance().GetEventProcessorServices().
                SelectEventAttributes(
                ((EventType) cmbEventCode.SelectedItem).EventCode);
            foreach (EventAttribute ea in eventAttributes)
            {
                ea.Name = GetString("EventAttributes", ea.Name);
            }
            cmbEventAttribute.DataSource = eventAttributes.OrderBy(item => item.Name).ToList();
        }

        private void Initialize()
        {
            //Event type field init
            cmbEventCode.ValueMember = "EventCode";
            cmbEventCode.DisplayMember = "";
            List<EventType> eventTypes = ServicesProvider.GetInstance().GetEventProcessorServices().SelectEventTypesForAccounting();
            foreach (EventType et in eventTypes)
            {
                et.Description = GetString("EventTypes", et.EventCode);
            }
            cmbEventCode.DataSource = eventTypes.OrderBy(item => item.EventCode).ToList();
            cmbEventCode.SelectedIndex = 0;

            InitializeComboboxEventAttribute();

            cmbDebitAccount.ValueMember = "Number";
            cmbDebitAccount.DisplayMember = "";
            cmbDebitAccount.DataSource = ServicesProvider.GetInstance().GetChartOfAccountsServices().FindAllAccounts().OrderBy(item => item.Number).ToList();
            
            cmbCreditAccount.ValueMember = "Number";
            cmbCreditAccount.DisplayMember = "";
            cmbCreditAccount.DataSource = ServicesProvider.GetInstance().GetChartOfAccountsServices().FindAllAccounts().OrderBy(item => item.Number).ToList();

            cmbCurrency.Items.Add(
                new Currency() { Id = 0, Name = MultiLanguageStrings.GetString(Ressource.AccountingRule, "All.Text") });
            foreach (Currency currency in ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies())
            {
                cmbCurrency.Items.Add(currency);
            } 
            cmbCurrency.SelectedIndex = 0;

            cbmEntryDirection.DataBind(typeof(OBookingDirections), Ressource.AccountingRule, true);
            cmbProductType.DataBind(typeof(OProductTypes), Ressource.AccountingRule, true);
            cmbClientType.DataBind(typeof(OClientTypes), Ressource.AccountingRule, true);
            tbOrder.Text = MaxOrder.ToString();

            InitializeComboboxActivity();
        }

        private void SetAccountingRule(ContractAccountingRule rule)
        {
            _id = rule.Id;
            cmbEventCode.SelectedValue = rule.EventType.EventCode;
            cmbEventAttribute.SelectedValue = rule.EventAttribute.Id;
            cmbDebitAccount.SelectedValue = rule.DebitAccount.Number;
            cmbCreditAccount.SelectedValue = rule.CreditAccount.Number;
            cbmEntryDirection.SelectedValue = rule.BookingDirection.ToString();
            cmbProductType.SelectedValue = rule.ProductType.ToString();
            cmbClientType.SelectedValue = rule.ClientType.ToString();
            tbOrder.Text = rule.Order.ToString();
            cmbCurrency.SelectedItem = rule.Currency;
            tbDescription.Text = rule.Description;
            
            switch (rule.ProductType)
            {
                case OProductTypes.All: cmbProduct.SelectedIndex = 0; break;
                case OProductTypes.Loan:
                    if (rule.LoanProduct != null)
                    {
                        var selectedItem = cmbProduct.Items.OfType<LoanProduct>().FirstOrDefault(item => item.Id == rule.LoanProduct.Id);
                        cmbProduct.SelectedItem = selectedItem;
                    }
                    else
                        cmbProduct.SelectedIndex = 0;
                    break;

                case OProductTypes.Saving:
                    if (rule.SavingProduct != null)
                    {
                        var selectedItem = cmbProduct.Items.OfType<ISavingProduct>().FirstOrDefault(item => item.Id == rule.SavingProduct.Id);
                        cmbProduct.SelectedItem = selectedItem;
                    }
                    else
                        cmbProduct.SelectedIndex = 0;
                    break;
            }

            if (rule.EconomicActivity != null)
            {
                var selectedItem = cmbEconomicActivity.Items.OfType<EconomicActivity>().FirstOrDefault(item => item.Id == rule.EconomicActivity.Id);
                cmbEconomicActivity.SelectedItem = selectedItem;
            }
            else
                cmbEconomicActivity.SelectedIndex = 0;
       }

        private ContractAccountingRule GetAccountingRule()
        {
            ContractAccountingRule rule = new ContractAccountingRule
                                              {
                                                  Id = _id,
                                                  EventType = cmbEventCode.SelectedItem as EventType,
                                                  EventAttribute = cmbEventAttribute.SelectedItem as EventAttribute,
                                                  DebitAccount = cmbDebitAccount.SelectedItem as Account,
                                                  CreditAccount = cmbCreditAccount.SelectedItem as Account,
                                                  ProductType =
                                                      (OProductTypes)
                                                      Enum.Parse(typeof (OProductTypes),
                                                                 cmbProductType.SelectedValue.ToString()),
                                                  ClientType =
                                                      (OClientTypes)
                                                      Enum.Parse(typeof (OClientTypes),
                                                                 cmbClientType.SelectedValue.ToString()),
                                                  //BookingDirection =
                                                  //    (OBookingDirections)
                                                  //    Enum.Parse(typeof (OBookingDirections),
                                                  //               cbmEntryDirection.SelectedValue.ToString()),
                                                  Order = Convert.ToInt32(tbOrder.Text),
                                                  Currency = cmbCurrency.SelectedItem as Currency,
                                                  Description = tbDescription.Text
                                              };

            switch (rule.ProductType)
            {
                case OProductTypes.Loan:
                    if (cmbProduct.SelectedItem is LoanProduct)
                        rule.LoanProduct = cmbProduct.SelectedItem as LoanProduct;
                    break;

                case OProductTypes.Saving:
                    if (cmbProduct.SelectedItem is ISavingProduct)
                        rule.SavingProduct = cmbProduct.SelectedItem as ISavingProduct;
                    break;
            }

            if (cmbEconomicActivity.SelectedItem is EconomicActivity)
                rule.EconomicActivity = cmbEconomicActivity.SelectedItem as EconomicActivity;

            return rule;
        }

        private void IntializeComboboxProduct()
        {
            cmbProduct.Items.Clear();
            cmbProduct.ValueMember = "Id";
            cmbProduct.DisplayMember = "Name";
            cmbProduct.Items.Add(
                new {Id = 0, Name = MultiLanguageStrings.GetString(Ressource.AccountingRule, "All.Text")});
            cmbProduct.SelectedIndex = 0;
            OProductTypes productType =
                (OProductTypes) Enum.Parse(typeof (OProductTypes), cmbProductType.SelectedValue.ToString());
            cmbProduct.Enabled = true;
            switch (productType)
            {
                case OProductTypes.Loan:
                    foreach(LoanProduct product in ServicesProvider.GetInstance().GetProductServices().FindAllPackages(false, OClientTypes.All))
                    {
                        cmbProduct.Items.Add(product);
                    }
                    break;

                case OProductTypes.Saving:
                    foreach (ISavingProduct product in ServicesProvider.GetInstance().GetSavingProductServices().FindAllSavingsProducts(false, OClientTypes.All))
                    {
                        cmbProduct.Items.Add(product);
                    }
                    break;
                default:
                    cmbProduct.Enabled = false; break;
            }
        }

        private void InitializeComboboxActivity()
        {
            cmbEconomicActivity.Items.Clear();
            cmbEconomicActivity.ValueMember = "Id";
            cmbEconomicActivity.DisplayMember = "Name";
            cmbEconomicActivity.Items.Add(
                new {Id = 0, Name = MultiLanguageStrings.GetString(Ressource.AccountingRule, "All.Text")});
            cmbEconomicActivity.SelectedIndex = 0;

            OClientTypes clientType = (OClientTypes)Enum.Parse(typeof(OClientTypes), cmbClientType.SelectedValue.ToString());
            if (clientType == OClientTypes.Person || clientType == OClientTypes.Corporate)
            {
                cmbEconomicActivity.Enabled = true; 
                foreach (EconomicActivity activity in ServicesProvider.GetInstance().GetEconomicActivityServices().FindAllEconomicActivities())
                {
                    cmbEconomicActivity.Items.Add(activity);
                }
            }
            else
                cmbEconomicActivity.Enabled = false;
        }

        private void comboBoxProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntializeComboboxProduct();
        }

        private void comboBoxClientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeComboboxActivity();
        }

        private void cbEventType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeComboboxEventAttribute();
        }
    }
}
