// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Contracts.Collaterals
{
    [Serializable]
	public class ContractCollateral
    {
        public int Id { get; set; }
        public List<CollateralPropertyValue> PropertyValues { get; set; }
    }
}
