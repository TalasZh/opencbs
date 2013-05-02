// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain;

namespace OpenCBS.Services
{
    /// <summary>
    /// CashReceipt Services.
    /// </summary>
    public class CashReceiptServices : MarshalByRefObject
    {

        //private ExchangeRateServices _eRS;
        //private CashReceiptsManager _cashReceiptManager;
        //private ApplicationSettings _generalParam;
        //private User _user = new User();

        //public CashReceiptServices(User pUser)
        //{
        //    //_user = pUser;

        //    //_eRS = new ExchangeRateServices(pUser);
        //    //_cashReceiptManager = new CashReceiptsManager(pUser);
        //    //_generalParam = ApplicationSettings.GetInstance(pUser.Md5);
        //}

        //[Serializable]
        //public struct CashReceiptErrors
        //{
        //    public bool findError;
        //    public string resultMessage;
        //}

        //public CashReceiptStock FindAllCashReceiptsForRepayment(DateTime pBeginDate, DateTime pEndDate, int? pLoanOfficerId, string pLanguage, bool pDisableFees, bool pUseGregorienCalendar, ref CashReceiptErrors pCashReceiptErrors)
        //{
        //    _cashReceiptManager.DeleteCashReceiptsFromUser(_user.Id);
        //    int nbOfDays = (pEndDate - pBeginDate).Days > 0 ? (pEndDate - pBeginDate).Days : 0;
        //    for (int i = 0; i <= nbOfDays; i++)
        //    {
        //        ExchangeRate rate = null;// _eRS.SelectExchangeRate(pBeginDate.AddDays(nbOfDays));
        //        if (rate == null && _generalParam.ExternalCurrency != null)
        //        {
        //            pCashReceiptErrors.findError = true;
        //            pCashReceiptErrors.resultMessage = MultiLanguageStrings.GetString(Ressource.stringRes, "CashReceipt_NeedExchangeRate.Text") + pBeginDate.AddDays(nbOfDays).ToShortDateString();
        //            return null;
        //        }
        //    }

        //    CashReceiptStock cashReceiptStock;
        //    if (_generalParam.IsCashReceiptBeforeConfirmation)
        //        cashReceiptStock = _cashReceiptManager.SelectRepaymentCashReceiptsBeforeConfirmation(pBeginDate, pEndDate, pLoanOfficerId, pLanguage, pDisableFees,  pUseGregorienCalendar);
        //    else
        //        cashReceiptStock = _cashReceiptManager.SelectRepaymentCashReceiptsAfterConfirmation(pBeginDate, pEndDate, pLoanOfficerId, pLanguage,  pDisableFees,  pUseGregorienCalendar);

        //    if (cashReceiptStock.GetNumberOfCashReceipts == 0)
        //        throw new OctopusReportsException(OctopusReportsExceptionsEnum.NoResult);

        //    else
        //    {
        //        foreach (CashReceipt cashReceipt in cashReceiptStock)
        //        {
        //            _cashReceiptManager.AddCashReceipt(cashReceipt);
        //            _cashReceiptManager.AddMembers(cashReceipt.UserId, cashReceipt.ContractCode);
        //        }
        //    }
        //    return cashReceiptStock;
        //}

        //public CashReceiptStock FindAllCashReceiptsForDisbursment(DateTime pBeginDate, DateTime pEndDate, int? pLoanOfficerId, string pLanguage, bool pDisableFees, bool pUseGregorienCalendar, ref CashReceiptErrors pCashReceiptErrors)
        //{
        //    _cashReceiptManager.DeleteCashReceiptsFromUser(_user.Id);
        //    int nbOfDays = (pEndDate - pBeginDate).Days > 0 ? (pEndDate - pBeginDate).Days : 0;
        //    for (int i = 0; i <= nbOfDays; i++)
        //    {
        //        ExchangeRate rate = null;// _eRS.SelectExchangeRate(pBeginDate.AddDays(nbOfDays));
        //        if (rate == null && _generalParam.ExternalCurrency != null)
        //        {
        //            pCashReceiptErrors.findError = true;
        //            pCashReceiptErrors.resultMessage = MultiLanguageStrings.GetString(Ressource.stringRes, "CashReceipt_NeedExchangeRate.Text") + pBeginDate.AddDays(nbOfDays).ToShortDateString();
        //            return null;
        //        }
        //    }

        //    CashReceiptStock cashReceiptStock;
        //    if (_generalParam.IsCashReceiptBeforeConfirmation)
        //        cashReceiptStock = _cashReceiptManager.SelectDisbursmentCashReceiptsBeforeConfirmation(pBeginDate, pEndDate, pLoanOfficerId, pLanguage,  pDisableFees,  pUseGregorienCalendar);
        //    else
        //        cashReceiptStock = _cashReceiptManager.SelectDisbursmentCashReceiptsAfterConfirmation(pBeginDate, pEndDate, pLoanOfficerId, pLanguage, pDisableFees, pUseGregorienCalendar);

