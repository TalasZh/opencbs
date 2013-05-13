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

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Flat
{
    /// <summary>
    /// Summary description for FlatExoticKeepNotExpectedInstallments.
    /// </summary>
    [Serializable]
    public class FlatExoticKeepNotExpectedInstallmentsWithNoCents: IRepayNextInstallments
    {
        private readonly Loan _contract;
        public List<Installment> _paidInstallments;

        public List<Installment> PaidInstallments
        {
            get { return _paidInstallments; }
            set { _paidInstallments = value; }
        }

        public FlatExoticKeepNotExpectedInstallmentsWithNoCents(Loan contract)
        {
            _paidInstallments = new List<Installment>();
            _contract = contract;
        }

        public void RepayNextInstallments(ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency interestPrepayment, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            if (amountPaid <= 0) return;
            OCurrency totalInterest = 0;
            OCurrency totalPrincipal = 0;
            double nbInterest = 0;
            double nbPrincipal = 0;

            foreach (Installment installment in _contract.InstallmentList)
            {
                if(installment.InterestsRepayment - installment.PaidInterests > 0)
                {
                    totalInterest += (installment.InterestsRepayment - installment.PaidInterests);
                    if (!_contract.GracePeriod.HasValue)
                    {
                        nbInterest += _contract.Product.ExoticProduct.GetExoticInstallment(installment.Number - 1).InterestCoeff.Value;
                    }
                    else if (installment.Number > _contract.GracePeriod.Value)
                    {
                        nbInterest += _contract.Product.ExoticProduct.GetExoticInstallment(installment.Number - 1 - _contract.GracePeriod.Value).InterestCoeff.Value;
                    }							
                }

                if(installment.CapitalRepayment - installment.PaidCapital > 0)
                {
                    totalPrincipal += (installment.CapitalRepayment - installment.PaidCapital);
                    if (!_contract.GracePeriod.HasValue)
                    {
                        nbPrincipal += _contract.Product.ExoticProduct.GetExoticInstallment(installment.Number - 1).PrincipalCoeff;
                    }
                    else if (installment.Number > _contract.GracePeriod.Value)
                    {
                        nbPrincipal += _contract.Product.ExoticProduct.GetExoticInstallment(installment.Number - 1 - _contract.GracePeriod.Value).PrincipalCoeff;
                    }
                }
            }

            if(AmountComparer.Compare(amountPaid,totalInterest) > 0)
            {
                amountPaid -= totalInterest;
                interestEvent += totalInterest;
                interestPrepayment += totalInterest;
                totalInterest = 0;
            }
            else
            {
                totalInterest -= amountPaid;
                interestEvent += amountPaid;
                interestPrepayment += amountPaid;
                amountPaid = 0;
            }

            if(AmountComparer.Compare(amountPaid,totalPrincipal) > 0)
            {
                amountPaid -= totalPrincipal;
                principalEvent += totalPrincipal;
                totalPrincipal = 0;
            }
            else
            {
                totalPrincipal -= amountPaid;
                principalEvent += amountPaid;
                amountPaid = 0;
            }

            if(totalInterest != 0)
                totalInterest = totalInterest / (Convert.ToDecimal(nbInterest) * 10);

            if(totalPrincipal != 0)
                totalPrincipal = totalPrincipal / (Convert.ToDecimal(nbPrincipal) * 10);

            OCurrency interestForLastInstallment = 0;
            OCurrency capitalForLastInstallment = 0;

            for (int i = 0; i < _contract.NbOfInstallments; i++)
            {
                Installment installment = _contract.GetInstallment(i);
                if(!installment.IsRepaid)
                {
                    ExoticInstallment exoticInstallment;

                    if (!_contract.GracePeriod.HasValue)
                    {
                        exoticInstallment = _contract.Product.ExoticProduct.GetExoticInstallment(i);

                        OCurrency tempInterest = Convert.ToDecimal(exoticInstallment.InterestCoeff)*totalInterest*(double)10;
                        OCurrency tempCapital = Convert.ToDecimal(exoticInstallment.PrincipalCoeff)*totalPrincipal*(double)10;

                        installment.InterestsRepayment = Math.Truncate(tempInterest.Value);
                        installment.CapitalRepayment = Math.Truncate(tempCapital.Value);

                        interestForLastInstallment += tempInterest - installment.InterestsRepayment;
                        capitalForLastInstallment += tempCapital - installment.CapitalRepayment;

                    }
                    else if (installment.Number > _contract.GracePeriod.Value)
                    {
                        exoticInstallment = _contract.Product.ExoticProduct.GetExoticInstallment(i - _contract.GracePeriod.Value);

                        OCurrency tempInterest = Convert.ToDecimal(exoticInstallment.InterestCoeff) * totalInterest * (double)10;
                        OCurrency tempCapital = Convert.ToDecimal(exoticInstallment.PrincipalCoeff) * totalPrincipal * (double)10;

                        installment.InterestsRepayment = Math.Truncate(tempInterest.Value);
                        installment.CapitalRepayment = Math.Truncate(tempCapital.Value);

                        interestForLastInstallment += tempInterest - installment.InterestsRepayment;
                        capitalForLastInstallment += tempCapital - installment.CapitalRepayment;
                    }
                    else
                    {
                        OCurrency tempInterest = 0;
                        if (totalInterest != 0)
                            tempInterest = totalPrincipal * Convert.ToDecimal(_contract.InterestRate);

                        installment.InterestsRepayment = Math.Truncate(tempInterest.Value);
                        installment.CapitalRepayment = 0;

                        interestForLastInstallment += tempInterest - installment.InterestsRepayment;
                    }
                }

                _paidInstallments.Add(installment);
            }

            Installment lastInstallment = _contract.GetInstallment(_contract.NbOfInstallments - 1);
            lastInstallment.InterestsRepayment += interestForLastInstallment;
            lastInstallment.CapitalRepayment += capitalForLastInstallment;
            lastInstallment.InterestsRepayment = lastInstallment.InterestsRepayment;
            lastInstallment.CapitalRepayment = lastInstallment.CapitalRepayment;

            lastInstallment.InterestsRepayment = Math.Round(lastInstallment.InterestsRepayment.Value, 0);
            lastInstallment.CapitalRepayment = Math.Round(lastInstallment.CapitalRepayment.Value, 0);
        }
    }
}
