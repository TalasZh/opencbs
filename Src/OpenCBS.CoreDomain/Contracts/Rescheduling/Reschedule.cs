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
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Events;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Rescheduling
{
    [Serializable]
    public struct ReschedulingOptions
    {
        public DateTime ReschedulingDate;
        public int RepaymentDateOffset;
        public int NewInstallments;
        public decimal InterestRate;
        public bool ChargeInterestDuringShift;
        public int GracePeriod;
        public bool ChargeInterestDuringGracePeriod;
    }

    public class RescheduleLoan
    {
        private Loan _contract;
        private NonWorkingDateSingleton _nwdS;
        private ApplicationSettings _generalSettings;

        public RescheduleLoanEvent Reschedule(ReschedulingOptions ro, Loan contract, NonWorkingDateSingleton nwdS, ApplicationSettings applicationSettings)
        {
            _contract = contract;
            _nwdS = nwdS;
            _generalSettings = applicationSettings;

            switch (contract.Product.LoanType)
            {
                case OLoanTypes.Flat:
                    _Reschedule_Flat(ro);
                    break;

                case OLoanTypes.DecliningFixedPrincipal:
                    _Reschedule_FixedPrincipal(ro);
                    break;

                case OLoanTypes.DecliningFixedInstallments:
                    _Reschedule_DecliningFixedInstallments(ro);
                    break;
            }

            _Reschedule_AdjustOverpaid();

            RescheduleLoanEvent rSe = new RescheduleLoanEvent
                                          {
                                              Date = ro.ReschedulingDate,
                                              Amount = contract.CalculateActualOlb(),
                                              Interest = contract.GetTotalInterestDue(),
                                              ClientType = contract.ClientType,
                                              BadLoan = contract.BadLoan,
                                              NbOfMaturity = ro.NewInstallments,
                                              DateOffset = ro.RepaymentDateOffset,
                                              GracePeriod = ro.GracePeriod,
                                              ChargeInterestDuringShift = ro.ChargeInterestDuringShift,
                                              ChargeInterestDuringGracePeriod = ro.ChargeInterestDuringGracePeriod,
                                              InstallmentNumber =
                                                  contract.GetLastFullyRepaidInstallment() == null
                                                      ? 1
                                                      : contract.GetLastFullyRepaidInstallment().Number + 1
                                          };
            _contract.CalculateStartDates();
            return rSe;
        }

        private DateTime[] _Reschedule_AdjustDates(ReschedulingOptions ro)
        {
            DateTime[] retval = new DateTime[2];
            DateTime oldDate = DateTime.MinValue;
            DateTime newDate = DateTime.MinValue;
            bool first = false;
            int number = 0;
            for (int i = 0; i < _contract.InstallmentList.Count; i++)
            {
                Installment installment = _contract.GetInstallment(i);
                if (installment.IsRepaid) continue;
                if (!first)
                {
                    first = true;
                    oldDate = installment.ExpectedDate;
                    newDate = installment.ExpectedDate;
                    if (0 == ro.RepaymentDateOffset)
                        break;

                    newDate = newDate.AddDays(ro.RepaymentDateOffset);

                    DateTime actualNewDate = _nwdS.GetTheNearestValidDate(newDate,
                                                                          _generalSettings.IsIncrementalDuringDayOff,
                                                                          _generalSettings.DoNotSkipNonWorkingDays, true);
                    installment.ExpectedDate = actualNewDate;
                }
                installment.ExpectedDate = _contract.CalculateInstallmentDate(newDate, number);
                number++;
            }
            retval[0] = oldDate;
            retval[1] = newDate;
            return retval;
        }

        private void _Reschedule_DecliningFixedInstallments(ReschedulingOptions ro)
        {
            int months = _contract.InstallmentType.NbOfMonths;
            int days = _contract.InstallmentType.NbOfDays;
            int rounding = _contract.UseCents ? 2 : 0;

            // Split installments into two lists - paid and unpaid
            List<Installment> paidInstallments = new List<Installment>();
            List<Installment> unpaidInstallments = new List<Installment>();
            foreach (Installment installment in _contract.InstallmentList)
            {
                if (installment.IsRepaid)
                {
                    paidInstallments.Add(installment);
                }
                else
                {
                    unpaidInstallments.Add(installment);
                }
            }

            // Add new installments
            if (ro.NewInstallments > 0)
            {
                int lastNumber = unpaidInstallments[unpaidInstallments.Count - 1].Number;
                for (int i = 1; i <= ro.NewInstallments; i++)
                {
                    Installment installment = new Installment();
                    installment.Number = lastNumber + i;
                    installment.CapitalRepayment = 0;
                    installment.PaidCapital = 0;
                    installment.InterestsRepayment = 0;
                    installment.PaidInterests = 0;
                    installment.FeesUnpaid = 0;
                    installment.CommissionsUnpaid = 0;
                    unpaidInstallments.Add(installment);
                }
            }

            // We want to recalculate installments if new installments are introduced,
            // interest rate is changed, or the first unpaid installment is partially paid.
            int moveToNext = 0;
            if (unpaidInstallments[0].IsPartiallyRepaid && ro.GracePeriod > 0)
            {
                moveToNext = 1;
            }

            bool recalculate = ro.NewInstallments > 0
                || _contract.InterestRate != ro.InterestRate
                || unpaidInstallments[0].IsPartiallyRepaid
                || ro.GracePeriod != ro.NewInstallments;

            //if (ro.NewInstallments > 0 || _contract.InterestRate != ro.InterestRate || unpaidInstallments[0].IsPartiallyRepaid)
            if (recalculate)
            {
                OCurrency initialOlb = unpaidInstallments[0].OLB - unpaidInstallments[0].PaidCapital;

                int n = unpaidInstallments.Count - ro.GracePeriod - moveToNext;
                // We use the formula below to calculate monthly annuity payments

                //OCurrency monthly = 0 == r ? initialOlb / n : initialOlb * r * Math.Pow(1 + r, n) / (Math.Pow(1 + r, n) - 1);
                OCurrency monthly = _contract.VPM(initialOlb, n, ro.InterestRate);
                monthly = Math.Round(monthly.Value, rounding, MidpointRounding.AwayFromZero); //_Reschedule_Round(monthly);

                // Now loop through the unpaid installments and calculate the principal and the interest
                OCurrency sum = 0;
                OCurrency olb = initialOlb;

                foreach (Installment installment in unpaidInstallments)
                {
                    OCurrency interest = olb * ro.InterestRate;

                    if (installment.IsPartiallyRepaid && ro.GracePeriod > 0)
                    {
                        installment.InterestsRepayment = installment.PaidInterests;
                        installment.CapitalRepayment = installment.PaidCapital;
                    }
                    else
                    {
                        if (installment.Number > ro.GracePeriod + unpaidInstallments[0].Number - 1 + moveToNext)
                        {
                            OCurrency principal = monthly - interest;
                            installment.CapitalRepayment = Math.Round(principal.Value, rounding,
                                                                      MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            installment.CapitalRepayment = 0;
                        }

                        if ((installment.Number > ro.GracePeriod + unpaidInstallments[0].Number - 1 + moveToNext) ||
                            (ro.ChargeInterestDuringGracePeriod))
                        {
                            installment.InterestsRepayment = Math.Round(interest.Value, rounding,
                                                                        MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            installment.InterestsRepayment = 0;
                        }

                        if (ro.GracePeriod > 0)
                        {
                            if (!installment.IsPartiallyRepaid)
                            {
                                sum += installment.CapitalRepayment;
                                installment.OLB = olb;
                                olb -= installment.CapitalRepayment.Value;
                            }
                        }
                        else
                        {
                            sum += installment.CapitalRepayment;
                            installment.OLB = olb;
                            olb -= installment.CapitalRepayment.Value;
                        }
                    }
                }

                // As a result of rounding there might be some difference between the
                // initial OLB and the sum of installment repayments. Thus we have to
                // compensate for this difference.
                OCurrency diff = sum - initialOlb;
                if (diff != 0)
                {
                    unpaidInstallments[ro.GracePeriod + moveToNext].CapitalRepayment -= diff;
                    int firstNumber = unpaidInstallments[ro.GracePeriod + moveToNext].Number;
                    // Iterate through all unpaid but first
                    foreach (Installment installment in unpaidInstallments.FindAll(x => x.Number > firstNumber))
                    {
                        installment.OLB += diff;
                    }
                }
            }

            // Adjust the amount of interest for the first installment
            if ((ro.RepaymentDateOffset > 0 && ro.ChargeInterestDuringShift) || 
                (_contract.FirstDateChanged && 1 == unpaidInstallments[0].Number && ro.GracePeriod > 0 && ro.ChargeInterestDuringGracePeriod) ||
                (_contract.FirstDateChanged && 1 == unpaidInstallments[0].Number && 0 == ro.GracePeriod))
            {
                DateTime startDate;
                DateTime endDate1;
                DateTime endDate2;
                if (0 == paidInstallments.Count)
                {
                    startDate = _contract.StartDate;
                    endDate1 = startDate.AddMonths(months).AddDays(days);
                    endDate2 = _contract.FirstInstallmentDate.AddDays(ro.RepaymentDateOffset * (ro.ChargeInterestDuringShift ? 1 : 0));
                }
                else
                {
                    startDate = paidInstallments[paidInstallments.Count - 1].ExpectedDate;
                    endDate1 = startDate.AddMonths(months).AddDays(days);
                    endDate2 = endDate1.AddDays(ro.RepaymentDateOffset * (ro.ChargeInterestDuringShift ? 1 : 0));
                }
                TimeSpan oldSpan = endDate1 - startDate;
                TimeSpan newSpan = endDate2 - startDate;
                double olb = Convert.ToDouble(unpaidInstallments[0].OLB.Value);
                double ratio;

                if (ro.GracePeriod > 0 && !ro.ChargeInterestDuringGracePeriod)
                {
                    ratio = (double)(newSpan.Days - oldSpan.Days) / oldSpan.Days;
                }
                else
                {
                    ratio = (double)newSpan.Days / oldSpan.Days;
                }

                OCurrency interest = Convert.ToDecimal(olb) * ro.InterestRate * Convert.ToDecimal(ratio);
                interest = Math.Round(interest.Value, rounding, MidpointRounding.AwayFromZero);
                unpaidInstallments[0].InterestsRepayment = interest;
            }

            if (unpaidInstallments[0].IsPartiallyRepaid)
            {
                int instNum = ro.GracePeriod > 0 ? ro.GracePeriod - 1 : 0;
                unpaidInstallments[instNum].CapitalRepayment += unpaidInstallments[0].PaidCapital;
                unpaidInstallments[instNum].OLB += unpaidInstallments[0].PaidCapital;
            }

            // Adjust dates
            DateTime baseDate = unpaidInstallments[0].ExpectedDate.AddDays(ro.RepaymentDateOffset);
            int num = 0;
            foreach (Installment installment in unpaidInstallments)
            {
                DateTime d = baseDate.AddMonths(months * num).AddDays(days * num);
                d = _nwdS.GetTheNearestValidDate(d, _generalSettings.IsIncrementalDuringDayOff,
                                                 _generalSettings.DoNotSkipNonWorkingDays, true);
                installment.ExpectedDate = d;
                num++;
            }

            // Merge the lists back into one
            _contract.InstallmentList = paidInstallments;
            _contract.InstallmentList.AddRange(unpaidInstallments);
            _contract.NbOfInstallments = _contract.InstallmentList.Count();
        }

        private void _Reschedule_ExtendMaturity(int newInstallments)
        {
            if (newInstallments > 0)
            {
                int lastNumber = _contract.LastInstallment.Number;
                for (int i = 1; i <= newInstallments; i++)
                {
                    DateTime expectedDate = _contract.CalculateInstallmentDate(_contract.FirstInstallmentDate, lastNumber + i - 1);
                    Installment installment = new Installment
                    {
                        Number = lastNumber + i,
                        CapitalRepayment = 0,
                        PaidCapital = 0,
                        CommissionsUnpaid = 0,
                        FeesUnpaid = 0,
                        InterestsRepayment = 1,
                        PaidInterests = 0,
                        ExpectedDate = expectedDate
                    };
                    _contract.AddInstallment(installment);
                }
            }
        }

        private void _Reschedule_Flat(ReschedulingOptions ro)
        {
            _Reschedule_ExtendMaturity(ro.NewInstallments);
            DateTime[] dates = _Reschedule_AdjustDates(ro);

            int roundDecimals = _contract.UseCents ? 2 : 0;
            bool firstNonRepaid = true;
            Installment firstUnpaidInstallment = _contract.GetFirstUnpaidInstallment();
            int moveToNext = 0;

            foreach (Installment installment in _contract.InstallmentList)
            {
                if (installment.IsRepaid)
                    continue;

                if (firstNonRepaid)
                {
                    firstNonRepaid = false;
                    if (ro.ChargeInterestDuringShift 
                        || _contract.InterestRate != ro.InterestRate 
                        || ro.NewInstallments > 0
                        || ro.GracePeriod > 0)
                    {
                        TimeSpan oldSpan;
                        TimeSpan newSpan;
                        if (1 == installment.Number)
                        {
                            DateTime endDate =
                                _contract.StartDate.AddMonths(_contract.InstallmentType.NbOfMonths).AddDays(
                                    _contract.InstallmentType.NbOfDays);
                            endDate = _nwdS.GetTheNearestValidDate(endDate, _generalSettings.IsIncrementalDuringDayOff,
                                                                   _generalSettings.DoNotSkipNonWorkingDays, true);
                            newSpan = (ro.ChargeInterestDuringShift ? installment.ExpectedDate : dates[0]) - _contract.StartDate;
                            oldSpan = endDate - _contract.StartDate;
                        }
                        else
                        {
                            DateTime startDate = _contract.GetInstallment(installment.Number - 2).ExpectedDate;
                            oldSpan = dates[0] - startDate;
                            newSpan = (ro.ChargeInterestDuringShift ? dates[1] : dates[0]) - startDate;
                        }

                        if (ro.GracePeriod > 0 && installment.IsPartiallyRepaid)
                        {
                            installment.InterestsRepayment = installment.PaidInterests;
                            moveToNext = 1;
                        }
                        else
                        {
                            if (installment.Number >= ro.GracePeriod + firstUnpaidInstallment.Number)
                            {
                                OCurrency interest = _contract.Amount * newSpan.Days * ro.InterestRate / oldSpan.Days;
                                interest = Math.Round(interest.Value, roundDecimals, MidpointRounding.AwayFromZero);
                                installment.InterestsRepayment = interest;
                            }
                            else
                            {
                                if (ro.ChargeInterestDuringGracePeriod)
                                {
                                    OCurrency interest = _contract.Amount * newSpan.Days * ro.InterestRate / oldSpan.Days;
                                    interest = Math.Round(interest.Value, roundDecimals, MidpointRounding.AwayFromZero);
                                    installment.InterestsRepayment = interest;
                                }
                                else
                                {
                                    installment.InterestsRepayment = 0;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ro.GracePeriod > 0 && installment.IsPartiallyRepaid)
                    {
                        installment.InterestsRepayment = installment.PaidInterests;
                        moveToNext = 1;
                    }
                    else
                    {
                        if (installment.Number > ro.GracePeriod + firstUnpaidInstallment.Number - 1 + moveToNext)
                        {
                            OCurrency interest = _contract.Amount * ro.InterestRate;
                            interest = Math.Round(interest.Value, roundDecimals, MidpointRounding.AwayFromZero);
                            installment.InterestsRepayment = interest;
                        }
                        else
                        {
                            if (ro.ChargeInterestDuringGracePeriod)
                            {
                                OCurrency interest = _contract.Amount * ro.InterestRate;
                                interest = Math.Round(interest.Value, roundDecimals, MidpointRounding.AwayFromZero);
                                installment.InterestsRepayment = interest;
                            }
                            else
                            {
                                installment.InterestsRepayment = 0;
                            }
                        }
                    }
                }
            }

            if (null == firstUnpaidInstallment)
                return;

            if (!firstUnpaidInstallment.IsPartiallyRepaid && 0 == ro.NewInstallments && 0 == ro.GracePeriod)
                return;

            OCurrency unpaidAmount = 0;

            foreach (Installment installment in _contract.InstallmentList)
            {
                unpaidAmount += installment.CapitalRepayment - installment.PaidCapital;
            }

            int nbOfUnpaid = _contract.InstallmentList.Count - firstUnpaidInstallment.Number + 1;

            int nextNumber = 0;
            if (firstUnpaidInstallment.IsPartiallyRepaid && ro.GracePeriod > 0)
            {
                nextNumber = 1;
            }

            double monthly = Math.Round(
                Convert.ToDouble(unpaidAmount.Value / (nbOfUnpaid - ro.GracePeriod - nextNumber)), roundDecimals,
                MidpointRounding.AwayFromZero);

            for (int i = firstUnpaidInstallment.Number - 1; i < _contract.InstallmentList.Count; i++)
            {
                Installment installment = _contract.GetInstallment(i);
                if (installment.Number > ro.GracePeriod + firstUnpaidInstallment.Number - 1 + nextNumber)
                {
                    installment.CapitalRepayment = Convert.ToDecimal(monthly);
                }
                else
                {
                    installment.CapitalRepayment = 0;
                }
            }

            firstUnpaidInstallment.CapitalRepayment += firstUnpaidInstallment.PaidCapital;

            double diff = Convert.ToDouble(unpaidAmount.Value) - monthly * (nbOfUnpaid - ro.GracePeriod - nextNumber);

            if (diff != 0)
            {
                if (nextNumber != 1)
                    if (ro.GracePeriod > 0)
                    {
                        _contract.GetInstallment(firstUnpaidInstallment.Number + ro.GracePeriod).CapitalRepayment += Convert.ToDecimal(diff);
                    }
                    else
                    {
                        firstUnpaidInstallment.CapitalRepayment += Convert.ToDecimal(diff);
                    }
                else
                {
                    _contract.GetInstallment(firstUnpaidInstallment.Number + ro.GracePeriod).CapitalRepayment += Convert.ToDecimal(diff);
                }
            }

            OCurrency olb = firstUnpaidInstallment.OLB;

            for (int i = firstUnpaidInstallment.Number - 1; i < _contract.InstallmentList.Count; i++)
            {
                Installment installment = _contract.GetInstallment(i);
                installment.OLB = olb;
                olb -= installment.CapitalRepayment;
            }
        }

        private void _Reschedule_FixedPrincipal(ReschedulingOptions ro)
        {
            _Reschedule_ExtendMaturity(ro.NewInstallments);
            int roundDecimals = _contract.UseCents ? 2 : 0;
            Installment firstUnpaidInstallment = _contract.GetFirstUnpaidInstallment();
            OCurrency paidCapital = 0;
            if (firstUnpaidInstallment.IsPartiallyRepaid && firstUnpaidInstallment.PaidCapital != 0 && ro.GracePeriod > 0)
            {
                paidCapital = firstUnpaidInstallment.PaidCapital;
                firstUnpaidInstallment.CapitalRepayment = firstUnpaidInstallment.PaidCapital;
                firstUnpaidInstallment = _contract.GetInstallment(firstUnpaidInstallment.Number);
            }

            int firstUnpaidInstallmentNumber = firstUnpaidInstallment.Number;
            int toNext = firstUnpaidInstallment.IsPartiallyRepaid && ro.GracePeriod > 0 ? 1 : 0;

            if (firstUnpaidInstallment.IsPartiallyRepaid || ro.NewInstallments > 0 || ro.GracePeriod > 0)
            {
                OCurrency unpaidAmount = 0;
                foreach (Installment installment in _contract.InstallmentList)
                {
                    unpaidAmount += installment.IsRepaid ? 0 : installment.CapitalRepayment - installment.PaidCapital;
                }

                unpaidAmount += paidCapital;
                int nbOfUnpaid = _contract.InstallmentList.Count - firstUnpaidInstallmentNumber + 1 - toNext;
                double monthly = Math.Round(Convert.ToDouble(unpaidAmount.Value / (nbOfUnpaid - ro.GracePeriod)),
                                            roundDecimals,
                                            MidpointRounding.AwayFromZero);

                for (int i = firstUnpaidInstallment.Number - 1; i < _contract.InstallmentList.Count; i++)
                {
                    Installment installment = _contract.GetInstallment(i);

                    if (installment.Number > ro.GracePeriod + firstUnpaidInstallment.Number - 1 + toNext)
                    {
                        installment.CapitalRepayment = Convert.ToDecimal(monthly);
                    }
                    else
                    {
                        installment.CapitalRepayment = 0;
                    }
                }

                Installment installmentOfCorrection = _contract.GetInstallment(firstUnpaidInstallmentNumber + ro.GracePeriod - 1 + toNext);

                installmentOfCorrection.CapitalRepayment += installmentOfCorrection.PaidCapital;
                double diff = Convert.ToDouble(unpaidAmount.Value) - monthly * (nbOfUnpaid - ro.GracePeriod);

                if (diff != 0)
                    installmentOfCorrection.CapitalRepayment += Convert.ToDecimal(diff);

                OCurrency olb = firstUnpaidInstallment.OLB + paidCapital;
                for (int i = firstUnpaidInstallment.Number - 1; i < _contract.InstallmentList.Count; i++)
                {
                    Installment installment = _contract.GetInstallment(i);
                    installment.OLB = olb;
                    olb -= installment.CapitalRepayment;
                }
            }

            DateTime[] dates = _Reschedule_AdjustDates(ro);
            bool firstNonRepaid = true;

            if(firstUnpaidInstallment.IsPartiallyRepaid && ro.GracePeriod > 0)
            {
                firstUnpaidInstallment.InterestsRepayment = firstUnpaidInstallment.PaidInterests;
                firstUnpaidInstallment.CapitalRepayment = firstUnpaidInstallment.PaidCapital;
                firstNonRepaid = false;
            }
            //interest calculation
            foreach (Installment installment in _contract.InstallmentList)
            {
                if (installment.IsRepaid) continue;
                if (firstNonRepaid)
                {
                    firstNonRepaid = false;

                    if (ro.ChargeInterestDuringShift 
                        || _contract.InterestRate != ro.InterestRate 
                        || ro.NewInstallments > 0
                        || ro.GracePeriod > 0)
                    {
                        TimeSpan oldSpan;
                        TimeSpan newSpan;

                        if ((installment.Number > ro.GracePeriod + firstUnpaidInstallment.Number - 1 + toNext) || (ro.ChargeInterestDuringGracePeriod))
                        {
                            if (1 == installment.Number)
                            {
                                DateTime endDate =
                                    _contract.StartDate.AddMonths(_contract.InstallmentType.NbOfMonths).AddDays(_contract.InstallmentType.NbOfDays);
                                endDate = _nwdS.GetTheNearestValidDate(endDate,
                                                                       _generalSettings.IsIncrementalDuringDayOff,
                                                                       _generalSettings.DoNotSkipNonWorkingDays, true);

                                newSpan = (ro.ChargeInterestDuringShift ? installment.ExpectedDate : dates[0]) -
                                          _contract.StartDate;
                                oldSpan = endDate - _contract.StartDate;
                            }
                            else
                            {
                                DateTime startDate = _contract.GetInstallment(installment.Number - 2).ExpectedDate;
                                oldSpan = dates[0] - startDate;
                                newSpan = (ro.ChargeInterestDuringShift ? dates[1] : dates[0]) - startDate;
                            }

                            OCurrency amount = installment.OLB - installment.PaidCapital;
                            decimal interest = (amount.Value * newSpan.Days) * ro.InterestRate / oldSpan.Days;
                            interest = Math.Round(interest, roundDecimals, MidpointRounding.AwayFromZero);
                            installment.InterestsRepayment = Convert.ToDecimal(interest);
                        }
                        else
                        {
                            installment.InterestsRepayment = 0;
                        }
                    }
                }
                else
                {
                    if (_contract.InterestRate != ro.InterestRate 
                        || firstUnpaidInstallment.IsPartiallyRepaid 
                        || ro.NewInstallments > 0
                        || ro.GracePeriod > 0)
                    {
                        if ((installment.Number > ro.GracePeriod + firstUnpaidInstallment.Number - 1 + toNext) || (ro.ChargeInterestDuringGracePeriod))
                        {
                            decimal interest = installment.OLB.Value * ro.InterestRate;
                            interest = Math.Round(interest, roundDecimals, MidpointRounding.AwayFromZero);
                            installment.InterestsRepayment = Convert.ToDecimal(interest);
                        }
                        else
                        {
                            installment.InterestsRepayment = 0;
                        }
                    }
                }
            }
        }

        private void _Reschedule_AdjustOverpaid()
        {
            Installment firstInstallment = _contract.GetFirstUnpaidInstallment();
            if (firstInstallment != null && firstInstallment.IsPartiallyRepaid)
            {
                if (firstInstallment.PaidInterests > firstInstallment.InterestsRepayment)
                {
                    OCurrency diff = firstInstallment.PaidInterests - firstInstallment.InterestsRepayment;
                    firstInstallment.PaidInterests = firstInstallment.InterestsRepayment;
                    for (int i = firstInstallment.Number; i < _contract.NbOfInstallments; i++)
                    {
                        Installment installment = _contract.GetInstallment(i);
                        if (diff > installment.InterestsRepayment)
                        {
                            installment.PaidInterests = installment.InterestsRepayment;
                            diff = diff - installment.InterestsRepayment;
                        }
                        else
                        {
                            installment.PaidInterests = diff;
                            diff = 0;
                            break;
                        }
                    }
                    if (diff > 0)
                    {
                        Installment installment = _contract.GetInstallment(_contract.NbOfInstallments - 1);
                        installment.PaidCapital = diff;
                    }
                }
            }
        }

    }
}
