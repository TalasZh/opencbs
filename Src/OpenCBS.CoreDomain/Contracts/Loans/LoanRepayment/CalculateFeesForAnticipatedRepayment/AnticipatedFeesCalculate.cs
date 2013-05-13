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
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment.BaseToCalculateFeesForAnticipatedRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment
{
    /// <summary>
    /// Summary description for AnticipatedFeesCalculate.
    /// </summary>
    [Serializable]
    public class AnticipatedFeesCalculate : ICalculateFeesForAnticipatedRepayment
    {
        private readonly CalculationBaseForAnticipatedFees _bTcffar;
        private readonly Loan _loan;

        public AnticipatedFeesCalculate(CreditContractOptions pCCO,Loan pContract)
        {
            _loan = pContract;
            _bTcffar = new CalculationBaseForAnticipatedFees(pCCO,pContract);
        }

        public void CalculateFees(DateTime pDate, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent)
        {
            OCurrency fees = _loan.UseCents 
                                        ? Math.Round(_bTcffar.CalculateFees(pDate).Value,2) 
                                        : Math.Round(_bTcffar.CalculateFees(pDate).Value, 0);

            if(AmountComparer.Compare( fees, pAmountPaid) > 0)
            {
                pFeesEvent += pAmountPaid;
                pAmountPaid = 0;
            }
            else
            {
                pAmountPaid -= fees;
                pFeesEvent += fees;
            }
        }
    }
}
