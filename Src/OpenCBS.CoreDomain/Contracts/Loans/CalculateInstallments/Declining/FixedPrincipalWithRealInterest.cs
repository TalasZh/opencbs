using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Interfaces;
using OpenCBS.CoreDomain.Contracts.Loans.Installments;
using OpenCBS.Shared;
using OpenCBS.Shared.Settings;

namespace OpenCBS.CoreDomain.Contracts.Loans.CalculateInstallments.Declining
{
    class FixedPrincipalWithRealInterest : ICalculateInstallments
    {
        private readonly Loan _contract;
        private readonly int _roundingPoint;
        private readonly ApplicationSettings _generalSettings;

        public FixedPrincipalWithRealInterest(Loan pContract, ApplicationSettings pGeneralSettings)
		{
            _contract = pContract;
            _roundingPoint = pContract.UseCents ? 2 : 0;
            _generalSettings = pGeneralSettings;
		}

        private static int GetDaysInAYear(int year)
        {
            int days = 0;
            for (int i = 1; i <= 12; i++)
            {
                days += DateTime.DaysInMonth(year, i);
            }
            return days;
        }

        public List<Installment> CalculateInstallments(bool changeDate)
		{
            List<Installment> schedule = new List<Installment>();
            OCurrency olb = _contract.Amount;

            DateTime date = _contract.AlignDisbursementDate;
            int daysInTheYear = GetDaysInAYear(_contract.AlignDisbursementDate.Date.Year);

            for (int number = 1; number <= _contract.NbOfInstallments; number++)
            {
                Installment installment = new Installment { Number = number, FeesUnpaid = 0 };

                if (changeDate)
                {
                    installment.ExpectedDate = _contract.CalculateInstallmentDate(_contract.AlignDisbursementDate, installment.Number);
                }

                installment.OLB = Math.Round(olb.Value, _roundingPoint);

                int days = (installment.ExpectedDate - date).Days;

                if (installment.Number == 1 && _generalSettings.PayFirstInterestRealValue)
                {
                    installment.InterestsRepayment =
                        Math.Round(olb.Value * Convert.ToDecimal(_contract.InterestRate) / daysInTheYear *
                            (installment.ExpectedDate - _contract.StartDate).Days, _roundingPoint);
                }
                else
                {
                    installment.InterestsRepayment =
                    Math.Round((olb * _contract.InterestRate / daysInTheYear * days).Value,
                        _roundingPoint, MidpointRounding.AwayFromZero);
                }

                if (number <= _contract.GracePeriod)
                {
                    if (!_contract.Product.ChargeInterestWithinGracePeriod)
                        installment.InterestsRepayment = 0;

                    installment.CapitalRepayment = 0;
                }
                else
                {
                    OCurrency principal = olb / (double)(_contract.NbOfInstallments - number + 1);
                    installment.CapitalRepayment = Math.Round(principal.Value, _roundingPoint);
                }

                olb -= installment.CapitalRepayment;
                date = installment.ExpectedDate;
                schedule.Add(installment);
            }
            return schedule;
		}
    }
}
