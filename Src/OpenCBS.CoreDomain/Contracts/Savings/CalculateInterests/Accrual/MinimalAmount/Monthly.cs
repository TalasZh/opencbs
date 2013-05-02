// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using OpenCBS.Shared;
using System;
using OpenCBS.CoreDomain.Events.Saving;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Accrual.MinimalAmount
{
    public class Monthly : ICalculateInterests
    {
        private SavingBookContract _saving;
        private User _user;

        public Monthly(SavingBookContract pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pClosureDate)
        {
            List<SavingInterestsAccrualEvent> listInterestsAccrualEvent = new List<SavingInterestsAccrualEvent>();

            DateTime lastClosureDate = _saving.GetLastAccrualDate();

            while (DateCalculationStrategy.DateCalculationMonthly(lastClosureDate, pClosureDate))
            {
                DateTime accrualDate = new DateTime(lastClosureDate.AddMonths(1).Year, lastClosureDate.AddMonths(1).Month, 01, 
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                listInterestsAccrualEvent.Add(GetInterests(lastClosureDate, accrualDate));

                lastClosureDate = lastClosureDate.AddMonths(1);
            }

            return listInterestsAccrualEvent;
        }

        private SavingInterestsAccrualEvent GetInterests(DateTime pLastClosureDate, DateTime pClosureDate)
        {
            double interestRate = _saving.InterestRate;

            OCurrency minimalAmount = _saving.GetBalanceMin(pLastClosureDate);
            DateTime currentDate = pLastClosureDate.AddDays(1); 

            while (currentDate < pClosureDate)
            {
                OCurrency amountAtCurrentDate = _saving.GetBalanceMin(currentDate);

                if (minimalAmount > amountAtCurrentDate)
                    minimalAmount = amountAtCurrentDate;

                currentDate = currentDate.AddDays(1);
            }

            OCurrency interests = interestRate * minimalAmount;

            return new SavingInterestsAccrualEvent
            {
                Amount = interests,
                Date = pClosureDate,
                Fee = 0,
                User = _user, 
                Cancelable = true,
                ProductType = _saving.Product.GetType(),
                Description = _saving.Code,
                Branch = _saving.Branch,
                Currency = _saving.Product.Currency,
                ContracId = _saving.Id
            };
        }
    }
}
