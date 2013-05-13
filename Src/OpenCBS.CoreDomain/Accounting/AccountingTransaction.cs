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

namespace OpenCBS.CoreDomain.Accounting
{
	/// <summary>
	/// Description r�sum�e de JournalEntry.
    /// </summary>
    [Serializable]
	public class AccountingTransaction
	{
	    private List<Booking> _bookings;

	    public int Id { get; set; }
	    public User User { get; set; }
	    public DateTime Date { get; set; }
        public DateTime ExportedDate { get; set; }

		public AccountingTransaction()
		{
            _bookings = new List<Booking>();
		}

	    public List<Booking> Bookings
		{
            get { return _bookings; }
            set { _bookings = value; }
		}

	    public void AddBooking(Booking pBooking)
		{
            _bookings.Add(pBooking);
		}

		public Booking GetBooking(int rank)
		{
            return rank < _bookings.Count ? _bookings[rank] : null;
		}

		public void Merge(AccountingTransaction movmentSet)
		{
		    foreach (Booking elem in movmentSet.Bookings)
		        AddBooking(elem);
		}
	}
}
