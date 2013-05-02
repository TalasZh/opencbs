// LICENSE PLACEHOLDER

using System;
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Products.Collaterals
{
    [Serializable]
    public class CollateralProduct : ICollateralProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Delete { get; set; }
        public List<CollateralProperty> Properties { get; set; }
    }
}
