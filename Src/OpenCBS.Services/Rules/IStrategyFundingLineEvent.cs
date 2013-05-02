// LICENSE PLACEHOLDER

using OpenCBS.CoreDomain.FundingLines;

namespace OpenCBS.Services.Rules
{
   public interface IStrategyFundingLineEvent
   {
        void ApplyRules(FundingLineEvent e);
   }  
}
