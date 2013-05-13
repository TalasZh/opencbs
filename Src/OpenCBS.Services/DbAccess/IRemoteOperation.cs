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

using OpenCBS.CoreDomain;
using OpenCBS.Services.Accounting;
using OpenCBS.Services.Currencies;
using OpenCBS.Services.Events;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;
using OpenCBS.Services.Export;

namespace OpenCBS.Services
{
    public interface IRemoteOperation
    {
        UserServices GetUserServices(User pUser);
        AccountingServices GetAccountingServices(User pUser);
        ExchangeRateServices GetExchangeRateServices(User pUser);
        ChartOfAccountsServices GetChartOfAccountsServices(User pUser);
        EventProcessorServices GetEventProcessorServices(User pUser);
        //CashReceiptServices GetCashReceiptServices(User pUser);
        ClientServices GetClientServices(User pUser);
        LoanServices GetContractServices(User pUser);
        DatabaseServices GetDatabaseServices(User pUser);
        EconomicActivityServices GetEconomicActivities(User pUser);
        GraphServices GetGraphServices(User pUser);
        LocationServices GetLocationServices(User pUser);
        PicturesServices GetPicturesServices(User pUser);
        ProductServices GetProductServices(User pUser);
        ApplicationSettingsServices GetApplicationSettingsServices(User pUser);
        SettingsImportExportServices GetSettingsImportExportServices(User pUser);
        ProjectServices GetProjectServices(User pUser);
        NonWorkingDateSingleton GetNonWorkingDate(User pUser);
        ApplicationSettings GetGeneralSettings(User pUser);
        FundingLineServices GetFundingLinesServices(User pUser);
        MFIServices GetMFIServices(User pUser);
        SQLToolServices GetSQLToolServices(User pUser);
        QuestionnaireServices GetQuestionnaireServices(User pUser);
        SavingProductServices GetSavingProductServices(User pUser);
        SavingServices GetSavingServices(User pUser);
        StandardBookingServices GetStandardBookingServices(User pUser);
        CurrencyServices GetCurrencyServices(User pUser);
        AccountingRuleServices GetAccountingRuleServices(User pUser);
        RoleServices GetRoleServices(User pUser);
        ExportServices GetExportServices(User pUser);
        BranchService GetBranchService(User user);
        TellerServices GetTellerServices(User user);
        CustomizableFieldsServices GetAdvancedCustomizableFieldsServices(User user);
        PaymentMethodServices GetPaymentMethodServices(User user);
        MenuItemServices GetMenuItemServices(User user);
        
        bool TestRemoting();

        void SuppressAllRemotingInfos(User pUser, string pComputerName, string pLoginName);

        string GetAuthentification(string login, string pass, string account, string pComputerName, string pLoginName);
        void RunTimeout();
    }
}
