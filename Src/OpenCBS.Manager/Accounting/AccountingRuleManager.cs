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
using System.Linq;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Accounting;
using System.Data.SqlClient;
using OpenCBS.CoreDomain.EconomicActivities;
using OpenCBS.CoreDomain.Events;
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Products;
using OpenCBS.Manager.Currencies;
using OpenCBS.Manager.Products;
using OpenCBS.CoreDomain.FundingLines;

namespace OpenCBS.Manager.Accounting
{
    public class AccountingRuleManager : Manager
    {
        private AccountManager _accountManager;
        private LoanProductManager _loanProductManager;
        private SavingProductManager _savingProductManager;
        private EconomicActivityManager _economicActivityManager;
        private FundingLineManager _fundingLineManager;
        private CurrencyManager _currencyManager;
        private PaymentMethodManager _paymentMethodManager;

        public AccountingRuleManager(User pUser) : base(pUser) 
        {
            _accountManager = new AccountManager(pUser);
            _loanProductManager = new LoanProductManager(pUser);
            _savingProductManager = new SavingProductManager(pUser);
            _economicActivityManager = new EconomicActivityManager(pUser);
            _fundingLineManager = new FundingLineManager(pUser);
            _currencyManager = new CurrencyManager(pUser);
            _paymentMethodManager = new PaymentMethodManager(pUser);
        }

        public AccountingRuleManager(string pTestDB) : base(pTestDB) 
        {
            _accountManager = new AccountManager(pTestDB);
            _loanProductManager = new LoanProductManager(pTestDB);
            _savingProductManager = new SavingProductManager(pTestDB);
            _economicActivityManager = new EconomicActivityManager(pTestDB);
            _fundingLineManager = new FundingLineManager(pTestDB);
            _currencyManager = new CurrencyManager(pTestDB);
        }

