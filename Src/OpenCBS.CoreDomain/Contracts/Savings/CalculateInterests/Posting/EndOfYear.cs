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
