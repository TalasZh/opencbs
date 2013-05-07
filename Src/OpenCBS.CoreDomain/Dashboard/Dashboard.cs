
using System.Collections.Generic;

namespace OpenCBS.CoreDomain.Dashboard
{
    public class Dashboard
    {
        public List<Action> Actions { get; private set; }
        public List<ActionStat> ActionStats { get; private set; }

        public Dashboard()
        {
            Actions = new List<Action>();
            ActionStats = new List<ActionStat>();
        }

        public decimal Olb { get; set; }
        public decimal Par1To30 { get; set; }
        public decimal Par31To60 { get; set; }
        public decimal Par61To90 { get; set; }
        public decimal Par91To180 { get; set; }
        public decimal Par181To365 { get; set; }
        public decimal Par365 { get; set; }
        public decimal Par
        {
            get { return Par1To30 + Par31To60 + Par61To90 + Par91To180 + Par181To365 + Par365; }
        }

        public List<PortfolioLine> PortfolioLines
        {
            get 
            { 
                var result = new List<PortfolioLine>();
                result.Add(new PortfolioLine
                {
                    Name = "Performing",
                    Amount = Olb - Par,
                    Quantity = 0,
                });
                result.Add(new PortfolioLine
                {
                    Name = "PAR 1-30",
                    Amount = Par1To30,
                    Quantity = 0,
                });
                result.Add(new PortfolioLine
                {
                    Name = "PAR 31-60",
                    Amount = Par31To60,
                    Quantity = 0,
                });
                result.Add(new PortfolioLine
                {
                    Name = "PAR 61-90",
                    Amount = Par61To90,
                    Quantity = 0,
                });
                result.Add(new PortfolioLine
                {
                    Name = "PAR 91-180",
                    Amount = Par91To180,
                    Quantity = 0,
                });
                result.Add(new PortfolioLine
                {
                    Name = "PAR 181-365",
                    Amount = Par181To365,
                    Quantity = 0,
                });
                result.Add(new PortfolioLine
                {
                    Name = "PAR >365",
                    Amount = Par365,
                    Quantity = 0,
                });
                result.Add(new PortfolioLine
                {
                    Name = "Total",
                    Amount = Olb,
                    Quantity = 0,
                });
                return result;
            }
        }
    }
}
