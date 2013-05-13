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

namespace OpenCBS.CoreDomain.Contracts.Loans.Installments
{
    /// <summary>
    /// Summary description for ExoticInstallment.
    /// </summary>
    [Serializable]
    public class ExoticInstallment
    {
        public ExoticInstallment()
        {}

        public ExoticInstallment(int pNumber, double pPincipalCoeff, double? pInterestCoeff)
        {
            Number = pNumber;
            PrincipalCoeff = pPincipalCoeff;
            InterestCoeff = pInterestCoeff;
        }

        public int Number { get; set; }
        public double PrincipalCoeff { get; set; }

        /// <summary>
        /// the interests coefficient refers to the interests to pay for one period and not the total interests
        /// </summary>
        public double? InterestCoeff { get; set; }
    }
}
