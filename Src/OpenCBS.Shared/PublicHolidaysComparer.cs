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
