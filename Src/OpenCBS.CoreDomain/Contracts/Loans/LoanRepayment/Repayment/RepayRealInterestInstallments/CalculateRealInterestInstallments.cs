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
using System.Collections.Generic;
using System.Linq;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.MaxRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment;
using OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.InterestRepayment;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayRealInterestInstallments
{
    [Serializable]
    class CalculateRealInterestInstallments
    {
        private readonly Loan _contract;
        private readonly CreditContractOptions _cCo;
        private readonly RepayFeesStrategy _methodToRepayFees;
        private readonly RepayCommisionStrategy _methodToRepayCommission;

        private readonly RepayInterestStrategy _methodToRepayInterest;
        private readonly ApplicationSettings _generalSettings;
        private readonly NonWorkingDateSingleton _nWds;
        private readonly CalculateMaximumAmountToRepayStrategy _calculateMaximumAmount;
             
        public List<Installment> PaidIstallments;

        public CalculateRealInterestInstallments(CreditContractOptions pCco,  
            CalculateMaximumAmountToRepayStrategy calculateMaximumAmount, Loan pContract, 
            ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNonWorkingDate)
        {
            _generalSettings = pGeneralSettings;
            _nWds = pNonWorkingDate;
            _contract = pContract;
            _cCo = pCco;
            _methodToRepayFees = new RepayFeesStrategy(pCco);
            _methodToRepayInterest = new RepayInterestStrategy(pCco);
            _methodToRepayCommission = new RepayCommisionStrategy(pCco);
            PaidIstallments = new List<Installment>();
            _calculateMaximumAmount = calculateMaximumAmount;
        }

        public void RepayInstallments(DateTime payDate, 
            ref OCurrency amountPaid, 
            ref OCurrency interestEvent, 
            ref OCurrency principalEvent, 
            ref OCurrency feesEvent, 
            ref OCurrency commissionsEvent, 
            ref OPaymentType paymentType)
        {
            if (amountPaid == 0) return;
            // we need date without time part
            payDate = payDate.Date;

            Installment installment = null;

            int daysInTheYear = _generalSettings.GetDaysInAYear(_contract.StartDate.Year);
            int startNumber = 0;
            int roundingPoint = _contract.UseCents ? 2 : 0;

            OCurrency olb = _contract.CalculateActualOlb();
            OCurrency actualOlb = _contract.CalculateActualOlb();
            OCurrency interestsToRepay = 0;
            OCurrency movedPrincipal = 0;
            OCurrency paidPrincipal = 0;
            OCurrency paymentOfInterest = 0;
            OCurrency totalAmount = _calculateMaximumAmount.CalculateMaximumAmountAuthorizedToRepay(payDate);
            
            DateTime beginDate = _contract.StartDate;
            DateTime date = _contract.StartDate;
            bool recalculateSchedule = false;
            bool totalPayment = totalAmount == amountPaid &&
                                _contract.GetFirstUnpaidInstallment().Number != _contract.InstallmentList.Count;

            #region calculate interest payment)
            for (int i = 0; i < _contract.NbOfInstallments; i++)
            {
                installment = _contract.GetInstallment(i);
                if (!installment.IsRepaid)
                {
                    //get last repDate
                    if (date < _contract.GetLastRepaymentDate())
                    {
                        date = _contract.GetLastRepaymentDate();
                        beginDate = date;
                    }
                }
            }

            for (int i = 0; i < _contract.NbOfInstallments; i++)
            {
                installment = _contract.GetInstallment(i);
                if (installment.IsRepaid) continue;
                if (installment.Number == 1 && installment.ExpectedDate > payDate)
                {
                    if (installment.PaidCapital == 0 && installment.PaidInterests > 0)
                    {
                        date = installment.Number == 1
                                   ? _contract.StartDate
                                   : _contract.GetInstallment(installment.Number - 2).ExpectedDate;
                        beginDate = date;
                    }

                    int daySpan = (payDate - _contract.StartDate).Days < 0
                                      ? 0
                                      : (payDate - _contract.StartDate).Days;

                    installment.InterestsRepayment = olb * _contract.InterestRate * daySpan / daysInTheYear;

                    installment.InterestsRepayment =
                        Math.Round(installment.InterestsRepayment.Value, roundingPoint,
                                   MidpointRounding.AwayFromZero);
                    interestsToRepay += installment.InterestsRepayment;
                    startNumber = 0;
                    date = installment.ExpectedDate;
                }

                if (installment.Number > 1 && installment.ExpectedDate > payDate)
                {
                    OCurrency paidInterests = installment.PaidInterests;

                    int daySpan = (payDate - date).Days < 0 ? 0 : (payDate - date).Days;
                    installment.InterestsRepayment = olb * _contract.InterestRate * daySpan / daysInTheYear + paidInterests;
                    
                    installment.InterestsRepayment =
                        Math.Round(installment.InterestsRepayment.Value, roundingPoint,
                                   MidpointRounding.AwayFromZero);
                    interestsToRepay += installment.InterestsRepayment - paidInterests;
                    date = installment.ExpectedDate;
                }

                if (installment.ExpectedDate <= payDate)
                {
                    OCurrency calculatedInterest = 0;
                    OCurrency paidInterest = 0;

                    if (installment.PaidInterests > 0
                        && installment.InterestsRepayment > installment.PaidInterests)
                    {
                        calculatedInterest = installment.PaidInterests;
                    }

                    if (installment.PaidCapital == 0
                        && installment.PaidInterests > 0
                        && installment.PaidInterests != installment.InterestsRepayment)
                    {

                        DateTime dateOfInstallment = installment.Number == 1
                                                         ? _contract.StartDate
                                                         : _contract.GetInstallment(installment.Number - 2).ExpectedDate;

                        int d = (date - dateOfInstallment).Days;

                        OCurrency olbBeforePayment =
                            _contract.Events.GetRepaymentEvents().Where(
                                repaymentEvent =>
                                !repaymentEvent.Deleted && repaymentEvent.Date <= dateOfInstallment).
                                Aggregate(
                                    _contract.Amount,
                                    (current, repaymentEvent) => current - repaymentEvent.Principal);

                        calculatedInterest =
                            (olbBeforePayment*Convert.ToDecimal(_contract.InterestRate)/daysInTheYear*d).Value;
                        calculatedInterest = Math.Round(calculatedInterest.Value, roundingPoint,
                                                         MidpointRounding.AwayFromZero);

                        if (installment.PaidInterests < calculatedInterest && actualOlb == olbBeforePayment)
                        {
                            paidInterest = installment.PaidInterests.Value;
                            calculatedInterest = 0;
                            date = dateOfInstallment;
                        }

                        if (installment.PaidInterests < calculatedInterest && actualOlb != olbBeforePayment)
                        {
                            paidInterest = 0;
                            calculatedInterest = installment.PaidInterests;
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

                    int daySpan = (expectedDate - date).Days < 0
                                      ? 0
                                      : (expectedDate - date).Days;

                    if (daySpan == 0 && calculatedInterest > 0)
                    {
                        interestsToRepay += calculatedInterest - installment.PaidInterests;
                    }
                    
                    if (daySpan > 0)
                    {
                        installment.InterestsRepayment = Math.Round((olb * _contract.InterestRate / daysInTheYear * daySpan).Value,
                        roundingPoint, MidpointRounding.AwayFromZero);
                        installment.InterestsRepayment =
                            Math.Round(calculatedInterest.Value + installment.InterestsRepayment.Value,
                                       roundingPoint,
                                       MidpointRounding.AwayFromZero);
                        interestsToRepay += installment.InterestsRepayment - calculatedInterest - paidInterest;
                        date = installment.ExpectedDate;
                    }
                }
            }
            #endregion

            #region Repay installment
            for (int i = 0; i < _contract.NbOfInstallments; i++)
            {
                installment = _contract.GetInstallment(i);

                #region payment on time
                if (installment.ExpectedDate == payDate)
                {
                    OCurrency interestPrepayment = 0;
                    installment.CommissionsUnpaid = 0;
                    installment.FeesUnpaid = 0;
                    installment.PaidFees = 0;

                    //interest
                    if (amountPaid == 0) break;
                    _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref paymentOfInterest,
                                                         ref interestPrepayment);

                    interestsToRepay -= paymentOfInterest;
                   
                    if (interestsToRepay < amountPaid && amountPaid > 0)
                    {
                        if (!totalPayment)
                        {
                            if (
                                AmountComparer.Compare(amountPaid,
                                                       installment.CapitalRepayment - installment.PaidCapital) > 0)
                            {
                                if (installment.CapitalRepayment - installment.PaidCapital == amountPaid)
                                {
                                    OCurrency principalHasToPay = installment.CapitalRepayment - installment.PaidCapital;
                                    installment.PaidCapital = installment.CapitalRepayment;
                                    amountPaid -= principalHasToPay;
                                    paidPrincipal += principalHasToPay;
                                }
                                else
                                {
                                    OCurrency paidCapital = installment.PaidCapital;
                                    movedPrincipal = amountPaid - installment.CapitalRepayment + paidCapital;
                                    installment.PaidCapital = amountPaid + paidCapital;
                                    installment.CapitalRepayment = amountPaid + paidCapital;
                                    paidPrincipal += amountPaid;
                                    amountPaid = 0;
                                }
                            }
                            else
                            {
                                installment.PaidCapital += amountPaid;
                                paidPrincipal += amountPaid;
                                amountPaid = 0;
                            }
                        }
                        else
                        {
                            installment.PaidCapital = amountPaid;
                            installment.CapitalRepayment = amountPaid;
                            paidPrincipal += amountPaid;
                            amountPaid = 0;
                            paymentType = OPaymentType.TotalPayment;
                        }
                    }

                    PaidIstallments.Add(installment);
                    if (installment.PaidCapital > 0)
                    {
                        olb -= paidPrincipal;
                    }
                    else
                    {
                        olb -= installment.CapitalRepayment;
                    }

                    beginDate = installment.ExpectedDate;
                    startNumber = installment.Number;
                    recalculateSchedule = true;

                    principalEvent += paidPrincipal;
                    interestEvent += paymentOfInterest;
                    paidPrincipal = 0;
                    paymentOfInterest = 0;
                    if (amountPaid == 0)
                        break;
                }
                #endregion

                #region payment installmets which is late
                if (installment.ExpectedDate < payDate)
                {
                    OCurrency interestPrepayment = 0;
                    installment.CommissionsUnpaid = 0;
                    installment.FeesUnpaid = 0;
                    installment.PaidFees = 0;
                    recalculateSchedule = true;
                    
                    //interest
                    if (amountPaid == 0) break;
                    _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref paymentOfInterest,
                                                         ref interestPrepayment);

                    interestsToRepay -= paymentOfInterest;

                    if (amountPaid - interestsToRepay > 0 && amountPaid > 0)
                    {
                        if (AmountComparer.Compare(amountPaid, installment.CapitalRepayment - installment.PaidCapital) > 0)
                        {
                            if (amountPaid - interestsToRepay < installment.CapitalRepayment - installment.PaidCapital)
                            {
                                OCurrency paidCapital = installment.PaidCapital;
                                installment.PaidCapital = amountPaid - interestsToRepay + paidCapital;
                                amountPaid -= installment.PaidCapital - paidCapital;
                                paidPrincipal += installment.PaidCapital - paidCapital;
                            }
                            else
                            {
                                OCurrency principalHasToPay = installment.CapitalRepayment - installment.PaidCapital;
                                installment.PaidCapital = installment.CapitalRepayment;
                                amountPaid -= principalHasToPay;
                                paidPrincipal += principalHasToPay;
                            }
                        }
                        else
                        {
                            installment.PaidCapital += amountPaid - interestsToRepay;
                            paidPrincipal += amountPaid - interestsToRepay;
                            amountPaid -= paidPrincipal;
                        }
                    }

                    if (paidPrincipal > 0 || paymentOfInterest > 0)
                        PaidIstallments.Add(installment);

                    if (installment.PaidCapital > 0)
                    {
                        olb -= paidPrincipal;
                    }
                    else
                    {
                        olb -= installment.CapitalRepayment;
                    }

                    startNumber = installment.Number;
                    beginDate = installment.ExpectedDate;

                    principalEvent += paidPrincipal;
                    interestEvent += paymentOfInterest;
                    paidPrincipal = 0;
                    paymentOfInterest = 0;
                    if (amountPaid == 0)
                        break;
                }
                #endregion

                #region early payment
                if (installment.ExpectedDate > payDate)
                {
                    OCurrency interestPrepayment = 0;
                    installment.CommissionsUnpaid = 0;
                    installment.FeesUnpaid = 0;
                    installment.PaidFees = 0;
                    recalculateSchedule = true;
                    //interest
                    if (amountPaid == 0) break;
                    _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref paymentOfInterest,
                                                         ref interestPrepayment);

                    interestsToRepay -= paymentOfInterest;

                    if (amountPaid - interestsToRepay > 0 && amountPaid > 0)
                    {
                        if (AmountComparer.Compare(amountPaid, installment.CapitalRepayment - installment.PaidCapital) > 0)
                        {
                            if (installment.CapitalRepayment - installment.PaidCapital == amountPaid)
                            {
                                OCurrency principalHasToPay = installment.CapitalRepayment - installment.PaidCapital;
                                installment.PaidCapital = installment.CapitalRepayment;
                                amountPaid -= principalHasToPay;
                                paidPrincipal += principalHasToPay;
                            }
                            else
                            {
                                OCurrency paidCapital = installment.PaidCapital;
                                movedPrincipal = amountPaid - installment.CapitalRepayment + paidCapital;
                                installment.PaidCapital = amountPaid + paidCapital;
                                installment.CapitalRepayment = amountPaid + paidCapital;
                                paidPrincipal += amountPaid;
                                amountPaid = 0;
                            }
                        }
                        else
                        {
                            installment.PaidCapital += amountPaid;
                            paidPrincipal += amountPaid;
                            amountPaid = 0;
                        }
                    }

                    if (paidPrincipal > 0)
                        startNumber = installment.Number;

                    if (installment.CapitalRepayment - installment.PaidCapital > 0
                        && installment.CapitalRepayment > 0)
                    {
                        startNumber = installment.Number - 1;
                    }

                    beginDate = startNumber > 0 ? installment.ExpectedDate : _contract.StartDate;

                    if (installment.PaidCapital > 0)
                    {
                        olb -= paidPrincipal;
                    }
                    else
                    {
                        if (startNumber == installment.Number)
                            olb -= installment.CapitalRepayment;
                    }

                    if (paidPrincipal > 0 || paymentOfInterest > 0)
                        PaidIstallments.Add(installment);

                    principalEvent += paidPrincipal;
                    interestEvent += paymentOfInterest;
                    paidPrincipal = 0;
                    paymentOfInterest = 0;

                    if (installment.CapitalRepayment > 0 
                        && installment.CapitalRepayment - installment.PaidCapital == 0 
                        && olb > 0)
                    {
                        OCurrency paidInterests = installment.PaidInterests;

                        int daySpan = (installment.ExpectedDate - payDate).Days < 0
                                          ? 0
                                          : (installment.ExpectedDate - payDate).Days;

                        installment.InterestsRepayment = paidInterests.Value +
                            Math.Round((olb*_contract.InterestRate / daysInTheYear*daySpan).Value
                                       , roundingPoint, MidpointRounding.AwayFromZero);
                    }

                    if (amountPaid == 0)
                        break;
                }
                #endregion
            }
            #endregion

            //recalculate installmets after repayment
            if (recalculateSchedule)
            {
                OCurrency o = _contract.CalculateActualOlb();
                bool isCapitalRemoved = false;
                bool isInterestCalculated = false;
               
                for (int i = 0; i < _contract.NbOfInstallments; i++)
                {
                    installment = _contract.GetInstallment(i);
                    if (installment.Number > startNumber)
                    {
                        OCurrency remainingPrincipal = 0;
                        Installment priorInstallment = null;
                        Installment priorInstallment1 = null;

                        if(installment.Number > 1)
                            priorInstallment =  _contract.GetInstallment(installment.Number - 2);
                        
                        if (installment.Number > 2)
                            priorInstallment1 = _contract.GetInstallment(installment.Number - 3);

                        if (priorInstallment != null 
                            && priorInstallment.CapitalRepayment - priorInstallment.PaidCapital > 0
                            && priorInstallment.PaidCapital > 0
                            && !isCapitalRemoved)
                        {
                            remainingPrincipal = priorInstallment.CapitalRepayment - priorInstallment.PaidCapital;
                            isCapitalRemoved = true;
                        }

                        if (installment.CapitalRepayment - installment.PaidCapital > 0
                            && installment.PaidCapital > 0
                            && !isCapitalRemoved)
                        {
                            remainingPrincipal = -1 * installment.PaidCapital.Value;
                            isCapitalRemoved = true;
                            beginDate = payDate;
                        }
                        // balloon case /////////////////////
                        if (installment.CapitalRepayment == 0
                            && installment.PaidCapital == 0
                            && installment.PaidInterests == 0)
                        {
                            beginDate = installment.Number > 1 ? priorInstallment.ExpectedDate : _contract.StartDate;
                            isInterestCalculated = false;
                            isCapitalRemoved = true;
                        }
                        /////////////////////////////
                        if (priorInstallment != null
                            && priorInstallment1 != null
                            && priorInstallment.InterestHasToPay == 0
                            && installment.PaidCapital == 0
                            && priorInstallment1.PaidCapital > 0
                            && priorInstallment1.CapitalRepayment - priorInstallment1.PaidCapital > 0
                            && !isCapitalRemoved)
                        {
                            olb = o;
                            remainingPrincipal = priorInstallment.CapitalRepayment + priorInstallment1.CapitalRepayment -
                                                 priorInstallment1.PaidCapital;
                            isCapitalRemoved = true;
                        }

                        if (installment.InterestsRepayment == 0)
                            o = olb;

                        installment.OLB = Math.Round(olb.Value, roundingPoint);

                        if (installment.ExpectedDate > payDate 
                            && installment.PaidInterests > 0
                            && olb == actualOlb)
                        {
                            beginDate = installment.Number > 1 ? priorInstallment.ExpectedDate : _contract.StartDate;
                        }

                        DateTime expectedDate = installment.ExpectedDate;
                        //in case very late repayment
                        if(installment.Number == _contract.InstallmentList.Count 
                            && installment.ExpectedDate < payDate
                            && installment.PaidCapital ==0)
                        {
                            expectedDate = payDate;
                            beginDate = priorInstallment.ExpectedDate;
                        }

                        int days = (expectedDate - beginDate).Days;

                        if (installment.PaidInterests == installment.InterestsRepayment && days < 0)
                        {
                            days = 0;
                        }

                        OCurrency interestPayment =
                            (o * Convert.ToDecimal(_contract.InterestRate) / daysInTheYear * days).Value;
                        

                        if (olb == actualOlb && !isInterestCalculated)
                        {
                            installment.InterestsRepayment =
                                Math.Round(interestPayment.Value, roundingPoint, MidpointRounding.AwayFromZero);
                            isInterestCalculated = true;
                        }
                        else
                        {
                            installment.InterestsRepayment +=
                                Math.Round(interestPayment.Value, roundingPoint, MidpointRounding.AwayFromZero);
                        }

                        if (installment.PaidCapital == 0 && movedPrincipal > 0)
                        {
                            movedPrincipal -= installment.CapitalRepayment;
                            if (movedPrincipal > 0)
                            {
                                installment.CapitalRepayment = 0;
                            }
                            else
                            {
                                installment.CapitalRepayment = installment.CapitalRepayment -
                                                               (installment.CapitalRepayment + movedPrincipal);
                            }
                        }
                        
                        if(!totalPayment)
                            olb -= installment.CapitalRepayment + remainingPrincipal;
                        else
                            installment.CapitalRepayment = 0;

                        if (installment.PaidInterests != installment.InterestsRepayment && days >= 0)
                        {
                            beginDate = installment.ExpectedDate;
                        }
                        o = olb;
                    }
                    else
                    {
                        beginDate = payDate >= installment.ExpectedDate ? payDate : installment.ExpectedDate;
                    }
                }
            }

            if (installment != null)
                for (int i = installment.Number; i < _contract.NbOfInstallments; i++)
                {
                    _contract.GetInstallment(i).FeesUnpaid = 0;
                }
        }
    }
}
