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
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment
{
    [Serializable]
    public class RepaymentMethod
    {
        private readonly Loan _contract;
        private readonly CreditContractOptions _cco;
        public List<Installment> PaidIstallments;

        public RepaymentMethod(Loan pContract, CreditContractOptions pCCO)
        {
            _contract = pContract;
            _cco = pCCO;
            PaidIstallments = new List<Installment>();
        }

        public void Repay(ref OCurrency pAmountPaid, ref OCurrency pFeesEvent, ref OCurrency pCommissionsEvent, ref OCurrency pInterestEvent)
        {
            if (_cco.CancelFees && (_cco.ManualFeesAmount + _cco.ManualCommissionAmount) >= 0)
            {
                pCommissionsEvent = _cco.ManualCommissionAmount;
                pFeesEvent = _cco.ManualFeesAmount;

                if (AmountComparer.Compare(pAmountPaid, _cco.ManualFeesAmount + _cco.ManualCommissionAmount) > 0)
                {
                    //pAmountPaid -= _cco.ManualFeesAmount + _cco.ManualCommissionAmount;
                }
                else
                    pAmountPaid = 0;
            }

            if (_cco.KeepExpectedInstallments)
            {
                if (_cco.CancelInterests && _cco.ManualInterestsAmount >= 0)
                {
                    OCurrency manualInterestAmount = _cco.ManualInterestsAmount;
                    for(int i = 0;i<_contract.NbOfInstallments;i++)
                    {
                        Installment installment = _contract.GetInstallment(i);
                        if(!installment.IsRepaid && pAmountPaid > 0 && manualInterestAmount > 0)
                        {
                            OCurrency interestHasToPay = installment.InterestsRepayment - installment.PaidInterests;
                            if (AmountComparer.Compare(manualInterestAmount,interestHasToPay) > 0)
                            {
                                pInterestEvent += interestHasToPay;
                                installment.PaidInterests = installment.InterestsRepayment;
                                pAmountPaid -= interestHasToPay;
                                manualInterestAmount -= interestHasToPay;
                            }
                            else
                            {
                                pInterestEvent += manualInterestAmount;
                                installment.PaidInterests += manualInterestAmount;
                                pAmountPaid -= manualInterestAmount;
                                manualInterestAmount = 0;
                            }

                            PaidIstallments.Add(installment);
                        }
                    }
                }
            }
        }
    }
}
