// LICENSE PLACEHOLDER

using System;

namespace OpenCBS.CoreDomain.Accounting
{
    [Serializable]
	public class ProvisioningRate
    {
        public int Number { get; set; }
        public int NbOfDaysMin { get; set; }
        public int NbOfDaysMax { get; set; }
        public double Rate { get; set; }
	}
}
