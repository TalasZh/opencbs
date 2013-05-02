// LICENSE PLACEHOLDER

using System;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Clients
{
	/// <summary>
	/// Summary description for Guarantor.
    /// </summary>
    [Serializable]
	public class Guarantor
	{
        public IClient Tiers { get; set; }
	    public OCurrency Amount { get; set; }
        public String Description { get; set; }
	}
}
