// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.CoreDomain.Events.Loan;

namespace OpenCBS.CoreDomain.Events
{
	/// <summary>
	/// Summary description for EventComparer.
    /// </summary>
    [Serializable]
    public class EventComparer : IComparer<Event>
	{
	    private bool _ByDate;
		public EventComparer(bool byDate)
		{
		    _ByDate = byDate;
			//
			// TODO: Add constructor logic here
			//
		}

		public int Compare(Event x, Event y)
		{
            if (_ByDate)
            {
                return x.Date.CompareTo(y.Date);
            }
		    
            if (x.Id > y.Id) return 1;
		    if (x.Id == y.Id) return 0;
		    return -1;
		}
	}
}
