using OpenCBS.CoreDomain.FundingLines;

namespace OpenCBS.Services.Rules
{
    public class Context
    {
       private FundingLineEvent _fundingLineEvent;
       private IStrategyFundingLineEvent _strategyFundingLine;

        public Context()
        {
        }
        public Context(IStrategyFundingLineEvent strategy)
        {
           _strategyFundingLine = strategy;
        }

       public void SetEventFundingLine(FundingLineEvent e)
        {
           _fundingLineEvent = e;
        }

       public void ValidateRulesForFundingLine()
        {
           _strategyFundingLine.ApplyRules(_fundingLineEvent);
        }

    }
}
