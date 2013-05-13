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

using OpenCBS.Services.Accounting;
using OpenCBS.Services.Currencies;
using OpenCBS.Services.Events;
using OpenCBS.Services.Rules;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using OpenCBS.Services.Export;

namespace OpenCBS.Services
{
    public interface IServices
    {
        UserServices GetUserServices();
        AccountingServices GetAccountingServices();
        ExchangeRateServices GetExchangeRateServices();
        ChartOfAccountsServices GetChartOfAccountsServices();
        EventProcessorServices GetEventProcessorServices();
        ClientServices GetClientServices();
        LoanServices GetContractServices();
        DatabaseServices GetDatabaseServices();
        EconomicActivityServices GetEconomicActivityServices();
        ApplicationSettingsServices GetApplicationSettingsServices();
        GraphServices GetGraphServices();
        LocationServices GetLocationServices();
        PicturesServices GetPicturesServices();
        ProductServices GetProductServices();
        CollateralProductServices GetCollateralProductServices();
        SettingsImportExportServices GetSettingsImportExportServices();
        ProjectServices GetProjectServices();
        NonWorkingDateSingleton GetNonWorkingDate();
        ApplicationSettings GetGeneralSettings();
        FundingLineServices GetFundingLinesServices();
        MFIServices GetMFIServices();
        SQLToolServices GetSQLToolServices();
        QuestionnaireServices GetQuestionnaireServices();
        SavingProductServices GetSavingProductServices();
        SavingServices GetSavingServices();
        StandardBookingServices GetStandardBookingServices();
        CurrencyServices GetCurrencyServices();
        RegExCheckerServices GetRegExCheckerServices();
        AccountingRuleServices GetAccountingRuleServices();
        RoleServices GetRoleServices();
        ExportServices GetExportServices();
        BranchService GetBranchService();
        TellerServices GetTellerServices();
        CustomizableFieldsServices GetCustomizableFieldsServices();
        PaymentMethodServices GetPaymentMethodServices();
        MenuItemServices GetMenuItemServices();

        void SuppressAllRemotingInfos(string pComputerName, string pLoginName);

        string GetAuthentification(string pOctoUsername, string pOctoPass, string pDbName, string pComputerName, string pLoginName);
        void RunTimeout();
        string GetToken();
    }
}
