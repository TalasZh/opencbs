//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System.Collections.Generic;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.Shared;

namespace Octopus.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
    /// <summary>
    /// Summary description for IRepayNextInstallment.
    /// </summary>
    public interface IRepayNextInstallments
    {
        List<Installment> PaidInstallments{ get; set;}

        void RepayNextInstallments(ref OCurrency amountPaid, ref OCurrency interestEvent,
                                   ref OCurrency interestPrepayment, ref OCurrency principalEvent,
                                   ref OCurrency feesEvent, ref OCurrency commissionsEvent);
    }
}