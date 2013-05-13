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
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment
{
    [Serializable]
    public class RepayCommisionStrategy
    {
        private readonly CreditContractOptions _pCCO;
        public RepayCommisionStrategy(CreditContractOptions pCCO)
        {
            _pCCO = pCCO;
        }

        public void RepayCommission(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pCommisionEvent)
        {
            if (!_pCCO.CancelFees)
                AutomaticRepayMethod(pInstallment, ref pAmountPaid, ref pCommisionEvent);
            else
                ManualRepayMethod(pInstallment, ref pAmountPaid, ref pCommisionEvent);
        }

        private static void AutomaticRepayMethod(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pCommisionEvent)
        {
            if (AmountComparer.Compare(pAmountPaid, pInstallment.CommissionsUnpaid) > 0)
            {
                pCommisionEvent += pInstallment.CommissionsUnpaid;
                pAmountPaid -= pInstallment.CommissionsUnpaid;
                pInstallment.PaidCommissions += pInstallment.CommissionsUnpaid;
                pInstallment.CommissionsUnpaid = 0;
            }
            else
            {
                pCommisionEvent += pAmountPaid;
                pInstallment.PaidCommissions += pAmountPaid;
                pInstallment.CommissionsUnpaid -= pAmountPaid;
                pAmountPaid = 0;
            }
        }

        private static void ManualRepayMethod(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pCommisionEvent)
        {
            pInstallment.PaidCommissions = pCommisionEvent;
            pInstallment.CommissionsUnpaid = 0;
            pAmountPaid -= pCommisionEvent;
            pCommisionEvent = 0;
            //Nothing to modify
        }
    }
}
