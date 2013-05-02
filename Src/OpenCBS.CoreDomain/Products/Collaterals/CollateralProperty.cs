// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;
using OpenCBS.Enums;

namespace OpenCBS.CoreDomain.Products.Collaterals
{
    [Serializable]
	public class CollateralProperty
    {
        public int Id { get; set; }
        public OCollateralPropertyTypes Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Collection { get; set; }
    }
}
