// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using OpenCBS.Shared;
using System;
using OpenCBS.CoreDomain.Events.Saving;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Accrual
{
    public class Daily : ICalculateInterests
    {
        private User _user;
        private ISavingsContract _saving;

        public Daily(ISavingsContract pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pClosureDate)
        {
            List<SavingInterestsAccrualEvent> listInterestsAccrualEvent = new List<SavingInterestsAccrualEvent>();

            DateTime lastClosureDate = new DateTime(_saving.GetLastAccrualDate().Year, 
                                                    _saving.GetLastAccrualDate().Month, 
                                                    _saving.GetLastAccrualDate().Day,
                                                    DateTime.Now.Hour, 
                                                    DateTime.Now.Minute, 
                                                    DateTime.Now.Second);

            while (DateCalculationStrategy.DateCalculationDiary(lastClosureDate, pClosureDate))
            {
                if (lastClosureDate.Date == _saving.CreationDate.Date &&
                    listInterestsAccrualEvent.Count == 0)
                    {
                        lastClosureDate= lastClosureDate.AddDays(-1);
                    } 
                lastClosureDate = lastClosureDate.AddDays(1);
                listInterestsAccrualEvent.Add(GetInterests(lastClosureDate));
            }

            return listInterestsAccrualEvent;
        }

        private SavingInterestsAccrualEvent GetInterests(DateTime closureDate)
        {
            OCurrency amount = _saving.GetBalance(closureDate);
            double interestRate = _saving.InterestRate;
            OCurrency interests = interestRate * amount;

            return new SavingInterestsAccrualEvent
                       {
                Amount = interests,
                Date = closureDate,
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
