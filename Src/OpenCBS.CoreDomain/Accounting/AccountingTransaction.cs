// LICENSE PLACEHOLDER

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
