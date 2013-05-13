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
using System.Collections;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Events;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.StockAccountBalance
{
    [Serializable]
    public static class Accrual
    {
        public static OCurrency GetStockBalance(Loan pCredit, ApplicationSettings settings, ProvisionTable pProvisionningTable, string pAccountNumber)
        {
            OCurrency balance = 0;

            return balance;
        }

        private static OCurrency AccruedInterestsRescheduleLoanBalance(Loan pCredit)
        {
            if (!pCredit.Disbursed || !pCredit.Rescheduled) return 0;
            if (pCredit.GetPastDueDays(TimeProvider.Today) != 0) return 0;

            foreach (Installment installment in pCredit.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    DateTime date = installment.Number == 1
                                        ? pCredit.StartDate
                                        : pCredit.GetInstallment(installment.Number - 2).ExpectedDate;
                    int days = (TimeProvider.Today - date).Days;
                    OCurrency accruedInterest =installment.InterestsRepayment * (double)days /(double) DateTime.DaysInMonth(date.Year, date.Month);
                    accruedInterest -= installment.PaidInterests;

                    return accruedInterest > 0 ? accruedInterest : 0;
                }
            }
            return 0;
        }

        private static OCurrency _AccruedInterestsLoanBalance(Loan pCredit)
        {
            if (!pCredit.Disbursed || pCredit.Rescheduled) return 0;
            if (pCredit.GetPastDueDays(TimeProvider.Today) != 0) return 0;

            foreach (Installment installment in pCredit.InstallmentList)
            {
                if(!installment.IsRepaid)
                {
                    DateTime date = installment.Number == 1
                                        ? pCredit.StartDate
                                        : pCredit.GetInstallment(installment.Number - 2).ExpectedDate;
                    int days = (TimeProvider.Today - date).Days;
                    OCurrency accruedInterest = installment.InterestsRepayment * (double)days / (double)DateTime.DaysInMonth(date.Year, date.Month);
                    accruedInterest -= installment.PaidInterests;

                    return accruedInterest > 0 ? accruedInterest : 0;
                }
            }
            return 0;
        }

        private static OCurrency _DeferredIncomeBalance(Loan pCredit)
        {
            if (!pCredit.Disbursed || pCredit.GetPastDueDays(TimeProvider.Today) != 0) return 0;

            OCurrency repaidAmount = 0;
            foreach (Installment installment in pCredit.InstallmentList)
            {
                if (TimeProvider.Today >= installment.ExpectedDate) continue;
                repaidAmount += installment.PaidInterests;
            }

            foreach (Installment installment in pCredit.InstallmentList)
            {
                if (TimeProvider.Today >= installment.ExpectedDate) continue;
                DateTime date = installment.Number == 1
                                    ? pCredit.StartDate
                                    : pCredit.GetInstallment(installment.Number - 2).ExpectedDate;
                int days = (TimeProvider.Today - date).Days;
                OCurrency accruedInterest = installment.InterestsRepayment * (double)days / (double)DateTime.DaysInMonth(date.Year, date.Month);
                repaidAmount -= accruedInterest;
                break;
            }
            return repaidAmount > 0 ? repaidAmount : 0;
        }

        private static OCurrency _LoanLossBalance(Loan pCredit, int writeOffDays)
        {
            if (pCredit.WrittenOff || pCredit.GetPastDueDays(TimeProvider.Today) > writeOffDays) return pCredit.GetOlb();
            return 0;
        }

        private static OCurrency _LoanLossReserveBalance(Loan pCredit, ProvisionTable pProvisionningTable, int writeOffDays)
        {
            OCurrency theoricBalance = 0;
            if (!pCredit.Disbursed) return theoricBalance;

            if (pCredit.GetPastDueDays(TimeProvider.Today) == 0) return theoricBalance;
            if (pCredit.GetPastDueDays(TimeProvider.Today) > writeOffDays) return theoricBalance;
            
            foreach (Installment installment in pCredit.InstallmentList)
            {
                if (installment.ExpectedDate >= TimeProvider.Today)
                    break;
                theoricBalance += (installment.InterestsRepayment - installment.PaidInterests);
            }

            theoricBalance += pCredit.GetUnpaidLatePenalties(TimeProvider.Today);
            
            int pastDueDays = pCredit.GetPastDueDays(TimeProvider.Today);
            theoricBalance += pCredit.GetOlb() * Convert.ToDecimal(pProvisionningTable.GetProvisiningRateByNbOfDays(pastDueDays).Rate);

            OCurrency realBalance = 0;
            return theoricBalance > realBalance ? theoricBalance : realBalance;
        }

        private static OCurrency _InterestsPastDueLoansBalance(Loan pCredit, int writeOffDays)
        {
            OCurrency balance = 0;
            if (!pCredit.Disbursed) return balance;

            if (pCredit.GetPastDueDays(TimeProvider.Today) == 0) return balance;
            if (pCredit.GetPastDueDays(TimeProvider.Today) > writeOffDays) return balance;
            foreach (Installment installment in pCredit.InstallmentList)
            {
                if (installment.ExpectedDate > TimeProvider.Today)
                    break;
                balance += (installment.InterestsRepayment - installment.PaidInterests);
            }

            return balance;
        }

        private static OCurrency _PenaltiesPastDueLoansBalanceAsset(Loan pCredit, int writeOffDays)
        {
            OCurrency balance = 0;
            if (!pCredit.Disbursed) return balance;

            if (pCredit.GetPastDueDays(TimeProvider.Today) == 0) return balance;
            if (pCredit.GetPastDueDays(TimeProvider.Today) > writeOffDays) return balance; 

            balance += pCredit.GetUnpaidLatePenalties(TimeProvider.Today);
            return balance;
        }

        private static OCurrency _InterestOnCashCreditBalance(Loan pCredit)
        {
            if (pCredit.Rescheduled || !pCredit.Disbursed) return 0;
            
            OCurrency balance = pCredit.GetPaidInterest();
            foreach (BadLoanRepaymentEvent e in pCredit.Events.GetEventsByType(typeof(BadLoanRepaymentEvent)))
            {
                balance -= e.Interests;
            }

            if (pCredit.GetPastDueDays(TimeProvider.Today) != 0) return balance;
            foreach (Installment installment in pCredit.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    DateTime date = installment.Number == 1
                                        ? pCredit.StartDate
                                        : pCredit.GetInstallment(installment.Number - 2).ExpectedDate;
                    int days = (TimeProvider.Today - date).Days;
                    OCurrency accruedInterest = installment.InterestsRepayment * (double)days / (double)DateTime.DaysInMonth(date.Year, date.Month);
                    accruedInterest -= installment.PaidInterests;

                    balance += accruedInterest > 0 ? accruedInterest : 0;
                    break;
                }
            }
            balance -= _DeferredIncomeBalance(pCredit);
            return balance;
        }

        private static OCurrency _UnrecoverableBadLoanBalance(Loan pCredit, int badLoanDays, int writeOffDays)
        {
            int pastDueDays = pCredit.GetPastDueDays(TimeProvider.Today);
            if (pastDueDays > badLoanDays && pastDueDays <= writeOffDays)
                return pCredit.GetOlb();
            return 0;
        }

        private static OCurrency _InterestOnRescheduledloanBalance(Loan pCredit)
        {
            if (!pCredit.Rescheduled) return 0;

            OCurrency balance = 0;
            balance += pCredit.GetPaidInterest();
            foreach (BadLoanRepaymentEvent e in pCredit.Events.GetEventsByType(typeof(BadLoanRepaymentEvent)))
            {
                balance -= e.Interests;
            }

            if (pCredit.GetPastDueDays(TimeProvider.Today) != 0) return balance;
            foreach (Installment installment in pCredit.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    DateTime date = installment.Number == 1
                                        ? pCredit.StartDate
                                        : pCredit.GetInstallment(installment.Number - 2).ExpectedDate;
                    int days = (TimeProvider.Today - date).Days;
                    OCurrency accruedInterest = installment.InterestsRepayment * (double)days / (double)DateTime.DaysInMonth(date.Year, date.Month);
                    accruedInterest -= installment.PaidInterests;

                    balance += accruedInterest > 0 ? accruedInterest : 0;
                    break;
                }
            }
            return balance;
        }

        private static OCurrency _InterestOnBadloanBalance(Loan pCredit)
        {
            OCurrency balance = 0;

            if (!pCredit.Disbursed) return 0;

            if (pCredit.GetPastDueDays(TimeProvider.Today) > 0)
            {
                foreach (Installment installment in pCredit.InstallmentList)
                {
                    if (installment.ExpectedDate > TimeProvider.Today)
                        break;
                    balance += (installment.InterestsRepayment - installment.PaidInterests);
                }
            }

            OCurrency realBalance = 0;
            return balance > realBalance ? balance : realBalance;
        }

        private static OCurrency _CommissionsBalance(Loan pCredit)
        {
            return pCredit.GetPaidCommissions();
        }

        private static OCurrency _ProvisionWriteOffBalance(Loan pCredit, int writeOffDays)
        {
            OCurrency balance = 0;
            if (pCredit.WrittenOff || pCredit.GetPastDueDays(TimeProvider.Today) > writeOffDays) return pCredit.GetOlb();

            foreach (BadLoanRepaymentEvent e in pCredit.Events.GetEventsByType(typeof(BadLoanRepaymentEvent)))
            {
                balance += e.AccruedProvision;
            }
            return balance;
        }

        private static OCurrency _LiabilitiesLoanLossBalance(Loan pCredit, ProvisionTable pProvisionningTable)
        {
            if (pCredit.Disbursed && !pCredit.BadLoan)
                return pCredit.GetPastDueDays(TimeProvider.Today) == 0
                           ? pCredit.GetOlb()* Convert.ToDecimal(pProvisionningTable.GetProvisiningRateByNbOfDays(0).Rate)
                           : 0;
            return 0;
        }

        private static OCurrency _ExpensesLoanLossBalance(Loan pCredit, ProvisionTable pProvisionningTable)
        {
            return pCredit.Disbursed && !pCredit.AllInstallmentsRepaid && !pCredit.WrittenOff
                       ? pCredit.Amount*Convert.ToDecimal(pProvisionningTable.GetProvisiningRateByNbOfDays(0).Rate)
                       : 0;
        }

        private static OCurrency _IncomeLoanLossBalance(Loan pCredit, ProvisionTable pProvisionningTable)
        {
            OCurrency olb = pCredit.GetOlb();
            OCurrency rate = Convert.ToDecimal(pProvisionningTable.GetProvisiningRateByNbOfDays(0).Rate);
            if (pCredit.GetPastDueDays(TimeProvider.Today) != 0) olb = 0;
            if (pCredit.WrittenOff) return 0;

            if (!pCredit.Disbursed) return 0;

            if (pCredit.AllInstallmentsRepaid) return 0;

            if (!pCredit.BadLoan) return (pCredit.Amount - olb) * rate;

            return (pCredit.Amount) * rate;
        }

        private static OCurrency _BadLoanBalance(Loan pCredit, int badLoanDays)
        {
            int pastDueDays = pCredit.GetPastDueDays(TimeProvider.Today);
            if (pastDueDays <= badLoanDays && pastDueDays != 0)
                return pCredit.GetOlb();
            return 0;
        }

        private static OCurrency _RescheduledLoanBalance(Loan pCredit)
        {
            return pCredit.Rescheduled ? pCredit.GetOlb() : 0;
        }

        private static OCurrency _CashStockBalance(Loan pCredit)
        {
            OCurrency balance = 0;
            if (!pCredit.Disbursed) return balance;

            balance += pCredit.GetPaidFees();
            balance += pCredit.GetPaidInterest();
            balance += pCredit.GetPaidPrincipal();

            return balance - pCredit.Amount;
        }

        private static OCurrency _CashCreditBalance(Loan pCredit)
        {
            if (!pCredit.Disbursed || pCredit.Rescheduled) return 0;

            return pCredit.GetPastDueDays(TimeProvider.Today) == 0 ? pCredit.GetOlb() : 0;

        }
    }
}
