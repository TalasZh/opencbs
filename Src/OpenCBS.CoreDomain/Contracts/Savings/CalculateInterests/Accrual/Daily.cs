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
