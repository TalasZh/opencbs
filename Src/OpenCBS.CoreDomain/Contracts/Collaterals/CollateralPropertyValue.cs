// LICENSE PLACEHOLDER

using System;
using OpenCBS.CoreDomain.Products.Collaterals;

namespace OpenCBS.CoreDomain.Contracts.Collaterals
{
    [Serializable]
	public class CollateralPropertyValue
    {
        public int Id { get; set; }
        public CollateralProperty Property { get; set; }
        public string Value { get; set; }
    }
}
