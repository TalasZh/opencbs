using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using OpenCBS.Shared;
using System;
using OpenCBS.CoreDomain.Events.Saving;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Accrual.MinimalAmount
{
    public class Weekly : ICalculateInterests
    {
        private SavingBookContract _saving;
        private User _user;
        private int _weekEndDay2;

        public Weekly(SavingBookContract pSaving, User pUser, int pWeekEndDay2)
        {
            _saving = pSaving;
            _user = pUser;
            _weekEndDay2 = pWeekEndDay2;
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pClosureDate)
        {
            List<SavingInterestsAccrualEvent> listInterestsAccrualEvent = new List<SavingInterestsAccrualEvent>();

            DateTime lastClosureDate = _saving.GetLastAccrualDate();

            while (DateCalculationStrategy.DateCalculationWeekly(lastClosureDate, pClosureDate, _weekEndDay2))
            {
                DateTime cDate = new DateTime(lastClosureDate.Year, lastClosureDate.Month, lastClosureDate.Day, 
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                DateTime accrualDate = DateCalculationStrategy.GetNextWeekly(cDate, _weekEndDay2);
                listInterestsAccrualEvent.Add(GetInterests(lastClosureDate, accrualDate));
                lastClosureDate = accrualDate;
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

            return new SavingInterestsAccrualEvent()
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
