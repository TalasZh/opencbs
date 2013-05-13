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
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Declining
{
    /// <summary>
    /// Summary description for DecliningKeepNotExpectedInstallments.
    /// </summary>
    [Serializable]
    public class DecliningKeepNotExpectedInstallments : IRepayNextInstallments
    {
        private readonly Loan _contract;
        private User _user;
        private readonly ApplicationSettings _generalSettings;

        public List<Installment> _paidInstallments;

        public List<Installment> PaidInstallments
        {
            get { return _paidInstallments; }
            set { _paidInstallments = value; }
        }

        public DecliningKeepNotExpectedInstallments(Loan contract, User pUser, ApplicationSettings pGeneralSettings)
        {
            _user = pUser;
            _generalSettings = pGeneralSettings;
            _contract = contract;
            _paidInstallments = new List<Installment>();
        }

        public void RepayNextInstallments(ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency interestPrepayment, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            int installmentNumber = 0;
            OCurrency interest = 0;
            _contract.InstallmentList.Sort((x, y) => x.ExpectedDate.CompareTo(y.ExpectedDate));
            foreach (Installment installment in _contract.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    if(_generalSettings.AccountingProcesses == OAccountingProcesses.Accrual)
                    {
                        interest = 0;
                        amountPaid -= interest;
                        interestEvent += interest;
                        installmentNumber = installment.Number;
                    }

                    principalEvent += amountPaid;
                    OCurrency startAmount = _contract.GetInstallment(installment.Number - 1).OLB - amountPaid;
                    amountPaid = 0;

                    if (installment.Number <= _contract.GracePeriod.Value)
                    {
                        if (_contract.Product.LoanType == OLoanTypes.DecliningFixedInstallments)
                        {
                            if (_contract.UseCents)
                                _CalculateDecliningFixedInstallmentsWithCents(installment.Number, startAmount, _contract.NbOfInstallments - _contract.GracePeriod.Value);
                            else
                                _CalculateDecliningFixedInstallmentsWithNoCents(installment.Number, startAmount, _contract.NbOfInstallments - _contract.GracePeriod.Value);
                        }
                        else
                        {
                            if (_contract.UseCents)
                                _CalculateDecliningFixedPrincipalWithCents(installment.Number, startAmount, _contract.NbOfInstallments - _contract.GracePeriod.Value);
                            else
                                _CalculateDecliningFixedPrincipalWithNoCents(installment.Number, startAmount, _contract.NbOfInstallments - _contract.GracePeriod.Value);
                        }
                    }
                    else
                    {
                        if (_contract.Product.LoanType == OLoanTypes.DecliningFixedInstallments)
                        {
                            if (_contract.UseCents)
                                _CalculateDecliningFixedInstallmentsWithCents(installment.Number, startAmount,_contract.NbOfInstallments -installment.Number + 1);
                            else
                                _CalculateDecliningFixedInstallmentsWithNoCents(installment.Number, startAmount,_contract.NbOfInstallments -installment.Number + 1);
                        }
                        else
                        {
                            if(_contract.UseCents)
                                _CalculateDecliningFixedPrincipalWithCents(installment.Number, startAmount, _contract.NbOfInstallments - installment.Number + 1);
                            else
                                _CalculateDecliningFixedPrincipalWithNoCents(installment.Number, startAmount, _contract.NbOfInstallments - installment.Number + 1);
                        }
                    }
                    break;
                }

                _paidInstallments.Add(installment);
            }

            if (installmentNumber != 0)
            {
                _contract.GetInstallment(installmentNumber - 1).InterestsRepayment = interest; 
                _contract.GetInstallment(installmentNumber - 1).PaidInterests = interest;
            }
        }

        private void _CalculateDecliningFixedPrincipalWithCents(int pStartInstallment, OCurrency   pStartAmount,int pNumberOfInstallmentsToPay)
        {
            OCurrency olb = pStartAmount;
            OCurrency principal = olb.Value / pNumberOfInstallmentsToPay;

            for (int number = pStartInstallment; number <= _contract.NbOfInstallments; number++)
            {
                Installment installment = _contract.GetInstallment(number - 1);

                installment.InterestsRepayment = Math.Round(olb.Value * Convert.ToDecimal(_contract.InterestRate),2);

                if (installment.Number <= _contract.GracePeriod)
                {
                    installment.CapitalRepayment = 0;
                    installment.OLB = olb;
                }
                else
                {
                    installment.CapitalRepayment = Math.Round(principal.Value,2);
                }

                installment.OLB = olb;
                olb -= installment.CapitalRepayment;
            }
        }

        private void _CalculateDecliningFixedPrincipalWithNoCents(int pStartInstallment, OCurrency pStartAmount,int pNumberOfInstallmentsToPay)
        {
            OCurrency olb = pStartAmount;
            OCurrency principalForLastInstallment = 0;
            OCurrency interestForLastInstallment = 0;
            OCurrency principal = olb /(OCurrency) pNumberOfInstallmentsToPay;

            principal = Math.Truncate(principal.Value);

            for (int number = pStartInstallment; number <= _contract.NbOfInstallments; number++)
            {
                Installment installment = _contract.GetInstallment(number - 1);

                OCurrency tempInterest = olb * Convert.ToDecimal(_contract.InterestRate);
                installment.InterestsRepayment = Math.Truncate(tempInterest.Value);

                if (installment.Number <= _contract.GracePeriod)
                {
                    installment.CapitalRepayment = 0;
                    installment.OLB = olb;
                    interestForLastInstallment += tempInterest - installment.InterestsRepayment;
                }
                else
                {
                    interestForLastInstallment += tempInterest - installment.InterestsRepayment;
                    installment.CapitalRepayment = principal;
                }

                installment.OLB = olb;
                olb -= installment.CapitalRepayment;
                principalForLastInstallment += installment.CapitalRepayment;
            }

            Installment lastInstallment = _contract.GetInstallment((_contract.NbOfInstallments - 1));
            lastInstallment.CapitalRepayment += pStartAmount - principalForLastInstallment;
            lastInstallment.InterestsRepayment += interestForLastInstallment;

            lastInstallment.CapitalRepayment = Math.Round(lastInstallment.CapitalRepayment.Value, 0);
            lastInstallment.InterestsRepayment = Math.Round(lastInstallment.InterestsRepayment.Value, 0);
        }

        private void _CalculateDecliningFixedInstallmentsWithCents(int startInstallment, OCurrency startAmount,int numberOfInstallmentsToPay)
        {
            OCurrency olb = startAmount;
            OCurrency fixedTotalAmount = _contract.VPM(startAmount, numberOfInstallmentsToPay).Value;

            for (int number = startInstallment; number <= _contract.NbOfInstallments; number++)
            {
                Installment installment = _contract.GetInstallment(number - 1);
                OCurrency interest = olb.Value*Convert.ToDecimal(_contract.InterestRate);

                if (installment.PaidInterests == 0)
                {
                    installment.InterestsRepayment = Math.Round(interest.Value, 2);
                }
                else
                {
                    installment.InterestsRepayment = installment.PaidInterests;
                }

                if (installment.Number <= _contract.GracePeriod)
                {
                    installment.CapitalRepayment = 0;
                    installment.OLB = olb;
                }
                else
                {
                    installment.CapitalRepayment = Math.Round(fixedTotalAmount.Value - interest.Value,2);
                }

                installment.OLB = olb;
                olb -= installment.CapitalRepayment;
            }
        }

        private void _CalculateDecliningFixedInstallmentsWithNoCents(int startInstallment, OCurrency startAmount,int numberOfInstallmentsToPay)
        {
            OCurrency olb = startAmount;
            OCurrency principalForLastInstallment = 0;
            OCurrency interestForLastInstallment = 0;
            OCurrency fixedTotalAmount = _contract.VPM(startAmount, numberOfInstallmentsToPay);

            fixedTotalAmount = Math.Truncate(fixedTotalAmount.Value);

            for (int number = startInstallment; number <= _contract.NbOfInstallments; number++)
            {
                Installment installment = _contract.GetInstallment(number - 1);

                OCurrency tempInterest = olb * Convert.ToDecimal(_contract.InterestRate);
                installment.InterestsRepayment = Math.Truncate(tempInterest.Value);

                if (installment.Number <= _contract.GracePeriod)
                {
                    installment.CapitalRepayment = 0;
                    installment.OLB = olb;
                    interestForLastInstallment += tempInterest - installment.InterestsRepayment;
                }
                else
                {
                    interestForLastInstallment += tempInterest - installment.InterestsRepayment;
                    installment.CapitalRepayment = fixedTotalAmount - installment.InterestsRepayment;
                }

                installment.OLB = olb;
                olb -= installment.CapitalRepayment;
                principalForLastInstallment += installment.CapitalRepayment;
            }

            Installment lastInstallment = _contract.GetInstallment((_contract.NbOfInstallments - 1));
            lastInstallment.CapitalRepayment += startAmount - principalForLastInstallment;
            lastInstallment.InterestsRepayment += interestForLastInstallment;
            lastInstallment.CapitalRepayment = lastInstallment.CapitalRepayment;
            lastInstallment.InterestsRepayment = lastInstallment.InterestsRepayment;

            lastInstallment.CapitalRepayment = Math.Round(lastInstallment.CapitalRepayment.Value, 0);
            lastInstallment.InterestsRepayment = Math.Round(lastInstallment.InterestsRepayment.Value, 0);
        }
    }
}
