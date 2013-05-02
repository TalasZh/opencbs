// LICENSE PLACEHOLDER

namespace OpenCBS.CoreDomain.Products.Collaterals
{
    public interface ICollateralProduct
    {
        int Id { get; set; }
        bool Delete { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
