// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.Accounting;
using OpenCBS.CoreDomain.FundingLines;
using OpenCBS.Enums;
using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Products
{
    public interface IProduct
    {
        int Id { get; set; }
        bool Delete { get; set; }
        string Name { get; set; }
        string Code { get; set; }
        char ClientType { get; set; }
        OProductTypes ProductType { get; }
        FundingLine FundingLine { get; set; }
        Currency Currency { get; set; }
        OCurrency Amount { get; set; }
        OCurrency AmountMin { get; set; }
        OCurrency AmountMax { get; set; }
        decimal? InterestRate { get; set; }
        decimal? InterestRateMin { get; set; }
        decimal? InterestRateMax { get; set; }
    }
}
