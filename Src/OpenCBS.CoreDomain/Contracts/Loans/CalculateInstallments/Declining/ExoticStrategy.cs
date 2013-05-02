// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Interfaces;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Declining
{
    /// <summary>
    /// Summary description for ExoticStrategy.
    /// </summary>
    [Serializable]
    public class ExoticStrategy : ICalculateInstallments
    {
        //private readonly ICalculateInstallments _iCI;
        private readonly Loan _contract;
        private readonly int _roundingPoint;
        private readonly ApplicationSettings _generalSettings;
        
        public ExoticStrategy(Loan pContract, ApplicationSettings pGeneralSettings)
        {
            _roundingPoint = pContract.UseCents ? 2 : 0;
            _contract = pContract;
            _generalSettings = pGeneralSettings;
        }

        public List<Installment> CalculateInstallments(bool changeDate)
        {
            List<Installment> schedule = new List<Installment>();
            OCurrency olb = _contract.Amount;

            for (int number = 1; number <= _contract.NbOfInstallments; number++)
            {
                Installment installment = new Installment { Number = number, FeesUnpaid = 0 };

                if (changeDate)
                {
                    installment.ExpectedDate = _contract.CalculateInstallmentDate(_contract.AlignDisbursementDate, installment.Number);
                }

                installment.OLB = olb;
                installment.InterestsRepayment = Math.Round(olb.Value * Convert.ToDecimal(_contract.InterestRate), _roundingPoint);

                if (number <= _contract.GracePeriod)
                {
                    if (!_contract.Product.ChargeInterestWithinGracePeriod)
                        installment.InterestsRepayment = 0;

                    installment.CapitalRepayment = 0;
                }
                else
                {
                    ExoticInstallment exoInstallment = _contract.Product.ExoticProduct.GetExoticInstallment(number - _contract.GracePeriod.Value - 1);
                    OCurrency capital = Convert.ToDecimal(exoInstallment.PrincipalCoeff) * _contract.Amount.Value;

                    installment.CapitalRepayment = Math.Round(capital.Value, _roundingPoint);
                    olb -= Math.Round(capital.Value, _roundingPoint);
                    if (installment.Number == _contract.NbOfInstallments)
                        installment.CapitalRepayment += olb;
                }

                if (installment.Number == 1 && _generalSettings.PayFirstInterestRealValue)
                {
                    installment.InterestsRepayment = Math.Round(installment.InterestsRepayment.Value / _contract.NbOfDaysInTheInstallment * (_contract.FirstInstallmentDate - _contract.StartDate).Days, _roundingPoint);
                }

                schedule.Add(installment);
            }
            return schedule;
        }
    }
}
