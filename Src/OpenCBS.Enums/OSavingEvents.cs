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

namespace OpenCBS.Enums
{
    [Serializable]
    public static class OSavingEvents
    {
        public const string InitialDeposit = "SVIE"; // SaVing Initial Event
        public const string Deposit = "SVDE"; // SaVing Deposit Event
        public const string PendingDeposit = "SPDE"; // Saving Pending Deposit Event
        public const string Withdraw = "SVWE"; // SaVing Withdraw Event
        public const string Accrual = "SIAE"; // Saving Interest Accrual Event
        public const string Posting = "SIPE"; // Saving Interest Posting Event
        public const string Close = "SVCE"; // SaVing Closing Event
        public const string DebitTransfer = "SDTE"; // Saving Debit Transfer Event
        public const string CreditTransfer = "SCTE"; // Saving Credit Transfer Event
        public const string ManagementFee = "SMFE"; // Saving Management Fee Event
        public const string SavingClosure = "SCLE"; // Saving CLosure Event
        public const string OverdraftFees = "SOFE"; // Saving Overdraft Fees Event
        public const string Agio = "SVAE"; // SaVing Agio Event
        public const string Reopen = "SVRE"; // SaVing Reopen Event
        public const string PendingDepositRefused = "SPDR"; // Savings Pending Deposit Refused event
        public const string SpecialOperationCredit = "SOCE"; // Savings Special Operation Credit
        public const string SpecialOperationDebit = "SODE"; // Savings Special Operation Debit
        public const string InterBranchDebitTransfer = "SDIT";
        public const string InterBranchCreditTransfer = "SCIT";
        public const string LoanDisbursement = "SVLD"; // SaVing Loan Disbursement
        public const string SavingLoanRepayment = "SRLE"; //Loan Repayment from Saving account Event
        public const string BlockCompulsarySavings = "SBCS"; //Savings Block Compulsory Savings
        public const string UnblockCompulsorySavings = "SUCS"; //Savings Unblock Compusory Savings
    }
}
