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

namespace OpenCBS.Enums
{
    public static class OAccounts
    {
        public static readonly string CASH = "1011";
        public static readonly string CASH_CREDIT = "2031";
        public static readonly string RESCHEDULED_LOANS = "2032";
        public static readonly string ACCRUED_INTERESTS_LOANS = "2037";
        public static readonly string ACCRUED_INTERESTS_RESCHEDULED_LOANS = "2038";
        public static readonly string BAD_LOANS = "2911";
        public static readonly string UNRECO_BAD_LOANS = "2921";
        public static readonly string INTERESTS_ON_PAST_DUE_LOANS = "2971";
        public static readonly string PENALTIES_ON_PAST_DUE_LOANS_ASSET = "2972";
        public static readonly string LOAN_LOSS_RESERVE = "2991";
        public static readonly string DEFERRED_INCOME = "3882";
        public static readonly string PROVISIONS_ON_BAD_LOANS = "6712";
        public static readonly string LOAN_LOSS = "6751";
        public static readonly string INTERESTS_ON_CASH_CREDIT = "7021";
        public static readonly string INTERESTS_ON_RESCHEDULED_LOANS = "7022";
        public static readonly string PENALTIES_ON_PAST_DUE_LOANS_INCOME = "7027";
        public static readonly string INTERESTS_ON_BAD_LOANS = "7028";
        public static readonly string COMMISSIONS = "7029";
        public static readonly string PROVISION_WRITE_OFF = "7712";
        public static readonly string LIABILITIES_LOAN_LOSS_CURRENT = "5211";
        public static readonly string EXPENSES_LOAN_LOSS_CURRENT = "6731";
        public static readonly string INCOME_LOAN_LOSS_CURRENT = "7731";
        public static readonly string ACCOUNTS_AND_TERM_LOANS = "1322";
        public static readonly string SAVINGS = "221";
        public static readonly string ACCOUNT_PAYABLE_INTERESTS_ON_SAVINGS_BOOKS = "2261";
        public static readonly string INTERESTS_ON_DEPOSIT_ACCOUNT = "60132";
        public static readonly string RECOVERY_OF_CHARGED_OFF_ASSETS = "42802";
        public static readonly string NON_BALANCE_COMMITTED_FUNDS = "9330201";
        public static readonly string NON_BALANCE_VALIDATED_LOANS = "9330202";
        public static readonly string TERM_DEPOSIT = "222";
        public static readonly string COMPULSORY_SAVINGS = "223";
        public static readonly string ACCOUNT_PAYABLE_INTERESTS_ON_TERM_DEPOSIT = "2262";
        public static readonly string ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS = "2263";
    }
}
