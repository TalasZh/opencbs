// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.Shared
{
	/// <summary>
	/// Summary description for PublicHolidaysComparer.
    /// </summary>
    /// 
    class DateTimeComparer : System.Collections.IComparer
    {
        private bool _sortDescending = false;

        //pass true to the constructor to sort descending
        public DateTimeComparer(bool descending)
        {
            _sortDescending = descending;
        }

        public int Compare(object x, object y)
        {
            DateTime dateA = (DateTime)x;
            DateTime dateB = (DateTime)y;

            if (_sortDescending)
                return dateA.CompareTo(dateB);

            return dateB.CompareTo(dateA);
        }

	    public int Compare(DateTime x, DateTime y)
	    {
	        throw new NotImplementedException();
	    }
    }

    [Serializable]
	public class PublicHolidaysComparer : System.Collections.IComparer
	{
		public PublicHolidaysComparer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public int Compare(object x, object y)
		{
			DateTime dateA = (DateTime)x;
			DateTime dateB = (DateTime)y;

			return dateA.CompareTo(dateB);
		}
	}
}
