using Octopus.CoreDomain.Accounting;
using Octopus.CoreDomain.FundingLines;
using Octopus.Enums;
using Octopus.Shared;

namespace Octopus.CoreDomain.Products
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
