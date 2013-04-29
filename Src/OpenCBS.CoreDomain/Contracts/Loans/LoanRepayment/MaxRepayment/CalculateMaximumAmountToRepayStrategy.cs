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
using System.Linq;
using Octopus.CoreDomain.Contracts.Loans.Installments;
using Octopus.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment.BaseToCalculateFeesForAnticipatedRepayment;
using Octopus.CoreDomain.Events;
using Octopus.Enums;
using Octopus.Shared;
using Octopus.Shared.Settings;


namespace Octopus.CoreDomain.Contracts.Loans.LoanRepayment.MaxRepayment
{
    /// <summary>
    /// Summary description for MaximumAmountAuthorizedToRepayStrategy.
    /// </summary>
    [Serializable]
    public class CalculateMaximumAmountToRepayStrategy
    {
        private readonly Loan _contract;
        private readonly CreditContractOptions _cCo;
        private readonly User _user;
        private readonly ApplicationSettings _generalSettings;
        private readonly NonWorkingDateSingleton _nWds;

        public CalculateMaximumAmountToRepayStrategy(CreditContractOptions pCCo, Loan pContract, User pUser, 
            ApplicationSettings pGeneralSettings,NonWorkingDateSingleton pNonWorkingDate)
        {
            _user = pUser;
            _generalSettings = pGeneralSettings;
            _nWds = pNonWorkingDate;

            _contract = pContract;
            _cCo = pCCo;
            _user = pUser;
        }

        private OCurrency MaxAmountOfRealSchedule(DateTime payDate)
        {
            Installment installment;

            OCurrency olb = _contract.CalculateActualOlb();
            OCurrency interestPayment = 0;
            OCurrency actualOlb = _contract.CalculateActualOlb();

            DateTime lasDateOfPayment = _contract.GetLastRepaymentDate();
            int daysInTheYear = _generalSettings.GetDaysInAYear(_contract.StartDate.Year);
            int roundingPoint = _contract.UseCents ? 2 : 0;
           
            if (_contract.StartDate > lasDateOfPayment)
                lasDateOfPayment = _contract.StartDate;

            for (int i = 0; i < _contract.NbOfInstallments; i++)
            {
                installment = _contract.GetInstallment(i);

                if (installment.IsRepaid)
                {
                    if (lasDateOfPayment < installment.ExpectedDate)
                    lasDateOfPayment = installment.ExpectedDate;
                }

                if (installment.IsRepaid || installment.InterestHasToPay == 0) continue;

                if (installment.ExpectedDate <= payDate)
                {
                    OCurrency calculatedInterests = 0;

                    if (installment.PaidInterests > 0 
                        && installment.InterestsRepayment > installment.PaidInterests)
                    {
                        calculatedInterests = installment.PaidInterests;
                    }
                    
                    if (installment.PaidCapital == 0 
                        && installment.PaidInterests > 0 
                        && installment.PaidInterests != installment.InterestsRepayment)
                    {
                        DateTime dateOfInstallment = installment.Number == 1
                                   ? _contract.StartDate
                                   : _contract.GetInstallment(installment.Number - 2).ExpectedDate;
                        
                        int d = (lasDateOfPayment - dateOfInstallment).Days;

                        OCurrency olbBeforePayment =
                            _contract.Events.GetRepaymentEvents().Where(
                                repaymentEvent => !repaymentEvent.Deleted && repaymentEvent.Date <= dateOfInstallment).Aggregate(
                                    _contract.Amount, (current, repaymentEvent) => current - repaymentEvent.Principal);

                        calculatedInterests =
                            (olbBeforePayment*Convert.ToDecimal(_contract.InterestRate)/daysInTheYear*d).Value;
                        calculatedInterests = Math.Round(calculatedInterests.Value, roundingPoint,
                                                         MidpointRounding.AwayFromZero);

                        if (installment.PaidInterests < calculatedInterests && actualOlb != olbBeforePayment)
                        {
                            calculatedInterests = installment.PaidInterests;
                        }
                    }
                    DateTime expectedDate = installment.ExpectedDate;
                    //in case very late repayment
                    if (installment.Number == _contract.InstallmentList.Count
                        && installment.ExpectedDate < payDate
                        && installment.PaidCapital == 0)
                    {
                        expectedDate = payDate;
                    }

                    int days = (expectedDate - lasDateOfPayment).Days;

                    interestPayment += Math.Round((olb * _contract.InterestRate / daysInTheYear * days).Value + calculatedInterests.Value,
                                                    roundingPoint, MidpointRounding.AwayFromZero) - installment.PaidInterests;
                    lasDateOfPayment = installment.ExpectedDate;
                }

                if (installment.Number > 1 && installment.ExpectedDate > payDate && _contract.GetInstallment(installment.Number - 2).ExpectedDate < payDate)
                {
                    OCurrency paidInterests = installment.PaidInterests;

                    int daySpan = (payDate - lasDateOfPayment).Days < 0 ? 0 : (payDate - lasDateOfPayment).Days;
                    installment.InterestsRepayment = olb * _contract.InterestRate * daySpan / daysInTheYear + paidInterests;

                    installment.InterestsRepayment =
                        Math.Round(installment.InterestsRepayment.Value, roundingPoint,
                                   MidpointRounding.AwayFromZero);
                    interestPayment += installment.InterestsRepayment - paidInterests;
                    lasDateOfPayment = installment.ExpectedDate;
                }

                if (installment.Number == 1 && installment.ExpectedDate > payDate)
                {
                    int daySpan = (payDate - _contract.StartDate).Days < 0
                                      ? 0
                                      : (payDate - _contract.StartDate).Days;
                    OCurrency interest = olb*_contract.InterestRate*daySpan/daysInTheYear;

                    interestPayment += Math.Round(interest.Value, roundingPoint, MidpointRounding.AwayFromZero) -
                                       installment.PaidInterests;

                }
            }
            return Math.Round(interestPayment.Value, roundingPoint, MidpointRounding.AwayFromZero) + olb;
        }

