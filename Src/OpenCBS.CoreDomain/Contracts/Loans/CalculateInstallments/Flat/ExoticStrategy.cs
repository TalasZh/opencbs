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
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Flat
{
    /// <summary>
    /// Summary description for ExoticStrategy.
    /// </summary>
    [Serializable]
    public class ExoticStrategy : ICalculateInstallments
    {
        private readonly Loan _contract;
        private readonly int _roundingPoint;
        private readonly ApplicationSettings _generalSettings;

        public ExoticStrategy(Loan pContract, ApplicationSettings pGeneralSettings)
        {
            _roundingPoint = pContract.UseCents ? 2 : 0;
            _contract = pContract;
            _generalSettings = pGeneralSettings;
        }

        public List<Installment> CalculateInstallments(bool changeDate)
        {
            List<Installment> schedule = new List<Installment>();
            OCurrency olb = _contract.Amount;
            OCurrency totalInterestsRepayment = _contract.Amount * Convert.ToDecimal(_contract.InterestRate) * (_contract.NbOfInstallments - _contract.GracePeriod.Value);
            decimal decCr = 0;
            decimal decIr = 0;

            for (int number = 1; number <= _contract.NbOfInstallments; number++)
            {
                Installment installment = new Installment { Number = number, FeesUnpaid = 0, OLB = olb };

                if (changeDate)
                {
                    installment.ExpectedDate = _contract.CalculateInstallmentDate(_contract.AlignDisbursementDate, installment.Number);
                }
                else
                {
                    installment = _contract.GetInstallment(number - 1);
                }

                if (number <= _contract.GracePeriod)
                {
                    if (!_contract.Product.ChargeInterestWithinGracePeriod)
                        installment.InterestsRepayment = 0;

                    installment.CapitalRepayment = 0;
                    installment.InterestsRepayment = Math.Round(installment.OLB.Value * Convert.ToDecimal(_contract.InterestRate) + decIr, _roundingPoint);
                    decIr = _contract.Amount.Value * Convert.ToDecimal(_contract.InterestRate) - installment.InterestsRepayment.Value;
                }
                else
                {
                    ExoticInstallment exoInstallment = _contract.Product.ExoticProduct.GetExoticInstallment(number - _contract.GracePeriod.Value - 1);

                    OCurrency cr = Convert.ToDecimal(exoInstallment.PrincipalCoeff) * _contract.Amount.Value + decCr;
                    installment.CapitalRepayment = Math.Round(cr.Value, _roundingPoint);
                    decCr = cr.Value - installment.CapitalRepayment.Value;

                    OCurrency ir = totalInterestsRepayment.Value * Convert.ToDecimal(exoInstallment.InterestCoeff) + decIr;

                    installment.InterestsRepayment = Math.Round(ir.Value, _roundingPoint);
                    decIr = ir.Value - installment.InterestsRepayment.Value;
                }
                olb -= installment.CapitalRepayment.Value;

                if (installment.Number == 1 && _generalSettings.PayFirstInterestRealValue)
                {
                    installment.InterestsRepayment = Math.Round(installment.InterestsRepayment.Value / _contract.NbOfDaysInTheInstallment * (_contract.FirstInstallmentDate - _contract.StartDate).Days, _roundingPoint);
                }

                schedule.Add(installment);
            }

            return schedule;
        }
    }
}
