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
using System.Linq;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment.BaseToCalculateFeesForAnticipatedRepayment;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;


namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.MaxRepayment
{
    /// <summary>
    /// Summary description for CalculateAmountToRepaySpecifiedInstallmentStrategy.
    /// </summary>
    [Serializable]
    public class CalculateAmountToRepaySpecifiedInstallmentStrategy
    {
        readonly Loan _contract;
        readonly CreditContractOptions _cCo;
        private readonly User _user;
        private readonly ApplicationSettings _generalSettings;
        private readonly NonWorkingDateSingleton _nWds;

        public CalculateAmountToRepaySpecifiedInstallmentStrategy(CreditContractOptions pCCo,Loan pContract, User pUser, 
            ApplicationSettings pGeneralSettings,NonWorkingDateSingleton pNonWorkingDate)
        {
            _user = pUser;
            _generalSettings = pGeneralSettings;
            _nWds = pNonWorkingDate;
            _contract = pContract;
            _cCo = pCCo;
        }

        private OCurrency MaxAmountOfRealSchedule(DateTime payDate)
        {
            Installment installment;

            OCurrency olb = _contract.CalculateActualOlb();
            OCurrency capitalRepayment = 0;
            OCurrency interestPayment = 0;
            OCurrency calculatedInterests;
            OCurrency actualOlb = _contract.CalculateActualOlb();

            bool calculated = false;

            DateTime lasDateOfPayment = _contract.GetLastRepaymentDate();
            int daysInTheYear = _generalSettings.GetDaysInAYear(_contract.StartDate.Year);
            int roundingPoint = _contract.UseCents ? 2 : 0;

            if (_contract.StartDate > lasDateOfPayment)
                lasDateOfPayment = _contract.StartDate.Date;

            for (int i = 0; i < _contract.NbOfInstallments; i++)
            {
                installment = _contract.GetInstallment(i);

                if (installment.IsRepaid)
                {
                    if (lasDateOfPayment < installment.ExpectedDate)
                        lasDateOfPayment = installment.ExpectedDate;
                }

                if (installment.ExpectedDate < payDate)
                {
                    capitalRepayment += installment.CapitalRepayment - installment.PaidCapital;
                    calculated = true;
                }

                if (_contract.StartDate < payDate && installment.Number == 1 && !calculated)
                {
                    capitalRepayment += installment.CapitalRepayment - installment.PaidCapital;
                    calculated = true;
                }

                if (installment.ExpectedDate == payDate && !calculated)
                {
                    capitalRepayment += installment.CapitalRepayment - installment.PaidCapital;
                    calculated = true;
                }

                if (installment.Number > 1
                    && installment.ExpectedDate != _contract.StartDate
                    && installment.ExpectedDate > payDate
                    && _contract.GetInstallment(installment.Number - 2).ExpectedDate < payDate
                    && !calculated)
                {
                    capitalRepayment += installment.CapitalRepayment - installment.PaidCapital;
                }

                calculated = false;

                if (installment.IsRepaid || installment.InterestHasToPay == 0) continue;

                if (installment.ExpectedDate <= payDate)
                {
                    calculatedInterests = 0;

                    if (installment.PaidInterests > 0
                        && installment.InterestsRepayment > installment.PaidInterests)
                    {
                        calculatedInterests = installment.PaidInterests;
                    }

                    if (installment.PaidCapital == 0
                        && installment.PaidInterests > 0
                        && installment.PaidInterests != installment.InterestsRepayment)
                    {
                        DateTime dateOfInstallment = installment.Number == 1
                                   ? _contract.StartDate
                                   : _contract.GetInstallment(installment.Number - 2).ExpectedDate;
                        int d = (lasDateOfPayment - dateOfInstallment).Days;
                        OCurrency olbBeforePayment =
                            _contract.Events.GetRepaymentEvents().Where(
                                repaymentEvent => !repaymentEvent.Deleted && repaymentEvent.Date <= dateOfInstallment).Aggregate(
                                    _contract.Amount, (current, repaymentEvent) => current - repaymentEvent.Principal);

                        calculatedInterests =
                            (olbBeforePayment * Convert.ToDecimal(_contract.InterestRate) / daysInTheYear * d).Value;
                        calculatedInterests = Math.Round(calculatedInterests.Value, roundingPoint,
                                                         MidpointRounding.AwayFromZero);

                        if (installment.PaidInterests < calculatedInterests && actualOlb != olbBeforePayment)
                        {
                            calculatedInterests = installment.PaidInterests;
                        }
                    }
                    DateTime expectedDate = installment.ExpectedDate;
                    //in case very late repayment
                    if (installment.Number == _contract.InstallmentList.Count
                        && installment.ExpectedDate < payDate
                        && installment.PaidCapital == 0)
                    {
                        expectedDate = payDate;
                    }

                    int days = (expectedDate - lasDateOfPayment).Days;
                    interestPayment += Math.Round((olb * _contract.InterestRate / daysInTheYear * days).Value + calculatedInterests.Value,
                                                    roundingPoint, MidpointRounding.AwayFromZero) - installment.PaidInterests;
                    lasDateOfPayment = installment.ExpectedDate;
                }

                if (installment.Number > 1
                    && installment.ExpectedDate > payDate
                    && installment.ExpectedDate > payDate
                    && _contract.GetInstallment(installment.Number - 2).ExpectedDate < payDate)
                {
                    OCurrency paidInterests = installment.PaidInterests;

                    int daySpan = (payDate - lasDateOfPayment).Days < 0 ? 0 : (payDate - lasDateOfPayment).Days;

                    installment.InterestsRepayment = olb * _contract.InterestRate * daySpan / daysInTheYear + paidInterests;

                    installment.InterestsRepayment =
                        Math.Round(installment.InterestsRepayment.Value, roundingPoint,
                                   MidpointRounding.AwayFromZero);
                    interestPayment += installment.InterestsRepayment - paidInterests;

                    lasDateOfPayment = installment.ExpectedDate;
                }

                if (installment.Number == 1 && installment.ExpectedDate > payDate)
                {
                    int daySpan = (payDate - _contract.StartDate).Days < 0
                                      ? 0
                                      : (payDate - _contract.StartDate).Days;
                    OCurrency interest = olb * _contract.InterestRate * daySpan / daysInTheYear;
                    interestPayment += Math.Round(interest.Value, roundingPoint, MidpointRounding.AwayFromZero) - installment.PaidInterests;
                }
            }

            return interestPayment + capitalRepayment < 0 ? 0 : interestPayment + capitalRepayment;
        }

        public OCurrency CalculateAmountToRepaySpecifiedInstallment(DateTime pDate,int pInstallmentNumber)
        {
            if (_cCo.LoansType == OLoanTypes.DecliningFixedPrincipalWithRealInterest)
                return MaxAmountOfRealSchedule(pDate.Date);

            OCurrency amount = 0;

            //principal
            amount += _contract.GetInstallment(pInstallmentNumber - 1).PrincipalHasToPay;

            //interest
            if (_cCo.CancelInterests)
                amount += _cCo.ManualInterestsAmount;

            else
                amount += _contract.GetInstallment(pInstallmentNumber - 1).InterestHasToPay;


            //commission
            if(_cCo.CancelFees)
            {
                amount += _cCo.ManualFeesAmount;
                amount += _cCo.ManualCommissionAmount;
            }
            else
                amount += CalculateLateAndAnticipatedFees(pDate, pInstallmentNumber);

            int decimalPoint = _contract.UseCents ? _generalSettings.InterestRateDecimalPlaces : 0;
            return Math.Round(amount.Value, decimalPoint, MidpointRounding.AwayFromZero);
        }

        private OCurrency CalculateLateAndAnticipatedFees(DateTime pDate, int pInstallmentNumber)
        {
            OCurrency fees = 0;
            Loan contract = _contract.Copy();
            new Repayment.RepayLateInstallments.CalculateInstallments(_cCo, contract, _user, _generalSettings,_nWds).CalculateNewInstallmentsWithLateFees(pDate);

            for (int i = 0; i < contract.NbOfInstallments; i++)
            {
                Installment installment = contract.GetInstallment(i);
                if (!installment.IsRepaid && installment.ExpectedDate <= pDate && installment.Number != pInstallmentNumber) //late
                {
                    fees += installment.FeesUnpaid;
                    installment.PaidCapital = installment.CapitalRepayment;
                    installment.PaidInterests = installment.InterestsRepayment;
                }
                else if (!installment.IsRepaid && installment.ExpectedDate <= pDate && installment.Number == pInstallmentNumber) //late
                {
                    fees += installment.FeesUnpaid;			
                    break;
                }
                else if(!installment.IsRepaid && installment.ExpectedDate > pDate && installment.Number != pInstallmentNumber)
                {
                    if (!_cCo.KeepExpectedInstallments)
                        fees += new CalculationBaseForAnticipatedFees(_cCo, contract).CalculateFees(pDate);
                    break;
                }
            }
            return fees;
        }
    }
}
