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
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Declining
{
    /// <summary>
    /// Summary description for DecliningExoticKeepNotExpectedInstallmentsWithNoCents.
    /// </summary>
    [Serializable]
    public class DecliningExoticKeepNotExpectedInstallmentsWithNoCents : IRepayNextInstallments
    {
        private readonly Loan _contract;
        public List<Installment> _paidInstallments;

        public List<Installment> PaidInstallments
        {
            get { return _paidInstallments; }
            set { _paidInstallments = value; }
        }

        public DecliningExoticKeepNotExpectedInstallmentsWithNoCents(Loan contract)
        {
            _paidInstallments = new List<Installment>();
            _contract = contract;
        }

        /// <summary>
        /// This method recalculates installments for a declining rate exotic loan in case of over payment.
        /// </summary>
        public void RepayNextInstallments(ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency interestPrepayment, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            OCurrency totalAmount = 0;
            bool firstInstallmentToRepay = false;
            double baseTocalculatePrincipalCoeff = 0;

            OCurrency principalForLastInstallment = 0;
            OCurrency interestForLastInstallment = 0;

            if(amountPaid > 0)
            {
                for (int i = 0; i < _contract.NbOfInstallments; i++)
                {
                    Installment installment = _contract.GetInstallment(i);
                    if (installment.IsRepaid && installment.Number > _contract.GracePeriod)
                        baseTocalculatePrincipalCoeff += _contract.Product.ExoticProduct.GetExoticInstallment(i - _contract.GracePeriod.Value).PrincipalCoeff;
                }

                baseTocalculatePrincipalCoeff = 1 - baseTocalculatePrincipalCoeff;

                for(int i = 0;i < _contract.NbOfInstallments;i++)
                {
                    Installment installment = _contract.GetInstallment(i);
                    if (!installment.IsRepaid && installment.Number <= _contract.GracePeriod)  //during Grace Period
                    {
                        installment.OLB -= amountPaid;
                        installment.CapitalRepayment = 0;
                        installment.InterestsRepayment = installment.OLB * Convert.ToDecimal(_contract.InterestRate);
                    }
                    else if(!installment.IsRepaid && !firstInstallmentToRepay)
                    {
                        ExoticInstallment exoticInstallment = _contract.Product.ExoticProduct.GetExoticInstallment(i - _contract.GracePeriod.Value);
                        firstInstallmentToRepay = true;
                        installment.OLB -= amountPaid;
                        totalAmount = installment.OLB;

                        OCurrency tempCapital = totalAmount.Value * Convert.ToDecimal(exoticInstallment.PrincipalCoeff / baseTocalculatePrincipalCoeff);
                        OCurrency tempInterest = installment.OLB.Value * Convert.ToDecimal(_contract.InterestRate);
                        installment.CapitalRepayment = Math.Truncate(tempCapital.Value);
                        installment.InterestsRepayment = Math.Truncate(tempInterest.Value);

                        principalForLastInstallment += tempCapital - installment.CapitalRepayment;
                        interestForLastInstallment += tempInterest - installment.InterestsRepayment;
                    }
                    else if(!installment.IsRepaid)
                    {
                        ExoticInstallment exoticInstallment = _contract.Product.ExoticProduct.GetExoticInstallment(i - _contract.GracePeriod.Value);
                        Installment _installment = _contract.GetInstallment(i - 1);
                        installment.OLB  = _installment.OLB - _installment.CapitalRepayment;

                        OCurrency tempCapital = totalAmount.Value * Convert.ToDecimal(exoticInstallment.PrincipalCoeff / baseTocalculatePrincipalCoeff);
                        OCurrency tempInterest = installment.OLB.Value * Convert.ToDecimal(_contract.InterestRate);
                        installment.CapitalRepayment = Math.Truncate(tempCapital.Value);
                        installment.InterestsRepayment = Math.Truncate(tempInterest.Value);

                        principalForLastInstallment += tempCapital - installment.CapitalRepayment;
                        interestForLastInstallment += tempInterest - installment.InterestsRepayment;
                    }

                    _paidInstallments.Add(installment);
                }

                Installment lastInstallment = _contract.GetInstallment((_contract.NbOfInstallments - 1));
                lastInstallment.CapitalRepayment += principalForLastInstallment;
                lastInstallment.InterestsRepayment += interestForLastInstallment;

                lastInstallment.CapitalRepayment = Math.Round(lastInstallment.CapitalRepayment.Value, 0);
                lastInstallment.InterestsRepayment = Math.Round(lastInstallment.InterestsRepayment.Value, 0);

                principalEvent += amountPaid;
                amountPaid = 0;
            }
        }
    }
}