        //    if (cashReceiptStock.GetNumberOfCashReceipts == 0)
        //        throw new OctopusReportsException(OctopusReportsExceptionsEnum.NoResult);
        //    else
        //    {
        //        foreach (CashReceipt cashReceipt in cashReceiptStock)
        //        {
        //            _cashReceiptManager.AddCashReceipt(cashReceipt);
        //            _cashReceiptManager.AddMembers(cashReceipt.UserId, cashReceipt.ContractCode);
        //        }
        //    }
        //    return cashReceiptStock;
        //}

        //public CashReceiptStock FindAllCashReceiptForRoadMap(DateTime pBeginDate, DateTime pEndDate, int? pLoanOfficerId, string pLanguage, bool pDisableFees, bool pUseGregorienCalendar)
        //{
        //    _cashReceiptManager.DeleteCashReceiptsFromUser(_user.Id);

        //    CashReceiptStock cashReceiptStock;
        //    if (_generalParam.IsCashReceiptBeforeConfirmation)
        //    {
        //        cashReceiptStock = _cashReceiptManager.SelectDisbursmentCashReceiptsBeforeConfirmation(pBeginDate, pEndDate, pLoanOfficerId, pLanguage,  pDisableFees,  pUseGregorienCalendar);
        //        cashReceiptStock.Add(_cashReceiptManager.SelectRepaymentCashReceiptsBeforeConfirmation(pBeginDate, pEndDate, pLoanOfficerId, pLanguage,  pDisableFees,  pUseGregorienCalendar));
        //    }
        //    else
        //    {
        //        cashReceiptStock = _cashReceiptManager.SelectDisbursmentCashReceiptsAfterConfirmation(pBeginDate, pEndDate, pLoanOfficerId, pLanguage, pDisableFees, pUseGregorienCalendar);
        //        cashReceiptStock.Add(_cashReceiptManager.SelectRepaymentCashReceiptsAfterConfirmation(pBeginDate, pEndDate, pLoanOfficerId, pLanguage,  pDisableFees,  pUseGregorienCalendar));
        //    }

        //    if (cashReceiptStock.GetNumberOfCashReceipts == 0)
        //        throw new OctopusReportsException(OctopusReportsExceptionsEnum.NoResult);
        //    else
        //    {
        //        foreach (CashReceipt cashReceipt in cashReceiptStock)
        //        {
        //            _cashReceiptManager.AddCashReceipt(cashReceipt);
        //            _cashReceiptManager.AddMembers(cashReceipt.UserId, cashReceipt.ContractCode);
        //        }
        //    }
        //    return cashReceiptStock;
        //}

        //public void GenerateCashReceiptAndAddItInDatabase(Loan pContract,IClient pClient, int? pInstallmentRank, RepaymentEvent pEvent, DateTime pDate, bool pDisableFees, bool pUseGregorienCalendar,string pLanguage)
        //{
        //    _cashReceiptManager.DeleteCashReceiptsFromUser(_user.Id);

        //    ExchangeRate rate = null;// _eRS.SelectExchangeRate(pDate);
        //    pEvent.User = _user;
        //    CashReceipt cashReceipt = new CashReceipt(pContract, pClient, pEvent, pInstallmentRank, _generalParam.ExternalCurrency, pLanguage, rate, pDate, _user, ApplicationSettings.GetInstance(_user.Md5), ChartOfAccounts.GetInstance(_user), pUseGregorienCalendar, NonWorkingDateSingleton.GetInstance(_user.Md5));
        //    _cashReceiptManager.AddCashReceipt(cashReceipt);
        //}

        //public void GenerateCashReceiptAndAddItInDatabase(Loan pContract, IClient pClient, LoanDisbursmentEvent pEvent, DateTime pDate, bool pDisableFees, bool pUseGregorienCalendar, string pLanguage)
        //{
        //    _cashReceiptManager.DeleteCashReceiptsFromUser(_user.Id);

        //    ExchangeRate rate = null;// _eRS.SelectExchangeRate(pDate);
        //    pEvent.User = _user;
        //    CashReceipt cashReceipt = new CashReceipt(pContract, pClient, pEvent, null, ApplicationSettings.GetInstance(_user.Md5).ExternalCurrency, pLanguage, rate, pDate, _user, ApplicationSettings.GetInstance(_user.Md5), ChartOfAccounts.GetInstance(_user), pUseGregorienCalendar, NonWorkingDateSingleton.GetInstance(_user.Md5));
        //    _cashReceiptManager.AddCashReceipt(cashReceipt);

        //    if(pClient is Group)
        //        _cashReceiptManager.AddMembers(cashReceipt.UserId, pContract.Code);
        //    else if(pClient is Person)
        //        _cashReceiptManager.AddBeneficiary(cashReceipt.UserId, pContract.Code);
        //    else
        //        _cashReceiptManager.AddCorporate(cashReceipt.UserId, pContract.Code);
        //}
    }

}
