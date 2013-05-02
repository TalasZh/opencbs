// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain
{
	/// <summary>
	/// Summary description for AlertComparer.
    /// </summary>
    [Serializable]
	public class AlertComparer : System.Collections.IComparer
	{
		public int Compare(object x, object y)
		{
			Alert alertA = (Alert)x;
            Alert alertB = (Alert)y;
			
			return DateTime.Compare(alertA.EffectDate,alertB.EffectDate);
		}
	}
}
