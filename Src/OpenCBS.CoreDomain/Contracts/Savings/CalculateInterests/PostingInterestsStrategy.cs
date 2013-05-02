// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests
{
    public class PostingInterestsStrategy : IPostingInterests
    {
        private IPostingInterests _ipi;

        public PostingInterestsStrategy(ISavingsContract pSaving, User pUser, int pWeekEndDay2)
        {
            if (((SavingBookContract)pSaving).Product.InterestFrequency == OSavingInterestFrequency.EndOfYear)
                _ipi = new Posting.EndOfYear(pSaving, pUser);
            else if (((SavingBookContract)pSaving).Product.InterestFrequency == OSavingInterestFrequency.EndOfMonth)
                _ipi = new Posting.EndOfMonth((SavingBookContract)pSaving, pUser);
            else if (((SavingBookContract)pSaving).Product.InterestFrequency == OSavingInterestFrequency.EndOfWeek)
                _ipi = new Posting.EndOfWeek((SavingBookContract)pSaving, pUser, pWeekEndDay2);
            else if (((SavingBookContract)pSaving).Product.InterestFrequency == OSavingInterestFrequency.EndOfDay ||
                       ((SavingBookContract)pSaving).UseTermDeposit
                        )
                _ipi = new Posting.PostingMethods((SavingBookContract)pSaving, pUser);
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime postingDate)
        {
            return _ipi.PostingInterests(postingDate);
        }
    }
}
