// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain.Accounting
{
	/// <summary>
	/// Summary description for ExchangeRate.
    /// </summary>
    [Serializable]
	public class ExchangeRate
	{
	    public DateTime Date { get; set; }
	    public double Rate { get; set; }
        public Currency Currency { get; set; }
	}
}
