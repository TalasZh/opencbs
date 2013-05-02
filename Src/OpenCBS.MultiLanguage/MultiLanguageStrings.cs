// LICENSE PLACEHOLDER

using System.Collections;
using System.Resources;

namespace OpenCBS.MultiLanguageRessources
{
    public enum Ressource
    {
        FrmSplash,
        AccountView,
        AddGuarantorForm,
        FrmAddLoanProduct,
        AddressUserControl,
        ApplicationExceptionHandler,
        AuditTrailView,
        BadLoanRepaymentForm,
        CashPrevisionForm,
        CashReceiptForm,
        ChartOfAccountsForm,
        Common,
        ContractReschedulingForm,
        CreditContractDisbursmentForm,
        CreditContractForm,
        CreditContractRepayForm,
        DateTimeUserControl,
        ElemMvtUserControl,
        ExchangeRateForm,
        ExportBookingsForm,
        FastChoiceForm,
        ReassingContract,
        FrmActivity,
        FrmFundingLine,
        FrmFundingLineEvent,
        FrmAdvancedSettings,
        FrmApplicationSetup,
        FrmBackup,
        FrmBranchConsolidations,
        FrmCollateralsCud,
        FrmCollateralsR,
        FrmDatabaseSettings,
        FrmGlobalConsolidations,
        FrmReportBrowser,
        FrmReportViewer,
        FrmRestore,
        FrmContractBookings,
        GeneralSettings,
        GroupForm,
        GroupUserControl,
        InternalReportsForm,
        LotrasmicMainWindowForm,
        ManualEntryForm,
        MonthlyClosureForm,
        NewExportConso,
        PackagesForm,
        PasswordForm,
        PasswordFormOnline,
        ClientForm,
        PersonUserControl,
        SearchClientForm,
        SearchCreditContractForm,
        StringRes,
        UserForm,
        GuaranteeContractForm,
        DatabaseMaitenance,
        CorporateUserControl,
        FrmCustomReportWizard,
        VillageForm,
        FrmCurrencyType,
        FrmAddSavingProduct,
        FrmAddSavingEvent,
        StandardBooking,
        AccountingRule,
        FrmExportSage,
        FrmRoles,
        CustomizableExport,
        ShowPictureForm,
        LoanSharesForm,
        FrmOpenCloseTeller,
        CreditScoringForm,
        ClientControl,
        MyInforamtionForm
    }

	/// <summary>
	/// Summary description for RessourcesManager.
	/// </summary>
	public static class  MultiLanguageStrings
	{
        public static string STRING_MESSAGES_RES_NAME = "stringRes";
        public static string ERROR_MESSAGES_RES_NAME = "errorRes";

        static Hashtable _ressourceManagers = new Hashtable();

        public static string GetString(Ressource pRessourceName, string pName)
        {
            ResourceManager rm = (ResourceManager) _ressourceManagers[pRessourceName];
            if (rm == null)
            {
                rm = new ResourceManager("OpenCBS.MultiLanguageRessources.resx." + pRessourceName, typeof(MultiLanguageStrings).Assembly);
                _ressourceManagers.Add(pRessourceName, rm);
            }
            return rm.GetString(pName);
        }

        public static string GetString(string res, string key)
        {
            ResourceManager rm = (ResourceManager)_ressourceManagers[res];
            if (rm == null)
            {
                rm = new ResourceManager("OpenCBS.MultiLanguageRessources.resx." + res, typeof(MultiLanguageStrings).Assembly);
                _ressourceManagers.Add(res, rm);
            }
            return rm.GetString(key);
        }

        public static void Reset()
        {
            _ressourceManagers.Clear();
        }
	}
}
