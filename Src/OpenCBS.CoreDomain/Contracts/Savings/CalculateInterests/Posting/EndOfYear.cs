using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Posting
{
    public class EndOfYear : IPostingInterests
    {
        private ISavingsContract _saving;
        private User _user;

        public EndOfYear(ISavingsContract pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime postingDate)
        {
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();

            DateTime lastPostingDate = _saving.GetLastPostingDate();

            while (DateCalculationStrategy.DateCalculationYearly(lastPostingDate, postingDate))
            {
                DateTime currentPostingDate = new DateTime(lastPostingDate.AddYears(1).Year, 01, 01, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                
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
                
                lastPostingDate = lastPostingDate.AddYears(1);
            }
            
            return list;
        }
    }
}
