// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using System.Linq;
using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Posting
{
    public class EndOfMonth : IPostingInterests
    {
        private SavingBookContract _saving;
        private User _user;

        public EndOfMonth(SavingBookContract pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime postingDate)
        {
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();

            DateTime lastPostingDate = _saving.GetLastPostingDate();
            DateTime currentPostingDate;

            while (DateCalculationStrategy.DateCalculationMonthly(lastPostingDate, postingDate))
            {
                currentPostingDate = new DateTime(lastPostingDate.AddMonths(1).Year, lastPostingDate.AddMonths(1).Month, 01, 
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                OCurrency interestsToPost =
                    _saving.Events.Where(
                        savEvent =>
                        savEvent is SavingInterestsAccrualEvent && savEvent.Date <= currentPostingDate &&
                        savEvent.Date >= lastPostingDate).Aggregate<SavingEvent, OCurrency>(0,
                                                                                            (current, savEvent) =>
                                                                                            current +
                                                                                            savEvent.Amount.Value);

                list.Add(new SavingInterestsPostingEvent
                             {
                    Date = currentPostingDate,
                    Amount = interestsToPost,
                    Description = string.Format("Posting interests for period : {0:d} to {1:d} : {2}", lastPostingDate, currentPostingDate, _saving.Code),
                    User = _user, 
                    Cancelable = true,
                    ProductType = _saving.Product.GetType(),
                    Branch = _saving.Branch,
                    Currency = _saving.Product.Currency,
                    ContracId = _saving.Id
                });

                lastPostingDate = lastPostingDate.AddMonths(1);
            }

            return list;
        }
    }
}
