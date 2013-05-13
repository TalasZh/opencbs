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


namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Flat
{
    /// <summary>
    /// Summary description for FlateKeepNotExpectedInstallments.
    /// </summary>
    [Serializable]
    public class FlateKeepNotExpectedInstallmentsWithCents : IRepayNextInstallments
    {
        private readonly Loan _contract;
        private readonly ApplicationSettings _generalSettings;
        public List<Installment> _paidInstallments;

        public List<Installment> PaidInstallments
        {
            get { return _paidInstallments; }
            set { _paidInstallments = value; }
        }

        public FlateKeepNotExpectedInstallmentsWithCents(Loan contract, ApplicationSettings pGeneralSettings)
        {
            _paidInstallments = new List<Installment>();
            _contract = contract;
            _generalSettings = pGeneralSettings;
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
                    if (_generalSettings.AccountingProcesses == OAccountingProcesses.Accrual)
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
                        _CalculateInstallments(installment.Number, startAmount, _contract.NbOfInstallments - _contract.GracePeriod.Value);
                    }
                    else
                    {
                        _CalculateInstallments(installment.Number, startAmount,_contract.NbOfInstallments - installment.Number + 1);
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

        private void _CalculateInstallments(int pStartInstallment, OCurrency pStartAmount, int pNumberOfInstallmentsToPay)
        {
            OCurrency olb = pStartAmount;
            OCurrency sumOfPrincipal = 0;
            int installmentNumber = 0;

            for (int number = pStartInstallment; number <= _contract.NbOfInstallments; number++)
            {
                Installment installment = _contract.GetInstallment(number - 1);

                installment.InterestsRepayment = Math.Round(pStartAmount.Value * Convert.ToDecimal(_contract.InterestRate),2);
                //in case TotalAnticipatedRepaymentFormerPrepaymentDayBeforeFlatCash 
                if ((pStartAmount == 0) && (installment.PaidInterests !=0))
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
                    installment.CapitalRepayment = decimal.Round((pStartAmount.Value - sumOfPrincipal.Value) / (pNumberOfInstallmentsToPay - installmentNumber), 2);
                    sumOfPrincipal += installment.CapitalRepayment;
                    installmentNumber++;
                }

                installment.OLB = olb;
                olb -= installment.CapitalRepayment;
            }
        }
    }
}
