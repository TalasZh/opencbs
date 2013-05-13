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


namespace OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Declining
{
    /// <summary>
    /// Summary description for FixedInstallmentStrategy.
    /// </summary>
    [Serializable]
    public class FixedInstallmentStrategy : ICalculateInstallments
    {
        private readonly OCurrency _startAmount;
        private readonly int _numberOfInstallmentsToPay;
        private readonly Loan _contract;
        private readonly int _roundingPoint;
        private readonly ApplicationSettings _generalSettings;

        public FixedInstallmentStrategy(Loan pContract, OCurrency pStartAmount, int pNumberOfInstalments, ApplicationSettings pGeneralSettings)
        {
            _contract = pContract;
            _startAmount = pStartAmount;
            _numberOfInstallmentsToPay = pNumberOfInstalments;
            _roundingPoint = pContract.UseCents ? 2 : 0;
            _generalSettings = pGeneralSettings;
        }

        public List<Installment> CalculateInstallments(bool pChangeDate)
        {
            List<Installment> schedule = new List<Installment>();
            OCurrency olb = _startAmount;
            OCurrency VPM = 0;

            for (int number = 1; number <= _contract.NbOfInstallments; number++)
            {
                Installment installment;

                if (pChangeDate)
                {
                    installment = new Installment {Number = number};
                    installment.ExpectedDate = _contract.CalculateInstallmentDate(_contract.AlignDisbursementDate,
                                                                                  installment.Number);
                }
                else
                {
                    installment = _contract.GetInstallment(number - 1);
                }

                installment.FeesUnpaid = 0;
                installment.InterestsRepayment = Math.Round(olb.Value*Convert.ToDecimal(_contract.InterestRate),
                                                            _roundingPoint, MidpointRounding.AwayFromZero);

                if (installment.Number <= _contract.GracePeriod)
                {
                    if (!_contract.Product.ChargeInterestWithinGracePeriod)
                        installment.InterestsRepayment = 0;

                    installment.CapitalRepayment = 0;
                }
                else
                {
                    OCurrency installmentVpm = Math.Round(_contract.VPM(olb, _numberOfInstallmentsToPay - installment.Number + 1).Value,
                                   _roundingPoint, MidpointRounding.AwayFromZero);

                    if ((_contract.Product.RoundingType == ORoundingType.End ||
                             _contract.Product.RoundingType == ORoundingType.Begin) && VPM == 0)
                        {
                            VPM = installmentVpm;
                        }
                        else if (_contract.Product.RoundingType == ORoundingType.Approximate)
                        {
                            VPM = installmentVpm;
                        }

                    OCurrency capital =
                        Math.Round((VPM.Value - Math.Round(olb.Value * Convert.ToDecimal(_contract.InterestRate), _roundingPoint)),
                            _roundingPoint, MidpointRounding.AwayFromZero);

                    installment.CapitalRepayment = capital;

                    //to count the difference in case End and Beging rounding type
                    OCurrency installemtDiff = VPM - (installment.CapitalRepayment + installment.InterestsRepayment);
                    
                    if(installemtDiff != 0)
                        installment.CapitalRepayment += installemtDiff;

                    olb -= capital + installemtDiff;
                    ///////////////////////////////////////////////////////////////
                }

                if (installment.Number == 1 && _generalSettings.PayFirstInterestRealValue)
                {
                    installment.InterestsRepayment =
                        Math.Round(
                            installment.InterestsRepayment.Value/_contract.NbOfDaysInTheInstallment*
                            (_contract.FirstInstallmentDate - _contract.StartDate).Days, _roundingPoint,
                            MidpointRounding.AwayFromZero);
                }

                if (pChangeDate)
                {
                    schedule.Add(installment);
                }
            }

            if (_contract.Product.RoundingType == ORoundingType.End && olb != 0)
            {
                schedule[_contract.NbOfInstallments - 1].CapitalRepayment += olb;
            }

            if (_contract.Product.RoundingType == ORoundingType.Begin && olb != 0)
            {
                schedule[0].CapitalRepayment += olb;
            }

            olb = _startAmount;
            OCurrency capetalRepayment = 0;
            //OLB Calculation
            foreach (Installment installment in schedule)
            {
                olb -= capetalRepayment;
                installment.OLB = olb;
                capetalRepayment = installment.CapitalRepayment;
            }

            return schedule;
        }
    }
}
