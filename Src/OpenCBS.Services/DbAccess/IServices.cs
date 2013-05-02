// LICENSE PLACEHOLDER

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
