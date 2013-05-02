using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.CoreDomain.Events;
using OpenCBS.Enums;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.Tranches
{
    [Serializable]
    public struct TrancheOptions
    {
        public DateTime TrancheDate;
        public OCurrency TrancheAmount;
        public int CountOfNewInstallments;
        public decimal InterestRate;
        public bool ApplyNewInterestOnOLB;
        public int Number;
    }

    [Serializable]
    public class Tranche
    {
        private readonly Loan _currentLoan;
        private readonly ApplicationSettings _generalSettings;
        private TrancheEvent _trancheEvent;
        private List<Installment> _previousSchedule = new List<Installment>();
        public Tranche()
        {
        }

        public Tranche(Loan pLoan, ApplicationSettings pGeeneralSettings)
        {
            _currentLoan = pLoan;
            _generalSettings = pGeeneralSettings;
            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                _previousSchedule.Add(installment);
            }
        }

        private void TrancheExtendMaturity(int newInstallments, DateTime pFirstInstallmentDate)
        {
            if (newInstallments > 0)
            {
                List<Installment> listInstallment = new List<Installment>();
                listInstallment.AddRange(_currentLoan.InstallmentList);

                foreach (Installment _installment in _currentLoan.InstallmentList)
                {
                    if (!_installment.IsRepaid)
                    {
                        listInstallment.Remove(_installment);
                    }
                }

                int lastNumber = listInstallment.Count;
                _trancheEvent.StartedFromInstallment = lastNumber;

                for (int i = 1; i <= newInstallments; i++)
                {
                    DateTime expectedDate = _currentLoan.CalculateInstallmentDate(pFirstInstallmentDate, i);
                    Installment installment = new Installment
                    {
                        Number = lastNumber + i,
                        CapitalRepayment = 0,
                        PaidCapital = 0,
                        CommissionsUnpaid = 0,
                        FeesUnpaid = 0,
                        InterestsRepayment = 1,
                        PaidInterests = 0,
                        ExpectedDate = expectedDate
                    };

                    listInstallment.Add(installment);
                }
                _currentLoan.InstallmentList = listInstallment;

                _currentLoan.NbOfInstallments = _currentLoan.InstallmentList.Count;
            }
        }
        
        private void AddFlatTranche(TrancheOptions to)
        {
            OCurrency remainsInterestAmount = 0;
            OCurrency remainsAmount = 0;

            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    if (to.TrancheDate > _currentLoan.InstallmentList[installment.Number - 2].ExpectedDate)
                    {
                        remainsInterestAmount += installment.InterestsRepayment *
                                                 (to.TrancheDate - _currentLoan.InstallmentList[installment.Number - 2].ExpectedDate).Days /
                                                 _currentLoan.NumberOfDaysInTheInstallment(installment.Number, to.TrancheDate);
                    }

                    remainsAmount += installment.CapitalRepayment - installment.PaidCapital;
                }
            }

            OCurrency interestAmount = !to.ApplyNewInterestOnOLB
                                           ? to.TrancheAmount * to.InterestRate +
                                             remainsInterestAmount / to.CountOfNewInstallments
                                           : (to.TrancheAmount + remainsAmount) * to.InterestRate +
                                             remainsInterestAmount / to.CountOfNewInstallments;

            OCurrency generalInterestAmount = interestAmount;

            interestAmount = _currentLoan.UseCents ? interestAmount : Math.Round(interestAmount.Value, 0);

            OCurrency olb = to.TrancheAmount + remainsAmount;

            TrancheExtendMaturity(to.CountOfNewInstallments, to.TrancheDate);

            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    installment.CapitalRepayment = _currentLoan.UseCents
                                                       ? (to.TrancheAmount + remainsAmount) /
                                                         to.CountOfNewInstallments
                                                       : Math.Round(
                                                             ((to.TrancheAmount + remainsAmount) /
                                                              to.CountOfNewInstallments).Value, 0);

                    installment.InterestsRepayment = interestAmount;

                    installment.PaidCapital = 0;
                    installment.PaidInterests = 0;
                    installment.OLB = olb;
                    olb -= installment.CapitalRepayment;
                }
            }

            OCurrency lastInstallmentInterest = _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - 1].InterestsRepayment +
                generalInterestAmount * to.CountOfNewInstallments - interestAmount * to.CountOfNewInstallments;

            _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - 1].InterestsRepayment = _currentLoan.UseCents
                                                                                ? lastInstallmentInterest
                                                                                : Math.Round(
                                                                                      lastInstallmentInterest
                                                                                          .Value, 0);
            _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - 1].CapitalRepayment =
                _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - 1].CapitalRepayment + olb;

            _currentLoan.Amount = _currentLoan.Amount + to.TrancheAmount;
        }

        private void AddFixedPrincipalTranche(TrancheOptions to)
        {
            OCurrency remainsAmount = 0;
//            List<Installment> _previousSchedule = new List<Installment>();
            int numberRemainInstallment = 0;

            foreach (Installment installment in _currentLoan.InstallmentList)
            {
//                previousSchedule.Add(installment);

                if (!installment.IsRepaid)
                {
                    remainsAmount += installment.CapitalRepayment - installment.PaidCapital;
                    numberRemainInstallment++;
                }
            }

            OCurrency olb = to.TrancheAmount;

            OCurrency remainsInterestAmount = _generalSettings.AccountingProcesses == OAccountingProcesses.Accrual
                                                  ? GenerateEvents.Accrual.CalculateRemainingInterests(_currentLoan, to.TrancheDate)
                                                  : GenerateEvents.Cash.CalculateRemainingInterests(_currentLoan, to.TrancheDate);

            remainsInterestAmount = _currentLoan.UseCents ? remainsInterestAmount : Math.Round(remainsInterestAmount.Value, 0, MidpointRounding.AwayFromZero);


            TrancheExtendMaturity(to.CountOfNewInstallments, to.TrancheDate);
            OCurrency remainingOlb = remainsAmount;
            bool itIsFirstNotRepaidInstallment=true;
            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (installment.IsRepaid) continue;
                
                
                if (_previousSchedule.Count >= installment.Number)
                {
                    if (!_previousSchedule[installment.Number - 1].IsRepaid)
                    {
                        installment.CapitalRepayment = CalculateCapitalRepayment(to, installment);

                        if (itIsFirstNotRepaidInstallment)
                        {
                            OCurrency interestAmount = RecalculateInterestForFirstNotPaidInstallment(installment, remainingOlb, to);
                            installment.InterestsRepayment = 
                                CalculateInterestRepaymentWithNewInterest(to, remainsInterestAmount, remainingOlb, olb, interestAmount);
                            itIsFirstNotRepaidInstallment = false;
                        }
                        else
                        {
                            installment.InterestsRepayment = CalculateInterestRepayment(to, remainsInterestAmount, remainingOlb, olb);
                        }
                        
                        installment.OLB = olb + remainingOlb;

                        if (installment.Number!=_previousSchedule.Count)
                            remainingOlb = _previousSchedule[installment.Number].OLB;
                        if (_currentLoan.UseCents)
                            olb -= Math.Round(to.TrancheAmount.Value / to.CountOfNewInstallments, 2, MidpointRounding.AwayFromZero);
                        else
                        {
                            olb -= Math.Round(to.TrancheAmount.Value/to.CountOfNewInstallments, 0, MidpointRounding.AwayFromZero);
                        }

                        installment.PaidCapital = 0;
                        installment.PaidInterests = 0;
                        remainsInterestAmount = 0;
                    }
                }
                else
                {
                    installment.CapitalRepayment = _currentLoan.UseCents
                                                       ? Math.Round(to.TrancheAmount.Value / to.CountOfNewInstallments, 2, MidpointRounding.AwayFromZero)
                                                       : Math.Round(
                                                             (to.TrancheAmount / to.CountOfNewInstallments).Value, 0,
                                                             MidpointRounding.AwayFromZero);
                    
                    CheckAndCorrectSumOfCapitalRepayments(to, installment);
                    installment.InterestsRepayment = olb.Value * to.InterestRate;

                    installment.InterestsRepayment = _currentLoan.UseCents
                                                         ? Math.Round(installment.InterestsRepayment.Value, 2, MidpointRounding.AwayFromZero)
                                                         : Math.Round(installment.InterestsRepayment.Value, 0,
                                                                      MidpointRounding.AwayFromZero);
                        
                    installment.PaidCapital = 0;
                    installment.PaidInterests = 0;
                    installment.OLB = olb;

                    olb -= installment.CapitalRepayment;
                }
            }

            _currentLoan.Amount = _currentLoan.Amount + to.TrancheAmount;
        }

        private void CheckAndCorrectSumOfCapitalRepayments(TrancheOptions to, Installment installment)
        {
            if (installment.Number==_currentLoan.InstallmentList.Count)
            {
                OCurrency sumOfCapitalRepayment = 0;
                foreach (Installment item in _currentLoan.InstallmentList)
                {
                    sumOfCapitalRepayment += item.CapitalRepayment;
                }
                installment.CapitalRepayment += _currentLoan.Amount + to.TrancheAmount - sumOfCapitalRepayment;
            }
        }

        private OCurrency RoundResult(bool useCents, OCurrency amount)
        {
            if (useCents)
            {
                return Math.Round(amount.Value, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(amount.Value, 0, MidpointRounding.AwayFromZero);
            }
        }

        private OCurrency CalculateInterestRepayment(TrancheOptions to, OCurrency remainsInterestAmount, OCurrency remainingOlb, OCurrency olb)
        {
            OCurrency result=0;
            if (to.ApplyNewInterestOnOLB)
            {
               result =(olb + remainingOlb)*to.InterestRate + remainsInterestAmount;
            }
            else
            {
                if (_currentLoan.UseCents)
                {
                    OCurrency previousCapitalRepayment =
                        Math.Round(remainingOlb.Value*_currentLoan.GivenTranches[to.Number - 1].InterestRate.Value,
                                   2, MidpointRounding.AwayFromZero);
                    OCurrency newCapitalRepayment = Math.Round(olb.Value*to.InterestRate, 2,
                                                               MidpointRounding.AwayFromZero);

                    result = newCapitalRepayment + previousCapitalRepayment + remainsInterestAmount;

                }
                else
                {
                    OCurrency previousCapitalRepayment =
                        Math.Round(remainingOlb.Value * _currentLoan.GivenTranches[to.Number - 1].InterestRate.Value,
                                   0, MidpointRounding.AwayFromZero);
                    OCurrency newCapitalRepayment = Math.Round(olb.Value * to.InterestRate, 0,
                                                               MidpointRounding.AwayFromZero);

                    result = newCapitalRepayment + previousCapitalRepayment + remainsInterestAmount;
                }
                
            }
            result = RoundResult(_currentLoan.UseCents, result);
            return result;
        }

        private OCurrency CalculateInterestRepaymentWithNewInterest(TrancheOptions to, OCurrency remainsInterestAmount,  OCurrency remainingOlb, OCurrency olb, OCurrency interestAmount)
        {
            OCurrency result;
            if (to.ApplyNewInterestOnOLB)
                result= (olb + remainingOlb)*to.InterestRate + remainsInterestAmount;
            else
                result = olb * to.InterestRate + interestAmount;
            result = RoundResult(_currentLoan.UseCents, result);
            return result;
        }

        private OCurrency CalculateCapitalRepayment(TrancheOptions to, Installment installment)
        {
            OCurrency result  = to.TrancheAmount/to.CountOfNewInstallments + _previousSchedule[installment.Number - 1].CapitalRepayment;
            result = RoundResult(_currentLoan.UseCents, result);
            return result;
        }

        private OCurrency RecalculateInterestForFirstNotPaidInstallment(Installment installment, OCurrency remainingOlb, TrancheOptions to)
        {
            OCurrency newInterest=0;
            if (installment.ExpectedDate>_previousSchedule[installment.Number-1].ExpectedDate)
            {
                if (installment.Number==1)
                {
                    int daysBeforeTranche = (to.TrancheDate - _currentLoan.StartDate).Days;
                    int oldQuantityOfDays =
                        (_previousSchedule[installment.Number - 1].ExpectedDate - _currentLoan.StartDate).Days;
                    newInterest = remainingOlb * _currentLoan.GivenTranches[to.Number - 1].InterestRate / oldQuantityOfDays * daysBeforeTranche 
                                + _previousSchedule[installment.Number - 1].InterestsRepayment;
                }
                else
                {
                    int daysBeforeTranche =
                        (to.TrancheDate - _previousSchedule[installment.Number - 2].ExpectedDate).Days;
                    int oldQuantityOfDays =
                        (_previousSchedule[installment.Number - 1].ExpectedDate -
                         _previousSchedule[installment.Number - 2].ExpectedDate).Days;
                    newInterest = remainingOlb * _currentLoan.GivenTranches[to.Number - 1].InterestRate / oldQuantityOfDays * daysBeforeTranche 
                                + _previousSchedule[installment.Number - 1].InterestsRepayment;
                }
            }
            else
            {
                newInterest = remainingOlb*_currentLoan.GivenTranches[to.Number - 1].InterestRate;
            }
            newInterest = RoundResult(_currentLoan.UseCents, newInterest);
            return newInterest;
        }

        private void AddFixedInstallmentTranche(TrancheOptions to)
        {
            OCurrency remainsAmount = 0;

            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    remainsAmount += installment.CapitalRepayment - installment.PaidCapital;
                }
            }

            OCurrency remainsInterestAmount = _generalSettings.AccountingProcesses == OAccountingProcesses.Accrual
                                                  ? GenerateEvents.Accrual.CalculateRemainingInterests(_currentLoan, to.TrancheDate)
                                                  : GenerateEvents.Cash.CalculateRemainingInterests(_currentLoan, to.TrancheDate);

            remainsInterestAmount = _currentLoan.UseCents ? remainsInterestAmount : Math.Round(remainsInterestAmount.Value, 0, MidpointRounding.AwayFromZero);

            TrancheExtendMaturity(to.CountOfNewInstallments, to.TrancheDate);

            OCurrency olb;
            OCurrency newAmountVpm;
            OCurrency priviousAmountVpm = 0;

            if (to.ApplyNewInterestOnOLB)
            {
                olb = to.TrancheAmount + remainsAmount;

                newAmountVpm = _currentLoan.UseCents
                                   ? _currentLoan.VPM(olb, to.CountOfNewInstallments, to.InterestRate)
                                   : Math.Round(_currentLoan.VPM(olb, to.CountOfNewInstallments, to.InterestRate).Value, 0, MidpointRounding.AwayFromZero);
                remainsAmount = 0;
            }
            else
            {
                olb = to.TrancheAmount;

                newAmountVpm = _currentLoan.UseCents
                                   ? _currentLoan.VPM(olb, to.CountOfNewInstallments, to.InterestRate)
                                   : Math.Round(_currentLoan.VPM(olb, to.CountOfNewInstallments, to.InterestRate).Value, 0,
                                                MidpointRounding.AwayFromZero);
                priviousAmountVpm = _currentLoan.UseCents
                                        ? _currentLoan.VPM(remainsAmount, to.CountOfNewInstallments,
                                              (_currentLoan.GivenTranches[to.Number - 1].InterestRate.Value))
                                        : Math.Round((_currentLoan.VPM(remainsAmount, to.CountOfNewInstallments,
                                                          (_currentLoan.GivenTranches[to.Number - 1].InterestRate.Value))).Value, 0,
                                                     MidpointRounding.AwayFromZero);
            }

            OCurrency _OLB = olb + remainsAmount;

            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    installment.InterestsRepayment = _currentLoan.UseCents
                                   ? Math.Round(olb.Value * to.InterestRate, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(olb.Value * to.InterestRate, 0, MidpointRounding.AwayFromZero);

                    OCurrency interestAmountForPriviousOlb = _currentLoan.UseCents
                                   ? remainsAmount * _currentLoan.GivenTranches[to.Number - 1].InterestRate
                                   : Math.Round((remainsAmount * _currentLoan.GivenTranches[to.Number - 1].InterestRate).Value, 0, MidpointRounding.AwayFromZero);

                    installment.CapitalRepayment = newAmountVpm.Value - installment.InterestsRepayment.Value;

                    installment.InterestsRepayment += _currentLoan.UseCents
                                   ? Math.Round(interestAmountForPriviousOlb.Value, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(interestAmountForPriviousOlb.Value, 0, MidpointRounding.AwayFromZero);

                    olb -= installment.CapitalRepayment;

                    installment.CapitalRepayment = _currentLoan.UseCents
                                   ? Math.Round(installment.CapitalRepayment.Value, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(installment.CapitalRepayment.Value, 0, MidpointRounding.AwayFromZero);


                    OCurrency priviousCapital = priviousAmountVpm - interestAmountForPriviousOlb;

                    installment.CapitalRepayment += _currentLoan.UseCents
                                   ? Math.Round(priviousCapital.Value, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(priviousCapital.Value, 0, MidpointRounding.AwayFromZero);


                    remainsAmount -= priviousCapital;

                    installment.InterestsRepayment += _currentLoan.UseCents
                                   ? Math.Round(remainsInterestAmount.Value, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(remainsInterestAmount.Value, 0, MidpointRounding.AwayFromZero);

                    remainsInterestAmount = 0;
                    installment.PaidCapital = 0;
                    installment.PaidInterests = 0;
                }
            }

            _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - 1].CapitalRepayment +=
                _currentLoan.UseCents
                    ? Math.Round(olb.Value + remainsAmount.Value, 2, MidpointRounding.AwayFromZero)
                    : Math.Round(olb.Value + remainsAmount.Value, 0, MidpointRounding.AwayFromZero);
            
            //OLB calculation
            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    installment.OLB = _OLB;
                    _OLB -= installment.CapitalRepayment;
                }
            }

            _currentLoan.Amount = _currentLoan.Amount + to.TrancheAmount;
            _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - 1].CapitalRepayment +=
                _currentLoan.UseCents
                    ? Math.Round(_OLB.Value, 2, MidpointRounding.AwayFromZero)
                    : Math.Round(_OLB.Value, 0, MidpointRounding.AwayFromZero);
        }

        public TrancheEvent AddTranche(TrancheOptions trancheOptions)
        {
            _trancheEvent = new TrancheEvent
                                {
                                    Date = trancheOptions.TrancheDate,
                                    InterestRate = trancheOptions.InterestRate,
                                    Amount = trancheOptions.TrancheAmount,
                                    Maturity = trancheOptions.CountOfNewInstallments,
                                    StartDate = trancheOptions.TrancheDate,
                                    Number = _currentLoan.GivenTranches.Count
                                };

            trancheOptions.Number = _trancheEvent.Number;

            switch (_currentLoan.Product.LoanType)
            {
                case OLoanTypes.Flat:
                    {
                        AddFlatTranche(trancheOptions);
                    }
                    break;

                case OLoanTypes.DecliningFixedPrincipal:
                    {
                        AddFixedPrincipalTranche(trancheOptions);
                    }
                    break;

                case OLoanTypes.DecliningFixedInstallments:
                    {
                        AddFixedInstallmentTranche(trancheOptions);
                    }
                    break;
            }

            _currentLoan.CalculateStartDates();
            return _trancheEvent;
        }
    }
}
