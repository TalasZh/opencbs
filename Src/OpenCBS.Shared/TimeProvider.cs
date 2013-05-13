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

namespace OpenCBS.Shared
{
	/// <summary>
	/// This class provides the current time to the applicaion.<br/>
	/// By default it uses the system time.<br/>
	/// <br/>
    /// IMPORTANT : DateTime.Today and DateTime.Now must NEVER be used by the application ! (excepts for logs and tech errors) <br/>
    /// </summary>
    [Serializable]
	public class TimeProvider
	{
		static private TimeSpan _timeSpan = TimeSpan.Zero;

	    static private DateTime now
	    {
	        get
	        {
	            return (DateTime.Now - _timeSpan);
	        }
	    }
        
        /// <summary>
        /// Returns application current time.
        /// </summary>
	    public static DateTime Now
	    {
	        get
	        {
	            return now;
            }
	    }

        /// <summary>
        /// Returns application current date.<br/>
        /// (DateTime with time value set to 12:00:00 midnight)
        /// </summary>
	    public static DateTime Today
	    {
	        get
	        {
	            return now.Date;
	        }
	    }
        
        /// <summary>
        /// Sets application's today's date<br/>
        /// Remark : Only day date part is used (hour is ignored).
        /// </summary>
        /// <param name="pTodayDate"></param>
        public static void SetToday(DateTime pTodayDate)
        {
            _timeSpan = DateTime.Today - pTodayDate.Date;
			if (DateChanged != null) DateChanged();
        }

        /// <summary>
        /// Asks application to use system's date.<br/> </summary>
        public static void UseSystemTime()
        {
            _timeSpan = TimeSpan.Zero;
			if (DateChanged != null) DateChanged();
        }

		/// <summary>
		/// Is application using system time
		/// </summary>
		/// <returns></returns>
		public static bool IsUsingSystemTime
		{
			get
			{
				return (_timeSpan == TimeSpan.Zero);
			}
		}

		public delegate void DateChangedHandler();

		public static event DateChangedHandler DateChanged;

	}
}
