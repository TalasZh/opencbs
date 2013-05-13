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
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.InterestRepayment;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Flat
{
    /// <summary>
    /// Summary description for FlateKeepExpectedInstallments.
    /// </summary>
    [Serializable]
    public class FlateKeepExpectedInstallments : IRepayNextInstallments
    {
        private readonly Loan _contract;
        private readonly RepayFeesStrategy _methodToRepayFees;
        private readonly RepayCommisionStrategy _methodToRepayCommission;
        private readonly RepayInterestStrategy _methodToRepayInterest;
        public List<Installment> _paidInstallments;

        public List<Installment> PaidInstallments
        {
            get { return _paidInstallments; }
            set { _paidInstallments = value; }
        }

        public FlateKeepExpectedInstallments(Loan contract, CreditContractOptions pCCO)
        {
            _contract = contract;
            _paidInstallments = new List<Installment>();
            _methodToRepayInterest = new RepayInterestStrategy(pCCO);
            _methodToRepayFees = new RepayFeesStrategy(pCCO);
            _methodToRepayCommission = new RepayCommisionStrategy(pCCO);
        }

        public void RepayNextInstallments(ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency interestPrepayment, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            if (amountPaid == 0) return;
            for(int i = 0 ; i < _contract.NbOfInstallments ; i++)
            {
                Installment installment = _contract.GetInstallment(i);
                if (!installment.IsRepaid && amountPaid > 0)
                {
                    //commission
                    _methodToRepayCommission.RepayCommission(installment, ref amountPaid, ref commissionsEvent);
                    if (amountPaid == 0) break;

                    //penalty
                    _methodToRepayFees.RepayFees(installment, ref amountPaid, ref feesEvent);
                    if (amountPaid == 0) break;

                    //Interests
                    if (amountPaid == 0) return;
                    _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref interestEvent,ref interestPrepayment);

                    //principal
                    if (amountPaid == 0)
                    {
                        _paidInstallments.Add(installment);
                        return;
                    }

                    OCurrency principalHasToPay = installment.CapitalRepayment - installment.PaidCapital;

                    if (AmountComparer.Compare(amountPaid, principalHasToPay) < 0)
                    {
                        installment.PaidCapital += amountPaid;
                        installment.PaidCapital = Math.Round(installment.PaidCapital.Value, 2);
                        principalEvent += amountPaid;
                        amountPaid = 0;
                    }
                    else if (AmountComparer.Compare(amountPaid, principalHasToPay) > 0)
                    {
                        installment.PaidCapital = installment.CapitalRepayment;
                        amountPaid -= principalHasToPay;
                        principalEvent += principalHasToPay;
                    }
                    else
                    {
                        installment.PaidCapital += amountPaid;
                        installment.PaidCapital = Math.Round(installment.PaidCapital.Value, 2);
                        principalEvent += amountPaid;
                        amountPaid = 0;
                    }

                    _paidInstallments.Add(installment);
                }
            }
        }
    }
}
