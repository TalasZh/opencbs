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