        public OCurrency CalculateMaximumAmountAuthorizedToRepay(DateTime pDate)
        {
            if (_cCo.LoansType == OLoanTypes.DecliningFixedPrincipalWithRealInterest)
                return MaxAmountOfRealSchedule(pDate);

            OCurrency amount = 0;
            //capital
            amount += _contract.CalculateActualOlb();

            //interest
            if (_cCo.CancelInterests)
                amount += _cCo.ManualInterestsAmount;

            else
            {
                //calculate Remaining Interests when loan is calculated on it
                if (_cCo.LoansType == OLoanTypes.Flat && !_cCo.KeepExpectedInstallments)
                {
                    amount += _contract.CalculateRemainingInterests(pDate);
                }
                else if (_cCo.LoansType != OLoanTypes.Flat && _cCo.LoansType != OLoanTypes.DecliningFixedPrincipal &&
                         !_cCo.KeepExpectedInstallments)
                {
                    amount += _contract.CalculateRemainingInterests(pDate);
                }
                else
                {
                    if (_cCo.LoansType == OLoanTypes.DecliningFixedPrincipal && !_cCo.KeepExpectedInstallments)
                    {
                        DateTime? installmentDate = null;

                        foreach (Installment installment in _contract.InstallmentList)
                        {
                            if (!installment.IsRepaid && installmentDate == null &&
                                (installment.ExpectedDate - pDate).Days > 0)
                                installmentDate = installment.ExpectedDate;
                        }

                        if (installmentDate > pDate)
                            amount += _contract.CalculateRemainingInterests(pDate);
                        else
                            amount += _contract.CalculateRemainingInterests();
                    }
                    else
                        amount += _contract.CalculateRemainingInterests();
                }
            }

            //commission
            if (_cCo.CancelFees)
            {
                amount += _cCo.ManualFeesAmount;
                amount += _cCo.ManualCommissionAmount;
            }
            else
                amount += CalculateLateAndAnticipatedFees(pDate);

            int decimalPoint = _contract.UseCents ? _generalSettings.InterestRateDecimalPlaces : 0;
            return Math.Round(amount.Value, decimalPoint, MidpointRounding.AwayFromZero);
        }

