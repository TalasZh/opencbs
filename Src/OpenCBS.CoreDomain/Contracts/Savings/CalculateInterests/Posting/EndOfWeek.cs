// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Posting
{
    public class EndOfWeek : IPostingInterests
    {
        private SavingBookContract _saving;
        private User _user;
        private int _weekEndDay2;

        public EndOfWeek(SavingBookContract pSaving, User pUser, int pWeekEndDay2)
        {
            _saving = pSaving;
            _user = pUser;
            _weekEndDay2 = pWeekEndDay2;
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime postingDate)
        {
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();

            DateTime lastPostingDate = _saving.GetLastPostingDate();
            DateTime currentPostingDate;

            while (DateCalculationStrategy.DateCalculationWeekly(lastPostingDate, postingDate, _weekEndDay2))
            {
                currentPostingDate = DateCalculationStrategy.GetNextWeekly(lastPostingDate, _weekEndDay2);
                currentPostingDate = new DateTime(currentPostingDate.Year, currentPostingDate.Month, currentPostingDate.Day,
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                
                OCurrency interestsToPost = 0;
                foreach (SavingEvent savEvent in _saving.Events)
                {
                    if (savEvent is SavingInterestsAccrualEvent
                        && savEvent.Date <= currentPostingDate
                        && savEvent.Date > lastPostingDate)
                    {
                        interestsToPost += savEvent.Amount.Value;
                    }
                }

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

                lastPostingDate = currentPostingDate;
            }

            return list;
        }
    }
}
