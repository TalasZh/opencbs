using System;
using System.Collections.Generic;
using System.Linq;
using Octopus.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using Octopus.CoreDomain.Events.Saving;
using Octopus.Shared;

namespace Octopus.CoreDomain.Contracts.Savings.CalculateInterests.Posting
{
    public class PostingMethods : IPostingInterests
    {
        private SavingBookContract _saving;
        private User _user;

        public PostingMethods(SavingBookContract pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime postingDate)
        {
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();

            if (_saving.UseTermDeposit)
            {
                DateTime lastPostingDate = _saving.GetLastPostingDate();
                OCurrency interestsToPost;

                interestsToPost = _saving.Events.Where(item =>
                                item is SavingInterestsAccrualEvent
                                && item.Date.Date > lastPostingDate
                                && item.Date.Date <= postingDate
                                && item.Deleted == false).Sum(item => item.Amount.Value);


                if (postingDate.Date == _saving.NextMaturity.Value.Date)
                {
                    list.Add(new SavingInterestsPostingEvent
                    {
                        Date =
                            new DateTime(postingDate.Year,
                                         postingDate.Month,
                                         postingDate.Day,
                                         DateTime.Now.Hour,
                                         DateTime.Now.Minute,
                                         DateTime.Now.Second),
                        Description =
                            string.Format("Posting interests for period : {0:d} to {1:d} : {2}",
                                           lastPostingDate,
                                           postingDate, _saving.Code
                                          ),
                        Amount = interestsToPost,
                        User = _user,
                        Cancelable = true,
                        ProductType = _saving.Product.GetType()
                    });
                }
            }
            else
            {
                DateTime lastPostingDate = _saving.GetLastPostingDate();
                DateTime currentPostingDate;

                while (DateCalculationStrategy.DateCalculationDiary(lastPostingDate, postingDate))
                {
                    currentPostingDate = lastPostingDate.AddDays(1);

                    OCurrency interestsToPost = 0;
                    foreach (SavingEvent savEvent in _saving.Events)
                    {
                        if (savEvent is SavingInterestsAccrualEvent
                            && savEvent.Date.Date <= currentPostingDate.Date
                            && savEvent.Date.Date > lastPostingDate.Date)
                        {
                            interestsToPost += savEvent.Amount.Value;
                        }
                    }

                    list.Add(new SavingInterestsPostingEvent
                                 {
                                     Date =
                                         new DateTime(currentPostingDate.Year, currentPostingDate.Month,
                                                      currentPostingDate.Day,
                                                      DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                                     Description =
                                         string.Format("Posting interests for period : {0:d} to {1:d} : {2}",
                                                       lastPostingDate, currentPostingDate, _saving.Code),
                                     Amount = interestsToPost,
                                     User = _user,
                                     Cancelable = true,
                                     ProductType = _saving.Product.GetType(),
                                     Branch = _saving.Branch,
                                     Currency = _saving.Product.Currency,
                                     ContracId = _saving.Id
                                 });

                    lastPostingDate = lastPostingDate.AddDays(1);
                }
            }
            
            return list;
        }
    }
}