        public OCurrency CalculateMaximumAmountForEscapedMember(DateTime pDate)
        {
            Installment installment = null;
            OCurrency interests = 0;
            OCurrency commission = 0;
            OCurrency aTprComission = 0;
            
            Installment priorInstallment;
            bool calculated = false;
            Loan contract = _contract.Copy();

            for (int i = 0; i < contract.NbOfInstallments; i++)
            {
                Installment getInstallment = contract.GetInstallment(i);

                if (!getInstallment.IsRepaid && getInstallment.ExpectedDate > pDate || (getInstallment.ExpectedDate == pDate && _contract.EscapedMember != null))
                {
                    if (installment == null)
                    {
                        installment = getInstallment;

                        if (_generalSettings.AccountingProcesses == OAccountingProcesses.Accrual)
                        {
                            DateTime expectedDate;
                            
                            OCurrency daysInTheInstallment = installment.Number == 1
                                                       ? (installment.ExpectedDate - _contract.StartDate).Days
                                                       : (installment.ExpectedDate -
                                                          _contract.GetInstallment(installment.Number - 2).ExpectedDate)
                                                             .Days;
                            if (i == 0)
                            {
                                expectedDate = contract.StartDate;
                            }
                            else
                            {
                                priorInstallment = contract.GetInstallment(i - 1);
                                expectedDate = priorInstallment.ExpectedDate;

                                if (contract.GetLastRepaymentDate() > expectedDate)
                                {
                                    if (installment.ExpectedDate > contract.GetLastRepaymentDate())
                                    {
                                        expectedDate = contract.GetLastRepaymentDate();
                                    }
                                    daysInTheInstallment = (installment.ExpectedDate - expectedDate).Days;
                                }
                            }

                            if (getInstallment.ExpectedDate == pDate && _contract.EscapedMember != null)
                            {
                                expectedDate = getInstallment.ExpectedDate;
                            }

                            int daySpan = (pDate - expectedDate).Days < 0 ? 0 : (pDate - expectedDate).Days;

                            if (contract.EscapedMember != null)
                            {
                                //calculate new interes for the person of the group
                                OCurrency amount = contract.Product.LoanType == OLoanTypes.Flat
                                                  ? contract.Amount
                                                  : contract.GetOlb();

                                if (daySpan != 0)
                                {
                                    interests = (amount*contract.EscapedMember.LoanShareAmount/contract.Amount)*daySpan/
                                                daysInTheInstallment*contract.InterestRate;
                                }
                                else
                                {
                                    interests = (amount*contract.EscapedMember.LoanShareAmount/contract.Amount)*
                                                contract.InterestRate;
                                }
                            }
                            else
                            {
                                interests = installment.InterestsRepayment * daySpan / daysInTheInstallment;
                            }

                        }
                        else
                        {
                            interests = installment.InterestsRepayment == installment.PaidInterests
                                            ? installment.InterestsRepayment
                                            : (installment.ExpectedDate > pDate ? 0 : installment.InterestsRepayment);

                            if (contract.EscapedMember != null)
                            {
                                interests = interests * contract.EscapedMember.LoanShareAmount/contract.Amount;
                            }
                        }
                    }

                    commission +=
                        new Repayment.RepayLateInstallments.CalculateInstallments(_cCo, contract, _user,
                                                                                  _generalSettings, _nWds).
                            CalculateCommision(pDate, getInstallment.Number, OPaymentType.TotalPayment, 0, ref calculated);

                    if (getInstallment.ExpectedDate == pDate && _contract.EscapedMember != null && _contract.Product.AnticipatedTotalRepaymentPenaltiesBase != OAnticipatedRepaymentPenaltiesBases.RemainingInterest)
                        aTprComission = commission;

                    if (getInstallment.ExpectedDate > pDate && _contract.EscapedMember != null && aTprComission > 0 && _contract.Product.AnticipatedTotalRepaymentPenaltiesBase != OAnticipatedRepaymentPenaltiesBases.RemainingInterest)
                        commission = aTprComission;

                    if (_cCo.ManualFeesAmount > 0)
                    {
                        commission = _cCo.ManualFeesAmount;
                    }

                    foreach (RepaymentEvent rPayment in _contract.Events.GetRepaymentEvents())
                    {
                        if (rPayment.Date == pDate && installment.Number == rPayment.InstallmentNumber)
                        {
                            installment.FeesUnpaid = 0;
                        }
                    }
                }
            }

            if (contract.EscapedMember != null)
            {
                OCurrency amount = interests + commission +
                                   contract.GetOlb()*contract.EscapedMember.LoanShareAmount/contract.Amount;
                return _contract.UseCents
                           ? Math.Round(amount.Value, 2, MidpointRounding.AwayFromZero)
                           : Math.Round(amount.Value, 0, MidpointRounding.AwayFromZero);
            }
             return 0;
        }

        private OCurrency CalculateLateAndAnticipatedFees(DateTime pDate)
        {
            OCurrency fees = 0;
            Loan contract = _contract.Copy();
            new Repayment.RepayLateInstallments.CalculateInstallments(_cCo, contract, _user, _generalSettings,_nWds).CalculateNewInstallmentsWithLateFees(pDate);
            for (int i = 0; i < contract.NbOfInstallments; i++)
            {
                Installment installment = contract.GetInstallment(i);
                if (!installment.IsRepaid && installment.ExpectedDate <= pDate)
                {
                    fees += installment.FeesUnpaid;
                    installment.PaidCapital = installment.CapitalRepayment;
                    installment.PaidInterests = installment.InterestsRepayment;
                }
            }
            if (!_cCo.KeepExpectedInstallments)
                fees += new CalculationBaseForAnticipatedFees(_cCo, contract).CalculateFees(pDate);

            return _contract.UseCents ? Math.Round(fees.Value, 2, MidpointRounding.AwayFromZero) : Math.Round(fees.Value, 0, MidpointRounding.AwayFromZero);
        }
    }
}