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
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.Events;
using OpenCBS.CoreDomain.Products;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;
using OpenCBS.ExceptionsHandler;
using OpenCBS.MultiLanguageRessources;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Online;
using OpenCBS.Services.Events;
using OpenCBS.Shared;

namespace OpenCBS.GUI.Accounting
{
    public partial class FrmAccountingRules : SweetBaseForm
    {
        public FrmAccountingRules()
        {
            InitializeComponent();
            _sorter = new ListViewSorter();
            listViewContractsRules.ListViewItemSorter = _sorter;
            GetEventTypes();
            Initialize();
        }

        private int _maxOrder;
        private readonly ListViewSorter _sorter;
        private ContractAccountingRule _rule;

        private void Initialize()
        {
            string eventType = cbEventTypes.SelectedItem.ToString() == "All" ? "" : ((EventType) cbEventTypes.SelectedItem).EventCode;
            AccountingRuleCollection rules = ServicesProvider.GetInstance().GetAccountingRuleServices().SelectAllByEventType(eventType);
            listViewContractsRules.Items.Clear();

            foreach (ContractAccountingRule rule in rules.GetContractAccountingRules())
            {
                listViewContractsRules.Items.Add(GetListViewItem(rule));
                if (_maxOrder < rule.Order)
                    _maxOrder = rule.Order;
            }
            listViewContractsRules.Sorting = SortOrder.Descending;
            _sorter.ByColumn = 4;
            _sorter.reset = true;
            listViewContractsRules.Sort();
        }

        private void GetEventTypes()
        {
            EventProcessorServices s = ServicesProvider.GetInstance().GetEventProcessorServices();
            List<EventType> eventTypes = s.SelectEventTypesForAccounting();
            eventTypes.Sort((x, y) => x.EventCode.CompareTo(y.EventCode));
            cbEventTypes.Items.Add("All");
            foreach (EventType t in eventTypes)
            {
                t.Description = GetString("EventTypes", t.EventCode);
                cbEventTypes.Items.Add(t);
            }
            cbEventTypes.SelectedIndex = 0;
        }

        private ListViewItem GetListViewItem(ContractAccountingRule rule)
        {
            ListViewItem item = new ListViewItem(rule.EventType.ToString());

            item.SubItems.Add(GetString("EventAttributes", rule.EventAttribute.ToString()));
            item.SubItems.Add(rule.DebitAccount.ToString());
            item.SubItems.Add(rule.CreditAccount.ToString());
            item.SubItems.Add(rule.Order.ToString());
            item.SubItems.Add(rule.Description);
            item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.AccountingRule, rule.ProductType + @".Text") ??
                              rule.ProductType.ToString());
            
