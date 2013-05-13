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

namespace OpenCBS.Shared
{
    [Serializable]
	public class NonWorkingDateSingleton
	{
        private static readonly IDictionary<string, NonWorkingDateSingleton> _nonWorkingDatesList = new Dictionary<string, NonWorkingDateSingleton>();
        private Dictionary<DateTime, string> _publicHolidays;


        public static void SuppressRemotingInfos(string pMd5)
        {
            if (_nonWorkingDatesList.ContainsKey(pMd5))
            {
                _nonWorkingDatesList.Remove(pMd5);
            }
        }

		private NonWorkingDateSingleton()
		{
            _publicHolidays = new Dictionary<DateTime, string>();
		}

        public static NonWorkingDateSingleton GetInstance(string pMd5)
        {
            if (!_nonWorkingDatesList.ContainsKey(pMd5))
                _nonWorkingDatesList.Add(pMd5, new NonWorkingDateSingleton());

            return _nonWorkingDatesList[pMd5];
        }

        public Dictionary<DateTime, string> PublicHolidays
		{
			get{return _publicHolidays;}
			set{_publicHolidays = value;}
		}

        public int WeekEndDay1 { get; set; }

        public int WeekEndDay2 { get; set; }

        public DateTime GetTheNearestValidDate(DateTime pDate, bool pIsIncrementalDuringDayOff, bool pDoNotSkipNonWorkingDays, bool ToCheckDoNotSkipNonWorkingDays)
        {
            foreach (DateTime publicHoliday in _publicHolidays.Keys)
            {
                if (pDate.Equals(publicHoliday))
                    pDate = pIsIncrementalDuringDayOff ? pDate.AddDays(1) : pDate.AddDays(-1);
            }

            if (!pDoNotSkipNonWorkingDays && ToCheckDoNotSkipNonWorkingDays)
            {
                if (!pDate.DayOfWeek.Equals((DayOfWeek) WeekEndDay1) && !pDate.DayOfWeek.Equals((DayOfWeek) WeekEndDay2))
                    return pDate;
            }
            else
            {
                return pDate;
            }

            pDate = pIsIncrementalDuringDayOff ? pDate.AddDays(1) : pDate.AddDays(-1);
            return GetTheNearestValidDate(pDate, pIsIncrementalDuringDayOff, pDoNotSkipNonWorkingDays, ToCheckDoNotSkipNonWorkingDays);
        }

 	}
}
