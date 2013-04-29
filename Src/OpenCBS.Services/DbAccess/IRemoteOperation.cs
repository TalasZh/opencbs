using Octopus.CoreDomain;
using Octopus.Services.Accounting;
using Octopus.Services.Currencies;
using Octopus.Services.Events;
using Octopus.Shared;
using Octopus.Shared.Settings;
using Octopus.Services.Export;

namespace Octopus.Services
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
