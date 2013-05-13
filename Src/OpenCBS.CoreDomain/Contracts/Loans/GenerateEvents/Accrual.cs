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
    public static class Accrual
    {
        private const int AVERAGE_NB_OF_DAYS_IN_MONTH = 30;
        private const int PAST_DUE_MIN_DAYS = 3;

        public static LoanDisbursmentEvent GenerateLoanDisbursmentEvent(Loan pLoan, ApplicationSettings pGeneralSettings, DateTime pDisburseDate, bool pAlignInstallmentsDatesOnRealDisbursmentDate, bool pDisableFees, User pUser)
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
                                           ? new LoanDisbursmentEvent
                                                 {
                                                     Date = pDisburseDate,
                                                     Amount = pLoan.Amount,
                                                     ClientType = pLoan.ClientType
                                                 }
                                           : new LoanDisbursmentEvent
                                                 {
                                                     Date = pDisburseDate,
                                                     Amount = pLoan.Amount,
                                                     Commissions = null,
                                                     ClientType = pLoan.ClientType
                                                 };
            pLoan.Events.Add(lDe);
            return lDe;
        }

        public static OCurrency CalculateRemainingInterests(Loan pLoan, DateTime pDate)
        {
            OCurrency amount = 0;
            foreach (Installment installment in pLoan.InstallmentList)
            {
                if (installment.IsRepaid) continue;
                if(installment.ExpectedDate < pDate)
                {
                    amount += installment.InterestsRepayment - installment.PaidInterests;
                }
                else if(installment.ExpectedDate == pDate)
                {
                    amount += installment.InterestsRepayment - installment.PaidInterests;

                    return Math.Round(amount.Value, 2);
                }
                else
                {
                    DateTime date = installment.Number == 1 ? pLoan.StartDate : pLoan.GetInstallment(installment.Number - 2).ExpectedDate;
                    int days;
                    
                    int daysInInstallment = installment.Number == 1
                                                       ? (installment.ExpectedDate - pLoan.StartDate).Days
                                                       : (installment.ExpectedDate -
                                                          pLoan.GetInstallment(installment.Number - 2).ExpectedDate)
                                                             .Days;

                    if (installment.Number != 1)
                    {
                        if(pLoan.GetInstallment(installment.Number - 2).IsRepaid && pLoan.GetInstallment(installment.Number - 2).ExpectedDate > pDate)
                        {
                            days = 0;
                        }
                        else
                        {
                            if (pLoan.GetNotDeletedRepaymentEvent().Date > date && pLoan.GetNotDeletedRepaymentEvent().Interests > 0
                                && pLoan.GetNotDeletedRepaymentEvent().RepaymentType == OPaymentType.PartialPayment)
                            {
                                date = pLoan.GetLastRepaymentDate();
                                daysInInstallment = (installment.ExpectedDate - date).Days;
                            }
                            days = (pDate - date).Days;
                        }
                    }
                    else
                        days = (pDate - date).Days;

                    amount += days >= pLoan.NumberOfDaysInTheInstallment(installment.Number, pDate)
                                             ? installment.InterestsRepayment
                                             : installment.InterestsRepayment * (double)days / (double) daysInInstallment;
                    
                    if (installment.PaidInterests > amount)
                        amount = 0;

                    if (installment.PaidInterests < amount)
                        amount -= installment.PaidInterests;

                    return amount.Value;
                }
            }
            return amount.Value;
        }
    }
}
