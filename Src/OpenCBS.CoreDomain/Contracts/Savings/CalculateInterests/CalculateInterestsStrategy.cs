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
using OpenCBS.Enums;
using OpenCBS.CoreDomain.Events.Saving;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Contracts.Savings.CalculateInterests
{
    public class CalculateInterestsStrategy : ICalculateInterests
    {
        private readonly ICalculateInterests _ici;

        public CalculateInterestsStrategy(ISavingsContract pSaving, User pUser, int pWeekEndDay2)
        {
            if (((SavingBookContract)pSaving).Product.InterestBase == OSavingInterestBase.Daily)
                _ici = new Accrual.Daily(pSaving, pUser);
            else if (((SavingBookContract)pSaving).Product.InterestBase == OSavingInterestBase.Monthly)
            {
                if (((SavingBookContract)pSaving).Product.CalculAmountBase == OSavingCalculAmountBase.MinimalAmount)
                    _ici = new Accrual.MinimalAmount.Monthly((SavingBookContract)pSaving, pUser);
            }
            else if (((SavingBookContract)pSaving).Product.InterestBase == OSavingInterestBase.Weekly)
            {
                if (((SavingBookContract)pSaving).Product.CalculAmountBase == OSavingCalculAmountBase.MinimalAmount)
                    _ici = new Accrual.MinimalAmount.Weekly((SavingBookContract)pSaving, pUser, pWeekEndDay2);
            }
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(System.DateTime closureDate)
        {
            return _ici.CalculateInterest(closureDate);
        }
    }
}
