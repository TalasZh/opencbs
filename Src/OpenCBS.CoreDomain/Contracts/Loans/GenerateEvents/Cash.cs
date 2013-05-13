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
using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Events;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.GenerateEvents
{
    public static class Cash
    {
        private const int AVERAGE_NB_OF_DAYS_IN_MONTH = 30;
        private const int PAST_DUE_MIN_DAYS = 3;

        public static LoanDisbursmentEvent GenerateLoanDisbursmentEvent(Loan pLoan,ApplicationSettings pGeneralSettings, DateTime pDisburseDate, bool pAlignInstallmentsDatesOnRealDisbursmentDate, bool pDisableFees,User pUser)
        {
            if (pAlignInstallmentsDatesOnRealDisbursmentDate)
            {
                //pLoan.StartDate = pDisburseDate;
                //for (int i = 1; i <= pLoan.NbOfInstallments; i++)
                //{
                //    pLoan.InstallmentList[i - 1].ExpectedDate = pLoan.CalculateInstallmentDate(pLoan.StartDate, i);
                //}
            }
            else
            {
                if (pGeneralSettings.PayFirstInterestRealValue)
                {
                    TimeSpan time = pDisburseDate - pLoan.StartDate;
                    int diffDays = Math.Abs(time.Days);

                    int nbOfDaysInPeriod = pLoan.InstallmentType.NbOfMonths * AVERAGE_NB_OF_DAYS_IN_MONTH + pLoan.InstallmentType.NbOfDays;

                    if (pDisburseDate.CompareTo(pLoan.StartDate) < 0)
                        pLoan.GetInstallment(0).InterestsRepayment += (Convert.ToDecimal(pLoan.InterestRate) * diffDays * pLoan.Amount / (double)nbOfDaysInPeriod);
                    else
                        pLoan.GetInstallment(0).InterestsRepayment -= (Convert.ToDecimal(pLoan.InterestRate) * diffDays * pLoan.Amount / (double)nbOfDaysInPeriod);

                    if (AmountComparer.Compare(pLoan.GetInstallment(0).InterestsRepayment, 0) < 0)
                    {
                        pLoan.GetInstallment(0).InterestsRepayment = 0;
                    }
                    pLoan.GetInstallment(0).InterestsRepayment = Math.Round(pLoan.GetInstallment(0).InterestsRepayment.Value, 2);
                }
            }

            pLoan.Disbursed = true;
            LoanDisbursmentEvent lDe = !pDisableFees
                                           ? new LoanDisbursmentEvent { Date = pDisburseDate, Amount = pLoan.Amount, ClientType = pLoan.ClientType }
                                           : new LoanDisbursmentEvent { Date = pDisburseDate, Amount = pLoan.Amount, Commissions = null, ClientType = pLoan.ClientType };
            //Book(pLoan, lDe, pUser);
            pLoan.Events.Add(lDe);
            return lDe;
        }

        public static bool CheckDegradeToBadLoan(Loan pLoan, ApplicationSettings settings, ProvisionTable pProvisionTable, DateTime pDate, bool authorizeSeveralPastDueEventByPeriod, int? pPstDueDays,User pUser)
        {
            DateTime date;
            int pastDueDays;
            if (pPstDueDays.HasValue)
            {
                pastDueDays = pPstDueDays.Value;
                date = pDate.AddDays(pastDueDays - pLoan.GetPastDueDays(pDate));
            }
            else
            {
                date = pDate;
                pastDueDays = pLoan.GetPastDueDays(date);
            }

            //In this case, the loan is not considered as bad loan
            if (pastDueDays < PAST_DUE_MIN_DAYS)
            {
                return false;
            }

           //A loan is only degraded from cash credit to bad loan one time
            OCurrency cashBalance = 0; //pLoan.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH_CREDIT, pLoan.Product.Currency.Id, pLoan, OBookingDirections.Both).Balance;
            if (cashBalance != 0)
            {
                pLoan.BadLoan = true;
            }
            else
            {
                //Downgrading the loan to unrecoverable bad loans is done once for loan with past due days greater than 180
                if (pastDueDays > settings.BadLoanDays)
                {
                    pLoan.BadLoan = true;
                }
            }
            return true;
        }

        public static OCurrency CalculateRemainingInterests(Loan pLoan,DateTime pDate)
        {
            OCurrency amount = 0;

            foreach (Installment installment in pLoan.InstallmentList)
            {
                if (DateTime.Compare(installment.ExpectedDate, pDate) > 0)
                {
                    return amount;
                }

                if (installment.IsRepaid) continue;

                amount += installment.InterestsRepayment - installment.PaidInterests;
            }
            return amount;
        }
    }
}
