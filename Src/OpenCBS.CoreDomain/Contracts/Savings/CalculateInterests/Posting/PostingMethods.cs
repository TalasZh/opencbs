// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.Collections.Generic;
using System.Linq;
using OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using OpenCBS.CoreDomain.Events.Saving;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests.Posting
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
