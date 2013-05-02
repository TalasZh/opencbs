// LICENSE PLACEHOLDER

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