            switch (rule.ProductType)
            {
                case OProductTypes.All:
                    item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.AccountingRule, @"All.Text")); break;
                case OProductTypes.Loan:
                    item.SubItems.Add(rule.LoanProduct != null
                                          ? rule.LoanProduct.Name
                                          : MultiLanguageStrings.GetString(Ressource.AccountingRule, @"All.Text"));
                    break;
                case OProductTypes.Saving:
                    item.SubItems.Add(rule.SavingProduct != null
                                          ? rule.SavingProduct.Name
                                          : MultiLanguageStrings.GetString(Ressource.AccountingRule, @"All.Text"));
                    break;
            }

            item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.AccountingRule, rule.ClientType + @".Text") ??
                              rule.ClientType.ToString());

            item.SubItems.Add(rule.EconomicActivity != null
                                  ? rule.EconomicActivity.Name
                                  : MultiLanguageStrings.GetString(Ressource.AccountingRule, @"All.Text"));
            
            item.SubItems.Add(rule.Currency != null
                                  ? rule.Currency.Name
                                  : MultiLanguageStrings.GetString(Ressource.AccountingRule, @"All.Text"));
            item.SubItems.Add(rule.PaymentMethod.Name);
            item.Tag = rule;

            return item;
        }

        private void AddRule()
        {
            FrmAddContractAccountingRule frmAddAccountingRule = new FrmAddContractAccountingRule(_maxOrder);
            
            while (frmAddAccountingRule.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    int id =
                        ServicesProvider.GetInstance().GetAccountingRuleServices().SaveAccountingRule(frmAddAccountingRule.AccountingRule);

                    listViewContractsRules.Items.Add(
                        GetListViewItem(
                            ServicesProvider.GetInstance().GetAccountingRuleServices().Select(id) as
                            ContractAccountingRule));
                    CoreDomainProvider.GetInstance().GetChartOfAccounts().AccountingRuleCollection =
                        ServicesProvider.GetInstance().GetAccountingRuleServices().SelectAll();
                    _maxOrder = frmAddAccountingRule.AccountingRule.Order;
                    break;
                }
                catch (Exception ex)
                {
                    frmAddAccountingRule.Show(this);
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                    frmAddAccountingRule.Hide();
                }
            }
        }

        private void EditRule()
        {

            if (listViewContractsRules.SelectedItems.Count > 0)
            {
                ContractAccountingRule rule = listViewContractsRules.SelectedItems[0].Tag as ContractAccountingRule;
                FrmAddContractAccountingRule frmEditAccountingRule = new FrmAddContractAccountingRule {AccountingRule = rule};
                while (frmEditAccountingRule.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ServicesProvider.GetInstance().GetAccountingRuleServices().UpdateAccountingRule(
                            frmEditAccountingRule.AccountingRule);
                        var editedRule =
                            ServicesProvider.GetInstance().GetAccountingRuleServices().Select(rule.Id) as
                            ContractAccountingRule;

                        int index = listViewContractsRules.Items.IndexOf(listViewContractsRules.SelectedItems[0]);
                        listViewContractsRules.Items[index] = GetListViewItem(editedRule);
                        CoreDomainProvider.GetInstance().GetChartOfAccounts().AccountingRuleCollection =
                            ServicesProvider.GetInstance().GetAccountingRuleServices().SelectAll();

                        break;
                    }
                    catch (Exception ex)
                    {
                        frmEditAccountingRule.Show(this);
                        new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                        frmEditAccountingRule.Hide();
                    }
                }
            }
        }

        private void DeleteRule()
        {
            if (listViewContractsRules.SelectedItems.Count > 0)
            {
                ContractAccountingRule rule = listViewContractsRules.SelectedItems[0].Tag as ContractAccountingRule;
                if (MessageBox.Show(MultiLanguageStrings.GetString(Ressource.AccountingRule, "DeleteRuleMessage.Text"),
                                    MultiLanguageStrings.GetString(Ressource.AccountingRule, "DeleteRuleTitle.Text"),
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        ServicesProvider.GetInstance().GetAccountingRuleServices().DeleteAccountingRule(rule);
                        listViewContractsRules.Items.Remove(listViewContractsRules.SelectedItems[0]);
                        CoreDomainProvider.GetInstance().GetChartOfAccounts().AccountingRuleCollection =
                            ServicesProvider.GetInstance().GetAccountingRuleServices().SelectAll();
                    }
                    catch (Exception ex)
                    {
                        new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                    }
                }
            }
        }

        private void ButtonEditRuleClick(object sender, EventArgs e)
        {
            EditRule();
        }

        private void ButtonAddRuleClick(object sender, EventArgs e)
        {
            AddRule();
        }

        private void ListViewAccountingRulesDoubleClick(object sender, EventArgs e)
        {
            EditRule();
        }

        private void ButtonDeleteRuleClick(object sender, EventArgs e)
        {
            DeleteRule();
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void CbEventTypesSelectedValueChanged(object sender, EventArgs e)
        {
            Initialize();
        }

        private void ListViewContractsRulesColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewContractsRules.Columns[_sorter.LastSort].ImageIndex = -1;
            if (_sorter.LastSort == e.Column)
            {
                if (listViewContractsRules.Sorting == SortOrder.Ascending)
                    listViewContractsRules.Sorting = SortOrder.Descending;
                else
                    listViewContractsRules.Sorting = SortOrder.Ascending;
            }
            else
            {
                listViewContractsRules.Sorting = SortOrder.Descending;
            }
            _sorter.ByColumn = e.Column;
            _sorter.reset = true;

            if (listViewContractsRules.Items.Count > 0)
                listViewContractsRules.Columns[_sorter.ByColumn].ImageIndex = listViewContractsRules.Sorting ==
                                                                              SortOrder.Ascending
                                                                                  ? 0
                                                                                  : 1;

            listViewContractsRules.Sort();
        }

        private void CopyStripMenuItem1Click(object sender, EventArgs e)
        {
            _rule = (ContractAccountingRule)listViewContractsRules.SelectedItems[0].Tag;
        }

        private void PasetToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                _rule.Order = _maxOrder + 1;
                int id = ServicesProvider.GetInstance().GetAccountingRuleServices().SaveAccountingRule(_rule);

                listViewContractsRules.Items.Add(
                    GetListViewItem(
                        ServicesProvider.GetInstance().GetAccountingRuleServices().Select(id) as
                        ContractAccountingRule));
                CoreDomainProvider.GetInstance().GetChartOfAccounts().AccountingRuleCollection =
                    ServicesProvider.GetInstance().GetAccountingRuleServices().SelectAll();
                _maxOrder = _rule.Order;
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
        }

        private void DeleteToolStripMenuItemClick(object sender, EventArgs e)
        {
            DeleteRule();
        }

        private void Export()
        {
            DataSet dataSet = ServicesProvider.GetInstance().GetAccountingRuleServices().GetRuleCollectionDataset();
            ExportFile.SaveToFile(dataSet, string.Empty, @",");
        }

        private void SbtnExportClick(object sender, EventArgs e)
        {
            Export();
        }

        private void Import()
        {
            fileDialog.InitialDirectory = Application.CommonAppDataPath;
            fileDialog.Filter = @"CSV (*.csv)|*.csv|All files (*.*)|*.*";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ServicesProvider.GetInstance().GetAccountingRuleServices().DeleteAllAccountingRules();
                    listViewContractsRules.Items.Clear();

                    using (StreamReader sr = new StreamReader(fileDialog.FileName, Encoding.Unicode))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] columns = line.Trim().Split(',');

                            ContractAccountingRule rule = new ContractAccountingRule
                                                              {
                                                                  EventType = new EventType(columns[0]),
                                                                  EventAttribute =
                                                                      new EventAttribute
                                                                          {Id = Convert.ToInt32(columns[1])},
                                                                  Order = Convert.ToInt32(columns[4]),
                                                                  Description = columns[5],
                                                                  ProductType =
                                                                      (OProductTypes)
                                                                      Enum.Parse(typeof (OProductTypes), columns[6]),
                                                                  LoanProduct =
                                                                      columns[7].Trim() == ""
                                                                          ? null
                                                                          : new LoanProduct
                                                                                {Id = Convert.ToInt32(columns[7])},
                                                                  SavingProduct =
                                                                      columns[8].Trim() == ""
                                                                          ? null
                                                                          : new SavingsBookProduct
                                                                                {Id = Convert.ToInt32(columns[8])},
                                                                  ClientType =
                                                                      columns[9].Trim() == "-"
                                                                          ? OClientTypes.All
                                                                          : columns[9].ConvertToClientType(),
                                                                  EconomicActivity =
                                                                      columns[10].Trim() == ""
                                                                          ? null
                                                                          : new EconomicActivity
                                                                                {Id = Convert.ToInt32(columns[10])},
                                                                  Currency =
                                                                      columns[11].Trim() == ""
                                                                          ? null
                                                                          : new Currency
                                                                                {Id = Convert.ToInt32(columns[11])},
                                                                  DebitAccount =
                                                                      ServicesProvider.GetInstance().
                                                                      GetChartOfAccountsServices().SelectAccountById(
                                                                          Convert.ToInt32(columns[2])),
                                                                  CreditAccount =
                                                                      ServicesProvider.GetInstance().
                                                                      GetChartOfAccountsServices().SelectAccountById(
                                                                          Convert.ToInt32(columns[3])),
                                                              };


                            int id = ServicesProvider.GetInstance().GetAccountingRuleServices().SaveAccountingRule(rule);

                            listViewContractsRules.Items.Add(
                                GetListViewItem(ServicesProvider.GetInstance().GetAccountingRuleServices().Select(id) as
                                    ContractAccountingRule));
                        }
                    }
                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                }
            }
        }

        private void SweetButton1Click(object sender, EventArgs e)
        {
            Import();
        }
    }
}
