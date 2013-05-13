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
using OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Interfaces;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Flat
{
    /// <summary>
    /// Summary description for FlatStrategy.
    /// </summary>
    [Serializable]
    public class FlatStrategy : ICalculateInstallments
    {
        //private readonly ICalculateInstallments _iCS;
        private readonly Loan _contract;
        private readonly int _roundingPoint;
        private readonly OCurrency _initialOlbOfContractBeforeRescheduling;
        private readonly ApplicationSettings _generalSettings;
        
        public FlatStrategy(Loan pContract, ApplicationSettings pGeneralSettings, OCurrency pInitialOlbOfContractBeforeRescheduling)
        {
            _roundingPoint = pContract.UseCents ? 2 : 0;

            _initialOlbOfContractBeforeRescheduling = pInitialOlbOfContractBeforeRescheduling;
            _contract = pContract;
            _generalSettings = pGeneralSettings;
        }

        public List<Installment> CalculateInstallments(bool changeDate)
        {
            List<Installment> schedule = new List<Installment>();
            OCurrency olb = _contract.Amount;
            OCurrency totalAmount = _contract.Amount;

            OCurrency totalInterest = _contract.GracePeriod.HasValue &&
                                      !_contract.Product.ChargeInterestWithinGracePeriod
                                          ? Math.Round(
                                                _contract.Amount.Value * Convert.ToDecimal(_contract.InterestRate) *
                                                (_contract.NbOfInstallments - _contract.GracePeriod.Value),
                                                _roundingPoint, MidpointRounding.AwayFromZero)
                                          : Math.Round(
                                                _contract.Amount.Value * Convert.ToDecimal(_contract.InterestRate) *
                                                _contract.NbOfInstallments, _roundingPoint,
                                                MidpointRounding.AwayFromZero);

            int nbOfInstallmentWithoutGracePeriod = _contract.GracePeriod.HasValue
                                                        ? _contract.NbOfInstallments - _contract.GracePeriod.Value
                                                        : _contract.NbOfInstallments;

            OCurrency sumOfPrincipal = 0;
            OCurrency sumOfInterest = 0;

            OCurrency interestBase = _contract.Product.ChargeInterestWithinGracePeriod
                                         ? Math.Round(
                                               (totalInterest.Value - sumOfInterest.Value)/
                                               (nbOfInstallmentWithoutGracePeriod + _contract.GracePeriod.Value),
                                               _roundingPoint, MidpointRounding.AwayFromZero)
                                         : Math.Round(
                                               (totalInterest.Value - sumOfInterest.Value)/
                                               (nbOfInstallmentWithoutGracePeriod),
                                               _roundingPoint, MidpointRounding.AwayFromZero);

            OCurrency principalBase = Math.Round(
                                (totalAmount.Value - sumOfPrincipal.Value) /
                                (nbOfInstallmentWithoutGracePeriod),
                                _roundingPoint, MidpointRounding.AwayFromZero);

            int installmentNumberWithoutGracePeriodForCapital = 0;
            int installmentNumberWithoutGracePeriodForInterest = 0;

            if (!_contract.Rescheduled)
            {
                for (int number = 1; number <= _contract.NbOfInstallments; number++)
                {
                    Installment installment = new Installment { Number = number, FeesUnpaid = 0 };

                    if (changeDate)
                    {
                        installment.ExpectedDate = _contract.CalculateInstallmentDate(_contract.AlignDisbursementDate, installment.Number);
                    }

                    installment.OLB = olb;

                    OCurrency interest = Math.Round(
                                (totalInterest.Value - sumOfInterest.Value) /
                                (_contract.NbOfInstallments - installmentNumberWithoutGracePeriodForInterest),
                                _roundingPoint, MidpointRounding.AwayFromZero);

                    OCurrency principal = Math.Round(
                                (totalAmount.Value - sumOfPrincipal.Value) /
                                (nbOfInstallmentWithoutGracePeriod - installmentNumberWithoutGracePeriodForCapital),
                                _roundingPoint, MidpointRounding.AwayFromZero);

                    if (_contract.Product.RoundingType == ORoundingType.End ||
                             _contract.Product.RoundingType == ORoundingType.Begin)
                    {
                        interest = interestBase;
                        principal = principalBase;
                    }

                    if (_contract.GracePeriod.HasValue && number <= _contract.GracePeriod)
                    {
                        installment.CapitalRepayment = 0;

                        if (_contract.Product.ChargeInterestWithinGracePeriod)
                        {
                            installment.InterestsRepayment = interest;

                            sumOfInterest += installment.InterestsRepayment;
                            installmentNumberWithoutGracePeriodForInterest++;
                        }
                        else
                        {
                            installment.InterestsRepayment = 0;
                            installmentNumberWithoutGracePeriodForInterest++;
                        }
                    }
                    else
                    {
                        installment.CapitalRepayment = principal;

                        sumOfPrincipal += installment.CapitalRepayment;
                        installmentNumberWithoutGracePeriodForCapital++;

                        installment.InterestsRepayment = interest;

                        sumOfInterest += installment.InterestsRepayment;
                        installmentNumberWithoutGracePeriodForInterest++;
                    }

                    if (installment.Number == 1 && _generalSettings.PayFirstInterestRealValue)
                    {
                        installment.InterestsRepayment =
                            Math.Round(
                                installment.InterestsRepayment.Value/_contract.NbOfDaysInTheInstallment*
                                (_contract.FirstInstallmentDate - _contract.StartDate).Days, _roundingPoint,
                                MidpointRounding.AwayFromZero);
                    }

                    olb -= installment.CapitalRepayment.Value;

                    schedule.Add(installment);
                }
            }

            if (_contract.Product.RoundingType == ORoundingType.End)
            {
                schedule[_contract.NbOfInstallments - 1].InterestsRepayment += totalInterest - sumOfInterest;
                schedule[_contract.NbOfInstallments - 1].CapitalRepayment += totalAmount - sumOfPrincipal;
            }

            if (_contract.Product.RoundingType == ORoundingType.Begin)
            {
                schedule[0].CapitalRepayment += totalInterest - sumOfInterest;
                schedule[0].CapitalRepayment += totalAmount - sumOfPrincipal;
            }

            return schedule;
        }
    }
}
