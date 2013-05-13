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

using OpenCBS.Enums;
using System;

namespace OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments
{
    /// <summary>
    /// Summary description for CalculateInstallmentsOption.
    /// </summary>
    [Serializable]
    public class CalculateInstallmentsOptions
    {
        private readonly OLoanTypes _loanType;
        private readonly bool _isExotic;
        private readonly Loan _contract;
        private readonly bool _changeDate;
        private readonly DateTime _startDate;

        public CalculateInstallmentsOptions(DateTime pStartDate, OLoanTypes pLoanType, bool pIsExotic, Loan pContract, bool pChangeDate)
        {
            _loanType = pLoanType;
            _isExotic = pIsExotic;
            _contract = pContract;
            _changeDate = pChangeDate;
            _startDate = pStartDate;
        }
		
        public Loan Contract
        {
            get { return _contract; }
        }

        public bool ChangeDate
        {
            get { return _changeDate; }
        }

        public OLoanTypes LoanType
        {
            get { return _loanType; }
        }

        public bool IsExotic
        {
            get { return _isExotic; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }

        }
    }
}
