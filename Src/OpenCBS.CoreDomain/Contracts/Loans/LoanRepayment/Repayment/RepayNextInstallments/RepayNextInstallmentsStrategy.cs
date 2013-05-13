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
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Declining;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments.Flat;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayNextInstallments
{
    /// <summary>
    /// Summary description for RepayNextInstallmentsStrategy.
    /// </summary>
    [Serializable]
    public class RepayNextInstallmentsStrategy
    {
        private readonly IRepayNextInstallments _repayNextInstallments;
        public List<Installment> PaidInstallments{ get; set;}

        public RepayNextInstallmentsStrategy(Loan contract,CreditContractOptions cCO, User pUser, ApplicationSettings pGeneralSettings)
        {
            if(cCO.LoansType != OLoanTypes.Flat) //declining
            {
                if(cCO.KeepExpectedInstallments)
                    _repayNextInstallments = new DecliningKeepExpectedInstallments(contract, cCO);
                else
                {
                    if (contract.Product.ExoticProduct != null)
                    {
                        if(contract.UseCents)
                            _repayNextInstallments = new DecliningExoticKeepNotExpectedInstallmentsWithCents(contract);
                        else
                            _repayNextInstallments = new DecliningExoticKeepNotExpectedInstallmentsWithNoCents(contract);
                    }
				    //???????????????????????????????????????
                    else
                        _repayNextInstallments = new DecliningKeepNotExpectedInstallments(contract, pUser, pGeneralSettings);
                }
            }
            else //flat
            {
                if (cCO.KeepExpectedInstallments)
                {
                    _repayNextInstallments = new FlateKeepExpectedInstallments(contract, cCO);
                }
                else
                {
                    if (contract.Product.ExoticProduct != null)
                    {
                        if (contract.UseCents)
                        {
                            _repayNextInstallments = new FlatExoticKeepNotExpectedInstallmentsWithCents(contract);
                        }
                        else
                        {
                            _repayNextInstallments = new FlatExoticKeepNotExpectedInstallmentsWithNoCents(contract);
                        }
                    }

                    else
                    {   // ??????????????????????????????????????????????????????????????
                        if (contract.UseCents)
                            _repayNextInstallments = new FlateKeepNotExpectedInstallmentsWithCents(contract, pGeneralSettings);
                        else
                            _repayNextInstallments = new FlateKeepNotExpectedInstallmentsWithNoCents(contract, pGeneralSettings);
                    }
                }
            }
        }

        public void RepayNextInstallments(ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency interestPrepayment, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            _repayNextInstallments.RepayNextInstallments(ref amountPaid, ref interestEvent,ref interestPrepayment, ref principalEvent, ref feesEvent, ref commissionsEvent);
            PaidInstallments = _repayNextInstallments.PaidInstallments;
        }
    }
}
