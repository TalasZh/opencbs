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

namespace OpenCBS.Enums
{
    [Serializable]
    public enum OReports
    {
        Folder = 0,
        QualityReport = 1,
        DelinquentLoan = 2,
        DisbursmentAndReimbursment = 3,
        DormantCustomers = 4,
        OLBPerLoan = 5,
        DropOut_months = 7,
        SimpleConsoReport = 8, //moi

        Conso_PortFolioAndPAR_ByHeadQuarter = 9,
        Conso_PortFolioAndPAR_ByHeadQuarter_12Months = 10,
        Conso_PortFolioAndPAR_ByBranchCode = 11,
        Conso_PortFolioAndPAR_ByBranchCode_12Months = 12,
        Conso_ClientPortfolioAnalysis = 13,
        Loans = 14,
        Default = 15,
        Conso_LoansPortFolioAnalysis = 16,
        Repayments = 20,
        ClientsAndShareOfWomen = 21,
        LoansDisbursed = 23,
        ActiveLoans = 24,
        ClientAndLoansStatistics = 25,
        Disbursments = 26,
        FinancialStock = 27,
        Follow_Up = 28,
        Conso_Follow_Up = 29,
        PARAnalysis = 30,
        Conso_ClientsEvolutions = 31,
        Export_LoanSizeMaturityGraceDomainDistrict = 32,
        Export_Conso_LoanSizeMaturityGraceDomainDistrict = 33,
        test = 34,
        nbtmonthly = 35,
        NbtQuartely = 36,
        PortfolioByLoanOfficer = 37,
        ResidualMaturity = 38
    }

    [Serializable]
    public enum OInternalReports
    {
        Default,

        CashReceiptInGrouped,
        CashReceiptInNotGrouped,
        CashReceiptOut,
        RoadMap,
        IndividualLoanAgreement,
        GroupLoanAgreement,
        IndividualSchedule,
        GroupSchedule,
        AccountingBalances,
        BookingsList,
        ClientList,
        ContractsHistory,
        GuarantorAgreement,
        CorporateLoanAgreement,
        CorporateSchedule,
        DisbursmentAgreement,
        LoanCCAgreement,
        GuaranteeCCAgreement,
        LoanProjectAnalysis,
        GuaranteeProjectAnalysis
    }
}
