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

using System.Collections.Generic;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Accounting
{
    public static class DefaultAccounts
    {
        public static List<Account> DefaultAccount(int pCurrencyId)
        {
            List<Account> accounts = new List<Account>
            {
                new Account("1011", "Cash", 0, "CASH", true,OAccountCategories.BalanceSheetAsset, pCurrencyId),
                new Account("2031", "Cash Credit", 0,"CASH_CREDIT", true,OAccountCategories.BalanceSheetAsset,pCurrencyId),
                new Account("2032", "Rescheduled Loans", 0, "RESCHEDULED_LOANS",true, OAccountCategories.BalanceSheetAsset,pCurrencyId),
                new Account("2037", "Accrued interests receivable", 0,"ACCRUED_INTERESTS_LOANS", true,OAccountCategories.BalanceSheetAsset,pCurrencyId),
                new Account("2038", "Accrued interests on rescheduled loans", 0,"ACCRUED_INTERESTS_RESCHEDULED_LOANS", true,OAccountCategories.BalanceSheetAsset,pCurrencyId),
                new Account("2911", "Bad Loans", 0, "BAD_LOANS", true,OAccountCategories.BalanceSheetAsset,pCurrencyId),
                new Account("2921", "Unrecoverable Bad Loans", 0,"UNRECO_BAD_LOANS", true,OAccountCategories.BalanceSheetAsset,pCurrencyId),
                new Account("2971", "Interest on Past Due Loans", 0,"INTERESTS_ON_PAST_DUE_LOANS", true, OAccountCategories.BalanceSheetAsset,pCurrencyId),
                new Account("2972", "Penalties on Past Due Loans", 0, "PENALTIES_ON_PAST_DUE_LOANS_ASSET", true, OAccountCategories.BalanceSheetAsset,pCurrencyId),
                new Account("2991", "Loan Loss Reserve", 0, "LOAN_LOSS_RESERVE",false, OAccountCategories.BalanceSheetLiabilities,pCurrencyId),
                new Account("3882", "Deferred Income", 0, "DEFERRED_INCOME", false,OAccountCategories.BalanceSheetLiabilities,pCurrencyId),
                new Account("5211", "Loan Loss Allowance on Current Loans", 0,"LIABILITIES_LOAN_LOSS_CURRENT", false,OAccountCategories.BalanceSheetLiabilities,pCurrencyId),
                new Account("6712", "Provision on bas loans", 0,"PROVISION_ON_BAD_LOANS", true,OAccountCategories.ProfitAndLossExpense,pCurrencyId),
                new Account("6731", "Loan Loss Allowance on Current Loans", 0,"EXPENSES_LOAN_LOSS_CURRENT", true,OAccountCategories.ProfitAndLossExpense,pCurrencyId),
                new Account("6751", "Loan Loss", 0, "LOAN_LOSS", true,OAccountCategories.ProfitAndLossExpense,pCurrencyId),
                new Account("7712", "Provision write-off", 0,"PROVISION_WRITE_OFF", false,OAccountCategories.ProfitAndLossExpense,pCurrencyId),
                new Account("7021", "Interests on cash credit", 0,"INTERESTS_ON_CASH_CREDIT", false,OAccountCategories.ProfitAndLossIncome,pCurrencyId),
                new Account("7022", "Interests on rescheduled loans", 0,"INTERESTS_ON_RESCHEDULED_LOANS", false,OAccountCategories.ProfitAndLossIncome,pCurrencyId),
                new Account("7027", "Penalties on past due loans",0, "PENALTIES_ON_PAST_DUE_LOANS_INCOME", false,OAccountCategories.ProfitAndLossIncome,pCurrencyId),
                new Account("7028", "Interests on bad loans", 0,"INTERESTS_ON_BAD_LOANS", false,OAccountCategories.ProfitAndLossIncome,pCurrencyId),
                new Account("7029", "Commissions", 0, "COMMISSIONS", false,OAccountCategories.ProfitAndLossIncome,pCurrencyId),
                new Account("7731", "Resumption of Loan Loss Allowance on Current Loan", 0,"INCOME_LOAN_LOSS_CURRENT", false,OAccountCategories.ProfitAndLossIncome,pCurrencyId),
                new Account("1322", "Accounts and Terms Loans", 0,"ACCOUNTS_AND_TERM_LOANS", false,OAccountCategories.BalanceSheetLiabilities,pCurrencyId),
                new Account("221", "Savings", 0, "SAVINGS", false, OAccountCategories.BalanceSheetLiabilities, pCurrencyId),
                new Account("2261", "Account payable interests on Savings Books", 0, "ACCOUNT_PAYABLE_INTERESTS_ON_SAVINGS_BOOKS", false, OAccountCategories.ProfitAndLossIncome, pCurrencyId),
                new Account("60132", "Interests on deposit account", 0, "INTERESTS_ON_DEPOSIT_ACCOUNT", true, OAccountCategories.ProfitAndLossIncome, pCurrencyId),
                new Account("42802", "RECOVERY OF CHARGED OFF ASSETS", 0, "RECOVERY_OF_CHARGED_OFF_ASSETS", false, OAccountCategories.ProfitAndLossIncome, pCurrencyId),
                new Account("9330201", "NON_BALANCE_COMMITTED_FUNDS", 0, "NON_BALANCE_COMMITTED_FUNDS", false, OAccountCategories.ProfitAndLossIncome, pCurrencyId),
                new Account("9330202", "NON_BALANCE_VALIDATED_LOANS", 0, "NON_BALANCE_VALIDATED_LOANS", true, OAccountCategories.BalanceSheetAsset, pCurrencyId),
                new Account("222", "Term Deposit", 0, "TERM_DEPOSIT", false, OAccountCategories.BalanceSheetLiabilities, pCurrencyId),
                new Account("223", "Compulsory Savings", 0, "COMPULSORY_SAVINGS", false, OAccountCategories.BalanceSheetLiabilities, pCurrencyId),
                new Account("2262", "Account payable interests on Term Deposit", 0, "ACCOUNT_PAYABLE_INTERESTS_ON_TERM_DEPOSIT", false, OAccountCategories.ProfitAndLossIncome, pCurrencyId),
                new Account("2263", "Account payable interests on Compulsory Savings", 0, "ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS", false, OAccountCategories.ProfitAndLossIncome, pCurrencyId)
            };
            return accounts;
        }
    }
}
