// LICENSE PLACEHOLDER

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
