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
    /// Summary description for DecliningExoticKeepNotExpectedInstallmentsWithCents.
    /// </summary>
    [Serializable]
    public class DecliningExoticKeepNotExpectedInstallmentsWithCents : IRepayNextInstallments
    {
        private readonly Loan _contract;

        public List<Installment> _paidInstallments;

        public List<Installment> PaidInstallments
        {
            get { return _paidInstallments; }
            set { _paidInstallments = value; }
        }

        public DecliningExoticKeepNotExpectedInstallmentsWithCents(Loan contract)
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

            if(amountPaid > 0)
            {
                for (int i = 0; i < _contract.NbOfInstallments; i++)
                {
                    Installment installment = _contract.GetInstallment(i);
                    if (installment.IsRepaid &&  installment.Number > _contract.GracePeriod)
                        baseTocalculatePrincipalCoeff += _contract.Product.ExoticProduct.GetExoticInstallment(i - _contract.GracePeriod.Value).PrincipalCoeff;
                }

                baseTocalculatePrincipalCoeff = 1 - baseTocalculatePrincipalCoeff;

                for(int i = 0;i < _contract.NbOfInstallments;i++)
                {
                    Installment installment = _contract.GetInstallment(i);
                    if(!installment.IsRepaid && installment.Number <= _contract.GracePeriod)  //during Grace Period
                    {
                        installment.OLB -= amountPaid;
                        installment.CapitalRepayment = 0;
                        installment.InterestsRepayment = Math.Round(installment.OLB.Value * Convert.ToDecimal(_contract.InterestRate),2);
                    }
                    else if(!installment.IsRepaid && !firstInstallmentToRepay)
                    {
                        ExoticInstallment exoticInstallment = _contract.Product.ExoticProduct.GetExoticInstallment(i - _contract.GracePeriod.Value);
                        firstInstallmentToRepay = true;
                        installment.OLB -= amountPaid;
                        totalAmount = installment.OLB;

                        installment.CapitalRepayment = Math.Round(totalAmount.Value * Convert.ToDecimal(exoticInstallment.PrincipalCoeff / baseTocalculatePrincipalCoeff),2);
                        installment.InterestsRepayment = Math.Round(installment.OLB.Value * Convert.ToDecimal(_contract.InterestRate),2);
                    }
                    else if(!installment.IsRepaid)
                    {
                        ExoticInstallment exoticInstallment = _contract.Product.ExoticProduct.GetExoticInstallment(i - _contract.GracePeriod.Value);
                        Installment _installment = _contract.GetInstallment(i - 1);
                        installment.OLB  = _installment.OLB - _installment.CapitalRepayment;

                        installment.CapitalRepayment = Math.Round(totalAmount.Value * Convert.ToDecimal(exoticInstallment.PrincipalCoeff / baseTocalculatePrincipalCoeff),2);
                        installment.InterestsRepayment = Math.Round(installment.OLB.Value * Convert.ToDecimal(_contract.InterestRate),2); 
                    }

                    _paidInstallments.Add(installment);
                }

                principalEvent += amountPaid;
                amountPaid = 0;
            }
        }
    }
}
