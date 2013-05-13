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
using OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Declining;
using OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Flat;
using OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Interfaces;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments
{
    /// <summary>
    /// Summary description for CalculateInstallmentsStrategy.
    /// </summary>
    [Serializable]
    public class CalculateInstallmentsStrategy
    {
        private readonly ICalculateInstallments _iCi;
        private readonly ApplicationSettings _generalSettings;

        public CalculateInstallmentsStrategy(CalculateInstallmentsOptions pCio, OCurrency pStartAmount, int pNumberOfInstallments, ApplicationSettings pGeneralSettings)
        {
            _generalSettings = pGeneralSettings;
            OCurrency initialOlbOfContractBeforeRescheduling = Loan.InitialOlbOfContractBeforeRescheduling;

            if (pCio.IsExotic)
            {
                if (pCio.LoanType == OLoanTypes.Flat)
                    _iCi = new Flat.ExoticStrategy(pCio.Contract, _generalSettings);
                else
                    _iCi = new Declining.ExoticStrategy(pCio.Contract, _generalSettings);
            }
            else
            {
                if (pCio.Contract.InterestRate == 0)
                {
                    _iCi = new FlatStrategy(pCio.Contract, _generalSettings, initialOlbOfContractBeforeRescheduling);
                }
                else
                {
                    switch (pCio.LoanType)
                    {
                        case OLoanTypes.Flat:
                            _iCi = new FlatStrategy(pCio.Contract, _generalSettings, initialOlbOfContractBeforeRescheduling);
                            break;
                        case OLoanTypes.DecliningFixedInstallments:
                            _iCi = new FixedInstallmentStrategy(pCio.Contract, pStartAmount, pNumberOfInstallments,
                                                                _generalSettings);
                            break;
                        case OLoanTypes.DecliningFixedPrincipal:
                            _iCi = new FixedPrincipalStrategy(pCio.Contract, _generalSettings);
                            break;
                        case OLoanTypes.DecliningFixedPrincipalWithRealInterest:
                            _iCi = new FixedPrincipalWithRealInterest(pCio.Contract, _generalSettings);
                            break;
                    }
                }
            }
        }

        public List<Installment> CalculateInstallments(bool pChangeDate)
        {
            return _iCi.CalculateInstallments(pChangeDate);
        }
    }
}
