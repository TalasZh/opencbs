using Octopus.CoreDomain.FundingLines;

namespace Octopus.Services.Rules
{
   public interface IStrategyFundingLineEvent
   {
        void ApplyRules(FundingLineEvent e);
   }  
}
