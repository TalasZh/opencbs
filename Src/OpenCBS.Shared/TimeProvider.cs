// LICENSE PLACEHOLDER

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
