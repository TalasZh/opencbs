using System;
using System.Collections.Generic;
using System.Linq;
using Octopus.CoreDomain.EconomicActivities;
using Octopus.CoreDomain.Events;
using Octopus.GUI.UserControl;
using Octopus.Services;
using Octopus.CoreDomain.Accounting;
using Octopus.Enums;
using Octopus.MultiLanguageRessources;
using Octopus.CoreDomain.Products;
using Octopus.GUI.Tools;

namespace Octopus.GUI.Accounting
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