        public int AddAccountingRule(IAccountingRule pRule)
        {
            const string sqlText = @"INSERT INTO [AccountingRules] (
                                       [debit_account_number_id], 
                                       [credit_account_number_id], 
                                       [rule_type], 
                                       [booking_direction],
                                       [event_type],
                                       [event_attribute_id],
                                       [order],
                                       [description])
                                    VALUES (@debit_account_number_id, 
                                            @credit_account_number_id, 
                                            @rule_type, 
                                            @booking_direction,
                                            @event_type,
                                            @event_attribute_id,
                                            @order,
                                            @description)
                                    SELECT SCOPE_IDENTITY()";
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand insert = new OpenCbsCommand(sqlText, conn))
                {
                    insert.AddParam("@debit_account_number_id",  pRule.DebitAccount.Id);
                    insert.AddParam("@credit_account_number_id", pRule.CreditAccount.Id);
                    insert.AddParam("@rule_type", pRule is ContractAccountingRule ? 'C' : 'F');
                    insert.AddParam("@booking_direction", (int)pRule.BookingDirection);

                    insert.AddParam("@event_type", pRule.EventType.EventCode);
                    insert.AddParam("@event_attribute_id", pRule.EventAttribute.Id);
                    insert.AddParam("@order", pRule.Order);
                    insert.AddParam("@description", pRule.Description);
                    pRule.Id = Convert.ToInt32(insert.ExecuteScalar());
                }
            }

            if (pRule is ContractAccountingRule)
                AddContractAccountingRule(pRule as ContractAccountingRule);
            
            return pRule.Id;
        }

        private void AddContractAccountingRule(ContractAccountingRule rule)
        {
            const string sqlText = @"INSERT INTO 
                                       [ContractAccountingRules] 
                                         ([id], 
                                          [product_type], 
                                          [loan_product_id], 
                                          [savings_product_id], 
                                          [client_type], 
                                          [activity_id],
                                          [currency_id])
                                     VALUES 
                                        (@id, 
                                         @productType, 
                                         @loanProductId, 
                                         @savingsProductId, 
                                         @clientType, 
                                         @activityId,
                                         @currency_id)";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, conn))
                {
                    SetAccountingRule(cmd, rule);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAccountingRule(IAccountingRule rule)
        {
            const string sqlText = @"UPDATE [AccountingRules]
                                     SET [debit_account_number_id] = @debit_account_number_id,
                                         [credit_account_number_id] = @credit_account_number_id,
                                         [booking_direction] = @booking_direction,
                                         [event_type] = @event_type,
                                         [event_attribute_id] = @event_attribute_id,
                                         [order] = @order,
                                         [description] = @description
                                     WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, conn))
                {
                    cmd.AddParam("@id", rule.Id);
                    cmd.AddParam("@debit_account_number_id", rule.DebitAccount.Id);
                    cmd.AddParam("@credit_account_number_id", rule.CreditAccount.Id);
                    cmd.AddParam("@event_type", rule.EventType.EventCode);
                    cmd.AddParam("@event_attribute_id", rule.EventAttribute.Id);
                    cmd.AddParam("@booking_direction", (int)rule.BookingDirection);
                    cmd.AddParam("@order", rule.Order);
                    cmd.AddParam("@description", rule.Description);
                    cmd.ExecuteNonQuery();
                }
            }

            if (rule is ContractAccountingRule)
                UpdateContractAccountingRule(rule as ContractAccountingRule);
        }

        private void UpdateContractAccountingRule(ContractAccountingRule rule)
        {
            const string sqlText = @"UPDATE [ContractAccountingRules]
                                     SET [product_type] = @productType,
                                         [loan_product_id] = @loanProductId,
                                         [savings_product_id] = @savingsProductId,
                                         [client_type] = @clientType,
                                         [activity_id] = @activityId,
                                         [currency_id] = @currency_id
                                     WHERE id = @id";
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand cmd = new OpenCbsCommand(sqlText, conn))
                {
                    SetAccountingRule(cmd, rule);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAccountingRule(IAccountingRule pRule)
        {
            const string sqlText = @"UPDATE [AccountingRules]
                                     SET [deleted] = 1
                                     WHERE id = @id";

            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand delete = new OpenCbsCommand(sqlText, conn))
                {
                    delete.AddParam("@id", pRule.Id);
                    delete.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAllAccountingRules()
        {
            const string sqlText1 = @"DELETE FROM [ContractAccountingRules]";
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand delete = new OpenCbsCommand(sqlText1, conn))
                {
                    delete.ExecuteNonQuery();
                }

                const string sqlText2 = @"DELETE FROM [AccountingRules]";

                using (OpenCbsCommand delete = new OpenCbsCommand(sqlText2, conn))
                {
                    delete.ExecuteNonQuery();
                }
            }
        }

        public IAccountingRule Select(int pId)
        {
            const string sqlText = @"SELECT rule_type
                                     FROM [AccountingRules]
                                     WHERE deleted = 0 
                                     AND id = @id";

            IAccountingRule rule = new ContractAccountingRule();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@id", pId);

                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return null;

                        reader.Read();
                        if (reader.GetChar("rule_type") == 'C')
                            rule = new ContractAccountingRule {Id = pId};
                    }
                }
            }

            if (rule is ContractAccountingRule)
                rule = SelectContractAccountingRule(rule.Id);

            List<Account> accounts = _accountManager.SelectAllAccounts();
            rule.DebitAccount = accounts.FirstOrDefault(item => item.Id == rule.DebitAccount.Id);
            rule.CreditAccount = accounts.FirstOrDefault(item => item.Id == rule.CreditAccount.Id);
           
            return rule;
        }

        private ContractAccountingRule SelectContractAccountingRule(int pId)
        {
            const string sqlText = @"SELECT AccountingRules.id,
                                            AccountingRules.debit_account_number_id, 
                                            AccountingRules.credit_account_number_id,
                                            AccountingRules.booking_direction,
                                            AccountingRules.event_type,
                                            AccountingRules.event_attribute_id,
                                            AccountingRules.[order],
                                            AccountingRules.[description] AS rule_description,
                                            EventAttributes.name AS attribute_name,
                                            EventTypes.description AS event_description,
                                            ContractAccountingRules.product_type, 
                                            ContractAccountingRules.loan_product_id, 
                                            ContractAccountingRules.savings_product_id, 
                                            ContractAccountingRules.client_type, 
                                            ContractAccountingRules.activity_id,
                                            ContractAccountingRules.currency_id
                                    FROM AccountingRules
                                    INNER JOIN EventAttributes ON EventAttributes.id = AccountingRules.event_attribute_id
                                    INNER JOIN EventTypes ON AccountingRules.event_type = EventTypes.event_type
                                    LEFT JOIN ContractAccountingRules ON AccountingRules.id = ContractAccountingRules.id                                    
                                    WHERE AccountingRules.id = @id";

            ContractAccountingRule rule;
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@id", pId);

                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return null;

                        reader.Read();
                        rule = GetContractAccountingRule(reader);
                    }
                }
            }

            if (rule.LoanProduct != null)
                rule.LoanProduct = _loanProductManager.Select(rule.LoanProduct.Id);

            if (rule.Currency != null)
                rule.Currency = _currencyManager.SelectCurrencyById(rule.Currency.Id);
            if (rule.Currency == null)
                rule.Currency = null;


            if (rule.SavingProduct != null)
                rule.SavingProduct = _savingProductManager.SelectSavingProduct(rule.SavingProduct.Id);

            if (rule.EconomicActivity != null)
                rule.EconomicActivity = _economicActivityManager.SelectEconomicActivity(rule.EconomicActivity.Id);

            if (rule.PaymentMethod.Id != 0)
                rule.PaymentMethod = _paymentMethodManager.SelectPaymentMethodById(rule.PaymentMethod.Id);

            return rule;
        }

        private FundingLineAccountingRule SelectFundingLineAccountingRule(int pId)
        {
            const string sqlText = @"SELECT AccountingRules.id,
                                            AccountingRules.debit_account_number_id, 
                                            AccountingRules.credit_account_number_id,
                                            AccountingRules.booking_direction,
                                            FundingLineAccountingRules.funding_line_id
                                    FROM AccountingRules
                                    INNER JOIN FundingLineAccountingRules ON AccountingRules.id = FundingLineAccountingRules.id
                                    WHERE AccountingRules.id = @id";
            
            FundingLineAccountingRule rule;
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@id", pId);

                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return null;

                        reader.Read();
                        rule = _getFundingLineAccountingRule(reader);
                    }
                }
            }

            if (rule.FundingLine != null)
                rule.FundingLine = _fundingLineManager.SelectFundingLineById(rule.FundingLine.Id, false);

            return rule;
        }

        public AccountingRuleCollection SelectAll()
        {
            const string sqlText = @"SELECT id, rule_type
                                     FROM [AccountingRules] 
                                     WHERE deleted = 0";

            AccountingRuleCollection rules = new AccountingRuleCollection();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return rules;

                        while (reader.Read())
                        {
                            if (reader.GetChar("rule_type") == 'C')
                                rules.Add(new ContractAccountingRule { Id = reader.GetInt("id") });

                        }
                    }
                }
            }

            List<Account> accounts = _accountManager.SelectAllAccounts();
            for (int i = 0; i < rules.Count; i++)
            {

                if (rules[i] is ContractAccountingRule)
                {
                    rules[i] = SelectContractAccountingRule(rules[i].Id);
                }
                else
                {
                    rules[i] = SelectFundingLineAccountingRule(rules[i].Id);
                }

                rules[i].DebitAccount = accounts.FirstOrDefault(item => item.Id == rules[i].DebitAccount.Id);
                rules[i].CreditAccount = accounts.FirstOrDefault(item => item.Id == rules[i].CreditAccount.Id);
            }

            return rules;
        }

        public DataSet GetRuleCollectionDataset()
        {
            const string sqlText = @"SELECT 
                                      event_type, 
                                      event_attribute_id, 
                                      debit_account_number_id, 
                                      credit_account_number_id, 
                                      [order],       
                                      [description],
                                      product_type,
                                      loan_product_id, 
                                      savings_product_id,
                                      client_type, 
                                      activity_id,
                                      currency_id                                      
                                    FROM dbo.AccountingRules ar
                                    INNER JOIN dbo.ContractAccountingRules cr ON ar.id = cr.id
                                    WHERE deleted = 0";
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand select = new SqlCommand(sqlText, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(select))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        return dataSet;
                    }
                }
            }
        }

        public AccountingRuleCollection SelectAllByEventType(string type)
        {
            const string sqlText = @"SELECT id, rule_type
                                     FROM [AccountingRules] 
                                     WHERE deleted = 0
                                       AND (event_type = @event_type OR @event_type = '')";

            AccountingRuleCollection rules = new AccountingRuleCollection();
            using (SqlConnection conn = GetConnection())
            {
                using (OpenCbsCommand select = new OpenCbsCommand(sqlText, conn))
                {
                    select.AddParam("@event_type", type);

                    using (OpenCbsReader reader = select.ExecuteReader())
                    {
                        if (reader.Empty) return rules;

                        while (reader.Read())
                        {
                            if (reader.GetChar("rule_type") == 'C')
                                rules.Add(new ContractAccountingRule { Id = reader.GetInt("id") });

                        }
                    }
                }
            }

            List<Account> accounts = _accountManager.SelectAllAccounts();
            for (int i = 0; i < rules.Count; i++)
            {

                if (rules[i] is ContractAccountingRule)
                {
                    rules[i] = SelectContractAccountingRule(rules[i].Id);
                }
                else
                {
                    rules[i] = SelectFundingLineAccountingRule(rules[i].Id);
                }

                rules[i].DebitAccount = accounts.FirstOrDefault(item => item.Id == rules[i].DebitAccount.Id);
                rules[i].CreditAccount = accounts.FirstOrDefault(item => item.Id == rules[i].CreditAccount.Id);
            }

            return rules;
        }

        private void SetAccountingRule(OpenCbsCommand cmd, ContractAccountingRule rule)
        {
            cmd.AddParam("@id", rule.Id);
            cmd.AddParam("@productType", (int)rule.ProductType);
            
            if (rule.LoanProduct != null)
                cmd.AddParam("@loanProductId", rule.LoanProduct.Id);
            else
                cmd.AddParam("@loanProductId", null);

            if (rule.Currency != null)
                cmd.AddParam("@currency_id", rule.Currency.Id);
            else
                cmd.AddParam("@currency_id", null);

            if (rule.SavingProduct != null)
                cmd.AddParam("@savingsProductId", rule.SavingProduct.Id);
            else
                cmd.AddParam("@savingsProductId", null);

            if (rule.ClientType == OClientTypes.Corporate)
                cmd.AddParam("@clientType", 'C');
            else if (rule.ClientType == OClientTypes.Group)
                cmd.AddParam("@clientType", 'G');
            else if (rule.ClientType == OClientTypes.Person)
                cmd.AddParam("@clientType", 'I');
            else if (rule.ClientType == OClientTypes.Village)
                cmd.AddParam("@clientType", 'V');
            else
                cmd.AddParam("@clientType", '-');

            if (rule.EconomicActivity != null)
                cmd.AddParam("@activityId", rule.EconomicActivity.Id);
            else
                cmd.AddParam("@activityId", null);
        }

        private static ContractAccountingRule GetContractAccountingRule(OpenCbsReader reader)
        {
            ContractAccountingRule rule = new ContractAccountingRule();

            rule.Id = reader.GetInt("id");
            rule.EventType = new EventType
                                 {
                                     EventCode = reader.GetString("event_type"),
                                     Description = reader.GetString("event_description")
                                 };
            rule.EventAttribute = new EventAttribute
                                      {
                                          Id = reader.GetInt("event_attribute_id"),
                                          Name = reader.GetString("attribute_name")
                                      };
            rule.DebitAccount = new Account { Id = reader.GetInt("debit_account_number_id") };
            rule.CreditAccount = new Account { Id = reader.GetInt("credit_account_number_id") };
            rule.BookingDirection = (OBookingDirections)reader.GetSmallInt("booking_direction");
            rule.Order = reader.GetInt("order");
            rule.Description = reader.GetString("rule_description");

            rule.ProductType = (OProductTypes)reader.GetSmallInt("product_type");

            int? loanProductId = reader.GetNullInt("loan_product_id");
            if (loanProductId.HasValue)
                rule.LoanProduct = new LoanProduct { Id = loanProductId.Value };

            int? currencyId = reader.GetNullInt("currency_id");
            if (currencyId.HasValue)
                rule.Currency = new Currency { Id = currencyId.Value };

            int? savingsProductId = reader.GetNullInt("savings_product_id");
            if (savingsProductId.HasValue)
                rule.SavingProduct = new SavingsBookProduct { Id = savingsProductId.Value };

            rule.ClientType = reader.GetChar("client_type").ConvertToClientType();

            int? activityId = reader.GetNullInt("activity_id");

            if (activityId.HasValue)
                rule.EconomicActivity = new EconomicActivity { Id = activityId.Value };

            return rule;
        }

        private FundingLineAccountingRule _getFundingLineAccountingRule(OpenCbsReader reader)
        {
            FundingLineAccountingRule rule = new FundingLineAccountingRule();

            rule.Id = reader.GetInt("id");
            rule.DebitAccount = new Account { Id = reader.GetInt("debit_account_number_id") };
            rule.CreditAccount = new Account { Id = reader.GetInt("credit_account_number_id") };
            rule.BookingDirection = (OBookingDirections)reader.GetSmallInt("booking_direction");
            
            int? fundingLineId = reader.GetInt("funding_line_id");

            if (fundingLineId.HasValue)
                rule.FundingLine = new FundingLine { Id = fundingLineId.Value };

            return rule;
        }
    }
}
